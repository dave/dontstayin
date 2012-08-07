

--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--IF OBJECT_ID('bbLicensing') IS NULL 
--begin
--EXEC('CREATE view [dbo].bbLicensing as select ''1'' as numUsers, ''01/22/2009'' as expDate,''Company Name'' as regCompany,''Admin Name'' as regName, ''S12345678'' as SRPID, ''*** UNLICENSED ***'' as keyCode')
--end


IF OBJECT_ID('Usr_FacebookUid_Not_Null') IS NULL 
	EXEC('CREATE VIEW dbo.Usr_FacebookUid_Not_Null WITH SCHEMABINDING AS SELECT FacebookUid FROM dbo.Usr WHERE FacebookUid IS NOT NULL')
GO


IF NOT EXISTS(
	SELECT * FROM sys.indexes i
	INNER JOIN sys.views t ON i.object_id = t.object_id 
	WHERE i.name  ='Usr_FacebookUid_Not_Null_Unique' AND t.name = 'Usr_FacebookUid_Not_Null'
) BEGIN
	CREATE UNIQUE CLUSTERED INDEX Usr_FacebookUid_Not_Null_Unique ON dbo.Usr_FacebookUid_Not_Null(FacebookUid) 
END
