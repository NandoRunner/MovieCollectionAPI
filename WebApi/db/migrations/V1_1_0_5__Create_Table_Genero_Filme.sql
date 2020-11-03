CREATE TABLE IF NOT EXISTS `mc_gf_genero_filme` (
  `gf_fil_filme_id` INT NOT NULL,
  `gf_gen_genero_id` INT NOT NULL,
  PRIMARY KEY (`gf_fil_filme_id`, `gf_gen_genero_id`),
  INDEX `fk_mc_fil_filme_has_mc_gen_genero_mc_gen_genero1_idx` (`gf_gen_genero_id` ASC),
  INDEX `fk_mc_fil_filme_has_mc_gen_genero_mc_fil_filme1_idx` (`gf_fil_filme_id` ASC),
  CONSTRAINT `fk_mc_fil_filme_has_mc_gen_genero_mc_fil_filme1`
    FOREIGN KEY (`gf_fil_filme_id`)
    REFERENCES `mc_fil_filme` (`fil_filme_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_mc_fil_filme_has_mc_gen_genero_mc_gen_genero1`
    FOREIGN KEY (`gf_gen_genero_id`)
    REFERENCES `mc_gen_genero` (`gen_genero_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARSET=utf8;