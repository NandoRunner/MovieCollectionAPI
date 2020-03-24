CREATE VIEW `vw_mc_filme_por_diretor` AS

SELECT `mc_dir_diretor`.`dir_diretor_id` AS `id`
	,`mc_dir_diretor`.`dir_nome` AS `nome`
	,`mc_fil_filme`.`fil_filme_id` AS `filme_id`
	,`mc_fil_filme`.`fil_titulo` AS `titulo`
	,`mc_fil_filme`.`fil_ano` AS `ano`
	,`mc_fil_filme`.`fil_rating` AS `rating`
FROM 
		`mc_dir_diretor` 
        JOIN `mc_fil_filme` ON mc_fil_filme.fil_dir_diretor_id = mc_dir_diretor.dir_diretor_id
ORDER BY 2
	,4;