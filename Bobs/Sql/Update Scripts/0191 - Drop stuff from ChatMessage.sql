
IF EXISTS(SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id WHERE [table].name = 'ChatMessage' AND	[column].name = 'FromUsrK' )       BEGIN 
DROP STATISTICS [dbo].[ChatMessage].[_dta_stat_1739153241_3_1]
DROP STATISTICS [dbo].[ChatMessage].[_dta_stat_1739153241_2_3_1]
DROP STATISTICS [dbo].[ChatMessage].[_dta_stat_1739153241_3_5_1]
DROP STATISTICS [dbo].[ChatMessage].[_dta_stat_1739153241_3_2_5_1]
DROP INDEX [IX_ChatMessage_PrivateThreadK] ON [dbo].[ChatMessage] WITH ( ONLINE = OFF )
ALTER TABLE ChatMessage DROP COLUMN FromUsrK 
ALTER TABLE ChatMessage DROP COLUMN ToUsrK
ALTER TABLE ChatMessage DROP COLUMN IsLol
ALTER TABLE ChatMessage DROP COLUMN LolCommentK
ALTER TABLE ChatMessage DROP COLUMN IsPrivate
ALTER TABLE ChatMessage DROP COLUMN PrivateThreadK
END
