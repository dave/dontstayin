IF NOT EXISTS(
	SELECT * FROM sys.columns c INNER JOIN sys.tables t ON c.object_id = t.object_id WHERE t.Name = 'Promoter' AND c.Name = 'IsAgency'
) BEGIN

	ALTER TABLE dbo.Promoter ADD
	IsAgency bit NOT NULL CONSTRAINT DF_Promoter_IsAgency DEFAULT 0

	
	DECLARE @v sql_variant 
	SET @v = N'if the promoter is an agency or not'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Promoter', N'COLUMN', N'IsAgency'
	
END

IF EXISTS(
	SELECT * FROM sys.columns c INNER JOIN sys.tables t ON c.object_id = t.object_id WHERE t.Name = 'InsertionOrderItem' AND c.Name = 'Rate'
) BEGIN
	ALTER TABLE dbo.InsertionOrderItem	DROP COLUMN Rate
	
	
	
	
 
	
END

	
GO
UPDATE Promoter SET IsAgency = 1 WHERE ClientSector = 13


GO


