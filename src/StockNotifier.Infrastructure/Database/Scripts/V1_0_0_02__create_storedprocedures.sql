CREATE OR ALTER PROCEDURE UpsertStock 
   @Name NVARCHAR(255),  
   @Value INT,
   @CreatedAt DATETIME2,
   @ModifiedAt DATETIME2
AS  
BEGIN  
   SET NOCOUNT ON;  

   IF EXISTS (SELECT 1 FROM Stocks WHERE Name = @Name)  
   BEGIN  
       UPDATE Stocks  
       SET Name = @Name,  
           Value = @Value,
           ModifiedAt = @ModifiedAt
       WHERE Name = @Name;  
   END  
   ELSE  
   BEGIN  
       INSERT INTO Stocks (Name, Value, CreatedAt, ModifiedAt)  
       VALUES (@Name, @Value, @CreatedAt, @ModifiedAt);  
   END  
END;
