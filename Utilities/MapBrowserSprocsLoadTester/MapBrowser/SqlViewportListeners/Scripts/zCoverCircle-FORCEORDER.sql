
if (@WhereClause is null) SET @WhereClause  = '1=1'
if (@OrderBy is null) SET @OrderBy = 'ORDER BY K'

DECLARE @SQL NVARCHAR(MAX) SET @SQL = @CustomVariableDeclarationSql + N' 
DECLARE @MidLat FLOAT SET @MidLat = @South + (@North - @South) / 2
DECLARE @MidLon FLOAT SET @MidLon = @West + (@East - @West) / 2
DECLARE @Radius Float SET @Radius = dbo.fDistanceLatLon(@North, @West, @South, @East) / 2;

	SELECT * FROM (
		SELECT TOP ' + convert(varchar(max), @LastRowIndex + 1) + ' - 1 + ROW_NUMBER() OVER(' + @OrderBy + ') as RowNum, p.*
		FROM dbo.[fHtmCoverCircleLatLon](@MidLat,  @MidLon,  @Radius) f 
		JOIN ' + @TableName + ' p ON ' + @WhereClause + ' AND p.HtmId BETWEEN HtmIdStart AND HtmIdEnd 
					AND	Lat BETWEEN @South AND @North AND Lon BETWEEN @West AND @East
	) T	
		WHERE T.RowNum BETWEEN @FirstRowIndex AND @LastRowIndex
			ORDER BY T.RowNum
		OPTION (FORCE ORDER)
'

EXECUTE sp_executesql  
	@SQL, 
	N'@North FLOAT, @East FLOAT, @South FLOAT, @West FLOAT, @FirstRowIndex  INT, @LastRowIndex INT',
    @North = @North, @East = @East, @South = @South, @West = @West, @FirstRowIndex = @FirstRowIndex, @LastRowIndex = @LastRowIndex
