CREATE TABLE [dbo].[T_Url] (
    [UrlId]      INT           IDENTITY (1, 1) NOT NULL,
    [UrlTitle]   VARCHAR (50)  NOT NULL,
    [Url]        VARCHAR (50)  NOT NULL,
    [UrlDesc]    VARCHAR (MAX) NOT NULL,
    [CategoryId] INT           NULL,
    [UserId]     INT           NULL,
    [IsApproved] VARCHAR (1)   CONSTRAINT [DF_T_Url_IsApproved] DEFAULT ('N') NULL,
    CONSTRAINT [PK_T_Url] PRIMARY KEY CLUSTERED ([UrlId] ASC),
    CONSTRAINT [FK_T_Url_T_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[T_Category] ([CategoryId]),
    CONSTRAINT [FK_T_Url_T_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[T_User] ([UserId])
);

