IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'InvoiceItem' 
	AND	[column].name = 'Discount'
) BEGIN

ALTER TABLE dbo.InvoiceItem ADD PriceBeforeDiscount float NULL
ALTER TABLE dbo.InvoiceItem ADD Discount float NULL

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Price before applying discount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InvoiceItem', @level2type=N'COLUMN',@level2name=N'PriceBeforeDiscount'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Discount to apply to this item, between 0.0 and 1.0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InvoiceItem', @level2type=N'COLUMN',@level2name=N'Discount'

END

go

update InvoiceItem set PriceBeforeDiscount = Price / (1.0 - Discount) 
