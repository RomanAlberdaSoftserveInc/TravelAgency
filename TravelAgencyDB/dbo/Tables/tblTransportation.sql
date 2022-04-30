CREATE TABLE [dbo].[tblTransportation] (
    [id]               INT             IDENTITY (1, 1) NOT NULL,
    [transportId]      INT             NULL,
    [depatureLocation] VARCHAR (255)   NOT NULL,
    [arrivalLocation]  VARCHAR (255)   NOT NULL,
    [depatureTime]     DATETIME        NOT NULL,
    [arrivalTime]      DATETIME        NOT NULL,
    [pricePerPerson]   DECIMAL (15, 2) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([transportId]) REFERENCES [dbo].[tblTransport] ([id])
);





