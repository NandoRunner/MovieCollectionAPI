CREATE OR REPLACE VIEW `vw_mc_filme_visto` AS
select
	fil_periodo periodo,
	DATE_FORMAT(fil_periodo, '%Y-%m') periodo_exibe,
	fil_filme_id id,
	fil_titulo titulo,
	fil_ano ano,
	fil_rating rating
from 
	mc_fil_filme 
where 
	fil_visto <> '' and fil_periodo is not null
order by
	2 desc;