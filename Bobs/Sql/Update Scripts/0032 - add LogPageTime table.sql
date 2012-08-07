IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LogPageTime]') AND type in (N'U'))

BEGIN
	CREATE TABLE dbo.LogPageTime
	(
		K int NOT NULL IDENTITY (1, 1),
		StartDateTime datetime NOT NULL,
		EndDateTime datetime NOT NULL,
		CurrentFilter varchar(150) NULL,
		MasterPath varchar(50) NULL,
		PagePath varchar(50) NULL,
		ObjectFilterK int NULL,
		ObjectFilterType int NULL,
		MachineName varchar(50) NULL,
		UsrK int NULL
	)  ON [PRIMARY]
	
ALTER TABLE dbo.LogPageTime ADD CONSTRAINT
PK_LogPageTime PRIMARY KEY CLUSTERED 
(
K
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

DECLARE @v0 sql_variant 
SET @v0 = N'Primary K'
EXECUTE sp_addextendedproperty N'MS_Description', @v0, N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'K'

DECLARE @v1 sql_variant 
SET @v1 = N'Start time of page load'
EXECUTE sp_addextendedproperty N'MS_Description', @v1, N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'StartDateTime'

DECLARE @v2 sql_variant 
SET @v2 = N'End time of page load'
EXECUTE sp_addextendedproperty N'MS_Description', @v2, N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'EndDateTime'

DECLARE @v3 sql_variant 
SET @v3 = N'Current page filter'
EXECUTE sp_addextendedproperty N'MS_Description', @v3, N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'CurrentFilter'

DECLARE @v4 sql_variant 
SET @v4 = N'Path of the master container page'
EXECUTE sp_addextendedproperty N'MS_Description', @v4, N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'MasterPath'

DECLARE @v5 sql_variant 
SET @v5 = N'Page path'
EXECUTE sp_addextendedproperty N'MS_Description', @v5, N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'PagePath'

DECLARE @v6 sql_variant 
SET @v6 = N'K of object referenced in the current filter'
EXECUTE sp_addextendedproperty N'MS_Description', @v6, N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'ObjectFilterK'

DECLARE @v7 sql_variant 
SET @v7 = N'K of current user'
EXECUTE sp_addextendedproperty N'MS_Description', @v7, N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'UsrK'

DECLARE @v8 sql_variant 
SET @v8 = N'Type of object referenced in the current filter'
EXECUTE sp_addextendedproperty N'MS_Description', @v8, N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'ObjectFilterType'

DECLARE @v9 sql_variant 
SET @v9 = N'Log of each page load time and page reference'
EXECUTE sp_addextendedproperty N'MS_Description', @v9, N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', NULL, NULL	
	
DECLARE @v10 sql_variant 
SET @v10 = N'Web server name'
EXECUTE sp_addextendedproperty N'MS_Description', @v10, N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'MachineName'

END


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'LogPageTime' 
	AND	[column].name = 'Selects'
) BEGIN
	ALTER TABLE dbo.LogPageTime ADD Selects int
	ALTER TABLE dbo.LogPageTime ADD Updates int
	ALTER TABLE dbo.LogPageTime ADD Inserts int
	ALTER TABLE dbo.LogPageTime ADD Deletes int

	DECLARE @v sql_variant 
	SET @v = N'Total Database select queries used to generate page'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'Selects'
	SET @v = N'Total Database update queries used to generate page'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'Updates'
	SET @v = N'Total Database insert queries used to generate page'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'Inserts'
	SET @v = N'Total Database delete queries used to generate page'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'Deletes'

END

GO
