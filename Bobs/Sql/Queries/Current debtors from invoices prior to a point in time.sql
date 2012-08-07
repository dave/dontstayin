DECLARE @SnapshotDate DATETIME SET @SnapshotDate = '20090301'
--DECLARE @SnapshotDate DATETIME SET @SnapshotDate = GETDATE()

select 
Invoice.K as InvoiceK, 
UrlName as Promoter, 
Price as Price, 
Vat as Vat, 
Total as Total, 
(select sum(Amount) from InvoiceTransfer where InvoiceK = Invoice.K) as Paid, 
(select sum(-Amount) from InvoiceCredit where InvoiceK = Invoice.K) as Credited, 
DueDateTime as Due, 
(select FirstName from Usr where K = Promoter.SalesUsrK) as Sales 
from Invoice inner join Promoter on Invoice.PromoterK = Promoter.K 
where Price > 0 
and TaxDateTime < @SnapshotDate 
and Paid = 0 
and not Invoice.k in (8531, 8986, 9121, 9123, 9146, 9772, 9857, 14626, 15162, 18915, 19283, 19284)
order by DueDateTime
