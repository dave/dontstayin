DECLARE @Counter INT SET @Counter = 0

WHILE @@ROWCOUNT > 0 AND @Counter < 50 BEGIN
	UPDATE	Place 
	SET		LatitudeDegreesNorth = ISNULL(Lat, LatitudeDegreesNorth),
			LongitudeDegreesWest = ISNULL((-1 * Lon), LongitudeDegreesWest)
	WHERE K IN (SELECT TOP 1000 K FROM Place WHERE LatitudeDegreesNorth <> ISNULL(Lat, LatitudeDegreesNorth) OR LongitudeDegreesWest <> ISNULL((-1 * Lon), LongitudeDegreesWest))
	SET @Counter = @Counter + 1
END

