DELETE FROM CampaignCredit
TRUNCATE TABLE CampaignCredit


insert into CampaignCredit (PromoterK, ActionDateTime, Description, Credits) values (2, getdate(), 'topup', 8000)
