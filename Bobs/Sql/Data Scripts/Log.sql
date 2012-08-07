
DELETE FROM [Log]
TRUNCATE TABLE [Log]
 
declare @Today datetime
set @Today = dateadd(day, 0, datediff(day, 0, getdate()))

declare @Date datetime

declare @Item int
set @Item = 1

while (@Item < 78)
begin
if (@Item in (2, 6, 48, 75, 76, 77))
begin
set @Date = dateadd(day, -30, @Today)
while (@Date < dateadd(day, 30, @Today))
begin
	if (datename(weekday, @Date) = 'Saturday')
		begin insert into [Log] (Item, Date, Count) values (@Item, @Date, 515314) end
	else if (datename(weekday, @Date) = 'Sunday')
		begin insert into [Log] (Item, Date, Count) values (@Item, @Date, 619932) end
	else if (datename(weekday, @Date) = 'Monday')
		begin insert into [Log] (Item, Date, Count) values (@Item, @Date, 873007) end
	else if (datename(weekday, @Date) = 'Tuesday')
		begin insert into [Log] (Item, Date, Count) values (@Item, @Date, 750108) end
	else if (datename(weekday, @Date) = 'Wednesday')
		begin insert into [Log] (Item, Date, Count) values (@Item, @Date, 678918) end
	else if (datename(weekday, @Date) = 'Thursday')
		begin insert into [Log] (Item, Date, Count) values (@Item, @Date, 710811) end
	else --Friday
		begin insert into [Log] (Item, Date, Count) values (@Item, @Date, 702525) end
	set @Date = dateadd(day, 1, @Date)
end
end
set @Item = @Item + 1
end
