IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE TABLE [Complexity] (
        [ID] int NOT NULL IDENTITY,
        [Nazov] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Complexity] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE TABLE [Generator] (
        [ID] int NOT NULL IDENTITY,
        [NazovProfilu] nvarchar(max) NOT NULL,
        [PocetUnikatnych] int NOT NULL,
        CONSTRAINT [PK_Generator] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE TABLE [MealKind] (
        [ID] int NOT NULL IDENTITY,
        [Nazov] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_MealKind] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE TABLE [Menu] (
        [ID] int NOT NULL IDENTITY,
        [DatumVytvorenia] datetime2 NOT NULL,
        [DatumPondelka] datetime2 NOT NULL,
        CONSTRAINT [PK_Menu] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE TABLE [Popularity] (
        [ID] int NOT NULL IDENTITY,
        [Nazov] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Popularity] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE TABLE [Price] (
        [ID] int NOT NULL IDENTITY,
        [Cena] decimal(4,2) NOT NULL,
        CONSTRAINT [PK_Price] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE TABLE [SoupKind] (
        [ID] int NOT NULL IDENTITY,
        [Nazov] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_SoupKind] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE TABLE [Volume] (
        [ID] int NOT NULL IDENTITY,
        [Objem] int NOT NULL,
        CONSTRAINT [PK_Volume] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE TABLE [WeekDay] (
        [ID] int NOT NULL IDENTITY,
        [Nazov] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_WeekDay] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE TABLE [Weight] (
        [ID] int NOT NULL IDENTITY,
        [Hmotnost] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Weight] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(128) NOT NULL,
        [ProviderKey] nvarchar(128) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(128) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE TABLE [GeneratorOptions] (
        [ID] int NOT NULL IDENTITY,
        [DayID] int NOT NULL,
        [GeneratorID] int NOT NULL,
        [MealKindID] int NOT NULL,
        [PopularityID] int NOT NULL,
        [PoradieJedla] int NOT NULL,
        CONSTRAINT [PK_GeneratorOptions] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_GeneratorOptions_WeekDay_DayID] FOREIGN KEY ([DayID]) REFERENCES [WeekDay] ([ID]) ON DELETE CASCADE,
        CONSTRAINT [FK_GeneratorOptions_Generator_GeneratorID] FOREIGN KEY ([GeneratorID]) REFERENCES [Generator] ([ID]) ON DELETE CASCADE,
        CONSTRAINT [FK_GeneratorOptions_MealKind_MealKindID] FOREIGN KEY ([MealKindID]) REFERENCES [MealKind] ([ID]) ON DELETE CASCADE,
        CONSTRAINT [FK_GeneratorOptions_Popularity_PopularityID] FOREIGN KEY ([PopularityID]) REFERENCES [Popularity] ([ID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE TABLE [Meal] (
        [ID] int NOT NULL IDENTITY,
        [MealKindID] int NULL,
        [SoupKindID] int NULL,
        [Nazov] nvarchar(max) NOT NULL,
        [PopularityID] int NOT NULL,
        [ComplexityID] int NOT NULL,
        [PriceID] int NOT NULL,
        [WeightID] int NULL,
        [VolumeID] int NULL,
        [Alergeny] nvarchar(max) NULL,
        CONSTRAINT [PK_Meal] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_Meal_Complexity_ComplexityID] FOREIGN KEY ([ComplexityID]) REFERENCES [Complexity] ([ID]) ON DELETE CASCADE,
        CONSTRAINT [FK_Meal_MealKind_MealKindID] FOREIGN KEY ([MealKindID]) REFERENCES [MealKind] ([ID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Meal_Popularity_PopularityID] FOREIGN KEY ([PopularityID]) REFERENCES [Popularity] ([ID]) ON DELETE CASCADE,
        CONSTRAINT [FK_Meal_Price_PriceID] FOREIGN KEY ([PriceID]) REFERENCES [Price] ([ID]) ON DELETE CASCADE,
        CONSTRAINT [FK_Meal_SoupKind_SoupKindID] FOREIGN KEY ([SoupKindID]) REFERENCES [SoupKind] ([ID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Meal_Volume_VolumeID] FOREIGN KEY ([VolumeID]) REFERENCES [Volume] ([ID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Meal_Weight_WeightID] FOREIGN KEY ([WeightID]) REFERENCES [Weight] ([ID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE TABLE [Schedule] (
        [ID] int NOT NULL IDENTITY,
        [DatumPodavania] datetime2 NOT NULL,
        [MenuID] int NOT NULL,
        [MealID] int NOT NULL,
        [PoradieJedla] int NOT NULL,
        CONSTRAINT [PK_Schedule] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_Schedule_Meal_MealID] FOREIGN KEY ([MealID]) REFERENCES [Meal] ([ID]) ON DELETE CASCADE,
        CONSTRAINT [FK_Schedule_Menu_MenuID] FOREIGN KEY ([MenuID]) REFERENCES [Menu] ([ID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE INDEX [IX_GeneratorOptions_DayID] ON [GeneratorOptions] ([DayID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE INDEX [IX_GeneratorOptions_GeneratorID] ON [GeneratorOptions] ([GeneratorID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE INDEX [IX_GeneratorOptions_MealKindID] ON [GeneratorOptions] ([MealKindID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE INDEX [IX_GeneratorOptions_PopularityID] ON [GeneratorOptions] ([PopularityID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE INDEX [IX_Meal_ComplexityID] ON [Meal] ([ComplexityID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE INDEX [IX_Meal_MealKindID] ON [Meal] ([MealKindID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE INDEX [IX_Meal_PopularityID] ON [Meal] ([PopularityID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE INDEX [IX_Meal_PriceID] ON [Meal] ([PriceID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE INDEX [IX_Meal_SoupKindID] ON [Meal] ([SoupKindID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE INDEX [IX_Meal_VolumeID] ON [Meal] ([VolumeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE INDEX [IX_Meal_WeightID] ON [Meal] ([WeightID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE INDEX [IX_Schedule_MealID] ON [Schedule] ([MealID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    CREATE INDEX [IX_Schedule_MenuID] ON [Schedule] ([MenuID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201124195614_all-in-one')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201124195614_all-in-one', N'3.1.10');
END;

GO

