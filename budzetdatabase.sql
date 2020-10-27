-- phpMyAdmin SQL Dump
-- version 5.0.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Czas generowania: 27 Paź 2020, 17:17
-- Wersja serwera: 10.4.14-MariaDB
-- Wersja PHP: 7.4.11

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Baza danych: `budzetdatabase`
--
CREATE DATABASE IF NOT EXISTS `budzetdatabase` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `budzetdatabase`;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `odbiorcy`
--

CREATE TABLE `odbiorcy` (
  `Id_odbiorcy` int(11) NOT NULL,
  `nazwa` varchar(30) NOT NULL,
  `numer_rachunku` varchar(26) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `osoba`
--

CREATE TABLE `osoba` (
  `Id_osoby` int(11) NOT NULL,
  `Imie` varchar(20) NOT NULL,
  `Nazwisko` varchar(20) NOT NULL,
  `isAdmin` tinyint(1) NOT NULL,
  `login` varchar(20) NOT NULL,
  `password` varchar(150) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Zrzut danych tabeli `osoba`
--

INSERT INTO `osoba` (`Id_osoby`, `Imie`, `Nazwisko`, `isAdmin`, `login`, `password`) VALUES
(1, 'Tomasz', 'Nowak', 1, 'admin', '2646C0DA3ROM56JE4E9DEED05AA945825AB73792044F671EF51B0BEE1728DF19DD5279BCFAA51E5284538AC848EC3FD49AEF909D4C'),
(2, 'Marta', 'Bąk', 0, 'marta1234', '8BEFD0DA3ROM56J63FF71C1360BF64F03D82EE2BDFBB5E2EB6310D5BD79409334A308AAB0BFED7B12D039DF9726970A28259A023B3');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `rachunek`
--

CREATE TABLE `rachunek` (
  `Id_rachunku` int(11) NOT NULL,
  `id_nadawcy` int(11) NOT NULL,
  `id_odbiorcy` int(11) NOT NULL,
  `kwota` double NOT NULL,
  `data_wykonania` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `wydatki_osobiste`
--

CREATE TABLE `wydatki_osobiste` (
  `Id_wydatku` int(11) NOT NULL,
  `id_osoby` int(11) NOT NULL,
  `nazwa` varchar(30) NOT NULL,
  `kwota` decimal(10,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `odbiorcy`
--
ALTER TABLE `odbiorcy`
  ADD PRIMARY KEY (`Id_odbiorcy`);

--
-- Indeksy dla tabeli `osoba`
--
ALTER TABLE `osoba`
  ADD PRIMARY KEY (`Id_osoby`);

--
-- Indeksy dla tabeli `rachunek`
--
ALTER TABLE `rachunek`
  ADD PRIMARY KEY (`Id_rachunku`),
  ADD KEY `id_nadawcy` (`id_nadawcy`),
  ADD KEY `id_odbiorcy` (`id_odbiorcy`);

--
-- Indeksy dla tabeli `wydatki_osobiste`
--
ALTER TABLE `wydatki_osobiste`
  ADD PRIMARY KEY (`Id_wydatku`),
  ADD KEY `id_osoby` (`id_osoby`);

--
-- AUTO_INCREMENT dla zrzuconych tabel
--

--
-- AUTO_INCREMENT dla tabeli `odbiorcy`
--
ALTER TABLE `odbiorcy`
  MODIFY `Id_odbiorcy` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `osoba`
--
ALTER TABLE `osoba`
  MODIFY `Id_osoby` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT dla tabeli `rachunek`
--
ALTER TABLE `rachunek`
  MODIFY `Id_rachunku` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `wydatki_osobiste`
--
ALTER TABLE `wydatki_osobiste`
  MODIFY `Id_wydatku` int(11) NOT NULL AUTO_INCREMENT;

--
-- Ograniczenia dla zrzutów tabel
--

--
-- Ograniczenia dla tabeli `rachunek`
--
ALTER TABLE `rachunek`
  ADD CONSTRAINT `rachunek_ibfk_1` FOREIGN KEY (`id_nadawcy`) REFERENCES `osoba` (`Id_osoby`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `rachunek_ibfk_2` FOREIGN KEY (`id_odbiorcy`) REFERENCES `odbiorcy` (`Id_odbiorcy`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Ograniczenia dla tabeli `wydatki_osobiste`
--
ALTER TABLE `wydatki_osobiste`
  ADD CONSTRAINT `wydatki_osobiste_ibfk_1` FOREIGN KEY (`id_osoby`) REFERENCES `osoba` (`Id_osoby`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
