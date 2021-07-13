CREATE DATABASE bank
GO

USE bank

CREATE TABLE Clients(
	[ClientId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DepartId] [int] NOT NULL,
	[TransactionId] [int] NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED (ClientId)
);
GO

CREATE TABLE Departments(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LoanRate] [tinyint] NOT NULL,
	[DepositRate] [tinyint] NOT NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED (Id)
);
GO

CREATE TABLE Money(
	[ClientId] [int] NOT NULL,
	[Funds] [int] NOT NULL,
	[Loan] [int] NULL,
	[Deposit] [int] NULL,
	[DepositType] [int] NULL,
 CONSTRAINT [PK_Money] PRIMARY KEY CLUSTERED (ClientId)
);
GO

CREATE TABLE Transactions(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Operation] [nvarchar](500) NOT NULL,
	[ClientId] [int] NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED (Id)
);
GO

CREATE TABLE DepositType(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NULL,
 CONSTRAINT [PK_DepositType] PRIMARY KEY CLUSTERED (Id)
);
GO


ALTER TABLE Clients WITH CHECK ADD CONSTRAINT [FK_Clients_Departments] FOREIGN KEY(DepartId)
REFERENCES Departments (Id)

ALTER TABLE Clients CHECK CONSTRAINT [FK_Clients_Departments]

ALTER TABLE Money WITH CHECK ADD CONSTRAINT [FK_Money_Clients] FOREIGN KEY(ClientId)
REFERENCES Clients (ClientId)

ALTER TABLE Money CHECK CONSTRAINT [FK_Money_Clients]

ALTER TABLE Money WITH CHECK ADD  CONSTRAINT [FK_Money_DepositType] FOREIGN KEY(DepositType)
REFERENCES DepositType (Id)

ALTER TABLE Money CHECK CONSTRAINT [FK_Money_DepositType]

ALTER TABLE Transactions  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Clients] FOREIGN KEY(ClientId)
REFERENCES Clients (ClientId)

ALTER TABLE Transactions CHECK CONSTRAINT [FK_Transactions_Clients]
GO

INSERT Departments VALUES (N'Individual', 15, 5)
INSERT Departments VALUES (N'Business', 10, 10)
INSERT Departments VALUES (N'VIP', 5, 15)
GO

INSERT DepositType VALUES (N'Simple')
INSERT DepositType VALUES (N'Capitalization')
GO

INSERT INTO Clients VALUES('Suki Battle',1),('Whoopi Franks',1),('Kermit Olsen',1),('Yoshi Gallagher',1),('Hamish Cole',1),('Emma Sharp',1),('Hilary Coleman',1),('Vance Barlow',1),('Felicia Sutton',1),('Dexter Huber',1);
INSERT INTO Clients VALUES('Lester Velez',1),('Zephania Rivers',1),('Aimee Huber',1),('Callum Hood',1),('Jakeem Donaldson',1),('Galena Schwartz',1),('Kyle Stafford',1),('Richard Le',1),('Naomi Alford',1),('Constance Tillman',1);
INSERT INTO Clients VALUES('Reese Espinoza',1),('Baker Burks',1),('Vaughan Slater',1),('Amity Cooke',1),('Destiny Huffman',1),('Lev Mccarty',1),('Vincent Douglas',1),('Daphne Owen',1),('Dora Gonzalez',1),('Paul Guerra',1);
INSERT INTO Clients VALUES('Abraham Campos',1),('Phelan Leach',1),('Ivan Barnett',1),('Lavinia Vincent',1),('Leandra Bass',1),('Joshua Greene',1),('Lucas Mays',1),('Illiana Cooper',1),('Emery Little',1),('Carol Delgado',1);
INSERT INTO Clients VALUES('Jasmine Jacobson',1),('Alvin Travis',1),('Perry Brooks',1),('Eaton Barrett',1),('Alika Mayo',1),('Kirk Simmons',1),('Shay Rivers',1),('Madeson Conley',1),('Kathleen Mcgowan',1),('Preston Robinson',1);
INSERT INTO Clients VALUES('Riley Doyle',1),('Ruth Little',1),('Xanthus Noble',1),('Marny Park',1),('Asher Mclean',1),('Rhea Medina',1),('Quamar Hill',1),('Emerald Stephenson',1),('Elaine Guerrero',1),('Cullen Torres',1);
INSERT INTO Clients VALUES('Cheyenne Johnson',1),('Iliana Dale',1),('Orson Avery',1),('Wynne Gilliam',1),('Austin Wilkins',1),('Caleb Stewart',1),('Tiger Whitehead',1),('Ora Weaver',1),('Ray Lyons',1),('Alden Ingram',1);
INSERT INTO Clients VALUES('Gabriel Perez',1),('Xanthus Knapp',1),('Juliet Clark',1),('Oscar Coleman',1),('Myles Marsh',1),('Trevor Mercado',1),('Leo Little',1),('Thane Talley',1),('Cameron Dillon',1),('Baxter Macias',1);
INSERT INTO Clients VALUES('Aphrodite Dixon',1),('Nicholas King',1),('Lydia Kirk',1),('Hop Buckley',1),('Isadora Kirk',1),('Melvin Bond',1),('Addison Green',1),('Kennan Mcdonald',1),('Illana Gray',1),('Carissa Madden',1);
INSERT INTO Clients VALUES('Jacob Mullen',1),('Veronica Anthony',1),('Rebekah Ellis',1),('Todd Lee',1),('Megan Levy',1),('Mia Acevedo',1),('Rosalyn Bowers',1),('Leroy Nguyen',1),('Dana Britt',1),('Lisandra Mosley',1);
INSERT INTO Clients VALUES('Mannix Garner',2),('Lee Cooper',2),('Felicia Glover',2),('Stone Farley',2),('Kyla Dyer',2),('Quentin Golden',2),('Nigel Pacheco',2),('Jolene Finley',2),('Zahir Atkinson',2),('Noel Dale',2);
INSERT INTO Clients VALUES('Nissim Bullock',2),('Colleen Howe',2),('Felix Barlow',2),('Kalia Pollard',2),('Ivana Kaufman',2),('Abdul Hudson',2),('Theodore Romero',2),('Hammett Savage',2),('Mary Valenzuela',2),('Ali King',2);
INSERT INTO Clients VALUES('Jonah Vaughan',2),('Mark Watkins',2),('Judah Moon',2),('Anne Hewitt',2),('Rama Mann',2),('Carson Ball',2),('Lionel Mercer',2),('Naida Hampton',2),('Jemima Sexton',2),('Ronan Navarro',2);
INSERT INTO Clients VALUES('Kaitlin Byers',2),('Robert Lawrence',2),('Samson Hopkins',2),('Lenore Soto',2),('Zeus Bond',2),('Aurelia Barnes',2),('Constance Pickett',2),('April Travis',2),('Bryar Downs',2),('Mufutau Mckay',2);
INSERT INTO Clients VALUES('Renee Savage',2),('Adara Holloway',2),('Aaron Stafford',2),('Beck Mathis',2),('Thane Figueroa',2),('Beau Gray',2),('Amal Kirby',2),('Tana Joseph',2),('Macaulay Byers',2),('Faith Jarvis',2);
INSERT INTO Clients VALUES('Amela Bullock',2),('Miriam Carver',2),('Peter Black',2),('Kaitlin Douglas',2),('Abraham Vang',2),('Karly Paul',2),('Scott Harrington',2),('Quintessa Bray',2),('Tyrone Stuart',2),('Dante Floyd',2);
INSERT INTO Clients VALUES('Nyssa Delacruz',2),('Elijah Rich',2),('Urielle Miller',2),('Branden Cervantes',2),('Keane Watson',2),('Lucius Lee',2),('Wesley Gillespie',2),('Bell Simmons',2),('Amery Benton',2),('Aiko Joyce',2);
INSERT INTO Clients VALUES('Myra Conner',2),('Carter Lawrence',2),('Jasmine Pierce',2),('Ashton Goodwin',2),('Kasper Rojas',2),('Amena Snow',2),('Gisela Watson',2),('Urielle Scott',2),('Nina Hawkins',2),('Ali Olsen',2);
INSERT INTO Clients VALUES('Jin Conrad',2),('Hoyt Acosta',2),('Quinn Hines',2),('Lenore Garza',2),('Aristotle Gibbs',2),('Zephania Christian',2),('Emerald Harris',2),('Suki Merritt',2),('Flavia Meyers',2),('Reese Sexton',2);
INSERT INTO Clients VALUES('Wendy Mendez',2),('Todd Hopper',2),('Lareina Potter',2),('Bruce Howe',2),('Cathleen Delaney',2),('Axel Stone',2),('Tallulah Hawkins',2),('Patricia Phillips',2),('Lila Mays',2),('Noel Barlow',2);
INSERT INTO Clients VALUES('Kylynn Summers',3),('Colby Mccray',3),('Macaulay Stokes',3),('Camilla Warner',3),('Jorden Mendoza',3),('Sybill Levy',3),('Irene Travis',3),('Michael Petty',3),('Bernard Holcomb',3),('Ulla Savage',3);
INSERT INTO Clients VALUES('Uta Delgado',3),('Brooke Moreno',3),('Hedley Bowen',3),('Kylynn Mcguire',3),('Gannon Fulton',3),('Barry Pacheco',3),('Trevor Tillman',3),('Velma Grimes',3),('Rogan Rosa',3),('Shelby Flynn',3);
INSERT INTO Clients VALUES('Lana Poole',3),('Julian Butler',3),('Nigel Hanson',3),('Blaze Fitzpatrick',3),('Lev Holland',3),('Urielle Ford',3),('Brendan Casey',3),('Signe Craig',3),('Belle Cameron',3),('Macaulay Lamb',3);
INSERT INTO Clients VALUES('Breanna Bryant',3),('Callum Austin',3),('Adrian Warren',3),('Keaton Rivers',3),('Uriah Estrada',3),('Ciara Shaw',3),('Nash Rodgers',3),('Wynter Yang',3),('Amy Reed',3),('Amy Keith',3);
INSERT INTO Clients VALUES('Rae Ward',3),('Jordan Robinson',3),('Danielle Armstrong',3),('Ashely Melendez',3),('Samuel Hyde',3),('Sawyer Pearson',3),('Summer Shepherd',3),('Kadeem Kelley',3),('Amaya Melton',3),('Alexa Carrillo',3);
INSERT INTO Clients VALUES('Keiko Harrington',3),('Medge Johnston',3),('Hannah Travis',3),('Slade Snyder',3),('Hermione Barrett',3),('Simon Bullock',3),('Wesley Hampton',3),('Doris Battle',3),('Florence Rivers',3),('Kirestin Holt',3);
INSERT INTO Clients VALUES('Clarke Cooley',3),('Kenneth Yang',3),('Mia Gentry',3),('Indira Dudley',3),('Colette Booker',3),('Charity Fleming',3),('Caleb Blackwell',3),('Guy Reeves',3),('Ulysses Holden',3),('Kylynn Stevenson',3);
INSERT INTO Clients VALUES('Blair Carpenter',3),('Ursa Saunders',3),('Jelani Woods',3),('Lyle Farrell',3),('Clark Farrell',3),('Zane Guerrero',3),('Ethan Cannon',3),('Gail Kaufman',3),('James Monroe',3),('Nell Randolph',3);
INSERT INTO Clients VALUES('Carlos Bailey',3),('Gavin Maldonado',3),('Allistair Acevedo',3),('Vance Marsh',3),('Kim Knowles',3),('Kevin Alford',3),('Ian Marshall',3),('Basia Nolan',3),('Arthur Floyd',3),('Ina Brady',3);
INSERT INTO Clients VALUES('Yvonne Mitchell',3),('Ishmael Valdez',3),('Jocelyn Sosa',3),('Gage Bass',3),('Xanthus Patton',3),('Lillian Chen',3),('Marsden Britt',3),('Barrett Hughes',3),('Mohammad Love',3),('Basil Barker',3);
GO

INSERT INTO Money VALUES(1,13922,0,0,NULL),(2,8452,0,0,NULL),(3,20543,0,0,NULL),(4,40967,0,0,NULL),(5,4595,0,0,NULL),(6,46566,0,0,NULL),(7,25378,0,0,NULL),(8,17358,0,0,NULL),(9,41162,0,0,NULL),(10,46062,0,0,NULL);
INSERT INTO Money VALUES(11,46814,0,0,NULL),(12,10516,0,0,NULL),(13,10740,0,0,NULL),(14,26993,0,0,NULL),(15,1213,0,0,NULL),(16,21018,0,0,NULL),(17,5459,0,0,NULL),(18,37907,0,0,NULL),(19,15563,0,0,NULL),(20,12695,0,0,NULL);
INSERT INTO Money VALUES(21,18124,0,0,NULL),(22,9670,0,0,NULL),(23,45049,0,0,NULL),(24,36542,0,0,NULL),(25,21236,0,0,NULL),(26,41542,0,0,NULL),(27,24922,0,0,NULL),(28,1840,0,0,NULL),(29,2407,0,0,NULL),(30,9335,0,0,NULL);
INSERT INTO Money VALUES(31,29278,0,0,NULL),(32,47590,0,0,NULL),(33,1806,0,0,NULL),(34,46400,0,0,NULL),(35,38557,0,0,NULL),(36,7256,0,0,NULL),(37,42274,0,0,NULL),(38,48285,0,0,NULL),(39,3990,0,0,NULL),(40,11960,0,0,NULL);
INSERT INTO Money VALUES(41,31206,0,0,NULL),(42,39261,0,0,NULL),(43,32768,0,0,NULL),(44,46185,0,0,NULL),(45,5037,0,0,NULL),(46,3226,0,0,NULL),(47,48669,0,0,NULL),(48,33576,0,0,NULL),(49,6932,0,0,NULL),(50,46566,0,0,NULL);
INSERT INTO Money VALUES(51,6039,0,0,NULL),(52,4643,0,0,NULL),(53,34053,0,0,NULL),(54,28705,0,0,NULL),(55,21196,0,0,NULL),(56,15958,0,0,NULL),(57,19795,0,0,NULL),(58,2575,0,0,NULL),(59,7358,0,0,NULL),(60,26071,0,0,NULL);
INSERT INTO Money VALUES(61,33340,0,0,NULL),(62,38210,0,0,NULL),(63,27773,0,0,NULL),(64,49746,0,0,NULL),(65,12708,0,0,NULL),(66,39160,0,0,NULL),(67,15813,0,0,NULL),(68,37608,0,0,NULL),(69,14204,0,0,NULL),(70,13919,0,0,NULL);
INSERT INTO Money VALUES(71,33231,0,0,NULL),(72,13772,0,0,NULL),(73,2508,0,0,NULL),(74,25814,0,0,NULL),(75,26023,0,0,NULL),(76,4945,0,0,NULL),(77,20947,0,0,NULL),(78,31277,0,0,NULL),(79,44712,0,0,NULL),(80,16144,0,0,NULL);
INSERT INTO Money VALUES(81,43139,0,0,NULL),(82,3602,0,0,NULL),(83,33090,0,0,NULL),(84,47977,0,0,NULL),(85,13873,0,0,NULL),(86,9168,0,0,NULL),(87,32463,0,0,NULL),(88,29821,0,0,NULL),(89,22957,0,0,NULL),(90,31500,0,0,NULL);
INSERT INTO Money VALUES(91,30390,0,0,NULL),(92,13127,0,0,NULL),(93,46634,0,0,NULL),(94,20654,0,0,NULL),(95,2275,0,0,NULL),(96,36446,0,0,NULL),(97,4348,0,0,NULL),(98,28063,0,0,NULL),(99,27627,0,0,NULL),(100,12397,0,0,NULL);
INSERT INTO Money VALUES(101,46715,0,0,NULL),(102,49135,0,0,NULL),(103,6343,0,0,NULL),(104,49721,0,0,NULL),(105,28862,0,0,NULL),(106,4639,0,0,NULL),(107,5880,0,0,NULL),(108,39748,0,0,NULL),(109,9159,0,0,NULL),(110,20357,0,0,NULL);
INSERT INTO Money VALUES(111,23875,0,0,NULL),(112,38416,0,0,NULL),(113,18678,0,0,NULL),(114,32443,0,0,NULL),(115,45152,0,0,NULL),(116,29316,0,0,NULL),(117,22036,0,0,NULL),(118,48349,0,0,NULL),(119,46383,0,0,NULL),(120,35033,0,0,NULL);
INSERT INTO Money VALUES(121,19670,0,0,NULL),(122,16366,0,0,NULL),(123,49777,0,0,NULL),(124,2363,0,0,NULL),(125,26201,0,0,NULL),(126,30585,0,0,NULL),(127,37312,0,0,NULL),(128,15208,0,0,NULL),(129,17596,0,0,NULL),(130,16923,0,0,NULL);
INSERT INTO Money VALUES(131,7381,0,0,NULL),(132,40738,0,0,NULL),(133,41420,0,0,NULL),(134,4922,0,0,NULL),(135,37415,0,0,NULL),(136,43219,0,0,NULL),(137,39694,0,0,NULL),(138,30726,0,0,NULL),(139,10753,0,0,NULL),(140,25808,0,0,NULL);
INSERT INTO Money VALUES(141,10742,0,0,NULL),(142,41471,0,0,NULL),(143,40453,0,0,NULL),(144,17120,0,0,NULL),(145,5421,0,0,NULL),(146,36600,0,0,NULL),(147,42169,0,0,NULL),(148,6362,0,0,NULL),(149,43355,0,0,NULL),(150,26590,0,0,NULL);
INSERT INTO Money VALUES(151,23936,0,0,NULL),(152,30519,0,0,NULL),(153,30067,0,0,NULL),(154,12258,0,0,NULL),(155,11055,0,0,NULL),(156,19487,0,0,NULL),(157,28759,0,0,NULL),(158,21076,0,0,NULL),(159,8295,0,0,NULL),(160,49227,0,0,NULL);
INSERT INTO Money VALUES(161,2789,0,0,NULL),(162,11454,0,0,NULL),(163,38692,0,0,NULL),(164,7627,0,0,NULL),(165,19137,0,0,NULL),(166,25451,0,0,NULL),(167,35395,0,0,NULL),(168,21826,0,0,NULL),(169,1172,0,0,NULL),(170,14871,0,0,NULL);
INSERT INTO Money VALUES(171,24187,0,0,NULL),(172,42841,0,0,NULL),(173,2705,0,0,NULL),(174,23161,0,0,NULL),(175,15759,0,0,NULL),(176,21093,0,0,NULL),(177,15189,0,0,NULL),(178,17158,0,0,NULL),(179,47792,0,0,NULL),(180,21099,0,0,NULL);
INSERT INTO Money VALUES(181,49418,0,0,NULL),(182,7230,0,0,NULL),(183,10146,0,0,NULL),(184,24044,0,0,NULL),(185,31423,0,0,NULL),(186,29901,0,0,NULL),(187,37019,0,0,NULL),(188,16024,0,0,NULL),(189,41537,0,0,NULL),(190,21333,0,0,NULL);
INSERT INTO Money VALUES(191,20291,0,0,NULL),(192,49306,0,0,NULL),(193,31711,0,0,NULL),(194,28098,0,0,NULL),(195,44973,0,0,NULL),(196,21182,0,0,NULL),(197,15655,0,0,NULL),(198,6660,0,0,NULL),(199,47794,0,0,NULL),(200,28007,0,0,NULL);
INSERT INTO Money VALUES(201,27406,0,0,NULL),(202,29957,0,0,NULL),(203,6395,0,0,NULL),(204,11182,0,0,NULL),(205,8815,0,0,NULL),(206,33127,0,0,NULL),(207,39753,0,0,NULL),(208,23697,0,0,NULL),(209,7023,0,0,NULL),(210,12293,0,0,NULL);
INSERT INTO Money VALUES(211,45430,0,0,NULL),(212,38457,0,0,NULL),(213,13083,0,0,NULL),(214,40231,0,0,NULL),(215,40696,0,0,NULL),(216,28172,0,0,NULL),(217,31897,0,0,NULL),(218,9285,0,0,NULL),(219,11004,0,0,NULL),(220,34219,0,0,NULL);
INSERT INTO Money VALUES(221,41693,0,0,NULL),(222,7972,0,0,NULL),(223,39470,0,0,NULL),(224,13359,0,0,NULL),(225,24233,0,0,NULL),(226,34205,0,0,NULL),(227,9884,0,0,NULL),(228,10575,0,0,NULL),(229,13563,0,0,NULL),(230,47595,0,0,NULL);
INSERT INTO Money VALUES(231,47287,0,0,NULL),(232,14091,0,0,NULL),(233,8783,0,0,NULL),(234,39350,0,0,NULL),(235,5646,0,0,NULL),(236,4166,0,0,NULL),(237,44048,0,0,NULL),(238,23188,0,0,NULL),(239,42138,0,0,NULL),(240,35158,0,0,NULL);
INSERT INTO Money VALUES(241,22665,0,0,NULL),(242,17128,0,0,NULL),(243,43679,0,0,NULL),(244,29073,0,0,NULL),(245,32470,0,0,NULL),(246,28326,0,0,NULL),(247,45503,0,0,NULL),(248,47508,0,0,NULL),(249,42990,0,0,NULL),(250,19496,0,0,NULL);
INSERT INTO Money VALUES(251,38709,0,0,NULL),(252,18870,0,0,NULL),(253,28755,0,0,NULL),(254,44077,0,0,NULL),(255,24596,0,0,NULL),(256,26649,0,0,NULL),(257,5485,0,0,NULL),(258,27437,0,0,NULL),(259,32623,0,0,NULL),(260,40313,0,0,NULL);
INSERT INTO Money VALUES(261,11190,0,0,NULL),(262,19533,0,0,NULL),(263,41738,0,0,NULL),(264,39270,0,0,NULL),(265,38241,0,0,NULL),(266,20762,0,0,NULL),(267,7968,0,0,NULL),(268,44258,0,0,NULL),(269,12902,0,0,NULL),(270,17766,0,0,NULL);
INSERT INTO Money VALUES(271,10710,0,0,NULL),(272,39934,0,0,NULL),(273,11639,0,0,NULL),(274,33915,0,0,NULL),(275,19470,0,0,NULL),(276,5987,0,0,NULL),(277,34315,0,0,NULL),(278,35969,0,0,NULL),(279,1713,0,0,NULL),(280,31633,0,0,NULL);
INSERT INTO Money VALUES(281,9832,0,0,NULL),(282,22561,0,0,NULL),(283,41907,0,0,NULL),(284,27727,0,0,NULL),(285,4845,0,0,NULL),(286,29333,0,0,NULL),(287,27224,0,0,NULL),(288,22300,0,0,NULL),(289,49066,0,0,NULL),(290,7363,0,0,NULL);
INSERT INTO Money VALUES(291,10248,0,0,NULL),(292,44762,0,0,NULL),(293,14694,0,0,NULL),(294,3719,0,0,NULL),(295,16922,0,0,NULL),(296,12043,0,0,NULL),(297,25398,0,0,NULL),(298,16265,0,0,NULL),(299,49765,0,0,NULL),(300,46793,0,0,NULL);
GO
