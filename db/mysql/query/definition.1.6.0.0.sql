
ALTER TABLE `oralcmsdb`.`transcriptions` 
CHANGE COLUMN `ReasonForPriority` `ReasonForPriority` VARCHAR(50) NULL DEFAULT NULL,
CHANGE COLUMN `Title` `Title` VARCHAR(250) NOT NULL,
CHANGE COLUMN `Description` `Description` TEXT NULL DEFAULT NULL ,
CHANGE COLUMN `Keywords` `Keywords` VARCHAR(250) NULL DEFAULT NULL ,
CHANGE COLUMN `Subject` `Subject` VARCHAR(300) NULL DEFAULT NULL ,
CHANGE COLUMN `InterviewerDescription` `InterviewerDescription` TEXT NULL DEFAULT NULL ,
CHANGE COLUMN `InterviewerKeywords` `InterviewerKeywords` VARCHAR(500) NULL DEFAULT NULL ,
CHANGE COLUMN `InterviewerSubjects` `InterviewerSubjects` VARCHAR(300) NULL DEFAULT NULL ,
CHANGE COLUMN `ScopeAndContents` `ScopeAndContents` TEXT NULL DEFAULT NULL ,
CHANGE COLUMN `Format` `Format` VARCHAR(100) NULL DEFAULT NULL ,
CHANGE COLUMN `Type` `Type` VARCHAR(100) NULL DEFAULT NULL ,
CHANGE COLUMN `RelationIsPartOf` `RelationIsPartOf` VARCHAR(100) NULL DEFAULT NULL ,
CHANGE COLUMN `Language` `Language` VARCHAR(20) NULL DEFAULT NULL ,
CHANGE COLUMN `Identifier` `Identifier` VARCHAR(50) NULL DEFAULT NULL ,
CHANGE COLUMN `FileName` `FileName` VARCHAR(100) NULL DEFAULT NULL ,
CHANGE COLUMN `RestrictionNote` `RestrictionNote` VARCHAR(250) NULL DEFAULT NULL ,
CHANGE COLUMN `DarkArchiveNote` `DarkArchiveNote` VARCHAR(250) NULL DEFAULT NULL ,
CHANGE COLUMN `AudioEquipmentUsed` `AudioEquipmentUsed` VARCHAR(50) NULL DEFAULT NULL ,
CHANGE COLUMN `VideoEquipmentUsed` `VideoEquipmentUsed` VARCHAR(50) NULL DEFAULT NULL ,
CHANGE COLUMN `EquipmentNumber` `EquipmentNumber` VARCHAR(20) NOT NULL ,
CHANGE COLUMN `Place` `Place` VARCHAR(100) NULL DEFAULT NULL ,
CHANGE COLUMN `TranscriberAssigned` `TranscriberAssigned` VARCHAR(50) NULL DEFAULT NULL ,
CHANGE COLUMN `AuditCheckCompleted` `AuditCheckCompleted` VARCHAR(50) NULL DEFAULT NULL ,
CHANGE COLUMN `FirstEditCompleted` `FirstEditCompleted` VARCHAR(50) NULL DEFAULT NULL ,
CHANGE COLUMN `SecondEditCompleted` `SecondEditCompleted` VARCHAR(50) NULL DEFAULT NULL ,
CHANGE COLUMN `ThirdEditCompleted` `ThirdEditCompleted` VARCHAR(50) NULL DEFAULT NULL ,
CHANGE COLUMN `EditWithCorrectionCompleted` `EditWithCorrectionCompleted` VARCHAR(50) NULL DEFAULT NULL ,
CHANGE COLUMN `FinalEditCompleted` `FinalEditCompleted` VARCHAR(50) NULL DEFAULT NULL ,
CHANGE COLUMN `MetadataDraft` `MetadataDraft` VARCHAR(50) NULL DEFAULT NULL ,
CHANGE COLUMN `TranscriptNote` `TranscriptNote` VARCHAR(250) NULL DEFAULT NULL ,
CHANGE COLUMN `MasterFileLocation` `MasterFileLocation` VARCHAR(150) NULL DEFAULT NULL ,
CHANGE COLUMN `AccessFileLocation` `AccessFileLocation` VARCHAR(150) NULL DEFAULT NULL ,
CHANGE COLUMN `TranscriptLocation` `TranscriptLocation` VARCHAR(150) NULL DEFAULT NULL ;



CREATE TABLE `repository` (
  `Id` smallint(1) NOT NULL AUTO_INCREMENT,
  `RepositoryName` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='			';



ALTER TABLE `oralcmsdb`.`subseries` 
ADD COLUMN `Number` VARCHAR(50) NULL DEFAULT NULL AFTER `SubseriesName`,
ADD COLUMN `Dates` VARCHAR(50) NULL DEFAULT NULL AFTER `Number`,
ADD COLUMN `Note` TEXT NULL DEFAULT NULL AFTER `Dates`,
ADD COLUMN `Subjects` VARCHAR(500) NULL DEFAULT NULL AFTER `Note`,
ADD COLUMN `Keywords` VARCHAR(500) NULL DEFAULT NULL AFTER `Subjects`,
ADD COLUMN `Description` TEXT NULL DEFAULT NULL AFTER `Keywords`,
ADD COLUMN `ScopeAndContent` TEXT NULL DEFAULT NULL AFTER `Description`,
ADD COLUMN `CustodialHistory` TEXT NULL DEFAULT NULL AFTER `ScopeAndContent`,
ADD COLUMN `Size` VARCHAR(250) NULL DEFAULT NULL AFTER `CustodialHistory`,
ADD COLUMN `Acquisitioninfo` TEXT NULL DEFAULT NULL AFTER `Size`,
ADD COLUMN `Language` VARCHAR(100) NULL DEFAULT NULL AFTER `Acquisitioninfo`,
ADD COLUMN `PreservationNote` TEXT NULL DEFAULT NULL AFTER `Language`,
ADD COLUMN `Rights` TEXT NULL DEFAULT NULL AFTER `PreservationNote`,
ADD COLUMN `AccessRestrictions` TEXT NULL DEFAULT NULL AFTER `Rights`,
ADD COLUMN `PublicationRights` TEXT NULL DEFAULT NULL AFTER `AccessRestrictions`,
ADD COLUMN `PreferredCitation` VARCHAR(500) NULL DEFAULT NULL AFTER `PublicationRights`,
ADD COLUMN `RelatedCollection` VARCHAR(500) NULL DEFAULT NULL AFTER `PreferredCitation`,
ADD COLUMN `SeparatedMaterial` VARCHAR(500) NULL DEFAULT NULL AFTER `RelatedCollection`,
ADD COLUMN `OriginalLocation` VARCHAR(500) NULL DEFAULT NULL AFTER `SeparatedMaterial`,
ADD COLUMN `CopiesLocation` VARCHAR(500) NULL DEFAULT NULL AFTER `OriginalLocation`,
ADD COLUMN `PublicationNote` TEXT NULL DEFAULT NULL AFTER `CopiesLocation`,
ADD COLUMN `Creator` TEXT NULL DEFAULT NULL AFTER `PublicationNote`,
ADD COLUMN `Contributors` TEXT NULL DEFAULT NULL AFTER `Creator`,
ADD COLUMN `ProcessedBy` TEXT NULL DEFAULT NULL AFTER `Contributors`,
ADD COLUMN `Sponsors` TEXT NULL DEFAULT NULL AFTER `ProcessedBy`,
ADD COLUMN `CreatedBy` INT(11)  NULL AFTER `Sponsors`,
ADD COLUMN `CreatedDate` DATETIME NOT NULL AFTER `CreatedBy`,
ADD COLUMN `UpdatedBy` INT(11) NOT NULL AFTER `CreatedDate`,
ADD COLUMN `UpdatedDate` DATETIME NOT NULL AFTER `UpdatedBy`;


UPDATE oralcmsdb.subseries
SET 
    CreatedBy = 1,
    UpdatedBy = 1,
	CreatedDate = '2019-10-01 00:00:00',
	UpdatedDate = '2019-10-01 00:00:00'

WHERE  
	id > 0;

ALTER TABLE `oralcmsdb`.`subseries` 
CHANGE COLUMN `CreatedBy` `CreatedBy` INT(11) NOT NULL ,
CHANGE COLUMN `CreatedDate` `CreatedDate` DATETIME NOT NULL ,
CHANGE COLUMN `UpdatedBy` `UpdatedBy` INT(11) NOT NULL ,
CHANGE COLUMN `UpdatedDate` `UpdatedDate` DATETIME NOT NULL ;


ALTER TABLE `oralcmsdb`.`collections` 
ADD COLUMN `Number` VARCHAR(50) NULL DEFAULT NULL AFTER `CollectionName`,
ADD COLUMN `Dates` VARCHAR(50) NULL DEFAULT NULL AFTER `Number`,
ADD COLUMN `Note` TEXT NULL DEFAULT NULL AFTER `Dates`,
ADD COLUMN `Subjects` VARCHAR(500) NULL DEFAULT NULL AFTER `Note`,
ADD COLUMN `Keywords` VARCHAR(500) NULL DEFAULT NULL AFTER `Subjects`,
ADD COLUMN `Description` TEXT NULL DEFAULT NULL AFTER `Keywords`,
ADD COLUMN `ScopeAndContent` TEXT NULL DEFAULT NULL AFTER `Description`,
ADD COLUMN `CustodialHistory` TEXT NULL DEFAULT NULL AFTER `ScopeAndContent`,
ADD COLUMN `Size` VARCHAR(250) NULL DEFAULT NULL AFTER `CustodialHistory`,
ADD COLUMN `Acquisitioninfo` TEXT NULL DEFAULT NULL AFTER `Size`,
ADD COLUMN `Language` VARCHAR(100) NULL DEFAULT NULL AFTER `Acquisitioninfo`,
ADD COLUMN `PreservationNote` TEXT NULL DEFAULT NULL AFTER `Language`,
ADD COLUMN `Rights` TEXT NULL DEFAULT NULL AFTER `PreservationNote`,
ADD COLUMN `AccessRestrictions` TEXT NULL DEFAULT NULL AFTER `Rights`,
ADD COLUMN `PublicationRights` TEXT NULL DEFAULT NULL AFTER `AccessRestrictions`,
ADD COLUMN `PreferredCitation` VARCHAR(500) NULL DEFAULT NULL AFTER `PublicationRights`,
ADD COLUMN `RelatedCollection` VARCHAR(500) NULL DEFAULT NULL AFTER `PreferredCitation`,
ADD COLUMN `SeparatedMaterial` VARCHAR(500) NULL DEFAULT NULL AFTER `RelatedCollection`,
ADD COLUMN `OriginalLocation` VARCHAR(500) NULL DEFAULT NULL AFTER `SeparatedMaterial`,
ADD COLUMN `CopiesLocation` VARCHAR(500) NULL DEFAULT NULL AFTER `OriginalLocation`,
ADD COLUMN `PublicationNote` TEXT NULL DEFAULT NULL AFTER `CopiesLocation`,
ADD COLUMN `Creator` TEXT NULL DEFAULT NULL AFTER `PublicationNote`,
ADD COLUMN `Contributors` TEXT NULL DEFAULT NULL AFTER `Creator`,
ADD COLUMN `ProcessedBy` TEXT NULL DEFAULT NULL AFTER `Contributors`,
ADD COLUMN `Sponsors` TEXT NULL DEFAULT NULL AFTER `ProcessedBy`,
ADD COLUMN `CreatedBy` INT(11)  NULL AFTER `Sponsors`,
ADD COLUMN `CreatedDate` DATETIME NULL AFTER `CreatedBy`,
ADD COLUMN `UpdatedBy` INT(11) NULL AFTER `CreatedDate`,
ADD COLUMN `UpdatedDate` DATETIME NULL AFTER `UpdatedBy`;



UPDATE oralcmsdb.collections
SET 
	RepositoryId =1,
    CreatedBy = 1,
    UpdatedBy = 1,
	CreatedDate = '2019-10-01 00:00:00',
	UpdatedDate = '2019-10-01 00:00:00'

WHERE  
	id > 0;

ALTER TABLE `oralcmsdb`.`collections` 
CHANGE COLUMN `CreatedBy` `CreatedBy` INT(11) NOT NULL ,
CHANGE COLUMN `CreatedDate` `CreatedDate` DATETIME NOT NULL ,
CHANGE COLUMN `UpdatedBy` `UpdatedBy` INT(11) NOT NULL ,
CHANGE COLUMN `UpdatedDate` `UpdatedDate` DATETIME NOT NULL ;