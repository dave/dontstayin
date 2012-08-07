
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Domain' 
	AND	[column].name = 'RegisteredPrimary'
) BEGIN
	ALTER TABLE dbo.Domain ADD RegisteredPrimary bit NULL
	ALTER TABLE dbo.Domain ADD RegisteredSecondary bit NULL

	DECLARE @v sql_variant 
	SET @v = N'Has this domain been registered in the Primary zone? (Extra)'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Domain', N'COLUMN', N'RegisteredPrimary'

	SET @v = N'Has this domain been registered in the Secondary zone? (Mace)'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Domain', N'COLUMN', N'RegisteredSecondary'

END

go


update Domain set RegisteredPrimary = 1, RegisteredSecondary = 1

go


