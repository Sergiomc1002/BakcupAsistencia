﻿CREATE TABLE [dbo].[PDF]
(
	[ID_PDF]	INT				IDENTITY (1, 1) NOT NULL,
	[PDF_File]		VARBINARY(max)  NOT NULL,
	PRIMARY KEY CLUSTERED ([ID_PDF] ASC)
)
