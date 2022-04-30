CREATE TABLE [dbo].[tblRoleManager] (
    [id]        INT IDENTITY (1, 1) NOT NULL,
    [roleId]    INT NULL,
    [managerId] INT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([managerId]) REFERENCES [dbo].[tblManager] ([id]),
    FOREIGN KEY ([roleId]) REFERENCES [dbo].[tblRole] ([id])
);

