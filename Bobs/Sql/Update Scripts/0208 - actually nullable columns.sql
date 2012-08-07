IF EXISTS(select * FROM FN_LISTEXTENDEDPROPERTY('IsNotNull', 'SCHEMA', 'dbo', 'TABLE', 'Promoter', 'COLUMN', 'SalesStatusExpires')) BEGIN
	EXEC sys.sp_dropextendedproperty @name=N'IsNotNull' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promoter', @level2type=N'COLUMN',@level2name=N'SalesStatusExpires'
END

IF EXISTS(select * FROM FN_LISTEXTENDEDPROPERTY('IsNotNull', 'SCHEMA', 'dbo', 'TABLE', 'Photo', 'COLUMN', 'ProcessingStartDateTime')) BEGIN
	EXEC sys.sp_dropextendedproperty @name=N'IsNotNull' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Photo', @level2type=N'COLUMN',@level2name=N'ProcessingStartDateTime'
	EXEC sys.sp_dropextendedproperty @name=N'IsNotNull' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Photo', @level2type=N'COLUMN',@level2name=N'ProcessingLastChange'	
END




