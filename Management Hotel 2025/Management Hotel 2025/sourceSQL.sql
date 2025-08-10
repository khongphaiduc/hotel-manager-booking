IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [room_types] (
    [room_type_id] int NOT NULL IDENTITY,
    [name] nvarchar(100) NOT NULL,
    [description] nvarchar(max) NULL,
    [price] decimal(12,2) NOT NULL,
    [max_guests] int NOT NULL,
    CONSTRAINT [PK__room_typ__42395E8438F59C5E] PRIMARY KEY ([room_type_id])
);

CREATE TABLE [users] (
    [user_id] int NOT NULL IDENTITY,
    [username] nvarchar(50) NOT NULL,
    [password_hash] nvarchar(255) NOT NULL,
    [email] nvarchar(100) NOT NULL,
    [full_name] nvarchar(100) NULL,
    [phone_number] nvarchar(20) NULL,
    [role] nvarchar(10) NOT NULL,
    [created_at] datetime NULL DEFAULT ((getdate())),
    [updated_at] datetime NULL DEFAULT ((getdate())),
    CONSTRAINT [PK__users__B9BE370F41C6AE08] PRIMARY KEY ([user_id])
);

CREATE TABLE [rooms] (
    [room_id] int NOT NULL IDENTITY,
    [room_type_id] int NOT NULL,
    [room_number] nvarchar(20) NOT NULL,
    [floor] int NULL,
    [status] nvarchar(20) NULL DEFAULT N'available',
    CONSTRAINT [PK__rooms__19675A8A1E843B59] PRIMARY KEY ([room_id]),
    CONSTRAINT [FK__rooms__room_type__5441852A] FOREIGN KEY ([room_type_id]) REFERENCES [room_types] ([room_type_id])
);

CREATE TABLE [notifications] (
    [notification_id] int NOT NULL IDENTITY,
    [user_id] int NOT NULL,
    [message] nvarchar(max) NOT NULL,
    [is_read] bit NULL DEFAULT CAST(0 AS bit),
    [created_at] datetime NULL DEFAULT ((getdate())),
    CONSTRAINT [PK__notifica__E059842F10D16AE6] PRIMARY KEY ([notification_id]),
    CONSTRAINT [FK__notificat__user___693CA210] FOREIGN KEY ([user_id]) REFERENCES [users] ([user_id])
);

CREATE TABLE [reviews] (
    [review_id] int NOT NULL IDENTITY,
    [user_id] int NOT NULL,
    [rating] int NULL,
    [comment] nvarchar(max) NULL,
    [created_at] datetime NULL DEFAULT ((getdate())),
    CONSTRAINT [PK__reviews__60883D904168ABEB] PRIMARY KEY ([review_id]),
    CONSTRAINT [FK__reviews__user_id__6477ECF3] FOREIGN KEY ([user_id]) REFERENCES [users] ([user_id])
);

CREATE TABLE [staff_actions] (
    [action_id] int NOT NULL IDENTITY,
    [staff_id] int NOT NULL,
    [action] nvarchar(255) NOT NULL,
    [action_time] datetime NULL DEFAULT ((getdate())),
    CONSTRAINT [PK__staff_ac__74EFC21721148A1D] PRIMARY KEY ([action_id]),
    CONSTRAINT [FK__staff_act__staff__5FB337D6] FOREIGN KEY ([staff_id]) REFERENCES [users] ([user_id])
);

CREATE TABLE [bookings] (
    [booking_id] int NOT NULL IDENTITY,
    [user_id] int NOT NULL,
    [room_id] int NOT NULL,
    [check_in] date NOT NULL,
    [check_out] date NOT NULL,
    [guests] int NOT NULL,
    [status] nvarchar(20) NULL DEFAULT N'pending',
    [created_at] datetime NULL DEFAULT ((getdate())),
    [updated_at] datetime NULL DEFAULT ((getdate())),
    CONSTRAINT [PK__bookings__5DE3A5B1846038DC] PRIMARY KEY ([booking_id]),
    CONSTRAINT [FK__bookings__room_i__5BE2A6F2] FOREIGN KEY ([room_id]) REFERENCES [rooms] ([room_id]),
    CONSTRAINT [FK__bookings__user_i__5AEE82B9] FOREIGN KEY ([user_id]) REFERENCES [users] ([user_id])
);

CREATE INDEX [IX_bookings_room_id] ON [bookings] ([room_id]);

CREATE INDEX [IX_bookings_user_id] ON [bookings] ([user_id]);

CREATE INDEX [IX_notifications_user_id] ON [notifications] ([user_id]);

CREATE INDEX [IX_reviews_user_id] ON [reviews] ([user_id]);

CREATE INDEX [IX_rooms_room_type_id] ON [rooms] ([room_type_id]);

CREATE INDEX [IX_staff_actions_staff_id] ON [staff_actions] ([staff_id]);

CREATE UNIQUE INDEX [UQ__users__AB6E6164AD37B433] ON [users] ([email]);

CREATE UNIQUE INDEX [UQ__users__F3DBC572794F2AF0] ON [users] ([username]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250810165008_V0.0', N'9.0.8');

COMMIT;
GO

