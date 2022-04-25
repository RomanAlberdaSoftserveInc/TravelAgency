CREATE TABLE [dbo].[tblTransport] (
    [id]     INT           IDENTITY (1, 1) NOT NULL,
    [number] VARCHAR (255) NOT NULL,
    [type]   VARCHAR (255) NOT NULL,
    [model]  VARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

