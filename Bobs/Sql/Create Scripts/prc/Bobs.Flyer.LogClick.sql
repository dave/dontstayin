if exists (SELECT * FROM sys.procedures WHERE name = 'Bobs.Flyer.LogClick') BEGIN
	DROP PROC [Bobs.Flyer.LogClick]
END

GO

CREATE PROC [Bobs.Flyer.LogClick] (@FlyerK INT)
AS 
UPDATE [Flyer] SET [Clicks] = [Clicks] + 1 WHERE [K] = @FlyerK

GO

