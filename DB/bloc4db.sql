-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 22, 2024 at 11:56 AM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `bloc4db`
--
CREATE DATABASE IF NOT EXISTS `bloc4db` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `bloc4db`;

-- --------------------------------------------------------

--
-- Table structure for table `connexion`
--

DROP TABLE IF EXISTS `connexion`;
CREATE TABLE `connexion` (
  `mot_de_passe` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `salaries`
--

DROP TABLE IF EXISTS `salaries`;
CREATE TABLE `salaries` (
  `id` int(11) NOT NULL,
  `nom` varchar(150) NOT NULL,
  `prenom` varchar(150) NOT NULL,
  `telephone_fixe` varchar(14) NOT NULL,
  `telephone_mobile` varchar(14) NOT NULL,
  `email` varchar(100) NOT NULL,
  `services_id` int(11) DEFAULT NULL,
  `sites_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `services`
--

DROP TABLE IF EXISTS `services`;
CREATE TABLE `services` (
  `id` int(11) NOT NULL,
  `nom` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `sites`
--

DROP TABLE IF EXISTS `sites`;
CREATE TABLE `sites` (
  `id` int(11) NOT NULL,
  `nom` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `salaries`
--
ALTER TABLE `salaries`
  ADD PRIMARY KEY (`id`),
  ADD KEY `service` (`services_id`),
  ADD KEY `site` (`sites_id`);

--
-- Indexes for table `services`
--
ALTER TABLE `services`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `sites`
--
ALTER TABLE `sites`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `salaries`
--
ALTER TABLE `salaries`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `services`
--
ALTER TABLE `services`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `sites`
--
ALTER TABLE `sites`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `salaries`
--
ALTER TABLE `salaries`
  ADD CONSTRAINT `service` FOREIGN KEY (`services_id`) REFERENCES `services` (`id`) ON DELETE SET NULL ON UPDATE CASCADE,
  ADD CONSTRAINT `site` FOREIGN KEY (`sites_id`) REFERENCES `sites` (`id`) ON DELETE SET NULL ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;


INSERT INTO `connexion` (`mot_de_passe`) VALUES
('motdepasse2024');

INSERT INTO `services` (`nom`) VALUES
('Développement'),
('Marketing'),
('Ressources Humaines'),
('Comptabilité'),
('Ventes'),
('Logistique'),
('Support Technique'),
('Administration'),
('Direction'),
('Qualité');


INSERT INTO `sites` (`nom`) VALUES
('Paris'),
('Lyon'),
('Marseille'),
('Toulouse'),
('Lille'),
('Nantes'),
('Bordeaux'),
('Strasbourg'),
('Rennes'),
('Nice');


INSERT INTO `salaries` (`nom`, `prenom`, `telephone_fixe`, `telephone_mobile`, `email`, `services_id`, `sites_id`) VALUES
('Dupont', 'Pierre', '0145123456', '0612345678', 'pierre.dupont@example.com', 1, 1),
('Martin', 'Sophie', '0145234567', '0623456789', 'sophie.martin@example.com', 2, 2),
('Bernard', 'Luc', '0145345678', '0634567890', 'luc.bernard@example.com', 3, 3),
('Lemoine', 'Claire', '0145456789', '0645678901', 'claire.lemoine@example.com', 4, 4),
('Girard', 'Michel', '0145567890', '0656789012', 'michel.girard@example.com', 5, 5),
('Rousseau', 'Julie', '0145678901', '0667890123', 'julie.rousseau@example.com', 6, 6),
('Petit', 'Antoine', '0145789012', '0678901234', 'antoine.petit@example.com', 7, 7),
('Moreau', 'Nathalie', '0145890123', '0689012345', 'nathalie.moreau@example.com', 8, 8),
('Faure', 'David', '0145901234', '0690123456', 'david.faure@example.com', 9, 9),
('Benoit', 'Caroline', '0146012345', '0611234567', 'caroline.benoit@example.com', 10, 10);
