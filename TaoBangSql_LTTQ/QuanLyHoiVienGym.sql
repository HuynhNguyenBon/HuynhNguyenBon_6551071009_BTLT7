CREATE DATABASE QuanLyHoiVienGym_EF;
GO
USE QuanLyHoiVienGym_EF;
GO

CREATE TABLE HoiVien
(
    MaHV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    GioiTinh BIT NOT NULL,
    NgaySinh DATE NOT NULL,
    SDT VARCHAR(15) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    HangThanhVien NVARCHAR(20) NOT NULL
        DEFAULT N'Basic',
    NgayDangKy DATETIME NOT NULL
        DEFAULT GETDATE(),
    TrangThai BIT NOT NULL
        DEFAULT 1
);
GO
INSERT INTO HoiVien
    (HoTen, GioiTinh, NgaySinh, SDT, Email, HangThanhVien, TrangThai)
VALUES
    (N'Nguyễn Văn An', 1, '2000-05-12',
     '0912345678', 'an@gmail.com', N'Basic', 1),

    (N'Trần Thị Bình', 0, '1998-08-20',
     '0987654321', 'binh@gmail.com', N'VIP', 1),

    (N'Lê Văn Cường', 1, '1995-02-10',
     '0901234567', 'cuong@gmail.com', N'Premium', 0);
GO

SELECT * FROM HoiVien;