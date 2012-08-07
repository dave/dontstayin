DECLARE @SnapshotDate DATETIME SET @SnapshotDate = '20090301'
--DECLARE @SnapshotDate DATETIME SET @SnapshotDate = GETDATE()

select * from (
select 
Invoice.K as InvoiceK, 
Promoter.UrlName as Promoter, 
ISNULL(Invoice.Price, 0) as Price, 
ISNULL(Invoice.Vat, 0) as Vat, 
ISNULL(Invoice.Total, 0) as Total, 
ISNULL((select sum(InvoiceTransfer.Amount) from InvoiceTransfer inner join transfer on InvoiceTransfer.TransferK=Transfer.K where InvoiceK = Invoice.K and transfer.DateTimeComplete < @SnapshotDate), 0) as Paid, 
ISNULL((select sum(-InvoiceCredit.Amount) from InvoiceCredit inner join Invoice credit on InvoiceCredit.CreditInvoiceK=credit.K where InvoiceK = Invoice.K and credit.TaxDateTime < @SnapshotDate), 0) as Credited, 
Invoice.DueDateTime as Due, 
(select FirstName from Usr where K = Promoter.SalesUsrK) as Sales 
from Invoice inner join Promoter on Invoice.PromoterK = Promoter.K 
where Price > 0 
and TaxDateTime < @SnapshotDate 
and (Paid = 0 or PaidDateTime >= @SnapshotDate)
and not Invoice.k in (8531, 8986, 9121, 9123, 9146, 9772, 9857, 14626, 15162, 18915, 19283, 19284)
) data
WHERE Total <> Paid + Credited
order by Due
