 

DELETE FROM Brand
TRUNCATE TABLE [Brand]
SET IDENTITY_INSERT [Brand] ON

INSERT INTO [Brand] 
([K],[Name],[PromoterK],[Pic],[OwnerUsrK],[IsNew],[IsEdited],[DuplicateGuid],[PromoterStatus],[UrlName],
[PicState],
[PicPhotoK],
[PicMiscK],
[GroupK],[TotalComments],[LastPost],[AverageCommentDateTime],[DateTimeCreated],[NoPhotos],[AddedRegulars])
VALUES
(
1,
'Hed Kandi', --Name
1,
'9146D1D7-8B44-47F0-AA74-B290FBA8DEA3',
1,
0,
0,
'5B7233A2-4F93-483E-95B9-B3931DF4E05F',
2,
'hed-kandi',
NULL,
NULL,
NULL,
1, --GroupK
81199,'Sep 14 2007 12:05:08:110AM','Mar 15 2006  6:45:15:890PM',NULL,NULL,1)

    
SET IDENTITY_INSERT [Brand] OFF





