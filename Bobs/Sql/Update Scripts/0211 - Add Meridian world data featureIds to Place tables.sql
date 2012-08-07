
--IF NOT EXISTS (SELECT * FROM MeridianWorldData.sys.indexes i WHERE Name = 'IX_Feature_CountryCode_Name') BEGIN
--	CREATE NONCLUSTERED INDEX [IX_Feature_CountryCode_Name] ON MeridianWorldData.[dbo].[Feature] 
--	(
--		[CountryCode] ASC,
--		[Name] ASC
--	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = OFF) ON [PRIMARY]
--END
--GO

if not exists( select * from sys.tables t inner join sys.columns c on t.object_id = c.object_id where t.name  = 'Place' and c.name = 'MeridianFeatureId')
begin
	alter table Place add MeridianFeatureId int 
end
go

