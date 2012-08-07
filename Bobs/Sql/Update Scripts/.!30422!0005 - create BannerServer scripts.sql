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
