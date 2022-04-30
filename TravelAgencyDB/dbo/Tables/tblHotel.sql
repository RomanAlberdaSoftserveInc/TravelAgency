CREATE TABLE [dbo].[tblHotel] (
    [id]          INT             IDENTITY (1, 1) NOT NULL,
    [name]        VARCHAR (255)   NOT NULL,
    [stars]       INT             NOT NULL,
    [description] VARCHAR (255)   NULL,
    [pricePerDay] DECIMAL (15, 2) NOT NULL,
    [country]     VARCHAR (255)   NULL,
    [city]        VARCHAR (255)   NULL,
    [address]     VARCHAR (255)   NULL,
    [eatingType]  VARCHAR (255)   NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);



