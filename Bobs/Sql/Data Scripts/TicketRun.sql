truncate table TicketRun

set identity_insert TicketRun on
insert into TicketRun (K, EventK, PromoterK, Name, Price, StartDateTime, EndDateTime, Enabled, MaxTickets, BookingFee) values (1, 1, 1, 'Ticket Run 1', 15.00, dateadd(day, -3, datediff(day, 0, getdate())), dateadd(day, 10, datediff(day, 0, getdate())), 1, 100, 1.20)
set identity_insert TicketRun off
