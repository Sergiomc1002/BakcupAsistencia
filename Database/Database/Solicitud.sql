CREATE TABLE [dbo].[Solicitud]
(
	[SolicitudID]			INT				IDENTITY (1, 1) NOT NULL,
	[Nivel]					NVARCHAR(50)	NOT NULL,
	[Nombre]				NVARCHAR(50)	NOT NULL,
	[NumeroDeCreditos]		SMALLINT		NOT NULL,
	[Semestre]				SMALLINT		NOT NULL,
	[Apellido1]				NVARCHAR(50)	NOT NULL,
	[Apellido2]				NVARCHAR(50)	NOT NULL,
	[CorreoSolicitante]		NVARCHAR(50)	NOT NULL,
	[Promedio]				FLOAT			NOT NULL,
	[Cedula]				NVARCHAR(50)	NOT NULL,
	[Carne]					NVARCHAR(6)		NOT NULL,
	[Telefono1]				INT DEFAULT 0	NULL,
	[Telefono2]				INT	DEFAULT 0	NULL,
	[CarreraQueCursa]		NVARCHAR(50)	NOT NULL,
	[NumeroDeCuenta]		BIGINT	DEFAULT 0	NULL,
    [InformeDeMatricula]	INT				NULL,
    [ExpedienteAcademico]	INT				NULL,
    [Cuentabancaria]        BIT				NOT NULL,
    [FotocopiaCedula]       INT				NULL,
    [Asistencia]			INT			    NULL,
    [TieneHE]				BIT				NOT NULL,
    [CantidadHE]            INT Default 0	NULL,
    [TieneHA]				BIT				NOT NULL,
    [CantidadHA]            INT	Default 0	NULL,
	[Banco]					NVARCHAR(50)    NULL,
	[PromedioRevisado]		FLOAT			NULL,

	PRIMARY KEY CLUSTERED ([SolicitudID] ASC),
	CONSTRAINT [FK_dbo.Solicitud_dbo.PDF_InformeDeMatricula] FOREIGN KEY ([InformeDeMatricula])
		REFERENCES [dbo].[PDF] ([ID_PDF]),
	CONSTRAINT [FK_dbo.Solicitud_dbo.PDF_ExpedienteAcademico] FOREIGN KEY ([ExpedienteAcademico])
		REFERENCES [dbo].[PDF] ([ID_PDF]),
	CONSTRAINT [FK_dbo.Solicitud_dbo.PDF_FotocopiaCedula] FOREIGN KEY ([FotocopiaCedula])
		REFERENCES [dbo].[PDF] ([ID_PDF]),
	CONSTRAINT [FK_dbo.Solicitud_dbo.Asistencia_IDAsistencia] FOREIGN KEY ([Asistencia])
		REFERENCES [dbo].[Asistencia_Disponible] ([IdAsistencia]) 
	
)
