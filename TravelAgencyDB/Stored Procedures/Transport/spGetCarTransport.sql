CREATE PROCEDURE [dbo].[spGetCarTransport]
AS
	BEGIN TRANSACTION

	DECLARE @transportType varchar(3) = 'car'


	--Select car transport
	SELECT *
	FROM tblTransport t
	WHERE t.type = @transportType
	
	COMMIT TRANSACTION
