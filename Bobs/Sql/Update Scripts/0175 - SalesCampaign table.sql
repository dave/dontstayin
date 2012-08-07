IF EXISTS(
	SELECT * FROM sys.tables WHERE Name = 'SalesCampaign' 
) BEGIN
	drop table dbo.SalesCampaign
END

GO

create TABLE dbo.SalesCampaign
(
	K int identity(1,1) not null,
	UsrK int not null,
	Name varchar(100) not null,
	Description text,
	DateStart datetime,
	DateEnd datetime
)
ALTER TABLE dbo.SalesCampaign ADD CONSTRAINT
	PK_SalesCampaign PRIMARY KEY CLUSTERED 
	(
		K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

EXECUTE sp_addextendedproperty N'MS_Description', N'Sales efforts in a particular demographic', N'SCHEMA', N'dbo', N'TABLE', N'SalesCampaign', NULL, NULL

EXECUTE sp_addextendedproperty N'MS_Description', N'K', N'SCHEMA', N'dbo', N'TABLE', N'SalesCampaign', N'COLUMN', N'K'
EXECUTE sp_addextendedproperty N'MS_Description', N'User that added the sales campaign', N'SCHEMA', N'dbo', N'TABLE', N'SalesCampaign', N'COLUMN', N'UsrK'
EXECUTE sp_addextendedproperty N'MS_Description', N'Name to identify this sales campaign', N'SCHEMA', N'dbo', N'TABLE', N'SalesCampaign', N'COLUMN', N'Name'
EXECUTE sp_addextendedproperty N'MS_Description', N'Description', N'SCHEMA', N'dbo', N'TABLE', N'SalesCampaign', N'COLUMN', N'Description'
EXECUTE sp_addextendedproperty N'MS_Description', N'Approximate start date - used for ordering and relative duration', N'SCHEMA', N'dbo', N'TABLE', N'SalesCampaign', N'COLUMN', N'DateStart'
EXECUTE sp_addextendedproperty N'MS_Description', N'Approximate end date - used for ordering and relative duration', N'SCHEMA', N'dbo', N'TABLE', N'SalesCampaign', N'COLUMN', N'DateEnd'

GO

