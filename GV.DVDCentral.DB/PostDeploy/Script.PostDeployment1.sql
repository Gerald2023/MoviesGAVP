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

:r .\DefaultData\Customers.sql
:r .\DefaultData\Directors.sql
:r .\DefaultData\Formats.sql
:r .\DefaultData\Genres.sql
:r .\DefaultData\MovieGenres.sql
:r .\DefaultData\Movies.sql
:r .\DefaultData\OrderItems.sql
:r .\DefaultData\Orders.sql
:r .\DefaultData\Ratings.sql








