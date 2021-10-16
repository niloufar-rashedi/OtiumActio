Create procedure GetActivityDetails(
    @id int
)
as  
begin  
   select * from [Activity].[Tbl_Activity] where Ac_Id = @id
End
