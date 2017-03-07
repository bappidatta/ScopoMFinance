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


-- COMMON DATA
:r ..\Common\Variables.sql

-- PRELOAD DATA

:r ..\PreloadData\AspNetUsers.sql
:r ..\PreloadData\AspNetRoles.sql
:r ..\PreloadData\AspNetUserRoles.sql
:r ..\PreloadData\GlobalPolicy.sql
:r ..\PreloadData\SysGender.sql
:r ..\PreloadData\SysDivision.sql
:r ..\PreloadData\SysDistrict.sql
:r ..\PreloadData\SysUpazila.sql
:r ..\PreloadData\SysUnion.sql
:r ..\PreloadData\SysVillage.sql
:r ..\PreloadData\SysDocType.sql
:r ..\PreloadData\Branch.sql
:r ..\PreloadData\UserProfile.sql
:r ..\PreloadData\UserBranch.sql
:r ..\PreloadData\OrgCategory.sql
:r ..\PreloadData\SysColcOption.sql
:r ..\PreloadData\ComponentType.sql
:r ..\PreloadData\Organization.sql
:r ..\PreloadData\AccDayOpenClose.sql
:r ..\PreloadData\EmployeeType.sql
:r ..\PreloadData\Employee.sql