CREATE OR REPLACE VIEW `vw_mc_filme_ver` AS
select
	fil_filme_id id,
	fil_titulo titulo,
	fil_ano ano,
	fil_rating rating
from 
	mc_fil_filme 
where 
	fil_visto = ''
order by
	2;