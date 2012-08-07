UPDATE Banner
SET TotalRequiredImpressions = bi.TotalRequiredImpressions 

FROM Banner INNER JOIN 
(
	SELECT b.k, TotalRequiredImpressions = CASE WHEN SUM(ISNULL(hits, 0)) = 0 THEN ISNULL(TotalHits, 0) ELSE Sum(ISNULL(hits, 0)) END
	FROM Banner b 
	LEFT JOIN BannerStat bs ON b.K = bs.BannerK
	where getdate() > DATEADD(day, 1, LastDay) 
	group by B.K, TotalHits


	UNION

	SELECT d.k, CASE WHEN ByRate > ByWeight THEN ByRate Else ByWeight END
	FROM (
		SELECT	b.k, 
				ceiling(60000.0/(24* 7.0) * (datediff(hour, FirstDay, dateadd(day, 1, LastDay)))) * weight as ByWeight,
				CAST(
					CAST(DATEDIFF(hour, FirstDay, dateadd(day, 1, LastDay)) as FLOAT) / CAST(DATEDIFF(hour, FirstDay, GETDATE()) as FLOAT) * SUM(ISNULL(hits, 0))
				AS INT) as ByRate
		FROM Banner b
		LEFT JOIN BannerStat bs ON b.K = bs.BannerK
		where getdate() between FirstDay and DATEADD(day, 1, LastDay) 
		group by B.K, FirstDay, LastDay, Weight
	) d
	inner join banner b ON d.K = b.k 

	UNION 

	SELECT	b.k,
			ceiling(60000.0/7.0 * (datediff(day, FirstDay, LastDay) + 1)) * weight 
	FROM Banner b
	where getdate() < FirstDay

) bi ON banner.K = bi.k
