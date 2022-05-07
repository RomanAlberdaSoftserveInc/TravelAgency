CREATE PROCEDURE [dbo].[spUpdateTour]
	@tourId int,
	@country varchar(255),
	@city varchar(255),
	@includedInsurance varchar(255),
	@hotelId int,
	@tourTypeId varchar(255),
	@createdAt DATETIME,
	@updatedAt DATETIME,
	@tourTransportIds [dbo].[TourTransportationType] READONLY
AS
BEGIN
	declare @errMsg nvarchar(1024);
    BEGIN TRY
		BEGIN TRANSACTION
		UPDATE  tblTour
		SET country = @country,
			city = @city,
			includedInsurance = @includedInsurance,
			hotelId = @hotelId,
			tourTypeId = @tourTypeId,
			createdAt = @createdAt,
			updatedAt = @updatedAt
			WHERE id = @tourId
		DELETE FROM tblTourTransportation WHERE tourId = @tourId
		IF EXISTS (SELECT * FROM @tourTransportIds)
		BEGIN
			INSERT INTO tblTourTransportation 
				(tourId, transportation_id)
			SELECT @tourId, transportationId
				FROM @tourTransportIds
		END
		COMMIT TRANSACTION;
		SELECT @tourId
	END TRY
    BEGIN CATCH
      ROLLBACK TRANSACTION;
      SET @errMsg = 'spAddTour: '+ ERROR_MESSAGE();
	
	  IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK TRANSACTION
		END

      RAISERROR(@errMsg, 16, 1);
	  RETURN -1;
    END CATCH  
END
    