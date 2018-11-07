-- MySQL dump 10.13  Distrib 8.0.12, for Win64 (x86_64)
--
-- Host: localhost    Database: oralcmsdb
-- ------------------------------------------------------
-- Server version	8.0.12

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `collections`
--

DROP TABLE IF EXISTS `collections`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `collections` (
  `Id` smallint(6) NOT NULL AUTO_INCREMENT,
  `CollectionName` varchar(250) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `keywords`
--

DROP TABLE IF EXISTS `keywords`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `keywords` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `KeywordName` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 ;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `subjects`
--

DROP TABLE IF EXISTS `subjects`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `subjects` (
  `Id` smallint(6) NOT NULL AUTO_INCREMENT,
  `SubjectName` varchar(250) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=97 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `subseries`
--

DROP TABLE IF EXISTS `subseries`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `subseries` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CollectionId` smallint(6) NOT NULL,
  `SubseriesName` varchar(250) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=39 DEFAULT CHARSET=utf8mb4 ;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `transcriptions`
--

DROP TABLE IF EXISTS `transcriptions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `transcriptions` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ProjectCode` varchar(50) NOT NULL,
  `IsPriority` tinyint(1) NOT NULL DEFAULT '0',
  `ReasonForPriority` varchar(250) DEFAULT NULL,
  `InitialNote` varchar(250) DEFAULT NULL,
  `CollectionId` smallint(6) NOT NULL,
  `SubseriesId` int(11) NOT NULL,
  `Interviewee` varchar(250) NOT NULL,
  `Interviewer` varchar(250) NOT NULL,
  `InterviewDate` date NOT NULL,
  `Title` varchar(500) NOT NULL,
  `Description` varchar(2000) DEFAULT NULL,
  `Keywords` varchar(500) DEFAULT NULL,
  `Subject` varchar(500) DEFAULT NULL,
  `ScopeAndContents` varchar(500) DEFAULT NULL,
  `Format` varchar(250) DEFAULT NULL,
  `Type` varchar(250) DEFAULT NULL,
  `Publisher` varchar(250) DEFAULT NULL,
  `RelationIsPartOf` varchar(250) DEFAULT NULL,
  `CoverageSpatial` varchar(250) DEFAULT NULL,
  `CoverageTemporal` varchar(250) DEFAULT NULL,
  `Rights` varchar(500) DEFAULT NULL,
  `Language` varchar(100) DEFAULT NULL,
  `Identifier` varchar(250) DEFAULT NULL,
  `Transcript` varchar(250) DEFAULT NULL,
  `FileName` varchar(250) DEFAULT NULL,
  `IsInContentDm` tinyint(1) NOT NULL DEFAULT '0',
  `IsRosetta` tinyint(1) NOT NULL DEFAULT '0',
  `IsRosettaForm` tinyint(1) NOT NULL DEFAULT '0',
  `IsRestriction` tinyint(1) NOT NULL DEFAULT '0',
  `LegalNote` varchar(500) DEFAULT NULL,
  `EquipmentUsed` varchar(250) DEFAULT NULL,
  `InterviewerNote` varchar(250) DEFAULT NULL,
  `ReleaseForm` tinyint(1) NOT NULL DEFAULT '0',
  `Place` varchar(250) DEFAULT NULL,
  `TranscriberAssigned` varchar(250) DEFAULT NULL,
  `TranscriberCompleted` datetime(6) DEFAULT NULL,
  `AuditCheckCompleted` varchar(250) DEFAULT NULL,
  `AuditCheckCompletedDate` datetime(6) DEFAULT NULL,
  `FirstEditCompleted` varchar(250) DEFAULT NULL,
  `FirstEditCompletedDate` datetime(6) DEFAULT NULL,
  `SecondEditCompleted` varchar(250) DEFAULT NULL,
  `SecondEditCompletedDate` datetime(6) DEFAULT NULL,
  `DraftSentDate` datetime(6) DEFAULT NULL,
  `EditWithCorrectionCompleted` varchar(250) DEFAULT NULL,
  `EditWithCorrectionDate` datetime(6) DEFAULT NULL,
  `FinalEditCompleted` varchar(250) DEFAULT NULL,
  `FinalEditDate` datetime(6) DEFAULT NULL,
  `FinalSentDate` datetime(6) DEFAULT NULL,
  `TranscriptStatus` tinyint(1) NOT NULL DEFAULT '0',
  `TranscriptLocation` tinyint(3) unsigned NOT NULL,
  `TranscriptNote` varchar(500) DEFAULT NULL,
  `IsAudioFormat` tinyint(1) NOT NULL DEFAULT '0',
  `IsVideoFormat` tinyint(1) NOT NULL DEFAULT '0',
  `IsBornDigital` tinyint(1) NOT NULL DEFAULT '0',
  `OriginalMediumType` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `OriginalMedium` varchar(50) DEFAULT NULL,
  `IsConvertToDigital` tinyint(1) NOT NULL DEFAULT '0',
  `ConvertToDigitalDate` datetime(6) DEFAULT NULL,
  `IsAccessMediaStatus` tinyint(1) NOT NULL DEFAULT '0',
  `MasterFileLocation` varchar(500) DEFAULT NULL,
  `AccessFileLocation` varchar(500) DEFAULT NULL,
  `TranscriberLocation` varchar(500) DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedDate` datetime(6) NOT NULL,
  `UpdatedBy` int(11) NOT NULL,
  `UpdatedDate` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=56 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;


--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `user` (
  `UserId` int(11) NOT NULL AUTO_INCREMENT,
  `UserType` tinyint(3) unsigned DEFAULT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Username` varchar(50) DEFAULT NULL,
  `Password` varchar(50) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `Mobile` varchar(50) DEFAULT NULL,
  `RowId` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;
