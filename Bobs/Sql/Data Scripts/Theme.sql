DELETE FROM THEME
TRUNCATE TABLE THEME

SET IDENTITY_INSERT Theme ON
INSERT INTO Theme (K, UrlName, Name, Description, Examples, [Order]) Values (1, 'nightlife', 'Parties & nightlife', NULL, 'clubbing friends groups$partying in your town$festivals$free parties etc...', 10)
INSERT INTO Theme (K, UrlName, Name, Description, Examples, [Order]) Values (2, 'music', 'Music', NULL, 'music styles$DJ fan groups$mixing$music production etc...', 20)
INSERT INTO Theme (K, UrlName, Name, Description, Examples, [Order]) Values (3, 'photography', 'Photography', NULL, 'spotters$digital cameras$pro protography etc...', 30)
INSERT INTO Theme (K, UrlName, Name, Description, Examples, [Order]) Values (4, 'style', 'Style', NULL, 'fashion & shopping$clothes & designers$hair & make-up etc...', 40)
INSERT INTO Theme (K, UrlName, Name, Description, Examples, [Order]) Values (5, 'technology', 'Technology', NULL, 'computers & internet$electronics & gadgets$hi-fi$sound & DJ equipment etc...', 50)
INSERT INTO Theme (K, UrlName, Name, Description, Examples, [Order]) Values (6, 'entertainment', 'Entertainment & the arts', NULL, 'movies & TV$celebrities$theatre$books$art & artists etc...', 60)
INSERT INTO Theme (K, UrlName, Name, Description, Examples, [Order]) Values (7, 'automotive', 'Automotive', NULL, 'cars & motorbikes$motor racing etc...', 70)
INSERT INTO Theme (K, UrlName, Name, Description, Examples, [Order]) Values (8, 'sports', 'Sports & health', NULL, 'professional & recreational sports$clubs & associations$health, fitness & working out etc...', 80)
INSERT INTO Theme (K, UrlName, Name, Description, Examples, [Order]) Values (9, 'news-and-politics', 'News & politics', NULL, 'news$government & politics$the environment etc...', 90)
INSERT INTO Theme (K, UrlName, Name, Description, Examples, [Order]) Values (10, 'culture', 'Culture', NULL, 'countries, culture & community$travel & holidays etc...', 100)
INSERT INTO Theme (K, UrlName, Name, Description, Examples, [Order]) Values (11, 'home-and-family', 'Home & family', NULL, 'family & kids$pets & animals$education etc...', 110)
INSERT INTO Theme (K, UrlName, Name, Description, Examples, [Order]) Values (12, 'food-and-drink', 'Food & drink', NULL, 'food & wine$eating out$heatlhy eating$dieting etc...', 120)
INSERT INTO Theme (K, UrlName, Name, Description, Examples, [Order]) Values (13, 'games', 'Games & hobbies', NULL, 'computer & console games$board games$hobbies & crafts etc...', 130)
INSERT INTO Theme (K, UrlName, Name, Description, Examples, [Order]) Values (14, 'relationships', 'Relationships', NULL, 'love & romance$sex & sexuality$lesbian, gay & bisexual etc...', 140)
INSERT INTO Theme (K, UrlName, Name, Description, Examples, [Order]) Values (15, 'science', 'Science & history', NULL, 'maths, physics, biology & chemistry$social science & psychology$space$weather etc...', 150)
INSERT INTO Theme (K, UrlName, Name, Description, Examples, [Order]) Values (16, 'philosophy', 'Philosophy & religion', NULL, 'religion & beliefs$philanthropy & charity$paranormal & astrology etc...', 160)
INSERT INTO Theme (K, UrlName, Name, Description, Examples, [Order]) Values (17, 'business', 'Business & money', NULL, 'careers & the workplace$entrepreneurs$property$saving & investing$tax & accounting etc...', 170)
INSERT INTO Theme (K, UrlName, Name, Description, Examples, [Order]) Values (18, 'other', 'Other', NULL, 'random & silly stuff$groups that don''t fit in elsewhere', 180)
SET IDENTITY_INSERT Theme OFF
