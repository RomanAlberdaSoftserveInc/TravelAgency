CREATE PROCEDURE [dbo].[spAddCheck]
	@price decimal(15,2),
	@personCount int,
	@tourId int,
	@userId int,
	@producedBy int,
	@createdAt DATETIME
AS
BEGIN
	declare @errMsg nvarchar(1024);
    BEGIN TRY
		BEGIN TRANSACTION
		declare @outputId table (outputId int)
		INSERT INTO tblCheck
			(price, personCount, tourId, userId, producedBy, createdAt)
		OUTPUT inserted.id into @outputId
		VALUES 
			(@price, @personCount, @tourId, @userId, @producedBy,@createdAt)

		declare @checkId int
		
		SELECT @checkId = OI.outputId FROM @outputId as OI
		COMMIT TRANSACTION;
		SELECT @checkId
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
    