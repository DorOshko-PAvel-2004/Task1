
CREATE TABLE dbo.Task1
	(
	Id int primary key identity(1,1),
	TaskDate date NULL,
	TaskEngSymbols nchar(10) NULL,
	TaskRusSymbols nchar(10) NULL,
	TaskInt int NULL,
	TaskFloat decimal(10, 8) NULL
	)  
GO
drop table Task1

