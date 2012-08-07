if (exists(select * from sys.procedures where name = 'CacheTriggers.Trigger.ResetBobsCachingStatus'))
begin
drop proc [CacheTriggers.Trigger.ResetBobsCachingStatus]
end

go
