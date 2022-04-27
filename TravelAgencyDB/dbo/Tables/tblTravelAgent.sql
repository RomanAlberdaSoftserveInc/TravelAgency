CREATE TABLE [dbo].[tblTravelAgent] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [name]        VARCHAR (25)  NOT NULL,
    [surname]     VARCHAR (25)  NOT NULL,
    [email]       VARCHAR (255) NOT NULL,
    [phoneNumber] VARCHAR (25)  NOT NULL,
    [agencyId]    INT           NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([agencyId]) REFERENCES [dbo].[tblAgency] ([id])
);

