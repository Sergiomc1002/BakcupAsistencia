CREATE TABLE [dbo].[Asistencia_Disponible]
(
	[IdAsistencia]		INT				IDENTITY (1, 1) NOT NULL,
	[Nombre]			NVARCHAR(50)	NOT NULL,
	[Profesor]			NVARCHAR(50)	NULL,
	[Grupos]			INT				NULL,
	PRIMARY KEY CLUSTERED ([IdAsistencia] ASC)
)
