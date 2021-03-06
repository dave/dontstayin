IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Fiat500Entry')
BEGIN	 
	DROP TABLE dbo.Fiat500Entry
END
GO
	CREATE TABLE dbo.Fiat500Entry
		(
		K int NOT NULL IDENTITY (1, 1),
		UsrK int NULL,
		Submitted datetime NOT NULL,
		FirstName varchar(50) NOT NULL,
		LastName varchar(50) NOT NULL,
		MobileNumber varchar(50) NOT NULL,
		EmailAddress varchar(50) NOT NULL,
		HouseNumberAndStreetName varchar(50) NOT NULL,
		Town varchar(50) NOT NULL,
		City varchar(50) NOT NULL,
		County varchar(50) NOT NULL,
		PostCode varchar(50) NOT NULL,
		AcceptConditions bit NOT NULL,
		NumberOfKids INT NOT NULL,
		NotifyByEmail BIT NOT NULL,
		NotifyByPost BIT NOT NULL,
		NotifyByPhone BIT NOT NULL,
		NotifyBySms BIT NOT NULL,
		
		)  ON [PRIMARY]

	DECLARE @v sql_variant 
	SET @v = N'Entries for the Fiat 500 competition'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Fiat500Entry', NULL, NULL
	SET @v = N'Primary key'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Fiat500Entry', N'COLUMN', N'K'
	SET @v = N'K of Usr who filled out the form'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Fiat500Entry', N'COLUMN', N'UsrK'
	SET @v = N'server'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Fiat500Entry', N'COLUMN', N'Submitted'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Fiat500Entry', N'COLUMN', N'FirstName'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Fiat500Entry', N'COLUMN', N'LastName'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Fiat500Entry', N'COLUMN', N'MobileNumber'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Fiat500Entry', N'COLUMN', N'EmailAddress'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Fiat500Entry', N'COLUMN', N'HouseNumberAndStreetName'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Fiat500Entry', N'COLUMN', N'City'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Fiat500Entry', N'COLUMN', N'Town'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Fiat500Entry', N'COLUMN', N'County'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Fiat500Entry', N'COLUMN', N'PostCode'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Fiat500Entry', N'COLUMN', N'AcceptConditions'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Fiat500Entry', N'COLUMN', N'NumberOfKids'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Fiat500Entry', N'COLUMN', N'NotifyByEmail'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Fiat500Entry', N'COLUMN', N'NotifyByPost'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Fiat500Entry', N'COLUMN', N'NotifyBySms'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Fiat500Entry', N'COLUMN', N'NotifyByPhone'
	
	

	ALTER TABLE dbo.Fiat500Entry ADD CONSTRAINT
		PK_Fiat500Entry PRIMARY KEY CLUSTERED 
		(
		K
		) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

 
