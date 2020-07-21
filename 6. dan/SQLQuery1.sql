DROP TABLE PersonCar;
DROP TABLE Person;
DROP TABLE Car;


CREATE TABLE Person(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	FirstName VARCHAR(250), 
	LastName VARCHAR(250),
	Height INT,
	Weight FLOAT
);
CREATE TABLE Car(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(50),
	MaxSpeed FLOAT,
	Color VARCHAR(15)
);
CREATE TABLE PersonCar(
	PersonID int,
	FOREIGN KEY(PersonID) REFERENCES Person(ID) ON DELETE CASCADE,
	CarID INT REFERENCES Car(ID) NOT NULL,
	Price float,
);

INSERT INTO Person(FirstName, LastName,Height,Weight) VALUES ('Pero','Peric', 185, 65.8);
INSERT INTO Person(FirstName, LastName,Height,Weight) VALUES ('Ivo','Ivic', 156, 58.8);
INSERT INTO Person(FirstName, LastName,Height,Weight) VALUES ('Marko','Marulic', 170, 70.1);
INSERT INTO Car VALUES('Mercedes',170.2,'blue');
INSERT INTO Car VALUES('Polo',150,'blue');
INSERT INTO Car VALUES('Renault', 200,'metalic');
INSERT INTO PersonCar VALUES(1,1,7854);
INSERT INTO PersonCar VALUES(1,3,4550);
INSERT INTO PersonCar VALUES(2,3,2789.54);


