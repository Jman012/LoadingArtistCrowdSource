Build started...
Build succeeded.
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
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

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
GO

CREATE TABLE [DeviceCodes] (
    [UserCode] nvarchar(200) NOT NULL,
    [DeviceCode] nvarchar(200) NOT NULL,
    [SubjectId] nvarchar(200) NULL,
    [SessionId] nvarchar(100) NULL,
    [ClientId] nvarchar(200) NOT NULL,
    [Description] nvarchar(200) NULL,
    [CreationTime] datetime2 NOT NULL,
    [Expiration] datetime2 NOT NULL,
    [Data] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_DeviceCodes] PRIMARY KEY ([UserCode])
);
GO

CREATE TABLE [PersistedGrants] (
    [Key] nvarchar(200) NOT NULL,
    [Type] nvarchar(50) NOT NULL,
    [SubjectId] nvarchar(200) NULL,
    [SessionId] nvarchar(100) NULL,
    [ClientId] nvarchar(200) NOT NULL,
    [Description] nvarchar(200) NULL,
    [CreationTime] datetime2 NOT NULL,
    [Expiration] datetime2 NULL,
    [ConsumedTime] datetime2 NULL,
    [Data] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_PersistedGrants] PRIMARY KEY ([Key])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE UNIQUE INDEX [IX_DeviceCodes_DeviceCode] ON [DeviceCodes] ([DeviceCode]);
GO

CREATE INDEX [IX_DeviceCodes_Expiration] ON [DeviceCodes] ([Expiration]);
GO

CREATE INDEX [IX_PersistedGrants_Expiration] ON [PersistedGrants] ([Expiration]);
GO

CREATE INDEX [IX_PersistedGrants_SubjectId_ClientId_Type] ON [PersistedGrants] ([SubjectId], [ClientId], [Type]);
GO

CREATE INDEX [IX_PersistedGrants_SubjectId_SessionId_Type] ON [PersistedGrants] ([SubjectId], [SessionId], [Type]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'00000000000000_CreateIdentitySchema', N'5.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Comic] (
    [Id] int NOT NULL IDENTITY,
    [Permalink] nvarchar(max) NOT NULL,
    [ComicPublishedDate] datetimeoffset NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [Tooltip] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [ImageUrlSrc] nvarchar(max) NOT NULL,
    [ImportedDate] datetimeoffset NOT NULL,
    [ImportedBy] nvarchar(450) NOT NULL,
    [LastUpdatedDate] datetimeoffset NULL,
    [LastUpdatedBy] nvarchar(450) NULL,
    CONSTRAINT [PK_Comic] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comic_AspNetUsers_ImportedBy] FOREIGN KEY ([ImportedBy]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_Comic_AspNetUsers_LastUpdatedBy] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [AspNetUsers] ([Id])
);
GO

CREATE TABLE [CrowdSourcedFieldDefinition] (
    [Id] uniqueidentifier NOT NULL,
    [IsActive] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    [Type] int NOT NULL,
    [DisplayOrder] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [ShortDescription] nvarchar(max) NOT NULL,
    [LongDescription] nvarchar(max) NOT NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(450) NOT NULL,
    [LastUpdatedDate] datetimeoffset NULL,
    [LastUpdatedBy] nvarchar(450) NULL,
    CONSTRAINT [PK_CrowdSourcedFieldDefinition] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CrowdSourcedFieldDefinition_AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_CrowdSourcedFieldDefinition_AspNetUsers_LastUpdatedBy] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [AspNetUsers] ([Id])
);
GO

CREATE TABLE [ComicHistoryLog] (
    [ComicId] int NOT NULL,
    [Id] int NOT NULL,
    [CrowdSourcedFieldDefinitionId] uniqueidentifier NOT NULL,
    [CreatedBy] nvarchar(450) NULL,
    [LogDate] datetimeoffset NOT NULL,
    [OldValue] nvarchar(max) NULL,
    [NewValue] nvarchar(max) NULL,
    [LogMessage] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ComicHistoryLog] PRIMARY KEY ([ComicId], [Id]),
    CONSTRAINT [FK_ComicHistoryLog_AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_ComicHistoryLog_Comic_ComicId] FOREIGN KEY ([ComicId]) REFERENCES [Comic] ([Id]),
    CONSTRAINT [FK_ComicHistoryLog_CrowdSourcedFieldDefinition_CrowdSourcedFieldDefinitionId] FOREIGN KEY ([CrowdSourcedFieldDefinitionId]) REFERENCES [CrowdSourcedFieldDefinition] ([Id])
);
GO

CREATE TABLE [CrowdSourcedFieldDefinitionFeedback] (
    [CrowdSourcedFieldDefinitionId] uniqueidentifier NOT NULL,
    [Id] int NOT NULL,
    [CreatedBy] nvarchar(450) NOT NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [Comment] nvarchar(max) NOT NULL,
    [CompletionDate] datetimeoffset NULL,
    [CompletedBy] nvarchar(450) NULL,
    [CompletionType] int NULL,
    [CompletionComment] nvarchar(max) NULL,
    CONSTRAINT [PK_CrowdSourcedFieldDefinitionFeedback] PRIMARY KEY ([CrowdSourcedFieldDefinitionId], [Id]),
    CONSTRAINT [FK_CrowdSourcedFieldDefinitionFeedback_AspNetUsers_CompletedBy] FOREIGN KEY ([CompletedBy]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_CrowdSourcedFieldDefinitionFeedback_AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_CrowdSourcedFieldDefinitionFeedback_CrowdSourcedFieldDefinition_CrowdSourcedFieldDefinitionId] FOREIGN KEY ([CrowdSourcedFieldDefinitionId]) REFERENCES [CrowdSourcedFieldDefinition] ([Id])
);
GO

CREATE TABLE [CrowdSourcedFieldDefinitionHistoryLog] (
    [CrowdSourcedFieldDefinitionId] uniqueidentifier NOT NULL,
    [Id] int NOT NULL,
    [CreatedBy] nvarchar(450) NULL,
    [LogDate] datetimeoffset NOT NULL,
    [LogMessage] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_CrowdSourcedFieldDefinitionHistoryLog] PRIMARY KEY ([CrowdSourcedFieldDefinitionId], [Id]),
    CONSTRAINT [FK_CrowdSourcedFieldDefinitionHistoryLog_AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_CrowdSourcedFieldDefinitionHistoryLog_CrowdSourcedFieldDefinition_CrowdSourcedFieldDefinitionId] FOREIGN KEY ([CrowdSourcedFieldDefinitionId]) REFERENCES [CrowdSourcedFieldDefinition] ([Id])
);
GO

CREATE TABLE [CrowdSourcedFieldUserEntry] (
    [ComicId] int NOT NULL,
    [CrowdSourcedFieldDefinitionId] uniqueidentifier NOT NULL,
    [CreatedBy] nvarchar(450) NOT NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastUpdatedDate] datetimeoffset NOT NULL,
    CONSTRAINT [PK_CrowdSourcedFieldUserEntry] PRIMARY KEY ([ComicId], [CrowdSourcedFieldDefinitionId], [CreatedBy]),
    CONSTRAINT [FK_CrowdSourcedFieldUserEntry_AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_CrowdSourcedFieldUserEntry_Comic_ComicId] FOREIGN KEY ([ComicId]) REFERENCES [Comic] ([Id]),
    CONSTRAINT [FK_CrowdSourcedFieldUserEntry_CrowdSourcedFieldDefinition_CrowdSourcedFieldDefinitionId] FOREIGN KEY ([CrowdSourcedFieldDefinitionId]) REFERENCES [CrowdSourcedFieldDefinition] ([Id])
);
GO

CREATE TABLE [CrowdSourcedFieldVerifiedEntry] (
    [ComicId] int NOT NULL,
    [CrowdSourcedFieldDefinitionId] uniqueidentifier NOT NULL,
    [FirstCreatedBy] nvarchar(450) NOT NULL,
    [VerificationDate] datetimeoffset NOT NULL,
    CONSTRAINT [PK_CrowdSourcedFieldVerifiedEntry] PRIMARY KEY ([ComicId], [CrowdSourcedFieldDefinitionId]),
    CONSTRAINT [FK_CrowdSourcedFieldVerifiedEntry_AspNetUsers_FirstCreatedBy] FOREIGN KEY ([FirstCreatedBy]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_CrowdSourcedFieldVerifiedEntry_Comic_ComicId] FOREIGN KEY ([ComicId]) REFERENCES [Comic] ([Id]),
    CONSTRAINT [FK_CrowdSourcedFieldVerifiedEntry_CrowdSourcedFieldDefinition_CrowdSourcedFieldDefinitionId] FOREIGN KEY ([CrowdSourcedFieldDefinitionId]) REFERENCES [CrowdSourcedFieldDefinition] ([Id])
);
GO

CREATE TABLE [CrowdSourcedFieldUserEntryValue] (
    [ComicId] int NOT NULL,
    [CrowdSourcedFieldDefinitionId] uniqueidentifier NOT NULL,
    [CreatedBy] nvarchar(450) NOT NULL,
    [Id] int NOT NULL,
    [Value] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_CrowdSourcedFieldUserEntryValue] PRIMARY KEY ([ComicId], [CrowdSourcedFieldDefinitionId], [CreatedBy], [Id]),
    CONSTRAINT [FK_CrowdSourcedFieldUserEntryValue_AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_CrowdSourcedFieldUserEntryValue_Comic_ComicId] FOREIGN KEY ([ComicId]) REFERENCES [Comic] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CrowdSourcedFieldUserEntryValue_CrowdSourcedFieldDefinition_CrowdSourcedFieldDefinitionId] FOREIGN KEY ([CrowdSourcedFieldDefinitionId]) REFERENCES [CrowdSourcedFieldDefinition] ([Id]),
    CONSTRAINT [FK_CrowdSourcedFieldUserEntryValue_CrowdSourcedFieldUserEntry_ComicId_CrowdSourcedFieldDefinitionId_CreatedBy] FOREIGN KEY ([ComicId], [CrowdSourcedFieldDefinitionId], [CreatedBy]) REFERENCES [CrowdSourcedFieldUserEntry] ([ComicId], [CrowdSourcedFieldDefinitionId], [CreatedBy])
);
GO

CREATE TABLE [CrowdSourcedFieldVerifiedEntryValue] (
    [ComicId] int NOT NULL,
    [CrowdSourcedFieldDefinitionId] uniqueidentifier NOT NULL,
    [Id] int NOT NULL,
    [Value] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_CrowdSourcedFieldVerifiedEntryValue] PRIMARY KEY ([ComicId], [CrowdSourcedFieldDefinitionId], [Id]),
    CONSTRAINT [FK_CrowdSourcedFieldVerifiedEntryValue_Comic_ComicId] FOREIGN KEY ([ComicId]) REFERENCES [Comic] ([Id]),
    CONSTRAINT [FK_CrowdSourcedFieldVerifiedEntryValue_CrowdSourcedFieldDefinition_CrowdSourcedFieldDefinitionId] FOREIGN KEY ([CrowdSourcedFieldDefinitionId]) REFERENCES [CrowdSourcedFieldDefinition] ([Id]),
    CONSTRAINT [FK_CrowdSourcedFieldVerifiedEntryValue_CrowdSourcedFieldVerifiedEntry_ComicId_CrowdSourcedFieldDefinitionId] FOREIGN KEY ([ComicId], [CrowdSourcedFieldDefinitionId]) REFERENCES [CrowdSourcedFieldVerifiedEntry] ([ComicId], [CrowdSourcedFieldDefinitionId])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ComicPublishedDate', N'Description', N'ImageUrlSrc', N'ImportedBy', N'ImportedDate', N'LastUpdatedBy', N'LastUpdatedDate', N'Permalink', N'Title', N'Tooltip') AND [object_id] = OBJECT_ID(N'[Comic]'))
    SET IDENTITY_INSERT [Comic] ON;
INSERT INTO [Comic] ([Id], [ComicPublishedDate], [Description], [ImageUrlSrc], [ImportedBy], [ImportedDate], [LastUpdatedBy], [LastUpdatedDate], [Permalink], [Title], [Tooltip])
VALUES (1, '2011-01-04T00:00:00.0000000-08:00', NULL, N'https://loadingartist.com/wp-content/uploads/2011/07/2011-01-04-born.png', N'jman012guy@gmail.com', '2021-01-13T21:26:37.3170627-08:00', NULL, NULL, N'https://loadingartist.com/comic/born/', N'Born', N'Born');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ComicPublishedDate', N'Description', N'ImageUrlSrc', N'ImportedBy', N'ImportedDate', N'LastUpdatedBy', N'LastUpdatedDate', N'Permalink', N'Title', N'Tooltip') AND [object_id] = OBJECT_ID(N'[Comic]'))
    SET IDENTITY_INSERT [Comic] OFF;
GO

CREATE INDEX [IX_Comic_ImportedBy] ON [Comic] ([ImportedBy]);
GO

CREATE INDEX [IX_Comic_LastUpdatedBy] ON [Comic] ([LastUpdatedBy]);
GO

CREATE INDEX [IX_ComicHistoryLog_CreatedBy] ON [ComicHistoryLog] ([CreatedBy]);
GO

CREATE INDEX [IX_ComicHistoryLog_CrowdSourcedFieldDefinitionId] ON [ComicHistoryLog] ([CrowdSourcedFieldDefinitionId]);
GO

CREATE INDEX [IX_CrowdSourcedFieldDefinition_CreatedBy] ON [CrowdSourcedFieldDefinition] ([CreatedBy]);
GO

CREATE INDEX [IX_CrowdSourcedFieldDefinition_LastUpdatedBy] ON [CrowdSourcedFieldDefinition] ([LastUpdatedBy]);
GO

CREATE INDEX [IX_CrowdSourcedFieldDefinitionFeedback_CompletedBy] ON [CrowdSourcedFieldDefinitionFeedback] ([CompletedBy]);
GO

CREATE INDEX [IX_CrowdSourcedFieldDefinitionFeedback_CreatedBy] ON [CrowdSourcedFieldDefinitionFeedback] ([CreatedBy]);
GO

CREATE INDEX [IX_CrowdSourcedFieldDefinitionHistoryLog_CreatedBy] ON [CrowdSourcedFieldDefinitionHistoryLog] ([CreatedBy]);
GO

CREATE INDEX [IX_CrowdSourcedFieldUserEntry_CreatedBy] ON [CrowdSourcedFieldUserEntry] ([CreatedBy]);
GO

CREATE INDEX [IX_CrowdSourcedFieldUserEntry_CrowdSourcedFieldDefinitionId] ON [CrowdSourcedFieldUserEntry] ([CrowdSourcedFieldDefinitionId]);
GO

CREATE INDEX [IX_CrowdSourcedFieldUserEntryValue_CreatedBy] ON [CrowdSourcedFieldUserEntryValue] ([CreatedBy]);
GO

CREATE INDEX [IX_CrowdSourcedFieldUserEntryValue_CrowdSourcedFieldDefinitionId] ON [CrowdSourcedFieldUserEntryValue] ([CrowdSourcedFieldDefinitionId]);
GO

CREATE INDEX [IX_CrowdSourcedFieldVerifiedEntry_CrowdSourcedFieldDefinitionId] ON [CrowdSourcedFieldVerifiedEntry] ([CrowdSourcedFieldDefinitionId]);
GO

CREATE INDEX [IX_CrowdSourcedFieldVerifiedEntry_FirstCreatedBy] ON [CrowdSourcedFieldVerifiedEntry] ([FirstCreatedBy]);
GO

CREATE INDEX [IX_CrowdSourcedFieldVerifiedEntryValue_CrowdSourcedFieldDefinitionId] ON [CrowdSourcedFieldVerifiedEntryValue] ([CrowdSourcedFieldDefinitionId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210114052637_InitialSchema', N'5.0.1');
GO

COMMIT;
GO


