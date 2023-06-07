BEGIN TRANSACTION;
GO

CREATE UNIQUE INDEX [IX_Users_Login] ON [Users] ([Login]) WHERE [Login] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221026090141_UserLiginIndexMigration', N'6.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP INDEX [IX_Users_Login] ON [Users];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Password');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Users] ALTER COLUMN [Password] nvarchar(100) NOT NULL;
ALTER TABLE [Users] ADD DEFAULT N'' FOR [Password];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Login');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Users] ALTER COLUMN [Login] nvarchar(100) NOT NULL;
ALTER TABLE [Users] ADD DEFAULT N'' FOR [Login];
GO

CREATE TABLE [UserFiles] (
    [Id] bigint NOT NULL IDENTITY,
    [UserId] bigint NOT NULL,
    [FileName] nvarchar(512) NOT NULL,
    [FileJson] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_UserFiles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserFiles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Users_Login] ON [Users] ([Login]);
GO

CREATE INDEX [IX_UserFiles_UserId] ON [UserFiles] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221026091513_UserFileMigration', N'6.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE UNIQUE INDEX [IX_UserFiles_FileName_UserId] ON [UserFiles] ([FileName], [UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221026095051_UserFileUniqueIndexMigration', N'6.0.10');
GO

COMMIT;
GO

