IF NOT EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'Ticket' and c.Name='CardCheckedByPromoter') BEGIN

	ALTER TABLE dbo.Ticket ADD CardCV2 varchar(3)
	EXECUTE sp_addextendedproperty N'MS_Description', N'CV2 Security code on the back on the credit card', 
		N'SCHEMA', N'dbo', N'TABLE', N'Ticket', N'COLUMN', N'CardCV2'

	ALTER TABLE dbo.Ticket ADD CardCheckedByPromoter bit
	EXECUTE sp_addextendedproperty N'MS_Description', N'Has the promoter proven that the card was checked?', 
		N'SCHEMA', N'dbo', N'TABLE', N'Ticket', N'COLUMN', N'CardCheckedByPromoter'

	ALTER TABLE dbo.Ticket ADD CardCheckAttempts int
	EXECUTE sp_addextendedproperty N'MS_Description', N'How many times the promoter has attempted to confirm card details', 
		N'SCHEMA', N'dbo', N'TABLE', N'Ticket', N'COLUMN', N'CardCheckAttempts'

END


IF NOT EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'Promoter' and c.Name='WillCheckCardsForPurchasedTickets') BEGIN

	ALTER TABLE dbo.Promoter ADD WillCheckCardsForPurchasedTickets bit
	EXECUTE sp_addextendedproperty N'MS_Description', N'Does this promoter want to confirm card details with us to avoid responsibility for card payments?', 
		N'SCHEMA', N'dbo', N'TABLE', N'Promoter', N'COLUMN', N'WillCheckCardsForPurchasedTickets'

END

