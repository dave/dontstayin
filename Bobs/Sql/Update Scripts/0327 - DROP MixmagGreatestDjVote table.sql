IF EXISTS(
	SELECT * FROM sys.tables WHERE Name = 'MixmagGreatestDjVote' 
) BEGIN
	drop table dbo.MixmagGreatestDjVote
END

GO
