IF EXISTS(
	SELECT * FROM sys.tables WHERE Name = 'FacebookPost' 
) BEGIN
	drop table dbo.FacebookPost
END

GO

create TABLE dbo.FacebookPost
(
	K int NOT NULL IDENTITY (1, 1),
	DateTime datetime null,
	Type int null,
	UsrK int null,
	Content text null
)

ALTER TABLE dbo.FacebookPost ADD CONSTRAINT
	[PK_FacebookPost] PRIMARY KEY CLUSTERED 
	(
		[K]
	) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO


CREATE NONCLUSTERED INDEX [IX_FacebookPost_Type] ON [dbo].[FacebookPost] 
(
	[Type] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = OFF) ON [PRIMARY]

GO


EXECUTE sp_addextendedproperty N'MS_Description', N'Facebook posts', N'SCHEMA', N'dbo', N'TABLE', N'FacebookPost', NULL, NULL

EXECUTE sp_addextendedproperty N'MS_Description', N'Key', N'SCHEMA', N'dbo', N'TABLE', N'FacebookPost', N'COLUMN', N'K'
EXECUTE sp_addextendedproperty N'MS_Description', N'Date/time', N'SCHEMA', N'dbo', N'TABLE', N'FacebookPost', N'COLUMN', N'DateTime'
EXECUTE sp_addextendedproperty N'MS_Description', N'Type', N'SCHEMA', N'dbo', N'TABLE', N'FacebookPost', N'COLUMN', N'Type'
EXECUTE sp_addextendedproperty N'MS_Description', N'The connected Usr at the time', N'SCHEMA', N'dbo', N'TABLE', N'FacebookPost', N'COLUMN', N'UsrK'
EXECUTE sp_addextendedproperty N'MS_Description', N'Content data in XML', N'SCHEMA', N'dbo', N'TABLE', N'FacebookPost', N'COLUMN', N'Content'

EXECUTE sp_AddExtendedProperty N'EnumProperty', N'TypeEnum', N'SCHEMA', N'dbo', N'TABLE', N'FacebookPost', N'COLUMN', N'Type'

GO

