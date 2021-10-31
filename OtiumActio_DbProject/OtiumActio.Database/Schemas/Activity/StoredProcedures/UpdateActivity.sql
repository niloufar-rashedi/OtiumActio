  create  PROCEDURE dbo.UpdateActivity
    ( 
    @activityId int,
    @categoryId int,
    @description VARCHAR(50) ,
    @participants TINYINT,
    @date DATETIME
    )
    AS  
    DECLARE @modified DATETIME;
    BEGIN
    SET @modified = GETDATE()

    update [Activity].[Activity] 
    set 
    Ac_CategoryId = @categoryId,
    Ac_Description= @description,
    Ac_Participants = @participants,
    Ac_Date = @date,
    Ac_Modified = @modified
    WHERE Ac_Id = @activityId;  
    IF @@rowcount <> 1   
    raiserror('Invalid Activity Id',16,1)
    ENd