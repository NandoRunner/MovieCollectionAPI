CREATE TABLE IF NOT EXISTS `mc_af_ator_filme` (
  `af_fil_filme_id` INT NOT NULL,
  `af_ato_ator_id` INT NOT NULL,
  PRIMARY KEY (`af_fil_filme_id`, `af_ato_ator_id`),
  INDEX `fk_mc_fil_filme_has_mc_ato_ator_mc_ato_ator1_idx` (`af_ato_ator_id` ASC),
  INDEX `fk_mc_fil_filme_has_mc_ato_ator_mc_fil_filme1_idx` (`af_fil_filme_id` ASC),
  CONSTRAINT `fk_mc_fil_filme_has_mc_ato_ator_mc_fil_filme1`
    FOREIGN KEY (`af_fil_filme_id`)
    REFERENCES `mc_fil_filme` (`fil_filme_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_mc_fil_filme_has_mc_ato_ator_mc_ato_ator1`
    FOREIGN KEY (`af_ato_ator_id`)
    REFERENCES `mc_ato_ator` (`ato_ator_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARSET=utf8;
