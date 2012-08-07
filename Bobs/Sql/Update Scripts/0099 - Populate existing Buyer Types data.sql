
update inv set BuyerType = 
	case p.IsAgency 
		when 1 then 1	--AgencyPromoter
		else 2			--NonAgencyPromoter
	end
from invoice inv inner join promoter p on inv.promoterk = p.k

update inv set BuyerType = 3 --TicketUsr
	from invoice inv inner join invoiceitem invi on inv.k = invi.invoicek and BuyableObjectType = 18 -- Ticket

update inv set BuyerType = 4 --NonTicketUsr
	from invoice inv left join invoiceitem invi on inv.k = invi.invoicek and BuyableObjectType = 18 -- Ticket
where promoterk = 0 and invi.k is null

