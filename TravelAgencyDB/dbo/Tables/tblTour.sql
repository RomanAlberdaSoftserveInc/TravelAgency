﻿CREATE TABLE [dbo].[tblTour] (
    [id]                INT           IDENTITY (1, 1) NOT NULL,
    [country]           VARCHAR (255) NOT NULL,
    [city]              VARCHAR (255) NOT NULL,
    [includedInsurance] BIT           NOT NULL,
    [hotelId]           INT           NULL,
    [tourTypeId]        INT           NULL,
    [createdBy]         INT           NULL,
    [createdAt]         DATETIME      NOT NULL,
    [updatedAt]         DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([createdBy]) REFERENCES [dbo].[tblManager] ([id]),
    FOREIGN KEY ([tourTypeId]) REFERENCES [dbo].[tblTourType] ([id]),
    CONSTRAINT [fk_inv_product_id] FOREIGN KEY ([hotelId]) REFERENCES [dbo].[tblHotel] ([id]) ON DELETE SET NULL
);





