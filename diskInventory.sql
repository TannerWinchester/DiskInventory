/***************************************************************************************
* DATE			PROGRAMMER			DESCRIPTION
*--------		-----------			-----------
*10/8/2021		TANNER WINCHESTER	INITIAL IMPLEMENTATION OF DISK DB.
*10/13/2021		Tanner winchester	updated tables and added more memory to char data types.
*10/14/2021		Tanner Winchester	Added insert statements to populate db.
*10/15/2021		Tanner Winchester	finsished populating data.
*10/20/2021		Tanner Winchester	Edited and fixed some minor bugs in tabels
*10/21/2021		Tanner Winchester	changed artist table to nvarchar and added individual view for artist
*10/25/2021		Tanner Winchester	add insert and update sp's for disk has borrower.
*10/27/2021		Tanner Winchester	Add ins, upd & del sp's for artsist, and borrower & disk.
****************************************************************************************/
-- drop and create db
USE master;

DROP DATABASE IF EXISTS disk_inventorytw;
go

CREATE DATABASE disk_inventorytw;
go

IF SUSER_ID('ProjectUserTW') IS NULL
		CREATE LOGIN ProjectUserTW WITH PASSWORD = 'MSPress#1',
		DEFAULT_DATABASE = disk_inventorytw;
go

USE disk_inventorytw;
go

CREATE USER ProjectUserTw;
go

--grant read-all to new user
ALTER ROLE db_datareader ADD MEMBER ProjectUserTW;
go

--create lookup tables disk_type, genre, artist_type
CREATE TABLE diskType
(diskTypeID		INT 		NOT NULL PRIMARY KEY IDENTITY,
description		char(10)	NOT NULL);
go

CREATE TABLE genreType
(genreID		INT 		NOT NULL PRIMARY KEY IDENTITY,
description		char(10)	NOT NULL);
go

CREATE TABLE artistType
(artistTypeID		INT 		NOT NULL PRIMARY KEY IDENTITY,
description		char(10)	NOT NULL);
go
select *
from artistType
-- create the rest of the tables
CREATE TABLE status
(statusID		INT 		NOT NULL PRIMARY KEY IDENTITY,
description		char(10)	NOT NULL);
go

CREATE TABLE borrower
(borrowerID			INT			NOT NULL PRIMARY KEY IDENTITY,
borrowerFName		CHAR(60)	NOT NULL,
borrowerLName		CHAR(60)	NOT NULL,
borrowerPhoneNum	CHAR(20)	NOT NULL
);

CREATE TABLE artist
(artistID		INT			NOT NULL PRIMARY KEY IDENTITY,	
artistName		NVARCHAR(60)	NOT NULL,
artistTypeID	INT			NOT NULL REFERENCES artistType(artistTypeID));


CREATE TABLE diskHasArtist
(diskArtist		INT	NOT NULL PRIMARY KEY IDENTITY,
artistID		INT			NOT NULL REFERENCES artist(artistID),
cdID			INT			NOT NULL);


CREATE TABLE disk
(cdID			INT			NOT NULL PRIMARY KEY IDENTITY,
cdName			CHAR(60)	NOT NULL,
releaseDate		DATE		NOT NULL, 
artistID		INT			NOT NULL REFERENCES artist(artistID),
genreID			INT			NOT NULL REFERENCES genreType(genreID),
statusID		INT			NOT NULL REFERENCES status(statusID),
diskTypeID		INT			NOT NULL REFERENCES diskType(diskTypeID));

CREATE TABLE diskHasBorrower
(diskBorrower	INT		NOT NULL PRIMARY KEY IDENTITY,
borrowerID		INT		NOT NULL REFERENCES borrower(borrowerID),
borrowedDate	DATE	NOT NULL,
returnedDate	DATE	NULL,
cdID			INT		NOT NULL REFERENCES Disk(cdID));

--populate tables in order  20 ROWS IN each except the lookup tables.
use disk_inventorytw
go
INSERT INTO diskType (description) --inserting types of cd's
VALUES
	('CD'),
	('Vinyl'),
	('8Track'),
	('Cassette'),
	('DVD');

select * 
from diskType

INSERT INTO genreType (description) --inserting genre data
VALUES
	('Pop'),
	('Rock'),
	('Country'),
	('Hip-Hop'),
	('Jazz'),
	('Metal'),
	('EDM');

INSERT INTO artistType (description) --inserting artist types
VALUES
	('Solo'),
	('Group');

INSERT INTO status (description) --giving status for cd's
VALUES
	('Available'),
	('On Loan'),
	('Damaged'),
	('Missing');

INSERT INTO borrower -- inserting customers who borrow the disks
	(borrowerFName, borrowerLName, borrowerPhoneNum)
VALUES
	('John', 'Doe', 2086586566),
	('Elon', 'Musk', 2086586565),
	('Bill', 'Gates', 2086586564),
	('Josh', 'Low', 2086586563),
	('Jen', 'Castro', 208658652),
	('Joe', 'Yunne', 2086586561),
	('Cam', 'Newton', 2086586560),
	('Bucky', 'Lanes', 2086586569),
	('Jim', 'Deer', 2086586568),
	('Donald', 'Trump', 2086586512),
	('Leerie', 'Lass', 2086586523),
	('Albert', 'Henry', 2086586534),
	('Lisa', 'Bendry', 2086586545),
	('Brady', 'Hacket', 2086586556),
	('Justin', 'Sun', 2086586567),
	('Satoshi', 'Nakomoto', 2086586578),
	('Jake', 'Lenneth', 2086586589),
	('Luke', 'Skywalker', 2086586598),
	('Ryan', 'Grands', 2086586587),
	('Lesly', 'Fennic', 2086586565);
select *
from borrower;

delete 
from borrower
where borrowerID = 22;


INSERT INTO artist -- populating artists and status
	(artistName, artistTypeID)
VALUES
	('Drake', 1),
	('Ariana Grande', 1),
	('Justin Bieber', 1),
	('AC/DC', 2),
	('The Beatles', 2),
	('21 Savage', 1),
	('Lord Huron', 2),
	('Hanz Zimmer', 1),
	('Avici', 1),
	('Illenium', 2),
	('John Legend', 1),
	('Black Shelton', 1),
	('Ed Sheeran', 1),
	('3 doors down', 2),
	('Linkin Park', 2),
	('Biggie Smalls', 1),
	('Lil Wayne', 1),
	('BlackBear', 1),
	('Luke Combs', 1),
	('Kane Brown', 2);
select *
from artist;


INSERT INTO diskHasArtist 
	(artistID, cdID)
VALUES
	(2, 1),
	(3, 2),
	(4, 3),
	(5, 4),
	(6, 5),
	(7, 6),
	(8, 7),
	(9, 8),
	(10, 9),
	(11, 10),
	(12, 11),
	(13, 12),
	(14, 13),
	(15, 14),
	(16, 15),
	(17,16),
	(18, 17),
	(19, 18),
	(20, 19);

INSERT INTO disk -- creating disks and artists on them 
	(cdName, releaseDate, artistID, genreID, statusID, diskTypeID)
VALUES
	('More Life', '3-18-2017', 2, 4, 1, 2),

	('Thank u Next', '1-18-2017', 3, 3, 1, 1),

	('Highway to Hell', '2-18-2017', 2, 4, 2, 2),

	('Purpose', '4-18-2017', 3, 4, 1, 1),

	('Abbey Road', '5-18-2017', 2, 3, 2, 2),

	('Savage Mode', '6-18-2017', 2, 2, 1, 5),

	('Strange Trails', '7-18-2017', 2, 1, 2, 5),

	('Kung Fu Panda', '8-18-2017', 2, 1, 1, 4),

	('Stories', '9-18-2017', 2, 3, 1, 3),

	('Fallen Embers', '3-28-2017', 2, 3, 2, 1),

	('All of you', '3-19-2017', 2, 4, 1, 2),

	('Ol Red', '3-11-2017', 2, 2, 1, 2),

	('Divide', '3-17-2017', 2, 4, 1, 3),

	('Here Without You', '3-10-2017', 2, 2, 1, 4),

	('In the End', '3-1-2017', 2, 4, 1, 5),

	('The Carter', '3-3-2017', 2, 1, 1, 1),

	('Wanderlust', '3-6-2017', 2, 3, 1, 2),

	('This Ones for you', '3-5-2017', 2, 3, 1, 3),

	('Nothin was the same', '3-4-2017', 2, 1, 1, 1),

	('You', '3-20-2017', 2, 3, 1, 2);

UPDATE disk 
set cdName = 'Updated You'
where cdID = 35;

select *
from disk;

INSERT INTO diskHasBorrower
	(borrowerID, cdID, borrowedDate, returnedDate)
VALUES
	(1, 1, '1-1-2021', '2-3-2012'),
	(3, 12, '1-2-2021', '2-2-2012'),
	(4, 16, '1-3-2021', '2-1-2012'),
	(5, 17, '1-4-2021', '2-6-2012'),
	(6, 18, '1-5-2021', '2-12-2012'),
	(7, 19, '1-6-2021', '2-15-2012'),
	(8, 20, '1-7-2021', '2-4-2012'),
	(9, 20, '1-8-2021', '2-3-2012'),
	(10, 12, '1-9-2021', '2-8-2012'),
	(11, 18, '1-10-2021', '2-15-2012'),
	(12, 17, '1-20-2021', '2-9-2012'),
	(4, 16, '1-13-2021', '2-12-2012'),
	(1, 15, '1-14-2021', '2-4-2012'),
	(3, 14, '1-22-2021', '2-1-2012'),
	(13, 13, '1-13-2021', '2-17-2012'),
	(14, 12, '1-16-2021', '2-20-2012'),
	(15, 11, '1-22-2021', NULL),
	(9, 11, '1-30-2021', NULL),
	(6, 9, '1-19-2021', NULL),
	(8, 10, '1-23-2021', NULL);

GO

select * 
from diskHasBorrower
where returnedDate is NULL;


--project 4 

select cdName, releaseDate, artistName --step 3 show the disks in your database and any associated individual artists only.
from disk
join diskHasArtist
on
disk.cdID = diskHasArtist.cdID
join artist
on
artist.artistID = diskHasArtist.artistID
where statusID = 1;
go

drop view if exists IndividualArtist
GO

CREATE VIEW IndividualArtist  --step 4 create view that shows artists names and no group names.
AS
	select artistID, artistName, iif(charindex(' ', artistName) > 0 , 
                left(artistName, charindex(' ', artistName) - 1), artistName) as first,
                iif(charindex(' ', artistName) > 0 ,
                right(artistName, len(artistName) - charindex(' ', artistName)), '') as last
	from artist
	where artistTypeID = 1;
GO
select first
from IndividualArtist;


--5
select cdName, releaseDate, artistName 
from disk
join diskHasArtist
on
disk.cdID = diskHasArtist.cdID
join artist
on
artist.artistID = diskHasArtist.artistID
where artistTypeID = 2;

--6.
select cdName, releaseDate, artistName 
from disk
join diskHasArtist
on
disk.cdID = diskHasArtist.cdID
join artist
on
artist.artistID = diskHasArtist.artistID
where artist.artistID NOT IN 
	(select IndividualArtist.artistID
	from IndividualArtist);
go


--7.
select borrowerFName, borrowerLName, disk.cdID, borrowedDate, returnedDate, cdName
from diskHasBorrower
join disk
on 
disk.cdID = diskHasBorrower.cdID
join borrower
on
borrower.borrowerID = diskHasBorrower.borrowerID
go




--8.
select  count(disk.cdID) AS TimesBorrowed , cdName
from diskHasBorrower
join disk
on 
disk.cdID = diskHasBorrower.cdID
join borrower
on
borrower.borrowerID = diskHasBorrower.borrowerID
group by disk.cdID, cdName
go



--9.
select cdName, borrowedDate, returnedDate, borrowerLName
from diskHasBorrower
join disk
on
disk.cdID = diskHasBorrower.cdID
join borrower
on
borrower.borrowerID = diskHasBorrower.borrowerID
where returnedDate is null
order by cdName asc;





--LAB WK5DAY1 CHAPTER 15
use disk_inventorytw;
go
--1.
DROP PROC IF EXISTS sp_ins_diskHasBorrower
GO
CREATE PROC sp_ins_diskHasBorrower
	@borrowerID int, @cdID int, @borrowedDate date, @returnedDate date = NULL
AS
BEGIN TRY
	INSERT INTO diskHasBorrower
		(borrowerID, cdID, borrowedDate, returnedDate)
		VALUES
			(@borrowerID, @cdID, @borrowedDate, @returnedDate);
END TRY
BEGIN CATCH
	print 'an error ocured. row was not inserted.';
	PRINT 'Error number: ' + CONVERT(varchar, ERROR_NUMBER());
	print 'error message: ' + CONVERT(varchar(255), ERROR_MESSAGE());
END CATCH
GO
EXEC sp_ins_diskHasBorrower 20, 19, '10/25/2021';
go
EXEC sp_ins_diskHasBorrower 20, 19, '10/01/2021','10/10/2021';
go
EXEC sp_ins_diskHasBorrower 20, 100, '10/04/2021','10/20/2021';
GO
GRANT EXEC ON sp_ins_diskHasBorrower TO ProjectUserTW;
GO
--2.
DROP PROC IF EXISTS sp_upd_diskHasBorrower;
go
CREATE PROC sp_upd_diskHasBorrower
	@borrowerID int, @cdID int, @borrowedDate date, @returnedDate date = NULL
AS
BEGIN TRY
UPDATE diskHasBorrower
	SET cdID = @cdID, borrowerID = @borrowerID, borrowedDate = @borrowedDate, returnedDate = @returnedDate
	WHERE borrowerID = @borrowerID;
END TRY
BEGIN CATCH
	print 'an error ocured. row was not updated.';
	PRINT 'Error number: ' + CONVERT(varchar, ERROR_NUMBER());
	print 'error message: ' + CONVERT(varchar(255), ERROR_MESSAGE());
END CATCH
GO
EXEC sp_upd_diskHasBorrower 12, 1, '1/1/2021', '2/2/2021';
go
EXEC sp_upd_diskHasBorrower 12, 1, '1/1/2021';
go
EXEC sp_upd_diskHasBorrower 22, 111, '1/1/2021';
go
GRANT EXEC ON sp_upd_diskHasBorrower TO ProjectUserTW;
GO

----------------PROJECT 5 add stored procedures for artist, borrower and disk--------------------------

--2. artist
DROP PROC IF EXISTS sp_ins_artist
GO
CREATE PROC sp_ins_artist
	@artistName nvarchar(60), @artistTypeID int
AS
BEGIN TRY
INSERT INTO artist
	(artistName, artistTypeID)
VALUES
	(@artistName, @artistTypeID);
END TRY
BEGIN CATCH
	PRINT 'an error ocured. row was not inserted.';
	PRINT 'Error number: ' + CONVERT(varchar, ERROR_NUMBER());
	PRINT 'Error message: ' + CONVERT(varchar(255), ERROR_MESSAGE());
END CATCH
GO
EXEC sp_ins_artist 'Cher', 1;
GO
GRANT EXEC ON sp_ins_artist TO ProjectUserTW;
GO
--update artist
DROP PROC IF EXISTS sp_upd_artist
GO
CREATE PROC sp_upd_artist
	@artistID int, @artistName nvarchar(60), @artistTypeID int
AS
BEGIN TRY
UPDATE artist
SET artistName = @artistName, artistTypeID = @artistTypeID
WHERE artistID = @artistID;
END TRY
BEGIN CATCH
	PRINT 'an error ocured. row was not updated.';
	PRINT 'Error number: ' + CONVERT(varchar, ERROR_NUMBER());
	PRINT 'Error message: ' + CONVERT(varchar(255), ERROR_MESSAGE());
END CATCH
GO
EXEC sp_upd_artist 22, 'Cher Updated with variable', 2;
GO
GRANT EXEC ON sp_upd_artist TO ProjectUserTW;
GO

--delete artist
DROP PROC IF EXISTS sp_del_artist
GO
CREATE PROC sp_del_artist
	@artistID int
AS
BEGIN TRY
	DELETE artist
	WHERE artistID = @artistID
END TRY
BEGIN CATCH
	PRINT 'an error ocured. row was not deleted.';
	PRINT 'Error number: ' + CONVERT(varchar, ERROR_NUMBER());
	PRINT 'Error message: ' + CONVERT(varchar(255), ERROR_MESSAGE());
END CATCH
GO
EXEC sp_del_artist 21;
GO
GRANT EXEC ON sp_del_artist TO ProjectUserTW;
go

--3.borrower
DROP PROC IF EXISTS sp_ins_borrower
GO
CREATE PROC sp_ins_borrower
	@borrowerFName char(60), @borrowerLName char(60), @borrowerPhoneNum char(60)
AS
BEGIN TRY
INSERT INTO borrower
	(borrowerFName, borrowerLName, borrowerPhoneNum)
VALUES
	(@borrowerFName, @borrowerLName, @borrowerPhoneNum)
END TRY
BEGIN CATCH
	PRINT 'an error ocured. row was not inserted.';
	PRINT 'Error number: ' + CONVERT(varchar, ERROR_NUMBER());
	PRINT 'Error message: ' + CONVERT(varchar(255), ERROR_MESSAGE());
END CATCH
GO
EXEC sp_ins_borrower 'Tiger','Woods','20896312211';
GO
GRANT EXEC ON sp_ins_borrower TO ProjectUserTW;
GO
select * from borrower;
--UPDATE BORROWER
DROP PROC IF EXISTS sp_upd_borrower
GO
CREATE PROC sp_upd_borrower
	@borrowerID int, @borrowerFName char(60), @borrowerLName char(60), @borrowerPhoneNum char(60)
AS
BEGIN TRY
UPDATE borrower
SET borrowerFName = @borrowerFName, borrowerLName = @borrowerLName, borrowerPhoneNum = @borrowerPhoneNum
WHERE borrowerID = @borrowerID; 
END TRY
BEGIN CATCH
	PRINT 'an error ocured. row was not updated.';
	PRINT 'Error number: ' + CONVERT(varchar, ERROR_NUMBER());
	PRINT 'Error message: ' + CONVERT(varchar(255), ERROR_MESSAGE());
END CATCH
GO
EXEC sp_upd_borrower 21, 'Tiger', 'Woods Updated', '208909363321';
GO
GRANT EXEC ON sp_upd_borrower TO ProjectUserTW;
GO
--DELETE BORROWER
DROP PROC IF EXISTS sp_del_borrower
GO
CREATE PROC sp_del_borrower
	@borrowerID int
AS
BEGIN TRY
	DELETE borrower
	WHERE borrowerID = @borrowerID
END TRY
BEGIN CATCH
	PRINT 'an error ocured. row was not deleted.';
	PRINT 'Error number: ' + CONVERT(varchar, ERROR_NUMBER());
	PRINT 'Error message: ' + CONVERT(varchar(255), ERROR_MESSAGE());
END CATCH
GO
EXEC sp_del_borrower 21;
GO
GRANT EXEC ON sp_del_borrower TO ProjectUserTW;
GO
--4. disk
DROP PROC IF EXISTS sp_ins_disk
GO
CREATE PROC sp_ins_disk
	@cdName char(60), @releaseDate date, @artistID int, @genreID int, @statusID int, @diskTypeID int
AS
BEGIN TRY
INSERT INTO disk
	(cdName, releaseDate, artistID, genreID, statusID, diskTypeID)
VALUES
	(@cdName, @releaseDate, @artistID, @genreID, @statusID, @diskTypeID);
END TRY
BEGIN CATCH
	PRINT 'an error ocured. row was not inserted.';
	PRINT 'Error number: ' + CONVERT(varchar, ERROR_NUMBER());
	PRINT 'Error message: ' + CONVERT(varchar(255), ERROR_MESSAGE());
END CATCH
GO
EXEC sp_ins_disk 'Journals', '2/14/2015', 1, 2, 1, 3;
GO
GRANT EXEC ON sp_ins_disk TO ProjectUserTW;
GO
--UPDATE DISK
select * from disk;
DROP PROC IF EXISTS sp_upd_disk
GO
CREATE PROC sp_upd_disk
	@cdID int, @cdName char(60), @releaseDate date, @artistID int, @genreID int, @statusID int, @diskTypeID int
AS
BEGIN TRY
UPDATE disk
SET cdName = @cdName, releaseDate = @releaseDate, artistID = @artistID, statusID = @statusID, diskTypeID = @diskTypeID
WHERE cdID = @cdID;
END TRY
BEGIN CATCH
	PRINT 'an error ocured. row was not updated.';
	PRINT 'Error number: ' + CONVERT(varchar, ERROR_NUMBER());
	PRINT 'Error message: ' + CONVERT(varchar(255), ERROR_MESSAGE());
END CATCH
GO
EXEC sp_upd_disk 21, 'Journals Updated', '2/15/2021', 1, 2, 1, 3;
GO
GRANT EXEC ON sp_upd_disk TO ProjectUserTW;
GO
--delete disk
DROP PROC IF EXISTS sp_del_disk
GO
CREATE PROC sp_del_disk
	@cdID int
AS
BEGIN TRY
	DELETE disk
	WHERE cdID = @cdID;
END TRY
BEGIN CATCH
	PRINT 'an error ocured. row was not deleted.';
	PRINT 'Error number: ' + CONVERT(varchar, ERROR_NUMBER());
	PRINT 'Error message: ' + CONVERT(varchar(255), ERROR_MESSAGE());
END CATCH
GO
EXEC sp_del_disk 21;
GO
GRANT EXEC ON sp_del_disk TO ProjectUserTW;
GO