if exists (select * from sys.columns c inner join sys.tables t on c.object_id = t.object_id where t.name = 'DonationIcon' and c.name = 'ImgUrl' and is_nullable = 0)
	alter table DonationIcon alter column ImgUrl varchar(MAX) NULL
