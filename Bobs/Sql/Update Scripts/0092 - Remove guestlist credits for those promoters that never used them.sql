UPDATE p
SET p.GuestListCredit = 0
FROM Promoter p 
LEFT JOIN GuestListCredit glc ON p.K = glc.PromoterK
WHERE glc.PromoterK is null
