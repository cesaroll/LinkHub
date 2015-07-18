CREATE TABLE [dbo].[T_User] (
    [UserId]    INT          IDENTITY (1, 1) NOT NULL,
    [UserEmail] VARCHAR (50) NOT NULL,
    [Password]  VARCHAR (50) NOT NULL,
    [Role]      VARCHAR (50) CONSTRAINT [DF_T_User_Role] DEFAULT ('U') NULL,
    CONSTRAINT [PK_T_User] PRIMARY KEY CLUSTERED ([UserId] ASC)
);

