select dbo.Doctors.id as [№ лікаря],
	   dbo.Doctors.name as [Ім'я лікаря],
	   dbo.Doctors.surname as [Прізвище лікаря],
	   dbo.Doctors.secname as [По батькові лікаря]
from dbo.Doctors inner join
	 dbo.DepatrmentToDoctor on dbo.DepatrmentToDoctor.doctor_id = dbo.Doctors.id inner join
	 dbo.Departments on dbo.Departments.id = dbo.DepatrmentToDoctor.department_id
where dbo.Departments.department_name = 'Кардіологічне відділення'