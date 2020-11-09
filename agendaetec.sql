C:\Users\Leandro\Desktop\Agenda Etec\Instalador\
-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: 09-Jun-2020 às 19:55
-- Versão do servidor: 10.1.36-MariaDB
-- versão do PHP: 7.2.11

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `agendaetec`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `agenda`
--

CREATE TABLE `agenda` (
  `codEv` int(5) NOT NULL,
  `dtCad` date NOT NULL,
  `hrCad` varchar(5) NOT NULL,
  `dtEv` date NOT NULL,
  `hrEv` varchar(5) NOT NULL,
  `localEv` varchar(50) NOT NULL,
  `evento` varchar(500) NOT NULL,
  `respCadEv` int(5) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `agenda`
--

INSERT INTO `agenda` (`codEv`, `dtCad`, `hrCad`, `dtEv`, `hrEv`, `localEv`, `evento`, `respCadEv`) VALUES
(2, '2020-06-05', '17:21', '2020-06-05', '20:00', 'teste', 'teste', 1),
(3, '2020-05-26', '17:12', '2020-05-27', '20:00', 'teste', 'teste', 3),
(4, '2020-05-26', '18:35', '2020-06-02', '18:00', 'Laboratório 08', 'Leandro, era 01/06 agora 02/06\r\nLab 12\r\n19:00', 1),
(7, '2020-06-05', '20:00', '2020-05-29', '20:00', 'Sala 30', 'Estou fazendo um teste para ver a distância do DatagridView, uma coluna pode ser que não apareça inteira e isso pode dificultar a visualização dos eventos.', 1),
(8, '2020-05-29', '17:52', '2020-05-29', '15:00', 'Salão Nobre', 'Teste Leandro', 1),
(9, '2020-05-30', '17:01', '2020-06-01', '18:00', 'Em Casa', 'Testando', 1),
(10, '2020-05-30', '18:04', '2020-06-08', '15:00', 'Laboratório 12', 'Conselho Intermediário - Redes de Computadores', 1),
(11, '2020-05-30', '18:36', '2020-06-11', '14:00', 'teste', 'teste', 1),
(12, '2020-06-01', '16:38', '2020-06-01', '18:55', 'Teste', 'Teste', 1),
(13, '2020-06-01', '18:54', '2020-06-01', '18:55', 'Laboratório 08', 'Minicurso: Redes de Computadores', 1),
(14, '2020-06-01', '18:54', '2020-06-01', '18:55', 'Salão Nobre', 'Palestra', 1),
(15, '2020-06-02', '18:33', '2020-06-03', '14:00', 'Laboratório 12', 'Teste', 1),
(16, '2020-06-04', '19:09', '2020-06-04', '19:10', 'Em casa', 'Teste Final', 1),
(18, '2020-06-05', '13:32', '2020-06-05', '14:22', 'Salão Nobre', 'Palestra', 1),
(19, '2020-06-06', '18:18', '2020-06-06', '18:20', 'teste', 'teste', 1),
(20, '2020-06-08', '01:18', '2020-06-08', '01:19', 'resre', 'teste', 1),
(21, '2020-06-06', '18:11', '2020-06-11', '18:00', 'teste', 'teste', 1),
(22, '2020-06-07', '23:52', '2020-06-07', '23:53', 'teste', 'teste', 1),
(23, '2020-06-09', '14:45', '2020-06-11', '10:00', 'teste', 'teste', 1),
(24, '2020-06-09', '14:52', '2020-06-11', '11:00', 'teste', 'treste', 1);

-- --------------------------------------------------------

--
-- Estrutura da tabela `usuario`
--

CREATE TABLE `usuario` (
  `codUs` int(5) NOT NULL,
  `nome` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `login` varchar(100) NOT NULL,
  `senha` varchar(50) NOT NULL,
  `funcao` varchar(100) NOT NULL,
  `coordenacao` varchar(100) NOT NULL,
  `situacao` varchar(7) NOT NULL,
  `acesso` varchar(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `usuario`
--

INSERT INTO `usuario` (`codUs`, `nome`, `email`, `login`, `senha`, `funcao`, `coordenacao`, `situacao`, `acesso`) VALUES
(1, 'Leandro Radighieri', 'leandroradesc@gmail.com', 'leandro', '123', 'Secretaria Acadêmica', '', 'Ativo', 'Sim'),
(2, 'Leandro da Silva', 'leandroradesc@hotmail.com', 'leandro', '1234', 'Professor', '', 'Ativo', ''),
(3, 'Carina Vitoriano', 'leandroradiguieri@etec.sp.gov.br', 'carina', '123', 'Secretaria Acadêmica', '', 'Ativo', ''),
(5, 'Sérgio Roberto Octaviano', 'e027aca@cps.sp.gov.br', 'sergio', '123', 'Secretaria Acadêmica', '', 'Ativo', 'Sim'),
(6, 'Rodrigo da Silva Stecca', 'rodrigostecca@gmail.com', 'rodrigo', '123456', 'Diretoria de Serviços', '', 'Ativo', 'Sim');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `agenda`
--
ALTER TABLE `agenda`
  ADD PRIMARY KEY (`codEv`);

--
-- Indexes for table `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`codUs`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `agenda`
--
ALTER TABLE `agenda`
  MODIFY `codEv` int(5) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT for table `usuario`
--
ALTER TABLE `usuario`
  MODIFY `codUs` int(5) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
