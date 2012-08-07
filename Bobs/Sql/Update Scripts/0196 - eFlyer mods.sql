IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Flyer' 
	AND	[column].name = 'IsReadyToSend'
) BEGIN

ALTER TABLE dbo.Flyer ADD IsReadyToSend bit NULL
ALTER TABLE dbo.Flyer ADD IsSending bit NULL
ALTER TABLE dbo.Flyer ADD PausedAtUsrK int NULL
	
DECLARE @v sql_variant 
SET @v = N'Is this eFlyer confirmed by admin and queued up to send?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'IsReadyToSend'

SET @v = N'Is this eFlyer currently in the process of sending?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'IsSending'

SET @v = N'If the eFlyer has been stopped mid-sending, the last usrK reached'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'PausedAtUsrK'

END

GO

IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Flyer' 
	AND	[column].name = 'HasFinishedSending'
) BEGIN

ALTER TABLE dbo.Flyer ADD HasFinishedSending bit NULL
	
DECLARE @v sql_variant 
SET @v = N'Has this eFlyer successfully finished sending?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'HasFinishedSending'

END
