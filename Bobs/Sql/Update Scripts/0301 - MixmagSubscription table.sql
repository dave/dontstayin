	IF EXISTS(
		SELECT * FROM sys.tables WHERE Name = 'MixmagSubscription' 
	) BEGIN
		drop table dbo.MixmagSubscription
	END

	GO

	create TABLE dbo.MixmagSubscription
	(
		K int NOT NULL IDENTITY (1, 1),
		FacebookUID bigint not null,
		FacebookPermissionEmail bit,
		FacebookPermissionPublish bit,
		DateTimeCreated datetime,
		SendMixmag bit,
		PublishStoryOnRead bit,
		TotalSent int,
		TotalRead int
	)

	ALTER TABLE dbo.MixmagSubscription ADD CONSTRAINT
		[PK_MixmagSubscription] PRIMARY KEY CLUSTERED 
		(
			[K]
		) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

	GO

	ALTER TABLE dbo.MixmagSubscription ADD CONSTRAINT 
		[IX_MixmagSubscription] UNIQUE NONCLUSTERED 
		(
			[FacebookUID] ASC
		) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

	GO


	EXECUTE sp_addextendedproperty N'MS_Description', N'Subscribers to the Mixmag-by-email service', N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', NULL, NULL

	EXECUTE sp_addextendedproperty N'MS_Description', N'Primary key',                                   N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'K'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Facebook user ID',                              N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'FacebookUID'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Facebook email extended permission',            N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'FacebookPermissionEmail'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Facebook publish_stream extended permission',   N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'FacebookPermissionPublish'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Date / time the subscription was created',      N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'DateTimeCreated'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Do we send mixmag by email?',                   N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'SendMixmag'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Do we publish to their wall when they read?',   N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'PublishStoryOnRead'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Total number of issues we have sent this user', N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'TotalSent'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Total number of issues this user has opened',   N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'TotalRead'

	GO

	IF EXISTS(
		SELECT * FROM sys.tables WHERE Name = 'MixmagIssue' 
	) BEGIN
		drop table dbo.MixmagIssue
	END

	GO

	create TABLE dbo.MixmagIssue
	(
		K int NOT NULL IDENTITY (1, 1),
		CerosUrl varchar(255),
		Ready bit,
		DateTimeSend datetime,
		IssueCoverDate datetime,
		TotalSent int,
		TotalRead int,
		SendingNow bit,
		SendingFinished bit,
		LastSendDateTime datetime
	)

	ALTER TABLE dbo.MixmagIssue ADD CONSTRAINT
		[PK_MixmagIssue] PRIMARY KEY CLUSTERED 
		(
			[K]
		) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

	GO



	EXECUTE sp_addextendedproperty N'MS_Description', N'Each Mixmag issue has a record in here',        N'SCHEMA', N'dbo', N'TABLE', N'MixmagIssue', NULL, NULL

	EXECUTE sp_addextendedproperty N'MS_Description', N'Primary key',                                   N'SCHEMA', N'dbo', N'TABLE', N'MixmagIssue', N'COLUMN', N'K'
	EXECUTE sp_addextendedproperty N'MS_Description', N'URL of the Ceros content',                      N'SCHEMA', N'dbo', N'TABLE', N'MixmagIssue', N'COLUMN', N'CerosUrl'
	EXECUTE sp_addextendedproperty N'MS_Description', 
			N'If this is not set, the email won''t send and it won''t be available as a back-issue',    N'SCHEMA', N'dbo', N'TABLE', N'MixmagIssue', N'COLUMN', N'Ready'
	EXECUTE sp_addextendedproperty N'MS_Description', 
			N'When we''re queued to send the email, and make available as a back-issue',                N'SCHEMA', N'dbo', N'TABLE', N'MixmagIssue', N'COLUMN', N'DateTimeSend'
	EXECUTE sp_addextendedproperty N'MS_Description', 
			N'The date on the cover (usually the month after when it''s sent)',                         N'SCHEMA', N'dbo', N'TABLE', N'MixmagIssue', N'COLUMN', N'IssueCoverDate'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Total sends',                                   N'SCHEMA', N'dbo', N'TABLE', N'MixmagIssue', N'COLUMN', N'TotalSent'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Total reads',                                   N'SCHEMA', N'dbo', N'TABLE', N'MixmagIssue', N'COLUMN', N'TotalRead'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Is the issue currently sending?',               N'SCHEMA', N'dbo', N'TABLE', N'MixmagIssue', N'COLUMN', N'SendingNow'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Has the issue finished sending?',               N'SCHEMA', N'dbo', N'TABLE', N'MixmagIssue', N'COLUMN', N'SendingFinished'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Date / time of the last send',                  N'SCHEMA', N'dbo', N'TABLE', N'MixmagIssue', N'COLUMN', N'LastSendDateTime'

	GO










	IF EXISTS(
		SELECT * FROM sys.tables WHERE Name = 'MixmagRead' 
	) BEGIN
		drop table dbo.MixmagRead
	END

	GO

	create TABLE dbo.MixmagRead
	(
		MixmagSubscriberK int NOT NULL,
		MixmagIssueK int NOT NULL,
		DateTimeRead datetime,
		StoryPublished bit
	)

	ALTER TABLE dbo.MixmagRead ADD CONSTRAINT
		[PK_MixmagRead] PRIMARY KEY CLUSTERED 
		(
			[MixmagSubscriberK],
			[MixmagIssueK]
		) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

	GO

	EXECUTE sp_addextendedproperty N'MS_Description', N'Each read of an issue',                         N'SCHEMA', N'dbo', N'TABLE', N'MixmagRead', NULL, NULL

	EXECUTE sp_addextendedproperty N'MS_Description', N'Subscriber key',                                N'SCHEMA', N'dbo', N'TABLE', N'MixmagRead', N'COLUMN', N'MixmagSubscriberK'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Issue key',                                     N'SCHEMA', N'dbo', N'TABLE', N'MixmagRead', N'COLUMN', N'MixmagIssueK'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Date time of the read',                         N'SCHEMA', N'dbo', N'TABLE', N'MixmagRead', N'COLUMN', N'DateTimeRead'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Was a story published to Facebook?',            N'SCHEMA', N'dbo', N'TABLE', N'MixmagRead', N'COLUMN', N'StoryPublished'

	GO
