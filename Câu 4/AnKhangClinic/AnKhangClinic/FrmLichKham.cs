
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using AnKhangClinic.Models;

namespace AnKhangClinic
{
    public partial class FrmLichKham : Form
    {

        private TextBox txtTenBenhNhan = null!;
        private TextBox txtSDT = null!;

        private DateTimePicker dtpNgayKham = null!;
        private DateTimePicker dtpGioKham = null!;
        private DateTimePicker dtpTuNgay = null!;
        private DateTimePicker dtpDenNgay = null!;

        private ComboBox cboBacSi = null!;
        private ComboBox cboTrangThai = null!;
        private ComboBox cboLocBacSi = null!;

        private Button btnThem = null!;
        private Button btnSua = null!;
        private Button btnXoa = null!;
        private Button btnLamMoi = null!;
        private Button btnTimKiem = null!;
        private Button btnDiLite = null!;

        private DataGridView dgvLichKham = null!;


        private readonly AnKhangClinicContext db = new AnKhangClinicContext();

        public FrmLichKham()
        {
            InitializeComponent();
            TaoGiaoDien();
            GanSuKien();
            NapBacSi();
            TaiDanhSach();
        }

        // =========================================
        // THIẾT KẾ GIAO DIỆN
        // =========================================

        private void TaoGiaoDien()
        {
            AutoScaleMode = AutoScaleMode.None;
            Text = "Quản Lý Lịch Khám Bệnh - An Khang Clinic";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1180, 620);
            MinimumSize = new Size(1196, 659);
            BackColor = Color.FromArgb(240, 240, 240);
            Font = new Font("Segoe UI", 10F);

            ThemLabel("Tên bệnh nhân", 15, 29, 105, 28);
            txtTenBenhNhan = ThemTextBox(125, 29, 200, 28);

            ThemLabel("Số điện thoại", 15, 69, 105, 28);
            txtSDT = ThemTextBox(125, 69, 200, 28);

            ThemLabel("Ngày khám", 15, 109, 105, 28);

            dtpNgayKham = new DateTimePicker
            {
                Location = new Point(125, 109),
                Size = new Size(200, 28),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy"
            };
            Controls.Add(dtpNgayKham);

            ThemLabel("Giờ khám", 355, 69, 130, 25);

            dtpGioKham = new DateTimePicker
            {
                Location = new Point(355, 109),
                Size = new Size(130, 28),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "HH:mm",
                ShowUpDown = true
            };
            Controls.Add(dtpGioKham);

            ThemLabel("Bác sĩ", 510, 69, 250, 25);

            cboBacSi = new ComboBox
            {
                Location = new Point(510, 89),
                Size = new Size(260, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            Controls.Add(cboBacSi);

            ThemLabel("Trạng thái", 790, 62, 170, 25);

            cboTrangThai = new ComboBox
            {
                Location = new Point(790, 94),
                Size = new Size(170, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cboTrangThai.Items.AddRange(new object[]
            {
                "Chờ khám", "Đã khám", "Đã hủy"
            });
            cboTrangThai.SelectedIndex = 0;
            Controls.Add(cboTrangThai);

            btnThem = ThemButton("Thêm", 790, 25, 75, 30);
            btnSua = ThemButton("Sửa", 875, 25, 75, 30);
            btnXoa = ThemButton("Xóa", 960, 25, 75, 30);
            btnLamMoi = ThemButton("Làm mới", 1045, 25, 100, 30);

            ThemLabel("Từ ngày", 15, 164, 65, 28);

            dtpTuNgay = new DateTimePicker
            {
                Location = new Point(85, 164),
                Size = new Size(145, 28),
                Format = DateTimePickerFormat.Short
            };
            Controls.Add(dtpTuNgay);

            ThemLabel("Đến ngày", 250, 164, 70, 28);

            dtpDenNgay = new DateTimePicker
            {
                Location = new Point(325, 164),
                Size = new Size(145, 28),
                Format = DateTimePickerFormat.Short
            };
            Controls.Add(dtpDenNgay);

            cboLocBacSi = new ComboBox
            {
                Location = new Point(490, 164),
                Size = new Size(280, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            Controls.Add(cboLocBacSi);

            btnTimKiem = ThemButton("Tìm kiếm", 790, 162, 90, 32);
            btnDiLite = ThemButton("Đi Làm", 1050, 162, 95, 32);

            dgvLichKham = new DataGridView
            {
                Name = "dgvLichKham",
                Location = new Point(10, 210),
                Size = new Size(1135, 395),
                BackgroundColor = Color.DarkGray,
                BorderStyle = BorderStyle.FixedSingle,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                MultiSelect = false,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                RowHeadersWidth = 35,
                AutoGenerateColumns = false,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.None,
                ColumnHeadersHeight = 32,
                EnableHeadersVisualStyles = false
            };

            dgvLichKham.RowTemplate.Height = 25;
            dgvLichKham.Font = new Font("Segoe UI", 10F);

            ThemCot("MaLich", "Mã lịch", 75);
            ThemCot("TenBenhNhan", "Tên bệnh nhân", 150);
            ThemCot("SDT", "SDT", 120);
            ThemCot("NgayKham", "Ngày khám", 145);
            ThemCot("GioKham", "Giờ khám", 90);
            ThemCot("BacSi", "Bác sĩ", 230);
            ThemCot("ChuyenKhoa", "Chuyên khoa", 150);
            ThemCot("TrangThai", "Trạng thái", 120);

            Controls.Add(dgvLichKham);
        }

        private void ThemCot(string name, string header, int width)
        {
            dgvLichKham.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                Width = width,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
        }

        private void ThemLabel(
            string noiDung, int x, int y, int width, int height)
        {
            Controls.Add(new Label
            {
                Text = noiDung,
                Location = new Point(x, y),
                Size = new Size(width, height),
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = false
            });
        }

        private TextBox ThemTextBox(
            int x, int y, int width, int height)
        {
            TextBox txt = new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(width, height)
            };

            Controls.Add(txt);
            return txt;
        }

        private Button ThemButton(
            string text, int x, int y, int width, int height)
        {
            Button btn = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, height),
                UseVisualStyleBackColor = true
            };

            Controls.Add(btn);
            return btn;
        }

        // =========================================
        // GẮN SỰ KIỆN CHO CÁC NÚT
        // =========================================

        private void GanSuKien()
        {
            btnThem.Click += (s, e) => ThemLichKham();
            btnSua.Click += (s, e) => SuaLichKham();
            btnXoa.Click += (s, e) => XoaLichKham();
            btnLamMoi.Click += (s, e) => LamMoi();
            btnTimKiem.Click += (s, e) => TimKiem();

            dgvLichKham.CellClick +=
                (s, e) => HienThiDongDuocChon();

            // Nút Đi Lite chưa gán chức năng vì chưa có yêu cầu cụ thể.
        }

        // =========================================
        // TẢI DANH SÁCH BÁC SĨ TỪ DATABASE
        // =========================================

        private void NapBacSi()
        {
            var danhSach = db.BacSis
                .AsNoTracking()
                .OrderBy(x => x.HoTen)
                .ToList();

            cboBacSi.DataSource = danhSach.ToList();
            cboBacSi.DisplayMember = "HoTen";
            cboBacSi.ValueMember = "MaBS";
            cboBacSi.SelectedIndex = -1;

            cboLocBacSi.Items.Clear();
            cboLocBacSi.Items.Add("Tất cả bác sĩ");

            foreach (var bs in danhSach)
            {
                cboLocBacSi.Items.Add(
                    new BacSiHienThi(bs.MaBs,
                        "BS. " + bs.HoTen + " - " + bs.ChuyenKhoa));
            }

            cboLocBacSi.SelectedIndex = 0;
        }

        // =========================================
        // HIỂN THỊ LỊCH KHÁM
        // =========================================

        private void TaiDanhSach()
        {
            try
            {
                var danhSach = db.LichKhams
                    .AsNoTracking()
                    .Include(x => x.MaBsNavigation)
                    .OrderBy(x => x.NgayKham)
                    .ThenBy(x => x.GioKham)
                    .ToList();

                dgvLichKham.Rows.Clear();

                foreach (var lich in danhSach)
                {
                    dgvLichKham.Rows.Add(
                        lich.MaLich,
                        lich.TenBenhNhan,
                        lich.Sdt,
                        lich.NgayKham.ToString("dd/MM/yyyy"),
                        lich.GioKham.ToString("HH:mm"),
                        lich.MaBsNavigation == null
                            ? ""
                            : "BS. " + lich.MaBsNavigation.HoTen
                              + " - " + lich.MaBsNavigation.ChuyenKhoa,
                        lich.MaBsNavigation?.ChuyenKhoa ?? "",
                        lich.TrangThai
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không tải được danh sách lịch khám.\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================
        // KIỂM TRA DỮ LIỆU
        // =========================================

        private bool KiemTraDuLieu()
        {
            if (string.IsNullOrWhiteSpace(txtTenBenhNhan.Text))
            {
                MessageBox.Show("Vui lòng nhập tên bệnh nhân.");
                txtTenBenhNhan.Focus();
                return false;
            }

            if (cboBacSi.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn bác sĩ.");
                cboBacSi.Focus();
                return false;
            }

            if (dtpNgayKham.Value.Date < DateTime.Today)
            {
                MessageBox.Show("Ngày khám không được nằm trong quá khứ.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(cboTrangThai.Text))
            {
                MessageBox.Show("Vui lòng chọn trạng thái.");
                return false;
            }

            if (txtSDT.Text.Trim().Length > 15)
            {
                MessageBox.Show("Số điện thoại không được quá 15 ký tự.");
                txtSDT.Focus();
                return false;
            }

            return true;
        }

        // =========================================
        // THÊM LỊCH KHÁM
        // =========================================

        private void ThemLichKham()
        {
            if (!KiemTraDuLieu())
                return;

            try
            {
                var lich = new LichKham
                {
                    TenBenhNhan = txtTenBenhNhan.Text.Trim(),
                    Sdt = txtSDT.Text.Trim(),
                    NgayKham = DateOnly.FromDateTime(dtpNgayKham.Value),
                    GioKham = TimeOnly.FromDateTime(dtpGioKham.Value),
                    MaBs = Convert.ToInt32(cboBacSi.SelectedValue),
                    TrangThai = cboTrangThai.Text
                };

                db.LichKhams.Add(lich);
                db.SaveChanges();

                MessageBox.Show("Thêm lịch khám thành công.");
                TaiDanhSach();
                LamSachThongTin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể thêm lịch khám.\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================
        // SỬA LỊCH KHÁM
        // =========================================

        private void SuaLichKham()
        {
            if (dgvLichKham.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn lịch khám cần sửa.");
                return;
            }

            if (!KiemTraDuLieu())
                return;

            int maLich = Convert.ToInt32(
                dgvLichKham.CurrentRow.Cells["MaLich"].Value);

            try
            {
                var lich = db.LichKhams
                    .FirstOrDefault(x => x.MaLich == maLich);

                if (lich == null)
                {
                    MessageBox.Show("Không tìm thấy lịch khám.");
                    return;
                }

                lich.TenBenhNhan = txtTenBenhNhan.Text.Trim();
                lich.Sdt = txtSDT.Text.Trim();
                lich.NgayKham = DateOnly.FromDateTime(dtpNgayKham.Value);
                lich.GioKham = TimeOnly.FromDateTime(dtpGioKham.Value);
                lich.MaBs = Convert.ToInt32(cboBacSi.SelectedValue);
                lich.TrangThai = cboTrangThai.Text;

                db.SaveChanges();

                MessageBox.Show("Cập nhật lịch khám thành công.");
                TaiDanhSach();
                LamSachThongTin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể sửa lịch khám.\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================
        // XÓA LỊCH KHÁM
        // =========================================

        private void XoaLichKham()
        {
            if (dgvLichKham.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn lịch khám cần xóa.");
                return;
            }

            int maLich = Convert.ToInt32(
                dgvLichKham.CurrentRow.Cells["MaLich"].Value);

            DialogResult ketQua = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa lịch khám này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (ketQua != DialogResult.Yes)
                return;

            try
            {
                var lich = db.LichKhams
                    .FirstOrDefault(x => x.MaLich == maLich);

                if (lich == null)
                {
                    MessageBox.Show("Lịch khám không còn tồn tại.");
                    TaiDanhSach();
                    return;
                }

                db.LichKhams.Remove(lich);
                db.SaveChanges();

                MessageBox.Show("Xóa lịch khám thành công.");
                TaiDanhSach();
                LamSachThongTin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể xóa lịch khám.\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================
        // CHỌN DÒNG TRONG BẢNG
        // =========================================

        private void HienThiDongDuocChon()
        {
            if (dgvLichKham.CurrentRow == null ||
                dgvLichKham.CurrentRow.Cells["MaLich"].Value == null)
                return;

            int maLich = Convert.ToInt32(
                dgvLichKham.CurrentRow.Cells["MaLich"].Value);

            var lich = db.LichKhams
                .AsNoTracking()
                .FirstOrDefault(x => x.MaLich == maLich);

            if (lich == null)
                return;

            txtTenBenhNhan.Text = lich.TenBenhNhan;
            txtSDT.Text = lich.Sdt;

            dtpNgayKham.Value = lich.NgayKham.ToDateTime(
                new TimeOnly(0, 0));

            dtpGioKham.Value = DateTime.Today.Add(
                lich.GioKham.ToTimeSpan());

            cboBacSi.SelectedValue = lich.MaBs;
            cboTrangThai.SelectedItem = lich.TrangThai;
        }

        // =========================================
        // TÌM KIẾM THEO KHOẢNG NGÀY VÀ BÁC SĨ
        // =========================================

        private void TimKiem()
        {
            if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
            {
                MessageBox.Show(
                    "Ngày bắt đầu không được lớn hơn ngày kết thúc.");
                return;
            }

            try
            {
                var query = db.LichKhams
                    .AsNoTracking()
                    .Include(x => x.MaBsNavigation)
                    .Where(x =>
                        x.NgayKham >= DateOnly.FromDateTime(dtpTuNgay.Value) &&
                        x.NgayKham <= DateOnly.FromDateTime(dtpDenNgay.Value));

                if (cboLocBacSi.SelectedItem is BacSiHienThi bsChon)
                {
                    query = query.Where(x => x.MaBs == bsChon.MaBS);
                }

                var danhSach = query
                    .OrderBy(x => x.NgayKham)
                    .ThenBy(x => x.GioKham)
                    .ToList();

                dgvLichKham.Rows.Clear();

                foreach (var lich in danhSach)
                {
                    dgvLichKham.Rows.Add(
                        lich.MaLich,
                        lich.TenBenhNhan,
                        lich.Sdt,
                        lich.NgayKham.ToString("dd/MM/yyyy"),
                        lich.GioKham.ToString("HH:mm"),
                        lich.MaBsNavigation == null
                            ? ""
                            : "BS. " + lich.MaBsNavigation.HoTen
                              + " - " + lich.MaBsNavigation.ChuyenKhoa,
                        lich.MaBsNavigation?.ChuyenKhoa ?? "",
                        lich.TrangThai
                    );
                }

                if (danhSach.Count == 0)
                    MessageBox.Show("Không tìm thấy lịch khám phù hợp.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tìm kiếm.\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================
        // LÀM MỚI
        // =========================================

        private void LamMoi()
        {
            LamSachThongTin();

            dtpTuNgay.Value = DateTime.Today;
            dtpDenNgay.Value = DateTime.Today;

            if (cboLocBacSi.Items.Count > 0)
                cboLocBacSi.SelectedIndex = 0;

            TaiDanhSach();
        }

        private void LamSachThongTin()
        {
            txtTenBenhNhan.Clear();
            txtSDT.Clear();

            dtpNgayKham.Value = DateTime.Today;
            dtpGioKham.Value = DateTime.Now;

            cboBacSi.SelectedIndex = -1;
            cboTrangThai.SelectedIndex = 0;

            dgvLichKham.ClearSelection();
            txtTenBenhNhan.Focus();
        }

        private sealed class BacSiHienThi
        {
            public int MaBS { get; }
            public string TenHienThi { get; }

            public BacSiHienThi(int maBS, string tenHienThi)
            {
                MaBS = maBS;
                TenHienThi = tenHienThi;
            }

            public override string ToString()
            {
                return TenHienThi;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            db.Dispose();
            base.OnFormClosed(e);
        }
    }
}
