    CREATE  PROCEDURE dbo.UpdateActivity
    ( 
    @activityId int,
    @description VARCHAR(50) ,
    @participants TINYINT,
    @date DATETIME
    )
    AS  
    DECLARE @modified DATETIME;
    BEGIN
    SET @modified = GETDATE()

    update [Activity].[Tbl_Activity] 
    set Ac_Description= @description,
    Ac_Participants = @participants,
    Ac_Date = @date,
    Ac_Modified = @modified
    WHERE activityId = @activityId;  
    IF @@rowcount <> 1   
    raiserror('Invalid Activity Id',16,1)
    ENd