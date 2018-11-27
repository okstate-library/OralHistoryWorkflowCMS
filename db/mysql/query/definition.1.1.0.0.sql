ALTER table oralcmsdb.transcriptions
Add column EquipmentNumber varchar(250) DEFAULT NULL AFTER EquipmentUsed;

ALTER table oralcmsdb.transcriptions
Add column InterviewerDescription varchar(1000) DEFAULT NULL AFTER Subject;

ALTER table oralcmsdb.transcriptions
Add column InterviewerKeywords varchar(1000) DEFAULT NULL AFTER InterviewerDescription;

ALTER table oralcmsdb.transcriptions
Add column InterviewerSubjects varchar(1000) DEFAULT NULL AFTER InterviewerKeywords;

ALTER table oralcmsdb.transcriptions
Add column MetadataDraft varchar(250) DEFAULT NULL AFTER TranscriptStatus;

ALTER table oralcmsdb.transcriptions
Add column SentOut tinyint(1) NOT NULL DEFAULT '0' AFTER TranscriberLocation;

ALTER TABLE `oralcmsdb`.`transcriptions` 
CHANGE COLUMN `InterviewerKeywords` `InterviewerKeywords` BLOB NULL DEFAULT NULL ,
CHANGE COLUMN `InterviewerSubjects` `InterviewerSubjects` BLOB NULL DEFAULT NULL ,
CHANGE COLUMN `ScopeAndContents` `ScopeAndContents` BLOB NULL DEFAULT NULL ;
