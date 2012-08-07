IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name =	'Banner' 
	AND	[column].name =		'FrequencyCapPerIdentifierPerDay'
) BEGIN
	ALTER TABLE dbo.Banner ADD
	FrequencyCapPerIdentifierPerDay int NOT NULL CONSTRAINT DF_Banner_FrequencyCapPerIdentifierPerDay DEFAULT -1
	
	
	DECLARE @v sql_variant 
	SET @v = N'The maximum number of times this banner should be served to a particular website user per day. -1 means uncapped'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'FrequencyCapPerIdentifierPerDay'

END

GO

IF EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name =	'BannerIdentityTempData' 
) BEGIN
	DROP table BannerIdentityTempData
END
GO

IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name =	'BannerIdentityTempData' 
) BEGIN

	CREATE TABLE dbo.BannerIdentityTempData
		(
		BannerK int NOT NULL,
		CurrentDay DateTime NOT NULL,
		IdentityGuid uniqueidentifier NOT NULL,
		Impressions int NOT NULL
		)
		
	DECLARE @v sql_variant 
	SET @v = N'BannerK being recorded'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'BannerIdentityTempData', N'COLUMN', N'BannerK'
	SET @v = N'Used to distinguish yesterday''s data from today''s before yesterday''s is archived'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'BannerIdentityTempData', N'COLUMN', N'CurrentDay'
	SET @v = N'Guid to identify this user'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'BannerIdentityTempData', N'COLUMN', N'IdentityGuid'
	SET @v = N'Total number of times banner has been served to this usr guid so far today'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'BannerIdentityTempData', N'COLUMN', N'Impressions'
	
	ALTER TABLE dbo.BannerIdentityTempData ADD CONSTRAINT
		PK_BannerIdentityTempData PRIMARY KEY CLUSTERED 
		(
		BannerK,
		CurrentDay,
		IdentityGuid
		) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Temporary store of Banner Impressions per Guid per Day, used for frequency capping and unique user stats' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'BannerIdentityTempData'

END
GO

IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name =	'BannerHighLevelData' 
) BEGIN

	CREATE TABLE dbo.BannerHighLevelData
		(
		BannerK int NOT NULL,
		Date datetime NOT NULL,
		Impressions int,
		UniqueUsrs int
		)
		
	DECLARE @v sql_variant 
	SET @v = N'BannerK being recorded'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'BannerHighLevelData', N'COLUMN', N'BannerK'
	SET @v = N'Date for recorded stats'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'BannerHighLevelData', N'COLUMN', N'Date'
	SET @v = N'Total number of times banner was been served on this day'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'BannerHighLevelData', N'COLUMN', N'Impressions'
	SET @v = N'Total number of unique usrs who were served this Banner on this Day'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'BannerHighLevelData', N'COLUMN', N'UniqueUsrs'
	
	ALTER TABLE dbo.BannerHighLevelData ADD CONSTRAINT
		PK_BannerHighLevelData PRIMARY KEY CLUSTERED 
		(
		BannerK,
		Date
		) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'BannerHighLevelData'

END
GO

IF EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name =	'Banner' 
	AND	[column].name =		'TargettingProperties'
) BEGIN
	ALTER TABLE dbo.Banner DROP CONSTRAINT DF_Banner_TargettingProperties 
	ALTER TABLE dbo.Banner DROP COLUMN TargettingProperties 

END
GO
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name =	'Banner' 
	AND	[column].name =		'TargettingProperties0'
) BEGIN
	ALTER TABLE dbo.Banner ADD
	TargettingProperties0 BIGINT NOT NULL CONSTRAINT DF_Banner_TargettingProperties0 DEFAULT 0,
	TargettingProperties1 BIGINT NOT NULL CONSTRAINT DF_Banner_TargettingProperties1 DEFAULT 0
	
	
	DECLARE @v sql_variant 
	SET @v = N'A total of all the targetting bit 0 = no targetting, otherwise is a bitwise total from BannerServer.Info.Types.TargettingBits range 0-62'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'TargettingProperties0'
	SET @v = N'A total of all the targetting bit 0 = no targetting, otherwise is a bitwise total from BannerServer.Info.Types.TargettingBits range 63-125'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'TargettingProperties1'
	

END




go
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'Guid'
) BEGIN
	EXEC('ALTER TABLE Usr ADD [Guid] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID()')
	EXEC('CREATE INDEX IDX_Usr_Guid ON Usr ([Guid])')
	
	EXECUTE sp_addextendedproperty N'MS_Description', N'A guid identifier for the Usr', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'Guid'

	EXEC('ALTER TABLE Demographics ADD [Guid] uniqueidentifier ')

	EXECUTE sp_addextendedproperty N'MS_Description', N'The guid of the usr or browser that the demographics information applies to', N'SCHEMA', N'dbo', N'TABLE', N'Demographics', N'COLUMN', N'Guid'

	EXEC('update d set d.[Guid] = u.[Guid] from Demographics d inner join Usr u on d.UsrK = u.K')
	BEGIN TRANSACTION

		CREATE TABLE dbo.Tmp_Demographics
			(
			Guid uniqueidentifier NOT NULL,
			DateTime datetime NULL,
			DrinkWater bit NULL,
			DrinkSoft bit NULL,
			DrinkEnergy bit NULL,
			DrinkDraftBeer bit NULL,
			DrinkBottledBeer bit NULL,
			DrinkSpirits bit NULL,
			DrinkWine bit NULL,
			DrinkAlcopops bit NULL,
			DrinkCider bit NULL,
			Smoke int NULL,
			EveningAllNight float(53) NULL,
			EveningLateNight float(53) NULL,
			EveningCoupleDrinks float(53) NULL,
			EveningOther float(53) NULL,
			EveningStayIn float(53) NULL,
			Employment int NULL,
			Salary int NULL,
			CreditCard bit NULL,
			Loan bit NULL,
			Mortgage bit NULL,
			OwnCar bit NULL,
			OwnBike bit NULL,
			OwnMp3 bit NULL,
			OwnPc bit NULL,
			OwnLaptop bit NULL,
			OwnMac bit NULL,
			OwnBroadband bit NULL,
			OwnConsole bit NULL,
			OwnCamera bit NULL,
			OwnDvd bit NULL,
			OwnDvdRec bit NULL,
			BuyCar bit NULL,
			BuyBike bit NULL,
			BuyMp3 bit NULL,
			BuyPc bit NULL,
			BuyLaptop bit NULL,
			BuyMac bit NULL,
			BuyBroadband bit NULL,
			BuyConsole bit NULL,
			BuyCamera bit NULL,
			BuyDvd bit NULL,
			BuyDvdRec bit NULL,
			SpendDesignerClothes int NULL,
			SpendHighStreetClothes int NULL,
			SpendMusicCd int NULL,
			SpendMusicVinyl int NULL,
			SpendMusicDownload int NULL,
			SpendDvd int NULL,
			SpendGames int NULL,
			SpendMobile int NULL,
			SpendSms int NULL,
			SpendCar int NULL,
			SpendTravel int NULL,
			Holidays int NULL,
			ImagingManufacturer varchar(255) NULL,
			ImagingImportant int NULL,
			ImagingOpinionSony int NULL,
			ImagingOpinionNokia int NULL,
			ImagingOpinionMotorola int NULL,
			ImagingOpinionSiemens int NULL,
			ImagingOpinionLg int NULL,
			ImagingOpinionSamsung int NULL,
			ImagingCapabilitySony int NULL,
			ImagingCapabilityNokia int NULL,
			ImagingCapabilityMotorola int NULL,
			ImagingCapabilitySiemens int NULL,
			ImagingCapabilityLg int NULL,
			ImagingCapabilitySamsung int NULL,
			ImagingBuySony int NULL,
			ImagingBuyNokia int NULL,
			ImagingBuyMotorola int NULL,
			ImagingBuySiemens int NULL,
			ImagingBuyLg int NULL,
			ImagingBuySamsung int NULL
		)  ON [PRIMARY]

		DECLARE @v sql_variant 
		SET @v = N'Demographics questionairre results'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', NULL, NULL
		SET @v = N'Guid of browser or Usr'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'Guid'
		SET @v = N'Date/time the questionairre was completed'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'DateTime'
		SET @v = N'When going out, what do you drink? Water'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'DrinkWater'
		SET @v = N'When going out, what do you drink? Soft drinks'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'DrinkSoft'
		SET @v = N'When going out, what do you drink? Energy drinks'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'DrinkEnergy'
		SET @v = N'When going out, what do you drink? Beer / lager (in a pint / glass)'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'DrinkDraftBeer'
		SET @v = N'When going out, what do you drink? Bottled beer / lager'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'DrinkBottledBeer'
		SET @v = N'When going out, what do you drink? Spirits'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'DrinkSpirits'
		SET @v = N'When going out, what do you drink? Wine'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'DrinkWine'
		SET @v = N'When going out, what do you drink? Alcopops'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'DrinkAlcopops'
		SET @v = N'When going out, what do you drink? Cider'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'DrinkCider'
		SET @v = N'Do you smoke? Yes=1, No=2, Only when I go out=3'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'Smoke'
		SET @v = N'How / how often do you spend your evenings: All night clubbing (times per week)'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'EveningAllNight'
		SET @v = N'How / how often do you spend your evenings: Late night at a pub/club (in bed by 3am) (times per week)'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'EveningLateNight'
		SET @v = N'How / how often do you spend your evenings: Couple of drinks in a bar (in bed by midnight) (times per week)'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'EveningCoupleDrinks'
		SET @v = N'How / how often do you spend your evenings: Other social event (e.g. cinema, restaurant etc.) (times per week)'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'EveningOther'
		SET @v = N'How / how often do you spend your evenings: Stay in / work (times per week)'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'EveningStayIn'
		SET @v = N'What’s your employment status: Full-time=1, Part-time=2, Currently unemployed=3, Student=4'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'Employment'
		SET @v = N'How much do you earn per year? [less than £15k]=1, [15 - 19]=2, [20 - 24]=3, [25 - 29]=4, [30 - 39]=5, [40 - 49]=6, [£50k+]=7'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'Salary'
		SET @v = N'Do you use a credit card?'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'CreditCard'
		SET @v = N'Do you have a personal loan?'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'Loan'
		SET @v = N'Do you have a mortgage?'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'Mortgage'
		SET @v = N'Do you own: Car / motorbike'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'OwnCar'
		SET @v = N'Do you own: Pedal bike'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'OwnBike'
		SET @v = N'Do you own: MP3 player'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'OwnMp3'
		SET @v = N'Do you own: PC'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'OwnPc'
		SET @v = N'Do you own: Laptop'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'OwnLaptop'
		SET @v = N'Do you own: Mac'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'OwnMac'
		SET @v = N'Do you own: Broadband internet'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'OwnBroadband'
		SET @v = N'Do you own: Games console'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'OwnConsole'
		SET @v = N'Do you own: Digital camera'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'OwnCamera'
		SET @v = N'Do you own: DVD player'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'OwnDvd'
		SET @v = N'Do you own: DVD recorder'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'OwnDvdRec'
		SET @v = N'Do you think you might buy in the next 6 months: Car / motorbike'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'BuyCar'
		SET @v = N'Do you think you might buy in the next 6 months: Pedal bike'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'BuyBike'
		SET @v = N'Do you think you might buy in the next 6 months: MP3 player'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'BuyMp3'
		SET @v = N'Do you think you might buy in the next 6 months: PC'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'BuyPc'
		SET @v = N'Do you think you might buy in the next 6 months: Laptop'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'BuyLaptop'
		SET @v = N'Do you think you might buy in the next 6 months: Mac'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'BuyMac'
		SET @v = N'Do you think you might buy in the next 6 months: Broadband internet'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'BuyBroadband'
		SET @v = N'Do you think you might buy in the next 6 months: Games console'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'BuyConsole'
		SET @v = N'Do you think you might buy in the next 6 months: Digital camera'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'BuyCamera'
		SET @v = N'Do you think you might buy in the next 6 months: DVD player'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'BuyDvd'
		SET @v = N'Do you think you might buy in the next 6 months: DVD recorder'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'BuyDvdRec'
		SET @v = N'How much do you spend on average per month on: Designer / branded clothes (Nothing=1, less than £10=2, £10-£19=3, £20-£49=4, £50-£99=5, £100-£200=6, £200+=7)'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'SpendDesignerClothes'
		SET @v = N'How much do you spend on average per month on: High street / non-branded clothes (Nothing=1, less than £10=2, £10-£19=3, £20-£49=4, £50-£99=5, £100-£200=6, £200+=7)'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'SpendHighStreetClothes'
		SET @v = N'How much do you spend on average per month on: Music on CD (Nothing=1, less than £10=2, £10-£19=3, £20-£49=4, £50-£99=5, £100-£200=6, £200+=7)'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'SpendMusicCd'
		SET @v = N'How much do you spend on average per month on: Music on vinyl (Nothing=1, less than £10=2, £10-£19=3, £20-£49=4, £50-£99=5, £100-£200=6, £200+=7)'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'SpendMusicVinyl'
		SET @v = N'How much do you spend on average per month on: Music downloads (Nothing=1, less than £10=2, £10-£19=3, £20-£49=4, £50-£99=5, £100-£200=6, £200+=7)'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'SpendMusicDownload'
		SET @v = N'How much do you spend on average per month on: DVDs (Nothing=1, less than £10=2, £10-£19=3, £20-£49=4, £50-£99=5, £100-£200=6, £200+=7)'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'SpendDvd'
		SET @v = N'How much do you spend on average per month on: Computer/video games (Nothing=1, less than £10=2, £10-£19=3, £20-£49=4, £50-£99=5, £100-£200=6, £200+=7)'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'SpendGames'
		SET @v = N'How much do you spend on average per month on: Mobile phone calls (Nothing=1, less than £10=2, £10-£19=3, £20-£49=4, £50-£99=5, £100-£200=6, £200+=7)'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'SpendMobile'
		SET @v = N'How much do you spend on average per month on: Ringtones / text voting etc. (Nothing=1, less than £10=2, £10-£19=3, £20-£49=4, £50-£99=5, £100-£200=6, £200+=7)'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'SpendSms'
		SET @v = N'How much do you spend on average per month on: Car / motorbike (Nothing=1, less than £10=2, £10-£19=3, £20-£49=4, £50-£99=5, £100-£200=6, £200+=7)'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'SpendCar'
		SET @v = N'How much do you spend on average per month on: Other travel / public transport (Nothing=1, less than £10=2, £10-£19=3, £20-£49=4, £50-£99=5, £100-£200=6, £200+=7)'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'SpendTravel'
		SET @v = N'How often do you go abroad on holiday? (time(s) per year)'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'Holidays'
		SET @v = N'When you think of mobile phone imaging technology, which mobile phone manufacturer comes to mind first?'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'ImagingManufacturer'
		SET @v = N'On a scale of 1-5, how important is imaging functionality in a mobile phone when considering which handset to get? (1 = not very important, 5 = very important)'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'ImagingImportant'
		SET @v = N'How would you describe your overall opinion about the following mobile phone manufacturers? (1 = not good, 5 = very good) Sony'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'ImagingOpinionSony'
		SET @v = N'How would you describe your overall opinion about the following mobile phone manufacturers? (1 = not good, 5 = very good) Nokia'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'ImagingOpinionNokia'
		SET @v = N'How would you describe your overall opinion about the following mobile phone manufacturers? (1 = not good, 5 = very good) Motorola'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'ImagingOpinionMotorola'
		SET @v = N'How would you describe your overall opinion about the following mobile phone manufacturers? (1 = not good, 5 = very good) BenQ/Siemens'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'ImagingOpinionSiemens'
		SET @v = N'How would you describe your overall opinion about the following mobile phone manufacturers? (1 = not good, 5 = very good) LG'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'ImagingOpinionLg'
		SET @v = N'How would you describe your overall opinion about the following mobile phone manufacturers? (1 = not good, 5 = very good) Samsung'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'ImagingOpinionSamsung'
		SET @v = N'Thinking of mobile phone imaging capabilities, how would you rate each of the following manufacturers on a scale of 1-5? (1=does not provide imaging technology, 5=provides advanced imaging technology) Sony'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'ImagingCapabilitySony'
		SET @v = N'Thinking of mobile phone imaging capabilities, how would you rate each of the following manufacturers on a scale of 1-5? (1=does not provide imaging technology, 5=provides advanced imaging technology) Nokia'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'ImagingCapabilityNokia'
		SET @v = N'Thinking of mobile phone imaging capabilities, how would you rate each of the following manufacturers on a scale of 1-5? (1=does not provide imaging technology, 5=provides advanced imaging technology) Motorola'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'ImagingCapabilityMotorola'
		SET @v = N'Thinking of mobile phone imaging capabilities, how would you rate each of the following manufacturers on a scale of 1-5? (1=does not provide imaging technology, 5=provides advanced imaging technology) BenQ/Siemens'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'ImagingCapabilitySiemens'
		SET @v = N'Thinking of mobile phone imaging capabilities, how would you rate each of the following manufacturers on a scale of 1-5? (1=does not provide imaging technology, 5=provides advanced imaging technology) LG'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'ImagingCapabilityLg'
		SET @v = N'Thinking of mobile phone imaging capabilities, how would you rate each of the following manufacturers on a scale of 1-5? (1=does not provide imaging technology, 5=provides advanced imaging technology) Samsung'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'ImagingCapabilitySamsung'
		SET @v = N'If you were to buy a new mobile phone, how likely would you be to consider the following manufacturers? (1=very unlikely, 5 = very likely) Sony'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'ImagingBuySony'
		SET @v = N'If you were to buy a new mobile phone, how likely would you be to consider the following manufacturers? (1=very unlikely, 5 = very likely) Nokia'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'ImagingBuyNokia'
		SET @v = N'If you were to buy a new mobile phone, how likely would you be to consider the following manufacturers? (1=very unlikely, 5 = very likely) Motorola'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'ImagingBuyMotorola'
		SET @v = N'If you were to buy a new mobile phone, how likely would you be to consider the following manufacturers? (1=very unlikely, 5 = very likely) BenQ/Siemens'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'ImagingBuySiemens'
		SET @v = N'If you were to buy a new mobile phone, how likely would you be to consider the following manufacturers? (1=very unlikely, 5 = very likely) LG'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'ImagingBuyLg'
		SET @v = N'If you were to buy a new mobile phone, how likely would you be to consider the following manufacturers? (1=very unlikely, 5 = very likely) Samsung'
		EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Demographics', N'COLUMN', N'ImagingBuySamsung'

		EXEC('INSERT INTO dbo.Tmp_Demographics (Guid, DateTime, DrinkWater, DrinkSoft, DrinkEnergy, DrinkDraftBeer, DrinkBottledBeer, DrinkSpirits, DrinkWine, DrinkAlcopops, DrinkCider, Smoke, EveningAllNight, EveningLateNight, EveningCoupleDrinks, EveningOther, EveningStayIn, Employment, Salary, CreditCard, Loan, Mortgage, OwnCar, OwnBike, OwnMp3, OwnPc, OwnLaptop, OwnMac, OwnBroadband, OwnConsole, OwnCamera, OwnDvd, OwnDvdRec, BuyCar, BuyBike, BuyMp3, BuyPc, BuyLaptop, BuyMac, BuyBroadband, BuyConsole, BuyCamera, BuyDvd, BuyDvdRec, SpendDesignerClothes, SpendHighStreetClothes, SpendMusicCd, SpendMusicVinyl, SpendMusicDownload, SpendDvd, SpendGames, SpendMobile, SpendSms, SpendCar, SpendTravel, Holidays, ImagingManufacturer, ImagingImportant, ImagingOpinionSony, ImagingOpinionNokia, ImagingOpinionMotorola, ImagingOpinionSiemens, ImagingOpinionLg, ImagingOpinionSamsung, ImagingCapabilitySony, ImagingCapabilityNokia, ImagingCapabilityMotorola, ImagingCapabilitySiemens, ImagingCapabilityLg, ImagingCapabilitySamsung, ImagingBuySony, ImagingBuyNokia, ImagingBuyMotorola, ImagingBuySiemens, ImagingBuyLg, ImagingBuySamsung)
				SELECT Guid, DateTime, DrinkWater, DrinkSoft, DrinkEnergy, DrinkDraftBeer, DrinkBottledBeer, DrinkSpirits, DrinkWine, DrinkAlcopops, DrinkCider, Smoke, EveningAllNight, EveningLateNight, EveningCoupleDrinks, EveningOther, EveningStayIn, Employment, Salary, CreditCard, Loan, Mortgage, OwnCar, OwnBike, OwnMp3, OwnPc, OwnLaptop, OwnMac, OwnBroadband, OwnConsole, OwnCamera, OwnDvd, OwnDvdRec, BuyCar, BuyBike, BuyMp3, BuyPc, BuyLaptop, BuyMac, BuyBroadband, BuyConsole, BuyCamera, BuyDvd, BuyDvdRec, SpendDesignerClothes, SpendHighStreetClothes, SpendMusicCd, SpendMusicVinyl, SpendMusicDownload, SpendDvd, SpendGames, SpendMobile, SpendSms, SpendCar, SpendTravel, Holidays, ImagingManufacturer, ImagingImportant, ImagingOpinionSony, ImagingOpinionNokia, ImagingOpinionMotorola, ImagingOpinionSiemens, ImagingOpinionLg, ImagingOpinionSamsung, ImagingCapabilitySony, ImagingCapabilityNokia, ImagingCapabilityMotorola, ImagingCapabilitySiemens, ImagingCapabilityLg, ImagingCapabilitySamsung, ImagingBuySony, ImagingBuyNokia, ImagingBuyMotorola, ImagingBuySiemens, ImagingBuyLg, ImagingBuySamsung FROM dbo.Demographics WITH (HOLDLOCK TABLOCKX) WHERE [Guid] IS NOT NULL')

		EXEC('DROP TABLE dbo.Demographics')

		EXECUTE sp_rename N'dbo.Tmp_Demographics', N'Demographics', 'OBJECT' 

		ALTER TABLE dbo.Demographics ADD CONSTRAINT
			PK_Demographics PRIMARY KEY CLUSTERED (Guid) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

	COMMIT


END
GO

IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name =	'TrafficLevelRelativeToMinuteOfDay' 
) BEGIN

	CREATE TABLE dbo.TrafficLevelRelativeToMinuteOfDay
		(
		Minute int NOT NULL,
		TrafficLevel int NOT NULL
		)
		
	DECLARE @v sql_variant 
	SET @v = N'Minute since midnight'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'TrafficLevelRelativeToMinuteOfDay', N'COLUMN', N'Minute'
	SET @v = N'A representative level of traffic at this minute'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'TrafficLevelRelativeToMinuteOfDay', N'COLUMN', N'TrafficLevel'
	
	ALTER TABLE dbo.TrafficLevelRelativeToMinuteOfDay ADD CONSTRAINT
		PK_TrafficLevelRelativeToMinuteOfDay PRIMARY KEY CLUSTERED 
		(
		Minute
		) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store of current analysed data calculating traffic levels at each minute of the day' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TrafficLevelRelativeToMinuteOfDay'

END
GO

IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name =	'TrafficExceptionDay' 
) BEGIN

	CREATE TABLE dbo.TrafficExceptionDay
		(
		ExceptionDate datetime NOT NULL,
		DateToUseInstead datetime NOT NULL
		)
		
	DECLARE @v sql_variant 
	SET @v = N'Date which is expected to not follow regular traffic patterns from week to week, e.g. days around a Bank Holiday'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'TrafficExceptionDay', N'COLUMN', N'ExceptionDate'
	SET @v = N'The date of a day whose traffic levels should be used instead, e.g. the previous Sunday if we expect low traffic'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'TrafficExceptionDay', N'COLUMN', N'DateToUseInstead'
	
	ALTER TABLE dbo.TrafficExceptionDay ADD CONSTRAINT
		PK_TrafficExceptionDay PRIMARY KEY CLUSTERED 
		(
		ExceptionDate
		) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Days which we do not expect to follow usual traffic patterns, and the day to use instead' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TrafficExceptionDay'

END
GO
IF EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name =	'MethodCachingExpirySettings' 
) BEGIN
	DROP TABLE dbo.MethodCachingExpirySettings
END 
GO
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name =	'MethodCachingExpirySetting' 
) BEGIN

	CREATE TABLE dbo.MethodCachingExpirySetting
		(
		MethodName varchar(200) NOT NULL,
		LifeSpanInSeconds int NOT NULL,
		ProbabilityThatCacheShouldNotBeUsed float(53) NOT NULL
		)  ON [PRIMARY]
	
	DECLARE @v sql_variant 
	SET @v = N'Configuration setting to control the frequency of request caching'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'MethodCachingExpirySetting', NULL, NULL
	SET @v = N'The name of the method to which the information applies'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'MethodCachingExpirySetting', N'COLUMN', N'MethodName'
	SET @v = N'The maximum lifespan in seconds that a call to this method should be cached. The default of -1 indicates that the content should never expire'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'MethodCachingExpirySetting', N'COLUMN', N'LifeSpanInSeconds'
	SET @v = N'A value between 0 and 1 representing the probability for any call to this method that the cache should be ignored and the value refreshed from the database'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'MethodCachingExpirySetting', N'COLUMN', N'ProbabilityThatCacheShouldNotBeUsed'
	
	ALTER TABLE dbo.MethodCachingExpirySetting ADD CONSTRAINT
		DF_MethodCachingExpirySetting_LifeSpanInSeconds DEFAULT -1 FOR LifeSpanInSeconds
	
	ALTER TABLE dbo.MethodCachingExpirySetting ADD CONSTRAINT
		DF_MethodCachingExpirySetting_ProbabilityThatCacheShouldNotBeUsed DEFAULT 1.0 FOR ProbabilityThatCacheShouldNotBeUsed

	ALTER TABLE dbo.MethodCachingExpirySetting ADD CONSTRAINT
		PK_MethodCachingExpirySetting PRIMARY KEY CLUSTERED 
		(
		MethodName
		) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END

GO

IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name =	'Banner' 
	AND	[column].name =		'ImpressionDateTimes'
) BEGIN

	ALTER TABLE dbo.Banner ADD ImpressionDateTimes VARCHAR(240)
	
	DECLARE @v sql_variant 
	SET @v = N'date times of recents hits as a concatenated string'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'ImpressionDateTimes'

END
GO

IF EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name =	'Banner' 
	AND	[column].name =		'TotalImpressionsUpUntilToday'
) BEGIN
	ALTER TABLE dbo.Banner DROP CONSTRAINT DF_Banner_TotalImpressionsUpUntilToday
	ALTER TABLE dbo.Banner DROP COLUMN TotalImpressionsUpUntilToday
	ALTER TABLE dbo.Banner DROP CONSTRAINT DF_Banner_TotalImpressionsSoFarToday
	ALTER TABLE dbo.Banner DROP COLUMN TotalImpressionsSoFarToday
END

go

IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name =	'Banner' 
	AND	[column].name =		'TotalImpressionsUpUntilToday'
) BEGIN

	ALTER TABLE dbo.Banner ADD TotalImpressionsUpUntilToday	int  NOT NULL CONSTRAINT DF_Banner_TotalImpressionsUpUntilToday	DEFAULT 0
	ALTER TABLE dbo.Banner ADD TotalImpressionsSoFarToday	int  NOT NULL CONSTRAINT DF_Banner_TotalImpressionsSoFarToday	DEFAULT 0
	
	DECLARE @v sql_variant 
	SET @v = N'total count of impressions this banner has had, updated once a day'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'TotalImpressionsUpUntilToday'
	SET @v = N'total count of impressions this banner has had so far today'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'TotalImpressionsSoFarToday'
	

END

GO










/**************************************************************************
	DATA CHANGES
***************************************************************************/

declare @Today datetime
set @Today = dateadd(day, 0, datediff(day, 0, getdate()))

update b set TotalImpressionsUpUntilToday = SumHits
from Banner b inner join 
(select BannerK, sum(Hits) SumHits from BannerStat where Date < @Today group by BannerK) bs
on b.K = bs.BannerK where Status = 2 and LastDay >= @Today

SET IDENTITY_INSERT [Global] ON
insert into Global (K, [Name], [Description], [ValueString]) values (18, 'BannerServerMethod', 'BannerGenerator / BannerGeneratorWithBannerServerLogging / BannerServer', 'BannerServer')
SET IDENTITY_INSERT [Global] OFF



DELETE FROM TrafficLevelRelativeToMinuteOfDay
TRUNCATE TABLE TrafficLevelRelativeToMinuteOfDay

insert into TrafficLevelRelativeToMinuteOfDay values (0, 4176)
insert into TrafficLevelRelativeToMinuteOfDay values (1, 4178)
insert into TrafficLevelRelativeToMinuteOfDay values (2, 4133)
insert into TrafficLevelRelativeToMinuteOfDay values (3, 4069)
insert into TrafficLevelRelativeToMinuteOfDay values (4, 3936)
insert into TrafficLevelRelativeToMinuteOfDay values (5, 3826)
insert into TrafficLevelRelativeToMinuteOfDay values (6, 3853)
insert into TrafficLevelRelativeToMinuteOfDay values (7, 3881)
insert into TrafficLevelRelativeToMinuteOfDay values (8, 3818)
insert into TrafficLevelRelativeToMinuteOfDay values (9, 3797)
insert into TrafficLevelRelativeToMinuteOfDay values (10, 3791)
insert into TrafficLevelRelativeToMinuteOfDay values (11, 4148)
insert into TrafficLevelRelativeToMinuteOfDay values (12, 4231)
insert into TrafficLevelRelativeToMinuteOfDay values (13, 4016)
insert into TrafficLevelRelativeToMinuteOfDay values (14, 4003)
insert into TrafficLevelRelativeToMinuteOfDay values (15, 4045)
insert into TrafficLevelRelativeToMinuteOfDay values (16, 4078)
insert into TrafficLevelRelativeToMinuteOfDay values (17, 3830)
insert into TrafficLevelRelativeToMinuteOfDay values (18, 3857)
insert into TrafficLevelRelativeToMinuteOfDay values (19, 3794)
insert into TrafficLevelRelativeToMinuteOfDay values (20, 3734)
insert into TrafficLevelRelativeToMinuteOfDay values (21, 3706)
insert into TrafficLevelRelativeToMinuteOfDay values (22, 3616)
insert into TrafficLevelRelativeToMinuteOfDay values (23, 3620)
insert into TrafficLevelRelativeToMinuteOfDay values (24, 3621)
insert into TrafficLevelRelativeToMinuteOfDay values (25, 3644)
insert into TrafficLevelRelativeToMinuteOfDay values (26, 3574)
insert into TrafficLevelRelativeToMinuteOfDay values (27, 3550)
insert into TrafficLevelRelativeToMinuteOfDay values (28, 3563)
insert into TrafficLevelRelativeToMinuteOfDay values (29, 3511)
insert into TrafficLevelRelativeToMinuteOfDay values (30, 3460)
insert into TrafficLevelRelativeToMinuteOfDay values (31, 3502)
insert into TrafficLevelRelativeToMinuteOfDay values (32, 3519)
insert into TrafficLevelRelativeToMinuteOfDay values (33, 3494)
insert into TrafficLevelRelativeToMinuteOfDay values (34, 3535)
insert into TrafficLevelRelativeToMinuteOfDay values (35, 3488)
insert into TrafficLevelRelativeToMinuteOfDay values (36, 3440)
insert into TrafficLevelRelativeToMinuteOfDay values (37, 3441)
insert into TrafficLevelRelativeToMinuteOfDay values (38, 3485)
insert into TrafficLevelRelativeToMinuteOfDay values (39, 3464)
insert into TrafficLevelRelativeToMinuteOfDay values (40, 3389)
insert into TrafficLevelRelativeToMinuteOfDay values (41, 3358)
insert into TrafficLevelRelativeToMinuteOfDay values (42, 3286)
insert into TrafficLevelRelativeToMinuteOfDay values (43, 3239)
insert into TrafficLevelRelativeToMinuteOfDay values (44, 3215)
insert into TrafficLevelRelativeToMinuteOfDay values (45, 3188)
insert into TrafficLevelRelativeToMinuteOfDay values (46, 3173)
insert into TrafficLevelRelativeToMinuteOfDay values (47, 3113)
insert into TrafficLevelRelativeToMinuteOfDay values (48, 3148)
insert into TrafficLevelRelativeToMinuteOfDay values (49, 3107)
insert into TrafficLevelRelativeToMinuteOfDay values (50, 3106)
insert into TrafficLevelRelativeToMinuteOfDay values (51, 3129)
insert into TrafficLevelRelativeToMinuteOfDay values (52, 3156)
insert into TrafficLevelRelativeToMinuteOfDay values (53, 3192)
insert into TrafficLevelRelativeToMinuteOfDay values (54, 3129)
insert into TrafficLevelRelativeToMinuteOfDay values (55, 3049)
insert into TrafficLevelRelativeToMinuteOfDay values (56, 3037)
insert into TrafficLevelRelativeToMinuteOfDay values (57, 3025)
insert into TrafficLevelRelativeToMinuteOfDay values (58, 3022)
insert into TrafficLevelRelativeToMinuteOfDay values (59, 2995)
insert into TrafficLevelRelativeToMinuteOfDay values (60, 2933)
insert into TrafficLevelRelativeToMinuteOfDay values (61, 2887)
insert into TrafficLevelRelativeToMinuteOfDay values (62, 2844)
insert into TrafficLevelRelativeToMinuteOfDay values (63, 2765)
insert into TrafficLevelRelativeToMinuteOfDay values (64, 2742)
insert into TrafficLevelRelativeToMinuteOfDay values (65, 2771)
insert into TrafficLevelRelativeToMinuteOfDay values (66, 2726)
insert into TrafficLevelRelativeToMinuteOfDay values (67, 2789)
insert into TrafficLevelRelativeToMinuteOfDay values (68, 2807)
insert into TrafficLevelRelativeToMinuteOfDay values (69, 2761)
insert into TrafficLevelRelativeToMinuteOfDay values (70, 2734)
insert into TrafficLevelRelativeToMinuteOfDay values (71, 2719)
insert into TrafficLevelRelativeToMinuteOfDay values (72, 2730)
insert into TrafficLevelRelativeToMinuteOfDay values (73, 2738)
insert into TrafficLevelRelativeToMinuteOfDay values (74, 2698)
insert into TrafficLevelRelativeToMinuteOfDay values (75, 2682)
insert into TrafficLevelRelativeToMinuteOfDay values (76, 2641)
insert into TrafficLevelRelativeToMinuteOfDay values (77, 2655)
insert into TrafficLevelRelativeToMinuteOfDay values (78, 2619)
insert into TrafficLevelRelativeToMinuteOfDay values (79, 2588)
insert into TrafficLevelRelativeToMinuteOfDay values (80, 2611)
insert into TrafficLevelRelativeToMinuteOfDay values (81, 2551)
insert into TrafficLevelRelativeToMinuteOfDay values (82, 2473)
insert into TrafficLevelRelativeToMinuteOfDay values (83, 2426)
insert into TrafficLevelRelativeToMinuteOfDay values (84, 2412)
insert into TrafficLevelRelativeToMinuteOfDay values (85, 2408)
insert into TrafficLevelRelativeToMinuteOfDay values (86, 2476)
insert into TrafficLevelRelativeToMinuteOfDay values (87, 2381)
insert into TrafficLevelRelativeToMinuteOfDay values (88, 2324)
insert into TrafficLevelRelativeToMinuteOfDay values (89, 2347)
insert into TrafficLevelRelativeToMinuteOfDay values (90, 2263)
insert into TrafficLevelRelativeToMinuteOfDay values (91, 2260)
insert into TrafficLevelRelativeToMinuteOfDay values (92, 2281)
insert into TrafficLevelRelativeToMinuteOfDay values (93, 2320)
insert into TrafficLevelRelativeToMinuteOfDay values (94, 2291)
insert into TrafficLevelRelativeToMinuteOfDay values (95, 2285)
insert into TrafficLevelRelativeToMinuteOfDay values (96, 2278)
insert into TrafficLevelRelativeToMinuteOfDay values (97, 2287)
insert into TrafficLevelRelativeToMinuteOfDay values (98, 2269)
insert into TrafficLevelRelativeToMinuteOfDay values (99, 2271)
insert into TrafficLevelRelativeToMinuteOfDay values (100, 2221)
insert into TrafficLevelRelativeToMinuteOfDay values (101, 2220)
insert into TrafficLevelRelativeToMinuteOfDay values (102, 2186)
insert into TrafficLevelRelativeToMinuteOfDay values (103, 2173)
insert into TrafficLevelRelativeToMinuteOfDay values (104, 2150)
insert into TrafficLevelRelativeToMinuteOfDay values (105, 2149)
insert into TrafficLevelRelativeToMinuteOfDay values (106, 2117)
insert into TrafficLevelRelativeToMinuteOfDay values (107, 2112)
insert into TrafficLevelRelativeToMinuteOfDay values (108, 2072)
insert into TrafficLevelRelativeToMinuteOfDay values (109, 2033)
insert into TrafficLevelRelativeToMinuteOfDay values (110, 2039)
insert into TrafficLevelRelativeToMinuteOfDay values (111, 2057)
insert into TrafficLevelRelativeToMinuteOfDay values (112, 2075)
insert into TrafficLevelRelativeToMinuteOfDay values (113, 1986)
insert into TrafficLevelRelativeToMinuteOfDay values (114, 1971)
insert into TrafficLevelRelativeToMinuteOfDay values (115, 2021)
insert into TrafficLevelRelativeToMinuteOfDay values (116, 1980)
insert into TrafficLevelRelativeToMinuteOfDay values (117, 1961)
insert into TrafficLevelRelativeToMinuteOfDay values (118, 1995)
insert into TrafficLevelRelativeToMinuteOfDay values (119, 2045)
insert into TrafficLevelRelativeToMinuteOfDay values (120, 2031)
insert into TrafficLevelRelativeToMinuteOfDay values (121, 2032)
insert into TrafficLevelRelativeToMinuteOfDay values (122, 1992)
insert into TrafficLevelRelativeToMinuteOfDay values (123, 1975)
insert into TrafficLevelRelativeToMinuteOfDay values (124, 1980)
insert into TrafficLevelRelativeToMinuteOfDay values (125, 2059)
insert into TrafficLevelRelativeToMinuteOfDay values (126, 2053)
insert into TrafficLevelRelativeToMinuteOfDay values (127, 2050)
insert into TrafficLevelRelativeToMinuteOfDay values (128, 2067)
insert into TrafficLevelRelativeToMinuteOfDay values (129, 1995)
insert into TrafficLevelRelativeToMinuteOfDay values (130, 1972)
insert into TrafficLevelRelativeToMinuteOfDay values (131, 1992)
insert into TrafficLevelRelativeToMinuteOfDay values (132, 1973)
insert into TrafficLevelRelativeToMinuteOfDay values (133, 1993)
insert into TrafficLevelRelativeToMinuteOfDay values (134, 2019)
insert into TrafficLevelRelativeToMinuteOfDay values (135, 1965)
insert into TrafficLevelRelativeToMinuteOfDay values (136, 1926)
insert into TrafficLevelRelativeToMinuteOfDay values (137, 1836)
insert into TrafficLevelRelativeToMinuteOfDay values (138, 1802)
insert into TrafficLevelRelativeToMinuteOfDay values (139, 1834)
insert into TrafficLevelRelativeToMinuteOfDay values (140, 1835)
insert into TrafficLevelRelativeToMinuteOfDay values (141, 1803)
insert into TrafficLevelRelativeToMinuteOfDay values (142, 1828)
insert into TrafficLevelRelativeToMinuteOfDay values (143, 1829)
insert into TrafficLevelRelativeToMinuteOfDay values (144, 1771)
insert into TrafficLevelRelativeToMinuteOfDay values (145, 1800)
insert into TrafficLevelRelativeToMinuteOfDay values (146, 1801)
insert into TrafficLevelRelativeToMinuteOfDay values (147, 1764)
insert into TrafficLevelRelativeToMinuteOfDay values (148, 1774)
insert into TrafficLevelRelativeToMinuteOfDay values (149, 1771)
insert into TrafficLevelRelativeToMinuteOfDay values (150, 1663)
insert into TrafficLevelRelativeToMinuteOfDay values (151, 1637)
insert into TrafficLevelRelativeToMinuteOfDay values (152, 1649)
insert into TrafficLevelRelativeToMinuteOfDay values (153, 1626)
insert into TrafficLevelRelativeToMinuteOfDay values (154, 1622)
insert into TrafficLevelRelativeToMinuteOfDay values (155, 1621)
insert into TrafficLevelRelativeToMinuteOfDay values (156, 1641)
insert into TrafficLevelRelativeToMinuteOfDay values (157, 1606)
insert into TrafficLevelRelativeToMinuteOfDay values (158, 1590)
insert into TrafficLevelRelativeToMinuteOfDay values (159, 1605)
insert into TrafficLevelRelativeToMinuteOfDay values (160, 1599)
insert into TrafficLevelRelativeToMinuteOfDay values (161, 1581)
insert into TrafficLevelRelativeToMinuteOfDay values (162, 1571)
insert into TrafficLevelRelativeToMinuteOfDay values (163, 1570)
insert into TrafficLevelRelativeToMinuteOfDay values (164, 1583)
insert into TrafficLevelRelativeToMinuteOfDay values (165, 1578)
insert into TrafficLevelRelativeToMinuteOfDay values (166, 1606)
insert into TrafficLevelRelativeToMinuteOfDay values (167, 1594)
insert into TrafficLevelRelativeToMinuteOfDay values (168, 1516)
insert into TrafficLevelRelativeToMinuteOfDay values (169, 1480)
insert into TrafficLevelRelativeToMinuteOfDay values (170, 1461)
insert into TrafficLevelRelativeToMinuteOfDay values (171, 1487)
insert into TrafficLevelRelativeToMinuteOfDay values (172, 1472)
insert into TrafficLevelRelativeToMinuteOfDay values (173, 1464)
insert into TrafficLevelRelativeToMinuteOfDay values (174, 1470)
insert into TrafficLevelRelativeToMinuteOfDay values (175, 1489)
insert into TrafficLevelRelativeToMinuteOfDay values (176, 1466)
insert into TrafficLevelRelativeToMinuteOfDay values (177, 1427)
insert into TrafficLevelRelativeToMinuteOfDay values (178, 1408)
insert into TrafficLevelRelativeToMinuteOfDay values (179, 1419)
insert into TrafficLevelRelativeToMinuteOfDay values (180, 1416)
insert into TrafficLevelRelativeToMinuteOfDay values (181, 1314)
insert into TrafficLevelRelativeToMinuteOfDay values (182, 1307)
insert into TrafficLevelRelativeToMinuteOfDay values (183, 1331)
insert into TrafficLevelRelativeToMinuteOfDay values (184, 1339)
insert into TrafficLevelRelativeToMinuteOfDay values (185, 1330)
insert into TrafficLevelRelativeToMinuteOfDay values (186, 1276)
insert into TrafficLevelRelativeToMinuteOfDay values (187, 1231)
insert into TrafficLevelRelativeToMinuteOfDay values (188, 1224)
insert into TrafficLevelRelativeToMinuteOfDay values (189, 1240)
insert into TrafficLevelRelativeToMinuteOfDay values (190, 1229)
insert into TrafficLevelRelativeToMinuteOfDay values (191, 1244)
insert into TrafficLevelRelativeToMinuteOfDay values (192, 1237)
insert into TrafficLevelRelativeToMinuteOfDay values (193, 1176)
insert into TrafficLevelRelativeToMinuteOfDay values (194, 1167)
insert into TrafficLevelRelativeToMinuteOfDay values (195, 1183)
insert into TrafficLevelRelativeToMinuteOfDay values (196, 1177)
insert into TrafficLevelRelativeToMinuteOfDay values (197, 1162)
insert into TrafficLevelRelativeToMinuteOfDay values (198, 1151)
insert into TrafficLevelRelativeToMinuteOfDay values (199, 1178)
insert into TrafficLevelRelativeToMinuteOfDay values (200, 1188)
insert into TrafficLevelRelativeToMinuteOfDay values (201, 1151)
insert into TrafficLevelRelativeToMinuteOfDay values (202, 1131)
insert into TrafficLevelRelativeToMinuteOfDay values (203, 1161)
insert into TrafficLevelRelativeToMinuteOfDay values (204, 1163)
insert into TrafficLevelRelativeToMinuteOfDay values (205, 1186)
insert into TrafficLevelRelativeToMinuteOfDay values (206, 1168)
insert into TrafficLevelRelativeToMinuteOfDay values (207, 1153)
insert into TrafficLevelRelativeToMinuteOfDay values (208, 1158)
insert into TrafficLevelRelativeToMinuteOfDay values (209, 1136)
insert into TrafficLevelRelativeToMinuteOfDay values (210, 1145)
insert into TrafficLevelRelativeToMinuteOfDay values (211, 1109)
insert into TrafficLevelRelativeToMinuteOfDay values (212, 1097)
insert into TrafficLevelRelativeToMinuteOfDay values (213, 1101)
insert into TrafficLevelRelativeToMinuteOfDay values (214, 1108)
insert into TrafficLevelRelativeToMinuteOfDay values (215, 1116)
insert into TrafficLevelRelativeToMinuteOfDay values (216, 1099)
insert into TrafficLevelRelativeToMinuteOfDay values (217, 1076)
insert into TrafficLevelRelativeToMinuteOfDay values (218, 1079)
insert into TrafficLevelRelativeToMinuteOfDay values (219, 1137)
insert into TrafficLevelRelativeToMinuteOfDay values (220, 1123)
insert into TrafficLevelRelativeToMinuteOfDay values (221, 1128)
insert into TrafficLevelRelativeToMinuteOfDay values (222, 1109)
insert into TrafficLevelRelativeToMinuteOfDay values (223, 1108)
insert into TrafficLevelRelativeToMinuteOfDay values (224, 1117)
insert into TrafficLevelRelativeToMinuteOfDay values (225, 1102)
insert into TrafficLevelRelativeToMinuteOfDay values (226, 1100)
insert into TrafficLevelRelativeToMinuteOfDay values (227, 1095)
insert into TrafficLevelRelativeToMinuteOfDay values (228, 1099)
insert into TrafficLevelRelativeToMinuteOfDay values (229, 1080)
insert into TrafficLevelRelativeToMinuteOfDay values (230, 1065)
insert into TrafficLevelRelativeToMinuteOfDay values (231, 1064)
insert into TrafficLevelRelativeToMinuteOfDay values (232, 1095)
insert into TrafficLevelRelativeToMinuteOfDay values (233, 1094)
insert into TrafficLevelRelativeToMinuteOfDay values (234, 1077)
insert into TrafficLevelRelativeToMinuteOfDay values (235, 1077)
insert into TrafficLevelRelativeToMinuteOfDay values (236, 1053)
insert into TrafficLevelRelativeToMinuteOfDay values (237, 1049)
insert into TrafficLevelRelativeToMinuteOfDay values (238, 1026)
insert into TrafficLevelRelativeToMinuteOfDay values (239, 1004)
insert into TrafficLevelRelativeToMinuteOfDay values (240, 1023)
insert into TrafficLevelRelativeToMinuteOfDay values (241, 1028)
insert into TrafficLevelRelativeToMinuteOfDay values (242, 1037)
insert into TrafficLevelRelativeToMinuteOfDay values (243, 1073)
insert into TrafficLevelRelativeToMinuteOfDay values (244, 1053)
insert into TrafficLevelRelativeToMinuteOfDay values (245, 1060)
insert into TrafficLevelRelativeToMinuteOfDay values (246, 1045)
insert into TrafficLevelRelativeToMinuteOfDay values (247, 1014)
insert into TrafficLevelRelativeToMinuteOfDay values (248, 997)
insert into TrafficLevelRelativeToMinuteOfDay values (249, 1012)
insert into TrafficLevelRelativeToMinuteOfDay values (250, 1030)
insert into TrafficLevelRelativeToMinuteOfDay values (251, 1048)
insert into TrafficLevelRelativeToMinuteOfDay values (252, 1070)
insert into TrafficLevelRelativeToMinuteOfDay values (253, 1036)
insert into TrafficLevelRelativeToMinuteOfDay values (254, 1022)
insert into TrafficLevelRelativeToMinuteOfDay values (255, 1011)
insert into TrafficLevelRelativeToMinuteOfDay values (256, 999)
insert into TrafficLevelRelativeToMinuteOfDay values (257, 991)
insert into TrafficLevelRelativeToMinuteOfDay values (258, 988)
insert into TrafficLevelRelativeToMinuteOfDay values (259, 978)
insert into TrafficLevelRelativeToMinuteOfDay values (260, 966)
insert into TrafficLevelRelativeToMinuteOfDay values (261, 990)
insert into TrafficLevelRelativeToMinuteOfDay values (262, 1008)
insert into TrafficLevelRelativeToMinuteOfDay values (263, 1004)
insert into TrafficLevelRelativeToMinuteOfDay values (264, 996)
insert into TrafficLevelRelativeToMinuteOfDay values (265, 983)
insert into TrafficLevelRelativeToMinuteOfDay values (266, 983)
insert into TrafficLevelRelativeToMinuteOfDay values (267, 984)
insert into TrafficLevelRelativeToMinuteOfDay values (268, 972)
insert into TrafficLevelRelativeToMinuteOfDay values (269, 939)
insert into TrafficLevelRelativeToMinuteOfDay values (270, 957)
insert into TrafficLevelRelativeToMinuteOfDay values (271, 993)
insert into TrafficLevelRelativeToMinuteOfDay values (272, 1001)
insert into TrafficLevelRelativeToMinuteOfDay values (273, 981)
insert into TrafficLevelRelativeToMinuteOfDay values (274, 967)
insert into TrafficLevelRelativeToMinuteOfDay values (275, 981)
insert into TrafficLevelRelativeToMinuteOfDay values (276, 979)
insert into TrafficLevelRelativeToMinuteOfDay values (277, 1012)
insert into TrafficLevelRelativeToMinuteOfDay values (278, 1029)
insert into TrafficLevelRelativeToMinuteOfDay values (279, 1014)
insert into TrafficLevelRelativeToMinuteOfDay values (280, 981)
insert into TrafficLevelRelativeToMinuteOfDay values (281, 1016)
insert into TrafficLevelRelativeToMinuteOfDay values (282, 1041)
insert into TrafficLevelRelativeToMinuteOfDay values (283, 1019)
insert into TrafficLevelRelativeToMinuteOfDay values (284, 1019)
insert into TrafficLevelRelativeToMinuteOfDay values (285, 1003)
insert into TrafficLevelRelativeToMinuteOfDay values (286, 974)
insert into TrafficLevelRelativeToMinuteOfDay values (287, 1001)
insert into TrafficLevelRelativeToMinuteOfDay values (288, 1008)
insert into TrafficLevelRelativeToMinuteOfDay values (289, 999)
insert into TrafficLevelRelativeToMinuteOfDay values (290, 991)
insert into TrafficLevelRelativeToMinuteOfDay values (291, 989)
insert into TrafficLevelRelativeToMinuteOfDay values (292, 992)
insert into TrafficLevelRelativeToMinuteOfDay values (293, 974)
insert into TrafficLevelRelativeToMinuteOfDay values (294, 975)
insert into TrafficLevelRelativeToMinuteOfDay values (295, 1011)
insert into TrafficLevelRelativeToMinuteOfDay values (296, 982)
insert into TrafficLevelRelativeToMinuteOfDay values (297, 903)
insert into TrafficLevelRelativeToMinuteOfDay values (298, 904)
insert into TrafficLevelRelativeToMinuteOfDay values (299, 940)
insert into TrafficLevelRelativeToMinuteOfDay values (300, 947)
insert into TrafficLevelRelativeToMinuteOfDay values (301, 968)
insert into TrafficLevelRelativeToMinuteOfDay values (302, 953)
insert into TrafficLevelRelativeToMinuteOfDay values (303, 919)
insert into TrafficLevelRelativeToMinuteOfDay values (304, 902)
insert into TrafficLevelRelativeToMinuteOfDay values (305, 880)
insert into TrafficLevelRelativeToMinuteOfDay values (306, 888)
insert into TrafficLevelRelativeToMinuteOfDay values (307, 891)
insert into TrafficLevelRelativeToMinuteOfDay values (308, 922)
insert into TrafficLevelRelativeToMinuteOfDay values (309, 915)
insert into TrafficLevelRelativeToMinuteOfDay values (310, 893)
insert into TrafficLevelRelativeToMinuteOfDay values (311, 927)
insert into TrafficLevelRelativeToMinuteOfDay values (312, 966)
insert into TrafficLevelRelativeToMinuteOfDay values (313, 963)
insert into TrafficLevelRelativeToMinuteOfDay values (314, 977)
insert into TrafficLevelRelativeToMinuteOfDay values (315, 983)
insert into TrafficLevelRelativeToMinuteOfDay values (316, 955)
insert into TrafficLevelRelativeToMinuteOfDay values (317, 908)
insert into TrafficLevelRelativeToMinuteOfDay values (318, 885)
insert into TrafficLevelRelativeToMinuteOfDay values (319, 915)
insert into TrafficLevelRelativeToMinuteOfDay values (320, 920)
insert into TrafficLevelRelativeToMinuteOfDay values (321, 927)
insert into TrafficLevelRelativeToMinuteOfDay values (322, 928)
insert into TrafficLevelRelativeToMinuteOfDay values (323, 916)
insert into TrafficLevelRelativeToMinuteOfDay values (324, 938)
insert into TrafficLevelRelativeToMinuteOfDay values (325, 936)
insert into TrafficLevelRelativeToMinuteOfDay values (326, 940)
insert into TrafficLevelRelativeToMinuteOfDay values (327, 944)
insert into TrafficLevelRelativeToMinuteOfDay values (328, 937)
insert into TrafficLevelRelativeToMinuteOfDay values (329, 912)
insert into TrafficLevelRelativeToMinuteOfDay values (330, 873)
insert into TrafficLevelRelativeToMinuteOfDay values (331, 875)
insert into TrafficLevelRelativeToMinuteOfDay values (332, 865)
insert into TrafficLevelRelativeToMinuteOfDay values (333, 861)
insert into TrafficLevelRelativeToMinuteOfDay values (334, 862)
insert into TrafficLevelRelativeToMinuteOfDay values (335, 853)
insert into TrafficLevelRelativeToMinuteOfDay values (336, 849)
insert into TrafficLevelRelativeToMinuteOfDay values (337, 846)
insert into TrafficLevelRelativeToMinuteOfDay values (338, 823)
insert into TrafficLevelRelativeToMinuteOfDay values (339, 822)
insert into TrafficLevelRelativeToMinuteOfDay values (340, 835)
insert into TrafficLevelRelativeToMinuteOfDay values (341, 901)
insert into TrafficLevelRelativeToMinuteOfDay values (342, 915)
insert into TrafficLevelRelativeToMinuteOfDay values (343, 924)
insert into TrafficLevelRelativeToMinuteOfDay values (344, 874)
insert into TrafficLevelRelativeToMinuteOfDay values (345, 849)
insert into TrafficLevelRelativeToMinuteOfDay values (346, 830)
insert into TrafficLevelRelativeToMinuteOfDay values (347, 851)
insert into TrafficLevelRelativeToMinuteOfDay values (348, 873)
insert into TrafficLevelRelativeToMinuteOfDay values (349, 859)
insert into TrafficLevelRelativeToMinuteOfDay values (350, 841)
insert into TrafficLevelRelativeToMinuteOfDay values (351, 855)
insert into TrafficLevelRelativeToMinuteOfDay values (352, 869)
insert into TrafficLevelRelativeToMinuteOfDay values (353, 848)
insert into TrafficLevelRelativeToMinuteOfDay values (354, 830)
insert into TrafficLevelRelativeToMinuteOfDay values (355, 843)
insert into TrafficLevelRelativeToMinuteOfDay values (356, 843)
insert into TrafficLevelRelativeToMinuteOfDay values (357, 835)
insert into TrafficLevelRelativeToMinuteOfDay values (358, 820)
insert into TrafficLevelRelativeToMinuteOfDay values (359, 790)
insert into TrafficLevelRelativeToMinuteOfDay values (360, 795)
insert into TrafficLevelRelativeToMinuteOfDay values (361, 807)
insert into TrafficLevelRelativeToMinuteOfDay values (362, 810)
insert into TrafficLevelRelativeToMinuteOfDay values (363, 827)
insert into TrafficLevelRelativeToMinuteOfDay values (364, 844)
insert into TrafficLevelRelativeToMinuteOfDay values (365, 828)
insert into TrafficLevelRelativeToMinuteOfDay values (366, 962)
insert into TrafficLevelRelativeToMinuteOfDay values (367, 957)
insert into TrafficLevelRelativeToMinuteOfDay values (368, 940)
insert into TrafficLevelRelativeToMinuteOfDay values (369, 957)
insert into TrafficLevelRelativeToMinuteOfDay values (370, 992)
insert into TrafficLevelRelativeToMinuteOfDay values (371, 973)
insert into TrafficLevelRelativeToMinuteOfDay values (372, 966)
insert into TrafficLevelRelativeToMinuteOfDay values (373, 986)
insert into TrafficLevelRelativeToMinuteOfDay values (374, 991)
insert into TrafficLevelRelativeToMinuteOfDay values (375, 1010)
insert into TrafficLevelRelativeToMinuteOfDay values (376, 1026)
insert into TrafficLevelRelativeToMinuteOfDay values (377, 1032)
insert into TrafficLevelRelativeToMinuteOfDay values (378, 1028)
insert into TrafficLevelRelativeToMinuteOfDay values (379, 1020)
insert into TrafficLevelRelativeToMinuteOfDay values (380, 1021)
insert into TrafficLevelRelativeToMinuteOfDay values (381, 1009)
insert into TrafficLevelRelativeToMinuteOfDay values (382, 1032)
insert into TrafficLevelRelativeToMinuteOfDay values (383, 1029)
insert into TrafficLevelRelativeToMinuteOfDay values (384, 1002)
insert into TrafficLevelRelativeToMinuteOfDay values (385, 1018)
insert into TrafficLevelRelativeToMinuteOfDay values (386, 1017)
insert into TrafficLevelRelativeToMinuteOfDay values (387, 1012)
insert into TrafficLevelRelativeToMinuteOfDay values (388, 1028)
insert into TrafficLevelRelativeToMinuteOfDay values (389, 1044)
insert into TrafficLevelRelativeToMinuteOfDay values (390, 1070)
insert into TrafficLevelRelativeToMinuteOfDay values (391, 1071)
insert into TrafficLevelRelativeToMinuteOfDay values (392, 1063)
insert into TrafficLevelRelativeToMinuteOfDay values (393, 1061)
insert into TrafficLevelRelativeToMinuteOfDay values (394, 1064)
insert into TrafficLevelRelativeToMinuteOfDay values (395, 1088)
insert into TrafficLevelRelativeToMinuteOfDay values (396, 1084)
insert into TrafficLevelRelativeToMinuteOfDay values (397, 1071)
insert into TrafficLevelRelativeToMinuteOfDay values (398, 1105)
insert into TrafficLevelRelativeToMinuteOfDay values (399, 1093)
insert into TrafficLevelRelativeToMinuteOfDay values (400, 1048)
insert into TrafficLevelRelativeToMinuteOfDay values (401, 1036)
insert into TrafficLevelRelativeToMinuteOfDay values (402, 1066)
insert into TrafficLevelRelativeToMinuteOfDay values (403, 1083)
insert into TrafficLevelRelativeToMinuteOfDay values (404, 1094)
insert into TrafficLevelRelativeToMinuteOfDay values (405, 1073)
insert into TrafficLevelRelativeToMinuteOfDay values (406, 1103)
insert into TrafficLevelRelativeToMinuteOfDay values (407, 1106)
insert into TrafficLevelRelativeToMinuteOfDay values (408, 1076)
insert into TrafficLevelRelativeToMinuteOfDay values (409, 1082)
insert into TrafficLevelRelativeToMinuteOfDay values (410, 1076)
insert into TrafficLevelRelativeToMinuteOfDay values (411, 1071)
insert into TrafficLevelRelativeToMinuteOfDay values (412, 1050)
insert into TrafficLevelRelativeToMinuteOfDay values (413, 1036)
insert into TrafficLevelRelativeToMinuteOfDay values (414, 1077)
insert into TrafficLevelRelativeToMinuteOfDay values (415, 1092)
insert into TrafficLevelRelativeToMinuteOfDay values (416, 1101)
insert into TrafficLevelRelativeToMinuteOfDay values (417, 1087)
insert into TrafficLevelRelativeToMinuteOfDay values (418, 1062)
insert into TrafficLevelRelativeToMinuteOfDay values (419, 1077)
insert into TrafficLevelRelativeToMinuteOfDay values (420, 1083)
insert into TrafficLevelRelativeToMinuteOfDay values (421, 1125)
insert into TrafficLevelRelativeToMinuteOfDay values (422, 1148)
insert into TrafficLevelRelativeToMinuteOfDay values (423, 1176)
insert into TrafficLevelRelativeToMinuteOfDay values (424, 1156)
insert into TrafficLevelRelativeToMinuteOfDay values (425, 1354)
insert into TrafficLevelRelativeToMinuteOfDay values (426, 1652)
insert into TrafficLevelRelativeToMinuteOfDay values (427, 1908)
insert into TrafficLevelRelativeToMinuteOfDay values (428, 2060)
insert into TrafficLevelRelativeToMinuteOfDay values (429, 1810)
insert into TrafficLevelRelativeToMinuteOfDay values (430, 1436)
insert into TrafficLevelRelativeToMinuteOfDay values (431, 1330)
insert into TrafficLevelRelativeToMinuteOfDay values (432, 1269)
insert into TrafficLevelRelativeToMinuteOfDay values (433, 1231)
insert into TrafficLevelRelativeToMinuteOfDay values (434, 1234)
insert into TrafficLevelRelativeToMinuteOfDay values (435, 1251)
insert into TrafficLevelRelativeToMinuteOfDay values (436, 1250)
insert into TrafficLevelRelativeToMinuteOfDay values (437, 1273)
insert into TrafficLevelRelativeToMinuteOfDay values (438, 1309)
insert into TrafficLevelRelativeToMinuteOfDay values (439, 1321)
insert into TrafficLevelRelativeToMinuteOfDay values (440, 1326)
insert into TrafficLevelRelativeToMinuteOfDay values (441, 1355)
insert into TrafficLevelRelativeToMinuteOfDay values (442, 1371)
insert into TrafficLevelRelativeToMinuteOfDay values (443, 1358)
insert into TrafficLevelRelativeToMinuteOfDay values (444, 1350)
insert into TrafficLevelRelativeToMinuteOfDay values (445, 1393)
insert into TrafficLevelRelativeToMinuteOfDay values (446, 1382)
insert into TrafficLevelRelativeToMinuteOfDay values (447, 1383)
insert into TrafficLevelRelativeToMinuteOfDay values (448, 1403)
insert into TrafficLevelRelativeToMinuteOfDay values (449, 1388)
insert into TrafficLevelRelativeToMinuteOfDay values (450, 1355)
insert into TrafficLevelRelativeToMinuteOfDay values (451, 1353)
insert into TrafficLevelRelativeToMinuteOfDay values (452, 1347)
insert into TrafficLevelRelativeToMinuteOfDay values (453, 1374)
insert into TrafficLevelRelativeToMinuteOfDay values (454, 1350)
insert into TrafficLevelRelativeToMinuteOfDay values (455, 1385)
insert into TrafficLevelRelativeToMinuteOfDay values (456, 1436)
insert into TrafficLevelRelativeToMinuteOfDay values (457, 1390)
insert into TrafficLevelRelativeToMinuteOfDay values (458, 1394)
insert into TrafficLevelRelativeToMinuteOfDay values (459, 1423)
insert into TrafficLevelRelativeToMinuteOfDay values (460, 1441)
insert into TrafficLevelRelativeToMinuteOfDay values (461, 1449)
insert into TrafficLevelRelativeToMinuteOfDay values (462, 1441)
insert into TrafficLevelRelativeToMinuteOfDay values (463, 1473)
insert into TrafficLevelRelativeToMinuteOfDay values (464, 1551)
insert into TrafficLevelRelativeToMinuteOfDay values (465, 1523)
insert into TrafficLevelRelativeToMinuteOfDay values (466, 1467)
insert into TrafficLevelRelativeToMinuteOfDay values (467, 1477)
insert into TrafficLevelRelativeToMinuteOfDay values (468, 1557)
insert into TrafficLevelRelativeToMinuteOfDay values (469, 1538)
insert into TrafficLevelRelativeToMinuteOfDay values (470, 1560)
insert into TrafficLevelRelativeToMinuteOfDay values (471, 1599)
insert into TrafficLevelRelativeToMinuteOfDay values (472, 1626)
insert into TrafficLevelRelativeToMinuteOfDay values (473, 1663)
insert into TrafficLevelRelativeToMinuteOfDay values (474, 1685)
insert into TrafficLevelRelativeToMinuteOfDay values (475, 1707)
insert into TrafficLevelRelativeToMinuteOfDay values (476, 1685)
insert into TrafficLevelRelativeToMinuteOfDay values (477, 1676)
insert into TrafficLevelRelativeToMinuteOfDay values (478, 1699)
insert into TrafficLevelRelativeToMinuteOfDay values (479, 1686)
insert into TrafficLevelRelativeToMinuteOfDay values (480, 1693)
insert into TrafficLevelRelativeToMinuteOfDay values (481, 1690)
insert into TrafficLevelRelativeToMinuteOfDay values (482, 1725)
insert into TrafficLevelRelativeToMinuteOfDay values (483, 1742)
insert into TrafficLevelRelativeToMinuteOfDay values (484, 1835)
insert into TrafficLevelRelativeToMinuteOfDay values (485, 1881)
insert into TrafficLevelRelativeToMinuteOfDay values (486, 1873)
insert into TrafficLevelRelativeToMinuteOfDay values (487, 1835)
insert into TrafficLevelRelativeToMinuteOfDay values (488, 1824)
insert into TrafficLevelRelativeToMinuteOfDay values (489, 1900)
insert into TrafficLevelRelativeToMinuteOfDay values (490, 1930)
insert into TrafficLevelRelativeToMinuteOfDay values (491, 1903)
insert into TrafficLevelRelativeToMinuteOfDay values (492, 1892)
insert into TrafficLevelRelativeToMinuteOfDay values (493, 1939)
insert into TrafficLevelRelativeToMinuteOfDay values (494, 1981)
insert into TrafficLevelRelativeToMinuteOfDay values (495, 1969)
insert into TrafficLevelRelativeToMinuteOfDay values (496, 1978)
insert into TrafficLevelRelativeToMinuteOfDay values (497, 1957)
insert into TrafficLevelRelativeToMinuteOfDay values (498, 1921)
insert into TrafficLevelRelativeToMinuteOfDay values (499, 2000)
insert into TrafficLevelRelativeToMinuteOfDay values (500, 2023)
insert into TrafficLevelRelativeToMinuteOfDay values (501, 2046)
insert into TrafficLevelRelativeToMinuteOfDay values (502, 2069)
insert into TrafficLevelRelativeToMinuteOfDay values (503, 2076)
insert into TrafficLevelRelativeToMinuteOfDay values (504, 2125)
insert into TrafficLevelRelativeToMinuteOfDay values (505, 2207)
insert into TrafficLevelRelativeToMinuteOfDay values (506, 2136)
insert into TrafficLevelRelativeToMinuteOfDay values (507, 2174)
insert into TrafficLevelRelativeToMinuteOfDay values (508, 2229)
insert into TrafficLevelRelativeToMinuteOfDay values (509, 2221)
insert into TrafficLevelRelativeToMinuteOfDay values (510, 2263)
insert into TrafficLevelRelativeToMinuteOfDay values (511, 2313)
insert into TrafficLevelRelativeToMinuteOfDay values (512, 2353)
insert into TrafficLevelRelativeToMinuteOfDay values (513, 2366)
insert into TrafficLevelRelativeToMinuteOfDay values (514, 2394)
insert into TrafficLevelRelativeToMinuteOfDay values (515, 2339)
insert into TrafficLevelRelativeToMinuteOfDay values (516, 2388)
insert into TrafficLevelRelativeToMinuteOfDay values (517, 2435)
insert into TrafficLevelRelativeToMinuteOfDay values (518, 2535)
insert into TrafficLevelRelativeToMinuteOfDay values (519, 2616)
insert into TrafficLevelRelativeToMinuteOfDay values (520, 2604)
insert into TrafficLevelRelativeToMinuteOfDay values (521, 2582)
insert into TrafficLevelRelativeToMinuteOfDay values (522, 2562)
insert into TrafficLevelRelativeToMinuteOfDay values (523, 2573)
insert into TrafficLevelRelativeToMinuteOfDay values (524, 2648)
insert into TrafficLevelRelativeToMinuteOfDay values (525, 2717)
insert into TrafficLevelRelativeToMinuteOfDay values (526, 2721)
insert into TrafficLevelRelativeToMinuteOfDay values (527, 2723)
insert into TrafficLevelRelativeToMinuteOfDay values (528, 2737)
insert into TrafficLevelRelativeToMinuteOfDay values (529, 2706)
insert into TrafficLevelRelativeToMinuteOfDay values (530, 2705)
insert into TrafficLevelRelativeToMinuteOfDay values (531, 2794)
insert into TrafficLevelRelativeToMinuteOfDay values (532, 3039)
insert into TrafficLevelRelativeToMinuteOfDay values (533, 3075)
insert into TrafficLevelRelativeToMinuteOfDay values (534, 3082)
insert into TrafficLevelRelativeToMinuteOfDay values (535, 3061)
insert into TrafficLevelRelativeToMinuteOfDay values (536, 3162)
insert into TrafficLevelRelativeToMinuteOfDay values (537, 3223)
insert into TrafficLevelRelativeToMinuteOfDay values (538, 3215)
insert into TrafficLevelRelativeToMinuteOfDay values (539, 3129)
insert into TrafficLevelRelativeToMinuteOfDay values (540, 3131)
insert into TrafficLevelRelativeToMinuteOfDay values (541, 3229)
insert into TrafficLevelRelativeToMinuteOfDay values (542, 3265)
insert into TrafficLevelRelativeToMinuteOfDay values (543, 3404)
insert into TrafficLevelRelativeToMinuteOfDay values (544, 3510)
insert into TrafficLevelRelativeToMinuteOfDay values (545, 3525)
insert into TrafficLevelRelativeToMinuteOfDay values (546, 3460)
insert into TrafficLevelRelativeToMinuteOfDay values (547, 3462)
insert into TrafficLevelRelativeToMinuteOfDay values (548, 3626)
insert into TrafficLevelRelativeToMinuteOfDay values (549, 3651)
insert into TrafficLevelRelativeToMinuteOfDay values (550, 3745)
insert into TrafficLevelRelativeToMinuteOfDay values (551, 3806)
insert into TrafficLevelRelativeToMinuteOfDay values (552, 3844)
insert into TrafficLevelRelativeToMinuteOfDay values (553, 3918)
insert into TrafficLevelRelativeToMinuteOfDay values (554, 3847)
insert into TrafficLevelRelativeToMinuteOfDay values (555, 3853)
insert into TrafficLevelRelativeToMinuteOfDay values (556, 3858)
insert into TrafficLevelRelativeToMinuteOfDay values (557, 3894)
insert into TrafficLevelRelativeToMinuteOfDay values (558, 3946)
insert into TrafficLevelRelativeToMinuteOfDay values (559, 3973)
insert into TrafficLevelRelativeToMinuteOfDay values (560, 3904)
insert into TrafficLevelRelativeToMinuteOfDay values (561, 3849)
insert into TrafficLevelRelativeToMinuteOfDay values (562, 3797)
insert into TrafficLevelRelativeToMinuteOfDay values (563, 3928)
insert into TrafficLevelRelativeToMinuteOfDay values (564, 4044)
insert into TrafficLevelRelativeToMinuteOfDay values (565, 4059)
insert into TrafficLevelRelativeToMinuteOfDay values (566, 4114)
insert into TrafficLevelRelativeToMinuteOfDay values (567, 4100)
insert into TrafficLevelRelativeToMinuteOfDay values (568, 4060)
insert into TrafficLevelRelativeToMinuteOfDay values (569, 4094)
insert into TrafficLevelRelativeToMinuteOfDay values (570, 4096)
insert into TrafficLevelRelativeToMinuteOfDay values (571, 4167)
insert into TrafficLevelRelativeToMinuteOfDay values (572, 4215)
insert into TrafficLevelRelativeToMinuteOfDay values (573, 4240)
insert into TrafficLevelRelativeToMinuteOfDay values (574, 4307)
insert into TrafficLevelRelativeToMinuteOfDay values (575, 3996)
insert into TrafficLevelRelativeToMinuteOfDay values (576, 4066)
insert into TrafficLevelRelativeToMinuteOfDay values (577, 4005)
insert into TrafficLevelRelativeToMinuteOfDay values (578, 4100)
insert into TrafficLevelRelativeToMinuteOfDay values (579, 4102)
insert into TrafficLevelRelativeToMinuteOfDay values (580, 4116)
insert into TrafficLevelRelativeToMinuteOfDay values (581, 4218)
insert into TrafficLevelRelativeToMinuteOfDay values (582, 4205)
insert into TrafficLevelRelativeToMinuteOfDay values (583, 4050)
insert into TrafficLevelRelativeToMinuteOfDay values (584, 4050)
insert into TrafficLevelRelativeToMinuteOfDay values (585, 4005)
insert into TrafficLevelRelativeToMinuteOfDay values (586, 4050)
insert into TrafficLevelRelativeToMinuteOfDay values (587, 4142)
insert into TrafficLevelRelativeToMinuteOfDay values (588, 4214)
insert into TrafficLevelRelativeToMinuteOfDay values (589, 4291)
insert into TrafficLevelRelativeToMinuteOfDay values (590, 4229)
insert into TrafficLevelRelativeToMinuteOfDay values (591, 4156)
insert into TrafficLevelRelativeToMinuteOfDay values (592, 4267)
insert into TrafficLevelRelativeToMinuteOfDay values (593, 4392)
insert into TrafficLevelRelativeToMinuteOfDay values (594, 4426)
insert into TrafficLevelRelativeToMinuteOfDay values (595, 4312)
insert into TrafficLevelRelativeToMinuteOfDay values (596, 4348)
insert into TrafficLevelRelativeToMinuteOfDay values (597, 4407)
insert into TrafficLevelRelativeToMinuteOfDay values (598, 4378)
insert into TrafficLevelRelativeToMinuteOfDay values (599, 4333)
insert into TrafficLevelRelativeToMinuteOfDay values (600, 4339)
insert into TrafficLevelRelativeToMinuteOfDay values (601, 4451)
insert into TrafficLevelRelativeToMinuteOfDay values (602, 4553)
insert into TrafficLevelRelativeToMinuteOfDay values (603, 4483)
insert into TrafficLevelRelativeToMinuteOfDay values (604, 4431)
insert into TrafficLevelRelativeToMinuteOfDay values (605, 4472)
insert into TrafficLevelRelativeToMinuteOfDay values (606, 4554)
insert into TrafficLevelRelativeToMinuteOfDay values (607, 4511)
insert into TrafficLevelRelativeToMinuteOfDay values (608, 4559)
insert into TrafficLevelRelativeToMinuteOfDay values (609, 4631)
insert into TrafficLevelRelativeToMinuteOfDay values (610, 4656)
insert into TrafficLevelRelativeToMinuteOfDay values (611, 4646)
insert into TrafficLevelRelativeToMinuteOfDay values (612, 4580)
insert into TrafficLevelRelativeToMinuteOfDay values (613, 4574)
insert into TrafficLevelRelativeToMinuteOfDay values (614, 4619)
insert into TrafficLevelRelativeToMinuteOfDay values (615, 4706)
insert into TrafficLevelRelativeToMinuteOfDay values (616, 4752)
insert into TrafficLevelRelativeToMinuteOfDay values (617, 4719)
insert into TrafficLevelRelativeToMinuteOfDay values (618, 4768)
insert into TrafficLevelRelativeToMinuteOfDay values (619, 4830)
insert into TrafficLevelRelativeToMinuteOfDay values (620, 4755)
insert into TrafficLevelRelativeToMinuteOfDay values (621, 4769)
insert into TrafficLevelRelativeToMinuteOfDay values (622, 4786)
insert into TrafficLevelRelativeToMinuteOfDay values (623, 4827)
insert into TrafficLevelRelativeToMinuteOfDay values (624, 4803)
insert into TrafficLevelRelativeToMinuteOfDay values (625, 4740)
insert into TrafficLevelRelativeToMinuteOfDay values (626, 4797)
insert into TrafficLevelRelativeToMinuteOfDay values (627, 4837)
insert into TrafficLevelRelativeToMinuteOfDay values (628, 4779)
insert into TrafficLevelRelativeToMinuteOfDay values (629, 4796)
insert into TrafficLevelRelativeToMinuteOfDay values (630, 4923)
insert into TrafficLevelRelativeToMinuteOfDay values (631, 4869)
insert into TrafficLevelRelativeToMinuteOfDay values (632, 4828)
insert into TrafficLevelRelativeToMinuteOfDay values (633, 4903)
insert into TrafficLevelRelativeToMinuteOfDay values (634, 4917)
insert into TrafficLevelRelativeToMinuteOfDay values (635, 5027)
insert into TrafficLevelRelativeToMinuteOfDay values (636, 5010)
insert into TrafficLevelRelativeToMinuteOfDay values (637, 5015)
insert into TrafficLevelRelativeToMinuteOfDay values (638, 5050)
insert into TrafficLevelRelativeToMinuteOfDay values (639, 4983)
insert into TrafficLevelRelativeToMinuteOfDay values (640, 4948)
insert into TrafficLevelRelativeToMinuteOfDay values (641, 4938)
insert into TrafficLevelRelativeToMinuteOfDay values (642, 4933)
insert into TrafficLevelRelativeToMinuteOfDay values (643, 4948)
insert into TrafficLevelRelativeToMinuteOfDay values (644, 4999)
insert into TrafficLevelRelativeToMinuteOfDay values (645, 5038)
insert into TrafficLevelRelativeToMinuteOfDay values (646, 5103)
insert into TrafficLevelRelativeToMinuteOfDay values (647, 4956)
insert into TrafficLevelRelativeToMinuteOfDay values (648, 5030)
insert into TrafficLevelRelativeToMinuteOfDay values (649, 5006)
insert into TrafficLevelRelativeToMinuteOfDay values (650, 5003)
insert into TrafficLevelRelativeToMinuteOfDay values (651, 5087)
insert into TrafficLevelRelativeToMinuteOfDay values (652, 5094)
insert into TrafficLevelRelativeToMinuteOfDay values (653, 5274)
insert into TrafficLevelRelativeToMinuteOfDay values (654, 5481)
insert into TrafficLevelRelativeToMinuteOfDay values (655, 5400)
insert into TrafficLevelRelativeToMinuteOfDay values (656, 5325)
insert into TrafficLevelRelativeToMinuteOfDay values (657, 5306)
insert into TrafficLevelRelativeToMinuteOfDay values (658, 5302)
insert into TrafficLevelRelativeToMinuteOfDay values (659, 5364)
insert into TrafficLevelRelativeToMinuteOfDay values (660, 5304)
insert into TrafficLevelRelativeToMinuteOfDay values (661, 5296)
insert into TrafficLevelRelativeToMinuteOfDay values (662, 5233)
insert into TrafficLevelRelativeToMinuteOfDay values (663, 5282)
insert into TrafficLevelRelativeToMinuteOfDay values (664, 5424)
insert into TrafficLevelRelativeToMinuteOfDay values (665, 5461)
insert into TrafficLevelRelativeToMinuteOfDay values (666, 5406)
insert into TrafficLevelRelativeToMinuteOfDay values (667, 5453)
insert into TrafficLevelRelativeToMinuteOfDay values (668, 5425)
insert into TrafficLevelRelativeToMinuteOfDay values (669, 5428)
insert into TrafficLevelRelativeToMinuteOfDay values (670, 5478)
insert into TrafficLevelRelativeToMinuteOfDay values (671, 5364)
insert into TrafficLevelRelativeToMinuteOfDay values (672, 5366)
insert into TrafficLevelRelativeToMinuteOfDay values (673, 5345)
insert into TrafficLevelRelativeToMinuteOfDay values (674, 5295)
insert into TrafficLevelRelativeToMinuteOfDay values (675, 5339)
insert into TrafficLevelRelativeToMinuteOfDay values (676, 5333)
insert into TrafficLevelRelativeToMinuteOfDay values (677, 5363)
insert into TrafficLevelRelativeToMinuteOfDay values (678, 5460)
insert into TrafficLevelRelativeToMinuteOfDay values (679, 5444)
insert into TrafficLevelRelativeToMinuteOfDay values (680, 5515)
insert into TrafficLevelRelativeToMinuteOfDay values (681, 5570)
insert into TrafficLevelRelativeToMinuteOfDay values (682, 5471)
insert into TrafficLevelRelativeToMinuteOfDay values (683, 5436)
insert into TrafficLevelRelativeToMinuteOfDay values (684, 5495)
insert into TrafficLevelRelativeToMinuteOfDay values (685, 5570)
insert into TrafficLevelRelativeToMinuteOfDay values (686, 5578)
insert into TrafficLevelRelativeToMinuteOfDay values (687, 5530)
insert into TrafficLevelRelativeToMinuteOfDay values (688, 5610)
insert into TrafficLevelRelativeToMinuteOfDay values (689, 5582)
insert into TrafficLevelRelativeToMinuteOfDay values (690, 5561)
insert into TrafficLevelRelativeToMinuteOfDay values (691, 5570)
insert into TrafficLevelRelativeToMinuteOfDay values (692, 5478)
insert into TrafficLevelRelativeToMinuteOfDay values (693, 5477)
insert into TrafficLevelRelativeToMinuteOfDay values (694, 5582)
insert into TrafficLevelRelativeToMinuteOfDay values (695, 5521)
insert into TrafficLevelRelativeToMinuteOfDay values (696, 5736)
insert into TrafficLevelRelativeToMinuteOfDay values (697, 5801)
insert into TrafficLevelRelativeToMinuteOfDay values (698, 5736)
insert into TrafficLevelRelativeToMinuteOfDay values (699, 5768)
insert into TrafficLevelRelativeToMinuteOfDay values (700, 5659)
insert into TrafficLevelRelativeToMinuteOfDay values (701, 5666)
insert into TrafficLevelRelativeToMinuteOfDay values (702, 5718)
insert into TrafficLevelRelativeToMinuteOfDay values (703, 5747)
insert into TrafficLevelRelativeToMinuteOfDay values (704, 5851)
insert into TrafficLevelRelativeToMinuteOfDay values (705, 5782)
insert into TrafficLevelRelativeToMinuteOfDay values (706, 5674)
insert into TrafficLevelRelativeToMinuteOfDay values (707, 5798)
insert into TrafficLevelRelativeToMinuteOfDay values (708, 5768)
insert into TrafficLevelRelativeToMinuteOfDay values (709, 5895)
insert into TrafficLevelRelativeToMinuteOfDay values (710, 5901)
insert into TrafficLevelRelativeToMinuteOfDay values (711, 5873)
insert into TrafficLevelRelativeToMinuteOfDay values (712, 5867)
insert into TrafficLevelRelativeToMinuteOfDay values (713, 5876)
insert into TrafficLevelRelativeToMinuteOfDay values (714, 5986)
insert into TrafficLevelRelativeToMinuteOfDay values (715, 5935)
insert into TrafficLevelRelativeToMinuteOfDay values (716, 5980)
insert into TrafficLevelRelativeToMinuteOfDay values (717, 6015)
insert into TrafficLevelRelativeToMinuteOfDay values (718, 5859)
insert into TrafficLevelRelativeToMinuteOfDay values (719, 5855)
insert into TrafficLevelRelativeToMinuteOfDay values (720, 5784)
insert into TrafficLevelRelativeToMinuteOfDay values (721, 5822)
insert into TrafficLevelRelativeToMinuteOfDay values (722, 5821)
insert into TrafficLevelRelativeToMinuteOfDay values (723, 5890)
insert into TrafficLevelRelativeToMinuteOfDay values (724, 5910)
insert into TrafficLevelRelativeToMinuteOfDay values (725, 5908)
insert into TrafficLevelRelativeToMinuteOfDay values (726, 5913)
insert into TrafficLevelRelativeToMinuteOfDay values (727, 6008)
insert into TrafficLevelRelativeToMinuteOfDay values (728, 6030)
insert into TrafficLevelRelativeToMinuteOfDay values (729, 5989)
insert into TrafficLevelRelativeToMinuteOfDay values (730, 6030)
insert into TrafficLevelRelativeToMinuteOfDay values (731, 5996)
insert into TrafficLevelRelativeToMinuteOfDay values (732, 6063)
insert into TrafficLevelRelativeToMinuteOfDay values (733, 6189)
insert into TrafficLevelRelativeToMinuteOfDay values (734, 6147)
insert into TrafficLevelRelativeToMinuteOfDay values (735, 6142)
insert into TrafficLevelRelativeToMinuteOfDay values (736, 6075)
insert into TrafficLevelRelativeToMinuteOfDay values (737, 6055)
insert into TrafficLevelRelativeToMinuteOfDay values (738, 6213)
insert into TrafficLevelRelativeToMinuteOfDay values (739, 6283)
insert into TrafficLevelRelativeToMinuteOfDay values (740, 6363)
insert into TrafficLevelRelativeToMinuteOfDay values (741, 6305)
insert into TrafficLevelRelativeToMinuteOfDay values (742, 6219)
insert into TrafficLevelRelativeToMinuteOfDay values (743, 6273)
insert into TrafficLevelRelativeToMinuteOfDay values (744, 6201)
insert into TrafficLevelRelativeToMinuteOfDay values (745, 6089)
insert into TrafficLevelRelativeToMinuteOfDay values (746, 6053)
insert into TrafficLevelRelativeToMinuteOfDay values (747, 5923)
insert into TrafficLevelRelativeToMinuteOfDay values (748, 6043)
insert into TrafficLevelRelativeToMinuteOfDay values (749, 6146)
insert into TrafficLevelRelativeToMinuteOfDay values (750, 6175)
insert into TrafficLevelRelativeToMinuteOfDay values (751, 6143)
insert into TrafficLevelRelativeToMinuteOfDay values (752, 6212)
insert into TrafficLevelRelativeToMinuteOfDay values (753, 6214)
insert into TrafficLevelRelativeToMinuteOfDay values (754, 6228)
insert into TrafficLevelRelativeToMinuteOfDay values (755, 6357)
insert into TrafficLevelRelativeToMinuteOfDay values (756, 6305)
insert into TrafficLevelRelativeToMinuteOfDay values (757, 6241)
insert into TrafficLevelRelativeToMinuteOfDay values (758, 6322)
insert into TrafficLevelRelativeToMinuteOfDay values (759, 6371)
insert into TrafficLevelRelativeToMinuteOfDay values (760, 6440)
insert into TrafficLevelRelativeToMinuteOfDay values (761, 6556)
insert into TrafficLevelRelativeToMinuteOfDay values (762, 6493)
insert into TrafficLevelRelativeToMinuteOfDay values (763, 6414)
insert into TrafficLevelRelativeToMinuteOfDay values (764, 6433)
insert into TrafficLevelRelativeToMinuteOfDay values (765, 6422)
insert into TrafficLevelRelativeToMinuteOfDay values (766, 6403)
insert into TrafficLevelRelativeToMinuteOfDay values (767, 6465)
insert into TrafficLevelRelativeToMinuteOfDay values (768, 6569)
insert into TrafficLevelRelativeToMinuteOfDay values (769, 6636)
insert into TrafficLevelRelativeToMinuteOfDay values (770, 6572)
insert into TrafficLevelRelativeToMinuteOfDay values (771, 6574)
insert into TrafficLevelRelativeToMinuteOfDay values (772, 6563)
insert into TrafficLevelRelativeToMinuteOfDay values (773, 6505)
insert into TrafficLevelRelativeToMinuteOfDay values (774, 6336)
insert into TrafficLevelRelativeToMinuteOfDay values (775, 6094)
insert into TrafficLevelRelativeToMinuteOfDay values (776, 6118)
insert into TrafficLevelRelativeToMinuteOfDay values (777, 6017)
insert into TrafficLevelRelativeToMinuteOfDay values (778, 6114)
insert into TrafficLevelRelativeToMinuteOfDay values (779, 6123)
insert into TrafficLevelRelativeToMinuteOfDay values (780, 6231)
insert into TrafficLevelRelativeToMinuteOfDay values (781, 6116)
insert into TrafficLevelRelativeToMinuteOfDay values (782, 6133)
insert into TrafficLevelRelativeToMinuteOfDay values (783, 6261)
insert into TrafficLevelRelativeToMinuteOfDay values (784, 6358)
insert into TrafficLevelRelativeToMinuteOfDay values (785, 6280)
insert into TrafficLevelRelativeToMinuteOfDay values (786, 6226)
insert into TrafficLevelRelativeToMinuteOfDay values (787, 6211)
insert into TrafficLevelRelativeToMinuteOfDay values (788, 6322)
insert into TrafficLevelRelativeToMinuteOfDay values (789, 6549)
insert into TrafficLevelRelativeToMinuteOfDay values (790, 6559)
insert into TrafficLevelRelativeToMinuteOfDay values (791, 6589)
insert into TrafficLevelRelativeToMinuteOfDay values (792, 6667)
insert into TrafficLevelRelativeToMinuteOfDay values (793, 6675)
insert into TrafficLevelRelativeToMinuteOfDay values (794, 6836)
insert into TrafficLevelRelativeToMinuteOfDay values (795, 6793)
insert into TrafficLevelRelativeToMinuteOfDay values (796, 6920)
insert into TrafficLevelRelativeToMinuteOfDay values (797, 6944)
insert into TrafficLevelRelativeToMinuteOfDay values (798, 6914)
insert into TrafficLevelRelativeToMinuteOfDay values (799, 6886)
insert into TrafficLevelRelativeToMinuteOfDay values (800, 6846)
insert into TrafficLevelRelativeToMinuteOfDay values (801, 6815)
insert into TrafficLevelRelativeToMinuteOfDay values (802, 6775)
insert into TrafficLevelRelativeToMinuteOfDay values (803, 6768)
insert into TrafficLevelRelativeToMinuteOfDay values (804, 6899)
insert into TrafficLevelRelativeToMinuteOfDay values (805, 6811)
insert into TrafficLevelRelativeToMinuteOfDay values (806, 6840)
insert into TrafficLevelRelativeToMinuteOfDay values (807, 6784)
insert into TrafficLevelRelativeToMinuteOfDay values (808, 6745)
insert into TrafficLevelRelativeToMinuteOfDay values (809, 6672)
insert into TrafficLevelRelativeToMinuteOfDay values (810, 6636)
insert into TrafficLevelRelativeToMinuteOfDay values (811, 6541)
insert into TrafficLevelRelativeToMinuteOfDay values (812, 6477)
insert into TrafficLevelRelativeToMinuteOfDay values (813, 6373)
insert into TrafficLevelRelativeToMinuteOfDay values (814, 6343)
insert into TrafficLevelRelativeToMinuteOfDay values (815, 6356)
insert into TrafficLevelRelativeToMinuteOfDay values (816, 6340)
insert into TrafficLevelRelativeToMinuteOfDay values (817, 6364)
insert into TrafficLevelRelativeToMinuteOfDay values (818, 6259)
insert into TrafficLevelRelativeToMinuteOfDay values (819, 6259)
insert into TrafficLevelRelativeToMinuteOfDay values (820, 6428)
insert into TrafficLevelRelativeToMinuteOfDay values (821, 6361)
insert into TrafficLevelRelativeToMinuteOfDay values (822, 6292)
insert into TrafficLevelRelativeToMinuteOfDay values (823, 6362)
insert into TrafficLevelRelativeToMinuteOfDay values (824, 6362)
insert into TrafficLevelRelativeToMinuteOfDay values (825, 6417)
insert into TrafficLevelRelativeToMinuteOfDay values (826, 6346)
insert into TrafficLevelRelativeToMinuteOfDay values (827, 6295)
insert into TrafficLevelRelativeToMinuteOfDay values (828, 6313)
insert into TrafficLevelRelativeToMinuteOfDay values (829, 6322)
insert into TrafficLevelRelativeToMinuteOfDay values (830, 6489)
insert into TrafficLevelRelativeToMinuteOfDay values (831, 6570)
insert into TrafficLevelRelativeToMinuteOfDay values (832, 6592)
insert into TrafficLevelRelativeToMinuteOfDay values (833, 6661)
insert into TrafficLevelRelativeToMinuteOfDay values (834, 6622)
insert into TrafficLevelRelativeToMinuteOfDay values (835, 6548)
insert into TrafficLevelRelativeToMinuteOfDay values (836, 6719)
insert into TrafficLevelRelativeToMinuteOfDay values (837, 6650)
insert into TrafficLevelRelativeToMinuteOfDay values (838, 6568)
insert into TrafficLevelRelativeToMinuteOfDay values (839, 6611)
insert into TrafficLevelRelativeToMinuteOfDay values (840, 6500)
insert into TrafficLevelRelativeToMinuteOfDay values (841, 6489)
insert into TrafficLevelRelativeToMinuteOfDay values (842, 6412)
insert into TrafficLevelRelativeToMinuteOfDay values (843, 6378)
insert into TrafficLevelRelativeToMinuteOfDay values (844, 6250)
insert into TrafficLevelRelativeToMinuteOfDay values (845, 6215)
insert into TrafficLevelRelativeToMinuteOfDay values (846, 6361)
insert into TrafficLevelRelativeToMinuteOfDay values (847, 6331)
insert into TrafficLevelRelativeToMinuteOfDay values (848, 6322)
insert into TrafficLevelRelativeToMinuteOfDay values (849, 6353)
insert into TrafficLevelRelativeToMinuteOfDay values (850, 6360)
insert into TrafficLevelRelativeToMinuteOfDay values (851, 6320)
insert into TrafficLevelRelativeToMinuteOfDay values (852, 6391)
insert into TrafficLevelRelativeToMinuteOfDay values (853, 6414)
insert into TrafficLevelRelativeToMinuteOfDay values (854, 6286)
insert into TrafficLevelRelativeToMinuteOfDay values (855, 6277)
insert into TrafficLevelRelativeToMinuteOfDay values (856, 6318)
insert into TrafficLevelRelativeToMinuteOfDay values (857, 6355)
insert into TrafficLevelRelativeToMinuteOfDay values (858, 6323)
insert into TrafficLevelRelativeToMinuteOfDay values (859, 6327)
insert into TrafficLevelRelativeToMinuteOfDay values (860, 6361)
insert into TrafficLevelRelativeToMinuteOfDay values (861, 6238)
insert into TrafficLevelRelativeToMinuteOfDay values (862, 6135)
insert into TrafficLevelRelativeToMinuteOfDay values (863, 6058)
insert into TrafficLevelRelativeToMinuteOfDay values (864, 5939)
insert into TrafficLevelRelativeToMinuteOfDay values (865, 5904)
insert into TrafficLevelRelativeToMinuteOfDay values (866, 5952)
insert into TrafficLevelRelativeToMinuteOfDay values (867, 6408)
insert into TrafficLevelRelativeToMinuteOfDay values (868, 6506)
insert into TrafficLevelRelativeToMinuteOfDay values (869, 6330)
insert into TrafficLevelRelativeToMinuteOfDay values (870, 6195)
insert into TrafficLevelRelativeToMinuteOfDay values (871, 6267)
insert into TrafficLevelRelativeToMinuteOfDay values (872, 6285)
insert into TrafficLevelRelativeToMinuteOfDay values (873, 6427)
insert into TrafficLevelRelativeToMinuteOfDay values (874, 6443)
insert into TrafficLevelRelativeToMinuteOfDay values (875, 6453)
insert into TrafficLevelRelativeToMinuteOfDay values (876, 6470)
insert into TrafficLevelRelativeToMinuteOfDay values (877, 6381)
insert into TrafficLevelRelativeToMinuteOfDay values (878, 6359)
insert into TrafficLevelRelativeToMinuteOfDay values (879, 6377)
insert into TrafficLevelRelativeToMinuteOfDay values (880, 6412)
insert into TrafficLevelRelativeToMinuteOfDay values (881, 6437)
insert into TrafficLevelRelativeToMinuteOfDay values (882, 6476)
insert into TrafficLevelRelativeToMinuteOfDay values (883, 6493)
insert into TrafficLevelRelativeToMinuteOfDay values (884, 6451)
insert into TrafficLevelRelativeToMinuteOfDay values (885, 6427)
insert into TrafficLevelRelativeToMinuteOfDay values (886, 6494)
insert into TrafficLevelRelativeToMinuteOfDay values (887, 6576)
insert into TrafficLevelRelativeToMinuteOfDay values (888, 6584)
insert into TrafficLevelRelativeToMinuteOfDay values (889, 6542)
insert into TrafficLevelRelativeToMinuteOfDay values (890, 6550)
insert into TrafficLevelRelativeToMinuteOfDay values (891, 6467)
insert into TrafficLevelRelativeToMinuteOfDay values (892, 6480)
insert into TrafficLevelRelativeToMinuteOfDay values (893, 6446)
insert into TrafficLevelRelativeToMinuteOfDay values (894, 6439)
insert into TrafficLevelRelativeToMinuteOfDay values (895, 6406)
insert into TrafficLevelRelativeToMinuteOfDay values (896, 6532)
insert into TrafficLevelRelativeToMinuteOfDay values (897, 6559)
insert into TrafficLevelRelativeToMinuteOfDay values (898, 6586)
insert into TrafficLevelRelativeToMinuteOfDay values (899, 6596)
insert into TrafficLevelRelativeToMinuteOfDay values (900, 6500)
insert into TrafficLevelRelativeToMinuteOfDay values (901, 6511)
insert into TrafficLevelRelativeToMinuteOfDay values (902, 6393)
insert into TrafficLevelRelativeToMinuteOfDay values (903, 6353)
insert into TrafficLevelRelativeToMinuteOfDay values (904, 6398)
insert into TrafficLevelRelativeToMinuteOfDay values (905, 6338)
insert into TrafficLevelRelativeToMinuteOfDay values (906, 6427)
insert into TrafficLevelRelativeToMinuteOfDay values (907, 6507)
insert into TrafficLevelRelativeToMinuteOfDay values (908, 6542)
insert into TrafficLevelRelativeToMinuteOfDay values (909, 6530)
insert into TrafficLevelRelativeToMinuteOfDay values (910, 6418)
insert into TrafficLevelRelativeToMinuteOfDay values (911, 6366)
insert into TrafficLevelRelativeToMinuteOfDay values (912, 6513)
insert into TrafficLevelRelativeToMinuteOfDay values (913, 6573)
insert into TrafficLevelRelativeToMinuteOfDay values (914, 6556)
insert into TrafficLevelRelativeToMinuteOfDay values (915, 6535)
insert into TrafficLevelRelativeToMinuteOfDay values (916, 6485)
insert into TrafficLevelRelativeToMinuteOfDay values (917, 6460)
insert into TrafficLevelRelativeToMinuteOfDay values (918, 6391)
insert into TrafficLevelRelativeToMinuteOfDay values (919, 6424)
insert into TrafficLevelRelativeToMinuteOfDay values (920, 6512)
insert into TrafficLevelRelativeToMinuteOfDay values (921, 6499)
insert into TrafficLevelRelativeToMinuteOfDay values (922, 6480)
insert into TrafficLevelRelativeToMinuteOfDay values (923, 6528)
insert into TrafficLevelRelativeToMinuteOfDay values (924, 6575)
insert into TrafficLevelRelativeToMinuteOfDay values (925, 6423)
insert into TrafficLevelRelativeToMinuteOfDay values (926, 6399)
insert into TrafficLevelRelativeToMinuteOfDay values (927, 6527)
insert into TrafficLevelRelativeToMinuteOfDay values (928, 6578)
insert into TrafficLevelRelativeToMinuteOfDay values (929, 6551)
insert into TrafficLevelRelativeToMinuteOfDay values (930, 6522)
insert into TrafficLevelRelativeToMinuteOfDay values (931, 6548)
insert into TrafficLevelRelativeToMinuteOfDay values (932, 6579)
insert into TrafficLevelRelativeToMinuteOfDay values (933, 6604)
insert into TrafficLevelRelativeToMinuteOfDay values (934, 6604)
insert into TrafficLevelRelativeToMinuteOfDay values (935, 6703)
insert into TrafficLevelRelativeToMinuteOfDay values (936, 6663)
insert into TrafficLevelRelativeToMinuteOfDay values (937, 6708)
insert into TrafficLevelRelativeToMinuteOfDay values (938, 6662)
insert into TrafficLevelRelativeToMinuteOfDay values (939, 6607)
insert into TrafficLevelRelativeToMinuteOfDay values (940, 6681)
insert into TrafficLevelRelativeToMinuteOfDay values (941, 6576)
insert into TrafficLevelRelativeToMinuteOfDay values (942, 6537)
insert into TrafficLevelRelativeToMinuteOfDay values (943, 6642)
insert into TrafficLevelRelativeToMinuteOfDay values (944, 6567)
insert into TrafficLevelRelativeToMinuteOfDay values (945, 6439)
insert into TrafficLevelRelativeToMinuteOfDay values (946, 6531)
insert into TrafficLevelRelativeToMinuteOfDay values (947, 6512)
insert into TrafficLevelRelativeToMinuteOfDay values (948, 6539)
insert into TrafficLevelRelativeToMinuteOfDay values (949, 6455)
insert into TrafficLevelRelativeToMinuteOfDay values (950, 6429)
insert into TrafficLevelRelativeToMinuteOfDay values (951, 6303)
insert into TrafficLevelRelativeToMinuteOfDay values (952, 6402)
insert into TrafficLevelRelativeToMinuteOfDay values (953, 6468)
insert into TrafficLevelRelativeToMinuteOfDay values (954, 6587)
insert into TrafficLevelRelativeToMinuteOfDay values (955, 6645)
insert into TrafficLevelRelativeToMinuteOfDay values (956, 6723)
insert into TrafficLevelRelativeToMinuteOfDay values (957, 6680)
insert into TrafficLevelRelativeToMinuteOfDay values (958, 6668)
insert into TrafficLevelRelativeToMinuteOfDay values (959, 6641)
insert into TrafficLevelRelativeToMinuteOfDay values (960, 6677)
insert into TrafficLevelRelativeToMinuteOfDay values (961, 6675)
insert into TrafficLevelRelativeToMinuteOfDay values (962, 6628)
insert into TrafficLevelRelativeToMinuteOfDay values (963, 6636)
insert into TrafficLevelRelativeToMinuteOfDay values (964, 6615)
insert into TrafficLevelRelativeToMinuteOfDay values (965, 6547)
insert into TrafficLevelRelativeToMinuteOfDay values (966, 6647)
insert into TrafficLevelRelativeToMinuteOfDay values (967, 6774)
insert into TrafficLevelRelativeToMinuteOfDay values (968, 6709)
insert into TrafficLevelRelativeToMinuteOfDay values (969, 6741)
insert into TrafficLevelRelativeToMinuteOfDay values (970, 6789)
insert into TrafficLevelRelativeToMinuteOfDay values (971, 6649)
insert into TrafficLevelRelativeToMinuteOfDay values (972, 6784)
insert into TrafficLevelRelativeToMinuteOfDay values (973, 6820)
insert into TrafficLevelRelativeToMinuteOfDay values (974, 6731)
insert into TrafficLevelRelativeToMinuteOfDay values (975, 6705)
insert into TrafficLevelRelativeToMinuteOfDay values (976, 6451)
insert into TrafficLevelRelativeToMinuteOfDay values (977, 6437)
insert into TrafficLevelRelativeToMinuteOfDay values (978, 6431)
insert into TrafficLevelRelativeToMinuteOfDay values (979, 6416)
insert into TrafficLevelRelativeToMinuteOfDay values (980, 6405)
insert into TrafficLevelRelativeToMinuteOfDay values (981, 6409)
insert into TrafficLevelRelativeToMinuteOfDay values (982, 6411)
insert into TrafficLevelRelativeToMinuteOfDay values (983, 6410)
insert into TrafficLevelRelativeToMinuteOfDay values (984, 6473)
insert into TrafficLevelRelativeToMinuteOfDay values (985, 6562)
insert into TrafficLevelRelativeToMinuteOfDay values (986, 6765)
insert into TrafficLevelRelativeToMinuteOfDay values (987, 6719)
insert into TrafficLevelRelativeToMinuteOfDay values (988, 6645)
insert into TrafficLevelRelativeToMinuteOfDay values (989, 6653)
insert into TrafficLevelRelativeToMinuteOfDay values (990, 6840)
insert into TrafficLevelRelativeToMinuteOfDay values (991, 7060)
insert into TrafficLevelRelativeToMinuteOfDay values (992, 6933)
insert into TrafficLevelRelativeToMinuteOfDay values (993, 6939)
insert into TrafficLevelRelativeToMinuteOfDay values (994, 6971)
insert into TrafficLevelRelativeToMinuteOfDay values (995, 7057)
insert into TrafficLevelRelativeToMinuteOfDay values (996, 7080)
insert into TrafficLevelRelativeToMinuteOfDay values (997, 7266)
insert into TrafficLevelRelativeToMinuteOfDay values (998, 7305)
insert into TrafficLevelRelativeToMinuteOfDay values (999, 7327)
insert into TrafficLevelRelativeToMinuteOfDay values (1000, 7300)
insert into TrafficLevelRelativeToMinuteOfDay values (1001, 7309)
insert into TrafficLevelRelativeToMinuteOfDay values (1002, 7382)
insert into TrafficLevelRelativeToMinuteOfDay values (1003, 7278)
insert into TrafficLevelRelativeToMinuteOfDay values (1004, 7187)
insert into TrafficLevelRelativeToMinuteOfDay values (1005, 7365)
insert into TrafficLevelRelativeToMinuteOfDay values (1006, 7403)
insert into TrafficLevelRelativeToMinuteOfDay values (1007, 7343)
insert into TrafficLevelRelativeToMinuteOfDay values (1008, 7273)
insert into TrafficLevelRelativeToMinuteOfDay values (1009, 7174)
insert into TrafficLevelRelativeToMinuteOfDay values (1010, 7155)
insert into TrafficLevelRelativeToMinuteOfDay values (1011, 7168)
insert into TrafficLevelRelativeToMinuteOfDay values (1012, 7225)
insert into TrafficLevelRelativeToMinuteOfDay values (1013, 7329)
insert into TrafficLevelRelativeToMinuteOfDay values (1014, 7296)
insert into TrafficLevelRelativeToMinuteOfDay values (1015, 7230)
insert into TrafficLevelRelativeToMinuteOfDay values (1016, 7242)
insert into TrafficLevelRelativeToMinuteOfDay values (1017, 7206)
insert into TrafficLevelRelativeToMinuteOfDay values (1018, 7133)
insert into TrafficLevelRelativeToMinuteOfDay values (1019, 7058)
insert into TrafficLevelRelativeToMinuteOfDay values (1020, 7031)
insert into TrafficLevelRelativeToMinuteOfDay values (1021, 6990)
insert into TrafficLevelRelativeToMinuteOfDay values (1022, 6957)
insert into TrafficLevelRelativeToMinuteOfDay values (1023, 6955)
insert into TrafficLevelRelativeToMinuteOfDay values (1024, 7064)
insert into TrafficLevelRelativeToMinuteOfDay values (1025, 7008)
insert into TrafficLevelRelativeToMinuteOfDay values (1026, 6956)
insert into TrafficLevelRelativeToMinuteOfDay values (1027, 6927)
insert into TrafficLevelRelativeToMinuteOfDay values (1028, 6902)
insert into TrafficLevelRelativeToMinuteOfDay values (1029, 6978)
insert into TrafficLevelRelativeToMinuteOfDay values (1030, 7083)
insert into TrafficLevelRelativeToMinuteOfDay values (1031, 7017)
insert into TrafficLevelRelativeToMinuteOfDay values (1032, 6998)
insert into TrafficLevelRelativeToMinuteOfDay values (1033, 6921)
insert into TrafficLevelRelativeToMinuteOfDay values (1034, 6878)
insert into TrafficLevelRelativeToMinuteOfDay values (1035, 6900)
insert into TrafficLevelRelativeToMinuteOfDay values (1036, 6855)
insert into TrafficLevelRelativeToMinuteOfDay values (1037, 6861)
insert into TrafficLevelRelativeToMinuteOfDay values (1038, 6829)
insert into TrafficLevelRelativeToMinuteOfDay values (1039, 7087)
insert into TrafficLevelRelativeToMinuteOfDay values (1040, 7055)
insert into TrafficLevelRelativeToMinuteOfDay values (1041, 6954)
insert into TrafficLevelRelativeToMinuteOfDay values (1042, 6944)
insert into TrafficLevelRelativeToMinuteOfDay values (1043, 7007)
insert into TrafficLevelRelativeToMinuteOfDay values (1044, 7083)
insert into TrafficLevelRelativeToMinuteOfDay values (1045, 7099)
insert into TrafficLevelRelativeToMinuteOfDay values (1046, 6992)
insert into TrafficLevelRelativeToMinuteOfDay values (1047, 6994)
insert into TrafficLevelRelativeToMinuteOfDay values (1048, 7110)
insert into TrafficLevelRelativeToMinuteOfDay values (1049, 7089)
insert into TrafficLevelRelativeToMinuteOfDay values (1050, 7079)
insert into TrafficLevelRelativeToMinuteOfDay values (1051, 7174)
insert into TrafficLevelRelativeToMinuteOfDay values (1052, 7200)
insert into TrafficLevelRelativeToMinuteOfDay values (1053, 7046)
insert into TrafficLevelRelativeToMinuteOfDay values (1054, 7101)
insert into TrafficLevelRelativeToMinuteOfDay values (1055, 7138)
insert into TrafficLevelRelativeToMinuteOfDay values (1056, 7053)
insert into TrafficLevelRelativeToMinuteOfDay values (1057, 7036)
insert into TrafficLevelRelativeToMinuteOfDay values (1058, 7114)
insert into TrafficLevelRelativeToMinuteOfDay values (1059, 7174)
insert into TrafficLevelRelativeToMinuteOfDay values (1060, 7207)
insert into TrafficLevelRelativeToMinuteOfDay values (1061, 7175)
insert into TrafficLevelRelativeToMinuteOfDay values (1062, 7098)
insert into TrafficLevelRelativeToMinuteOfDay values (1063, 7019)
insert into TrafficLevelRelativeToMinuteOfDay values (1064, 7060)
insert into TrafficLevelRelativeToMinuteOfDay values (1065, 6949)
insert into TrafficLevelRelativeToMinuteOfDay values (1066, 6932)
insert into TrafficLevelRelativeToMinuteOfDay values (1067, 6941)
insert into TrafficLevelRelativeToMinuteOfDay values (1068, 6896)
insert into TrafficLevelRelativeToMinuteOfDay values (1069, 6879)
insert into TrafficLevelRelativeToMinuteOfDay values (1070, 6832)
insert into TrafficLevelRelativeToMinuteOfDay values (1071, 6782)
insert into TrafficLevelRelativeToMinuteOfDay values (1072, 6616)
insert into TrafficLevelRelativeToMinuteOfDay values (1073, 6571)
insert into TrafficLevelRelativeToMinuteOfDay values (1074, 6526)
insert into TrafficLevelRelativeToMinuteOfDay values (1075, 6586)
insert into TrafficLevelRelativeToMinuteOfDay values (1076, 6654)
insert into TrafficLevelRelativeToMinuteOfDay values (1077, 6723)
insert into TrafficLevelRelativeToMinuteOfDay values (1078, 6527)
insert into TrafficLevelRelativeToMinuteOfDay values (1079, 6371)
insert into TrafficLevelRelativeToMinuteOfDay values (1080, 6237)
insert into TrafficLevelRelativeToMinuteOfDay values (1081, 6325)
insert into TrafficLevelRelativeToMinuteOfDay values (1082, 6424)
insert into TrafficLevelRelativeToMinuteOfDay values (1083, 6443)
insert into TrafficLevelRelativeToMinuteOfDay values (1084, 6442)
insert into TrafficLevelRelativeToMinuteOfDay values (1085, 6345)
insert into TrafficLevelRelativeToMinuteOfDay values (1086, 6385)
insert into TrafficLevelRelativeToMinuteOfDay values (1087, 6231)
insert into TrafficLevelRelativeToMinuteOfDay values (1088, 6306)
insert into TrafficLevelRelativeToMinuteOfDay values (1089, 6454)
insert into TrafficLevelRelativeToMinuteOfDay values (1090, 6395)
insert into TrafficLevelRelativeToMinuteOfDay values (1091, 6476)
insert into TrafficLevelRelativeToMinuteOfDay values (1092, 6580)
insert into TrafficLevelRelativeToMinuteOfDay values (1093, 6519)
insert into TrafficLevelRelativeToMinuteOfDay values (1094, 6638)
insert into TrafficLevelRelativeToMinuteOfDay values (1095, 6704)
insert into TrafficLevelRelativeToMinuteOfDay values (1096, 6589)
insert into TrafficLevelRelativeToMinuteOfDay values (1097, 6590)
insert into TrafficLevelRelativeToMinuteOfDay values (1098, 6654)
insert into TrafficLevelRelativeToMinuteOfDay values (1099, 6608)
insert into TrafficLevelRelativeToMinuteOfDay values (1100, 6563)
insert into TrafficLevelRelativeToMinuteOfDay values (1101, 6608)
insert into TrafficLevelRelativeToMinuteOfDay values (1102, 6691)
insert into TrafficLevelRelativeToMinuteOfDay values (1103, 6781)
insert into TrafficLevelRelativeToMinuteOfDay values (1104, 6784)
insert into TrafficLevelRelativeToMinuteOfDay values (1105, 6626)
insert into TrafficLevelRelativeToMinuteOfDay values (1106, 6685)
insert into TrafficLevelRelativeToMinuteOfDay values (1107, 6824)
insert into TrafficLevelRelativeToMinuteOfDay values (1108, 6832)
insert into TrafficLevelRelativeToMinuteOfDay values (1109, 6774)
insert into TrafficLevelRelativeToMinuteOfDay values (1110, 6714)
insert into TrafficLevelRelativeToMinuteOfDay values (1111, 6691)
insert into TrafficLevelRelativeToMinuteOfDay values (1112, 6808)
insert into TrafficLevelRelativeToMinuteOfDay values (1113, 6932)
insert into TrafficLevelRelativeToMinuteOfDay values (1114, 6793)
insert into TrafficLevelRelativeToMinuteOfDay values (1115, 6828)
insert into TrafficLevelRelativeToMinuteOfDay values (1116, 6885)
insert into TrafficLevelRelativeToMinuteOfDay values (1117, 6808)
insert into TrafficLevelRelativeToMinuteOfDay values (1118, 6946)
insert into TrafficLevelRelativeToMinuteOfDay values (1119, 6989)
insert into TrafficLevelRelativeToMinuteOfDay values (1120, 6922)
insert into TrafficLevelRelativeToMinuteOfDay values (1121, 6837)
insert into TrafficLevelRelativeToMinuteOfDay values (1122, 6766)
insert into TrafficLevelRelativeToMinuteOfDay values (1123, 6793)
insert into TrafficLevelRelativeToMinuteOfDay values (1124, 6786)
insert into TrafficLevelRelativeToMinuteOfDay values (1125, 7011)
insert into TrafficLevelRelativeToMinuteOfDay values (1126, 6956)
insert into TrafficLevelRelativeToMinuteOfDay values (1127, 6809)
insert into TrafficLevelRelativeToMinuteOfDay values (1128, 6720)
insert into TrafficLevelRelativeToMinuteOfDay values (1129, 6709)
insert into TrafficLevelRelativeToMinuteOfDay values (1130, 6865)
insert into TrafficLevelRelativeToMinuteOfDay values (1131, 6812)
insert into TrafficLevelRelativeToMinuteOfDay values (1132, 6844)
insert into TrafficLevelRelativeToMinuteOfDay values (1133, 6847)
insert into TrafficLevelRelativeToMinuteOfDay values (1134, 6834)
insert into TrafficLevelRelativeToMinuteOfDay values (1135, 6813)
insert into TrafficLevelRelativeToMinuteOfDay values (1136, 6778)
insert into TrafficLevelRelativeToMinuteOfDay values (1137, 6575)
insert into TrafficLevelRelativeToMinuteOfDay values (1138, 6671)
insert into TrafficLevelRelativeToMinuteOfDay values (1139, 6663)
insert into TrafficLevelRelativeToMinuteOfDay values (1140, 6648)
insert into TrafficLevelRelativeToMinuteOfDay values (1141, 6693)
insert into TrafficLevelRelativeToMinuteOfDay values (1142, 6785)
insert into TrafficLevelRelativeToMinuteOfDay values (1143, 6821)
insert into TrafficLevelRelativeToMinuteOfDay values (1144, 6907)
insert into TrafficLevelRelativeToMinuteOfDay values (1145, 6821)
insert into TrafficLevelRelativeToMinuteOfDay values (1146, 6786)
insert into TrafficLevelRelativeToMinuteOfDay values (1147, 6770)
insert into TrafficLevelRelativeToMinuteOfDay values (1148, 6773)
insert into TrafficLevelRelativeToMinuteOfDay values (1149, 6863)
insert into TrafficLevelRelativeToMinuteOfDay values (1150, 6902)
insert into TrafficLevelRelativeToMinuteOfDay values (1151, 6864)
insert into TrafficLevelRelativeToMinuteOfDay values (1152, 6915)
insert into TrafficLevelRelativeToMinuteOfDay values (1153, 6935)
insert into TrafficLevelRelativeToMinuteOfDay values (1154, 6995)
insert into TrafficLevelRelativeToMinuteOfDay values (1155, 6970)
insert into TrafficLevelRelativeToMinuteOfDay values (1156, 6997)
insert into TrafficLevelRelativeToMinuteOfDay values (1157, 6996)
insert into TrafficLevelRelativeToMinuteOfDay values (1158, 7077)
insert into TrafficLevelRelativeToMinuteOfDay values (1159, 7119)
insert into TrafficLevelRelativeToMinuteOfDay values (1160, 6936)
insert into TrafficLevelRelativeToMinuteOfDay values (1161, 6770)
insert into TrafficLevelRelativeToMinuteOfDay values (1162, 6817)
insert into TrafficLevelRelativeToMinuteOfDay values (1163, 6743)
insert into TrafficLevelRelativeToMinuteOfDay values (1164, 6749)
insert into TrafficLevelRelativeToMinuteOfDay values (1165, 6720)
insert into TrafficLevelRelativeToMinuteOfDay values (1166, 6578)
insert into TrafficLevelRelativeToMinuteOfDay values (1167, 6620)
insert into TrafficLevelRelativeToMinuteOfDay values (1168, 6661)
insert into TrafficLevelRelativeToMinuteOfDay values (1169, 6704)
insert into TrafficLevelRelativeToMinuteOfDay values (1170, 6598)
insert into TrafficLevelRelativeToMinuteOfDay values (1171, 6664)
insert into TrafficLevelRelativeToMinuteOfDay values (1172, 6667)
insert into TrafficLevelRelativeToMinuteOfDay values (1173, 6526)
insert into TrafficLevelRelativeToMinuteOfDay values (1174, 6652)
insert into TrafficLevelRelativeToMinuteOfDay values (1175, 6786)
insert into TrafficLevelRelativeToMinuteOfDay values (1176, 6760)
insert into TrafficLevelRelativeToMinuteOfDay values (1177, 6742)
insert into TrafficLevelRelativeToMinuteOfDay values (1178, 6708)
insert into TrafficLevelRelativeToMinuteOfDay values (1179, 6709)
insert into TrafficLevelRelativeToMinuteOfDay values (1180, 6643)
insert into TrafficLevelRelativeToMinuteOfDay values (1181, 6581)
insert into TrafficLevelRelativeToMinuteOfDay values (1182, 6623)
insert into TrafficLevelRelativeToMinuteOfDay values (1183, 6653)
insert into TrafficLevelRelativeToMinuteOfDay values (1184, 6614)
insert into TrafficLevelRelativeToMinuteOfDay values (1185, 6540)
insert into TrafficLevelRelativeToMinuteOfDay values (1186, 6603)
insert into TrafficLevelRelativeToMinuteOfDay values (1187, 6474)
insert into TrafficLevelRelativeToMinuteOfDay values (1188, 6450)
insert into TrafficLevelRelativeToMinuteOfDay values (1189, 6586)
insert into TrafficLevelRelativeToMinuteOfDay values (1190, 6564)
insert into TrafficLevelRelativeToMinuteOfDay values (1191, 6623)
insert into TrafficLevelRelativeToMinuteOfDay values (1192, 6572)
insert into TrafficLevelRelativeToMinuteOfDay values (1193, 6558)
insert into TrafficLevelRelativeToMinuteOfDay values (1194, 6558)
insert into TrafficLevelRelativeToMinuteOfDay values (1195, 6529)
insert into TrafficLevelRelativeToMinuteOfDay values (1196, 6477)
insert into TrafficLevelRelativeToMinuteOfDay values (1197, 6418)
insert into TrafficLevelRelativeToMinuteOfDay values (1198, 6450)
insert into TrafficLevelRelativeToMinuteOfDay values (1199, 6501)
insert into TrafficLevelRelativeToMinuteOfDay values (1200, 6439)
insert into TrafficLevelRelativeToMinuteOfDay values (1201, 6386)
insert into TrafficLevelRelativeToMinuteOfDay values (1202, 6384)
insert into TrafficLevelRelativeToMinuteOfDay values (1203, 6351)
insert into TrafficLevelRelativeToMinuteOfDay values (1204, 6240)
insert into TrafficLevelRelativeToMinuteOfDay values (1205, 6255)
insert into TrafficLevelRelativeToMinuteOfDay values (1206, 6324)
insert into TrafficLevelRelativeToMinuteOfDay values (1207, 6349)
insert into TrafficLevelRelativeToMinuteOfDay values (1208, 6473)
insert into TrafficLevelRelativeToMinuteOfDay values (1209, 6538)
insert into TrafficLevelRelativeToMinuteOfDay values (1210, 6524)
insert into TrafficLevelRelativeToMinuteOfDay values (1211, 6495)
insert into TrafficLevelRelativeToMinuteOfDay values (1212, 6506)
insert into TrafficLevelRelativeToMinuteOfDay values (1213, 6513)
insert into TrafficLevelRelativeToMinuteOfDay values (1214, 6487)
insert into TrafficLevelRelativeToMinuteOfDay values (1215, 6627)
insert into TrafficLevelRelativeToMinuteOfDay values (1216, 6576)
insert into TrafficLevelRelativeToMinuteOfDay values (1217, 6594)
insert into TrafficLevelRelativeToMinuteOfDay values (1218, 6533)
insert into TrafficLevelRelativeToMinuteOfDay values (1219, 6500)
insert into TrafficLevelRelativeToMinuteOfDay values (1220, 6618)
insert into TrafficLevelRelativeToMinuteOfDay values (1221, 6470)
insert into TrafficLevelRelativeToMinuteOfDay values (1222, 6468)
insert into TrafficLevelRelativeToMinuteOfDay values (1223, 6539)
insert into TrafficLevelRelativeToMinuteOfDay values (1224, 6490)
insert into TrafficLevelRelativeToMinuteOfDay values (1225, 6414)
insert into TrafficLevelRelativeToMinuteOfDay values (1226, 6470)
insert into TrafficLevelRelativeToMinuteOfDay values (1227, 6565)
insert into TrafficLevelRelativeToMinuteOfDay values (1228, 6459)
insert into TrafficLevelRelativeToMinuteOfDay values (1229, 6333)
insert into TrafficLevelRelativeToMinuteOfDay values (1230, 6334)
insert into TrafficLevelRelativeToMinuteOfDay values (1231, 6498)
insert into TrafficLevelRelativeToMinuteOfDay values (1232, 6583)
insert into TrafficLevelRelativeToMinuteOfDay values (1233, 6593)
insert into TrafficLevelRelativeToMinuteOfDay values (1234, 6537)
insert into TrafficLevelRelativeToMinuteOfDay values (1235, 6581)
insert into TrafficLevelRelativeToMinuteOfDay values (1236, 6585)
insert into TrafficLevelRelativeToMinuteOfDay values (1237, 6503)
insert into TrafficLevelRelativeToMinuteOfDay values (1238, 6450)
insert into TrafficLevelRelativeToMinuteOfDay values (1239, 6454)
insert into TrafficLevelRelativeToMinuteOfDay values (1240, 6469)
insert into TrafficLevelRelativeToMinuteOfDay values (1241, 6471)
insert into TrafficLevelRelativeToMinuteOfDay values (1242, 6514)
insert into TrafficLevelRelativeToMinuteOfDay values (1243, 6539)
insert into TrafficLevelRelativeToMinuteOfDay values (1244, 6582)
insert into TrafficLevelRelativeToMinuteOfDay values (1245, 6644)
insert into TrafficLevelRelativeToMinuteOfDay values (1246, 6503)
insert into TrafficLevelRelativeToMinuteOfDay values (1247, 6444)
insert into TrafficLevelRelativeToMinuteOfDay values (1248, 6443)
insert into TrafficLevelRelativeToMinuteOfDay values (1249, 6329)
insert into TrafficLevelRelativeToMinuteOfDay values (1250, 6145)
insert into TrafficLevelRelativeToMinuteOfDay values (1251, 6233)
insert into TrafficLevelRelativeToMinuteOfDay values (1252, 6325)
insert into TrafficLevelRelativeToMinuteOfDay values (1253, 6344)
insert into TrafficLevelRelativeToMinuteOfDay values (1254, 6314)
insert into TrafficLevelRelativeToMinuteOfDay values (1255, 6225)
insert into TrafficLevelRelativeToMinuteOfDay values (1256, 6226)
insert into TrafficLevelRelativeToMinuteOfDay values (1257, 6217)
insert into TrafficLevelRelativeToMinuteOfDay values (1258, 6153)
insert into TrafficLevelRelativeToMinuteOfDay values (1259, 6128)
insert into TrafficLevelRelativeToMinuteOfDay values (1260, 6542)
insert into TrafficLevelRelativeToMinuteOfDay values (1261, 6637)
insert into TrafficLevelRelativeToMinuteOfDay values (1262, 6740)
insert into TrafficLevelRelativeToMinuteOfDay values (1263, 6630)
insert into TrafficLevelRelativeToMinuteOfDay values (1264, 6682)
insert into TrafficLevelRelativeToMinuteOfDay values (1265, 6639)
insert into TrafficLevelRelativeToMinuteOfDay values (1266, 6632)
insert into TrafficLevelRelativeToMinuteOfDay values (1267, 6631)
insert into TrafficLevelRelativeToMinuteOfDay values (1268, 6650)
insert into TrafficLevelRelativeToMinuteOfDay values (1269, 6570)
insert into TrafficLevelRelativeToMinuteOfDay values (1270, 6594)
insert into TrafficLevelRelativeToMinuteOfDay values (1271, 6647)
insert into TrafficLevelRelativeToMinuteOfDay values (1272, 6572)
insert into TrafficLevelRelativeToMinuteOfDay values (1273, 6502)
insert into TrafficLevelRelativeToMinuteOfDay values (1274, 6497)
insert into TrafficLevelRelativeToMinuteOfDay values (1275, 6573)
insert into TrafficLevelRelativeToMinuteOfDay values (1276, 6509)
insert into TrafficLevelRelativeToMinuteOfDay values (1277, 6601)
insert into TrafficLevelRelativeToMinuteOfDay values (1278, 6585)
insert into TrafficLevelRelativeToMinuteOfDay values (1279, 6546)
insert into TrafficLevelRelativeToMinuteOfDay values (1280, 6600)
insert into TrafficLevelRelativeToMinuteOfDay values (1281, 6661)
insert into TrafficLevelRelativeToMinuteOfDay values (1282, 6648)
insert into TrafficLevelRelativeToMinuteOfDay values (1283, 6623)
insert into TrafficLevelRelativeToMinuteOfDay values (1284, 6576)
insert into TrafficLevelRelativeToMinuteOfDay values (1285, 6618)
insert into TrafficLevelRelativeToMinuteOfDay values (1286, 6542)
insert into TrafficLevelRelativeToMinuteOfDay values (1287, 6470)
insert into TrafficLevelRelativeToMinuteOfDay values (1288, 6383)
insert into TrafficLevelRelativeToMinuteOfDay values (1289, 6352)
insert into TrafficLevelRelativeToMinuteOfDay values (1290, 6335)
insert into TrafficLevelRelativeToMinuteOfDay values (1291, 6339)
insert into TrafficLevelRelativeToMinuteOfDay values (1292, 6371)
insert into TrafficLevelRelativeToMinuteOfDay values (1293, 6394)
insert into TrafficLevelRelativeToMinuteOfDay values (1294, 6382)
insert into TrafficLevelRelativeToMinuteOfDay values (1295, 6338)
insert into TrafficLevelRelativeToMinuteOfDay values (1296, 6384)
insert into TrafficLevelRelativeToMinuteOfDay values (1297, 6291)
insert into TrafficLevelRelativeToMinuteOfDay values (1298, 6292)
insert into TrafficLevelRelativeToMinuteOfDay values (1299, 6379)
insert into TrafficLevelRelativeToMinuteOfDay values (1300, 6405)
insert into TrafficLevelRelativeToMinuteOfDay values (1301, 6413)
insert into TrafficLevelRelativeToMinuteOfDay values (1302, 6439)
insert into TrafficLevelRelativeToMinuteOfDay values (1303, 6365)
insert into TrafficLevelRelativeToMinuteOfDay values (1304, 6381)
insert into TrafficLevelRelativeToMinuteOfDay values (1305, 6462)
insert into TrafficLevelRelativeToMinuteOfDay values (1306, 6466)
insert into TrafficLevelRelativeToMinuteOfDay values (1307, 6475)
insert into TrafficLevelRelativeToMinuteOfDay values (1308, 6497)
insert into TrafficLevelRelativeToMinuteOfDay values (1309, 6494)
insert into TrafficLevelRelativeToMinuteOfDay values (1310, 6400)
insert into TrafficLevelRelativeToMinuteOfDay values (1311, 6428)
insert into TrafficLevelRelativeToMinuteOfDay values (1312, 6442)
insert into TrafficLevelRelativeToMinuteOfDay values (1313, 6346)
insert into TrafficLevelRelativeToMinuteOfDay values (1314, 6346)
insert into TrafficLevelRelativeToMinuteOfDay values (1315, 6285)
insert into TrafficLevelRelativeToMinuteOfDay values (1316, 6195)
insert into TrafficLevelRelativeToMinuteOfDay values (1317, 6149)
insert into TrafficLevelRelativeToMinuteOfDay values (1318, 6128)
insert into TrafficLevelRelativeToMinuteOfDay values (1319, 6175)
insert into TrafficLevelRelativeToMinuteOfDay values (1320, 6193)
insert into TrafficLevelRelativeToMinuteOfDay values (1321, 6269)
insert into TrafficLevelRelativeToMinuteOfDay values (1322, 6274)
insert into TrafficLevelRelativeToMinuteOfDay values (1323, 6228)
insert into TrafficLevelRelativeToMinuteOfDay values (1324, 6259)
insert into TrafficLevelRelativeToMinuteOfDay values (1325, 6237)
insert into TrafficLevelRelativeToMinuteOfDay values (1326, 6160)
insert into TrafficLevelRelativeToMinuteOfDay values (1327, 6168)
insert into TrafficLevelRelativeToMinuteOfDay values (1328, 6181)
insert into TrafficLevelRelativeToMinuteOfDay values (1329, 6212)
insert into TrafficLevelRelativeToMinuteOfDay values (1330, 6334)
insert into TrafficLevelRelativeToMinuteOfDay values (1331, 6290)
insert into TrafficLevelRelativeToMinuteOfDay values (1332, 6379)
insert into TrafficLevelRelativeToMinuteOfDay values (1333, 6327)
insert into TrafficLevelRelativeToMinuteOfDay values (1334, 6323)
insert into TrafficLevelRelativeToMinuteOfDay values (1335, 6409)
insert into TrafficLevelRelativeToMinuteOfDay values (1336, 6406)
insert into TrafficLevelRelativeToMinuteOfDay values (1337, 6408)
insert into TrafficLevelRelativeToMinuteOfDay values (1338, 6517)
insert into TrafficLevelRelativeToMinuteOfDay values (1339, 6505)
insert into TrafficLevelRelativeToMinuteOfDay values (1340, 6386)
insert into TrafficLevelRelativeToMinuteOfDay values (1341, 6346)
insert into TrafficLevelRelativeToMinuteOfDay values (1342, 6299)
insert into TrafficLevelRelativeToMinuteOfDay values (1343, 6250)
insert into TrafficLevelRelativeToMinuteOfDay values (1344, 6216)
insert into TrafficLevelRelativeToMinuteOfDay values (1345, 6183)
insert into TrafficLevelRelativeToMinuteOfDay values (1346, 6162)
insert into TrafficLevelRelativeToMinuteOfDay values (1347, 6085)
insert into TrafficLevelRelativeToMinuteOfDay values (1348, 6090)
insert into TrafficLevelRelativeToMinuteOfDay values (1349, 6002)
insert into TrafficLevelRelativeToMinuteOfDay values (1350, 5972)
insert into TrafficLevelRelativeToMinuteOfDay values (1351, 5968)
insert into TrafficLevelRelativeToMinuteOfDay values (1352, 6139)
insert into TrafficLevelRelativeToMinuteOfDay values (1353, 6157)
insert into TrafficLevelRelativeToMinuteOfDay values (1354, 6134)
insert into TrafficLevelRelativeToMinuteOfDay values (1355, 6183)
insert into TrafficLevelRelativeToMinuteOfDay values (1356, 6084)
insert into TrafficLevelRelativeToMinuteOfDay values (1357, 6077)
insert into TrafficLevelRelativeToMinuteOfDay values (1358, 6076)
insert into TrafficLevelRelativeToMinuteOfDay values (1359, 5985)
insert into TrafficLevelRelativeToMinuteOfDay values (1360, 5992)
insert into TrafficLevelRelativeToMinuteOfDay values (1361, 6072)
insert into TrafficLevelRelativeToMinuteOfDay values (1362, 6044)
insert into TrafficLevelRelativeToMinuteOfDay values (1363, 5988)
insert into TrafficLevelRelativeToMinuteOfDay values (1364, 5930)
insert into TrafficLevelRelativeToMinuteOfDay values (1365, 5909)
insert into TrafficLevelRelativeToMinuteOfDay values (1366, 5885)
insert into TrafficLevelRelativeToMinuteOfDay values (1367, 5848)
insert into TrafficLevelRelativeToMinuteOfDay values (1368, 5864)
insert into TrafficLevelRelativeToMinuteOfDay values (1369, 5776)
insert into TrafficLevelRelativeToMinuteOfDay values (1370, 5773)
insert into TrafficLevelRelativeToMinuteOfDay values (1371, 5722)
insert into TrafficLevelRelativeToMinuteOfDay values (1372, 5698)
insert into TrafficLevelRelativeToMinuteOfDay values (1373, 5723)
insert into TrafficLevelRelativeToMinuteOfDay values (1374, 5704)
insert into TrafficLevelRelativeToMinuteOfDay values (1375, 5752)
insert into TrafficLevelRelativeToMinuteOfDay values (1376, 5733)
insert into TrafficLevelRelativeToMinuteOfDay values (1377, 5729)
insert into TrafficLevelRelativeToMinuteOfDay values (1378, 5753)
insert into TrafficLevelRelativeToMinuteOfDay values (1379, 5686)
insert into TrafficLevelRelativeToMinuteOfDay values (1380, 5627)
insert into TrafficLevelRelativeToMinuteOfDay values (1381, 5661)
insert into TrafficLevelRelativeToMinuteOfDay values (1382, 5686)
insert into TrafficLevelRelativeToMinuteOfDay values (1383, 5605)
insert into TrafficLevelRelativeToMinuteOfDay values (1384, 5554)
insert into TrafficLevelRelativeToMinuteOfDay values (1385, 5563)
insert into TrafficLevelRelativeToMinuteOfDay values (1386, 5575)
insert into TrafficLevelRelativeToMinuteOfDay values (1387, 5681)
insert into TrafficLevelRelativeToMinuteOfDay values (1388, 5717)
insert into TrafficLevelRelativeToMinuteOfDay values (1389, 5660)
insert into TrafficLevelRelativeToMinuteOfDay values (1390, 5634)
insert into TrafficLevelRelativeToMinuteOfDay values (1391, 5551)
insert into TrafficLevelRelativeToMinuteOfDay values (1392, 5519)
insert into TrafficLevelRelativeToMinuteOfDay values (1393, 5433)
insert into TrafficLevelRelativeToMinuteOfDay values (1394, 5926)
insert into TrafficLevelRelativeToMinuteOfDay values (1395, 5857)
insert into TrafficLevelRelativeToMinuteOfDay values (1396, 5829)
insert into TrafficLevelRelativeToMinuteOfDay values (1397, 5799)
insert into TrafficLevelRelativeToMinuteOfDay values (1398, 5793)
insert into TrafficLevelRelativeToMinuteOfDay values (1399, 5677)
insert into TrafficLevelRelativeToMinuteOfDay values (1400, 5671)
insert into TrafficLevelRelativeToMinuteOfDay values (1401, 5592)
insert into TrafficLevelRelativeToMinuteOfDay values (1402, 5534)
insert into TrafficLevelRelativeToMinuteOfDay values (1403, 5430)
insert into TrafficLevelRelativeToMinuteOfDay values (1404, 5061)
insert into TrafficLevelRelativeToMinuteOfDay values (1405, 5033)
insert into TrafficLevelRelativeToMinuteOfDay values (1406, 5069)
insert into TrafficLevelRelativeToMinuteOfDay values (1407, 5095)
insert into TrafficLevelRelativeToMinuteOfDay values (1408, 5052)
insert into TrafficLevelRelativeToMinuteOfDay values (1409, 5060)
insert into TrafficLevelRelativeToMinuteOfDay values (1410, 5017)
insert into TrafficLevelRelativeToMinuteOfDay values (1411, 4977)
insert into TrafficLevelRelativeToMinuteOfDay values (1412, 5055)
insert into TrafficLevelRelativeToMinuteOfDay values (1413, 5106)
insert into TrafficLevelRelativeToMinuteOfDay values (1414, 5011)
insert into TrafficLevelRelativeToMinuteOfDay values (1415, 4956)
insert into TrafficLevelRelativeToMinuteOfDay values (1416, 5016)
insert into TrafficLevelRelativeToMinuteOfDay values (1417, 5018)
insert into TrafficLevelRelativeToMinuteOfDay values (1418, 4868)
insert into TrafficLevelRelativeToMinuteOfDay values (1419, 4837)
insert into TrafficLevelRelativeToMinuteOfDay values (1420, 4884)
insert into TrafficLevelRelativeToMinuteOfDay values (1421, 4833)
insert into TrafficLevelRelativeToMinuteOfDay values (1422, 4778)
insert into TrafficLevelRelativeToMinuteOfDay values (1423, 4820)
insert into TrafficLevelRelativeToMinuteOfDay values (1424, 4791)
insert into TrafficLevelRelativeToMinuteOfDay values (1425, 4647)
insert into TrafficLevelRelativeToMinuteOfDay values (1426, 4667)
insert into TrafficLevelRelativeToMinuteOfDay values (1427, 4657)
insert into TrafficLevelRelativeToMinuteOfDay values (1428, 4660)
insert into TrafficLevelRelativeToMinuteOfDay values (1429, 4778)
insert into TrafficLevelRelativeToMinuteOfDay values (1430, 4753)
insert into TrafficLevelRelativeToMinuteOfDay values (1431, 4573)
insert into TrafficLevelRelativeToMinuteOfDay values (1432, 4587)
insert into TrafficLevelRelativeToMinuteOfDay values (1433, 4464)
insert into TrafficLevelRelativeToMinuteOfDay values (1434, 4451)
insert into TrafficLevelRelativeToMinuteOfDay values (1435, 4420)
insert into TrafficLevelRelativeToMinuteOfDay values (1436, 4411)
insert into TrafficLevelRelativeToMinuteOfDay values (1437, 4289)
insert into TrafficLevelRelativeToMinuteOfDay values (1438, 4317)
insert into TrafficLevelRelativeToMinuteOfDay values (1439, 4290)

GO

/*************************************************************************
	CREATE SCHEDULED TASK FOR CLEARING BannerIdentityTempData every day
**************************************************************************/

/****** Object:  Job [Clear BannerIdentity data]    Script Date: 06/04/2007 17:52:38 ******/
/*
BEGIN TRANSACTION
DECLARE @ReturnCode INT
SELECT @ReturnCode = 0
*/
/****** Object:  JobCategory [Database Maintenance]    Script Date: 06/04/2007 17:52:38 ******/
/*IF NOT EXISTS (SELECT name FROM msdb.dbo.syscategories WHERE name=N'Database Maintenance' AND category_class=1)
BEGIN
EXEC @ReturnCode = msdb.dbo.sp_add_category @class=N'JOB', @type=N'LOCAL', @name=N'Database Maintenance'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback

END

DECLARE @jobId BINARY(16)
EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name=N'Clear Banner Identity temp data', 
		@enabled=1, 
		@notify_level_eventlog=0, 
		@notify_level_email=0, 
		@notify_level_netsend=0, 
		@notify_level_page=0, 
		@delete_level=0, 
		@description=N'Every day sweep Banner Identity data into High Level data table', 
		@category_name=N'Database Maintenance', 
		@owner_login_name=N'DSIHOME\t.iles', @job_id = @jobId OUTPUT
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback*/
/****** Object:  Step [Do sweep]    Script Date: 06/04/2007 17:52:39 ******/
/*EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'Do sweep', 
		@step_id=1, 
		@cmdexec_success_code=0, 
		@on_success_action=1, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'declare @today datetime
set @today = getdate()
exec [BannerServer.Banner.ClearBannerIdentityTempDataFromBeforeTodayIntoHighLevelDataTable] @today', 
		@database_name=N'db_spotted', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'Perform Daily', 
		@enabled=1, 
		@freq_type=4, 
		@freq_interval=1, 
		@freq_subday_type=1, 
		@freq_subday_interval=0, 
		@freq_relative_interval=0, 
		@freq_recurrence_factor=0, 
		@active_start_date=20070604, 
		@active_end_date=99991231, 
		@active_start_time=300, 
		@active_end_time=235959
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'(local)'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
COMMIT TRANSACTION
GOTO EndSave
QuitWithRollback:
    IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION
EndSave:
GO

*/