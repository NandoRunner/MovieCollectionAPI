CREATE VIEW `vw_mc_filme_por_genero` AS
SELECT `mc_gen_genero`.`gen_genero_id` AS `id`
	,`mc_gen_genero`.`gen_nome` AS `nome`
	,`mc_fil_filme`.`fil_filme_id` AS `filme_id`
	,`mc_fil_filme`.`fil_titulo` AS `titulo`
	,`mc_fil_filme`.`fil_ano` AS `ano`
	,`mc_fil_filme`.`fil_rating` AS `rating`
FROM (
	(
		`mc_gen_genero` JOIN `mc_gf_genero_filme` ON ((`mc_gen_genero`.`gen_genero_id` = `mc_gf_genero_filme`.`gf_gen_genero_id`))
		) JOIN `mc_fil_filme` ON ((`mc_fil_filme`.`fil_filme_id` = `mc_gf_genero_filme`.`gf_fil_filme_id`))
	)
ORDER BY 2
	,4;
