IF NOT EXISTS(
	SELECT * FROM sys.tables AS tbl INNER JOIN sys.indexes AS i ON (i.index_id > 0 and i.is_hypothetical = 0) AND (i.object_id=tbl.object_id)
	WHERE tbl.name=N'Domain'
	and i.Name = 'UQ_DomainName'
)
BEGIN
	ALTER TABLE Domain ADD CONSTRAINT UQ_DomainName UNIQUE (DomainName) 
END


