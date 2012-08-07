
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BannerServer.Banner.RecalculateTrafficLevelRelativeToMinuteOfDay')
	BEGIN
		DROP  Procedure  [BannerServer.Banner.RecalculateTrafficLevelRelativeToMinuteOfDay]
	END

GO

create procedure [BannerServer.Banner.RecalculateTrafficLevelRelativeToMinuteOfDay] (@UseWeekStartingFromDate datetime)
as

create table #minutesInDay (minute int)
declare @minute int
set @minute = 0
while (@minute < 1440)
begin
	insert into #minutesInDay values (@minute)
	set @minute = @minute + 1
end

select m.minute, datediff(day, @UseWeekStartingFromDate, datetimestart) dayNumber,
sum(1.0 * pages / datediff(minute, datetimestart, datetimelast)) total
into #Results
from visit with (nolock) inner join #minutesInDay m on 
	(
		(
			m.minute
			between
				datediff(minute, dateadd(day, datediff(day, 0, datetimestart), 0), datetimestart) 
			and
				datediff(minute, dateadd(day, datediff(day, 0, datetimestart), 0), datetimelast) 
		)
		OR
		(
			datediff(day, datetimestart, datetimelast) = 1 and m.minute + 1440
			between
				datediff(minute, dateadd(day, datediff(day, 0, datetimestart), 0), datetimestart) 
			and
				datediff(minute, dateadd(day, datediff(day, 0, datetimestart), 0), datetimelast)
		)
	)
where datetimestart between @UseWeekStartingFromDate and dateadd(day, 7, @UseWeekStartingFromDate)
	and datediff(minute, datetimestart, datetimelast) > 0
group by datediff(day, @UseWeekStartingFromDate, datetimestart), m.minute

select m.minute, floor(sum (r.total))
from #minutesInDay m left join #Results r on m.minute = r.minute
group by m.minute
order by m.minute

drop table #minutesInDay
drop table #Results

go
