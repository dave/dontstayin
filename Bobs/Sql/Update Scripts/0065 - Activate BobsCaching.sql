IF EXISTS (SELECT * FROM Setting WHERE Name = 'BobsCachingIsEnabled') BEGIN
	UPDATE Setting SET Value = CAST(1 AS BIT) WHERE Name = 'BobsCachingIsEnabled'
END ELSE BEGIN
	INSERT INTO Setting (Name, Value) VALUES ('BobsCachingIsEnabled', CAST(1 AS BIT) )
END 
