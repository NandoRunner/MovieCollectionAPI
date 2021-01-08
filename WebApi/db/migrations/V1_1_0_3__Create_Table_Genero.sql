CREATE TABLE IF NOT EXISTS `mc_gen_genero` (
  `gen_genero_id` int(11) NOT NULL AUTO_INCREMENT,
  `gen_nome` varchar(45) NOT NULL,
  `gen_nome_pt` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`gen_genero_id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8;
