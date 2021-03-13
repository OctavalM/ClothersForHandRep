USE [ClothersForHandDB]
GO

/****** Object:  Trigger [dbo].[TR_Material_AfterUpdate]    Script Date: 13/03/21 13:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[TR_Material_AfterUpdate]
ON [dbo].[Material]
AFTER UPDATE
AS 
DECLARE @materialID INT
DECLARE @count INT
DECLARE @updateCount INT
SELECT @materialID = i.MaterialID, @count = m.CountInStock, @updateCount = i.CountInStock
FROM INSERTED i, MaterialsChangeHistory m
WHERE m.MaterialID = i.MaterialID

IF @count < @updateCount
BEGIN
  INSERT INTO MaterialsChangeHistory (MaterialID, Operation, CountInStock, Date)
  SELECT MaterialID, 'Количество материал ' + MaterialName + ' увеличилось на склад', CountInStock, GETDATE()
  FROM INSERTED
END
ELSE IF @count > @updateCount
BEGIN
  INSERT INTO MaterialsChangeHistory (MaterialID, Operation, CountInStock, Date)
  SELECT MaterialID, 'Количество материал ' + MaterialName + ' уменьшилось на склад', CountInStock, GETDATE()
  FROM INSERTED
END
GO

ALTER TABLE [dbo].[Material] ENABLE TRIGGER [TR_Material_AfterUpdate]
GO

