/* example column length check */
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] 
	WHERE [table].name = 'IncomingSms' 
) BEGIN

/****** Object:  Table [dbo].[IncomingSms]    Script Date: 04/16/2007 15:31:04 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[IncomingSms]') AND type in (N'U'))
BEGIN
CREATE TABLE [IncomingSms](
	[K] [int] IDENTITY(1,1) NOT NULL,
	[Message] [varchar](400) NULL,
	[DateTime] [datetime] NULL,
	[MobileK] [int] NULL,
	[ServiceType] [int] NULL,
 CONSTRAINT [PK_IncomingSms] PRIMARY KEY CLUSTERED 
(
	[K] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END

SET ANSI_PADDING OFF

IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'IncomingSms', N'COLUMN',N'K'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The primary key' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IncomingSms', @level2type=N'COLUMN',@level2name=N'K'

IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'IncomingSms', N'COLUMN',N'Message'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The text of the text message (should start "tonight")' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IncomingSms', @level2type=N'COLUMN',@level2name=N'Message'

IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'IncomingSms', N'COLUMN',N'DateTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DateTime that the message arrived' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IncomingSms', @level2type=N'COLUMN',@level2name=N'DateTime'

IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'IncomingSms', N'COLUMN',N'MobileK'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The mobile number that this sms came from.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IncomingSms', @level2type=N'COLUMN',@level2name=N'MobileK'

IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'IncomingSms', N'COLUMN',N'ServiceType'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Incoming type - Tonight or Pllay' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IncomingSms', @level2type=N'COLUMN',@level2name=N'ServiceType'

IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'IncomingSms', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Table where all incoming sms''s are logged' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IncomingSms'

END

