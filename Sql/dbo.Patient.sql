--Select * FROM Patient;

--TRUNCATE TABLE Patient;

--DROP TABLE Patient;

CREATE TABLE [dbo].[Patient]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	[Name] VARCHAR(MAX),
	[FileNo] INT,
	[CitizenId] VARCHAR(MAX),
	[Birthdate] DATE,
	[Gender] TINYINT,
	[Nationality] VARCHAR(MAX),
	[PhoneNumber] VARCHAR(MAX),
	[Email] VARCHAR(MAX),
	[Country] VARCHAR(MAX),
	[City] VARCHAR(MAX),
	[Street] VARCHAR(MAX),
	[Address1] VARCHAR(MAX),
	[Address2] VARCHAR(MAX),
	[ContactPerson] VARCHAR(MAX),
	[ContactRelation] VARCHAR(MAX),
	[ContactPhone] VARCHAR(MAX),
	[Photo] VARCHAR(MAX),
	[FirstVisitDate] DATE,
	[RecordCreationDate] DATE,
)