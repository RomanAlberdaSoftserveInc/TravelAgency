CREATE TABLE [dbo].[tblManager] (
    [id]      INT           IDENTITY (1, 1) NOT NULL,
    [name]    VARCHAR (255) NOT NULL,
    [surname] VARCHAR (255) NOT NULL,
    [phone]   VARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

