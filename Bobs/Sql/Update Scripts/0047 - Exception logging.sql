/*
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SpottedException]') AND type in (N'U'))
begin
	drop table SpottedException
end

go

create table SpottedException
(
	K int NOT NULL IDENTITY(1,1),
	ParentK int,
	ExceptionDateTime datetime NOT NULL,
	ExceptionType varchar(50),
	Message varchar(4000),
	Source varchar(50),
	StackTrace varchar(4000),
	Url varchar(150),
	MasterPath varchar(50),
	PagePath varchar(50),
	CurrentFilter varchar(150),
	ObjectFilterK int,
	ObjectFilterType int,
	MachineName varchar(50),
	UsrK int,
	DsiGuid uniqueidentifier,
	Cookies varchar(400),
	PostData varchar(4000)
)

ALTER TABLE dbo.SpottedException ADD CONSTRAINT
PK_SpottedException PRIMARY KEY CLUSTERED 
(
	K
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Log of Exceptions thrown from the Spotted website' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SpottedException'

EXECUTE sp_addextendedproperty N'MS_Description', 'K of the Exception', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'K'
EXECUTE sp_addextendedproperty N'MS_Description', 'K of the parent Exception, when this is an InnerException', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'ParentK'
EXECUTE sp_addextendedproperty N'MS_Description', 'Time of logging Exception', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'ExceptionDateTime'
EXECUTE sp_addextendedproperty N'MS_Description', 'The type of Exception', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'ExceptionType'
EXECUTE sp_addextendedproperty N'MS_Description', 'Exception message', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'Message'
EXECUTE sp_addextendedproperty N'MS_Description', 'Exception source', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'Source'
EXECUTE sp_addextendedproperty N'MS_Description', 'Exception stack trace', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'StackTrace'
EXECUTE sp_addextendedproperty N'MS_Description', 'The Url which caused the Exception', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'Url'
EXECUTE sp_addextendedproperty N'MS_Description', 'Path of the master container page', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'MasterPath'
EXECUTE sp_addextendedproperty N'MS_Description', 'Page path', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'PagePath'
EXECUTE sp_addextendedproperty N'MS_Description', 'Current page filter', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'CurrentFilter'
EXECUTE sp_addextendedproperty N'MS_Description', 'K of object referenced in current filter', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'ObjectFilterK'
EXECUTE sp_addextendedproperty N'MS_Description', 'Type of object referenced in current filter', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'ObjectFilterType'
EXECUTE sp_addextendedproperty N'MS_Description', 'Machine name of the server on which this Exception was thrown', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'MachineName'
EXECUTE sp_addextendedproperty N'MS_Description', 'K of current Usr', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'UsrK'
EXECUTE sp_addextendedproperty N'MS_Description', 'Current browser guid', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'DsiGuid'
EXECUTE sp_addextendedproperty N'MS_Description', 'Contents of browser cookies', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'Cookies'
EXECUTE sp_addextendedproperty N'MS_Description', 'Post data of Request', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'PostData'

*/




IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SpottedException]') AND type in (N'U'))

BEGIN
	CREATE TABLE dbo.SpottedException
	(
		K int NOT NULL IDENTITY(1,1),
		ParentK int,
		ExceptionDateTime datetime NOT NULL,
		ExceptionType varchar(50),
		Message varchar(4000),
		Source varchar(50),
		StackTrace varchar(4000),
		Url varchar(150),
		MasterPath varchar(50),
		PagePath varchar(50),
		CurrentFilter varchar(150),
		ObjectFilterK int,
		ObjectFilterType int,
		MachineName varchar(50),
		UsrK int,
		DsiGuid uniqueidentifier,
		Cookies varchar(400),
		PostData varchar(4000)
	)  ON [PRIMARY]
	
ALTER TABLE dbo.SpottedException ADD CONSTRAINT
PK_SpottedException PRIMARY KEY CLUSTERED 
(
K
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

EXECUTE sp_addextendedproperty N'MS_Description', 'Log of Exceptions thrown from the Spotted website', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', NULL, NULL	
EXECUTE sp_addextendedproperty N'MS_Description', 'K of the Exception', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'K'
EXECUTE sp_addextendedproperty N'MS_Description', 'K of the parent Exception, when this is an InnerException', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'ParentK'
EXECUTE sp_addextendedproperty N'MS_Description', 'Time of logging Exception', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'ExceptionDateTime'
EXECUTE sp_addextendedproperty N'MS_Description', 'The type of Exception', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'ExceptionType'
EXECUTE sp_addextendedproperty N'MS_Description', 'Exception message', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'Message'
EXECUTE sp_addextendedproperty N'MS_Description', 'Exception source', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'Source'
EXECUTE sp_addextendedproperty N'MS_Description', 'Exception stack trace', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'StackTrace'
EXECUTE sp_addextendedproperty N'MS_Description', 'The Url which caused the Exception', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'Url'
EXECUTE sp_addextendedproperty N'MS_Description', 'Path of the master container page', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'MasterPath'
EXECUTE sp_addextendedproperty N'MS_Description', 'Page path', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'PagePath'
EXECUTE sp_addextendedproperty N'MS_Description', 'Current page filter', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'CurrentFilter'
EXECUTE sp_addextendedproperty N'MS_Description', 'K of object referenced in current filter', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'ObjectFilterK'
EXECUTE sp_addextendedproperty N'MS_Description', 'Type of object referenced in current filter', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'ObjectFilterType'
EXECUTE sp_addextendedproperty N'MS_Description', 'Machine name of the server on which this Exception was thrown', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'MachineName'
EXECUTE sp_addextendedproperty N'MS_Description', 'K of current Usr', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'UsrK'
EXECUTE sp_addextendedproperty N'MS_Description', 'Current browser guid', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'DsiGuid'
EXECUTE sp_addextendedproperty N'MS_Description', 'Contents of browser cookies', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'Cookies'
EXECUTE sp_addextendedproperty N'MS_Description', 'Post data of Request', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'PostData'


END


GO
