--CREATE SCHEMA Activity

CREATE TABLE [Activity].[Tbl_Category] (
    [Cat_Id] INT IDENTITY(1,1) NOT NULL,
    [Cat_Name] VARCHAR(50) NOT NULL,
    CONSTRAINT [PK_Tbl_Category] PRIMARY KEY  ([Cat_Id] ASC)
);