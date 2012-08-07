IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'TicketPromoterEvent' 
	AND	[column].name = 'FundsLockTotalFundsDontMatch'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.TicketPromoterEvent ADD
	FundsLockTotalFundsDontMatch bit NULL
	
DECLARE @v sql_variant 
SET @v = N'Lock when the total funds dont match the ticket run funds'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'TicketPromoterEvent', N'COLUMN', N'FundsLockTotalFundsDontMatch'

END
