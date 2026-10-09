namespace QuanLyHoiVienGym
{
    partial class FrmHoiVien
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblHoTen = new Label();
            txtHoTen = new TextBox();
            lblSDT = new Label();
            txtSDT = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblNgaySinh = new Label();
            dtpNgaySinh = new DateTimePicker();
            lblHang = new Label();
            cboHang = new ComboBox();
            chkTrangThai = new CheckBox();
            grpGioiTinh = new GroupBox();
            rdoNu = new RadioButton();
            rdoNam = new RadioButton();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            txtTimTen = new TextBox();
            lblLocHang = new Label();
            cboLocHang = new ComboBox();
            btnTimKiem = new Button();
            dgvHoiVien = new DataGridView();
            grpGioiTinh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHoiVien).BeginInit();
            SuspendLayout();
            // 
            // lblHoTen
            // 
            lblHoTen.Location = new Point(25, 35);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(100, 25);
            lblHoTen.TabIndex = 0;
            lblHoTen.Text = "Họ tên";
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(155, 32);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(200, 27);
            txtHoTen.TabIndex = 1;
            // 
            // lblSDT
            // 
            lblSDT.Location = new Point(25, 75);
            lblSDT.Name = "lblSDT";
            lblSDT.Size = new Size(100, 25);
            lblSDT.TabIndex = 2;
            lblSDT.Text = "Số điện thoại";
            // 
            // txtSDT
            // 
            txtSDT.Location = new Point(155, 72);
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(200, 27);
            txtSDT.TabIndex = 3;
            // 
            // lblEmail
            // 
            lblEmail.Location = new Point(25, 115);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(100, 25);
            lblEmail.TabIndex = 4;
            lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(155, 112);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(200, 27);
            txtEmail.TabIndex = 5;
            // 
            // lblNgaySinh
            // 
            lblNgaySinh.Location = new Point(25, 155);
            lblNgaySinh.Name = "lblNgaySinh";
            lblNgaySinh.Size = new Size(100, 25);
            lblNgaySinh.TabIndex = 6;
            lblNgaySinh.Text = "Ngày sinh";
            // 
            // dtpNgaySinh
            // 
            dtpNgaySinh.Format = DateTimePickerFormat.Short;
            dtpNgaySinh.Location = new Point(155, 152);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new Size(200, 27);
            dtpNgaySinh.TabIndex = 7;
            // 
            // lblHang
            // 
            lblHang.Location = new Point(25, 195);
            lblHang.Name = "lblHang";
            lblHang.Size = new Size(124, 25);
            lblHang.TabIndex = 8;
            lblHang.Text = "Hạng thành viên";
            // 
            // cboHang
            // 
            cboHang.FormattingEnabled = true;
            cboHang.Location = new Point(155, 192);
            cboHang.Name = "cboHang";
            cboHang.Size = new Size(200, 28);
            cboHang.TabIndex = 9;
            // 
            // chkTrangThai
            // 
            chkTrangThai.AutoSize = true;
            chkTrangThai.Location = new Point(25, 235);
            chkTrangThai.Name = "chkTrangThai";
            chkTrangThai.Size = new Size(140, 24);
            chkTrangThai.TabIndex = 10;
            chkTrangThai.Text = "Đang hoạt động";
            chkTrangThai.UseVisualStyleBackColor = true;
            // 
            // grpGioiTinh
            // 
            grpGioiTinh.Controls.Add(rdoNu);
            grpGioiTinh.Controls.Add(rdoNam);
            grpGioiTinh.Location = new Point(390, 35);
            grpGioiTinh.Name = "grpGioiTinh";
            grpGioiTinh.Size = new Size(180, 65);
            grpGioiTinh.TabIndex = 11;
            grpGioiTinh.TabStop = false;
            grpGioiTinh.Text = "Giới tính";
            // 
            // rdoNu
            // 
            rdoNu.AutoSize = true;
            rdoNu.Location = new Point(95, 28);
            rdoNu.Name = "rdoNu";
            rdoNu.Size = new Size(50, 24);
            rdoNu.TabIndex = 1;
            rdoNu.TabStop = true;
            rdoNu.Text = "Nữ";
            rdoNu.UseVisualStyleBackColor = true;
            // 
            // rdoNam
            // 
            rdoNam.AutoSize = true;
            rdoNam.Location = new Point(15, 28);
            rdoNam.Name = "rdoNam";
            rdoNam.Size = new Size(62, 24);
            rdoNam.TabIndex = 0;
            rdoNam.TabStop = true;
            rdoNam.Text = "Nam";
            rdoNam.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(760, 35);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(150, 38);
            btnThem.TabIndex = 12;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(760, 85);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(150, 38);
            btnSua.TabIndex = 13;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(760, 135);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(150, 38);
            btnXoa.TabIndex = 14;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(760, 185);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(150, 38);
            btnLamMoi.TabIndex = 15;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // txtTimTen
            // 
            txtTimTen.Location = new Point(25, 295);
            txtTimTen.Name = "txtTimTen";
            txtTimTen.Size = new Size(300, 27);
            txtTimTen.TabIndex = 16;
            // 
            // lblLocHang
            // 
            lblLocHang.Location = new Point(345, 298);
            lblLocHang.Name = "lblLocHang";
            lblLocHang.Size = new Size(122, 25);
            lblLocHang.TabIndex = 17;
            lblLocHang.Text = "Hạng thành viên";
            // 
            // cboLocHang
            // 
            cboLocHang.FormattingEnabled = true;
            cboLocHang.Location = new Point(473, 294);
            cboLocHang.Name = "cboLocHang";
            cboLocHang.Size = new Size(180, 28);
            cboLocHang.TabIndex = 18;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Location = new Point(760, 290);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(150, 38);
            btnTimKiem.TabIndex = 19;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = true;
            btnTimKiem.Click += btnTimKiem_Click;
            // 
            // dgvHoiVien
            // 
            dgvHoiVien.AllowUserToAddRows = false;
            dgvHoiVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHoiVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHoiVien.Location = new Point(25, 345);
            dgvHoiVien.MultiSelect = false;
            dgvHoiVien.Name = "dgvHoiVien";
            dgvHoiVien.ReadOnly = true;
            dgvHoiVien.RowHeadersWidth = 51;
            dgvHoiVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHoiVien.Size = new Size(950, 300);
            dgvHoiVien.TabIndex = 20;
            dgvHoiVien.SelectionChanged += dgvHoiVien_SelectionChanged;
            // 
            // FrmHoiVien
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 653);
            Controls.Add(dgvHoiVien);
            Controls.Add(btnTimKiem);
            Controls.Add(cboLocHang);
            Controls.Add(lblLocHang);
            Controls.Add(txtTimTen);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(grpGioiTinh);
            Controls.Add(chkTrangThai);
            Controls.Add(cboHang);
            Controls.Add(lblHang);
            Controls.Add(dtpNgaySinh);
            Controls.Add(lblNgaySinh);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(txtSDT);
            Controls.Add(lblSDT);
            Controls.Add(txtHoTen);
            Controls.Add(lblHoTen);
            Name = "FrmHoiVien";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý hội viên Gym";
            Load += FrmHoiVien_Load;
            grpGioiTinh.ResumeLayout(false);
            grpGioiTinh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHoiVien).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblHoTen;
        private TextBox txtHoTen;
        private Label lblSDT;
        private TextBox txtSDT;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblNgaySinh;
        private DateTimePicker dtpNgaySinh;
        private Label lblHang;
        private ComboBox cboHang;
        private CheckBox chkTrangThai;
        private GroupBox grpGioiTinh;
        private RadioButton rdoNam;
        private RadioButton rdoNu;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private TextBox txtTimTen;
        private Label lblLocHang;
        private ComboBox cboLocHang;
        private Button btnTimKiem;
        private DataGridView dgvHoiVien;
    }
}
