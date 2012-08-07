IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'TicketPromoterEvent' 
	AND	[column].name = 'ContactEmail'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.TicketPromoterEvent ADD
	ContactEmail varchar(100) NULL
	
DECLARE @v sql_variant 
SET @v = N'Contact email address for users to contact regarding ticket sales'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'TicketPromoterEvent', N'COLUMN', N'ContactEmail'

END
