


CREATE TABLE `interviewers` (
  `id` smallint(6) NOT NULL AUTO_INCREMENT,
  `InterviewerName` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;


CREATE TABLE `audioequipmentused` (
  `id` smallint(6) NOT NULL AUTO_INCREMENT,
  `AudioEquipmentUsedName` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;


CREATE TABLE `videoequipmentused` (
  `id` smallint(6) NOT NULL AUTO_INCREMENT,
  `VideoEquipmentUsedName` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;


ALTER TABLE `oralcmsdb`.`transcriptions` 
CHANGE COLUMN `EquipmentUsed` `AudioEquipmentUsed` VARCHAR(250) NULL DEFAULT NULL ;


ALTER table oralcmsdb.transcriptions
Add column VideoEquipmentUsed varchar(100) DEFAULT NULL AFTER AudioEquipmentUsed


ALTER TABLE oralcmsdb.transcriptions MODIFY AudioEquipmentUsed VARCHAR(100) ;



CREATE TABLE `usertype` (
  `Id` smallint(6) NOT NULL AUTO_INCREMENT,
  `UserTypeName` varchar(50) NOT NULL,
  `IsHorizontalMenu` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;



