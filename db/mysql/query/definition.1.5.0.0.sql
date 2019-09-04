
ALTER TABLE `oralcmsdb`.`transcriptions` 
ADD COLUMN `GeneralNote` VARCHAR(250) NULL AFTER `InterviewerNote`;
ADD COLUMN `TechnicalSpecification` VARCHAR(250) NULL AFTER `IsAccessMediaStatus`;

ALTER TABLE `oralcmsdb`.`transcriptions` 
DROP COLUMN `TranscriptLocation`;

ALTER TABLE `oralcmsdb`.`transcriptions` 
CHANGE COLUMN `TranscriberLocation` `TranscriptLocation` VARCHAR(250) NULL DEFAULT NULL ;
