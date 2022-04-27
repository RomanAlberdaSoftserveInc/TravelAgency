CREATE PROCEDURE [dbo].[spDeleteTransportation]
	@transportationdId int
AS
BEGIN
	declare @errMsg nvarchar(1024);
    BEGIN TRANSACTION
    BEGIN TRY
		IF EXISTS (SELECT 1 FROM  tblTourTransportation WHERE id = @transportationdId)
		BEGIN
			DELETE FROM tblTourTransportation WHERE id = @transportationdId
			DELETE FROM tblTransportation WHERE id = @transportationdId 
			SELECT * FROM tblTransportation WHERE id = @transportationdId 
			COMMIT TRANSACTION;
			PRINT('LOL');
			SELECT 1;
			RETURN 1;
		END
		ELSE
		BEGIN
			COMMIT TRANSACTION;
			PRINT('BABY');
			SELECT 0;
			RETURN 0;
		END
    END TRY
    BEGIN CATCH
      ROLLBACK TRANSACTION;
      SET @errMsg = 'spDeleteTransportation : '+ ERROR_MESSAGE();
	
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
    