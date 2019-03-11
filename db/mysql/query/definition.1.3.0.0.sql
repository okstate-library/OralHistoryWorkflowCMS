
ALTER TABLE `oralcmsdb`.`transcriptions` 
CHANGE COLUMN `ScopeAndContents` `ScopeAndContents` VARCHAR(1000) NULL DEFAULT NULL ,
CHANGE COLUMN `MasterFileLocation` `MasterFileLocation` VARCHAR(250) NULL DEFAULT NULL ,
CHANGE COLUMN `AccessFileLocation` `AccessFileLocation` VARCHAR(250) NULL DEFAULT NULL ,
CHANGE COLUMN `TranscriberLocation` `TranscriberLocation` VARCHAR(250) NULL DEFAULT NULL ;

