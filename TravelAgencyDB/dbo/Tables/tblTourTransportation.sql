CREATE TABLE [dbo].[tblTourTransportation] (
    [id]                INT IDENTITY (1, 1) NOT NULL,
    [transportation_id] INT NULL,
    [tourId]            INT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([tourId]) REFERENCES [dbo].[tblTour] ([id]),
    FOREIGN KEY ([transportation_id]) REFERENCES [dbo].[tblTransportation] ([id])
);

