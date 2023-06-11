select dbo.Visits.complaints as [Скарги],
	   dbo.Departments.department_name as [Направлення]
from dbo.Doctors inner join
	 dbo.Visits on dbo.Visits.doctor_id = dbo.Doctors.id inner join
	 dbo.Departments on dbo.Departments.id = dbo.Visits.department_id
where dbo.Visits.complaints = 'головний біль'