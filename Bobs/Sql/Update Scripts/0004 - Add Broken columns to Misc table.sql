 /*example column existance check*/
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Misc' 
	AND	[column].name = 'BannerBroken'
) BEGIN
	/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
	BEGIN TRANSACTION
	SET QUOTED_IDENTIFIER ON
	SET ARITHABORT ON
	SET NUMERIC_ROUNDABORT OFF
	SET CONCAT_NULL_YIELDS_NULL ON
	SET ANSI_NULLS ON
	SET ANSI_PADDING ON
	SET ANSI_WARNINGS ON
	COMMIT
	BEGIN TRANSACTION
	ALTER TABLE dbo.Misc ADD
		BannerBroken bit NULL,
		BannerBrokenReason varchar(50) NULL
	DECLARE @v sql_variant 
	SET @v = N'Manual flag that admin may set to disable banner artwork'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Misc', N'COLUMN', N'BannerBroken'
	SET @v = N'String entered by admin to communicate to the promoter why the banner is broken'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Misc', N'COLUMN', N'BannerBrokenReason'
	COMMIT
END
