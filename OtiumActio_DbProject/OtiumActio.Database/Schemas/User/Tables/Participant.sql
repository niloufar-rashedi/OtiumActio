--CREATE SCHEMA [User]

CREATE TABLE [User].[Tbl_Participant] (
    [Prtc_Id] INT IDENTITY(1,1) NOT NULL,
    [Prtc_ActivityId] INT FOREIGN KEY REFERENCES [Activity].[Tbl_Activity]([Ac_Id]),
    [Prtc_FirstName] VARCHAR(50),
    [Prtc_LastName] VARCHAR(50),
    [Prtc_Age] INT,
    [Prtc_FavouritCategory] INT FOREIGN KEY REFERENCES [Activity].[Tbl_Category]([Cat_Id]),

    CONSTRAINT [PK_Tbl_Participant] PRIMARY KEY  ([Prtc_Id] ASC)
);