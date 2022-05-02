CREATE PROCEDURE [dbo].[spAddTransportation]
	@depatureLocation varchar(255),
	@arrivalLocation varchar(255),
	@depatureTime DATETIME,
	@arrivalTime DATETIME,
	@pricePerPerson decimal(15,2),
	@transportId int
AS
BEGIN
	declare @errMsg nvarchar(1024);
    BEGIN TRY
		BEGIN TRANSACTION
		INSERT INTO tblTransportation
			(depatureLocation, 
			arrivalLocation,
			depatureTime, 
			arrivalTime, 
			pricePerPerson, 
			transportId)
		VALUES 
			(@depatureLocation, @arrivalLocation, @depatureTime, @arrivalTime, @pricePerPerson, @transportId)
		COMMIT TRANSACTION;
	END TRY
    BEGIN CATCH
      ROLLBACK TRANSACTION;
      SET @errMsg = 'spAddTransportation: '+ ERROR_MESSAGE();
	
	  IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK TRANSACTION
		END

      RAISERROR(@errMsg, 16, 1);
	  PRINT('MAN');
	  SELECT 0;
	  RETURN 0;
    END CATCH  
END
    