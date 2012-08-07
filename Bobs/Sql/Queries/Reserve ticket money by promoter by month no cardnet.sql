DECLARE @SnapshotStart DATETIME SET @SnapshotStart = '20090401'
DECLARE @SnapshotEnd DATETIME SET @SnapshotEnd = '20090501'

SELECT
	p.K,
	p.UrlName,
	SUM(t.Price) Val
INTO
	##totalFunds
FROM
	Promoter p
	INNER JOIN TicketRun tr ON p.K = tr.PromoterK
	INNER JOIN Ticket t ON t.TicketRunK = tr.K
	INNER JOIN TicketPromoterEvent tpe ON p.K = tpe.PromoterK AND tr.EventK = tpe.EventK
	LEFT JOIN Transfer ft ON tpe.FundsTransferK = ft.K
WHERE
	t.K >= 82
	AND t.Enabled = 1
	AND t.Quantity > 0
	AND t.BuyDateTime >= @SnapshotStart
	AND t.BuyDateTime < @SnapshotEnd
	AND
	(
		t.Cancelled = 0
		OR t.CancelledDateTime >= @SnapshotEnd
		OR (tpe.FundsReleased = 1 AND t.CancelledDateTime >= ft.DateTimeComplete)
	)
GROUP BY
	p.K,
	p.UrlName




SELECT
	p.K,
	p.UrlName,
	SUM(RefundTransfer.Amount) * -1.0 Val
INTO
	##ticketMoneyToPromoterBankAccounts
FROM
	Transfer PaymentTransfer 
	LEFT JOIN Transfer RefundTransfer ON PaymentTransfer.K = RefundTransfer.TransferRefundedK 
	LEFT JOIN Promoter p ON PaymentTransfer.PromoterK = p.K
WHERE
		PaymentTransfer.Type = 1		--Payment
	AND PaymentTransfer.Method = 5		--TicketSales
	AND PaymentTransfer.DateTimeCreated >= @SnapshotStart
	AND PaymentTransfer.DateTimeCreated < @SnapshotEnd
	AND PaymentTransfer.Notes LIKE '%System: Funds released from Event K=%' --Excludes dodgy ones? - e.g. K = 30436
	AND PaymentTransfer.Status = 2		--Success
	
	AND RefundTransfer.Type = 2			--Refund
	AND RefundTransfer.Method = 2		--BankTransfer
	AND RefundTransfer.Status = 2		--Success
GROUP BY
	p.K,
	p.UrlName


SELECT
	p.K,
	p.UrlName,
	SUM(it.Amount) val 
INTO
	##ticketMoneyAppliedToInvoices
FROM
	InvoiceTransfer it
	LEFT JOIN Transfer t ON it.TransferK = t.K 
	LEFT JOIN Promoter p ON p.K = t.PromoterK
WHERE
	t.Method = 5
	AND t.Type = 1
	AND t.Status = 2
	AND t.DateTimeCreated >= @SnapshotStart
	AND t.DateTimeCreated < @SnapshotEnd
GROUP BY
	p.K,
	p.UrlName


SELECT 
	*, 
	ticketMoneyEarnt - ticketMoneyToPromoterBankAccounts - ticketMoneyAppliedToInvoices TicketMoneyReserve
FROM
(
	SELECT
		p.K,
		p.UrlName, 
		ISNULL(t4.val, 0) TicketMoneyEarnt, 
		ISNULL(t2.val, 0) TicketMoneyToPromoterBankAccounts, 
		ISNULL(t3.val, 0) TicketMoneyAppliedToInvoices 
	FROM
		Promoter p
		LEFT JOIN ##ticketMoneyToPromoterBankAccounts t2 ON t2.K = p.K
		LEFT JOIN ##ticketMoneyAppliedToInvoices t3 ON t3.K = p.K
		LEFT JOIN ##totalFunds t4 ON t4.K = p.K
	WHERE
		t2.Val is NOT null OR t3.Val is NOT null OR t4.Val is NOT null 
) 
AS t

SELECT
	SUM(Total) as Total, 
	sum(TicketMoneyToPromoterBankAccounts) as TicketMoneyToPromoterBankAccounts, 
	sum(TicketMoneyAppliedToInvoices) as TicketMoneyAppliedToInvoices, 
	sum(Total - TicketMoneyToPromoterBankAccounts - TicketMoneyAppliedToInvoices ) as TicketMoneyReserve 
FROM 
(
	SELECT
		p.K, 
		ISNULL(t4.val, 0) Total, 
		ISNULL(t2.val, 0) TicketMoneyToPromoterBankAccounts, 
		ISNULL(t3.val, 0) TicketMoneyAppliedToInvoices 
	FROM
		Promoter p
		LEFT JOIN ##ticketMoneyToPromoterBankAccounts t2 ON t2.K = p.K
		LEFT JOIN ##ticketMoneyAppliedToInvoices t3 ON t3.K = p.K
		LEFT JOIN ##totalFunds t4 ON t4.K = p.K
	WHERE
		t2.Val is NOT null OR t3.Val is NOT null OR t4.Val is NOT null 
)
AS t

 DROP Table ##ticketMoneyToPromoterBankAccounts
 DROP Table ##ticketMoneyAppliedToInvoices
 DROP Table ##totalFunds

/*
	Ticket money earnt
	Ticket money earnt with cardnet delay
	Ticket money to promoter bank accounts
	Ticket money applied to invoices
	Ticket money to be held in reserve
*/
