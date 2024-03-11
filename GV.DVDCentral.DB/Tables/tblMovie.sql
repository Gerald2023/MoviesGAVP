CREATE TABLE [dbo].[tblMovie]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Title] VARCHAR(50) NOT NULL,
	[Description] VARCHAR(50) NOT NULL, 
	[Cost] FLOAT NOT NULL,
	[RatingId] INT NOT NULL,
	[FormatId] INT NOT NULL,
	[DirectorId] INT NOT NULL,
	[InStkQty] INT NOT NULL,
	[ImagePath] VARCHAR(MAX) NOT NULL,


)
