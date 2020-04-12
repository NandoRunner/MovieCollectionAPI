CREATE TABLE IF NOT EXISTS `file_renamer_command` (
	`id` int(11) NOT NULL AUTO_INCREMENT,
	`caminho` varchar(80) NOT NULL,
	`extensao` varchar(10) NOT NULL,
	`prefixo` varchar(20) NULL,
	`substituir` varchar(40) NULL,
	`substpor` varchar(20) NULL,
	`abreviar` varchar(10) NOT NULL,
   PRIMARY KEY (`id`)
  ) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;
