IF NOT EXISTS ( 

	SELECT * FROM sys.indexes indexes WHERE name = 'Index_Photo_EnabledByUsrK' 

)

BEGIN 

	CREATE NONCLUSTERED INDEX [Index_Photo_EnabledByUsrK] ON [dbo].[Photo] 
	(
		[EnabledByUsrK] ASC
	)
	WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]


END
