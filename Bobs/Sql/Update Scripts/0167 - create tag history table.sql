

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TagPhotoHistory]') AND type in (N'U')) BEGIN

DELETE FROM TagPhoto
DELETE FROM Tag
CREATE TABLE [dbo].[TagPhotoHistory](
	[K] [int] IDENTITY(1,1) NOT NULL,
	[TagPhotoK] [int] NOT NULL,
	[Action] [int] NOT NULL,
	[UsrK] [int] NOT NULL,
	[DateTime] [DateTime] NOT NULL,
 CONSTRAINT [PK_TagPhotoHistory] PRIMARY KEY CLUSTERED 
(
	[K] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TagPhotoHistory', @level2type=N'COLUMN',@level2name=N'K'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The tagPhoto that was edited' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TagPhotoHistory', @level2type=N'COLUMN',@level2name=N'TagPhotoK'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'What the person did' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TagPhotoHistory', @level2type=N'COLUMN',@level2name=N'Action'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The usr that did it' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TagPhotoHistory', @level2type=N'COLUMN',@level2name=N'UsrK'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'When they did it' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TagPhotoHistory', @level2type=N'COLUMN',@level2name=N'DateTime'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'History of actions on a tag photo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TagPhotoHistory'

ALTER TABLE dbo.TagPhoto
	DROP COLUMN UsrK, DateTime, DisabledByUsrK, DisabledDateTime, ReenabledByUsrK, ReenabledDateTime

END

