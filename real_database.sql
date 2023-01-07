-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jan 07, 2023 at 10:24 AM
-- Server version: 10.4.25-MariaDB
-- PHP Version: 8.1.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `real_database`
--

-- --------------------------------------------------------

--
-- Table structure for table `account_project`
--

CREATE TABLE `account_project` (
  `AccountId` bigint(20) NOT NULL,
  `ProjectId` bigint(20) NOT NULL,
  `Id` bigint(20) NOT NULL,
  `Sort` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `account_real_news`
--

CREATE TABLE `account_real_news` (
  `AccountId` bigint(20) NOT NULL,
  `RealEstateId` bigint(20) NOT NULL,
  `Id` bigint(20) NOT NULL,
  `Sort` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

CREATE TABLE `categories` (
  `Id` bigint(20) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Sort` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `cities`
--

CREATE TABLE `cities` (
  `Id` bigint(20) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Created` datetime(6) NOT NULL DEFAULT current_timestamp(6),
  `Updated` datetime(6) NOT NULL,
  `Sort` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `directions`
--

CREATE TABLE `directions` (
  `Id` bigint(20) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Sort` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `districts`
--

CREATE TABLE `districts` (
  `Id` bigint(20) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `city` bigint(20) NOT NULL,
  `Created` datetime(6) NOT NULL DEFAULT current_timestamp(6),
  `Updated` datetime(6) NOT NULL,
  `Sort` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `images`
--

CREATE TABLE `images` (
  `Id` bigint(20) NOT NULL,
  `image` varchar(255) NOT NULL,
  `batdongsan_id` bigint(20) NOT NULL,
  `Sort` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `news`
--

CREATE TABLE `news` (
  `Id` bigint(20) NOT NULL,
  `tieude` longtext NOT NULL,
  `loaitin` longtext NOT NULL,
  `tomtat` longtext NOT NULL,
  `noidung` longtext NOT NULL,
  `Thumbnail` longtext NOT NULL,
  `ReadTime` int(11) NOT NULL,
  `created_datetime` datetime(6) NOT NULL,
  `updated_datetime` datetime(6) NOT NULL,
  `Sort` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `notifications`
--

CREATE TABLE `notifications` (
  `Id` bigint(20) NOT NULL,
  `AccountId` bigint(20) NOT NULL,
  `RealId` bigint(20) NOT NULL,
  `NewsId` bigint(20) NOT NULL,
  `IsRead` tinyint(1) NOT NULL DEFAULT 0,
  `Created` datetime(6) NOT NULL DEFAULT current_timestamp(6),
  `Updated` datetime(6) NOT NULL DEFAULT current_timestamp(6),
  `Sort` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `projects`
--

CREATE TABLE `projects` (
  `Id` bigint(20) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Investor` varchar(255) NOT NULL,
  `DistrictId` bigint(20) NOT NULL,
  `Address` varchar(255) NOT NULL,
  `Thumbnail` varchar(255) NOT NULL,
  `Description` varchar(255) NOT NULL,
  `Floor` int(11) NOT NULL,
  `Acreage` double NOT NULL,
  `AcreageFloor` double NOT NULL,
  `ApartmentNumber` int(11) NOT NULL,
  `HandedOver` varchar(255) NOT NULL,
  `Created` datetime(6) NOT NULL DEFAULT current_timestamp(6),
  `Updated` datetime(6) NOT NULL DEFAULT current_timestamp(6),
  `Sort` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `real_estates`
--

CREATE TABLE `real_estates` (
  `Id` bigint(20) NOT NULL,
  `tieude` varchar(255) NOT NULL,
  `dientich_matbang` double NOT NULL,
  `diachi` longtext NOT NULL,
  `gia` double NOT NULL,
  `kieugia` longtext NOT NULL,
  `Room` int(11) NOT NULL,
  `Longitude` double NOT NULL,
  `Latitude` double NOT NULL,
  `mota` longtext NOT NULL,
  `CreatorId` bigint(20) NOT NULL,
  `ProjectId` bigint(20) NOT NULL,
  `contact_name` longtext NOT NULL,
  `contact_phone` longtext NOT NULL,
  `IsSale` tinyint(1) NOT NULL DEFAULT 0,
  `Attachment` longtext NOT NULL,
  `WardId` bigint(20) NOT NULL,
  `thanhpho` int(11) NOT NULL,
  `quan` int(11) NOT NULL,
  `loai` bigint(20) NOT NULL,
  `huong` bigint(20) NOT NULL,
  `StartAt` datetime(6) NOT NULL,
  `EndAt` datetime(6) NOT NULL,
  `created_datetime` datetime(6) NOT NULL DEFAULT current_timestamp(6),
  `updated_datetime` datetime(6) NOT NULL DEFAULT current_timestamp(6),
  `Sort` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `streets`
--

CREATE TABLE `streets` (
  `Id` bigint(20) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `ward` bigint(20) NOT NULL,
  `Created` datetime(6) NOT NULL DEFAULT current_timestamp(6),
  `Updated` datetime(6) NOT NULL,
  `Sort` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `Id` bigint(20) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `password_hash` varchar(255) NOT NULL,
  `name` varchar(255) NOT NULL,
  `phone` varchar(20) NOT NULL,
  `auth_key` text DEFAULT NULL,
  `FacebookId` varchar(255) DEFAULT NULL,
  `GoogleId` varchar(255) DEFAULT NULL,
  `created_datetime` datetime(6) NOT NULL DEFAULT current_timestamp(6),
  `updated_datetime` datetime(6) DEFAULT NULL,
  `Sort` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`Id`, `Email`, `password_hash`, `name`, `phone`, `auth_key`, `FacebookId`, `GoogleId`, `created_datetime`, `updated_datetime`, `Sort`) VALUES
(1, 'baonguyen@gmail.com', '$2a$11$Iy/0BfHNfKVrXrURdYQ3qenbfEIjuvhlEYF7IiaoLKVb/3A5.E0Ba', 'Bao Nguyen', '0985451254', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2NvdW50Ijoie1xyXG4gIFwiRW1haWxcIjogXCJiYW9uZ3V5ZW5AZ21haWwuY29tXCIsXHJcbiAgXCJJZFwiOiAxLFxyXG4gIFwiRnVsbE5hbWVcIjogXCJCYW8gTmd1eWVuXCJcclxufSIsIm5iZiI6MTY3MzA4MTY5OSwiZXhwIjoxNjczMTY4MDk5LCJpYXQiOjE2NzMwODE2OTl9.SLMPeek1cNBLyWarVD06x-IZYn4-64olThbQtAbB1Ds', NULL, NULL, '2022-11-11 05:27:26.691831', '2022-11-11 05:27:26.691831', 0);

-- --------------------------------------------------------

--
-- Table structure for table `wards`
--

CREATE TABLE `wards` (
  `Id` bigint(20) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `DistrictId` bigint(20) NOT NULL,
  `Created` datetime(6) NOT NULL DEFAULT current_timestamp(6),
  `Updated` datetime(6) NOT NULL,
  `Sort` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `account_project`
--
ALTER TABLE `account_project`
  ADD PRIMARY KEY (`AccountId`,`ProjectId`),
  ADD KEY `IX_account_project_ProjectId` (`ProjectId`);

--
-- Indexes for table `account_real_news`
--
ALTER TABLE `account_real_news`
  ADD PRIMARY KEY (`AccountId`,`RealEstateId`),
  ADD KEY `IX_account_real_news_RealEstateId` (`RealEstateId`);

--
-- Indexes for table `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `cities`
--
ALTER TABLE `cities`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_city_Name` (`Name`);

--
-- Indexes for table `directions`
--
ALTER TABLE `directions`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `districts`
--
ALTER TABLE `districts`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_quan_Name` (`Name`),
  ADD KEY `IX_quan_city` (`city`);

--
-- Indexes for table `images`
--
ALTER TABLE `images`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_hinhanh_batdongsan_id` (`batdongsan_id`);

--
-- Indexes for table `news`
--
ALTER TABLE `news`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `notifications`
--
ALTER TABLE `notifications`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `projects`
--
ALTER TABLE `projects`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_projects_DistrictId` (`DistrictId`);

--
-- Indexes for table `real_estates`
--
ALTER TABLE `real_estates`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_batdongsan_huong` (`huong`),
  ADD KEY `IX_batdongsan_loai` (`loai`),
  ADD KEY `IX_batdongsan_WardId` (`WardId`);

--
-- Indexes for table `streets`
--
ALTER TABLE `streets`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_streets_Name` (`Name`),
  ADD KEY `IX_streets_ward` (`ward`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_users_Email` (`Email`);

--
-- Indexes for table `wards`
--
ALTER TABLE `wards`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_wards_Name` (`Name`),
  ADD KEY `IX_wards_DistrictId` (`DistrictId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `categories`
--
ALTER TABLE `categories`
  MODIFY `Id` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `cities`
--
ALTER TABLE `cities`
  MODIFY `Id` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `directions`
--
ALTER TABLE `directions`
  MODIFY `Id` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `districts`
--
ALTER TABLE `districts`
  MODIFY `Id` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `images`
--
ALTER TABLE `images`
  MODIFY `Id` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `news`
--
ALTER TABLE `news`
  MODIFY `Id` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `notifications`
--
ALTER TABLE `notifications`
  MODIFY `Id` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `projects`
--
ALTER TABLE `projects`
  MODIFY `Id` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `real_estates`
--
ALTER TABLE `real_estates`
  MODIFY `Id` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `streets`
--
ALTER TABLE `streets`
  MODIFY `Id` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `Id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `wards`
--
ALTER TABLE `wards`
  MODIFY `Id` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `account_project`
--
ALTER TABLE `account_project`
  ADD CONSTRAINT `FK_account_project_projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `projects` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_account_project_users_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `users` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `account_real_news`
--
ALTER TABLE `account_real_news`
  ADD CONSTRAINT `FK_account_real_news_batdongsan_RealEstateId` FOREIGN KEY (`RealEstateId`) REFERENCES `real_estates` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_account_real_news_users_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `users` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `districts`
--
ALTER TABLE `districts`
  ADD CONSTRAINT `FK_quan_city_city` FOREIGN KEY (`city`) REFERENCES `cities` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `images`
--
ALTER TABLE `images`
  ADD CONSTRAINT `FK_hinhanh_batdongsan_batdongsan_id` FOREIGN KEY (`batdongsan_id`) REFERENCES `real_estates` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `projects`
--
ALTER TABLE `projects`
  ADD CONSTRAINT `FK_projects_quan_DistrictId` FOREIGN KEY (`DistrictId`) REFERENCES `districts` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `real_estates`
--
ALTER TABLE `real_estates`
  ADD CONSTRAINT `FK_batdongsan_category_loai` FOREIGN KEY (`loai`) REFERENCES `categories` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_batdongsan_huong_huong` FOREIGN KEY (`huong`) REFERENCES `directions` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_batdongsan_wards_WardId` FOREIGN KEY (`WardId`) REFERENCES `wards` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `streets`
--
ALTER TABLE `streets`
  ADD CONSTRAINT `FK_streets_wards_ward` FOREIGN KEY (`ward`) REFERENCES `wards` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `wards`
--
ALTER TABLE `wards`
  ADD CONSTRAINT `FK_wards_quan_DistrictId` FOREIGN KEY (`DistrictId`) REFERENCES `districts` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
