CREATE PROCEDURE UpdateActCat
(   
    @activity int,
    @category int
)
AS  
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
		SAVE TRANSACTION SavePoint;
		SELECT @@TRANCOUNT AS [TranCount];
		INSERT INTO [Activity].[Tbl_ActivityCategory]
        (Acat_CategoryId, Acat_ActivityId)
        VALUES(@category, @activity)

		COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION SavePoint
        END
        SELECT
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_STATE() AS ErrorState,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_PROCEDURE() AS ErrorProcedure,
        ERROR_LINE() AS ErrorLine,
        ERROR_MESSAGE() AS ErrorMessage;
        END CATCH
END