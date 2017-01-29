﻿/*
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


-- COMMON DATA
:r ..\Common\Variables.sql

-- PRELOAD DATA

:r ..\PreloadData\AspNetUsers.sql
:r ..\PreloadData\AspNetRoles.sql
:r ..\PreloadData\AspNetUserRoles.sql
:r ..\PreloadData\Branch.sql
:r ..\PreloadData\UserProfile.sql