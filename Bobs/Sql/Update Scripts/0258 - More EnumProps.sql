
IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'ObjectType')and((tbl.name=N'Abuse' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Model.Entities.ObjectType', N'SCHEMA', N'dbo', N'TABLE', N'Abuse', N'COLUMN', N'ObjectType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'ParentObjectType')and((tbl.name=N'Article' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Model.Entities.ObjectType', N'SCHEMA', N'dbo', N'TABLE', N'Article', N'COLUMN', N'ParentObjectType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'BuyableObjectType')and((tbl.name=N'CampaignCredit' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Model.Entities.ObjectType', N'SCHEMA', N'dbo', N'TABLE', N'CampaignCredit', N'COLUMN', N'BuyableObjectType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'ParentObjectType')and((tbl.name=N'CommentAlert' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Model.Entities.ObjectType', N'SCHEMA', N'dbo', N'TABLE', N'CommentAlert', N'COLUMN', N'ParentObjectType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'RedirectObjectType')and((tbl.name=N'DomaiN'and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Model.Entities.ObjectType', N'SCHEMA', N'dbo', N'TABLE', N'Domain', N'COLUMN', N'RedirectObjectType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'BuyableObjectType')and((tbl.name=N'InvoiceItem' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Model.Entities.ObjectType', N'SCHEMA', N'dbo', N'TABLE', N'InvoiceItem', N'COLUMN', N'BuyableObjectType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'ObjectFilterType')and((tbl.name=N'LogPageTime' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Model.Entities.ObjectType', N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'ObjectFilterType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'ObjectFilterType')and((tbl.name=N'SpottedExceptioN'and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Model.Entities.ObjectType', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'ObjectFilterType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'ParentObjectType')and((tbl.name=N'Thread' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Model.Entities.ObjectType', N'SCHEMA', N'dbo', N'TABLE', N'Thread', N'COLUMN', N'ParentObjectType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'StatusChangeObjectType')and((tbl.name=N'ThreadUsr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Model.Entities.ObjectType', N'SCHEMA', N'dbo', N'TABLE', N'ThreadUsr', N'COLUMN', N'StatusChangeObjectType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'DesignType')and((tbl.name=N'Banner' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'DesignTypes', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'DesignType'
END

IF EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'IsNotNull')and((clmns.name=N'ThreadK')and((tbl.name=N'Article' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_DropExtendedProperty N'IsNotNull', N'SCHEMA', N'dbo', N'TABLE', N'Article', N'COLUMN', N'ThreadK'
END

IF EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'IsNotNull')and((clmns.name=N'ThreadK')and((tbl.name=N'Photo' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_DropExtendedProperty N'IsNotNull', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'ThreadK'
END


IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'InvoiceItemType')and((tbl.name=N'CampaignCredit' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'InvoiceItem.Types', N'SCHEMA', N'dbo', N'TABLE', N'CampaignCredit', N'COLUMN', N'InvoiceItemType'
END


IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Status')and((tbl.name=N'Photo' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'StatusEnum', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'Status'
END


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

