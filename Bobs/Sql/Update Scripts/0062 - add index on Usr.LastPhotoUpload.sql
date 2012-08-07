IF NOT EXISTS ( 

	SELECT * FROM sys.indexes indexes WHERE name = 'Index_Usr_LastPhotoUpload' 

)

BEGIN 

	CREATE NONCLUSTERED INDEX [Index_Usr_LastPhotoUpload] ON [dbo].[Usr] 
	(
		[LastPhotoUpload] DESC
	)
	WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]


END
