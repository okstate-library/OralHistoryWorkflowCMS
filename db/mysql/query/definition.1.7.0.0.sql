

-- Truncate all table.
TRUNCATE TABLE audioequipmentused;
TRUNCATE TABLE collections;
TRUNCATE TABLE keywords;
TRUNCATE TABLE predefineduser;
TRUNCATE TABLE repositories;
TRUNCATE TABLE subjects;
TRUNCATE TABLE subseries;
TRUNCATE TABLE transcriptions;
TRUNCATE TABLE user;
TRUNCATE TABLE usertype;
TRUNCATE TABLE videoequipmentused;



ALTER TABLE `oralcmsdb`.`transcriptions` 
CHANGE COLUMN `Keywords` `Keywords` TEXT NULL DEFAULT NULL;
CHANGE COLUMN `Subject` `Subject` TEXT NULL DEFAULT NULL;
CHANGE COLUMN `Rights` `Rights` TEXT NULL DEFAULT NULL ;



