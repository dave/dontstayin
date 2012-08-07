IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'BuyableObjectType')and((tbl.name=N'InvoiceItem' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Model.Entities.ObjectType', N'SCHEMA', N'dbo', N'TABLE', N'InvoiceItem', N'COLUMN', N'BuyableObjectType'
END




