-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `Porikis` DEFAULT CHARACTER SET utf8 ;
USE `Porikis` ;

-- -----------------------------------------------------
-- Table `mydb`.`Lietotajs`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Porikis`.`Lietotajs` (
  `Lietotajs_ID` INT NOT NULL AUTO_INCREMENT,
  `Vards` VARCHAR(45) NOT NULL,
  `Uzvards` VARCHAR(45) NOT NULL,
  `Email` VARCHAR(255) NOT NULL,
  `Password` VARCHAR(45) NOT NULL,
  `Nummurs` INT NOT NULL,
  `Status` TINYINT NOT NULL DEFAULT 0,
  PRIMARY KEY (`Lietotajs_ID`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `Porikis`.`Rezervacija` (
  `Rezervacija_ID` INT NOT NULL AUTO_INCREMENT,
  `Check_in` TIMESTAMP NULL,
  `Checkout` TIMESTAMP NULL,
  `Izmaksa` DOUBLE NULL,
  `Lietotajs_ID` INT NOT NULL,
  `Administratori_ID` INT NOT NULL,
  `Hotelis_ID` INT NOT NULL,
  PRIMARY KEY (`Rezervacija_ID`, `Lietotajs_ID`, `Administratori_ID`),
  CONSTRAINT `fk_Rezervacija_Lietotajs1`
    FOREIGN KEY (`Lietotajs_ID`)
    REFERENCES `Porikis`.`Lietotajs1` (`Lietotajs_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Rezervacija_Hotelis1`
    FOREIGN KEY (`Hotelis_ID`)
    REFERENCES `Porikis`.`Hotelis` (`Hotelis_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Hotelis`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Porikis`.`Hotelis` (
  `Hotelis_ID` INT NOT NULL AUTO_INCREMENT,
  `Hotela_vards` VARCHAR(45) NOT NULL,
  `Valsts` VARCHAR(45) NOT NULL,
  `Pilsēta` VARCHAR(45) NOT NULL,
  `Adresse` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Hotelis_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Izstaba`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Porikis`.`Izstaba` (
  `Izstaba_ID` INT NOT NULL AUTO_INCREMENT,
  `Skaits` INT NOT NULL,
  `Wifi` TINYINT NOT NULL,
  `AC` TINYINT NOT NULL,
  `Siltums` TINYINT NOT NULL,
  `Ratings` INT NOT NULL,
  `Cena` INT NOT NULL,
  `Hotelis_ID` INT NOT NULL,
  PRIMARY KEY (`Izstaba_ID`, `Hotelis_ID`),
  CONSTRAINT `fk_Izstaba_Hotelis1`
    FOREIGN KEY (`Hotelis_ID`)
    REFERENCES `Porikis`.`Hotelis` (`Hotelis_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

INSERT INTO Lietotajs (Vards, Uzvards, Email, Password, Nummurs, Status)
Values('Emils', 'Porikis', 'emils@gmail.com', 'Emils123', 20096512, 0),
('Alberts', 'Kovaļevskis', 'Alberts@gmail.com', 'Alberts123', 05594614, 0),
('Keita', 'Lieģe', 'Keita@gmail.com', 'Keita123', 91499846, 0),
('Katrīna', 'Lieģe', 'Katrīna@gmail.com', 'Katrīna123', 56379722, 0),
('Emils', 'Porikis', 'Porikis@gmail.com', 'Porikis123', 58265037, 1),
('Marija', 'Kuda', 'Marija@gmail.com', 'Marija123',74657206, 1),
('Tomass', 'Balodis', 'Tomass@gmail.com', 'Tomass123',10475639, 1);

INSERT INTO Hotelis (Hotela_vards, Valsts, Pilsēta, adresse)
Values ('Citadel Hotel','Latvija','Liepāja','Rīgas iela 35'),
('Utopia Hotel','Latvija','Rīga','Rudens iela 3'),
('Diorama Hotel','Lietuva','Viļņa','Kęstučio g. 26'),
('Rainbow Hotel','Igaunija','Narva','Sanglepa 45'),
('Refresh Motel','Lietuva','Klaipēda','Vėjų g. 10'),
('Bronze Emperor Resort','Igaunija','Tallina','Karja 25'),
('Noble Shield Motel','Lietuva','Kauņa','Pasvalio g. 4'),
('Saffron Temple Hotel','Krievija','Maskava','Izmaylovskiy Bulv., bld. 50, appt. 18'),
('Cozy Resort','Igaunija','Valga','Ehitajate 24'),
('Grand Hotel','Krievija','Sanktpēterburga','Karla/marksa, bld. 57, appt. 21'),
('Antiquity Hotel','Igaunija','Veru','Jõe 21'),
('Parallel Carnaval Hotel','Krievija','Abakana','Moskovskaya, bld. 4, appt. 183'),
('Seashore Hotel','Krievija','Abaza','Arzamasskaya 3, bld. 11, appt. 76');

INSERT INTO Izstaba (Skaits, Wifi, AC, Siltums, Ratings, Cena, Hotelis_ID)
Values (2, 1, 1, 1, 4, 41, 1),
(2, 1, 1, 0, 5, 55, 2),
(2, 1, 1, 1, 5, 46, 3),
(2, 0, 1, 1, 4, 60, 4),
(3, 1, 1, 1, 4, 44, 5),
(3, 1, 1, 1, 4, 40, 6),
(3, 1, 1, 1, 4, 42, 7),
(3, 1, 0, 0, 4, 70, 8),
(4, 1, 1, 1, 3, 48, 9),
(4, 1, 1, 1, 4, 40, 10),
(4, 1, 1, 1, 4, 46, 11),
(2, 1, 1, 1, 4, 40, 12),
(2, 0, 1, 1, 4, 44, 13),
(2, 1, 1, 1, 3, 50, 1),
(2, 1, 0, 0, 4, 40, 2),
(2, 1, 1, 1, 4, 49, 3),
(2, 1, 1, 1, 4, 40, 4),
(3, 1, 1, 1, 2, 25, 5),
(3, 1, 1, 1, 4, 40, 6),
(3, 0, 1, 1, 4, 46, 7),
(2, 1, 1, 0, 4, 60, 8),
(2, 1, 0, 1, 3, 47, 9),
(4, 1, 1, 1, 4, 40, 10),
(4, 1, 1, 1, 1, 29, 11),
(2, 1, 1, 0, 4, 42, 12),
(2, 0, 0, 1, 4, 64, 13),
(2, 1, 1, 0, 4, 43, 1);