CREATE TABLE [dbo].[tblManagerTour] (
    [id]        INT IDENTITY (1, 1) NOT NULL,
    [managerId] INT NULL,
    [userId]    INT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([managerId]) REFERENCES [dbo].[tblManager] ([id]),
    FOREIGN KEY ([userId]) REFERENCES [dbo].[tblUser] ([id])
);

