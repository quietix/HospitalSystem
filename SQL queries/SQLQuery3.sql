select dbo.Doctors.id as [� �����],
	   dbo.Doctors.name as [��'� �����],
	   dbo.Doctors.surname as [������� �����],
	   dbo.Doctors.secname as [�� ������� �����]
from dbo.Doctors inner join
	 dbo.DepatrmentToDoctor on dbo.DepatrmentToDoctor.doctor_id = dbo.Doctors.id inner join
	 dbo.Departments on dbo.Departments.id = dbo.DepatrmentToDoctor.department_id
where dbo.Departments.department_name = '����������� ��������'