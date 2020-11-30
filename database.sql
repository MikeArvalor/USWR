create database USWR

use USWR


create table roles(
id int primary key NOT NULL identity(1,1),
role nvarchar(20) NOT NULL)


create table users(
id uniqueidentifier primary key NOT NULL DEFAULT newid(),
login nvarchar(MAX) NOT NULL,
password nvarchar(MAX) NOT NULL,
role_id int NOT NULL default 2)



create table sites(
id uniqueidentifier primary key NOT NULL DEFAULT newid(),
link nvarchar(MAX) NOT NULL,
header nvarchar(MAX) NOT NULL,
keywords nvarchar(MAX) NOT NULL,
description nvarchar(MAX) NOT NULL
)

create table ratings(
id uniqueidentifier primary key NOT NULL DEFAULT newid(),
site_id uniqueidentifier NOT NULL,
user_id uniqueidentifier NOT NULL,
rating decimal NOT NULL)

create table comments(
id uniqueidentifier primary key NOT NULL DEFAULT newid(),
user_id uniqueidentifier NOT NULL,
site_id uniqueidentifier NOT NULL,
comment nvarchar(max) NOT NULL)

alter table users
add constraint Fk_UserRole_Cascade
foreign key (role_id) references roles(id) on delete cascade

alter table ratings
add constraint Fk_RatingSiteId_Cascade
foreign key (site_id) references sites(id) on delete cascade

alter table ratings
add constraint Fk_RatingUserId_Cascade
foreign key (user_id) references users(id) on delete cascade

alter table comments
add constraint Fk_CommentsSiteId_Cascade
foreign key (site_id) references sites(id) on delete cascade

alter table comments
add constraint Fk_UserCommentsSiteId_Cascade
foreign key (user_id) references users(id) on delete cascade


insert into roles(role) values ('admin')
insert into roles(role) values ('user')

insert into users(login,password,role_id) values('Admin','123456',1)
