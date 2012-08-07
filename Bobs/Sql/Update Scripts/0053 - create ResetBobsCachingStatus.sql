

ALTER TABLE Setting drop constraint PK_Setting
alter table Setting alter column Name varchar(100) not null

ALTER TABLE dbo.Setting ADD CONSTRAINT
PK_Setting PRIMARY KEY CLUSTERED 
(
	Name
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]



go


update Setting set Value = cast(0 as bit) where Name = 'BobsCaching'
update Setting set Value = cast(1 as bit) where Name = 'WhenBobsCacheIsDisabledStillRunCacheDeletesAndDontRollbackTransactionOnError'
