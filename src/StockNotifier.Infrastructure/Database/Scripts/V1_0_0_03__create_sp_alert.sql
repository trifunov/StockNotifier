CREATE OR ALTER PROCEDURE UpsertAlert
   @Name NVARCHAR(255),  
   @Id INT,
   @CreatedAt DATETIME2,
   @ModifiedAt DATETIME2,
   @StockId INT,
   @ThresholdType INT,
   @ThresholdValue INT,
   @IsActive BIT
AS  
BEGIN  
   SET NOCOUNT ON;  

   IF EXISTS (SELECT 1 FROM Alerts WHERE Id = @Id)  
   BEGIN  
       UPDATE Alerts  
       SET Name = @Name,  
           ModifiedAt = @ModifiedAt,
           StockId = @StockId,
           ThresholdType = @ThresholdType,
           ThresholdValue = @ThresholdValue,
           IsActive = @IsActive
       WHERE Id = @Id; 
   END  
   ELSE  
   BEGIN  
       INSERT INTO Alerts (Name, CreatedAt, ModifiedAt, 
       StockId, ThresholdType, ThresholdValue, IsActive
       )  
       VALUES (@Name, @CreatedAt, @ModifiedAt,
       @StockId, @ThresholdType, @ThresholdValue, @IsActive
       );  
   END  
END;
GO

CREATE OR ALTER PROCEDURE GetAlerts
AS  
BEGIN  
   SET NOCOUNT ON;  

   SELECT Id, Name, CreatedAt, ModifiedAt, 
          StockId, ThresholdType, ThresholdValue, IsActive
          FROM Alerts
           WHERE IsActive = 1;

END;
GO

CREATE OR ALTER PROCEDURE GetStocks
AS  
BEGIN  
   SET NOCOUNT ON;  
   SELECT Id, Name, Value, CreatedAt, ModifiedAt
   FROM Stocks;
END;
GO