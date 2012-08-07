BEGIN TRANSACTION T1

CREATE TABLE dbo.Tmp_UsrPhotoMe
	(
	K int NOT NULL IDENTITY (1, 1),
	[UsrK] [int] NOT NULL,
	[PhotoK] [int] NOT NULL
) ON [PRIMARY]

IF (@@ERROR <> 0) ROLLBACK TRANSACTION T1

SET IDENTITY_INSERT dbo.Tmp_UsrPhotoMe OFF
INSERT INTO dbo.Tmp_UsrPhotoMe (
	[UsrK],
	[PhotoK])
		SELECT [UsrK],
	[PhotoK]
	FROM dbo.UsrPhotoMe WITH (HOLDLOCK TABLOCKX)

IF (@@ERROR <> 0) ROLLBACK TRANSACTION T1
DROP TABLE dbo.UsrPhotoMe
IF (@@ERROR <> 0) ROLLBACK TRANSACTION T1
EXECUTE sp_rename N'dbo.Tmp_UsrPhotoMe', N'UsrPhotoMe', 'OBJECT' 
IF (@@ERROR <> 0) ROLLBACK TRANSACTION T1


SET ANSI_PADDING OFF
GO
DECLARE @v sql_variant 
SET @v = N'Primary K - not clustered index'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'UsrPhotoMe', N'COLUMN', N'K'
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Link to Usr table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsrPhotoMe', @level2type=N'COLUMN',@level2name=N'UsrK'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Link to the Photo table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsrPhotoMe', @level2type=N'COLUMN',@level2name=N'PhotoK'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Links a user to many photos (photos of me)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsrPhotoMe'
GO


ALTER TABLE dbo.UsrPhotoMe ADD CONSTRAINT
	PK_UsrPhotoMe PRIMARY KEY NONCLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO


ALTER TABLE dbo.UsrPhotoMe ADD CONSTRAINT [IX_UsrPhotoMe] UNIQUE NONCLUSTERED 
(
	[UsrK] ASC,
	[PhotoK] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [_dta_index_UsrPhotoMe_7_407672500__K2_K1] ON [dbo].[UsrPhotoMe] 
(
	[PhotoK] ASC,
	[UsrK] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

GO

CREATE CLUSTERED INDEX [UsrPhotoMe12] ON [dbo].[UsrPhotoMe] 
(
	[PhotoK] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

COMMIT TRANSACTION T1
