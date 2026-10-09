
CREATE DATABASE AnKhangClinic;
GO

USE AnKhangClinic;
GO

CREATE TABLE BacSi
(
    MaBS INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    ChuyenKhoa NVARCHAR(100),
    SDT VARCHAR(15)
);
GO

CREATE TABLE LichKham
(
    MaLich INT IDENTITY(1,1) PRIMARY KEY,
    TenBenhNhan NVARCHAR(100) NOT NULL,
    SDT VARCHAR(15),
    NgayKham DATE NOT NULL,
    GioKham TIME NOT NULL,
    MaBS INT NOT NULL,
    TrangThai NVARCHAR(20) NOT NULL
        DEFAULT N'Chờ khám',

    CONSTRAINT FK_LichKham_BacSi
        FOREIGN KEY (MaBS) REFERENCES BacSi(MaBS),

    CONSTRAINT CK_LichKham_TrangThai
        CHECK (TrangThai IN
        (N'Chờ khám', N'Đã khám', N'Đã hủy'))
);
GO

INSERT INTO BacSi (HoTen, ChuyenKhoa, SDT)
VALUES
(N'Nguyễn Văn A', N'Nội tổng quát', '0901234567'),
(N'Nguyễn Văn B', N'Nội tổng quát', '0912345678'),
(N'Nguyễn Văn C', N'Nhi khoa', '0923456789'),
(N'Nguyễn Văn D', N'Nội tổng quát', '0934567890');
GO

INSERT INTO LichKham
    (TenBenhNhan, SDT, NgayKham, GioKham, MaBS, TrangThai)
VALUES
(N'Nguyễn Xuân B', '09725667894', '2027-03-15', '09:00', 1, N'Chờ khám'),
(N'Nguyễn Hàm', '09725667899', '2027-03-16', '10:00', 2, N'Đã khám'),
(N'Nguyễn Tính', '09725667890', '2027-03-17', '16:00', 3, N'Đã hủy');
GO
