IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Brand' 
	AND	[column].name = 'TicketsDomainInclude'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Brand ADD
	TicketsDomainInclude bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Include override on this tickets domain?', N'SCHEMA', N'dbo', N'TABLE', N'Brand', N'COLUMN', N'TicketsDomainInclude'

END


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Venue' 
	AND	[column].name = 'TicketsDomainInclude'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Venue ADD
	TicketsDomainInclude bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Include override on this tickets domain?', N'SCHEMA', N'dbo', N'TABLE', N'Venue', N'COLUMN', N'TicketsDomainInclude'

END



IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Brand' 
	AND	[column].name = 'TicketsDomainExclude'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Brand ADD
	TicketsDomainExclude bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Exclude override on this tickets domain?', N'SCHEMA', N'dbo', N'TABLE', N'Brand', N'COLUMN', N'TicketsDomainExclude'

END


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Venue' 
	AND	[column].name = 'TicketsDomainExclude'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Venue ADD
	TicketsDomainExclude bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Exclude override on this tickets domain?', N'SCHEMA', N'dbo', N'TABLE', N'Venue', N'COLUMN', N'TicketsDomainExclude'

END
