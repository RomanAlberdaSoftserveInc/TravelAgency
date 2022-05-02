CREATE PROCEDURE [dbo].[spAddTour]
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
		declare @outputId table (outputId int)
		INSERT INTO tblTour
			(country, 
			city,
			includedInsurance, 
			hotelId, 
			tourTypeId,
			createdAt,
			updatedAt)
		OUTPUT inserted.id into @outputId
		VALUES 
			(@country, @city, @includedInsurance, @hotelId, @tourTypeId, @createdAt, @updatedAt)

		declare @tourId int
		
		SELECT @tourId = OI.outputId FROM @outputId as OI
		IF NOT EXISTS (SELECT * FROM @tourTransportIds)
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
    