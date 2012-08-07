IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Promoter' 
	AND	[column].name = 'Discount'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Promoter ADD
	Discount int NULL
	
DECLARE @v sql_variant 
SET @v = N'Discount percentage as an integer'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Promoter', N'COLUMN', N'Discount'

END

GO

Update Promoter set Discount =  ROUND((1.0 - PricingMultiplier) * 100.0, 1)
