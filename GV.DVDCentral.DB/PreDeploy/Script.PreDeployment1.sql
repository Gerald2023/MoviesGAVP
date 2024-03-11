/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

DROP TABLE IF EXISTS tblCustomer
DROP TABLE IF EXISTS tblDirector
DROP TABLE IF EXISTS tblFormat
DROP TABLE IF EXISTS tblGenre
DROP TABLE IF EXISTS tblMovie
DROP TABLE IF EXISTS tblMovieGenre
DROP TABLE IF EXISTS tblOrder
DROP TABLE IF EXISTS tblOrderItem
DROP TABLE IF EXISTS tblRating
DROP TABLE IF EXISTS tblUser










