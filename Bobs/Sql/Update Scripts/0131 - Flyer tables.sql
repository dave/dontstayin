IF EXISTS(
	SELECT * FROM sys.tables WHERE Name = 'Flyer' 
) BEGIN
	drop table dbo.Flyer
END

GO

create TABLE dbo.Flyer
(
	K int identity(1,1) not null,
	PromoterK int not null,
	Name varchar(100) not null,
	Subject varchar(150),
	BackgroundColor varchar(6),
	MiscK int not null,
	SendDateTime datetime not null,
	LinkTargetUrl varchar(250),
	PlaceKs varchar(400),
	MusicTypeKs varchar(400),
	Sends int,
	Views int,
	Clicks int,
	Unsubscribes int
)
ALTER TABLE dbo.Flyer ADD CONSTRAINT
	PK_Flyer PRIMARY KEY CLUSTERED 
	(
		K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

EXECUTE sp_addextendedproperty N'MS_Description', N'eFlyers sent out by Promoters', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', NULL, NULL

EXECUTE sp_addextendedproperty N'MS_Description', N'K of the Flyer', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'K'
EXECUTE sp_addextendedproperty N'MS_Description', N'Promoter', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'PromoterK'
EXECUTE sp_addextendedproperty N'MS_Description', N'Name to identify this flyer run', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'Name'
EXECUTE sp_addextendedproperty N'MS_Description', N'Subject for the email', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'Subject'
EXECUTE sp_addextendedproperty N'MS_Description', N'Background colour of the email to make it blend with flyer. Use hex, e.g. #D2D2D2, but without the #', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'BackgroundColor'
EXECUTE sp_addextendedproperty N'MS_Description', N'Flyer image K entry in Misc table', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'MiscK'
EXECUTE sp_addextendedproperty N'MS_Description', N'DateTime when to send the eFlyer', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'SendDateTime'
EXECUTE sp_addextendedproperty N'MS_Description', N'Url to redirect to when clicking on the Flyer image', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'LinkTargetUrl'
EXECUTE sp_addextendedproperty N'MS_Description', N'Comma-delimited list of PlaceKs to which this Flyer is targetted', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'PlaceKs'
EXECUTE sp_addextendedproperty N'MS_Description', N'Comma-delimited list of MusicTypeKs to which this Flyer is targetted', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'MusicTypeKs'
EXECUTE sp_addextendedproperty N'MS_Description', N'Total eFlyers we have sent', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'Sends'
EXECUTE sp_addextendedproperty N'MS_Description', N'Total times image has been viewed (downloaded) (or displayed in popup if the user clicks "I can''t see the flyer" in the email)', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'Views'
EXECUTE sp_addextendedproperty N'MS_Description', N'Total clicks on the email image (or the popup)', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'Clicks'
EXECUTE sp_addextendedproperty N'MS_Description', N'Total people who unsubscribed because of this flyer', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'Unsubscribes'

GO

