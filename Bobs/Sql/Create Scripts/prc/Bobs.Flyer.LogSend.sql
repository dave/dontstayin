if exists (SELECT * FROM sys.procedures WHERE name = 'Bobs.Flyer.LogSend') BEGIN
	DROP PROC [Bobs.Flyer.LogSend]
END

GO

CREATE PROC [Bobs.Flyer.LogSend] (@FlyerK INT)
AS 
UPDATE [Flyer] SET [Sends] = [Sends] + 1 WHERE [K] = @FlyerK

GO

