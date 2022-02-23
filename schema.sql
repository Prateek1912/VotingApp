if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='voters')
begin
create table voters(name varchar(50) not null,aadhar char(12) not null unique,voterid int primary key);
end

if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='parties')
begin
create table parties(name varchar(50) not null,partyid int primary key);
end

if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='votingstatus')
begin
create table votingstatus(voterid int not null,electionid int not null,hasvoted bit not null);
end

if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='partystatus')
begin
create table partystatus(partyid int not null,electionid int not null,votes int not null);
end