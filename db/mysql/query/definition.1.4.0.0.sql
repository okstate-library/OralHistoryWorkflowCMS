


CREATE TABLE `predefineduser` (
  `id` smallint(6) NOT NULL AUTO_INCREMENT,
  `usertype` tinyint(1) NOT NULL,
  `Name` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4;


ALTER TABLE `oralcmsdb`.`transcriptions` 
DROP COLUMN `FinalEditDate`;
DROP COLUMN `EditWithCorrectionDate`;
DROP COLUMN `SecondEditCompletedDate`;
DROP COLUMN `FirstEditCompletedDate`;
DROP COLUMN `AuditCheckCompletedDate`;
DROP COLUMN `TranscriberCompleted`;

ALTER TABLE `oralcmsdb`.`transcriptions` 
ADD COLUMN `InterviewDate1` DATE NULL AFTER `InterviewDate`;
ADD COLUMN `InterviewDate2` DATE NULL AFTER `InterviewDate1`;
ADD COLUMN `ThirdEditCompleted` DATE NULL AFTER `SecondEditCompleted`;
ADD COLUMN `IsDarkArchive` TINYINT(1) NULL AFTER `IsRestriction`;

CHANGE COLUMN `IsInContentDm` `IsOnline` TINYINT(1) NOT NULL DEFAULT '0' ;
CHANGE COLUMN `IsDarkArchive` `IsDarkArchive` TINYINT(1) NOT NULL DEFAULT 0 ;
CHANGE COLUMN `LegalNote` `RestrictionNote` VARCHAR(500) NULL DEFAULT NULL ;
CHANGE COLUMN `RestrictionNote` `RestrictionNote` VARCHAR(500) NULL DEFAULT NULL AFTER `IsRestriction`;

ADD COLUMN `DarkArchiveNote` VARCHAR(200) NULL AFTER `IsDarkArchive`;

ALTER TABLE `oralcmsdb`.`transcriptions` 
CHANGE COLUMN `InterviewDate` `InterviewDate` VARCHAR(10) NOT NULL ,
CHANGE COLUMN `InterviewDate1` `InterviewDate1` VARCHAR(10) NULL DEFAULT NULL ,
CHANGE COLUMN `InterviewDate2` `InterviewDate2` VARCHAR(10) NULL DEFAULT NULL ;


ALTER TABLE `oralcmsdb`.`transcriptions` 
CHANGE COLUMN `TranscriberAssigned` `TranscriberAssigned` VARCHAR(150) NULL DEFAULT NULL ,
CHANGE COLUMN `AuditCheckCompleted` `AuditCheckCompleted` VARCHAR(150) NULL DEFAULT NULL ,
CHANGE COLUMN `FirstEditCompleted` `FirstEditCompleted` VARCHAR(150) NULL DEFAULT NULL ,
CHANGE COLUMN `SecondEditCompleted` `SecondEditCompleted` VARCHAR(150) NULL DEFAULT NULL ,
CHANGE COLUMN `ThirdEditCompleted` `ThirdEditCompleted` VARCHAR(100) NULL DEFAULT NULL ,
CHANGE COLUMN `EditWithCorrectionCompleted` `EditWithCorrectionCompleted` VARCHAR(150) NULL DEFAULT NULL ,
CHANGE COLUMN `FinalEditCompleted` `FinalEditCompleted` VARCHAR(150) NULL DEFAULT NULL ;
