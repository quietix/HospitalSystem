select dbo.Doctors.cabinet as [Кабінет], 
	   dbo.Doctors.days_of_reception as [Дні прийому],
	   dbo.Doctors.time_start as [Час початку прийому],
	   dbo.Doctors.time_end as [Час закінчення прийому]
from dbo.Doctors