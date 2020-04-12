create procedure PromoteStudents @Studies varchar(100),@Semester int
as
begin
	begin tran
	declare @studyCount int = (select count(IdStudy) from Studies where Name=@Studies)
	if(@studyCount=0)
		begin
			rollback
			raiserror('No such studies',1,1)
			return
		end
	else
		begin
		declare @enrollmentCount int =(select count(IdEnrollment) from Studies s, Enrollment e where s.IdStudy= e.IdStudy and s.Name=@Studies and e.Semester=@Semester)
		if(@enrollmentCount=0)
			begin
				rollback
				raiserror('No such enrollment',1,1)
				return
			end
		declare @oldEnrollment int = (select IdEnrollment from Studies s, Enrollment e where s.IdStudy= e.IdStudy and s.Name=@Studies and e.Semester=@Semester),
		@newEnrollmentId int = (select IdEnrollment from Studies s, Enrollment e where s.IdStudy= e.IdStudy and s.Name=@Studies and e.Semester=@Semester+1)
		if(@newEnrollmentId is null)
			begin
				set @newEnrollmentId = (select MAX(IdEnrollment)+1 from Enrollment)
				insert into Enrollment (IdEnrollment,Semester,IdStudy,StartDate)
				values(@newEnrollmentId,@Semester+1,(select IdStudy from Studies where Name=@Studies),GETDATE())
			end
		update Student set IdEnrollment=@newEnrollmentId where IdEnrollment=@oldEnrollment
		select IdEnrollment, Semester,e.IdStudy, StartDate from Enrollment e join Studies s
		on e.IdStudy= s.IdStudy
		where IdEnrollment=@newEnrollmentId;
		commit
	end
end



select * from studies
select * from student
select * from enrollment
select count(IdEnrollment) from Studies s, Enrollment e where s.IdStudy= e.IdStudy and s.Name='Bezrobocie' and e.Semester=1
select count(IdStudy) from Studies where Name='Bezrobocie'

begin tran
exec PromoteStudents 'Informatyka',3

rollback