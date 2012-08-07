/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_SpottedException
	(
	K int NOT NULL IDENTITY (1, 1),
	ParentK int NULL,
	ExceptionDateTime datetime NOT NULL,
	ExceptionType varchar(50) NULL,
	Message varchar(4000) NULL,
	Source varchar(50) NULL,
	StackTrace varchar(4000) NULL,
	Url varchar(150) NULL,
	MasterPath varchar(50) NULL,
	PagePath varchar(50) NULL,
	CurrentFilter varchar(150) NULL,
	ObjectFilterK int NULL,
	ObjectFilterType int NULL,
	MachineName varchar(50) NULL,
	UsrK int NULL,
	DsiGuid uniqueidentifier NULL,
	Cookies varchar(MAX) NULL,
	PostData varchar(MAX) NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
DECLARE @v sql_variant 
SET @v = N'Log of Exceptions thrown from the Spotted website'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_SpottedException', NULL, NULL
GO
DECLARE @v sql_variant 
SET @v = cast(N'K of the Exception' as varchar(18))
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_SpottedException', N'COLUMN', N'K'
GO
DECLARE @v sql_variant 
SET @v = cast(N'K of the parent Exception, when this is an InnerException' as varchar(57))
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_SpottedException', N'COLUMN', N'ParentK'
GO
DECLARE @v sql_variant 
SET @v = cast(N'Time of logging Exception' as varchar(25))
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_SpottedException', N'COLUMN', N'ExceptionDateTime'
GO
DECLARE @v sql_variant 
SET @v = cast(N'The type of Exception' as varchar(21))
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_SpottedException', N'COLUMN', N'ExceptionType'
GO
DECLARE @v sql_variant 
SET @v = cast(N'Exception message' as varchar(17))
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_SpottedException', N'COLUMN', N'Message'
GO
DECLARE @v sql_variant 
SET @v = cast(N'Exception source' as varchar(16))
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_SpottedException', N'COLUMN', N'Source'
GO
DECLARE @v sql_variant 
SET @v = cast(N'Exception stack trace' as varchar(21))
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_SpottedException', N'COLUMN', N'StackTrace'
GO
DECLARE @v sql_variant 
SET @v = cast(N'The Url which caused the Exception' as varchar(34))
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_SpottedException', N'COLUMN', N'Url'
GO
DECLARE @v sql_variant 
SET @v = cast(N'Path of the master container page' as varchar(33))
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_SpottedException', N'COLUMN', N'MasterPath'
GO
DECLARE @v sql_variant 
SET @v = cast(N'Page path' as varchar(9))
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_SpottedException', N'COLUMN', N'PagePath'
GO
DECLARE @v sql_variant 
SET @v = cast(N'Current page filter' as varchar(19))
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_SpottedException', N'COLUMN', N'CurrentFilter'
GO
DECLARE @v sql_variant 
SET @v = cast(N'K of object referenced in current filter' as varchar(40))
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_SpottedException', N'COLUMN', N'ObjectFilterK'
GO
DECLARE @v sql_variant 
SET @v = cast(N'Type of object referenced in current filter' as varchar(43))
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_SpottedException', N'COLUMN', N'ObjectFilterType'
GO
DECLARE @v sql_variant 
SET @v = cast(N'Machine name of the server on which this Exception was thrown' as varchar(61))
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_SpottedException', N'COLUMN', N'MachineName'
GO
DECLARE @v sql_variant 
SET @v = cast(N'K of current Usr' as varchar(16))
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_SpottedException', N'COLUMN', N'UsrK'
GO
DECLARE @v sql_variant 
SET @v = cast(N'Current browser guid' as varchar(20))
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_SpottedException', N'COLUMN', N'DsiGuid'
GO
DECLARE @v sql_variant 
SET @v = cast(N'Contents of browser cookies' as varchar(27))
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_SpottedException', N'COLUMN', N'Cookies'
GO
DECLARE @v sql_variant 
SET @v = cast(N'Post data of Request' as varchar(20))
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_SpottedException', N'COLUMN', N'PostData'
GO
SET IDENTITY_INSERT dbo.Tmp_SpottedException ON
GO
IF EXISTS(SELECT * FROM dbo.SpottedException)
	 EXEC('INSERT INTO dbo.Tmp_SpottedException (K, ParentK, ExceptionDateTime, ExceptionType, Message, Source, StackTrace, Url, MasterPath, PagePath, CurrentFilter, ObjectFilterK, ObjectFilterType, MachineName, UsrK, DsiGuid, Cookies, PostData)
		SELECT K, ParentK, ExceptionDateTime, ExceptionType, Message, Source, StackTrace, Url, MasterPath, PagePath, CurrentFilter, ObjectFilterK, ObjectFilterType, MachineName, UsrK, DsiGuid, CONVERT(varchar(MAX), Cookies), CONVERT(varchar(MAX), PostData) FROM dbo.SpottedException WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SpottedException OFF
GO
DROP TABLE dbo.SpottedException
GO
EXECUTE sp_rename N'dbo.Tmp_SpottedException', N'SpottedException', 'OBJECT' 
GO
ALTER TABLE dbo.SpottedException ADD CONSTRAINT
	PK_SpottedException PRIMARY KEY CLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
