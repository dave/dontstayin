/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/

IF EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagEntry' 
	AND	[column].name = 'ImageUrl'
) BEGIN


ALTER TABLE dbo.MixmagEntry
	DROP COLUMN ImageUrl

END
