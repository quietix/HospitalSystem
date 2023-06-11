use DB1
go
declare @id int
set @id = 1
select dbo.Doctors.id, dbo.Doctors.surname, dbo.Doctors.name,
	   dbo.Departments.id, dbo.Departments.department_name
from dbo.Doctors inner join
	 dbo.DepatrmentToDoctor on dbo.DepatrmentToDoctor.doctor_id = dbo.Doctors.id inner join
	 dbo.Departments on dbo.Departments.id = dbo.DepatrmentToDoctor.department_id
where dbo.Departments.id = @id
order by dbo.Doctors.id