CREATE FUNCTION [dbo].[fnGetChecks]
(
	@checkId int
)
RETURNS TABLE
AS
RETURN
	SELECT c.id, 
		c.personCount, 
		c.price,
		c.createdAt,
		m.id as managerId,
		m.name as managerName,
		m.surname as managerSurname,
		m.email, 
		m.phoneNumber as managerPhoneNumber,
		u.id as userId,
		u.name as userName,
		u.surname as userSurname, 
		u.passport as passport, 
		u.phoneNumber as userPhoneNumber, 
		t.id as tourId, 
		t.city, 
		t.country, 
		t.includedInsurance  
	FROM tblCheck c
	INNER JOIN tblManager m
	ON m.id = c.producedBy
	INNER JOIN tblUser u
	ON u.id = c.userId
	INNER JOIN tblTour t
	ON t.id = c.tourId
	WHERE c.id = @checkId
