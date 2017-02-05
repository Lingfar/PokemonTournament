DROP TABLE IF EXISTS [dbo].[Match];
CREATE TABLE [dbo].[Match] (
    [Id]                 INT IDENTITY (1, 1) NOT NULL,
    [IdTournoi]          INT NOT NULL,
    [IdPokemonVainqueur] INT NOT NULL,
    [PhaseTournoi]       INT NOT NULL,
    [Pokemon1]           INT NOT NULL,
    [Pokemon2]           INT NOT NULL,
    [Stade]              INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
DROP TABLE IF EXISTS [dbo].[Tournoi];
CREATE TABLE [dbo].[Tournoi] (
    [Id]                 INT          IDENTITY (1, 1) NOT NULL,
    [Nom]                VARCHAR (50) NOT NULL,
    [IdPokemonVainqueur] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);