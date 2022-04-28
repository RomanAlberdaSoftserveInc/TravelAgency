CREATE PROCEDURE [dbo].[spAddHotel]
	@name varchar(255),
	@stars int,
	@description varchar(255),
	@pricePerDay decimal(15,2),
	@country varchar(255),
	@city varchar(255),
	@address varchar(255),
	@eatingTypeId int
AS
BEGIN
	declare @errMsg nvarchar(1024);
    BEGIN TRY
		BEGIN TRANSACTION
		INSERT INTO tblHotel
			(name, 
			stars,
			description, 
			pricePerDay, 
			country, 
			city,
			address,
			eatingTypeId)
		VALUES 
			(@name, @stars, @description, @pricePerDay, @country, @city, @address, @eatingTypeId)
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
    