CREATE TABLE [dbo].[tblTour] (
    [id]                   INT           IDENTITY (1, 1) NOT NULL,
    [country]              VARCHAR (255) NOT NULL,
    [includedInsurance]    BIT           NOT NULL,
    [dayDuration]          INT           NOT NULL,
    [tourType]             INT           NULL,
    [tourTransportationId] INT           NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([tourTransportationId]) REFERENCES [dbo].[tblTourTransportation] ([id]),
    FOREIGN KEY ([tourType]) REFERENCES [dbo].[tblTourType] ([id])
);

