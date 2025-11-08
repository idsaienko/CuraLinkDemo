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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250914163328_InitialCreate'
)
BEGIN
    CREATE TABLE [ApiKeys] (
        [Id] int NOT NULL IDENTITY,
        [KeyHash] nvarchar(max) NOT NULL,
        [Owner] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [ExpiresAt] datetime2 NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_ApiKeys] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250914163328_InitialCreate'
)
BEGIN
    CREATE TABLE [Residents] (
        [ResidentId] int NOT NULL IDENTITY,
        [FullName] nvarchar(max) NOT NULL,
        [RoomNumber] nvarchar(max) NOT NULL,
        [CareLevel] int NOT NULL,
        [DateOfBirth] datetime2 NOT NULL,
        [Notes] nvarchar(max) NULL,
        CONSTRAINT [PK_Residents] PRIMARY KEY ([ResidentId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250914163328_InitialCreate'
)
BEGIN
    CREATE TABLE [Staff] (
        [StaffId] int NOT NULL IDENTITY,
        [FullName] nvarchar(max) NOT NULL,
        [Role] int NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Staff] PRIMARY KEY ([StaffId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250914163328_InitialCreate'
)
BEGIN
    CREATE TABLE [Medications] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Dosage] nvarchar(max) NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NOT NULL,
        [ResidentId] int NOT NULL,
        CONSTRAINT [PK_Medications] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Medications_Residents_ResidentId] FOREIGN KEY ([ResidentId]) REFERENCES [Residents] ([ResidentId]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250914163328_InitialCreate'
)
BEGIN
    CREATE TABLE [PainObservations] (
        [Id] int NOT NULL,
        [Type] nvarchar(max) NOT NULL,
        [Amount] int NOT NULL,
        [Time] datetime2 NOT NULL,
        [ResidentId] int NOT NULL,
        CONSTRAINT [FK_PainObservations_Residents_ResidentId] FOREIGN KEY ([ResidentId]) REFERENCES [Residents] ([ResidentId]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250914163328_InitialCreate'
)
BEGIN
    CREATE TABLE [VitalSigns] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Rate] int NOT NULL,
        [Time] datetime2 NOT NULL,
        [ResidentId] int NOT NULL,
        CONSTRAINT [PK_VitalSigns] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_VitalSigns_Residents_ResidentId] FOREIGN KEY ([ResidentId]) REFERENCES [Residents] ([ResidentId]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250914163328_InitialCreate'
)
BEGIN
    CREATE TABLE [Appointments] (
        [AppointmentId] int NOT NULL IDENTITY,
        [ResidentId] int NOT NULL,
        [StaffId] int NOT NULL,
        [DateTime] datetime2 NOT NULL,
        [Type] nvarchar(max) NOT NULL,
        [Notes] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Appointments] PRIMARY KEY ([AppointmentId]),
        CONSTRAINT [FK_Appointments_Residents_ResidentId] FOREIGN KEY ([ResidentId]) REFERENCES [Residents] ([ResidentId]) ON DELETE CASCADE,
        CONSTRAINT [FK_Appointments_Staff_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [Staff] ([StaffId]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250914163328_InitialCreate'
)
BEGIN
    CREATE TABLE [Reports] (
        [Id] int NOT NULL IDENTITY,
        [Message] nvarchar(max) NOT NULL,
        [StaffId] int NOT NULL,
        CONSTRAINT [PK_Reports] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Reports_Staff_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [Staff] ([StaffId]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250914163328_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Appointments_ResidentId] ON [Appointments] ([ResidentId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250914163328_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Appointments_StaffId] ON [Appointments] ([StaffId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250914163328_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Medications_ResidentId] ON [Medications] ([ResidentId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250914163328_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_PainObservations_ResidentId] ON [PainObservations] ([ResidentId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250914163328_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Reports_StaffId] ON [Reports] ([StaffId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250914163328_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_VitalSigns_ResidentId] ON [VitalSigns] ([ResidentId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250914163328_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250914163328_InitialCreate', N'9.0.9');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250930015602_FirstCreate'
)
BEGIN
    EXEC sp_rename N'[PainObservations].[Amount]', N'Score', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250930015602_FirstCreate'
)
BEGIN
    ALTER TABLE [Residents] ADD [Phone] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250930015602_FirstCreate'
)
BEGIN
    ALTER TABLE [Residents] ADD [PhotoUrl] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250930015602_FirstCreate'
)
BEGIN
    ALTER TABLE [Reports] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250930015602_FirstCreate'
)
BEGIN
    ALTER TABLE [Reports] ADD [ResidentId] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250930015602_FirstCreate'
)
BEGIN
    ALTER TABLE [PainObservations] ADD [Location] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250930015602_FirstCreate'
)
BEGIN
    ALTER TABLE [PainObservations] ADD [Notes] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250930015602_FirstCreate'
)
BEGIN
    CREATE TABLE [Ausscheidungen] (
        [Id] int NOT NULL IDENTITY,
        [ResidentId] int NOT NULL,
        [StaffId] int NOT NULL,
        [Time] datetime2 NOT NULL,
        [Abstand] nvarchar(max) NOT NULL,
        [Menge] nvarchar(max) NOT NULL,
        [Konsistenz] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Ausscheidungen] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Ausscheidungen_Residents_ResidentId] FOREIGN KEY ([ResidentId]) REFERENCES [Residents] ([ResidentId]) ON DELETE CASCADE,
        CONSTRAINT [FK_Ausscheidungen_Staff_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [Staff] ([StaffId]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250930015602_FirstCreate'
)
BEGIN
    CREATE TABLE [MealSchedules] (
        [Id] int NOT NULL IDENTITY,
        [ResidentId] int NOT NULL,
        [MealType] nvarchar(max) NOT NULL,
        [MealTime] datetime2 NOT NULL,
        [Comments] nvarchar(max) NOT NULL,
        [MealName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_MealSchedules] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_MealSchedules_Residents_ResidentId] FOREIGN KEY ([ResidentId]) REFERENCES [Residents] ([ResidentId]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250930015602_FirstCreate'
)
BEGIN
    CREATE TABLE [ResidentMovements] (
        [Id] int NOT NULL IDENTITY,
        [ResidentId] int NOT NULL,
        [StaffId] int NOT NULL,
        [Room] nvarchar(max) NOT NULL,
        [Object] nvarchar(max) NOT NULL,
        [Angle] float NULL,
        [MovementTime] datetime2 NOT NULL,
        [Notes] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ResidentMovements] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ResidentMovements_Residents_ResidentId] FOREIGN KEY ([ResidentId]) REFERENCES [Residents] ([ResidentId]) ON DELETE CASCADE,
        CONSTRAINT [FK_ResidentMovements_Staff_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [Staff] ([StaffId]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250930015602_FirstCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ResidentId', N'CareLevel', N'DateOfBirth', N'FullName', N'Notes', N'Phone', N'PhotoUrl', N'RoomNumber') AND [object_id] = OBJECT_ID(N'[Residents]'))
        SET IDENTITY_INSERT [Residents] ON;
    EXEC(N'INSERT INTO [Residents] ([ResidentId], [CareLevel], [DateOfBirth], [FullName], [Notes], [Phone], [PhotoUrl], [RoomNumber])
    VALUES (1, 0, ''0001-01-01T00:00:00.0000000'', N''Max Muster'', N''Vegetarisch, lactosefrei'', N''+49 176 12345678'', N''/images/residents/max.jpg'', N''101A'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ResidentId', N'CareLevel', N'DateOfBirth', N'FullName', N'Notes', N'Phone', N'PhotoUrl', N'RoomNumber') AND [object_id] = OBJECT_ID(N'[Residents]'))
        SET IDENTITY_INSERT [Residents] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250930015602_FirstCreate'
)
BEGIN
    CREATE INDEX [IX_Ausscheidungen_ResidentId] ON [Ausscheidungen] ([ResidentId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250930015602_FirstCreate'
)
BEGIN
    CREATE INDEX [IX_Ausscheidungen_StaffId] ON [Ausscheidungen] ([StaffId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250930015602_FirstCreate'
)
BEGIN
    CREATE INDEX [IX_MealSchedules_ResidentId] ON [MealSchedules] ([ResidentId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250930015602_FirstCreate'
)
BEGIN
    CREATE INDEX [IX_ResidentMovements_ResidentId] ON [ResidentMovements] ([ResidentId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250930015602_FirstCreate'
)
BEGIN
    CREATE INDEX [IX_ResidentMovements_StaffId] ON [ResidentMovements] ([StaffId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250930015602_FirstCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250930015602_FirstCreate', N'9.0.9');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251002204909_SeedInitialData'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Comments', N'MealName', N'MealTime', N'MealType', N'ResidentId') AND [object_id] = OBJECT_ID(N'[MealSchedules]'))
        SET IDENTITY_INSERT [MealSchedules] ON;
    EXEC(N'INSERT INTO [MealSchedules] ([Id], [Comments], [MealName], [MealTime], [MealType], [ResidentId])
    VALUES (1, N''Normal'', N'''', ''2025-11-22T08:00:00.0000000'', N''Frühstück'', 1),
    (2, N''Vegetarisch'', N'''', ''2025-11-22T12:30:00.0000000'', N''Mittagessen'', 1),
    (3, N''Obst'', N'''', ''2025-11-22T15:00:00.0000000'', N''Zwischenmahlzeit'', 1),
    (4, N''Leicht'', N'''', ''2025-11-22T18:30:00.0000000'', N''Abendessen'', 1)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Comments', N'MealName', N'MealTime', N'MealType', N'ResidentId') AND [object_id] = OBJECT_ID(N'[MealSchedules]'))
        SET IDENTITY_INSERT [MealSchedules] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251002204909_SeedInitialData'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'StaffId', N'FullName', N'PhoneNumber', N'Role') AND [object_id] = OBJECT_ID(N'[Staff]'))
        SET IDENTITY_INSERT [Staff] ON;
    EXEC(N'INSERT INTO [Staff] ([StaffId], [FullName], [PhoneNumber], [Role])
    VALUES (1, N''Anna Schmidt'', N''+491234567890'', 2)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'StaffId', N'FullName', N'PhoneNumber', N'Role') AND [object_id] = OBJECT_ID(N'[Staff]'))
        SET IDENTITY_INSERT [Staff] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251002204909_SeedInitialData'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'AppointmentId', N'DateTime', N'Notes', N'ResidentId', N'StaffId', N'Type') AND [object_id] = OBJECT_ID(N'[Appointments]'))
        SET IDENTITY_INSERT [Appointments] ON;
    EXEC(N'INSERT INTO [Appointments] ([AppointmentId], [DateTime], [Notes], [ResidentId], [StaffId], [Type])
    VALUES (1, ''2025-12-15T10:15:00.0000000'', N''Hausarzt'', 1, 1, N''Arztbesuch'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'AppointmentId', N'DateTime', N'Notes', N'ResidentId', N'StaffId', N'Type') AND [object_id] = OBJECT_ID(N'[Appointments]'))
        SET IDENTITY_INSERT [Appointments] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251002204909_SeedInitialData'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Abstand', N'Konsistenz', N'Menge', N'ResidentId', N'StaffId', N'Time') AND [object_id] = OBJECT_ID(N'[Ausscheidungen]'))
        SET IDENTITY_INSERT [Ausscheidungen] ON;
    EXEC(N'INSERT INTO [Ausscheidungen] ([Id], [Abstand], [Konsistenz], [Menge], [ResidentId], [StaffId], [Time])
    VALUES (1, N''3h'', N''normal'', N''200ml'', 1, 1, ''2025-10-02T14:00:00.0000000'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Abstand', N'Konsistenz', N'Menge', N'ResidentId', N'StaffId', N'Time') AND [object_id] = OBJECT_ID(N'[Ausscheidungen]'))
        SET IDENTITY_INSERT [Ausscheidungen] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251002204909_SeedInitialData'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Angle', N'MovementTime', N'Notes', N'Object', N'ResidentId', N'Room', N'StaffId') AND [object_id] = OBJECT_ID(N'[ResidentMovements]'))
        SET IDENTITY_INSERT [ResidentMovements] ON;
    EXEC(N'INSERT INTO [ResidentMovements] ([Id], [Angle], [MovementTime], [Notes], [Object], [ResidentId], [Room], [StaffId])
    VALUES (1, 0.0E0, ''2025-10-02T18:00:00.0000000'', N''Transfer ins Bett'', N''Bett'', 1, N''Zimmer 101'', 1),
    (2, 90.0E0, ''2025-10-02T13:24:00.0000000'', N''Transfer in Rollstuhl'', N''Rollstuhl'', 1, N''Zimmer 101'', 1)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Angle', N'MovementTime', N'Notes', N'Object', N'ResidentId', N'Room', N'StaffId') AND [object_id] = OBJECT_ID(N'[ResidentMovements]'))
        SET IDENTITY_INSERT [ResidentMovements] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251002204909_SeedInitialData'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251002204909_SeedInitialData', N'9.0.9');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251009193432_DBUpdate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251009193432_DBUpdate', N'9.0.9');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251018034944_SeedStaffData'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251018034944_SeedStaffData', N'9.0.9');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251030143311_Init'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251030143311_Init', N'9.0.9');
END;

COMMIT;
GO

