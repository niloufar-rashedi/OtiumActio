Create procedure GetAllActivities   
as  
Begin  
 Select A.Ac_Id,   A.Ac_Description, A.Ac_Date, C.Cat_Name AS [CategoryName], A.Ac_Participants
 From [Activity].[Tbl_Activity] A 
 Inner join [Activity].[Tbl_Category] C ON C.Cat_Id = A.Ac_CategoryId
End 