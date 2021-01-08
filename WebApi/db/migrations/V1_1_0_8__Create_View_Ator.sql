CREATE OR REPLACE VIEW  `vw_mc_ator` AS
    SELECT 
        `mc_ato_ator`.`ato_nome` AS `nome`, COUNT(0) AS `filmes`
    FROM
        (`mc_ato_ator`
        JOIN `mc_af_ator_filme` ON ((`mc_af_ator_filme`.`af_ato_ator_id` = `mc_ato_ator`.`ato_ator_id`)))
    GROUP BY `mc_ato_ator`.`ato_nome`
    ORDER BY 2 DESC , 1;
