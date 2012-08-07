IF NOT EXISTS ( 

	SELECT * FROM sys.indexes indexes WHERE name = '_dta_index_Photo_15_568389094__K39_K40_1_19_41_43_62_106' 

)

BEGIN 

	CREATE NONCLUSTERED INDEX [_dta_index_Photo_15_568389094__K39_K40_1_19_41_43_62_106] ON [dbo].[Photo] 
	(
		[PhotoOfWeek] ASC,
		[PhotoOfWeekDateTime] ASC
	)
	INCLUDE ( [K],
	[Icon],
	[PhotoOfWeekCaption],
	[ContentDisabled],
	[UrlFragment],
	[IsSonyK800i]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]


END
