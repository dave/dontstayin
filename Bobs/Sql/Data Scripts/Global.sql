
DELETE FROM [Global]
TRUNCATE TABLE [Global]

SET IDENTITY_INSERT [Global] ON

insert into Global (K, Name, Description, ValueInt, ValueDouble) values (3,	'TopBanner',	'Current top banner base price - per week per slot (double) and available slots (int)',	193,	49.99)
insert into Global (K, Name, Description, ValueInt, ValueDouble) values (4,	'Hotbox',	'Current hotbox base price - per week per slot (double) and available slots (int)',	190,	59.99)
insert into Global (K, Name, Description, ValueInt, ValueDouble) values (5,	'PhotoBanner',	'Current photo banner base price - per week per slot (double) and available slots (int)',	118,	29.99)
insert into Global (K, Name, Description, ValueInt, ValueDouble) values (6,	'EmailBanner',	'Current email banner base price - per week per slot (double) and available slots (int)',	54,	19.99)
INSERT INTO [dbo].[Global] (K, [Name], [Description], [ValueString], [ValueInt], [ValueDouble], [ValueDateTime], [ValueText]) VALUES (12, 'PhotoAbuseReports', 'Number of pending photo abuse reports', NULL,	0,	NULL,	NULL,	NULL)
insert into Global (K, Name, Description, ValueInt, ValueDouble) values (14,	'Skyscraper',	'Current price per week for skyscraper banners',	192,	39.99)
insert into Global (K, Name, Description, ValueText) values (17, 'BannerPositionData', 'BannerPositionData', 
'<?xml version="1.0" encoding="utf-16"?>
<PositionStats xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <PercentageFull>
    <item>
      <key>
        <Positions>Leaderboard</Positions>
      </key>
      <value>
        <double>35.039970392301996</double>
      </value>
    </item>
    <item>
      <key>
        <Positions>Hotbox</Positions>
      </key>
      <value>
        <double>56.157894736842117</double>
      </value>
    </item>
    <item>
      <key>
        <Positions>EmailBanner</Positions>
      </key>
      <value>
        <double>12.169312169312169</double>
      </value>
    </item>
    <item>
      <key>
        <Positions>PhotoBanner</Positions>
      </key>
      <value>
        <double>22.154963680387407</double>
      </value>
    </item>
    <item>
      <key>
        <Positions>Skyscraper</Positions>
      </key>
      <value>
        <double>22.233630952380953</double>
      </value>
    </item>
  </PercentageFull>
  <ClicksPerSlot>
    <item>
      <key>
        <Positions>Leaderboard</Positions>
      </key>
      <value>
        <double>56.386223235344673</double>
      </value>
    </item>
    <item>
      <key>
        <Positions>Hotbox</Positions>
      </key>
      <value>
        <double>69.349872010786413</double>
      </value>
    </item>
    <item>
      <key>
        <Positions>EmailBanner</Positions>
      </key>
      <value>
        <double>41.964285714285715</double>
      </value>
    </item>
    <item>
      <key>
        <Positions>PhotoBanner</Positions>
      </key>
      <value>
        <double>40.987458193979933</double>
      </value>
    </item>
    <item>
      <key>
        <Positions>Skyscraper</Positions>
      </key>
      <value>
        <double>76.979313432988022</double>
      </value>
    </item>
  </ClicksPerSlot>
  <HitsPerSlot>
    <item>
      <key>
        <Positions>Leaderboard</Positions>
      </key>
      <value>
        <double>99602.4663213269</double>
      </value>
    </item>
    <item>
      <key>
        <Positions>Hotbox</Positions>
      </key>
      <value>
        <double>61843.791449557844</double>
      </value>
    </item>
    <item>
      <key>
        <Positions>EmailBanner</Positions>
      </key>
      <value>
        <double>303267.67063492065</double>
      </value>
    </item>
    <item>
      <key>
        <Positions>PhotoBanner</Positions>
      </key>
      <value>
        <double>106159.05555820992</double>
      </value>
    </item>
    <item>
      <key>
        <Positions>Skyscraper</Positions>
      </key>
      <value>
        <double>147452.96002164512</double>
      </value>
    </item>
  </HitsPerSlot>
</PositionStats>')

SET IDENTITY_INSERT [Global] OFF
