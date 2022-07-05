create database RecycleProjectDB
go

-------------------------
create table Bottle_Categories(
Id int IDENTITY (1,1) primary key,
[Name] nvarchar(200) not null,
Recycle_Price float not null,
Photo_Url nvarchar(max)
)
go


create table Countries(
Id int IDENTITY (1,1) primary key,
[Name] nvarchar(15) not null
)
go

create table Cities(
Id int identity (1,1) primary key,
[Name] nvarchar(15) not null,
[Country_Id] int foreign key references Countries(Id),
)
go

create table Addresses(
Id int identity (1,1) primary key,
[City_Id] int foreign key references Cities(Id) not null,
[Address] nvarchar(40) not null,
Coordinates nvarchar(max) 
)
go

create table Collectors(
Id int identity (1,1) primary key,
First_Name nvarchar(40) not null,
Last_Name nvarchar(40) not null,
Address_Id int foreign key references Addresses(Id) not null,
Tel nvarchar(40),
Current_Num_Of_Bottles int,
Total_History_Num_Of_Bottles int,
Current_Worth float,
Total_Worth_History float 
)
go

create table Recyclers(
Id int identity (1,1) primary key,
First_Name nvarchar(40) not null,
Last_Name nvarchar(40) not null,
Address_Id int foreign key references Addresses(Id) not null,
Tel nvarchar(40),
Current_Num_Of_Bottles int,
Total_History_Num_Of_Bottles int,
Current_Worth float,
Total_Worth_History float,
Num_Of_Collected_Points int
)
go

create table Bottles(
Id int identity (1,1) primary key,
[Category_Id] int foreign key references Bottle_Categories(Id) not null,
[Collector_Id] int foreign key references Collectors(Id),
[Recycler_Id] int foreign key references Recyclers(Id),
)
go
