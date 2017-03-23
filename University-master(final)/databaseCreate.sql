CREATE TABLE `courses` (
   `ID` int(11) NOT NULL,
   `Name` varchar(45) DEFAULT NULL,
   `Teacher` varchar(45) DEFAULT NULL,
   `Year` int(11) DEFAULT NULL,
   PRIMARY KEY (`ID`)
 ) ENGINE=InnoDB DEFAULT CHARSET=utf8;
 CREATE TABLE `student` (
   `ID` int(11) NOT NULL,
   `Name` varchar(45) DEFAULT NULL,
   `BirthDate` date DEFAULT NULL,
   `Address` varchar(45) DEFAULT NULL,
   PRIMARY KEY (`ID`)
 ) ENGINE=InnoDB DEFAULT CHARSET=utf8;
 CREATE TABLE `register` (
   `IDStudent` int(11) DEFAULT NULL,
   `IDCourse` int(11) DEFAULT NULL,
   KEY `IDStudent` (`IDStudent`),
   KEY `IDCourse` (`IDCourse`),
   CONSTRAINT `IDCourse` FOREIGN KEY (`IDCourse`) REFERENCES `courses` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
   CONSTRAINT `IDStudent` FOREIGN KEY (`IDStudent`) REFERENCES `student` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
 ) ENGINE=InnoDB DEFAULT CHARSET=utf8;
DELIMITER //

CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateStudent`(IN StudentID INT,IN StudentName varchar(45), IN StudentBirthDate date, IN StudentAddress varchar(45))
BEGIN
UPDATE studentregister
     SET   Name=StudentName, BirthDate= StudentBirthDate, Address = StudentAddress
     WHERE ID = StudentID ;
END//
DELIMITER ;
 