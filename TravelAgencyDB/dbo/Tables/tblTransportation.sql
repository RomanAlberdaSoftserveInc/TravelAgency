CREATE TABLE [dbo].[tblTransportation] (
    [id]                INT           IDENTITY (1, 1) NOT NULL,
    [transport_id]      INT           NULL,
    [depature_location] VARCHAR (255) NOT NULL,
    [arrival_location]  VARCHAR (255) NOT NULL,
    [depature_date]     DATETIME      NOT NULL,
    [arrival_date]      DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([transport_id]) REFERENCES [dbo].[tblTransport] ([id])
);

