
DELETE FROM Country
TRUNCATE TABLE [Country]
SET IDENTITY_INSERT [Country] ON

INSERT INTO [Country]
           ([K]
           ,[Name]
           ,[CurrencyCode]
           ,[CurrencyName]
           ,[CurrencyDecimals]
           ,[Region]
           ,[Code2Letter]
           ,[Code3Letter]
           ,[Code3Number]
           ,[EuVatCodePrefix]
           ,[PlacePopulationMinimum]
           ,[FriendlyName]
           ,[PostcodeType]
           ,[Mature]
           ,[UseRegion]
           ,[RegionName]
           ,[Enabled]
           ,[MinEventsForPlaceMenu]
           ,[DialingCode]
           ,[TotalEvents]
           ,[UrlName]
           ,[CustomHtml]
           ,[PostageZone])
     VALUES
           (224
		   ,'United Kingdom'
           ,'GBP'
           ,'Pounds Sterling'
           ,2
           ,1
           ,'GB'
           ,'GBR'
           ,826
           ,'GB'
           ,300
           ,'UK'
           ,1
           ,1
           ,null
           ,null
           ,1
           ,25
           ,44
           ,2
           ,'uk'
           ,null
           ,0
           )
           
SET IDENTITY_INSERT [Country] OFF
