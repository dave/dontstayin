IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Banner' 
	AND	[column].name = 'PriceCreditsStored'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Banner ADD
	PriceCreditsStored int NULL
	
DECLARE @v sql_variant 
SET @v = N'Price in credits (either for fixed price banners, or for after banner is booked)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'PriceCreditsStored'

END
