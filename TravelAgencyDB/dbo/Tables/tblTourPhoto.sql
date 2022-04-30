CREATE TABLE [dbo].[tblTourPhoto] (
    [id]     INT           IDENTITY (1, 1) NOT NULL,
    [isMain] BIT           NULL,
    [url]    VARCHAR (255) NOT NULL,
    [tourId] INT           NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([tourId]) REFERENCES [dbo].[tblTour] ([id])
);

