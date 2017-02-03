DROP TABLE IF EXISTS [dbo].[Utilisateur];
DROP TABLE IF EXISTS [dbo].[Tournoi];
DROP TABLE IF EXISTS [dbo].[Caracteristique];
DROP TABLE IF EXISTS [dbo].[Pokemon];
DROP TABLE IF EXISTS [dbo].[Stade];
DROP TABLE IF EXISTS [dbo].[Match];
DROP TABLE IF EXISTS [dbo].[TournoiMatches];
DROP TABLE IF EXISTS [dbo].[TournoiPokemons];
DROP TABLE IF EXISTS [dbo].[TournoiStades];

CREATE TABLE [dbo].[Utilisateur] (
    [Login]    VARCHAR (50)  NOT NULL,
    [Nom]      VARCHAR (100) NOT NULL,
    [Prenom]   VARCHAR (100) NOT NULL,
    [Password] VARCHAR (200) NOT NULL,
    PRIMARY KEY CLUSTERED ([Login] ASC)
);

CREATE TABLE [dbo].[Tournoi] (
    [Id]  INT          IDENTITY (1, 1) NOT NULL,
    [Nom] VARCHAR (50) NOT NULL,
    [IdPokemonVainqueur] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Caracteristique] (
    [Id]      INT IDENTITY (1, 1) NOT NULL,
    [PV]      INT NOT NULL,
    [Attaque] INT NOT NULL,
    [Defense] INT NOT NULL,
    [Vitesse] INT NOT NULL,
    [Esquive] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Pokemon] (
    [Id]               INT          IDENTITY (1, 1) NOT NULL,
    [Nom]              VARCHAR (50) NOT NULL,
    [Type]             INT          NOT NULL,
    [Caracteristiques] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Stade] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [Nom]      VARCHAR (50) NOT NULL,
    [Type]     INT          NOT NULL,
    [NbPlaces] INT          NOT NULL,
    [Attaque]  INT          NOT NULL,
    [Defense]  INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Match] (
    [Id]                 INT IDENTITY (1, 1) NOT NULL,
    [IdPokemonVainqueur] INT NOT NULL,
    [PhaseTournoi]       INT NOT NULL,
    [Pokemon1]           INT NOT NULL,
    [Pokemon2]           INT NOT NULL,
    [Stade]              INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[TournoiPokemons] (
    [IdTournoi] INT NOT NULL,
    [IdPokemon] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([IdTournoi] ASC, [IdPokemon] ASC)
);

CREATE TABLE [dbo].[TournoiMatches] (
    [IdTournoi] INT NOT NULL,
    [IdMatch]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([IdTournoi] ASC, [IdMatch] ASC)
);

CREATE TABLE [dbo].[TournoiStades] (
    [IdTournoi] INT NOT NULL,
    [IdStade]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([IdTournoi] ASC, [IdStade] ASC)
);