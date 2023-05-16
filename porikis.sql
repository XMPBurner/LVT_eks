-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 16, 2023 at 07:14 PM
-- Server version: 10.4.27-MariaDB
-- PHP Version: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `porikis`
--
CREATE DATABASE IF NOT EXISTS `porikis` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `porikis`;

-- --------------------------------------------------------

--
-- Table structure for table `hotelis`
--

CREATE TABLE `hotelis` (
  `Hotelis_ID` int(11) NOT NULL,
  `Hotela_vards` varchar(45) NOT NULL,
  `Valsts` varchar(45) NOT NULL,
  `Pilsēta` varchar(45) NOT NULL,
  `Adresse` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `hotelis`
--

INSERT INTO `hotelis` (`Hotelis_ID`, `Hotela_vards`, `Valsts`, `Pilsēta`, `Adresse`) VALUES
(1, 'Citadel Hotel', 'Latvija', 'Liepāja', 'Rīgas iela 35'),
(2, 'Utopia Hotel', 'Latvija', 'Rīga', 'Rudens iela 3'),
(3, 'Diorama Hotel', 'Lietuva', 'Viļņa', 'Kęstučio g. 26'),
(4, 'Rainbow Hotel', 'Igaunija', 'Narva', 'Sanglepa 45'),
(5, 'Refresh Motel', 'Lietuva', 'Klaipēda', 'Vėjų g. 10'),
(6, 'Bronze Emperor Resort', 'Igaunija', 'Tallina', 'Karja 25'),
(7, 'Noble Shield Motel', 'Lietuva', 'Kauņa', 'Pasvalio g. 4'),
(8, 'Saffron Temple Hotel', 'Krievija', 'Maskava', 'Izmaylovskiy Bulv., bld. 50, appt. 18'),
(9, 'Cozy Resort', 'Igaunija', 'Valga', 'Ehitajate 24'),
(10, 'Grand Hotel', 'Krievija', 'Sanktpēterburga', 'Karla/marksa, bld. 57, appt. 21'),
(11, 'Antiquity Hotel', 'Igaunija', 'Veru', 'Jõe 21'),
(12, 'Parallel Carnaval Hotel', 'Krievija', 'Abakana', 'Moskovskaya, bld. 4, appt. 183'),
(13, 'Seashore Hotel', 'Krievija', 'Abaza', 'Arzamasskaya 3, bld. 11, appt. 76');

-- --------------------------------------------------------

--
-- Table structure for table `izstaba`
--

CREATE TABLE `izstaba` (
  `Izstaba_ID` int(11) NOT NULL,
  `Skaits` int(11) NOT NULL,
  `Wifi` tinyint(4) NOT NULL,
  `AC` tinyint(4) NOT NULL,
  `Siltums` tinyint(4) NOT NULL,
  `Ratings` int(11) NOT NULL,
  `Cena` int(11) NOT NULL,
  `Hotelis_ID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `izstaba`
--

INSERT INTO `izstaba` (`Izstaba_ID`, `Skaits`, `Wifi`, `AC`, `Siltums`, `Ratings`, `Cena`, `Hotelis_ID`) VALUES
(1, 2, 1, 1, 1, 4, 41, 1),
(2, 2, 1, 1, 0, 5, 55, 2),
(3, 2, 1, 1, 1, 5, 46, 3),
(4, 2, 0, 1, 1, 4, 60, 4),
(5, 3, 1, 1, 1, 4, 44, 5),
(6, 3, 1, 1, 1, 4, 40, 6),
(7, 3, 1, 1, 1, 4, 42, 7),
(8, 3, 1, 0, 0, 4, 70, 8),
(9, 4, 1, 1, 1, 3, 48, 9),
(10, 4, 1, 1, 1, 4, 40, 10),
(11, 4, 1, 1, 1, 4, 46, 11),
(12, 2, 1, 1, 1, 4, 40, 12),
(13, 2, 0, 1, 1, 4, 44, 13),
(14, 2, 1, 1, 1, 3, 50, 1),
(15, 2, 1, 0, 0, 4, 40, 2),
(16, 2, 1, 1, 1, 4, 49, 3),
(17, 2, 1, 1, 1, 4, 40, 4),
(18, 3, 1, 1, 1, 2, 25, 5),
(19, 3, 1, 1, 1, 4, 40, 6),
(20, 3, 0, 1, 1, 4, 46, 7),
(21, 2, 1, 1, 0, 4, 60, 8),
(22, 2, 1, 0, 1, 3, 47, 9),
(23, 4, 1, 1, 1, 4, 40, 10),
(24, 4, 1, 1, 1, 1, 29, 11),
(25, 2, 1, 1, 0, 4, 42, 12),
(26, 2, 0, 0, 1, 4, 64, 13),
(27, 2, 1, 1, 0, 4, 43, 1);

-- --------------------------------------------------------

--
-- Table structure for table `lietotajs`
--

CREATE TABLE `lietotajs` (
  `Lietotajs_ID` int(11) NOT NULL,
  `Vards` varchar(45) NOT NULL,
  `Uzvards` varchar(45) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Password` varchar(45) NOT NULL,
  `Nummurs` int(11) NOT NULL,
  `Status` tinyint(4) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `lietotajs`
--

INSERT INTO `lietotajs` (`Lietotajs_ID`, `Vards`, `Uzvards`, `Email`, `Password`, `Nummurs`, `Status`) VALUES
(1, 'Emils', 'Porikis', 'emils@gmail.com', 'Emils123', 20096512, 0),
(2, 'Alberts', 'Kovaļevskis', 'Alberts@gmail.com', 'Alberts123', 5594614, 0),
(3, 'Keita', 'Lieģe', 'Keita@gmail.com', 'Keita123', 91499846, 0),
(4, 'Katrīna', 'Lieģe', 'Katrīna@gmail.com', 'Katrīna123', 56379722, 0),
(5, 'Emils', 'Porikis', 'Porikis@gmail.com', 'Porikis123', 58265037, 1),
(6, 'Marija', 'Kuda', 'Marija@gmail.com', 'Marija123', 74657206, 1),
(7, 'Tomass', 'Balodis', 'Tomass@gmail.com', 'Tomass123', 10475639, 1);

-- --------------------------------------------------------

--
-- Table structure for table `rezervacija`
--

CREATE TABLE `rezervacija` (
  `Rezervacija_ID` int(11) NOT NULL,
  `Check_in` timestamp NULL DEFAULT NULL,
  `Checkout` timestamp NULL DEFAULT NULL,
  `Izmaksa` double DEFAULT NULL,
  `Lietotajs_ID` int(11) NOT NULL,
  `Hotelis_ID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `hotelis`
--
ALTER TABLE `hotelis`
  ADD PRIMARY KEY (`Hotelis_ID`);

--
-- Indexes for table `izstaba`
--
ALTER TABLE `izstaba`
  ADD PRIMARY KEY (`Izstaba_ID`,`Hotelis_ID`),
  ADD KEY `fk_Izstaba_Hotelis1` (`Hotelis_ID`);

--
-- Indexes for table `lietotajs`
--
ALTER TABLE `lietotajs`
  ADD PRIMARY KEY (`Lietotajs_ID`);

--
-- Indexes for table `rezervacija`
--
ALTER TABLE `rezervacija`
  ADD PRIMARY KEY (`Rezervacija_ID`),
  ADD KEY `fk_Rezervacija_Lietotajs` (`Lietotajs_ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `hotelis`
--
ALTER TABLE `hotelis`
  MODIFY `Hotelis_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT for table `izstaba`
--
ALTER TABLE `izstaba`
  MODIFY `Izstaba_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT for table `lietotajs`
--
ALTER TABLE `lietotajs`
  MODIFY `Lietotajs_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `rezervacija`
--
ALTER TABLE `rezervacija`
  MODIFY `Rezervacija_ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `izstaba`
--
ALTER TABLE `izstaba`
  ADD CONSTRAINT `fk_Izstaba_Hotelis1` FOREIGN KEY (`Hotelis_ID`) REFERENCES `hotelis` (`Hotelis_ID`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `rezervacija`
--
ALTER TABLE `rezervacija`
  ADD CONSTRAINT `fk_Rezervacija_Lietotajs` FOREIGN KEY (`Lietotajs_ID`) REFERENCES `lietotajs` (`Lietotajs_ID`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
