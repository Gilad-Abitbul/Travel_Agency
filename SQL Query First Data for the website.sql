USE tempdb
GO
DROP TABLE tblCreditCards;
DROP TABLE tblTickets;
DROP TABLE tblFlights;
DROP TABLE tblAirplanes;
DROP TABLE tblUsers;


CREATE TABLE tblUsers (
	firstName nvarchar(20) NOT NULL,
	lastName nvarchar(20) NOT NULL,
	email nvarchar(30) PRIMARY KEY,
	password nvarchar(12) NOT NULL,
	is_admin bit NOT NULL
);

INSERT INTO tblUsers(firstName, lastName, email, password, is_admin) VALUES ('admin', 'admin' ,'admin@admin.com','Admin123$', 1);

CREATE TABLE tblAirplanes (
	airplane_identifier int IDENTITY(1,1),
	airplane_name nvarchar(30) NOT NULL,

	premium_seats int DEFAULT 0,
	business_seats int DEFAULT 0,
	economy_seats int NOT NULL,
	PRIMARY KEY(airplane_identifier),

	CHECK (
		premium_seats >= 0 AND
		business_seats>= 0 AND
		economy_seats >= 0
	)
);

INSERT INTO tblAirplanes (airplane_name, premium_seats, business_seats, economy_seats) VALUES ('BOEING:787-9', 35, 20, 183); 
INSERT INTO tblAirplanes (airplane_name, premium_seats, business_seats, economy_seats) VALUES ('BOEING:737-900-ER', 0, 16, 159); 
INSERT INTO tblAirplanes (airplane_name, premium_seats, business_seats, economy_seats) VALUES ('BOEING:777-200', 0, 41, 238); 

CREATE TABLE tblFlights (
	flight_identifier nvarchar(10) NOT NULL,
	airplane_identifier int NOT NULL,

	departure_time datetime NOT NULL,
	departure_country nvarchar(10) NOT NULL,
	departure_airport nvarchar(20) NOT NULL,

	landing_country nvarchar(10) NOT NULL,
	landing_airport nvarchar(20) NOT NULL,

	airline nvarchar(20) NOT NULL,

	gate_identifier nvarchar(4) NOT NULL,

	economy_seat_price int NOT NULL,
	remain_economy_seats int NOT NULL,

	business_seat_price int,
	remain_business_seats int DEFAULT 0,

	premium_seat_price int,
	remain_premium_seats int DEFAULT 0,

	CONSTRAINT fk_airplaneID FOREIGN KEY (airplane_identifier) 
	REFERENCES tblAirplanes(airplane_identifier),

	PRIMARY KEY(flight_identifier),
);

INSERT INTO tblFlights(flight_identifier, airplane_identifier, departure_time,
	departure_country, departure_airport, landing_country,
	landing_airport, airline, gate_identifier,
	economy_seat_price, premium_seat_price, business_seat_price,
	remain_premium_seats, remain_business_seats, remain_economy_seats)
VALUES ('TLV-548-94', 1, '2023-03-26 16:05:00',
	'ISRAEL', 'BNG-TLV', 'SPAIN',
	'MAD-MADRID BARAJAS', 'Air Europa', 'G30',
	933, 1299, 1499,
	 35, 20, 183); 

INSERT INTO tblFlights(flight_identifier, airplane_identifier, departure_time,
	departure_country, departure_airport, landing_country,
	landing_airport, airline, gate_identifier,
	economy_seat_price, premium_seat_price, business_seat_price,
	remain_premium_seats, remain_business_seats, remain_economy_seats)
VALUES ('TLV-964-18', 2, '2023-02-10 21:10:00',
		'ISRAEL', 'BNG-TLV', 'ITALY',
		'FCO-ROMA FIUMICINO', 'Ryanair', 'D15',
		288, 0, 699,
		0, 16, 159); 

INSERT INTO tblFlights(flight_identifier, airplane_identifier, departure_time,
	departure_country, departure_airport, landing_country,
	landing_airport, airline, gate_identifier,
	economy_seat_price, premium_seat_price, business_seat_price,
	remain_premium_seats, remain_business_seats, remain_economy_seats)
VALUES ('HOG-843-01', 3, '2023-04-13 08:05:00',
		'CHINA', 'HKG-HONG KONG INTL', 'ISRAEL',
		'BNG-TLV', 'Ethiopian Air', 'A30',
		4128, 0, 8828,
		0, 41, 238); 

INSERT INTO tblFlights(flight_identifier, airplane_identifier, departure_time,
	departure_country, departure_airport, landing_country,
	landing_airport, airline, gate_identifier,
	economy_seat_price, premium_seat_price, business_seat_price,
	remain_premium_seats, remain_business_seats, remain_economy_seats)
VALUES ('SPN-567-12', 1, '2023-04-07 14:15:00',
		'SPAIN', 'MAD-MADRID BARAJAS', 'ISRAEL',
		'BNG-TLV', 'Air Europa', 'B05',
		899, 1100, 1328,
		35, 20, 183); 

CREATE TABLE tblTickets (
	flight_identifier nvarchar(10) NOT NULL,
	seat_identifier nvarchar(20) NOT NULL,
	passenger_name nvarchar(42) NOT NULL,
	user_email nvarchar(30)

	CONSTRAINT fk_flightID FOREIGN KEY (flight_identifier) 
	REFERENCES tblFlights(flight_identifier),

	PRIMARY KEY (flight_identifier, seat_identifier)
);

CREATE TABLE tblCreditCards (
	email nvarchar(30) PRIMARY KEY,
	card_holder_id nvarchar(50) NOT NULL,
	card_holder_name nvarchar(50) NOT NULL,
	expiration_date datetime Not NULL,
	card_number nvarchar(50) NOT NULL,
	card_CVC nvarchar(50) NOT NULL,
	CONSTRAINT fk_email FOREIGN KEY (email) 
	REFERENCES tblUsers(email)
);