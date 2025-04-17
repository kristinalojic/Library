CREATE DATABASE  IF NOT EXISTS `library` /*!40100 DEFAULT CHARACTER SET utf8mb3 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `library`;
-- MySQL dump 10.13  Distrib 8.0.31, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: library
-- ------------------------------------------------------
-- Server version	8.0.31

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `book`
--

DROP TABLE IF EXISTS `book`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `book` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(45) NOT NULL,
  `Author` varchar(45) NOT NULL,
  `Year_of_publication` int NOT NULL,
  `Copies` tinyint NOT NULL,
  `Available_copies` tinyint NOT NULL,
  `Is_available` tinyint(1) NOT NULL DEFAULT '1',
  `genre_Id` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  KEY `fk_book_genre1_idx` (`genre_Id`),
  CONSTRAINT `fk_book_genre1` FOREIGN KEY (`genre_Id`) REFERENCES `genre` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=70 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `book`
--

LOCK TABLES `book` WRITE;
/*!40000 ALTER TABLE `book` DISABLE KEYS */;
INSERT INTO `book` VALUES (1,'Dina','Frank Herbert',1965,25,23,0,1),(2,'Solaris','Stanislav Lem',1961,15,15,1,1),(3,'Gospodar prstenova','J.R.R. Tolkin',1954,30,30,1,1),(4,'Hobit','J.R.R. Tolkin',1937,20,20,1,1),(5,'Silmarilion','J.R.R. Tolkin',1977,18,18,1,1),(6,'Psiho','Robert Bloh',1959,12,12,1,2),(7,'Drakula','Bram Stoker',1897,14,10,1,2),(8,'Frankenštajn','Meri Šeli',1818,16,15,1,2),(9,'Ubistvo u Orijent Ekspresu','Agata Kristi',1934,22,22,1,3),(10,'Deset malih crnaca','Agata Kristi',1939,30,30,1,3),(11,'Da Vinčijev kod','Den Braun',2003,18,15,1,3),(12,'Inferno','Den Braun',2013,12,11,1,3),(13,'Gospodar senki','Donato Karizi',2017,15,15,1,4),(14,'Sumrak','Stefani Mejer',2005,28,28,1,5),(15,'Pedeset nijansi sive','E.L. Džejms',2011,24,24,1,5),(16,'Na Drini ćuprija','Ivo Andrić',1945,20,20,1,6),(17,'Prokleta avlija','Ivo Andrić',1954,15,15,1,6),(18,'Seobe','Miloš Crnjanski',1929,18,18,1,6),(19,'Roman o Londonu','Miloš Crnjanski',1971,14,14,1,6),(20,'Derviš i smrt','Meša Selimović',1966,22,22,1,6),(21,'Tvrđava','Meša Selimović',1970,12,12,1,6),(22,'Koreni','Dobrica Ćosić',1954,17,17,1,6),(23,'Deobe','Dobrica Ćosić',1961,10,10,1,6),(24,'Moć sadašnjeg trenutka','Ekhart Tol',1997,18,18,1,7),(25,'Razgovori sa Bogom','Nil Donald Volš',1995,12,12,1,7),(26,'Bašta, pepeo','Danilo Kiš',1965,13,13,1,8),(27,'Enciklopedija mrtvih','Danilo Kiš',1983,16,16,1,8),(28,'Gorski vijenac','Petar II Petrović Njegoš',1847,20,20,1,9),(29,'Pesme','Jovan Dučić',1901,12,12,1,9),(30,'Lirika','Aleksa Šantić',1908,18,18,1,9),(31,'Robinzon Kruso','Danijel Defo',1719,22,22,1,10),(32,'Hari Poter i kamen mudrosti','Dž.K. Rouling',1997,40,40,1,11),(33,'Hari Poter i odaja tajni','Dž.K. Rouling',1998,35,35,1,11),(34,'Mali princ','Antoan de Sent-Egziperi',1943,28,28,1,11),(35,'Alisa u zemlji čuda','Luis Kerol',1865,26,24,1,11),(36,'Pipi Duga Čarapa','Astrid Lindgren',1945,30,30,1,11),(37,'Psihologija mase','Gistav le Bon',1895,15,15,1,12),(38,'Emocionalna inteligencija','Danijel Goleman',1995,14,14,1,12),(39,'Braća Karamazovi','Fjodor Dostojevski',1880,18,18,0,13),(40,'Zločin i kazna','Fjodor Dostojevski',1866,22,22,1,13),(41,'Ana Karenjina','Lav Tolstoj',1877,14,14,1,13),(42,'Rat i mir','Lav Tolstoj',1869,20,20,1,13);
/*!40000 ALTER TABLE `book` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employee`
--

DROP TABLE IF EXISTS `employee`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employee` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(15) NOT NULL,
  `Surname` varchar(15) NOT NULL,
  `Username` varchar(45) NOT NULL,
  `Password` varchar(45) NOT NULL,
  `Is_acive` tinyint(1) NOT NULL DEFAULT '1',
  `Account_type` tinyint NOT NULL DEFAULT '2',
  `JBMG` varchar(45) NOT NULL,
  `Phone` varchar(45) NOT NULL,
  `Email` varchar(45) NOT NULL,
  `Address` varchar(45) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `id_UNIQUE` (`Id`),
  UNIQUE KEY `JBMG_UNIQUE` (`JBMG`),
  UNIQUE KEY `Phone_UNIQUE` (`Phone`),
  UNIQUE KEY `Email_UNIQUE` (`Email`),
  UNIQUE KEY `Username_UNIQUE` (`Username`)
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employee`
--

LOCK TABLES `employee` WRITE;
/*!40000 ALTER TABLE `employee` DISABLE KEYS */;
INSERT INTO `employee` VALUES (1,'Kristina','Lojic','admin','admin',1,1,'130700','066410903','admin@gmai.coml','Banja Luka'),(2,'Vladimir','Jankovic','vladimir','vladimir',1,2,'291299','061951952','vladimir@gmail.com','Karadjordjeva'),(5,'Kristina','Lojic','kristina','kristina',0,2,'1307000','0664109033','kristina@gmail.com','Golesi'),(10,'Marko','Markovic','marko','marko',1,2,'15546456','065123456','marko@gmail.com','Laktasi'),(11,'Jovan','Jovanovic','jovan','jovan',0,2,'85245454','066123455','jovan@gmail.com','Banja Luka'),(12,'Nikola','Nikolic','nikola','nikola',1,2,'1212000','065447882','nikola@gmail.com','Banja Luka'),(13,'Dunja','Dobric','dunja','dunja',1,2,'0505030','065050505','dunja@gmail.com','Banja Luka'),(14,'Jovana','Jovic','jovana','jovana',0,2,'545454','065789456','jovana@gmail.com','Banja Luka');
/*!40000 ALTER TABLE `employee` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `after_employee_insert` AFTER INSERT ON `employee` FOR EACH ROW BEGIN
    INSERT INTO settings (`employee`) VALUES (NEW.Id);
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `genre`
--

DROP TABLE IF EXISTS `genre`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `genre` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `genre`
--

LOCK TABLES `genre` WRITE;
/*!40000 ALTER TABLE `genre` DISABLE KEYS */;
INSERT INTO `genre` VALUES (1,'Naučna fantastika'),(2,'Horor'),(3,'Misterija'),(4,'Triler'),(5,'Ljubavni roman'),(6,'Istorijski roman'),(7,'Nauka'),(8,'Drama'),(9,'Poezija'),(10,'Avantura'),(11,'Dječija književnost'),(12,'Psihologija'),(13,'Kriminalistički roman');
/*!40000 ALTER TABLE `genre` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `loans`
--

DROP TABLE IF EXISTS `loans`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `loans` (
  `book` int NOT NULL,
  `member` int NOT NULL,
  `Issue_date` date NOT NULL,
  `Due_date` date NOT NULL,
  `Has_been_extended` tinyint(1) NOT NULL DEFAULT '0',
  `Is_Returned` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`book`,`member`,`Issue_date`),
  KEY `fk_zaduzivanje_member1_idx` (`member`),
  CONSTRAINT `fk_zaduzivanje_book1` FOREIGN KEY (`book`) REFERENCES `book` (`Id`),
  CONSTRAINT `fk_zaduzivanje_member1` FOREIGN KEY (`member`) REFERENCES `member` (`Membership_card_number`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `loans`
--

LOCK TABLES `loans` WRITE;
/*!40000 ALTER TABLE `loans` DISABLE KEYS */;
INSERT INTO `loans` VALUES (1,10000002,'2025-04-16','2025-05-28',0,0),(1,10000005,'2025-04-09','2025-04-24',0,0),(3,10000015,'2025-04-09','2025-04-09',1,1),(4,10000015,'2025-04-09','2025-04-09',1,1),(7,10000001,'2025-04-09','2025-05-09',0,0),(7,10000003,'2025-04-16','2025-04-26',0,0),(7,10000007,'2025-04-17','2025-04-25',0,0),(7,10000011,'2025-04-16','2025-04-29',0,0),(8,10000001,'2025-04-09','2025-05-09',0,0),(11,10000004,'2025-04-16','2025-05-14',0,0),(11,10000009,'2025-04-16','2025-05-15',0,0),(11,10000013,'2025-04-08','2025-04-09',0,1),(11,10000017,'2025-04-16','2025-05-07',0,0),(12,10000005,'2025-04-09','2025-04-16',1,1),(12,10000013,'2025-04-08','2025-04-15',1,0),(35,10000002,'2025-04-16','2025-05-22',0,0),(35,10000025,'2025-04-16','2025-04-25',0,0);
/*!40000 ALTER TABLE `loans` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `member`
--

DROP TABLE IF EXISTS `member`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `member` (
  `Membership_card_number` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(15) NOT NULL,
  `Surname` varchar(15) NOT NULL,
  `Email` varchar(45) NOT NULL,
  `Phone` varchar(20) NOT NULL,
  `Address` varchar(45) NOT NULL,
  PRIMARY KEY (`Membership_card_number`),
  UNIQUE KEY `id_UNIQUE` (`Membership_card_number`),
  UNIQUE KEY `Email_UNIQUE` (`Email`),
  UNIQUE KEY `Phone_UNIQUE` (`Phone`)
) ENGINE=InnoDB AUTO_INCREMENT=10000046 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `member`
--

LOCK TABLES `member` WRITE;
/*!40000 ALTER TABLE `member` DISABLE KEYS */;
INSERT INTO `member` VALUES (10000001,'Marko','Petrović','marko.petrovic@example.com','065123456','Kralja Petra I Karađorđevića 85'),(10000002,'Ana','Jovanović','ana.jovanovic@example.com','066987654','Aleja Svetog Save 24'),(10000003,'Nikola','Knežević','nikola.knezevic@example.com','065234567','Gundulićeva 11'),(10000004,'Milica','Đurić','milica.djuric@example.com','066345678','Cara Dušana 50'),(10000005,'Stefan','Marić','stefan.maric@example.com','065456789','Bulevar vojvode Stepe 17'),(10000006,'Jelena','Nikolić','jelena.nikolic@example.com','066567890','Branka Radičevića 33'),(10000007,'Ivan','Radić','ivan.radic@example.com','065678901','Meše Selimovića 22'),(10000008,'Sara','Stanković','sara.stankovic@example.com','066789012','Ulica Milana Karanovića 5'),(10000009,'Aleksandar','Lazić','aleksandar.lazic@example.com','065890123','Jovana Dučića 14'),(10000010,'Marija','Babić','marija.babic@example.com','066901234','Majke Jugovića 8'),(10000011,'Filip','Popović','filip.popovic@example.com','065912345','Carigradska 27'),(10000012,'Ivana','Kovačević','ivana.kovacevic@example.com','066923456','Mladena Stojanovića 12'),(10000013,'Petar','Vuković','petar.vukovic@example.com','065934567','Karađorđeva 40'),(10000014,'Tijana','Milošević','tijana.milosevic@example.com','066945678','Njegoševa 19'),(10000015,'Vladimir','Savić','vladimir.savic@example.com','065956789','Ulica Dragiše Vasića 3'),(10000016,'Kristina','Ilić','kristina.ilic@example.com','066967890','Bulevar cara Dušana 45'),(10000017,'Dejan','Pavlović','dejan.pavlovic@example.com','065978901','Vojvode Sinđelića 7'),(10000018,'Sandra','Blagojević','sandra.blagojevic@example.com','066989012','Nikole Pašića 38'),(10000019,'Nemanja','Stojanović','nemanja.stojanovic@example.com','065990123','Vase Pelagića 14'),(10000020,'Maja','Mirković','maja.mirkovic@example.com','066991234','Vladike Platona 10'),(10000021,'Goran','Ristić','goran.ristic@example.com','065992345','Ulica Srpskih rudara 6'),(10000022,'Slobodan','Vasić','slobodan.vasic@example.com','066993456','Kralja Petra II 29'),(10000023,'Nikolina','Perić','nikolina.peric@example.com','065994567','Vidovdanska 20'),(10000024,'Bojan','Lukić','bojan.lukic@example.com','066995678','Ulica Gavrila Principa 9'),(10000025,'Tamara','Bogdanović','tamara.bogdanovic@example.com','065996789','Ulica Svetozara Markovića 4'),(10000026,'Dragan','Kostić','dragan.kostic@example.com','066997890','Braće Pantića 15'),(10000027,'Milena','Janković','milena.jankovic@example.com','065998901','Romanijska 3'),(10000028,'Dario','Radovanović','dario.radovanovic@example.com','066999112','Gavrila Principa 23'),(10000029,'Sanja','Petković','sanja.petkovic@example.com','065999234','Ulica Branka Ćopića 18'),(10000030,'Branko','Stanić','branko.stanic@example.com','066999345','Vojvode Mišića 41'),(10000031,'Jovana','Cvijanović','jovana.cvijanovic@example.com','065999456','Milana Tepavca 26'),(10000032,'Vesna','Dimitrijević','vesna.dimitrijevic@example.com','066999567','Bulevar vojvode Petra Bojovića 39'),(10000033,'Luka','Gajić','luka.gajic@example.com','065999678','Aleja Svetog Save 2'),(10000034,'Kristijan','Dragičević','kristijan.dragicevic@example.com','066999789','Ivana Frane Jukića 9'),(10000035,'Nevena','Tadić','nevena.tadic@example.com','065999890','Bulevar vojvode Živojina Mišića 14'),(10000036,'Dunja','Tomić','dunja.tomic@example.com','066999901','Ulica Braće Mažar i majke Marije 7'),(10000037,'Miloš','Vujinović','milos.vujinovic@example.com','065999912','Srpska 17'),(10000038,'Aleksa','Stevanović','aleksa.stevanovic@example.com','066999223','Bulevar vojvode Petra Bojovića 10'),(10000039,'Teodora','Krstić','teodora.krstic@example.com','065999123','Mladena Stojanovića 30'),(10000040,'Danilo','Zdravković','danilo.zdravkovic@example.com','066999234','Ulica Pave Radana 16');
/*!40000 ALTER TABLE `member` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `membership_fees`
--

DROP TABLE IF EXISTS `membership_fees`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `membership_fees` (
  `Issuence` date NOT NULL,
  `Expiration` date NOT NULL,
  `member` int NOT NULL,
  PRIMARY KEY (`member`),
  CONSTRAINT `fk_membership_fees_member1` FOREIGN KEY (`member`) REFERENCES `member` (`Membership_card_number`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `membership_fees`
--

LOCK TABLES `membership_fees` WRITE;
/*!40000 ALTER TABLE `membership_fees` DISABLE KEYS */;
INSERT INTO `membership_fees` VALUES ('2025-04-09','2026-04-09',10000001),('2025-04-09','2026-04-09',10000002),('2024-08-09','2025-08-09',10000003),('2025-04-09','2026-04-09',10000004),('2024-07-17','2025-07-17',10000005),('2022-07-18','2023-07-18',10000006),('2024-10-22','2025-10-22',10000007),('2023-01-22','2024-01-22',10000008),('2024-06-15','2025-06-15',10000009),('2022-07-30','2023-07-30',10000010),('2024-05-05','2025-05-05',10000011),('2023-12-10','2024-12-10',10000012),('2024-09-19','2025-09-19',10000013),('2021-06-25','2022-06-25',10000014),('2024-12-30','2025-12-30',10000015),('2023-03-14','2024-03-14',10000016),('2024-11-11','2025-11-11',10000017),('2022-10-05','2023-10-05',10000018),('2024-07-07','2025-07-07',10000019),('2023-05-01','2024-05-01',10000020),('2024-04-20','2025-04-20',10000021),('2021-02-28','2022-02-28',10000022),('2024-01-14','2025-01-14',10000023),('2022-08-09','2023-08-09',10000024),('2024-06-29','2025-06-29',10000025),('2022-07-17','2023-07-17',10000026),('2024-09-05','2025-09-05',10000027),('2022-10-22','2023-10-22',10000028),('2024-11-08','2025-11-08',10000029),('2023-06-15','2024-06-15',10000030),('2024-10-17','2025-10-17',10000031),('2025-04-16','2026-04-16',10000032),('2024-03-21','2025-03-21',10000033),('2021-09-19','2022-09-19',10000034),('2024-02-18','2025-02-18',10000035),('2022-12-30','2023-12-30',10000036),('2024-06-06','2025-06-06',10000037),('2023-11-11','2024-11-11',10000038),('2024-09-12','2025-09-12',10000039),('2023-07-07','2024-07-07',10000040);
/*!40000 ALTER TABLE `membership_fees` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `settings`
--

DROP TABLE IF EXISTS `settings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `settings` (
  `Theme` varchar(45) NOT NULL DEFAULT 'Light',
  `Language` varchar(45) NOT NULL DEFAULT 'Serbian',
  `Color` varchar(45) NOT NULL DEFAULT 'Purple',
  `employee` int NOT NULL,
  PRIMARY KEY (`employee`),
  UNIQUE KEY `employee_UNIQUE` (`employee`),
  CONSTRAINT `fk_settings_employee1` FOREIGN KEY (`employee`) REFERENCES `employee` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `settings`
--

LOCK TABLES `settings` WRITE;
/*!40000 ALTER TABLE `settings` DISABLE KEYS */;
INSERT INTO `settings` VALUES ('Dark','Serbian','Green',1),('Dark','Serbian','Green',2),('Light','Serbian','Purple',5),('Dark','English','Green',10),('Light','Serbian','Purple',11),('Dark','Serbian','Green',12),('Dark','English','Yellow',13),('Light','Serbian','Purple',14);
/*!40000 ALTER TABLE `settings` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-17 21:55:28
