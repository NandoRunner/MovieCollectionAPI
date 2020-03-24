CREATE VIEW `vw_mc_genero` AS
    SELECT 
        `mc_gen_genero`.`gen_nome` AS `nome`, COUNT(0) AS `filmes`
    FROM
        (`mc_gen_genero`
        JOIN `mc_gf_genero_filme` ON ((`mc_gf_genero_filme`.`gf_gen_genero_id` = `mc_gen_genero`.`gen_genero_id`)))
    GROUP BY `mc_gen_genero`.`gen_nome`
    ORDER BY 2 DESC;
