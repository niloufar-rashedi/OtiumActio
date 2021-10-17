    CREATE PROCEDURE DeleteActivity (@id int)  
    AS  
    delete from [Activity].[Tbl_Activity] where Ac_Id = @id  
    if @@rowcount <> 1   
    raiserror('Invalid Activity Id',16,1)  