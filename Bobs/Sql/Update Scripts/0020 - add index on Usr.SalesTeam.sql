
IF NOT EXISTS ( 

	SELECT * FROM sys.indexes indexes WHERE name = '_dta_index_Usr_15_728389664__K144_1_10_11_12' 

)

BEGIN 

	CREATE NONCLUSTERED INDEX [_dta_index_Usr_15_728389664__K144_1_10_11_12] ON [dbo].[Usr] ( [SalesTeam] ASC )
	INCLUDE ( [K], [FirstName], [LastName], [NickName]) 
	WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY] 

END

