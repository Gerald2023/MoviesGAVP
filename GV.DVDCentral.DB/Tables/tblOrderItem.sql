CREATE TABLE [dbo].[tblOrderItem]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[OrderId] INT NOT NULL,
	[MovieId] INT NOT NULL,
	[Quantity] INT NOT NULL,
	[Cost] FLOAT NOT NULL
)
