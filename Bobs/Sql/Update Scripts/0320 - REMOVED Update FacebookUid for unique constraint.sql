﻿--DECLARE @found int SET @found = 1 WHILE @found > 0
--BEGIN TRY
--UPDATE [Usr] SET [Usr].[FacebookUid] = ( 0 - [Usr].[K] ) WHERE [Usr].[K] IN (SELECT TOP 1000 [Usr].[K] FROM [Usr] WHERE [Usr].[FacebookUid] IS NULL ORDER BY [Usr].[K] DESC) 
--SET @found = @@ROWCOUNT
--END TRY
--BEGIN CATCH
--END CATCH
