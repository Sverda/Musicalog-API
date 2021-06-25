CREATE TABLE [dbo].[Albums] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Title]    VARCHAR (100) NOT NULL,
    [ArtistId] INT           NOT NULL,
    [Type]     INT           NOT NULL,
    [Stock]    INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Album_Artists] FOREIGN KEY ([ArtistId]) REFERENCES [dbo].[Artists] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

