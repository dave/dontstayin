IF NOT EXISTS ( 

	SELECT * FROM sys.indexes indexes WHERE name = 'Index_Photo_ParentDateTime' 

)

BEGIN 

	CREATE NONCLUSTERED INDEX [Index_Photo_ParentDateTime] ON [dbo].[Photo] 
	(
		[ParentDateTime] DESC
	)
	WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]


END
