select * from [ax].RETAILSTORETABLE as T where T.STORENUMBER = 'HOUSTON'

select * from  [ext].[CONTOSORETAILSTOREHOURSTABLE]

delete from [ext].[CONTOSORETAILSTOREHOURSTABLE]
insert into
[ext].[CONTOSORETAILSTOREHOURSTABLE]
values
(1, 1, 28800, 61200, 5637144592),
(2, 2, 28800, 61200, 5637144592),
(3, 3, 28800, 61200, 5637144592),
(4, 4, 28800, 61200, 5637144592),
(5, 5, 28800, 61200, 5637144592),
(6, 6, 28800, 61200, 5637144592),
(7, 7, 28800, 61200, 5637144592)