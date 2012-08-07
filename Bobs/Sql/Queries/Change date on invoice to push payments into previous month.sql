
declare @newdate datetime
set @newdate = '20100228 12:00:00'
update transfer set DateTimeCreated=@newdate, DateTimeComplete=@newdate where K IN (XXXX)
update invoice set PaidDateTime=@newdate where K IN (XXXX)

