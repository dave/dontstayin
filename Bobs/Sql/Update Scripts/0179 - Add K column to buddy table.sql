 
 --To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.

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
GO
CREATE TABLE dbo.Tmp_Buddy
	(
	K int NOT NULL IDENTITY (1, 1),
	UsrK int NOT NULL,
	BuddyUsrK int NOT NULL,
	FullBuddy bit NULL,
	LastPopupHoldOff datetime NULL,
	CanInvite bit NULL,
	CanBuddyInvite bit NULL,
	Denied bit NULL,
	BuddyFoundByMethod int NULL
	)  ON [PRIMARY]
GO
DECLARE @v sql_variant 
SET @v = N'02/05/2008 13:32:09'
EXECUTE sp_addextendedproperty N'DataScriptLastRun', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Buddy', NULL, NULL
SET @v = N'Links one user to another'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Buddy', NULL, NULL
GO
DECLARE @v sql_variant 
SET @v = N'Primary K - not clustered index'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Buddy', N'COLUMN', N'K'
GO
DECLARE @v sql_variant 
SET @v = N'The user that added the buddy'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Buddy', N'COLUMN', N'UsrK'
GO
DECLARE @v sql_variant 
SET @v = N'The buddy'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Buddy', N'COLUMN', N'BuddyUsrK'
GO
DECLARE @v sql_variant 
SET @v = N'Has the buddy added this user to his buddy list?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Buddy', N'COLUMN', N'FullBuddy'
GO
DECLARE @v sql_variant 
SET @v = N'Has the user asked not to be alerted by pop-up from this buddy? If so, this is set to the data/time that the request was made. For 15 mins pop-up alerts will not be sent.'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Buddy', N'COLUMN', N'LastPopupHoldOff'
GO
DECLARE @v sql_variant 
SET @v = N'Can BuddyUsr invite Buddy to threads?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Buddy', N'COLUMN', N'CanInvite'
GO
DECLARE @v sql_variant 
SET @v = N'Can Buddy invite BuddyUsr to threads?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Buddy', N'COLUMN', N'CanBuddyInvite'
GO
DECLARE @v sql_variant 
SET @v = N'Has this buddy request been denied?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Buddy', N'COLUMN', N'Denied'
GO
DECLARE @v sql_variant 
SET @v = N'0 = Nickname, 1 = Real Name, 2 = Email Address, 3 = Spotter Number'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Buddy', N'COLUMN', N'BuddyFoundByMethod'
GO
SET IDENTITY_INSERT dbo.Tmp_Buddy OFF
GO
INSERT INTO dbo.Tmp_Buddy (UsrK, BuddyUsrK, FullBuddy, LastPopupHoldOff, CanInvite, CanBuddyInvite, Denied, BuddyFoundByMethod)
		SELECT UsrK, BuddyUsrK, FullBuddy, LastPopupHoldOff, CanInvite, CanBuddyInvite, Denied, BuddyFoundByMethod FROM dbo.Buddy WITH (HOLDLOCK TABLOCKX)
GO
DROP TABLE dbo.Buddy
GO
EXECUTE sp_rename N'dbo.Tmp_Buddy', N'Buddy', 'OBJECT' 
GO
ALTER TABLE dbo.Buddy ADD CONSTRAINT
	PK_Buddy PRIMARY KEY NONCLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE CLUSTERED INDEX Buddy12 ON dbo.Buddy
	(
	UsrK
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Buddy ADD CONSTRAINT
	IX_Buddy UNIQUE NONCLUSTERED 
	(
	UsrK,
	BuddyUsrK
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX _dta_index_Buddy_7_1691153070__K1_K3_K2 ON dbo.Buddy
	(
	UsrK,
	FullBuddy,
	BuddyUsrK
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX _dta_index_Buddy_7_1691153070__K2_K3_K1_6 ON dbo.Buddy
	(
	BuddyUsrK,
	FullBuddy,
	UsrK
	) INCLUDE (CanBuddyInvite) 
 WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
COMMIT

  
