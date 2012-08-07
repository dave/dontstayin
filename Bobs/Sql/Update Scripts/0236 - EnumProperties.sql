
IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Status')and((tbl.name=N'Abuse' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'StatusEnum', N'SCHEMA', N'dbo', N'TABLE', N'Abuse', N'COLUMN', N'Status'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'ResolveStatus')and((tbl.name=N'Abuse' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'ResolveStatusEnum', N'SCHEMA', N'dbo', N'TABLE', N'Abuse', N'COLUMN', N'ResolveStatus'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'ObjectType')and((tbl.name=N'AdmiN'and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'AdminObjectType', N'SCHEMA', N'dbo', N'TABLE', N'Admin', N'COLUMN', N'ObjectType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Status')and((tbl.name=N'Article' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'StatusEnum', N'SCHEMA', N'dbo', N'TABLE', N'Article', N'COLUMN', N'Status'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Relevance')and((tbl.name=N'Article' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'RelevanceEnum', N'SCHEMA', N'dbo', N'TABLE', N'Article', N'COLUMN', N'Relevance'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Type')and((tbl.name=N'BankExport' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Types', N'SCHEMA', N'dbo', N'TABLE', N'BankExport', N'COLUMN', N'Type'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Status')and((tbl.name=N'BankExport' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Statuses', N'SCHEMA', N'dbo', N'TABLE', N'BankExport', N'COLUMN', N'Status'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'DisplayType')and((tbl.name=N'Banner' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'DisplayTypes', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'DisplayType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Position')and((tbl.name=N'Banner' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Positions', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'Position'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Status')and((tbl.name=N'Banner' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'StatusEnum', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'Status'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'LinkTarget')and((tbl.name=N'Banner' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'LinkTargets', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'LinkTarget'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'AutomaticExposureLevel')and((tbl.name=N'Banner' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'ExposureLevels', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'AutomaticExposureLevel'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Type')and((tbl.name=N'BinRange' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Types', N'SCHEMA', N'dbo', N'TABLE', N'BinRange', N'COLUMN', N'Type'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'PromoterStatus')and((tbl.name=N'Brand' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'PromoterStatusEnum', N'SCHEMA', N'dbo', N'TABLE', N'Brand', N'COLUMN', N'PromoterStatus'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'BuddyFoundByMethod')and((tbl.name=N'Buddy' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'BuddyFindingMethod', N'SCHEMA', N'dbo', N'TABLE', N'Buddy', N'COLUMN', N'BuddyFoundByMethod'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'DisplayType')and((tbl.name=N'Comp' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'DisplayTypes', N'SCHEMA', N'dbo', N'TABLE', N'Comp', N'COLUMN', N'DisplayType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Status')and((tbl.name=N'Comp' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'StatusEnum', N'SCHEMA', N'dbo', N'TABLE', N'Comp', N'COLUMN', N'Status'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'LinkType')and((tbl.name=N'Comp' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'LinkTypes', N'SCHEMA', N'dbo', N'TABLE', N'Comp', N'COLUMN', N'LinkType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Region ')and((tbl.name=N'Country' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'BillingRegionEnum', N'SCHEMA', N'dbo', N'TABLE', N'Country', N'COLUMN', N'Region '
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'PostageZone')and((tbl.name=N'Country' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'PostageZones', N'SCHEMA', N'dbo', N'TABLE', N'Country', N'COLUMN', N'PostageZone'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'StartTime')and((tbl.name=N'Event' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'StartTimes', N'SCHEMA', N'dbo', N'TABLE', N'Event', N'COLUMN', N'StartTime'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Restriction')and((tbl.name=N'Group' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'RestrictionEnum', N'SCHEMA', N'dbo', N'TABLE', N'Group', N'COLUMN', N'Restriction'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'CustomRestrictionType')and((tbl.name=N'Group' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'CustomRestrictionTypes', N'SCHEMA', N'dbo', N'TABLE', N'Group', N'COLUMN', N'CustomRestrictionType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Status')and((tbl.name=N'GroupUsr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'StatusEnum', N'SCHEMA', N'dbo', N'TABLE', N'GroupUsr', N'COLUMN', N'Status'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Type')and((tbl.name=N'Invoice' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Types', N'SCHEMA', N'dbo', N'TABLE', N'Invoice', N'COLUMN', N'Type'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'PaymentType')and((tbl.name=N'Invoice' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'PaymentTypes', N'SCHEMA', N'dbo', N'TABLE', N'Invoice', N'COLUMN', N'PaymentType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'VatCode')and((tbl.name=N'Invoice' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'VATCodes', N'SCHEMA', N'dbo', N'TABLE', N'Invoice', N'COLUMN', N'VatCode'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'BuyerType')and((tbl.name=N'Invoice' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'BuyerTypes', N'SCHEMA', N'dbo', N'TABLE', N'Invoice', N'COLUMN', N'BuyerType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Type')and((tbl.name=N'InvoiceItem' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Types', N'SCHEMA', N'dbo', N'TABLE', N'InvoiceItem', N'COLUMN', N'Type'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Item')and((tbl.name=N'Log' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Items', N'SCHEMA', N'dbo', N'TABLE', N'Log', N'COLUMN', N'Item'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'LogItem')and((tbl.name=N'PageTime' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'LogItems', N'SCHEMA', N'dbo', N'TABLE', N'PageTime', N'COLUMN', N'LogItem'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Type')and((tbl.name=N'Para' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'TypeEnum', N'SCHEMA', N'dbo', N'TABLE', N'Para', N'COLUMN', N'Type'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'PhotoAlign')and((tbl.name=N'Para' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'PhotoAlignEnum', N'SCHEMA', N'dbo', N'TABLE', N'Para', N'COLUMN', N'PhotoAlign'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'PhotoType')and((tbl.name=N'Para' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'PhotoTypes', N'SCHEMA', N'dbo', N'TABLE', N'Para', N'COLUMN', N'PhotoType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'MediaType')and((tbl.name=N'Photo' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'MediaTypes', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'MediaType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Overlay')and((tbl.name=N'Photo' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Overlays', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'Overlay'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'RatingType')and((tbl.name=N'PhotoReview' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'RatingTypes', N'SCHEMA', N'dbo', N'TABLE', N'PhotoReview', N'COLUMN', N'RatingType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Status')and((tbl.name=N'Promoter' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'StatusEnum', N'SCHEMA', N'dbo', N'TABLE', N'Promoter', N'COLUMN', N'Status'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'SalesStatus')and((tbl.name=N'Promoter' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'SalesStatusEnum', N'SCHEMA', N'dbo', N'TABLE', N'Promoter', N'COLUMN', N'SalesStatus'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'LetterType')and((tbl.name=N'Promoter' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'LetterTypes', N'SCHEMA', N'dbo', N'TABLE', N'Promoter', N'COLUMN', N'LetterType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'LetterStatus')and((tbl.name=N'Promoter' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'LetterStatusEnum', N'SCHEMA', N'dbo', N'TABLE', N'Promoter', N'COLUMN', N'LetterStatus'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'OfferType')and((tbl.name=N'Promoter' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'OfferTypes', N'SCHEMA', N'dbo', N'TABLE', N'Promoter', N'COLUMN', N'OfferType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'SalesEstimate')and((tbl.name=N'Promoter' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'SalesEstimateEnum', N'SCHEMA', N'dbo', N'TABLE', N'Promoter', N'COLUMN', N'SalesEstimate'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'ClientSector')and((tbl.name=N'Promoter' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'ClientSectorEnum', N'SCHEMA', N'dbo', N'TABLE', N'Promoter', N'COLUMN', N'ClientSector'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'AddedMethod')and((tbl.name=N'Promoter' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'AddedMedhods', N'SCHEMA', N'dbo', N'TABLE', N'Promoter', N'COLUMN', N'AddedMethod'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'VatStatus')and((tbl.name=N'Promoter' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'VatStatusEnum', N'SCHEMA', N'dbo', N'TABLE', N'Promoter', N'COLUMN', N'VatStatus'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Direction')and((tbl.name=N'SalesCall' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Directions', N'SCHEMA', N'dbo', N'TABLE', N'SalesCall', N'COLUMN', N'Direction'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Type')and((tbl.name=N'SalesCall' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Types', N'SCHEMA', N'dbo', N'TABLE', N'SalesCall', N'COLUMN', N'Type'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Type')and((tbl.name=N'SalesStatusChange' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Types', N'SCHEMA', N'dbo', N'TABLE', N'SalesStatusChange', N'COLUMN', N'Type'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Action')and((tbl.name=N'TagPhotoHistory' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'TagPhotoHistoryAction', N'SCHEMA', N'dbo', N'TABLE', N'TagPhotoHistory', N'COLUMN', N'Action'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'NewsStatus')and((tbl.name=N'Thread' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'NewsStatusEnum', N'SCHEMA', N'dbo', N'TABLE', N'Thread', N'COLUMN', N'NewsStatus'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Status')and((tbl.name=N'ThreadUsr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'StatusEnum', N'SCHEMA', N'dbo', N'TABLE', N'ThreadUsr', N'COLUMN', N'Status'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'PrivateChatType')and((tbl.name=N'ThreadUsr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'PrivateChatTypes', N'SCHEMA', N'dbo', N'TABLE', N'ThreadUsr', N'COLUMN', N'PrivateChatType'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Feedback')and((tbl.name=N'Ticket' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'FeedbackEnum', N'SCHEMA', N'dbo', N'TABLE', N'Ticket', N'COLUMN', N'Feedback'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'DeliveryMethod')and((tbl.name=N'TicketRuN'and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'DeliveryMethodType', N'SCHEMA', N'dbo', N'TABLE', N'TicketRun', N'COLUMN', N'DeliveryMethod'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Type')and((tbl.name=N'Transfer' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'TransferTypes', N'SCHEMA', N'dbo', N'TABLE', N'Transfer', N'COLUMN', N'Type'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Status')and((tbl.name=N'Transfer' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'StatusEnum', N'SCHEMA', N'dbo', N'TABLE', N'Transfer', N'COLUMN', N'Status'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Method')and((tbl.name=N'Transfer' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'Methods', N'SCHEMA', N'dbo', N'TABLE', N'Transfer', N'COLUMN', N'Method'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'RefundStatus')and((tbl.name=N'Transfer' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'RefundStatuses', N'SCHEMA', N'dbo', N'TABLE', N'Transfer', N'COLUMN', N'RefundStatus'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'DSIBankAccount')and((tbl.name=N'Transfer' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'DSIBankAccounts', N'SCHEMA', N'dbo', N'TABLE', N'Transfer', N'COLUMN', N'DSIBankAccount'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'AdminLevel')and((tbl.name=N'Usr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'AdminLevels', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'AdminLevel'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'CardStatus')and((tbl.name=N'Usr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'CardStatusEnum', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'CardStatus'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Status')and((tbl.name=N'UsrDate' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'StatusEnum', N'SCHEMA', N'dbo', N'TABLE', N'UsrDate', N'COLUMN', N'Status'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'Match')and((tbl.name=N'UsrDate' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'MatchEnum', N'SCHEMA', N'dbo', N'TABLE', N'UsrDate', N'COLUMN', N'Match'
END

IF NOT EXISTS(SELECT * FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
WHERE
(p.name=N'EnumProperty')and((clmns.name=N'PromoterStatus')and((tbl.name=N'Venue' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))
BEGIN
	EXECUTE sp_AddExtendedProperty N'EnumProperty', N'PromoterStatusEnum', N'SCHEMA', N'dbo', N'TABLE', N'Venue', N'COLUMN', N'PromoterStatus'
END

