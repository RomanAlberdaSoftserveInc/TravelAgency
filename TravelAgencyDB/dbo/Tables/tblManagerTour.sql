CREATE TABLE [dbo].[tblManagerTour] (
    [id]        INT           IDENTITY (1, 1) NOT NULL,
    [tourId]    INT           NULL,
    [mangerId]  INT           NULL,
    [createdAt] DATETIME2 (7) DEFAULT (sysdatetime()) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([mangerId]) REFERENCES [dbo].[tblManager] ([id]),
    FOREIGN KEY ([tourId]) REFERENCES [dbo].[tblTour] ([id])
);

