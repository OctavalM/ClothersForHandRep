USE [ClothersForHandDB]
GO

/****** Object:  Trigger [dbo].[TR_Material_AfterInsert]    Script Date: 13/03/21 13:06:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[TR_Material_AfterInsert]
ON [dbo].[Material]
AFTER INSERT
AS 
INSERT INTO MaterialsChangeHistory (MaterialID, Operation, CountInStock, Date)
SELECT MaterialID, 'Материал ' + MaterialName + ' добавлен на склад', CountInStock, GETDATE()
FROM INSERTED
GO

ALTER TABLE [dbo].[Material] ENABLE TRIGGER [TR_Material_AfterInsert]
GO

