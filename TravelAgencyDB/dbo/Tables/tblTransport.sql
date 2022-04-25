CREATE TABLE [dbo].[tblTransport] (
    [id]     INT           IDENTITY (1, 1) NOT NULL,
    [number] VARCHAR (255) NULL,
    [type]   VARCHAR (255) NULL,
    [model]  VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

