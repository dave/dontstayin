IF EXISTS(
	SELECT * FROM sys.tables WHERE Name = 'MixmagGreatestDj' 
) BEGIN
	drop table dbo.MixmagGreatestDj
END

GO

create TABLE dbo.MixmagGreatestDj
(
	K int NOT NULL IDENTITY (1, 1),
	UrlName varchar(255) null,
	Name varchar(255) null,
	ImageUrl varchar(255) null,
	YoutubeId varchar(255) null,
	Description text null
)

ALTER TABLE dbo.MixmagGreatestDj ADD CONSTRAINT
	[PK_MixmagGreatestDj] PRIMARY KEY CLUSTERED 
	(
		[K]
	) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

EXECUTE sp_addextendedproperty N'MS_Description', N'DJs in the Greatest DJ poll', N'SCHEMA', N'dbo', N'TABLE', N'MixmagGreatestDj', NULL, NULL

EXECUTE sp_addextendedproperty N'MS_Description', N'Key', N'SCHEMA', N'dbo', N'TABLE', N'MixmagGreatestDj', N'COLUMN', N'K'
EXECUTE sp_addextendedproperty N'MS_Description', N'Name in the url', N'SCHEMA', N'dbo', N'TABLE', N'MixmagGreatestDj', N'COLUMN', N'UrlName'
EXECUTE sp_addextendedproperty N'MS_Description', N'Name of the DJ', N'SCHEMA', N'dbo', N'TABLE', N'MixmagGreatestDj', N'COLUMN', N'Name'
EXECUTE sp_addextendedproperty N'MS_Description', N'Url of a small image', N'SCHEMA', N'dbo', N'TABLE', N'MixmagGreatestDj', N'COLUMN', N'ImageUrl'
EXECUTE sp_addextendedproperty N'MS_Description', N'Youtube id of the video', N'SCHEMA', N'dbo', N'TABLE', N'MixmagGreatestDj', N'COLUMN', N'YoutubeId'
EXECUTE sp_addextendedproperty N'MS_Description', N'Description', N'SCHEMA', N'dbo', N'TABLE', N'MixmagGreatestDj', N'COLUMN', N'Description'

GO

