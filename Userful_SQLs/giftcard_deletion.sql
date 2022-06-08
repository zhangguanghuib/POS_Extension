IF OBJECT_ID('tempdb..#GiftCardNumbers') IS NOT NULL
    DROP TABLE #GiftCardNumbers
GO

CREATE TABLE #GiftCardNumbers(
    CardNumber VARCHAR(20)
);
GO

BEGIN TRANSACTION;   
BULK INSERT #GiftCardNumbers
FROM 'C:\a\giftcardnumbers.txt'
WITH  
(
    ROWTERMINATOR ='\n'
);

delete from #GiftCardNumbers where CardNumber is NULL;

select count(*) from #GiftCardNumbers -- expected that totally 67077 rows

select * from #GiftCardNumbers -- expected that totally 67077 rows

delete T from RetailGiftCardTransactions as T  
join dbo.RETAILGIFTCARDTABLE as T1 on T1.ENTRYID = T.CARDNUMBER
join #GiftCardNumbers as  T2 on T1.ENTRYID = T2.CardNumber
where T.DATAAREAID = 'AHL' and T1.RESERVED = 0

delete T from dbo.RETAILGIFTCARDTABLE as T
join #GiftCardNumbers as  T1 on T.ENTRYID = T1.CardNumber
where T.DATAAREAID = 'AHL' and T.RESERVED = 0

COMMIT TRANSACTION; 

IF OBJECT_ID('tempdb..#GiftCardNumbers') IS NOT NULL
    DROP TABLE #GiftCardNumbers
GO

