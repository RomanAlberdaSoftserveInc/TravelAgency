CREATE TABLE [dbo].[tblTour] (
    [id]                INT           IDENTITY (1, 1) NOT NULL,
    [country]           VARCHAR (255) NOT NULL,
    [city]              VARCHAR (255) NOT NULL,
    [includedInsurance] BIT           NOT NULL,
    [createdAt]         DATETIME      DEFAULT (sysdatetime()) NOT NULL,
    [updatedAt]         DATETIME      NULL,
    [hotelId]           INT           NULL,
    [tourTypeId]        INT           NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([hotelId]) REFERENCES [dbo].[tblHotel] ([id]),
    FOREIGN KEY ([tourTypeId]) REFERENCES [dbo].[tblTourType] ([id])
);

