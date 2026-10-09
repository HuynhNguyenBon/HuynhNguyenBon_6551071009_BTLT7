CREATE DATABASE SunriseHomestay_EF;
GO

USE SunriseHomestay_EF;
GO

CREATE TABLE LoaiPhong
(
    MaLoai INT IDENTITY(1,1) PRIMARY KEY,
    TenLoai NVARCHAR(100) NOT NULL,
    GiaMoiDem DECIMAL(18,2) NOT NULL,
    MoTa NVARCHAR(255) NULL
);
GO

CREATE TABLE Phong
(
    MaPhong INT IDENTITY(1,1) PRIMARY KEY,
    SoPhong VARCHAR(10) NOT NULL,
    TangSo INT NOT NULL,
    TinhTrang NVARCHAR(20) NOT NULL
        DEFAULT N'Trống',
    HinhAnh NVARCHAR(255) NULL,
    MaLoai INT NOT NULL,

    CONSTRAINT FK_Phong_LoaiPhong
        FOREIGN KEY (MaLoai)
        REFERENCES LoaiPhong(MaLoai),

    CONSTRAINT CK_Phong_TinhTrang
        CHECK (TinhTrang IN (N'Trống', N'Đang ở', N'Đang dọn'))
);
GO

INSERT INTO LoaiPhong (TenLoai, GiaMoiDem, MoTa)
VALUES
(N'Phòng đơn', 350000, N'Phòng dành cho một khách'),
(N'Phòng đôi', 550000, N'Phòng dành cho hai khách'),
(N'Phòng VIP', 900000, N'Phòng cao cấp');
GO

INSERT INTO Phong (SoPhong, TangSo, TinhTrang, HinhAnh, MaLoai)
VALUES
('101', 1, N'Trống', NULL, 1),
('201', 2, N'Đang ở', NULL, 2),
('301', 3, N'Đang dọn', NULL, 3);
GO