LOCK TABLES `mc_ato_ator` WRITE;

update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xC1;', 'Á') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xC2;', 'Â') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xD3;', 'Ó') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xD4;', 'Ô') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xE0;', 'à') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xE1;', 'á') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xE3;', 'ã') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xE5;', 'å') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xE6;', 'æ') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xE7;', 'ç') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xE8;', 'è') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xE9;', 'É') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xe9;', 'é') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xEB;', 'ë') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xED;', 'í') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xEE;', 'î') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xEF;', 'ï') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#x27;', ' ') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xF1;', 'n') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xF3;', 'ó') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xF4;', 'ô') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xF6;', 'ö') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xF8;', 'ø') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xFA;', 'ú') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xFB;', 'û') where `ato_nome` like '%&#%';
update `mc_ato_ator` set `ato_nome` = replace(`ato_nome`, '&#xFC;', 'ü') where `ato_nome` like '%&#%';

UNLOCK TABLES;


-- SELECT * FROM mc_ato_ator where `ato_nome` like '%&#%' order by 2;
LOCK TABLES `mc_fil_filme` WRITE;

update `mc_fil_filme` set `fil_titulo_original` = replace(`fil_titulo_original`, '&#x27;', ' ') where `fil_titulo_original` like '%&#%';

-- SELECT * FROM mc_fil_filme where fil_titulo_original like '%&#%' order by 2;

-- SELECT * FROM mc_fil_filme where fil_titulo like '%&#%' order by 2;

UNLOCK TABLES;
