DECLARE @K INT SET @K = 4
DECLARE @SendTime DATETIME SET @SendTime = (SELECT DateTimeSend from MixmagIssue WHERE K=@K)
DECLARE @SendTimeNext DATETIME SET @SendTimeNext = CASE WHEN ((SELECT COUNT(*) from MixmagIssue WHERE K>@K)>0) THEN (SELECT TOP 1 DateTimeSend from MixmagIssue WHERE K>@K ORDER BY K) ELSE (GETDATE()) END

--select * from MixmagIssue
--select Summary, IssueCoverDate, DateTimeSend, TotalSent, (select) from MixmagIssue

--select Count(*) from MixmagSubscription where DateTimeCreated < (select DateTimeSend from MixmagIssue where K=10) and SendMixmag=1

select count(*) from MixmagSubscription where DateTimeCreated < @SendTime and SendMixmag=1 and IsAddressComplete = 1 and (IsEmailBroken=0 OR EmailBrokenDateTime > @SendTimeNext) 
select count(*) from MixmagSubscription where IsEmailBroken=1 and EmailBrokenDateTime > @SendTime and EmailBrokenDateTime < @SendTimeNext 

select Email, DateTimeCreated, (DATEADD(ms, K*346 + 124546*@K, (SELECT DateTimeSend FROM MixmagIssue WHERE K=@K))) AS DateTimeEmailSent, FirstName, LastName, AddressFirstLine, AddressPostcode, (SELECT Name FROM Country WHERE Country.K = AddressCountryK) from MixmagSubscription where DateTimeCreated < @SendTime and SendMixmag=1 and IsAddressComplete = 1 and (IsEmailBroken=0 OR EmailBrokenDateTime > @SendTimeNext) ORDER BY K
select Email, DateTimeCreated, (DATEADD(ms, K*346 + 124546*@K, (SELECT DateTimeSend FROM MixmagIssue WHERE K=@K))) AS DateTimeEmailSent, EmailBrokenDateTime from MixmagSubscription where IsEmailBroken=1 and EmailBrokenDateTime > @SendTime and EmailBrokenDateTime < @SendTimeNext ORDER BY K

--select count(*) from MixmagSubscription where IsAddressComplete=0 OR IsAddressComplete IS NULL
