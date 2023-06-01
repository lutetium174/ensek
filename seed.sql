DROP TABLE IF EXISTS meter_readings;
DROP TABLE IF EXISTS accounts;

CREATE TABLE accounts (
    id SERIAL PRIMARY KEY,
    first_name VARCHAR(127),
    surname VARCHAR(127)
);

CREATE TABLE meter_readings (
    id SERIAL PRIMARY KEY,
    reading VARCHAR(5),
    timestamp TIMESTAMP,
    account_id INT,
    FOREIGN KEY (account_id) REFERENCES accounts(id)
);

INSERT INTO accounts (id, first_name, surname) VALUES (2344,'Tommy','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (2233,'Barry','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (8766,'Sally','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (2345,'Jerry','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (2346,'Ollie','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (2347,'Tara','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (2348,'Tammy','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (2349,'Simon','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (2350,'Colin','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (2351,'Gladys','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (2352,'Greg','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (2353,'Tony','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (2355,'Arthur','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (2356,'Craig','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (6776,'Laura','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (4534,'JOSH','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (1234,'Freya','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (1239,'Noddy','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (1240,'Archie','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (1241,'Lara','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (1242,'Tim','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (1243,'Graham','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (1244,'Tony','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (1245,'Neville','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (1246,'Jo','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (1247,'Jim','Test');
INSERT INTO accounts (id, first_name, surname) VALUES (1248,'Pam','Test');