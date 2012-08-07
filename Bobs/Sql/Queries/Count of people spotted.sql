SELECT Usr.Nickname, COUNT(DISTINCT [UsrPhotoMe].[UsrK]) FROM Usr
INNER JOIN Photo ON Usr.[K] = [Photo].[UsrK]
INNER JOIN [UsrPhotoMe] ON [Photo].[K] = [UsrPhotoMe].[PhotoK]
WHERE [Usr].[IsProSpotter] = 1 AND [Photo].[ParentDateTime] >= '3 Nov 2007'
GROUP BY [Usr].[NickName]
ORDER BY 2 DESC 
