DECLARE @v sql_variant 

IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'DateTimeLastUpdateEmail'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Usr ADD
	DateTimeLastUpdateEmail datetime NOT NULL default '01/01/2000 00:00:00'
	

SET @v = N'Date/time of the tuesday that the last update email that was delivered to this user. Used to make the update email restartable'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'DateTimeLastUpdateEmail'

END