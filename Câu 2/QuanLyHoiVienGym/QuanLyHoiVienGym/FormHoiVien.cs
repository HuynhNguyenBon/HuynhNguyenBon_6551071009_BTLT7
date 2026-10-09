using Microsoft.EntityFrameworkCore;
using QuanLyHoiVienGym.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyHoiVienGym
{
    public partial class FrmHoiVien : Form
    {
        public FrmHoiVien()
        {
            InitializeComponent();
        }
        private async Task LoadHoiVienAsync()
        {
            using var db = new QuanLyHoiVienGym_EFContext();

            var danhSach = await db.HoiViens
                .AsNoTracking()
                .OrderBy(x => x.MaHv)
                .ToListAsync();

            dgvHoiVien.DataSource = danhSach;

            // Ẩn cột ngày đăng ký nếu không muốn hiển thị
            if (dgvHoiVien.Columns["NgayDangKy"] != null)
                dgvHoiVien.Columns["NgayDangKy"].Visible = false;

            // Đổi tiêu đề cột
            if (dgvHoiVien.Columns["MaHv"] != null)
                dgvHoiVien.Columns["MaHv"].HeaderText = "Mã HV";

            if (dgvHoiVien.Columns["HoTen"] != null)
                dgvHoiVien.Columns["HoTen"].HeaderText = "Họ tên";

            if (dgvHoiVien.Columns["Sdt"] != null)
                dgvHoiVien.Columns["Sdt"].HeaderText = "SĐT";

            if (dgvHoiVien.Columns["HangThanhVien"] != null)
                dgvHoiVien.Columns["HangThanhVien"].HeaderText = "Hạng thành viên";

            if (dgvHoiVien.Columns["TrangThai"] != null)
                dgvHoiVien.Columns["TrangThai"].HeaderText = "Trạng thái";
        }
        private void LamMoi()
        {
            txtHoTen.Clear();
            txtSDT.Clear();
            txtEmail.Clear();

            dtpNgaySinh.Value = DateTime.Today;

            cboHang.SelectedIndex = 0;

            rdoNam.Checked = true;
            chkTrangThai.Checked = true;

            dgvHoiVien.ClearSelection();
        }
        private async void FrmHoiVien_Load(object sender, EventArgs e)
        {
            cboHang.Items.Clear();
            cboHang.Items.AddRange(
                new object[] { "Basic", "VIP", "Premium" });

            cboHang.SelectedIndex = 0;

            cboLocHang.Items.Clear();
            cboLocHang.Items.AddRange(
                new object[] { "Tất cả", "Basic", "VIP", "Premium" });

            cboLocHang.SelectedIndex = 0;

            await LoadHoiVienAsync();
            LamMoi();
        }
        private bool KiemTraDuLieu()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!");
                txtHoTen.Focus();
                return false;
            }

            if (!Regex.IsMatch(txtSDT.Text, @"^\d{9,11}$"))
            {
                MessageBox.Show(
                    "Số điện thoại phải gồm 9 đến 11 chữ số!");
                txtSDT.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) ||
                !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Email phải chứa ký tự @!");
                txtEmail.Focus();
                return false;
            }

            var ngaySinh = dtpNgaySinh.Value.Date;
            var homNay = DateTime.Today;

            int tuoi = homNay.Year - ngaySinh.Year;

            if (ngaySinh > homNay)
            {
                MessageBox.Show("Ngày sinh không được ở tương lai!");
                return false;
            }

            if (ngaySinh > homNay.AddYears(-tuoi))
                tuoi--;

            if (tuoi < 15)
            {
                MessageBox.Show("Hội viên phải từ 15 tuổi trở lên!");
                return false;
            }

            if (cboHang.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn hạng thành viên!");
                return false;
            }

            return true;
        }
        private async void btnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraDuLieu())
                return;

            try
            {
                using var db = new QuanLyHoiVienGym_EFContext();

                var hv = new HoiVien
                {
                    HoTen = txtHoTen.Text.Trim(),
                    GioiTinh = rdoNam.Checked,
                    NgaySinh = DateOnly.FromDateTime(
                        dtpNgaySinh.Value.Date),
                    Sdt = txtSDT.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    HangThanhVien = cboHang.Text,
                    TrangThai = chkTrangThai.Checked
                };

                db.HoiViens.Add(hv);
                await db.SaveChangesAsync();

                MessageBox.Show("Thêm hội viên thành công!");

                await LoadHoiVienAsync();
                LamMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm hội viên: " + ex.Message);
            }
        }
        private void dgvHoiVien_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dgvHoiVien.CurrentRow == null ||
                dgvHoiVien.CurrentRow.IsNewRow)
                return;

            var row = dgvHoiVien.CurrentRow;

            if (row.Cells["HoTen"].Value == null)
                return;

            txtHoTen.Text =
                row.Cells["HoTen"].Value.ToString();

            txtSDT.Text =
                row.Cells["Sdt"].Value?.ToString() ?? "";

            txtEmail.Text =
                row.Cells["Email"].Value?.ToString() ?? "";

            cboHang.Text =
                row.Cells["HangThanhVien"].Value?.ToString() ?? "Basic";

            bool gioiTinh = Convert.ToBoolean(
                row.Cells["GioiTinh"].Value);

            rdoNam.Checked = gioiTinh;
            rdoNu.Checked = !gioiTinh;

            if (DateTime.TryParse(
                row.Cells["NgaySinh"].Value?.ToString(),
                out DateTime ngaySinh))
            {
                dtpNgaySinh.Value = ngaySinh;
            }

            chkTrangThai.Checked = Convert.ToBoolean(
                row.Cells["TrangThai"].Value);
        }
        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvHoiVien.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn hội viên cần sửa!");
                return;
            }

            if (!KiemTraDuLieu())
                return;

            int maHV = Convert.ToInt32(
                dgvHoiVien.CurrentRow.Cells["MaHv"].Value);

            try
            {
                using var db = new QuanLyHoiVienGym_EFContext();

                var hv = await db.HoiViens
                    .FirstOrDefaultAsync(x => x.MaHv == maHV);

                if (hv == null)
                {
                    MessageBox.Show("Không tìm thấy hội viên!");
                    return;
                }

                hv.HoTen = txtHoTen.Text.Trim();
                hv.GioiTinh = rdoNam.Checked;
                hv.NgaySinh = DateOnly.FromDateTime(
                    dtpNgaySinh.Value.Date);
                hv.Sdt = txtSDT.Text.Trim();
                hv.Email = txtEmail.Text.Trim();
                hv.HangThanhVien = cboHang.Text;
                hv.TrangThai = chkTrangThai.Checked;

                await db.SaveChangesAsync();

                MessageBox.Show("Cập nhật hội viên thành công!");

                await LoadHoiVienAsync();
                LamMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message);
            }
        }
        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvHoiVien.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn hội viên cần xóa!");
                return;
            }

            var ketQua = MessageBox.Show(
                "Bạn có chắc muốn xóa hội viên này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (ketQua != DialogResult.Yes)
                return;

            int maHV = Convert.ToInt32(
                dgvHoiVien.CurrentRow.Cells["MaHv"].Value);

            try
            {
                using var db = new QuanLyHoiVienGym_EFContext();

                var hv = await db.HoiViens
                    .FirstOrDefaultAsync(x => x.MaHv == maHV);

                if (hv == null)
                {
                    MessageBox.Show("Hội viên không tồn tại!");
                    return;
                }

                db.HoiViens.Remove(hv);
                await db.SaveChangesAsync();

                MessageBox.Show("Xóa hội viên thành công!");

                await LoadHoiVienAsync();
                LamMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }
        private async void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimTen.Clear();
            cboLocHang.SelectedIndex = 0;

            await LoadHoiVienAsync();
            LamMoi();
        }
        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                using var db = new QuanLyHoiVienGym_EFContext();

                var query = db.HoiViens.AsNoTracking();

                string ten = txtTimTen.Text.Trim();
                string hang = cboLocHang.Text;

                if (!string.IsNullOrWhiteSpace(ten))
                {
                    query = query.Where(x => x.HoTen.Contains(ten));
                }

                if (!string.IsNullOrWhiteSpace(hang) &&
                    hang != "Tất cả")
                {
                    query = query.Where(
                        x => x.HangThanhVien == hang);
                }

                dgvHoiVien.DataSource = await query
                    .OrderBy(x => x.MaHv)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }
    }
}
