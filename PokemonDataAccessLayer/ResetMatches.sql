delete from match;
go

DBCC CHECKIDENT ('match', RESEED, 0);