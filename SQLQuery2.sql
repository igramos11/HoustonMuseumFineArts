CREATE TABLE Building(
	B_name VARCHAR(20),
    PRIMARY KEY (B_name)
);

CREATE TABLE Collection(
	C_name VARCHAR(20),
    C_building VARCHAR(20),
    PRIMARY KEY (C_name)
);

CREATE TABLE Exhibition(
	E_name VARCHAR(20),
    E_startdate INT,
    E_enddate INT,
    PRIMARY KEY (E_name)
);

CREATE TABLE Giftshop(
	G_item VARCHAR(20),
    G_price INT,
    G_num INT,
    G_avail tinyint(1),
    PRIMARY KEY (G_item)
    
);

CREATE TABLE Revenue(
    R_ticket VARCHAR(20),
    R_count INT,
    R_item VARCHAR(20),
    R_price INT,
    FOREIGN KEY (R_item) REFERENCES Giftshop(G_item),
    FOREIGN KEY (R_ticket) REFERENCES Ticket(T_type)
);

CREATE TABLE Ticket(
	T_building VARCHAR(20),
	T_price INT,
    T_type VARCHAR(20),
    PRIMARY KEY (T_type),
    FOREIGN KEY (T_building) REFERENCES Building(B_name)
);

CREATE TABLE Artwork(
	A_name VARCHAR(20),
    A_artist VARCHAR(20),
    A_date INT,
    A_size VARCHAR(20),
    A_collection VARCHAR(20),
    A_ID INT(8),
    A_donor VARCHAR(20),
    A_exhibition VARCHAR(20),
    A_origin VARCHAR(20),
    PRIMARY KEY (A_ID),
    FOREIGN KEY (A_exhibition) REFERENCES Exhibition(E_name),
    FOREIGN KEY (A_collection) REFERENCES Collection(C_name)
);