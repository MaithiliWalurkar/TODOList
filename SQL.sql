go 
use TODO
go 
create table SecurityQue(
    QueId int identity(1, 1) primary key,
	Questions varchar(200) not null
)
insert into SecurityQue values ('What is your nickname?'), ('In what city were you born?'), ('which is your favorite place?'),
('What is your favorite food?'), ('What high school did you attend?');

go 
create table Users(
    UserId int identity(101, 1) primary key,
	Username varchar(50) not null,
	passwords varchar(30) not null,
	Emails varchar(30) not null,
	QueId int references SecurityQue not null,
	Answers varchar(40) not null
)
insert into Users values('Vinita Singh', 'Vinita123', 'vinitasingh@gmail.com', 1, 'Vina');

go 
create table Categories(
    CatId int identity primary key,
	Category varchar(50) not null
)
insert into Categories values('Entertainment');

go 
create table Taskss(
    TaskId int identity primary key,
	CatId int references Categories not null,
	UserId int references Users not null,
	Task varchar(100) not null,
)
insert into Taskss values (1, 101, 'Goa Trip')
select * from Taskss
alter table Taskss drop column UserId;

go
Alter PROCEDURE [dbo].[usersSP] (
    @Userid int = null,
	@Username varchar(100) = null,
	@Pass varchar(30) = null,
	@Email varchar(30) = null,
	@Queid int = null,
	@Questions varchar(100) = null,
	@Answers varchar(50) = null,
	@Catid int = null,
	@Category varchar(30) = null,
	@Task varchar(100) = null,
	@Action varchar(300)
)
as
Begin 
    if @Action = 'Secquestions'
	BEGIN
        SELECT * FROM SecurityQue;
    END
	if @Action = 'InsertData'
	BEGIN
        INSERT INTO Users (Username, passwords, Emails, QueId, Answers) VALUES (@Username, @Pass, @Email, @Queid, @Answers);
    END
	if @Action = 'Check'
	BEGIN
        SELECT * FROM Users WHERE Username = @Username and passwords = @pass or Emails = @Email;
    END
	if @Action = 'Categ'
	BEGIN
        SELECT * FROM Categories;
    END
	if @Action = 'AddTask'
	BEGIN
        INSERT INTO Taskss(CatId, Userid, Task) VALUES (@Catid, @Userid, @Task);
    END
	if @Action ='Getid'
	BEGIN 
	    select sq.Questions, u.QueId, u.Answers, u.Username, u.passwords, u.UserId
        from Users u
        inner join SecurityQue sq 
        on u.QueId = sq.QueId
        where Username = @Username;
	END
	if @Action ='ChangePass'
	BEGIN 
	    UPDATE Users
		SET passwords = @Pass WHERE Username = @Username;
	END
	if @Action = 'GetTask'
	BEGIN
	    select c.Category, t.Task
        from Taskss t
        inner join Categories c 
        on c.CatId = t.CatId
        where t.UserId = @Userid and t.CatId = @Catid;
	END
End

select * from Users
select * from Taskss
select * from Categories

select c.Category, t.Task
from Categories c
left join Taskss t 
on c.CatId = t.CatId
where t.CatId = 1 and t.UserId = 102;