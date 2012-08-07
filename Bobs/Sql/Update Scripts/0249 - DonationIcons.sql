IF NOT EXISTS(
	SELECT * FROM sys.tables [table] WHERE [table].name = 'DonationIcon'
) BEGIN

CREATE TABLE dbo.DonationIcon
	(
	K int NOT NULL IDENTITY (1, 1),
	IconName varchar(50) NOT NULL,
	IconText varchar(50) NOT NULL,
	ImgUrl varchar(50) NOT NULL,
	ThreadK int NOT NULL,
	StartDateTime datetime NOT NULL,
	PriceWhenActive decimal(18,2) NOT NULL,
	PriceWhenRetroactive decimal(18,2) NOT NULL,
	DonatePageControl int NOT NULL,
	DonatePageHeader varchar(MAX) NULL,
	DonatePageCenterText varchar(MAX) NULL,
	DonatePageLine1Text varchar(MAX) NULL,
	DonatePageLine2Text varchar(MAX) NULL
	)  ON [PRIMARY]

ALTER TABLE dbo.DonationIcon ADD CONSTRAINT
	PK_DonationIcon PRIMARY KEY CLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

DECLARE @v sql_variant 
SET @v = N'K'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon', N'COLUMN', N'K'
SET @v = N'Name displayed on donation pages etc.'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon', N'COLUMN', N'IconName'
SET @v = N'Text displayed by Icon on Rollovers, etc.'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon', N'COLUMN', N'IconText'
SET @v = N'image path'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon', N'COLUMN', N'ImgUrl'
SET @v = N'Chat thread to discuss how much one loves one''s donation icon'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon', N'COLUMN', N'ThreadK'
SET @v = N'Date and time when this icon becomes the Active icon'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon', N'COLUMN', N'StartDateTime'
SET @v = N'Price if bought while icon is active'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon', N'COLUMN', N'PriceWhenActive'
SET @v = N'Price if bought once icon is retroactive'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon', N'COLUMN', N'PriceWhenRetroactive'
SET @v = N'Donate page - which control layout to use'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon', N'COLUMN', N'DonatePageControl'
SET @v = N'Donate page Header'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon', N'COLUMN', N'DonatePageHeader'
SET @v = N'Donate page center text'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon', N'COLUMN', N'DonatePageCenterText'
SET @v = N'Donate page line 1 text'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon', N'COLUMN', N'DonatePageLine1Text'
SET @v = N'Donate page line 2 text'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon', N'COLUMN', N'DonatePageLine2Text'

EXECUTE sp_addextendedproperty N'MS_Description', N'Donation icons', N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon'

INSERT INTO DonationIcon VALUES ('donation dog', 'WOOF!',	'/gfx/don/dog.gif',	339052,	'Dec 15 2005  5:33PM',	5,5,0,	'',	'',	'',	'')
INSERT INTO DonationIcon VALUES ('donation Dave', 'DAVE!',	'/gfx/don/dave.gif',	418589,	'Feb  2 2006 11:44AM',	5,5,0,	'',	'',	'',	'')
INSERT INTO DonationIcon VALUES ('donation duck', 'QUACK!',	'/gfx/don/duck.gif',	552759,	'Mar 29 2006  2:32PM',	5,5,0,	'',	'',	'',	'')
INSERT INTO DonationIcon VALUES ('donation decks', 'SCRATCH!',	'/gfx/don/deck.gif',	640480,	'May  1 2006 11:24AM',	5,5,0,	'',	'',	'',	'')
INSERT INTO DonationIcon VALUES ('donation jump',	'JUMP!',	'/gfx/don/jump.gif',	1125271,	'Oct 30 2006 11:41AM',	5,5,0,	'',	'',	'',	'')
INSERT INTO DonationIcon VALUES ('profile penguin', 'PENGUIN!',	'/gfx/don/penguin.gif',	1815643,	'Jun 11 2007 12:24PM',	5,5,0,	'PENGUINS!',	'PENGUINS!',	'Say hello to the new and improved PROFILE PENGUIN!',	'He will sit on your profile and ward off evil profile polar bears for a whole month')
INSERT INTO DonationIcon VALUES ('gay profile pumpkin', 'PETER!',	'/gfx/don/peter.gif',	2228386,	'Oct 23 2007 11:45AM',	5,5,0,	'Happy Halloween DSIers!',	'Happy Halloween DSIers!',	'Celebrate Halloween with us with our new friend, Peter the gay profile pumpkin! He''s here to ward off the pukey profile poltergiests and wicked web witches, and for no reason at all, he''s a homosexual!',	'')
INSERT INTO DonationIcon VALUES ('polar bear', 'POLAR!',	'/gfx/don/polar.gif',	2374956,	'Jan  7 2008  9:28AM',	5,5,0,	'WOO HOO!',	'',	'They are constantly on the look out for the evilest of evils....  THE PROFILE PIRATES!',	'Save your profile today and buy a profile polar bear!')
INSERT INTO DonationIcon VALUES ('profile pirate', 'PIRATE!!',	'/gfx/don/pirate.gif',	2521598,	'Mar 18 2008 10:28AM',	5,5,0,	'AVAST YEE LAND LUBBERS',	'AHOY THERE ME HEARTIES!<br />PIRATES BE TAKIN'' OVER YEE WEBSITE!',	'Get yee''self a profile pirate by donating 5 gold nuggets to the boys at DSI or they will make yee walk the plank!!',	'ARRRRRGHHH SHIVER ME TIMBERS')
INSERT INTO DonationIcon VALUES ('smiley', 'SMILEY!',	'/gfx/don/acid.gif',	2609448,	'May  1 2008  4:40PM',	5,5,0,	'20 Years of ACID HOUSE!',	'Be happy and smiley and help us celebreate 20 Years of ACID house!',	'Get your self a smiley on your profile and help support DSI.',	'ACCCIIIIIIIIIIIEEEEEEEEEEEEDDDD')
INSERT INTO DonationIcon VALUES ('MONKEY!', 'MONKEY!',	'/gfx/don/monkey.gif',	2805868,	'Sep  5 2008  4:44PM',	5,5,2,	'',	'',	'',	'')

END




GO



IF NOT EXISTS(
	SELECT * FROM sys.tables [table] WHERE [table].name = 'UsrDonationIcon'
) BEGIN

CREATE TABLE dbo.UsrDonationIcon
	(
	K int NOT NULL IDENTITY (1, 1),
	UsrK int NOT NULL,
	DonationIconK int NOT NULL,
	BuyDateTime DateTime NOT NULL,
	Enabled bit NOT NULL
	)  ON [PRIMARY]

ALTER TABLE dbo.UsrDonationIcon ADD CONSTRAINT
	PK_UsrDonationIcon PRIMARY KEY CLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

DECLARE @v sql_variant 
SET @v = N'K'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'UsrDonationIcon', N'COLUMN', N'K'
SET @v = N'UsrK who bought icon'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'UsrDonationIcon', N'COLUMN', N'UsrK'
SET @v = N'DonationIcon bought'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'UsrDonationIcon', N'COLUMN', N'DonationIconK'
SET @v = N'DateTime bought'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'UsrDonationIcon', N'COLUMN', N'BuyDateTime'
-- it is stupid that we need this. Must fix the generator. But later.
EXECUTE sp_addextendedproperty N'IsNotNull', 'true', N'SCHEMA', N'dbo', N'TABLE', N'UsrDonationIcon', N'COLUMN', N'BuyDateTime'
SET @v = N'Enabled - if payment processed correctly'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'UsrDonationIcon', N'COLUMN', N'Enabled'

EXECUTE sp_addextendedproperty N'MS_Description', N'Usrs having bought DonationIcons', N'SCHEMA', N'dbo', N'TABLE', N'UsrDonationIcon'

END

GO

