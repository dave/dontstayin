if exists (SELECT * FROM sys.procedures WHERE name = 'Bobs.Flyer.LogView') BEGIN
	DROP PROC [Bobs.Flyer.LogView]
END

GO

CREATE PROC [Bobs.Flyer.LogView] (@FlyerK INT)
AS 
UPDATE [Flyer] SET [Views] = [Views] + 1 WHERE [K] = @FlyerK

GO

