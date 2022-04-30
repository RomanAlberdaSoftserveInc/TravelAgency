CREATE TABLE [dbo].[tblCheck] (
    [id]          INT          IDENTITY (1, 1) NOT NULL,
    [price]       VARCHAR (55) NOT NULL,
    [personCount] INT          NOT NULL,
    [tourId]      INT          NULL,
    [userId]      INT          NULL,
    [producedBy]  INT          NULL,
    [createdAt]   DATETIME     NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([producedBy]) REFERENCES [dbo].[tblManager] ([id]),
    FOREIGN KEY ([tourId]) REFERENCES [dbo].[tblTour] ([id]),
    FOREIGN KEY ([userId]) REFERENCES [dbo].[tblUser] ([id])
);







