
-- 1. Bảng người dùng
CREATE TABLE users (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
    username NVARCHAR(50) UNIQUE NOT NULL,
    password_hash NVARCHAR(255) NOT NULL,
    email NVARCHAR(100) UNIQUE NOT NULL,
    full_name NVARCHAR(100),
    phone_number NVARCHAR(20),
    role NVARCHAR(10) CHECK (role IN ('admin', 'staff', 'user')) NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
);

-- 2. Bảng loại phòng
CREATE TABLE room_types (
    room_type_id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL,
    description NVARCHAR(MAX),
    price DECIMAL(12,2) NOT NULL,
    max_guests INT NOT NULL
);

-- 3. Bảng phòng
CREATE TABLE rooms (
    room_id INT IDENTITY(1,1) PRIMARY KEY,
    room_type_id INT NOT NULL,
    room_number NVARCHAR(20) NOT NULL,
    floor INT,
    status NVARCHAR(20) CHECK (status IN ('available', 'booked', 'maintenance')) DEFAULT 'available',
    FOREIGN KEY (room_type_id) REFERENCES room_types(room_type_id)
);

-- 4. Bảng đặt phòng
CREATE TABLE bookings (
    booking_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    room_id INT NOT NULL,
    check_in DATE NOT NULL,
    check_out DATE NOT NULL,
    guests INT NOT NULL,
    status NVARCHAR(20) CHECK (status IN ('pending', 'confirmed', 'cancelled', 'checked_in', 'checked_out')) DEFAULT 'pending',
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES users(user_id),
    FOREIGN KEY (room_id) REFERENCES rooms(room_id)
);

-- 5. Bảng lịch sử thao tác của staff
CREATE TABLE staff_actions (
    action_id INT IDENTITY(1,1) PRIMARY KEY,
    staff_id INT NOT NULL,
    action NVARCHAR(255) NOT NULL,
    action_time DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (staff_id) REFERENCES users(user_id)
);

-- 6. Bảng đánh giá/phản hồi
CREATE TABLE reviews (
    review_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    rating INT CHECK (rating BETWEEN 1 AND 5),
    comment NVARCHAR(MAX),
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);

-- 7. Bảng thông báo
CREATE TABLE notifications (
    notification_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    message NVARCHAR(MAX) NOT NULL,
    is_read BIT DEFAULT 0,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);