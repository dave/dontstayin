DELETE FROM [Visit]
TRUNCATE TABLE [Visit]
SET IDENTITY_INSERT [Visit] ON
--												K,	[Guid] ,								[UsrK]
INSERT INTO [Visit] (K, [Guid] ,[UsrK]) VALUES (1,	'BA82A9B2-EFC5-457B-AC5A-67DEF5F9A2C5',	1)
SET IDENTITY_INSERT [Visit] OFF