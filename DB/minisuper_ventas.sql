-- MySQL dump 10.13  Distrib 5.6.17, for Win32 (x86)
--
-- Host: localhost    Database: minisuper
-- ------------------------------------------------------
-- Server version	5.6.22-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `ventas`
--

DROP TABLE IF EXISTS `ventas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ventas` (
  `IdVenta` bigint(20) NOT NULL AUTO_INCREMENT,
  `Cliente` varchar(20) DEFAULT NULL,
  `Fecha` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Total` float(10,0) NOT NULL,
  `PagoCon` float(10,2) NOT NULL,
  `Cambio` float(10,2) NOT NULL,
  `Estado` char(1) NOT NULL DEFAULT 'A',
  PRIMARY KEY (`IdVenta`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ventas`
--

LOCK TABLES `ventas` WRITE;
/*!40000 ALTER TABLE `ventas` DISABLE KEYS */;
INSERT INTO `ventas` VALUES (1,'Publico en General','2015-02-28 23:10:02',215,300.00,85.00,'A'),(2,'Publico en General','2015-02-28 23:24:41',3483,4000.00,517.00,'A'),(3,'Publico en General','2015-03-01 19:24:39',425,500.00,75.00,'A'),(4,'Publico en General','2015-03-01 19:24:54',425,500.00,75.00,'A'),(5,'Publico en General','2015-03-01 19:35:55',245,300.00,55.00,'A'),(6,'Publico en General','2015-03-01 19:43:45',410,500.00,90.00,'A'),(7,'Publico en General','2015-03-01 19:59:24',75,100.00,25.00,'A'),(8,'Publico en General','2015-03-01 20:00:09',20,50.00,30.00,'A'),(9,'Publico en General','2015-03-01 20:00:31',20,50.00,30.00,'A'),(10,'Publico en General','2015-03-01 20:00:50',215,300.00,85.00,'A'),(11,'Publico en General','2015-03-01 20:01:38',80,100.00,20.00,'A'),(12,'Publico en General','2015-03-01 20:16:16',345,400.00,55.00,'A'),(13,'Publico en General','2015-03-01 20:17:04',500,500.00,0.00,'A');
/*!40000 ALTER TABLE `ventas` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2015-03-01 13:20:37
