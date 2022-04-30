CREATE TABLE [dbo].[tblUser] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [name]        VARCHAR (55)  NOT NULL,
    [surname]     VARCHAR (55)  NOT NULL,
    [email]       VARCHAR (255) NOT NULL,
    [phoneNumber] VARCHAR (25)  NOT NULL,
    [passport]    VARCHAR (55)  NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);



