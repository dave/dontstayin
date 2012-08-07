IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'DonationIcon' 
	AND	[column].name = 'ImgGuid'
) BEGIN

alter table DonationIcon add
	ImgGuid uniqueidentifier,
	ImgExtension varchar(4)

EXECUTE sp_AddExtendedProperty N'MS_Description', N'Storage GUID for icon, if applicable', N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon', N'COLUMN', N'ImgGuid'
EXECUTE sp_AddExtendedProperty N'MS_Description', N'Image extension for icon, if applicable', N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon', N'COLUMN', N'ImgExtension'

END
