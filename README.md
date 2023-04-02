# My eShopOnWeb [WIP]

This is a fork of the eShopOnWeb ASP.NET Core Reference Application, you can read the original README [here](README-MS.md)

## Stock management

```sql
-- Update/Fix Catalog Stock Reserved Quantity
MERGE dbo.CatalogStock as tgt
USING (
	SELECT ItemOrdered_CatalogItemId AS CatalogItemId, sum(Units)
	from dbo.OrderItems 
	group by ItemOrdered_CatalogItemId
) as src (CatalogItemId, Units)
ON tgt.Id = src.CatalogItemId
WHEN MATCHED THEN  
	UPDATE SET ReservedQuantity = src.Units
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Id, StockQuantity, ReservedQuantity) VALUES (src.CatalogItemId, 0, src.Units);
  
select *
from [dbo].[CatalogStock]

-- Check Stock
select *
from (
	SELECT ItemOrdered_CatalogItemId AS CatalogItemId, sum(Units) as OrderedQuantity
	from dbo.OrderItems 
	group by ItemOrdered_CatalogItemId
) as qOrdered
FULL OUTER JOIN dbo.CatalogStock as qStock ON qStock.Id = qOrdered.CatalogItemId
where qOrdered.CatalogItemId is NULL or qStock.Id is NULL or qOrdered.OrderedQuantity <> qStock.ReservedQuantity
```