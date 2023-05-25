create table login_user
(
User_Name varchar(100) not null primary key,
Pass varchar(100) not null
)

insert into login_user (User_Name,Pass) values ('Yashu','pass')

insert into login_user (User_Name,Pass) values ('Pavani',123)

select * from login_user




create table Student_details
(
Student_id int primary key,
Student_Name varchar(50),
Book_Issued varchar(20)
)
select * from Student_details


create table Books
(
Book_Id int primary key identity,
Title varchar(40),
Author varchar(50),
descr_iption varchar(50),
Student_id int references Student_details(Student_id)
)

drop table Books

create table Books
(
Book_Id int primary key identity,
Title varchar(40),
Author varchar(50),
descr_iption varchar(50),
Availabile varchar(30),
Student_id int references Student_details(Student_id)
)

select * from books

