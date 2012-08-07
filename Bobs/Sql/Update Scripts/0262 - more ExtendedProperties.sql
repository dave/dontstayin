
IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'DonatePageControl')and((tbl.name=N'DonationIcon' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Control', N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon', N'COLUMN', N'DonatePageControl'
END


IF EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'MS_Description')and((clmns.name=N'EventK')and((tbl.name=N'Flyer' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'EventK'
END


IF EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.extended_properties AS p ON major_id = tbl.object_id and minor_id = 0 
WHERE
(p.name=N'MS_Description')and((tbl.name=N'InvoiceItemRevenue' and SCHEMA_NAME(tbl.schema_id)=N'dbo')))
BEGIN
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'InvoiceItemRevenue'
END

IF EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.extended_properties AS p ON major_id = tbl.object_id and minor_id = 0 
WHERE
(p.name=N'MS_Description')and((tbl.name=N'InvoiceLink' and SCHEMA_NAME(tbl.schema_id)=N'dbo')))
BEGIN
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'InvoiceLink'
END


IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Type')and((tbl.name=N'OutgoingSms' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Types', N'SCHEMA', N'dbo', N'TABLE', N'OutgoingSms', N'COLUMN', N'Type'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'ChargeType')and((tbl.name=N'OutgoingSms' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'ChargeTypes', N'SCHEMA', N'dbo', N'TABLE', N'OutgoingSms', N'COLUMN', N'ChargeType'
END




IF EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'MS_Description')and((clmns.name=N'Donate1Expire')and((tbl.name=N'Usr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'Donate1Expire'
END

IF EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'MS_Description')and((clmns.name=N'Donate1Icon')and((tbl.name=N'Usr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'Donate1Icon'
END

IF EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'MS_Description')and((clmns.name=N'Donate2Expire')and((tbl.name=N'Usr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'Donate2Expire'
END

IF EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'MS_Description')and((clmns.name=N'Donate2Icon')and((tbl.name=N'Usr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'Donate2Icon'
END


IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'IsNotNull')and((clmns.name=N'DateTime')and((tbl.name=N'OutgoingSms'and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'IsNotNull', N'true', N'SCHEMA', N'dbo', N'TABLE', N'OutgoingSms', N'COLUMN', N'DateTime'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'IsNotNull')and((clmns.name=N'ErrorCode')and((tbl.name=N'OutgoingSms'and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'IsNotNull', N'true', N'SCHEMA', N'dbo', N'TABLE', N'OutgoingSms', N'COLUMN', N'ErrorCode'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'IsNotNull')and((clmns.name=N'Sent')and((tbl.name=N'OutgoingSms'and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'IsNotNull', N'true', N'SCHEMA', N'dbo', N'TABLE', N'OutgoingSms', N'COLUMN', N'Sent'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'IsNotNull')and((clmns.name=N'Delivered')and((tbl.name=N'OutgoingSms'and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'IsNotNull', N'true', N'SCHEMA', N'dbo', N'TABLE', N'OutgoingSms', N'COLUMN', N'Delivered'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'IsNotNull')and((clmns.name=N'MobileK')and((tbl.name=N'OutgoingSms'and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'IsNotNull', N'true', N'SCHEMA', N'dbo', N'TABLE', N'OutgoingSms', N'COLUMN', N'MobileK'
END


IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'IsNotNull')and((clmns.name=N'PhotoOfWeekUser')and((tbl.name=N'Photo'and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'IsNotNull', N'true', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'PhotoOfWeekUser'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'IsNotNull')and((clmns.name=N'BlockedFromPhotoOfWeekUser')and((tbl.name=N'Photo'and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'IsNotNull', N'true', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'BlockedFromPhotoOfWeekUser'
END


IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'ServiceType')and((tbl.name=N'OutgoingSms' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'IncomingSms.ServiceTypes', N'SCHEMA', N'dbo', N'TABLE', N'OutgoingSms', N'COLUMN', N'ServiceType'
END
