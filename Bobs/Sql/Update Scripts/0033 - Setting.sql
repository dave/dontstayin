
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Setting]') AND type in (N'U'))
begin
	drop table Setting
end

go

create table Setting
(
	Name varchar(50) NOT NULL,
	Value sql_variant NOT NULL
)

ALTER TABLE dbo.Setting ADD CONSTRAINT
PK_Setting PRIMARY KEY CLUSTERED 
(
	Name
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

DECLARE @v sql_variant 
SET @v = N'Name of the Setting'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Setting', N'COLUMN', N'Name'
SET @v = N'Value of the Setting'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Setting', N'COLUMN', N'Value'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Configuration settings' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Setting'

INSERT INTO Setting (Name, Value) VALUES ('BannerServerMethod', 0)
INSERT INTO Setting (Name, Value) VALUES ('CacheTesting', 0)
INSERT INTO Setting (Name, Value) VALUES ('LoggingPageTime', 0)
INSERT INTO Setting (Name, Value) VALUES ('BobsCaching', 0)

delete from Global where K = 18
