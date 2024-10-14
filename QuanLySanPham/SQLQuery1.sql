create table Catalog (
Id int primary key,
CatalogCode nvarchar(50) null,
CatalogName nvarchar(250)
)

create table SanPham(
Id int primary key,
catalogId int 
)