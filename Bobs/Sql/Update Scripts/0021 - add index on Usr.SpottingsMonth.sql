IF NOT EXISTS ( 

	SELECT * FROM sys.indexes indexes WHERE name = 'Index_Usr_SpottingsMonth_desc' 

)

BEGIN 

	CREATE NONCLUSTERED INDEX [Index_Usr_SpottingsMonth_desc] ON [dbo].[Usr] ( [SpottingsMonth] DESC )
	INCLUDE ( [K], [SpottingsMonthRank], [NickName], [Pic], [IsAdmin], [DateTimeSignUp], [AdminLevel], [IsSpotter], [Donate1Icon], [Donate1Expire], [Donate2Icon], [Donate2Expire], [ChatMessageCount], [CommentCount], [BuddyCount], [EventCount], [IsProSpotter], [SpottingsTotal], [IsLoggedOn], [DateTimeLastPageRequest], [ExtraIcon], [ExtraExpire], [HasTicket], [LastTicketEventDateTime]) 
	WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY] 

END
