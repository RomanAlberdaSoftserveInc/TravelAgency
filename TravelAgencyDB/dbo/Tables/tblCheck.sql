CREATE TABLE [dbo].[tblCheck] (
    [id]            INT           IDENTITY (1, 1) NOT NULL,
    [price]         VARCHAR (55)  NOT NULL,
    [personCount]   INT           NOT NULL,
    [tourId]        INT           NULL,
    [userId]        INT           NULL,
    [travelAgentId] INT           NULL,
    [createdAt]     DATETIME2 (7) DEFAULT (sysdatetime()) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([tourId]) REFERENCES [dbo].[tblTour] ([id]),
    FOREIGN KEY ([travelAgentId]) REFERENCES [dbo].[tblTravelAgent] ([id]),
    FOREIGN KEY ([userId]) REFERENCES [dbo].[tblUser] ([id])
);



