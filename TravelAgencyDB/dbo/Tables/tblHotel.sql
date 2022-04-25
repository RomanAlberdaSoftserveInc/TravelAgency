CREATE TABLE [dbo].[tblHotel] (
    [id]           INT           IDENTITY (1, 1) NOT NULL,
    [name]         VARCHAR (255) NOT NULL,
    [stars]        INT           NULL,
    [eatingTypeId] INT           NULL,
    [description]  VARCHAR (255) NOT NULL,
    [country]      VARCHAR (255) NOT NULL,
    [street]       VARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([eatingTypeId]) REFERENCES [dbo].[tblEatingType] ([id])
);

