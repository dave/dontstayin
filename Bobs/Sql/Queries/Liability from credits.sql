DECLARE @MonthlyLiabilityReductionPercentage float SET @MonthlyLiabilityReductionPercentage = 50.0
DECLARE @AverageCreditDiscountPercentage float SET @AverageCreditDiscountPercentage = 40.0
--DECLARE @DateTimeNow datetime SET @DateTimeNow = getdate()
DECLARE @DateTimeNow datetime SET @DateTimeNow = '20120701'
--DECLARE @DateTimeNow datetime SET @DateTimeNow = '20081101'
--DECLARE @DateTimeNow datetime SET @DateTimeNow = '20081114'

--select sum(TotalCredits) TotalCredits, sum(Liability) Liability from (
select p.K, p.Name, 'http://www.dontstayin.com/promoters/' + p.urlname Url,
sum(c.credits) TotalCredits,
datediff(day, lastaction.actiondatetime, @DateTimeNow) / 30.0 MonthsSinceLastAction,
sum(c.credits) * POWER((1 - (@MonthlyLiabilityReductionPercentage / 100.0)), (datediff(day, lastaction.actiondatetime, @DateTimeNow)/30.0)) * (1 - (@AverageCreditDiscountPercentage / 100.0)) LiabilityInPounds
from promoter p inner join campaigncredit c on p.k = c.promoterk 
inner join 
(
	select pu.k, max(u.datetimelastaccess) datetimelastaccess From 
	(
		select p.k, p.primaryusrk usrk from promoter p union
		select p.k, pu.usrk from promoter p
		inner join promoterusr pu on p.k = pu.promoterk
	) pu
	inner join usr u on pu.usrk = u.k
	group by pu.k
) q1 on p.k = q1.k

left join banner b on c.k = b.refundcampaigncreditk
left join (select promoterk, max(actiondatetime) actiondatetime from campaigncredit where ActionDateTime < @DateTimeNow and enabled = 1 group by promoterk) lastaction on p.k = lastaction.promoterk

where c.ActionDateTime < @DateTimeNow and c.enabled = 1 and p.isagency = 0 and not p.k=1622 and not p.k=251 and not p.k = 700 
group  by p.k, p.name, 'http://www.dontstayin.com/promoters/' + p.urlname, q1.datetimelastaccess, lastaction.actiondatetime
having sum(c.credits)>0 
order by p.k
--) as ttt
