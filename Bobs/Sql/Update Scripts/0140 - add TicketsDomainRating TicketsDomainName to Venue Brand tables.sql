IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Brand' 
	AND	[column].name = 'TicketsDomainRating'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Brand ADD
	TicketsDomainRating int NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Rating for tickets domain purchase', N'SCHEMA', N'dbo', N'TABLE', N'Brand', N'COLUMN', N'TicketsDomainRating'

END


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Venue' 
	AND	[column].name = 'TicketsDomainRating'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Venue ADD
	TicketsDomainRating int NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Rating for tickets domain purchase', N'SCHEMA', N'dbo', N'TABLE', N'Venue', N'COLUMN', N'TicketsDomainRating'

END



IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Brand' 
	AND	[column].name = 'TicketsDomain'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Brand ADD
	TicketsDomain varchar(50) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Actual domain name purchased (with tickets prefix)', N'SCHEMA', N'dbo', N'TABLE', N'Brand', N'COLUMN', N'TicketsDomain'

END


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Venue' 
	AND	[column].name = 'TicketsDomain'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Venue ADD
	TicketsDomain varchar(50) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Actual domain name purchased (with tickets prefix)', N'SCHEMA', N'dbo', N'TABLE', N'Venue', N'COLUMN', N'TicketsDomain'

END
