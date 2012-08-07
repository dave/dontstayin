
IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Status')and((tbl.name=N'InsertionOrder' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'InsertionOrderStatus', N'SCHEMA', N'dbo', N'TABLE', N'InsertionOrder', N'COLUMN', N'Status'
END


IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'VatCode')and((tbl.name=N'InvoiceItem' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'VATCodes', N'SCHEMA', N'dbo', N'TABLE', N'InvoiceItem', N'COLUMN', N'VatCode'
END


IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'CardType')and((tbl.name=N'Transfer' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'BinRange.Types', N'SCHEMA', N'dbo', N'TABLE', N'Transfer', N'COLUMN', N'CardType'
END

