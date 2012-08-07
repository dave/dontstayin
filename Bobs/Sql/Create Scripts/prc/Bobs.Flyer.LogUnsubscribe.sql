if exists (SELECT * FROM sys.procedures WHERE name = 'Bobs.Flyer.LogUnsubscribe') BEGIN
	DROP PROC [Bobs.Flyer.LogUnsubscribe]
END

GO

CREATE PROC [Bobs.Flyer.LogUnsubscribe] (@FlyerK INT)
AS 
UPDATE [Flyer] SET [Unsubscribes] = [Unsubscribes] + 1 WHERE [K] = @FlyerK

GO

