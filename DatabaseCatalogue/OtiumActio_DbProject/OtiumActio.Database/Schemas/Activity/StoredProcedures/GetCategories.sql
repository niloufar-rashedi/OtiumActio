Create procedure GetAllCategories   
as  
Begin  
 Select C.Cat_Id,   C.Cat_Name  
 From [Activity].[Tbl_Category] C  
End 

--exec GetAllCategories