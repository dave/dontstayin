

-- Usage: sp_generate_inserts @table_name = 'Venue', @from = 'FROM [Venue] WHERE [Venue].[K]=180'



SET NOCOUNT ON
GO

PRINT 'Using Master database'
USE master
GO

PRINT 'Checking for the existence of this procedure'
IF (SELECT OBJECT_ID('sp_generate_inserts','P')) IS NOT NULL --means, the procedure already exists
	BEGIN
		PRINT 'Procedure already exists. So, dropping it'
		DROP PROC sp_generate_inserts
	END
GO

CREATE PROC sp_generate_inserts
(
	@table_name varchar(776),  		-- The table/view for which the INSERT statements will be generated using the existing data
	@target_table varchar(776) = NULL, 	-- Use this parameter to specify a different table name into which the data will be inserted
	@include_column_list bit = 1,		-- Use this parameter to include/ommit column list in the generated INSERT statement
	@from varchar(800) = NULL, 		-- Use this parameter to filter the rows based on a filter condition (using WHERE)
	@include_timestamp bit = 0, 		-- Specify 1 for this parameter, if you want to include the TIMESTAMP/ROWVERSION column's data in the INSERT statement
	@debug_mode bit = 0,			-- If @debug_mode is set to 1, the SQL statements constructed by this procedure will be printed for later examination
	@owner varchar(64) = NULL,		-- Use this parameter if you are not the owner of the table
	@ommit_images bit = 0,			-- Use this parameter to generate INSERT statements by omitting the 'image' columns
	@ommit_identity bit = 0,		-- Use this parameter to ommit the identity columns
	@top int = NULL,			-- Use this parameter to generate INSERT statements only for the TOP n rows
	@cols_to_include varchar(8000) = NULL,	-- List of columns to be included in the INSERT statement
	@cols_to_exclude varchar(8000) = NULL,	-- List of columns to be excluded from the INSERT statement
	@disable_constraints bit = 0,		-- When 1, disables foreign key constraints and enables them after the INSERT statements
	@ommit_computed_cols bit = 0		-- When 1, computed columns will not be included in the INSERT statement
	
)
AS
BEGIN

/***********************************************************************************************************
Procedure:	sp_generate_inserts  (Build 22) 
