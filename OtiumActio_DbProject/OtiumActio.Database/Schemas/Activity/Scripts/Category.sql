DECLARE @Cat_Id INT = 1;

SET IDENTITY_INSERT [Activity].[Tbl_Category] ON
MERGE INTO [Activity].[Tbl_Category] AS TARGET
USING (VALUES
(@Cat_Id ,'Naturvanrding'),
(@Cat_Id+1, 'Idrott'),
(@Cat_Id+2, 'Bio'),
(@Cat_Id+3, 'Matlagning'),
(@Cat_Id+4, 'Föreläsning'),
(@Cat_Id+5, 'Musik')
)As source ([Cat_Id], [Cat_Name])
On target.[Cat_Id] = source.[Cat_Id]

WHEN MATCHED AND (target.[Cat_Name] <> source.[Cat_Name])
	THEN UPDATE SET
	[Cat_Name] = source.[Cat_Name]

WHEN NOT MATCHED BY TARGET
	THEN INSERT ([Cat_Id], [Cat_Name])
	VALUES ([Cat_Id], [Cat_Name])
WHEN NOT MATCHED BY source
	THEN DELETE;

SET IDENTITY_INSERT [Activity].[Tbl_Category] OFF
GO