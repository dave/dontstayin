/* CANCELLED FOR NOT WORKING :)
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usr]') AND name = N'Usr47')  DROP INDEX [Usr47] ON [dbo].Usr WITH ( ONLINE = OFF )
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usr]') AND name = N'_dta_index_Usr_7_1689773077__K61_K25_K1_K12_K6_K103_5_8_24_30_47_48_49_50_65_68_82_114_115_122_123')  DROP INDEX [_dta_index_Usr_7_1689773077__K61_K25_K1_K12_K6_K103_5_8_24_30_47_48_49_50_65_68_82_114_115_122_123] ON [dbo].Usr WITH ( ONLINE = OFF )
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usr]') AND name = N'_dta_index_Usr_8_728389664__K1_K103_K8_K6_K24_5_12_25_30_47_48_49_50_61_65_82_122_123_124_127_128_129_130_133_134')  DROP INDEX [_dta_index_Usr_8_728389664__K1_K103_K8_K6_K24_5_12_25_30_47_48_49_50_61_65_82_122_123_124_127_128_129_130_133_134] ON [dbo].Usr WITH ( ONLINE = OFF )
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usr]') AND name = N'_dta_index_Usr_7_728389664__K2_K12_K1')  DROP INDEX [_dta_index_Usr_7_728389664__K2_K12_K1] ON [dbo].Usr WITH ( ONLINE = OFF )
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usr]') AND name = N'IDX_Usr_Guid')  DROP INDEX [IDX_Usr_Guid] ON [dbo].Usr WITH ( ONLINE = OFF )
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usr]') AND name = N'Index_Usr_SpottingsMonth_desc')  DROP INDEX [Index_Usr_SpottingsMonth_desc] ON [dbo].Usr WITH ( ONLINE = OFF )
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usr]') AND name = N'_dta_index_Usr_8_728389664__K6_K103_K1')  DROP INDEX [_dta_index_Usr_8_728389664__K6_K103_K1] ON [dbo].Usr WITH ( ONLINE = OFF )
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usr]') AND name = N'_dta_index_Usr_8_728389664__K6_K103_K8_K1')  DROP INDEX [_dta_index_Usr_8_728389664__K6_K103_K8_K1] ON [dbo].Usr WITH ( ONLINE = OFF )
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usr]') AND name = N'_dta_index_Usr_7_728389664__K34_K8_K36_K35_K45_K46_K1')  DROP INDEX [_dta_index_Usr_7_728389664__K34_K8_K36_K35_K45_K46_K1] ON [dbo].Usr WITH ( ONLINE = OFF )
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usr]') AND name = N'_dta_index_Usr_7_728389664__K103_K24')  DROP INDEX [_dta_index_Usr_7_728389664__K103_K24] ON [dbo].Usr WITH ( ONLINE = OFF )
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usr]') AND name = N'_dta_index_Usr_8_728389664__K103_K61_K6_K25_K1_K12_5_8_24_30_47_48_49_50_65_82_122_123_124_127_128_129_130_133_134')  DROP INDEX [_dta_index_Usr_8_728389664__K103_K61_K6_K25_K1_K12_5_8_24_30_47_48_49_50_65_82_122_123_124_127_128_129_130_133_134] ON [dbo].Usr WITH ( ONLINE = OFF )
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usr]') AND name = N'_dta_index_Usr_7_1689773077__K30_1')  DROP INDEX [_dta_index_Usr_7_1689773077__K30_1] ON [dbo].Usr WITH ( ONLINE = OFF )
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usr]') AND name = N'Usr2')  DROP INDEX [Usr2] ON [dbo].Usr WITH ( ONLINE = OFF )
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usr]') AND name = N'_dta_index_Usr_8_728389664__K1_K61_K25')  DROP INDEX [_dta_index_Usr_8_728389664__K1_K61_K25] ON [dbo].Usr WITH ( ONLINE = OFF )
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usr]') AND name = N'IX_Usr_on_SalesTeam_include_K_FirstName_LastName_NickName')  DROP INDEX [IX_Usr_on_SalesTeam_include_K_FirstName_LastName_NickName] ON [dbo].Usr WITH ( ONLINE = OFF )
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usr]') AND name = N'Index_Usr_LastPhotoUpload')  DROP INDEX [Index_Usr_LastPhotoUpload] ON [dbo].Usr WITH ( ONLINE = OFF )

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usr]') AND name = N'Usr8')  DROP INDEX [Usr8] ON [dbo].Usr WITH ( ONLINE = OFF )
GO
IF EXISTS (SELECT * FROM sys.columns c INNER JOIN sys.tables t ON t.object_id=c.object_id AND t.Name = 'Usr' AND c.Name = 'SearchColumn')

BEGIN 
	ALTER TABLE dbo.Usr DROP COLUMN
		SearchColumn  
	
END
GO
 

IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_1291867669_1_12') DROP STATISTICS Usr._dta_stat_1291867669_1_12   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_1291867669_1_5_125') DROP STATISTICS Usr._dta_stat_1291867669_1_5_125   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_1291867669_1_61_103_6_25') DROP STATISTICS Usr._dta_stat_1291867669_1_61_103_6_25   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_1291867669_103_1_25_61') DROP STATISTICS Usr._dta_stat_1291867669_103_1_25_61   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_1291867669_103_61_6') DROP STATISTICS Usr._dta_stat_1291867669_103_61_6   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_1291867669_12_61_103_6') DROP STATISTICS Usr._dta_stat_1291867669_12_61_103_6   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_1291867669_25_61_103') DROP STATISTICS Usr._dta_stat_1291867669_25_61_103   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_1291867669_6_1_25_61') DROP STATISTICS Usr._dta_stat_1291867669_6_1_25_61   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_1291867669_6_25_103') DROP STATISTICS Usr._dta_stat_1291867669_6_25_103   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_1291867669_87_1') DROP STATISTICS Usr._dta_stat_1291867669_87_1   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_1_103_24_132') DROP STATISTICS Usr._dta_stat_728389664_1_103_24_132   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_1_12_6') DROP STATISTICS Usr._dta_stat_728389664_1_12_6   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_1_25') DROP STATISTICS Usr._dta_stat_728389664_1_25   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_1_34_35_36_45') DROP STATISTICS Usr._dta_stat_728389664_1_34_35_36_45   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_1_34_8_36_35_45_46') DROP STATISTICS Usr._dta_stat_728389664_1_34_8_36_35_45_46   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_1_36_35_45_46_34') DROP STATISTICS Usr._dta_stat_728389664_1_36_35_45_46_34   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_1_65_124') DROP STATISTICS Usr._dta_stat_728389664_1_65_124   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_1_8') DROP STATISTICS Usr._dta_stat_728389664_1_8   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_1_8_35_36_45_46') DROP STATISTICS Usr._dta_stat_728389664_1_8_35_36_45_46   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_103_12_24_1') DROP STATISTICS Usr._dta_stat_728389664_103_12_24_1   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_103_24_132_12') DROP STATISTICS Usr._dta_stat_728389664_103_24_132_12   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_103_6_1_8_24') DROP STATISTICS Usr._dta_stat_728389664_103_6_1_8_24   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_12_1_103') DROP STATISTICS Usr._dta_stat_728389664_12_1_103   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_12_103_24') DROP STATISTICS Usr._dta_stat_728389664_12_103_24   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_12_24') DROP STATISTICS Usr._dta_stat_728389664_12_24   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_12_6_103') DROP STATISTICS Usr._dta_stat_728389664_12_6_103   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_12_61_6_25_103') DROP STATISTICS Usr._dta_stat_728389664_12_61_6_25_103   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_131_1') DROP STATISTICS Usr._dta_stat_728389664_131_1   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_132_1_103_12_24') DROP STATISTICS Usr._dta_stat_728389664_132_1_103_12_24   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_24_1_103_6') DROP STATISTICS Usr._dta_stat_728389664_24_1_103_6   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_24_1_103_8') DROP STATISTICS Usr._dta_stat_728389664_24_1_103_8   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_24_1_132') DROP STATISTICS Usr._dta_stat_728389664_24_1_132   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_25_103') DROP STATISTICS Usr._dta_stat_728389664_25_103   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_35_1_34_8') DROP STATISTICS Usr._dta_stat_728389664_35_1_34_8   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_35_36_45_46_34') DROP STATISTICS Usr._dta_stat_728389664_35_36_45_46_34   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_36_1_34') DROP STATISTICS Usr._dta_stat_728389664_36_1_34   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_45_1_34_8_36') DROP STATISTICS Usr._dta_stat_728389664_45_1_34_8_36   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_45_1_8_34_35') DROP STATISTICS Usr._dta_stat_728389664_45_1_8_34_35   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_46_1_8_34_35_36') DROP STATISTICS Usr._dta_stat_728389664_46_1_8_34_35_36   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_6_103_1_12') DROP STATISTICS Usr._dta_stat_728389664_6_103_1_12   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_6_103_8') DROP STATISTICS Usr._dta_stat_728389664_6_103_8   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_6_103_8_1_24') DROP STATISTICS Usr._dta_stat_728389664_6_103_8_1_24   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_65_124') DROP STATISTICS Usr._dta_stat_728389664_65_124   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_8_1_24') DROP STATISTICS Usr._dta_stat_728389664_8_1_24   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_8_1_6') DROP STATISTICS Usr._dta_stat_728389664_8_1_6   
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_WA_Sys_00000047_0EF836A4') DROP STATISTICS Usr._WA_Sys_00000047_0EF836A4   


IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_1291867669_1_12') DROP STATISTICS Usr._dta_stat_1291867669_1_12 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_1291867669_1_5_125') DROP STATISTICS Usr._dta_stat_1291867669_1_5_125 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_1291867669_1_61_103_6_25') DROP STATISTICS Usr._dta_stat_1291867669_1_61_103_6_25 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_1291867669_103_1_25_61') DROP STATISTICS Usr._dta_stat_1291867669_103_1_25_61 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_1291867669_103_61_6') DROP STATISTICS Usr._dta_stat_1291867669_103_61_6 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_1291867669_12_61_103_6') DROP STATISTICS Usr._dta_stat_1291867669_12_61_103_6 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_1291867669_25_61_103') DROP STATISTICS Usr._dta_stat_1291867669_25_61_103 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_1291867669_6_1_25_61') DROP STATISTICS Usr._dta_stat_1291867669_6_1_25_61 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_1291867669_6_25_103') DROP STATISTICS Usr._dta_stat_1291867669_6_25_103 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_1291867669_87_1') DROP STATISTICS Usr._dta_stat_1291867669_87_1 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_1_103_24_132') DROP STATISTICS Usr._dta_stat_728389664_1_103_24_132 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_1_12_6') DROP STATISTICS Usr._dta_stat_728389664_1_12_6 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_1_25') DROP STATISTICS Usr._dta_stat_728389664_1_25 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_1_34_35_36_45') DROP STATISTICS Usr._dta_stat_728389664_1_34_35_36_45 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_1_34_8_36_35_45_46') DROP STATISTICS Usr._dta_stat_728389664_1_34_8_36_35_45_46 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_1_36_35_45_46_34') DROP STATISTICS Usr._dta_stat_728389664_1_36_35_45_46_34 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_1_65_124') DROP STATISTICS Usr._dta_stat_728389664_1_65_124 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_1_8') DROP STATISTICS Usr._dta_stat_728389664_1_8 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_1_8_35_36_45_46') DROP STATISTICS Usr._dta_stat_728389664_1_8_35_36_45_46 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_103_12_24_1') DROP STATISTICS Usr._dta_stat_728389664_103_12_24_1 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_103_24_132_12') DROP STATISTICS Usr._dta_stat_728389664_103_24_132_12 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_103_6_1_8_24') DROP STATISTICS Usr._dta_stat_728389664_103_6_1_8_24 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_12_1_103') DROP STATISTICS Usr._dta_stat_728389664_12_1_103 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_12_103_24') DROP STATISTICS Usr._dta_stat_728389664_12_103_24 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_12_24') DROP STATISTICS Usr._dta_stat_728389664_12_24 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_12_6_103') DROP STATISTICS Usr._dta_stat_728389664_12_6_103 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_12_61_6_25_103') DROP STATISTICS Usr._dta_stat_728389664_12_61_6_25_103 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_131_1') DROP STATISTICS Usr._dta_stat_728389664_131_1 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_132_1_103_12_24') DROP STATISTICS Usr._dta_stat_728389664_132_1_103_12_24 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_24_1_103_6') DROP STATISTICS Usr._dta_stat_728389664_24_1_103_6 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_24_1_103_8') DROP STATISTICS Usr._dta_stat_728389664_24_1_103_8 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_24_1_132') DROP STATISTICS Usr._dta_stat_728389664_24_1_132 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_25_103') DROP STATISTICS Usr._dta_stat_728389664_25_103 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_35_1_34_8') DROP STATISTICS Usr._dta_stat_728389664_35_1_34_8 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_35_36_45_46_34') DROP STATISTICS Usr._dta_stat_728389664_35_36_45_46_34 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_36_1_34') DROP STATISTICS Usr._dta_stat_728389664_36_1_34 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_45_1_34_8_36') DROP STATISTICS Usr._dta_stat_728389664_45_1_34_8_36 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_45_1_8_34_35') DROP STATISTICS Usr._dta_stat_728389664_45_1_8_34_35 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_46_1_8_34_35_36') DROP STATISTICS Usr._dta_stat_728389664_46_1_8_34_35_36 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_6_103_1_12') DROP STATISTICS Usr._dta_stat_728389664_6_103_1_12 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_6_103_8') DROP STATISTICS Usr._dta_stat_728389664_6_103_8 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_6_103_8_1_24') DROP STATISTICS Usr._dta_stat_728389664_6_103_8_1_24 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_65_124') DROP STATISTICS Usr._dta_stat_728389664_65_124 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_8_1_24') DROP STATISTICS Usr._dta_stat_728389664_8_1_24 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_dta_stat_728389664_8_1_6') DROP STATISTICS Usr._dta_stat_728389664_8_1_6 
IF EXISTS( SELECT * FROM sys.stats s INNER JOIN sys.tables t ON s.object_id = t.object_id WHERE t.Name = 'Usr' AND s.Name = '_WA_Sys_00000047_0EF836A4') DROP STATISTICS Usr._WA_Sys_00000047_0EF836A4 

GO
ALTER TABLE [Usr] ALTER COLUMN [NickName]
            varchar(100) COLLATE database_default NOT NULL
GO

CREATE STATISTICS _dta_stat_1291867669_1_12   ON Usr(K, NickName) 
CREATE STATISTICS _dta_stat_1291867669_1_5_125   ON Usr(K, IsAdmin, SpottingsMonth) 
CREATE STATISTICS _dta_stat_1291867669_1_61_103_6_25   ON Usr(K, IsLoggedOn, IsSkeleton, IsEmailVerified, DateTimeLastPageRequest) 
CREATE STATISTICS _dta_stat_1291867669_103_1_25_61   ON Usr(IsSkeleton, K, DateTimeLastPageRequest, IsLoggedOn) 
CREATE STATISTICS _dta_stat_1291867669_103_61_6   ON Usr(IsSkeleton, IsLoggedOn, IsEmailVerified) 
CREATE STATISTICS _dta_stat_1291867669_12_61_103_6   ON Usr(NickName, IsLoggedOn, IsSkeleton, IsEmailVerified) 
CREATE STATISTICS _dta_stat_1291867669_25_61_103   ON Usr(DateTimeLastPageRequest, IsLoggedOn, IsSkeleton) 
CREATE STATISTICS _dta_stat_1291867669_6_1_25_61   ON Usr(IsEmailVerified, K, DateTimeLastPageRequest, IsLoggedOn) 
CREATE STATISTICS _dta_stat_1291867669_6_25_103   ON Usr(IsEmailVerified, DateTimeLastPageRequest, IsSkeleton) 
CREATE STATISTICS _dta_stat_1291867669_87_1   ON Usr(UpdateSendGenericMusic, K) 
CREATE STATISTICS _dta_stat_728389664_1_103_24_132   ON Usr(K, IsSkeleton, DateTimeSignUp, CampTickets) 
CREATE STATISTICS _dta_stat_728389664_1_12_6   ON Usr(K, NickName, IsEmailVerified) 
CREATE STATISTICS _dta_stat_728389664_1_25   ON Usr(K, DateTimeLastPageRequest) 
CREATE STATISTICS _dta_stat_728389664_1_34_35_36_45   ON Usr(K, IsSingle, IsMale, IsFemale, SexHelperMale) 
CREATE STATISTICS _dta_stat_728389664_1_34_8_36_35_45_46   ON Usr(K, IsSingle, Pic, IsFemale, IsMale, SexHelperMale, SexHelperFemale) 
CREATE STATISTICS _dta_stat_728389664_1_36_35_45_46_34   ON Usr(K, IsFemale, IsMale, SexHelperMale, SexHelperFemale, IsSingle) 
CREATE STATISTICS _dta_stat_728389664_1_65_124   ON Usr(K, IsProSpotter, SpottingsTotal) 
CREATE STATISTICS _dta_stat_728389664_1_8   ON Usr(K, Pic) 
CREATE STATISTICS _dta_stat_728389664_1_8_35_36_45_46   ON Usr(K, Pic, IsMale, IsFemale, SexHelperMale, SexHelperFemale) 
CREATE STATISTICS _dta_stat_728389664_103_12_24_1   ON Usr(IsSkeleton, NickName, DateTimeSignUp, K) 
CREATE STATISTICS _dta_stat_728389664_103_24_132_12   ON Usr(IsSkeleton, DateTimeSignUp, CampTickets, NickName) 
CREATE STATISTICS _dta_stat_728389664_103_6_1_8_24   ON Usr(IsSkeleton, IsEmailVerified, K, Pic, DateTimeSignUp) 
CREATE STATISTICS _dta_stat_728389664_12_1_103   ON Usr(NickName, K, IsSkeleton) 
CREATE STATISTICS _dta_stat_728389664_12_103_24   ON Usr(NickName, IsSkeleton, DateTimeSignUp) 
CREATE STATISTICS _dta_stat_728389664_12_24   ON Usr(NickName, DateTimeSignUp) 
CREATE STATISTICS _dta_stat_728389664_12_6_103   ON Usr(NickName, IsEmailVerified, IsSkeleton) 
CREATE STATISTICS _dta_stat_728389664_12_61_6_25_103   ON Usr(NickName, IsLoggedOn, IsEmailVerified, DateTimeLastPageRequest, IsSkeleton) 
CREATE STATISTICS _dta_stat_728389664_131_1   ON Usr(IsPromoter, K) 
CREATE STATISTICS _dta_stat_728389664_132_1_103_12_24   ON Usr(CampTickets, K, IsSkeleton, NickName, DateTimeSignUp) 
CREATE STATISTICS _dta_stat_728389664_24_1_103_6   ON Usr(DateTimeSignUp, K, IsSkeleton, IsEmailVerified) 
CREATE STATISTICS _dta_stat_728389664_24_1_103_8   ON Usr(DateTimeSignUp, K, IsSkeleton, Pic) 
CREATE STATISTICS _dta_stat_728389664_24_1_132   ON Usr(DateTimeSignUp, K, CampTickets) 
CREATE STATISTICS _dta_stat_728389664_25_103   ON Usr(DateTimeLastPageRequest, IsSkeleton) 
CREATE STATISTICS _dta_stat_728389664_35_1_34_8   ON Usr(IsMale, K, IsSingle, Pic) 
CREATE STATISTICS _dta_stat_728389664_35_36_45_46_34   ON Usr(IsMale, IsFemale, SexHelperMale, SexHelperFemale, IsSingle) 
CREATE STATISTICS _dta_stat_728389664_36_1_34   ON Usr(IsFemale, K, IsSingle) 
CREATE STATISTICS _dta_stat_728389664_45_1_34_8_36   ON Usr(SexHelperMale, K, IsSingle, Pic, IsFemale) 
CREATE STATISTICS _dta_stat_728389664_45_1_8_34_35   ON Usr(SexHelperMale, K, Pic, IsSingle, IsMale) 
CREATE STATISTICS _dta_stat_728389664_46_1_8_34_35_36   ON Usr(SexHelperFemale, K, Pic, IsSingle, IsMale, IsFemale) 
CREATE STATISTICS _dta_stat_728389664_6_103_1_12   ON Usr(IsEmailVerified, IsSkeleton, K, NickName) 
CREATE STATISTICS _dta_stat_728389664_6_103_8   ON Usr(IsEmailVerified, IsSkeleton, Pic) 
CREATE STATISTICS _dta_stat_728389664_6_103_8_1_24   ON Usr(IsEmailVerified, IsSkeleton, Pic, K, DateTimeSignUp) 
CREATE STATISTICS _dta_stat_728389664_65_124   ON Usr(IsProSpotter, SpottingsTotal) 
CREATE STATISTICS _dta_stat_728389664_8_1_24   ON Usr(Pic, K, DateTimeSignUp) 
CREATE STATISTICS _dta_stat_728389664_8_1_6   ON Usr(Pic, K, IsEmailVerified) 
CREATE STATISTICS _WA_Sys_00000047_0EF836A4   ON Usr(TotalPhotoUploads) 
 GO
CREATE NONCLUSTERED INDEX [_dta_index_Usr_7_1689773077__K61_K25_K1_K12_K6_K103_5_8_24_30_47_48_49_50_65_68_82_114_115_122_123] ON [dbo].[Usr] ([IsLoggedOn], [DateTimeLastPageRequest], [K], [NickName], [IsEmailVerified], [IsSkeleton]) INCLUDE ([AdminLevel], [BuddyCount], [ChatMessageCount], [CommentCount], [DateTimeSignUp], [DonateExpire], [DonateIcon], [EventCount], [ExtraExpire], [ExtraIcon], [Introductions], [IsAdmin], [IsProSpotter], [IsSpotter], [Pic])
GO
CREATE NONCLUSTERED INDEX [Usr47] ON [dbo].[Usr] ([DateTimeSignUp])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Usr_8_728389664__K1_K103_K8_K6_K24_5_12_25_30_47_48_49_50_61_65_82_122_123_124_127_128_129_130_133_134] ON [dbo].[Usr] ([K], [IsSkeleton], [Pic], [IsEmailVerified], [DateTimeSignUp]) INCLUDE ([AdminLevel], [BuddyCount], [ChatMessageCount], [CommentCount], [DateTimeLastPageRequest], [Donate1Expire], [Donate1Icon], [Donate2Expire], [Donate2Icon], [EventCount], [ExtraExpire], [ExtraIcon], [HasTicket], [IsAdmin], [IsLoggedOn], [IsProSpotter], [IsSpotter], [LastTicketEventDateTime], [NickName], [SpottingsTotal])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Usr_7_728389664__K2_K12_K1] ON [dbo].[Usr] ([Email], [NickName], [K])
GO
CREATE NONCLUSTERED INDEX [IDX_Usr_Guid] ON [dbo].[Usr] ([Guid])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Usr_8_728389664__K6_K103_K1] ON [dbo].[Usr] ([IsEmailVerified], [IsSkeleton], [K])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Usr_8_728389664__K6_K103_K8_K1] ON [dbo].[Usr] ([IsEmailVerified], [IsSkeleton], [Pic], [K])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Usr_7_728389664__K34_K8_K36_K35_K45_K46_K1] ON [dbo].[Usr] ([IsSingle], [Pic], [IsFemale], [IsMale], [SexHelperMale], [SexHelperFemale], [K])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Usr_7_728389664__K103_K24] ON [dbo].[Usr] ([IsSkeleton], [DateTimeSignUp])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Usr_8_728389664__K103_K61_K6_K25_K1_K12_5_8_24_30_47_48_49_50_65_82_122_123_124_127_128_129_130_133_134] ON [dbo].[Usr] ([IsSkeleton], [IsLoggedOn], [IsEmailVerified], [DateTimeLastPageRequest], [K], [NickName]) INCLUDE ([AdminLevel], [BuddyCount], [ChatMessageCount], [CommentCount], [DateTimeSignUp], [Donate1Expire], [Donate1Icon], [Donate2Expire], [Donate2Icon], [EventCount], [ExtraExpire], [ExtraIcon], [HasTicket], [IsAdmin], [IsProSpotter], [IsSpotter], [LastTicketEventDateTime], [Pic], [SpottingsTotal])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Usr_7_1689773077__K30_1] ON [dbo].[Usr] ([AdminLevel]) INCLUDE ([K])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Usr_15_728389664__K144_1_10_11_12] ON [dbo].[Usr] ([SalesTeam]) INCLUDE ([FirstName], [K], [LastName], [NickName])
GO
CREATE NONCLUSTERED INDEX [Usr2] ON [dbo].[Usr] ([K], [IsAdmin], [Pic], [NickName], [DateTimeSignUp], [AdminLevel], [BuddyCount], [ChatMessageCount], [CommentCount], [EventCount], [HasDonated], [IsProSpotter], [Introductions], [IsSpotter])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Usr_8_728389664__K1_K61_K25] ON [dbo].[Usr] ([K], [IsLoggedOn], [DateTimeLastPageRequest])
GO
CREATE NONCLUSTERED INDEX [Index_Usr_LastPhotoUpload] ON [dbo].[Usr] ([LastPhotoUpload] DESC)
GO
CREATE NONCLUSTERED INDEX [Usr8] ON [dbo].[Usr] ([NickName])
GO
CREATE NONCLUSTERED INDEX [Index_Usr_SpottingsMonth_desc] ON [dbo].[Usr] ([SpottingsMonth] DESC) INCLUDE ([AdminLevel], [BuddyCount], [ChatMessageCount], [CommentCount], [DateTimeLastPageRequest], [DateTimeSignUp], [Donate1Expire], [Donate1Icon], [Donate2Expire], [Donate2Icon], [EventCount], [ExtraExpire], [ExtraIcon], [HasTicket], [IsAdmin], [IsLoggedOn], [IsProSpotter], [IsSpotter], [K], [LastTicketEventDateTime], [NickName], [Pic], [SpottingsMonthRank], [SpottingsTotal])
GO
GO

IF NOT EXISTS (SELECT * FROM sys.columns c INNER JOIN sys.tables t ON t.object_id=c.object_id AND t.Name = 'Usr' AND c.Name = 'SearchColumn')
BEGIN 
	ALTER TABLE dbo.Usr ADD
		SearchColumn  AS NickName + ', ' + FirstName + ' ' + LastName + ', ' + Email PERSISTED 
	
	DECLARE @v sql_variant 
	SET @v = N'Column used for searching'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'SearchColumn'
	
END
GO
*/
