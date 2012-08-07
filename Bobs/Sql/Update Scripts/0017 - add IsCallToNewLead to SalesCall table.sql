
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'SalesCall' 
	AND	[column].name = 'IsCallToNewLead'
) BEGIN
	ALTER TABLE dbo.SalesCall ADD
	IsCallToNewLead bit NULL

	DECLARE @v sql_variant 
	SET @v = N'Is this a call to a promoter that was recently added to the site by the sales user?'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'SalesCall', N'COLUMN', N'IsCallToNewLead'

END

 


