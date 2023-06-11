create view view1 as
select dbo.Visits.id [№ відвідування],
	   dbo.Cards.name [Ім'я пацієнта],
	   dbo.Cards.surname [Прізвщие пацієнта],
	   dbo.Visits.date [Дата відвідування],
	   dbo.Visits.complaints [Скарги],
	   dbo.Visits.diagnosis [Діагнози],
	   dbo.Departments.department_name [Направлення],
	   dbo.Visits.sickleave_term_days [Тривалість лікарняного],
	   dbo.Doctors.surname [Прізвище лікаря]
from dbo.Visits inner join
	 dbo.Cards on dbo.Cards.id = dbo.Visits.card_id inner join
	 dbo.Departments on dbo.Departments.id = dbo.Visits.department_id left join
	 dbo.Doctors on dbo.Doctors.id = dbo.Visits.doctor_id