create view view1 as
select dbo.Visits.id [� ����������],
	   dbo.Cards.name [��'� ��������],
	   dbo.Cards.surname [������� ��������],
	   dbo.Visits.date [���� ����������],
	   dbo.Visits.complaints [������],
	   dbo.Visits.diagnosis [ĳ������],
	   dbo.Departments.department_name [�����������],
	   dbo.Visits.sickleave_term_days [��������� ����������],
	   dbo.Doctors.surname [������� �����]
from dbo.Visits inner join
	 dbo.Cards on dbo.Cards.id = dbo.Visits.card_id inner join
	 dbo.Departments on dbo.Departments.id = dbo.Visits.department_id left join
	 dbo.Doctors on dbo.Doctors.id = dbo.Visits.doctor_id