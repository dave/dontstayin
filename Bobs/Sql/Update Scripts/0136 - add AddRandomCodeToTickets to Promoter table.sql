IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Promoter' 
	AND	[column].name = 'AddRandomCodeToTickets'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Promoter ADD
	AddRandomCodeToTickets bit NULL
	
DECLARE @v sql_variant 
SET @v = N'Add a random code to tickets, to be displayed on doorlists'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Promoter', N'COLUMN', N'AddRandomCodeToTickets'

END




--rewert


--dfsd
