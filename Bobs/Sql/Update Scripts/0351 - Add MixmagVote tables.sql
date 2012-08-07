	IF EXISTS(
		SELECT * FROM sys.tables WHERE Name = 'MixmagVote' 
	) BEGIN
		drop table dbo.MixmagVote
	END

	GO

	create TABLE dbo.MixmagVote
	(
		K int NOT NULL IDENTITY (1, 1),
		FacebookUID bigint not null,
		MixmagEntryK int,
		DateTime datetime
	)

	ALTER TABLE dbo.MixmagVote ADD CONSTRAINT
		[PK_MixmagVote] PRIMARY KEY CLUSTERED 
		(
			[K]
		) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

	GO

	ALTER TABLE dbo.MixmagVote ADD CONSTRAINT 
		[IX_MixmagVote] UNIQUE NONCLUSTERED 
		(
			[FacebookUID] ASC,
			[MixmagEntryK] ASC
		) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

	GO


	EXECUTE sp_addextendedproperty N'MS_Description', N'Votes in the Mixmag Vote system', N'SCHEMA', N'dbo', N'TABLE', N'MixmagVote', NULL, NULL

	EXECUTE sp_addextendedproperty N'MS_Description', N'Primary key',                                   N'SCHEMA', N'dbo', N'TABLE', N'MixmagVote', N'COLUMN', N'K'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Facebook user ID',                              N'SCHEMA', N'dbo', N'TABLE', N'MixmagVote', N'COLUMN', N'FacebookUID'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Entry K',                                       N'SCHEMA', N'dbo', N'TABLE', N'MixmagVote', N'COLUMN', N'MixmagEntryK' 
	EXECUTE sp_addextendedproperty N'MS_Description', N'When the vote was cast',						N'SCHEMA', N'dbo', N'TABLE', N'MixmagVote', N'COLUMN', N'DateTime'

	GO

	IF EXISTS(
		SELECT * FROM sys.tables WHERE Name = 'MixmagEntry' 
	) BEGIN
		drop table dbo.MixmagEntry
	END

	GO

	create TABLE dbo.MixmagEntry
	(
		K int NOT NULL IDENTITY (1, 1),
		MixmagCompK int not null,
		FacebookUid bigint,
		DateTime datetime,
		ImageUrl text
	)

	ALTER TABLE dbo.MixmagEntry ADD CONSTRAINT
		[PK_MixmagEntry] PRIMARY KEY CLUSTERED 
		(
			[K]
		) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

	GO



	EXECUTE sp_addextendedproperty N'MS_Description', N'Each entry into the Mixmag Vote competition has a record in here',        N'SCHEMA', N'dbo', N'TABLE', N'MixmagEntry', NULL, NULL

	EXECUTE sp_addextendedproperty N'MS_Description', N'Primary key',                                   N'SCHEMA', N'dbo', N'TABLE', N'MixmagEntry', N'COLUMN', N'K'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Comp K',                                        N'SCHEMA', N'dbo', N'TABLE', N'MixmagEntry', N'COLUMN', N'MixmagCompK'
	EXECUTE sp_addextendedproperty N'MS_Description', 
										  N'Facebook UID (can by null if votes come before the entry)', N'SCHEMA', N'dbo', N'TABLE', N'MixmagEntry', N'COLUMN', N'FacebookUid'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Date / time of the entry',                      N'SCHEMA', N'dbo', N'TABLE', N'MixmagEntry', N'COLUMN', N'DateTime'
	EXECUTE sp_addextendedproperty N'MS_Description', N'Url of the image entered',                      N'SCHEMA', N'dbo', N'TABLE', N'MixmagEntry', N'COLUMN', N'ImageUrl'

	GO








