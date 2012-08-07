/* example column length check */
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] 
	WHERE [table].name = 'OutgoingSms' 
) BEGIN

/****** Object:  Table [dbo].[OutgoingSms]    Script Date: 04/16/2007 15:31:04 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[OutgoingSms]') AND type in (N'U'))
BEGIN
CREATE TABLE [OutgoingSms](
	[K] [int] IDENTITY(1,1) NOT NULL,
	[DateTime] [datetime] NULL,
	[Type] [int] NULL,
	[IncomingSmsK] [int] NULL,
	[PostString] [varchar](500) NULL,
	[Message] [varchar](400) NULL,
	[ErrorCode] [int] NULL,
	[ErrorText] [varchar](100) NULL,
	[SubmissionReference] [varchar](100) NULL,
	[ChargeType] [int] NULL,
	[Sent] [bit] NULL,
	[MobileK] [int] NULL,
	[ServiceType] [int] NULL,
 CONSTRAINT [PK_OutgoingSms] PRIMARY KEY CLUSTERED 
(
	[K] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END

SET ANSI_PADDING OFF

IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'OutgoingSms', N'COLUMN',N'K'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The primary key' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OutgoingSms', @level2type=N'COLUMN',@level2name=N'K'

IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'OutgoingSms', N'COLUMN',N'DateTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The datetime that the message was sent' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OutgoingSms', @level2type=N'COLUMN',@level2name=N'DateTime'

IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'OutgoingSms', N'COLUMN',N'Type'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type - the type of response' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OutgoingSms', @level2type=N'COLUMN',@level2name=N'Type'

IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'OutgoingSms', N'COLUMN',N'IncomingSmsK'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'If theis sms was triggered by an incoming sms, this is the link to the IncomingSms table.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OutgoingSms', @level2type=N'COLUMN',@level2name=N'IncomingSmsK'

IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'OutgoingSms', N'COLUMN',N'PostString'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'This is the full string that was posted to iTagg to send the message' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OutgoingSms', @level2type=N'COLUMN',@level2name=N'PostString'

IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'OutgoingSms', N'COLUMN',N'Message'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The message' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OutgoingSms', @level2type=N'COLUMN',@level2name=N'Message'

IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'OutgoingSms', N'COLUMN',N'ErrorCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Did the sms send OK? 0=OK, !0=Error' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OutgoingSms', @level2type=N'COLUMN',@level2name=N'ErrorCode'

IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'OutgoingSms', N'COLUMN',N'ErrorText'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Error string returned' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OutgoingSms', @level2type=N'COLUMN',@level2name=N'ErrorText'

IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'OutgoingSms', N'COLUMN',N'SubmissionReference'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Submission reference returned by the sms server' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OutgoingSms', @level2type=N'COLUMN',@level2name=N'SubmissionReference'

IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'OutgoingSms', N'COLUMN',N'ChargeType'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'How is this outgoing sms charged?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OutgoingSms', @level2type=N'COLUMN',@level2name=N'ChargeType'

IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'OutgoingSms', N'COLUMN',N'Sent'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Has the text been sent to the sms server properly?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OutgoingSms', @level2type=N'COLUMN',@level2name=N'Sent'

IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'OutgoingSms', N'COLUMN',N'MobileK'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The mobile that the message is being sent to' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OutgoingSms', @level2type=N'COLUMN',@level2name=N'MobileK'

IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'OutgoingSms', N'COLUMN',N'ServiceType'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Incoming type - Tonight or Pllay' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OutgoingSms', @level2type=N'COLUMN',@level2name=N'ServiceType'

IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'OutgoingSms', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Table where all outgoing sms''s are logged' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OutgoingSms'

END

