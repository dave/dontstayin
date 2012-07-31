USE [CreateCommonDotCsProjectTestDatabase]

CREATE TABLE dbo.ExampleTable
	(
	K int NOT NULL IDENTITY (1, 1),
	VarcharColumn varchar(50) NULL,
	IntColumn int NULL,
	IntColumnWithDefaultOfMinusOne int NOT NULL,
	DateTimeColumn datetime NULL,
	GuidColumn uniqueidentifier NULL,
	GuidColumnWithDefaultOfNewId uniqueidentifier NULL,
	ColumnWithNoDescriptionThatShouldBeIgnored int NULL,
	BitColumn bit NULL,
	BitColumnWithDefaultOfZero bit NULL,
	BigIntColumn bigint NULL,
	BigIntColumnWithDefaultOfZero bigint NULL,
	CharColumn char(10) NULL,
	FloatColumn float(53) NULL,
	NCharColumn nchar(10) NULL,
	NTextColumn ntext NULL,
	NVarcharColumn nvarchar(50) NULL,
	SmallDateTimeColumn smalldatetime NULL,
	SmallIntColumn smallint NULL,
	TextColumn text NULL,
	RowVersion timestamp NULL,
	TinyIntColumn tinyint NULL,
	XmlColumn xml NULL,
	VarBinaryColumn VARBINARY(MAX),
	VarBinaryColumnWithDefaultOfZero VARBINARY(MAX),
	SqlVariantColumn sql_variant
	/*
	DateTimeColumnWithDefaultOf1Jan2000 DATETIME NOT NULL,
	VarCharColumnWithDefaultOfHello VARCHAR(MAX) NOT NULL,*/
	
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]

ALTER TABLE dbo.ExampleTable ADD CONSTRAINT
	DF_ExampleTable_IntColumnWithDefault DEFAULT -1 FOR IntColumnWithDefaultOfMinusOne
ALTER TABLE dbo.ExampleTable ADD CONSTRAINT
	DF_ExampleTable_GuidColumnWithDefaultOfNewId DEFAULT NEWID() FOR GuidColumnWithDefaultOfNewId
ALTER TABLE dbo.ExampleTable ADD CONSTRAINT
	DF_ExampleTable_VarBinaryColumnWithDefault DEFAULT 0 FOR VarBinaryColumnWithDefaultOfZero
ALTER TABLE dbo.ExampleTable ADD CONSTRAINT
	DF_ExampleTable_BigIntColumnWithDefault DEFAULT 0 FOR BigIntColumnWithDefaultOfZero
ALTER TABLE dbo.ExampleTable ADD CONSTRAINT
	DF_ExampleTable_BitColumnWithDefault DEFAULT 0 FOR BitColumnWithDefaultOfZero

	
/*
ALTER TABLE dbo.ExampleTable ADD CONSTRAINT
	DF_ExampleTable_DateTimeColumnWithDefault DEFAULT '1 Jan 2000' FOR DateTimeColumnWithDefaultOf1Jan2000

ALTER TABLE dbo.ExampleTable ADD CONSTRAINT
	DF_ExampleTable_VarCharColumnWithDefault DEFAULT 'Hello' FOR VarCharColumnWithDefaultOfHello
*/

ALTER TABLE dbo.ExampleTable ADD CONSTRAINT
	PK_ExampleTable PRIMARY KEY CLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


DECLARE @v sql_variant 
SET @v = N'An example primary key column'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'K'
SET @v = N'An example varchar column'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'VarcharColumn'
SET @v = N'An example int column'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'IntColumn'
SET @v = N'An example int column with a default value of -1'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'IntColumnWithDefaultOfMinusOne'
SET @v = N'An example datetime column'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'DateTimeColumn'
SET @v = N'An example guid column'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'GuidColumn'
SET @v = N'An example guid column with default of NEWID()'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'GuidColumnWithDefaultOfNewId'
SET @v = N'An example bit column'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'BitColumn'
SET @v = N'An example bit column with a default of zero'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'BitColumnWithDefaultOfZero'
SET @v = N'An example bigint column'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'BigIntColumn'
SET @v = N'An example bigint column with default of zero'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'BigIntColumnWithDefaultOfZero'
SET @v = N'An example char column'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'CharColumn'
SET @v = N'An example float column'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'FloatColumn'
SET @v = N'An example NChar column'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'NCharColumn'
SET @v = N'An example ntext column'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'NTextColumn'
SET @v = N'An example nvarchar column'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'NVarcharColumn'
SET @v = N'An example small datetime column'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'SmallDateTimeColumn'
SET @v = N'An example small int column'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'SmallIntColumn'
SET @v = N'An example text column'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'TextColumn'
SET @v = N'An example timestamp column for RowVersion'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'RowVersion'
SET @v = N'an example tinyint column'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'TinyIntColumn'
SET @v = N'An example xml column'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'XmlColumn'
SET @v = N'An example varbinary column'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'VarBinaryColumn'
SET @v = N'An example varbinary column with a default of zero'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'VarBinaryColumnWithDefaultOfZero'
SET @v = N'An example sql_variant column'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'SqlVariantColumn'

/*
SET @v = N'An example datetime column with a default of 1 Jan 2000'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'DateTimeColumnWithDefaultOf1Jan2000'
SET @v = N'An example varchar column with a default of "Hello"'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExampleTable', N'COLUMN', N'VarCharColumnWithDefaultOfHello'
*/

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'An example table' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ExampleTable'

