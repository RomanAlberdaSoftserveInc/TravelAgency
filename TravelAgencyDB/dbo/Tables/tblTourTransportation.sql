CREATE TABLE [dbo].[tblTourTransportation] (
    [id]               INT IDENTITY (1, 1) NOT NULL,
    [transportationId] INT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([transportationId]) REFERENCES [dbo].[tblTransportation] ([id])
);

