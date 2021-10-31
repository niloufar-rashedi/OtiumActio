    CREATE  PROCEDURE dbo.UpdateParticipant
    ( 
    @prtcId int,
    @prtcPasswordSalt VARbinary(MAX),
    @prtcPasswordHash VARbinary(MAX)
    )
    AS  
    DECLARE @modified DATETIME;
    BEGIN
    SET @modified = GETDATE()

    update [User].[Participant]
    set 
    PrtcPasswordHash= @prtcPasswordHash,
    PrtcPasswordSalt = @prtcPasswordSalt,
    PrtcModified = @modified
    WHERE Prtc_Id = @prtcId;  
    IF @@rowcount <> 1   
    raiserror('Invalid User Id',16,1)
    ENd