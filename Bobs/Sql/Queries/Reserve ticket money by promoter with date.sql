
--IF EXISTS( SELECT * FROM sys.tables [table] WHERE [table].name = '##ticketMoneyWithCardnetDelay' ) BEGIN DROP Table ##ticketMoneyWithCardnetDelay END
--IF EXISTS( SELECT * FROM sys.tables [table] WHERE [table].name = '##ticketMoneyToPromoterBankAccounts' ) BEGIN DROP Table ##ticketMoneyToPromoterBankAccounts END
--IF EXISTS( SELECT * FROM sys.tables [table] WHERE [table].name = '##ticketMoneyAppliedToInvoices' ) BEGIN DROP Table ##ticketMoneyAppliedToInvoices END
--IF EXISTS( SELECT * FROM sys.tables [table] WHERE [table].name = '##totalFunds' ) BEGIN DROP Table ##totalFunds END
-- DROP Table ##ticketMoneyWithCardnetDelay
-- DROP Table ##ticketMoneyToPromoterBankAccounts
-- DROP Table ##ticketMoneyAppliedToInvoices
-- DROP Table ##totalFunds

--DECLARE @SnapshotDate DATETIME SET @SnapshotDate = '20080601'
--DECLARE @SnapshotDate DATETIME SET @SnapshotDate = GETDATE()
DECLARE @SnapshotDate DATETIME SET @SnapshotDate = '20090401'
DECLARE @CardNetDate DATETIME SET @CardNetDate = DATEADD(day, CASE WHEN DATENAME(dw, @SnapshotDate) = 'Friday' THEN -13 WHEN DATENAME(dw, @SnapshotDate) = 'Saturday' THEN -14 ELSE -15 END, @SnapshotDate)
SET @CardNetDate = DATEADD(d, 0, DATEDIFF(d, 0, @CardNetDate)) 

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
	AND t.BuyDateTime < @SnapshotDate
	AND
	(
		t.Cancelled = 0
		OR t.CancelledDateTime >= @SnapshotDate
		OR (tpe.FundsReleased = 1 AND t.CancelledDateTime >= ft.DateTimeComplete)
	)
GROUP BY
	p.K,
	p.UrlName

--SELECT * FROM TicketPromoterEvent
--SELECT * FROM Transfer

--SELECT p.K, p.Name, SUM(ISNULL(t_ticketMoneyWithCardnetDelay.Price, 0)) as val
--INTO ##ticketMoneyWithCardnetDelay
--FROM [Ticket] t_ticketMoneyWithCardnetDelay
--LEFT JOIN TicketRun tr_ticketMoneyWithCardnetDelay ON tr_ticketMoneyWithCardnetDelay.K = t_ticketMoneyWithCardnetDelay.TicketRunK
--LEFT join Promoter p ON tr_ticketMoneyWithCardnetDelay.PromoterK = p.K
--WHERE t_ticketMoneyWithCardnetDelay.[Enabled] = 1 AND t_ticketMoneyWithCardnetDelay.K >= 82 AND t_ticketMoneyWithCardnetDelay.[Cancelled] = 0 AND t_ticketMoneyWithCardnetDelay.[BuyDateTime] < @CardNetDate
--GROUP BY p.k, p.Name

SELECT
	p.K,
	p.UrlName,
	SUM(t.Price) Val
INTO
	##ticketMoneyWithCardnetDelay
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
	AND t.BuyDateTime < @CardNetDate
	AND
	(
		t.Cancelled = 0
		OR t.CancelledDateTime >= @CardNetDate
		OR (tpe.FundsReleased = 1 AND t.CancelledDateTime >= ft.DateTimeComplete)
	)
GROUP BY p.K, p.UrlName



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
	AND PaymentTransfer.DateTimeCreated < @SnapshotDate
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
	AND t.DateTimeCreated < @SnapshotDate
GROUP BY
	p.K,
	p.UrlName


SELECT 
	*, 
	ticketMoneyWithCardnetDelay - ticketMoneyToPromoterBankAccounts - ticketMoneyAppliedToInvoices TicketMoneyReserve
FROM
(
	SELECT
		p.K,
		p.UrlName, 
		ISNULL(p.EnableTickets, 0) EnableTickets,
		ISNULL(t4.val, 0) TicketMoneyEarnt, 
		ISNULL(t1.val, 0) TicketMoneyWithCardnetDelay, 
		ISNULL(t2.val, 0) TicketMoneyToPromoterBankAccounts, 
		ISNULL(t3.val, 0) TicketMoneyAppliedToInvoices 
	FROM
		Promoter p
		LEFT JOIN ##ticketMoneyWithCardnetDelay t1 ON t1.K = p.K
		LEFT JOIN ##ticketMoneyToPromoterBankAccounts t2 ON t2.K = p.K
		LEFT JOIN ##ticketMoneyAppliedToInvoices t3 ON t3.K = p.K
		LEFT JOIN ##totalFunds t4 ON t4.K = p.K
	WHERE
		t1.Val is NOT null OR t2.Val is NOT null OR t3.Val is NOT null OR t4.Val is NOT null 
) 
AS t

SELECT
	SUM(total) as Total, 
	sum(ticketmoneywithcardnetdelay) as TicketMoneyWithCardnetDelay, 
	sum(ticketmoneytopromoterbankaccounts) as TicketMoneyToPromoterBankAccounts, 
	sum(ticketmoneyAppliedToInvoices) as TicketMoneyAppliedToInvoices, 
	sum(ticketMoneyWithCardnetDelay - ticketMoneyToPromoterBankAccounts - ticketMoneyAppliedToInvoices ) as TicketMoneyReserve 
FROM 
(
	SELECT
		p.K, 
		ISNULL(t4.val, 0) Total, 
		ISNULL(t1.val, 0) TicketMoneyWithCardnetDelay, 
		ISNULL(t2.val, 0) TicketMoneyToPromoterBankAccounts, 
		ISNULL(t3.val, 0) TicketMoneyAppliedToInvoices 
	FROM
		Promoter p
		LEFT JOIN ##ticketMoneyWithCardnetDelay t1 ON t1.K = p.K
		LEFT JOIN ##ticketMoneyToPromoterBankAccounts t2 ON t2.K = p.K
		LEFT JOIN ##ticketMoneyAppliedToInvoices t3 ON t3.K = p.K
		LEFT JOIN ##totalFunds t4 ON t4.K = p.K
	WHERE
		t1.Val is NOT null OR t2.Val is NOT null OR t3.Val is NOT null OR t4.Val is NOT null 
)
AS t

 DROP Table ##ticketMoneyWithCardnetDelay
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
