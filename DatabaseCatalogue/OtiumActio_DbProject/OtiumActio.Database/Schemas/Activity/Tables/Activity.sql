CREATE TABLE [Activity].[Tbl_Activity] (
    [Ac_Id] INT IDENTITY(1,1) NOT NULL,
    --[Ac_CategoryId] int FOREIGN KEY REFERENCES Activity.Tbl_ActivityCategory(Acat_CategoryId),  
    [Ac_Description] VARCHAR(50),
    [Ac_Participants] TINYINT,
    [Ac_Date] DATETIME

    CONSTRAINT [PK_Tbl_Activity] PRIMARY KEY ([Ac_Id] ASC)
);

