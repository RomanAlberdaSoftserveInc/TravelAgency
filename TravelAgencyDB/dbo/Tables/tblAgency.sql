CREATE TABLE [dbo].[tblAgency] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [name]       VARCHAR (25)  NOT NULL,
    [address]    VARCHAR (255) NOT NULL,
    [zipCode]    INT           NOT NULL,
    [helpNumber] VARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

