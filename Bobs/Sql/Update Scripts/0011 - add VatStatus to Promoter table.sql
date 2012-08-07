IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Promoter' 
	AND	[column].name = 'VatStatus'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Promoter ADD
	VatStatus int NULL
	
DECLARE @v sql_variant 
SET @v = N'Enum for Promoter''s VAT status: 0 = unknown, 1 = not registered, 2 = registered'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Promoter', N'COLUMN', N'VatStatus'

ALTER TABLE dbo.Promoter ADD CONSTRAINT
	DF_Promoter_VatStatus DEFAULT ((0)) FOR VatStatus
END
