DECLARE @DatabaseName VARCHAR(50) 
SET @DatabaseName = 'CreateCommonDotCsProjectTestDatabase'
DECLARE @spidstr varchar(8000)


DECLARE @ConnectionKilled smallint
SET @ConnectionKilled=0
SET @spidstr = ''

IF db_id(@DatabaseName) < 4  BEGIN
    PRINT 'No can do...'
    RETURN
END

SELECT @spidstr=COALESCE(@spidstr,',' )+'kill '+CONVERT(VARCHAR, spid)+ '; '
FROM master..sysprocesses WHERE dbid=db_id(@DatabaseName)

IF LEN(@spidstr) > 0 
BEGIN
    EXEC(@spidstr)

    SELECT @ConnectionKilled = COUNT(1)
    FROM master..sysprocesses WHERE dbid=db_id(@DatabaseName) 

END



DROP DATABASE [CreateCommonDotCsProjectTestDatabase] 
