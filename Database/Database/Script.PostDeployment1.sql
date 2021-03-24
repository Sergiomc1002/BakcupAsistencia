/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

/*
MERGE INTO Id_TIpo AS Target 
USING (VALUES
        (1,'administrador'),
        (2,'secretaria'),
        (3,'docencia')
)
AS Source(TipoID,Tipo)
ON Target.TipoID = Source.TipoID  
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Tipo)
VALUES (Tipo);

MERGE INTO Usuario AS Target 
USING (VALUES
        (6,'admin','1234',1),
        (7,'secre','secre',2),
        (8,'profe','profe',3)
)
AS Source([UserID], Correo, Contraseña,TipoUsuario)
ON Target.UserID = Source.UserID  
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Correo, Contraseña,TipoUsuario)
VALUES (Correo, Contraseña,TipoUsuario);*/