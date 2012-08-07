
DECLARE @found int
SET @found = 1
WHILE @found > 0
BEGIN
	UPDATE [UsrPhotoFavourite]
		SET [DateTimeAdded] = (Select EnabledDateTime from Photo where Photo.K = UsrPhotoFavourite.PhotoK)
	WHERE [UsrPhotoFavourite].[K] IN 
		(SELECT TOP 1000 K FROM [UsrPhotoFavourite] WHERE [DateTimeAdded] is null)
	SET @found = @@ROWCOUNT
END

