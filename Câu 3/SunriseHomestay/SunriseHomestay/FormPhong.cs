
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using SunriseHomestay.Models;

namespace SunriseHomestay
{
    public partial class FrmPhong : Form
    {
        private Label lblSoPhong;
        private Label lblTangSo;
        private Label lblLoaiPhong;
        private Label lblTinhTrang;

        private TextBox txtSoPhong;
        private NumericUpDown nudTangSo;
        private ComboBox cboLoaiPhong;
        private ComboBox cboTinhTrang;

        private PictureBox picPhong;
        private Button btnChonAnh;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;

        private ComboBox cboLocLoaiPhong;
        private ComboBox cboLocTinhTrang;
        private Button btnTimKiem;

        private DataGridView dgvPhong;

        // Lưu đường dẫn ảnh vừa chọn
        private string duongDanAnh = "";

        // Mã phòng đang được chọn để sửa hoặc xóa
        private int maPhongDangChon = 0;

        // Thư mục chứa ảnh phòng
        private string ThuMucAnh =>
            Path.Combine(Application.StartupPath, "Images");

        public FrmPhong()
        {
            InitializeComponent();
            TaoGiaoDien();

            // Bắt sự kiện các nút
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnChonAnh.Click += btnChonAnh_Click;
            btnTimKiem.Click += btnTimKiem_Click;
            dgvPhong.CellClick += dgvPhong_CellClick;

            LoadLoaiPhong();
            LoadDanhSachPhong();
        }

        private void TaoGiaoDien()
        {
            this.Text = "Quản Lý Phòng - Sunrise Homestay";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(912, 477);
            this.MinimumSize = new Size(840, 480);
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.White;
            this.AutoScaleMode = AutoScaleMode.Font;

            lblSoPhong = TaoLabel("Số phòng", 24, 17, 100);
            lblTangSo = TaoLabel("Tầng số", 142, 17, 80);
            lblLoaiPhong = TaoLabel("Loại phòng", 240, 17, 100);
            lblTinhTrang = TaoLabel("Tình trạng", 355, 17, 100);

            txtSoPhong = new TextBox
            {
                Name = "txtSoPhong",
                Location = new Point(24, 40),
                Size = new Size(100, 25)
            };

            nudTangSo = new NumericUpDown
            {
                Name = "nudTangSo",
                Location = new Point(142, 40),
                Size = new Size(80, 25),
                Minimum = 0,
                Maximum = 100,
                Value = 0
            };

            cboLoaiPhong = new ComboBox
            {
                Name = "cboLoaiPhong",
                Location = new Point(240, 40),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cboTinhTrang = new ComboBox
            {
                Name = "cboTinhTrang",
                Location = new Point(355, 40),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboTinhTrang.Items.AddRange(new object[]
            {
                "Trống", "Đang ở", "Đang dọn"
            });

            picPhong = new PictureBox
            {
                Name = "picPhong",
                Location = new Point(470, 30),
                Size = new Size(68, 60),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.WhiteSmoke
            };

            btnChonAnh = TaoButton(
                "Chọn ảnh...", "btnChonAnh", 470, 95, 68, 25);

            btnThem = TaoButton("Thêm", "btnThem", 585, 34, 58, 28);
            btnSua = TaoButton("Sửa", "btnSua", 650, 34, 58, 28);
            btnXoa = TaoButton("Xóa", "btnXoa", 715, 34, 58, 28);
            btnLamMoi = TaoButton(
                "Làm mới", "btnLamMoi", 778, 34, 76, 28);

            cboLocLoaiPhong = new ComboBox
            {
                Name = "cboLocLoaiPhong",
                Location = new Point(24, 135),
                Size = new Size(165, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cboLocTinhTrang = new ComboBox
            {
                Name = "cboLocTinhTrang",
                Location = new Point(195, 135),
                Size = new Size(165, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboLocTinhTrang.Items.AddRange(new object[]
            {
                "Tất cả tình trạng",
                "Trống",
                "Đang ở",
                "Đang dọn"
            });
            cboLocTinhTrang.SelectedIndex = 0;

            btnTimKiem = TaoButton(
                "Tìm kiếm", "btnTimKiem", 365, 133, 75, 28);

            dgvPhong = new DataGridView
            {
                Name = "dgvPhong",
                Location = new Point(24, 163),
                Size = new Size(792, 265),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersWidth = 24,
                RowTemplate = { Height = 38 },
                AutoGenerateColumns = false
            };

            dgvPhong.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaPhong",
                HeaderText = "Mã phòng",
                FillWeight = 85
            });

            dgvPhong.Columns.Add(new DataGridViewImageColumn
            {
                Name = "colAnh",
                HeaderText = "Ảnh",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                FillWeight = 55
            });

            dgvPhong.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSoPhong",
                HeaderText = "Số phòng",
                FillWeight = 100
            });

            dgvPhong.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTang",
                HeaderText = "Tầng",
                FillWeight = 90
            });

            dgvPhong.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colLoaiPhong",
                HeaderText = "Loại phòng",
                FillWeight = 125
            });

            dgvPhong.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colGiaDem",
                HeaderText = "Giá/đêm",
                FillWeight = 110
            });

            dgvPhong.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTinhTrang",
                HeaderText = "Tình trạng",
                FillWeight = 110
            });

            Controls.AddRange(new Control[]
            {
                lblSoPhong, lblTangSo, lblLoaiPhong, lblTinhTrang,
                txtSoPhong, nudTangSo, cboLoaiPhong, cboTinhTrang,
                picPhong, btnChonAnh, btnThem, btnSua, btnXoa,
                btnLamMoi, cboLocLoaiPhong, cboLocTinhTrang,
                btnTimKiem, dgvPhong
            });
        }

        // =========================
        // TẢI DANH SÁCH LOẠI PHÒNG
        // =========================
        private void LoadLoaiPhong()
        {
            try
            {
                using var db = new SunriseHomestay_EFContext();

                var danhSach = db.LoaiPhongs
                    .AsNoTracking()
                    .OrderBy(x => x.MaLoai)
                    .Select(x => new
                    {
                        x.MaLoai,
                        x.TenLoai
                    })
                    .ToList();

                cboLoaiPhong.DataSource = null;
                cboLoaiPhong.DisplayMember = "TenLoai";
                cboLoaiPhong.ValueMember = "MaLoai";
                cboLoaiPhong.DataSource = danhSach;

                cboLocLoaiPhong.Items.Clear();
                cboLocLoaiPhong.Items.Add("Tất cả loại phòng");

                foreach (var loai in danhSach)
                    cboLocLoaiPhong.Items.Add(loai.TenLoai);

                cboLocLoaiPhong.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải loại phòng:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================
        // TẢI VÀ HIỂN THỊ PHÒNG
        // =========================
        private void LoadDanhSachPhong()
        {
            try
            {
                using var db = new SunriseHomestay_EFContext();

                var danhSach = db.Phongs
                    .AsNoTracking()
                    .Include(x => x.MaLoaiNavigation)
                    .OrderBy(x => x.MaPhong)
                    .ToList();

                dgvPhong.Rows.Clear();

                foreach (var p in danhSach)
                {
                    Image anh = DocAnh(p.HinhAnh);

                    int dong = dgvPhong.Rows.Add(
                        p.MaPhong,
                        anh,
                        p.SoPhong,
                        p.TangSo,
                        p.MaLoaiNavigation?.TenLoai ?? "",
                        p.MaLoaiNavigation == null
                            ? ""
                            : p.MaLoaiNavigation.GiaMoiDem.ToString("N0"),
                        p.TinhTrang
                    );

                    // Lưu tên ảnh vào Tag để khi chọn dòng có thể đọc lại
                    dgvPhong.Rows[dong].Tag = p.HinhAnh;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải danh sách phòng:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Đọc ảnh và sao chép vào bộ nhớ để tránh khóa file
        private Image DocAnh(string tenAnh)
        {
            if (string.IsNullOrWhiteSpace(tenAnh))
                return null;

            string duongDan = Path.Combine(ThuMucAnh, tenAnh);

            if (!File.Exists(duongDan))
                return null;

            try
            {
                using var fs = new FileStream(
                    duongDan, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var img = Image.FromStream(fs);

                return new Bitmap(img);
            }
            catch
            {
                return null;
            }
        }

        // =========================
        // CHỌN ẢNH
        // =========================
        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Chọn ảnh phòng",
                Filter = "Tệp hình ảnh|*.jpg;*.jpeg;*.png;*.bmp;*.webp",
                Multiselect = false
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                duongDanAnh = dialog.FileName;

                using var fs = new FileStream(
                    duongDanAnh, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var img = Image.FromStream(fs);

                Image anhMoi = new Bitmap(img);

                picPhong.Image?.Dispose();
                picPhong.Image = anhMoi;
            }
            catch (Exception ex)
            {
                duongDanAnh = "";
                MessageBox.Show("Không thể mở ảnh:\n" + ex.Message);
            }
        }

        // Sao chép ảnh vào thư mục Images, trả về tên file để lưu DB
        private string LuuAnh()
        {
            if (string.IsNullOrWhiteSpace(duongDanAnh))
                return null;

            Directory.CreateDirectory(ThuMucAnh);

            string tenFile = Guid.NewGuid().ToString("N")
                + Path.GetExtension(duongDanAnh);

            string dich = Path.Combine(ThuMucAnh, tenFile);

            File.Copy(duongDanAnh, dich, true);

            return tenFile;
        }

        // =========================
        // THÊM PHÒNG
        // =========================
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraDuLieu())
                return;

            try
            {
                using var db = new SunriseHomestay_EFContext();

                string soPhong = txtSoPhong.Text.Trim();

                bool daTonTai = db.Phongs.Any(
                    x => x.SoPhong == soPhong);

                if (daTonTai)
                {
                    MessageBox.Show("Số phòng này đã tồn tại.");
                    return;
                }

                var phong = new Phong
                {
                    SoPhong = soPhong,
                    TangSo = (int)nudTangSo.Value,
                    MaLoai = Convert.ToInt32(cboLoaiPhong.SelectedValue),
                    TinhTrang = cboTinhTrang.Text,
                    HinhAnh = LuuAnh()
                };

                db.Phongs.Add(phong);
                db.SaveChanges();

                MessageBox.Show("Thêm phòng thành công!");

                LamMoiForm();
                LoadDanhSachPhong();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Thêm phòng thất bại:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================
        // SỬA PHÒNG
        // =========================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (maPhongDangChon == 0)
            {
                MessageBox.Show("Hãy chọn phòng cần sửa trong bảng.");
                return;
            }

            if (!KiemTraDuLieu())
                return;

            try
            {
                using var db = new SunriseHomestay_EFContext();

                var phong = db.Phongs
                    .FirstOrDefault(x => x.MaPhong == maPhongDangChon);

                if (phong == null)
                {
                    MessageBox.Show("Không tìm thấy phòng cần sửa.");
                    return;
                }

                string soPhong = txtSoPhong.Text.Trim();

                bool trungSoPhong = db.Phongs.Any(
                    x => x.SoPhong == soPhong
                         && x.MaPhong != maPhongDangChon);

                if (trungSoPhong)
                {
                    MessageBox.Show("Số phòng này đã được sử dụng.");
                    return;
                }

                phong.SoPhong = soPhong;
                phong.TangSo = (int)nudTangSo.Value;
                phong.MaLoai = Convert.ToInt32(cboLoaiPhong.SelectedValue);
                phong.TinhTrang = cboTinhTrang.Text;

                // Chỉ thay ảnh khi người dùng chọn ảnh mới
                if (!string.IsNullOrWhiteSpace(duongDanAnh))
                    phong.HinhAnh = LuuAnh();

                db.SaveChanges();

                MessageBox.Show("Cập nhật phòng thành công!");

                LamMoiForm();
                LoadDanhSachPhong();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Sửa phòng thất bại:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================
        // XÓA PHÒNG
        // =========================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (maPhongDangChon == 0)
            {
                MessageBox.Show("Hãy chọn phòng cần xóa trong bảng.");
                return;
            }

            DialogResult xacNhan = MessageBox.Show(
                "Bạn có chắc muốn xóa phòng này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (xacNhan != DialogResult.Yes)
                return;

            try
            {
                using var db = new SunriseHomestay_EFContext();

                var phong = db.Phongs
                    .FirstOrDefault(x => x.MaPhong == maPhongDangChon);

                if (phong == null)
                {
                    MessageBox.Show("Không tìm thấy phòng cần xóa.");
                    return;
                }

                db.Phongs.Remove(phong);
                db.SaveChanges();

                MessageBox.Show("Xóa phòng thành công!");

                LamMoiForm();
                LoadDanhSachPhong();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Xóa phòng thất bại. Có thể phòng đang được tham chiếu "
                    + "bởi dữ liệu đặt phòng.\n\nChi tiết: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================
        // CHỌN DÒNG ĐỂ SỬA / XÓA
        // =========================
        private void dgvPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow dong = dgvPhong.Rows[e.RowIndex];

            maPhongDangChon = Convert.ToInt32(dong.Cells["colMaPhong"].Value);

            txtSoPhong.Text =
                Convert.ToString(dong.Cells["colSoPhong"].Value);

            nudTangSo.Value = Convert.ToDecimal(
                dong.Cells["colTang"].Value ?? 0);

            cboTinhTrang.Text =
                Convert.ToString(dong.Cells["colTinhTrang"].Value);

            string tenLoai =
                Convert.ToString(dong.Cells["colLoaiPhong"].Value);

            // Chọn đúng loại phòng theo tên hiển thị
            for (int i = 0; i < cboLoaiPhong.Items.Count; i++)
            {
                dynamic item = cboLoaiPhong.Items[i];

                if (Convert.ToString(item.TenLoai) == tenLoai)
                {
                    cboLoaiPhong.SelectedIndex = i;
                    break;
                }
            }

            // Xóa đường dẫn ảnh đã chọn trước đó.
            // Không đặt duongDanAnh thành đường dẫn ảnh cũ,
            // nếu không khi bấm Sửa sẽ lưu lại ảnh không cần thiết.
            duongDanAnh = "";

            string tenAnh = Convert.ToString(dong.Tag);
            Image anh = DocAnh(tenAnh);

            picPhong.Image?.Dispose();
            picPhong.Image = anh;
        }

        // =========================
        // TÌM KIẾM / LỌC
        // =========================
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                using var db = new SunriseHomestay_EFContext();

                var query = db.Phongs
                    .AsNoTracking()
                    .Include(x => x.MaLoaiNavigation)
                    .AsQueryable();

                string loaiChon = Convert.ToString(
                    cboLocLoaiPhong.SelectedItem);

                string tinhTrangChon = Convert.ToString(
                    cboLocTinhTrang.SelectedItem);

                if (!string.IsNullOrWhiteSpace(loaiChon)
                    && loaiChon != "Tất cả loại phòng")
                {
                    query = query.Where(
                        x => x.MaLoaiNavigation != null
                             && x.MaLoaiNavigation.TenLoai == loaiChon);
                }

                if (!string.IsNullOrWhiteSpace(tinhTrangChon)
                    && tinhTrangChon != "Tất cả tình trạng")
                {
                    query = query.Where(
                        x => x.TinhTrang == tinhTrangChon);
                }

                var danhSach = query
                    .OrderBy(x => x.MaPhong)
                    .ToList();

                dgvPhong.Rows.Clear();

                foreach (var p in danhSach)
                {
                    int dong = dgvPhong.Rows.Add(
                        p.MaPhong,
                        DocAnh(p.HinhAnh),
                        p.SoPhong,
                        p.TangSo,
                        p.MaLoaiNavigation?.TenLoai ?? "",
                        p.MaLoaiNavigation == null
                            ? ""
                            : p.MaLoaiNavigation.GiaMoiDem.ToString("N0"),
                        p.TinhTrang
                    );

                    dgvPhong.Rows[dong].Tag = p.HinhAnh;
                }

                maPhongDangChon = 0;

                if (danhSach.Count == 0)
                    MessageBox.Show("Không tìm thấy phòng phù hợp.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Tìm kiếm thất bại:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================
        // LÀM MỚI FORM
        // =========================
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoiForm();
            LoadDanhSachPhong();
        }

        private void LamMoiForm()
        {
            maPhongDangChon = 0;
            duongDanAnh = "";

            txtSoPhong.Clear();
            nudTangSo.Value = 0;

            if (cboLoaiPhong.Items.Count > 0)
                cboLoaiPhong.SelectedIndex = 0;

            if (cboTinhTrang.Items.Count > 0)
                cboTinhTrang.SelectedIndex = 0;

            if (cboLocLoaiPhong.Items.Count > 0)
                cboLocLoaiPhong.SelectedIndex = 0;

            if (cboLocTinhTrang.Items.Count > 0)
                cboLocTinhTrang.SelectedIndex = 0;

            picPhong.Image?.Dispose();
            picPhong.Image = null;

            dgvPhong.ClearSelection();
        }

        // =========================
        // KIỂM TRA DỮ LIỆU NHẬP
        // =========================
        private bool KiemTraDuLieu()
        {
            if (string.IsNullOrWhiteSpace(txtSoPhong.Text))
            {
                MessageBox.Show("Vui lòng nhập số phòng.");
                txtSoPhong.Focus();
                return false;
            }

            if (cboLoaiPhong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại phòng.");
                return false;
            }

            if (cboTinhTrang.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn tình trạng phòng.");
                return false;
            }

            return true;
        }

        private Label TaoLabel(
            string noiDung, int x, int y, int width)
        {
            return new Label
            {
                Text = noiDung,
                Location = new Point(x, y),
                Size = new Size(width, 22),
                AutoSize = false
            };
        }

        private Button TaoButton(
            string noiDung, string ten,
            int x, int y, int width, int height)
        {
            return new Button
            {
                Name = ten,
                Text = noiDung,
                Location = new Point(x, y),
                Size = new Size(width, height),
                UseVisualStyleBackColor = true
            };
        }
    }
}
