CREATE OR REPLACE VIEW `vw_mc_diretor` AS
    SELECT 
        `mc_dir_diretor`.`dir_nome` AS `nome`,
        COUNT(0) AS `filmes`
    FROM
        (`mc_fil_filme`
        JOIN `mc_dir_diretor` ON ((`mc_dir_diretor`.`dir_diretor_id` = `mc_fil_filme`.`fil_dir_diretor_id`)))
    GROUP BY `mc_dir_diretor`.`dir_nome`
    ORDER BY 2 DESC , 1;

