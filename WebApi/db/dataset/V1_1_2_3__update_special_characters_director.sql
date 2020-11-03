LOCK TABLES `mc_dir_diretor` WRITE;

update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xC1;', 'Á') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xC2;', 'Â') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xD3;', 'Ó') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xD4;', 'Ô') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xD8;', 'Ø') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xE0;', 'à') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xE1;', 'á') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xE3;', 'ã') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xE5;', 'å') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xE6;', 'æ') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xE7;', 'ç') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xE8;', 'è') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xE9;', 'É') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xe9;', 'é') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xEB;', 'ë') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xED;', 'í') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xEE;', 'î') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xEF;', 'ï') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#x27;', ' ') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xF1;', 'n') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xF3;', 'ó') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xF4;', 'ô') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xF6;', 'ö') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xF8;', 'ø') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xFA;', 'ú') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xFB;', 'û') where `dir_nome` like '%&#%';
update `mc_dir_diretor` set `dir_nome` = replace(`dir_nome`, '&#xFC;', 'ü') where `dir_nome` like '%&#%';

-- SELECT * FROM `mc_dir_diretor` where `dir_nome` like '%&#%' order by 2;

UNLOCK TABLES;

