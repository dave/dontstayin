IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Prefs]') AND type in (N'U'))

BEGIN
	CREATE TABLE dbo.Prefs
	(
		Guid uniqueidentifier NOT NULL,
		PrefsString text NOT NULL
	)  ON [PRIMARY]
	
ALTER TABLE dbo.Prefs ADD CONSTRAINT
PK_Prefs PRIMARY KEY CLUSTERED 
(
Guid
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

DECLARE @v0 sql_variant 
SET @v0 = N'Prefs settings for browser guid'
EXECUTE sp_addextendedproperty N'MS_Description', @v0, N'SCHEMA', N'dbo', N'TABLE', N'Prefs', NULL, NULL	
	
DECLARE @v1 sql_variant 
SET @v1 = N'Browser guid'
EXECUTE sp_addextendedproperty N'MS_Description', @v1, N'SCHEMA', N'dbo', N'TABLE', N'Prefs', N'COLUMN', N'Guid'

DECLARE @v2 sql_variant 
SET @v2 = N'Prefs string'
EXECUTE sp_addextendedproperty N'MS_Description', @v2, N'SCHEMA', N'dbo', N'TABLE', N'Prefs', N'COLUMN', N'PrefsString'


END


GO
