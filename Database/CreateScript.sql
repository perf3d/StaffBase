CREATE DATABASE StaffBase;
GO

USE StaffBase;
CREATE TABLE [Employee](
	[Id] uniqueidentifier NOT NULL,
	[Lastname] nvarchar(100) NOT NULL,
	[Firstname] nvarchar(100) NOT NULL,
	[Patronomic] nvarchar(100) NOT NULL,
	[Birthdate] datetime NOT NULL,
	[PassportSeries] nvarchar(4) NOT NULL,
	[PassportNumber] nvarchar(6) NOT NULL,
	[OrganizationId] uniqueidentifier NOT NULL,
	Constraint PK_Employee PRIMARY KEY(Id)
)
CREATE TABLE [Organizations](
	[Id] uniqueidentifier NOT NULL,
	[Name] nvarchar(100) NOT NULL,
	[Inn] nvarchar(10) NOT NULL,
	[LegalAddress] nvarchar(max) NOT NULL,
	[ActualAddress] nvarchar(max) NOT NULL,
	Constraint PK_Organizations PRIMARY KEY(Id)
)
GO

USE StaffBase;
ALTER TABLE [Employee]
ADD CONSTRAINT FK_Employee_Organizations FOREIGN KEY(OrganizationId) REFERENCES Organizations(Id)
GO

INSERT INTO [Organizations] ([Id], [Name], [Inn], [LegalAddress], [ActualAddress])
VALUES 
(
N'A876FE4A-9AA2-4630-A96F-112758109895', 
N'ООО «ДЕПО Электроникс»', 
N'5024096727', 
N'Московская обл., г. Красногорск, тер. коммунальная зона Красногорск-Митино, Д. 12',
N'Московская обл., г. Красногорск, тер. коммунальная зона Красногорск-Митино, Д. 12'
),
(
N'4CE3DB0F-5231-454C-AD7B-3399CE3FA366', 
N'Яндекс, ООО', 
N'7736207543', 
N'г. Москва, вн.тер.г. муниципальный округ Хамовники, ул. Льва Толстого, д. 16',
N'г. Москва, вн.тер.г. муниципальный округ Хамовники, ул. Льва Толстого, д. 16'
);
GO

INSERT INTO [Employee] 
([Id],[Lastname],[Firstname],[Patronomic],[Birthdate],[PassportSeries],[PassportNumber],[OrganizationId])
VALUES
(
N'05282DB0-5C7E-4B08-93DB-88971CBF8CA4',
N'Иванов',
N'Иван',
N'Иванович',
N'10-03-1990',
N'4509',
N'233444',
N'A876FE4A-9AA2-4630-A96F-112758109895'
),
(
N'05282DB0-5C7E-4B08-93DB-88971CBF8CA5',
N'Петров',
N'Петр',
N'Петрович',
N'12-10-1985',
N'4222',
N'567434',
N'A876FE4A-9AA2-4630-A96F-112758109895'
),
(
N'C845AF36-249A-4605-864C-7CF74DBF8577',
N'Сидоров',
N'Сидор',
N'Сидорович',
N'12-10-1975',
N'4202',
N'789797',
N'4CE3DB0F-5231-454C-AD7B-3399CE3FA366'
);