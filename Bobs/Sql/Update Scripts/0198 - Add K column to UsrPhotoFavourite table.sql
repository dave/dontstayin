BEGIN TRANSACTION T1

CREATE TABLE dbo.Tmp_UsrPhotoFavourite
	(
	K int NOT NULL IDENTITY (1, 1),
	[UsrK] [int] NOT NULL,
	[PhotoK] [int] NOT NULL
) ON [PRIMARY]

IF (@@ERROR <> 0) ROLLBACK TRANSACTION T1

SET IDENTITY_INSERT dbo.Tmp_UsrPhotoFavourite OFF
INSERT INTO dbo.Tmp_UsrPhotoFavourite (
	[UsrK],
	[PhotoK])
		SELECT [UsrK],
	[PhotoK]
	FROM dbo.UsrPhotoFavourite WITH (HOLDLOCK TABLOCKX)

IF (@@ERROR <> 0) ROLLBACK TRANSACTION T1
DROP TABLE dbo.UsrPhotoFavourite
IF (@@ERROR <> 0) ROLLBACK TRANSACTION T1
EXECUTE sp_rename N'dbo.Tmp_UsrPhotoFavourite', N'UsrPhotoFavourite', 'OBJECT' 
IF (@@ERROR <> 0) ROLLBACK TRANSACTION T1


SET ANSI_PADDING OFF
GO
DECLARE @v sql_variant 
SET @v = N'Primary K - not clustered index'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'UsrPhotoFavourite', N'COLUMN', N'K'
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Link to Usr table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsrPhotoFavourite', @level2type=N'COLUMN',@level2name=N'UsrK'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Link to the Photo table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsrPhotoFavourite', @level2type=N'COLUMN',@level2name=N'PhotoK'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Links a user to many photos (my favorite photos)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsrPhotoFavourite'
GO


ALTER TABLE dbo.UsrPhotoFavourite ADD CONSTRAINT
	PK_UsrPhotoFavourite PRIMARY KEY NONCLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UsrPhotoFavourite] ADD  CONSTRAINT [IX_UsrPhotoFavourite] UNIQUE NONCLUSTERED 
(
	[UsrK] ASC,
	[PhotoK] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

COMMIT TRANSACTION T1
