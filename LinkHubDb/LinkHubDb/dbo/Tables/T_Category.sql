CREATE TABLE [dbo].[T_Category] (
    [CategoryId]   INT           IDENTITY (1000, 1) NOT NULL,
    [CategoryName] VARCHAR (50)  NOT NULL,
    [CategoryDesc] VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_T_Category] PRIMARY KEY CLUSTERED ([CategoryId] ASC)
);

