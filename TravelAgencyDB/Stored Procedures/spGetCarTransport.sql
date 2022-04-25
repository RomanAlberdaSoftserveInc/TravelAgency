CREATE PROCEDURE [dbo].[spGetCarTransport]
AS
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL SNAPSHOT;
	BEGIN TRANSACTION

	DECLARE @transportType varchar(3) = 'car'


	--Select car transport
	SELECT *
	FROM tblTransport t
	WHERE t.type = @transportType
	
	COMMIT TRANSACTION
