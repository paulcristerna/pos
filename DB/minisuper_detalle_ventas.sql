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
-- Table structure for table `detalle_ventas`
--

DROP TABLE IF EXISTS `detalle_ventas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `detalle_ventas` (
  `IdDetalle` bigint(20) NOT NULL AUTO_INCREMENT,
  `Codigo` bigint(20) DEFAULT NULL,
  `Cantidad` float NOT NULL,
  `PrecioUnitario` float NOT NULL,
  PRIMARY KEY (`IdDetalle`)
) ENGINE=InnoDB AUTO_INCREMENT=42 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `detalle_ventas`
--

LOCK TABLES `detalle_ventas` WRITE;
/*!40000 ALTER TABLE `detalle_ventas` DISABLE KEYS */;
INSERT INTO `detalle_ventas` VALUES (1,3178,1,215),(2,71649217579,1,270),(3,660731364507,1,10),(4,794685223617,1,45),(5,39800099099,1,45),(6,5387,1,400),(7,399,1,260),(8,408,1,120),(9,409,1,220),(10,400,1,180),(11,402,1,420),(12,290,1,15),(13,391,1,40),(14,391,1,40),(15,392,1,200),(16,412,1,65),(17,399,1,260),(18,401,1,550),(19,411,1,175),(20,370,1,8),(21,384,1,160),(22,12,1,400),(23,7501892815039,1,25),(24,12,1,400),(25,7501892815039,1,25),(26,7502240723181,1,75),(27,39961004031,1,170),(28,70042194005,1,110),(29,398,1,300),(30,7501973705853,1,70),(31,63,1,5),(32,64,1,20),(33,64,1,20),(34,660731012033,1,215),(35,293,1,10),(36,660731070408,1,70),(37,5826,1,45),(38,398,1,300),(39,70042194005,1,110),(40,398,1,300),(41,168,2,45);
/*!40000 ALTER TABLE `detalle_ventas` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2015-03-01 13:20:36