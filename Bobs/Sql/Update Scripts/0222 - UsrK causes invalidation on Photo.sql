IF NOT EXISTS (SELECT * FROM
	sys.tables AS tbl
	INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
	INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
	WHERE
	(p.name=N'CausesInvalidation')and((clmns.name=N'UsrK')and((tbl.name=N'Photo' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))

BEGIN
	EXECUTE sp_addextendedproperty N'CausesInvalidation', N'true', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'UsrK'
END
