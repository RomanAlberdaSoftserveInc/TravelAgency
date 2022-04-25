CREATE TABLE [dbo].[tblCheck] (
    [id]        INT IDENTITY (1, 1) NOT NULL,
    [tourId]    INT NULL,
    [userId]    INT NULL,
    [managerId] INT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([managerId]) REFERENCES [dbo].[tblManager] ([id]),
    FOREIGN KEY ([tourId]) REFERENCES [dbo].[tblTour] ([id]),
    FOREIGN KEY ([userId]) REFERENCES [dbo].[tblUser] ([id])
);

