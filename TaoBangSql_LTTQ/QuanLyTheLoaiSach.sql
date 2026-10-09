CREATE DATABASE QuanLySach_EF;
GO
USE QuanLySach_EF;
GO

CREATE TABLE TheLoaiSach (
    MaTL INT IDENTITY(1,1) PRIMARY KEY,
    TenTheLoai NVARCHAR(100) NOT NULL UNIQUE,
    MoTa NVARCHAR(255) NULL,
    SoLuongSach INT NOT NULL DEFAULT 0,
    NgayTao DATETIME NOT NULL DEFAULT GETDATE()
);
GO
INSERT INTO TheLoaiSach
(
    TenTheLoai,
    MoTa,
    SoLuongSach
)
VALUES
(
    N'Tiểu thuyết',
    N'Các loại sách tiểu thuyết',
    10
),
(
    N'Kỹ năng sống',
    N'Sách phát triển kỹ năng cá nhân',
    5
),
(
    N'Thiếu nhi',
    N'Sách dành cho trẻ em',
    8
),
(
    N'Sách giáo khoa',
    N'Sách giáo khoa các cấp',
    20
);
GO