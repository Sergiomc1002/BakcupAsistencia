CREATE TABLE [dbo].[Anuncios]
(
	[Id] INT	IDENTITY (1, 1) NOT NULL,  
    [Fecha] DATE NOT NULL, 
    [Descripcion] VARCHAR(100) NULL
    PRIMARY KEY CLUSTERED ([Id] ASC)
)
