BEGIN TRANSACTION T1

CREATE TABLE dbo.Tmp_GroupPhoto
	(
	K int NOT NULL IDENTITY (1, 1),
	[GroupK] [int] NOT NULL,
	[PhotoK] [int] NOT NULL,
	[Caption] [varchar](255) NULL,
	[DateTime] [datetime] NULL,
	[AddedByUsrK] [int] NULL,
	[ShowOnFrontPage] [bit] NULL
) ON [PRIMARY]

IF (@@ERROR <> 0) ROLLBACK TRANSACTION T1

SET IDENTITY_INSERT dbo.Tmp_GroupPhoto OFF
INSERT INTO dbo.Tmp_GroupPhoto (
	[GroupK],
	[PhotoK],
	[Caption],
	[DateTime],
	[AddedByUsrK],
	[ShowOnFrontPage])
		SELECT [GroupK],
	[PhotoK],
	[Caption],
	[DateTime],
	[AddedByUsrK],
	[ShowOnFrontPage]
	FROM dbo.GroupPhoto WITH (HOLDLOCK TABLOCKX)

IF (@@ERROR <> 0) ROLLBACK TRANSACTION T1

DROP TABLE dbo.GroupPhoto
IF (@@ERROR <> 0) ROLLBACK TRANSACTION T1
EXECUTE sp_rename N'dbo.Tmp_GroupPhoto', N'GroupPhoto', 'OBJECT' 
IF (@@ERROR <> 0) ROLLBACK TRANSACTION T1

GO
SET ANSI_PADDING OFF
GO
DECLARE @v sql_variant 
SET @v = N'Primary K - not clustered index'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GroupPhoto', N'COLUMN', N'K'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Link to Group table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GroupPhoto', @level2type=N'COLUMN',@level2name=N'GroupK'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Link to the Photo table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GroupPhoto', @level2type=N'COLUMN',@level2name=N'PhotoK'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Caption for the group homepage' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GroupPhoto', @level2type=N'COLUMN',@level2name=N'Caption'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'When was the photo added' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GroupPhoto', @level2type=N'COLUMN',@level2name=N'DateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Who added/modified the photo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GroupPhoto', @level2type=N'COLUMN',@level2name=N'AddedByUsrK'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Do we show this on the group front page?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GroupPhoto', @level2type=N'COLUMN',@level2name=N'ShowOnFrontPage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Top photos on the group front-pages' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GroupPhoto'
GO

ALTER TABLE dbo.GroupPhoto ADD CONSTRAINT
	PK_GroupPhoto PRIMARY KEY NONCLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

ALTER TABLE [dbo].[GroupPhoto] ADD  CONSTRAINT [IX_GroupPhoto] UNIQUE NONCLUSTERED 
(
	[GroupK] ASC,
	[PhotoK] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IX_GroupPhoto_1] ON [dbo].[GroupPhoto] 
(
	[GroupK] ASC,
	[DateTime] DESC,
	[ShowOnFrontPage] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

COMMIT TRANSACTION T1
