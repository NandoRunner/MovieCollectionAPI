CREATE VIEW `vw_mc_filme_por_ator` AS

SELECT `mc_ato_ator`.`ato_ator_id` AS `id`
	,`mc_ato_ator`.`ato_nome` AS `nome`
	,`mc_fil_filme`.`fil_filme_id` AS `filme_id`
	,`mc_fil_filme`.`fil_titulo` AS `titulo`
	,`mc_fil_filme`.`fil_ano` AS `ano`
	,`mc_fil_filme`.`fil_rating` AS `rating`
FROM (
	(
		`mc_ato_ator` JOIN `mc_af_ator_filme` ON ((`mc_ato_ator`.`ato_ator_id` = `mc_af_ator_filme`.`af_ato_ator_id`))
		) JOIN `mc_fil_filme` ON ((`mc_fil_filme`.`fil_filme_id` = `mc_af_ator_filme`.`af_fil_filme_id`))
	)
ORDER BY 2
	,4;