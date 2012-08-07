/* example column length check */
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Transfer' 
	AND	[column].name = 'CardDigits'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Transfer ADD
	CardDigits int NULL
	
DECLARE @v sql_variant 
SET @v = N'Last 6 digits of the card number'
EXECUTE sp_updateextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Transfer', N'COLUMN', N'CardNumberEnd'

SET @v = N'Number of digits in the card number'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Transfer', N'COLUMN', N'CardDigits'

END

