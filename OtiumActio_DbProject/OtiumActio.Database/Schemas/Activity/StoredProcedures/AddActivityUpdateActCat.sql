CREATE PROCEDURE AddActivityUpdateActCat
(   
    @category int,
    @description VARCHAR(50) ,
    @participants TINYINT,
    @date DATETIME
)
AS 
DECLARE @created DATETIME;
BEGIN
SET @created = GETDATE()
INSERT INTO [Activity].[Tbl_Activity] 
(
Ac_CategoryId,Ac_Description,Ac_Participants,Ac_Date, Ac_Created)  
VALUES
(
@category,@description,@participants,@date, @created)

INSERT INTO [Activity].[Tbl_ActivityCategory] (
    [Acat_CategoryId] ,[Acat_ActivityId] ,[Acat_Created]
)
VALUES
(
    @category, SCOPE_IDENTITY(), @created
)

END








------------------------------------------------------------------
/*****Another version that tries to create a temp data with parameters, result is random ids with duplicate rows of into and null activity category table

|
|
|



USE [OtiumActio]
GO
/****** Object:  StoredProcedure [dbo].[AddActivityUpdateActCat]    Script Date: 2021-09-25 13:26:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AddActivityUpdateActCat]
    @category int,
    @description VARCHAR(50) ,
    @participants TINYINT,
    @date DATETIME
AS  
DECLARE @ActivityCategoryTemp TABLE (
    [Acat_CategoryId]          INT,
    [Acat_ActivityId]       INT
	--,
 --   [Acat_Created]       INT
)
Declare @created DATETIME;
Declare @activity int;
BEGIN
	UPDATE [Activity].[Tbl_Activity]
	--SET @created = 

	SET Ac_CategoryId = @category, Ac_Description = @description, Ac_Date = @date, Ac_Participants = @participants
	OUTPUT inserted.Ac_Id, inserted.Ac_CategoryId
	INTO @ActivityCategoryTemp
	Insert INTO [Activity].[Tbl_ActivityCategory](Acat_CategoryId, Acat_ActivityId)
	Select Acat_CategoryId, Acat_ActivityId from @ActivityCategoryTemp
END