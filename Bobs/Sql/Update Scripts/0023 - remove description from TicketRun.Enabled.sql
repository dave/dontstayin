BEGIN TRANSACTION
GO
DECLARE @v sql_variant 
SET @v = N''
EXECUTE sp_updateextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'TicketRun', N'COLUMN', N'Enabled'
GO
COMMIT
