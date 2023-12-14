alter table Products  add CreatedDate DATETIME null,  UpdatedDate DATETIME null , DeletedDate DATETIME null ;

select * from Products;


alter table Categories  add CreatedDate DATETIME null,  UpdatedDate DATETIME null , DeletedDate DATETIME null ;

select * from Categories;

update products set CreatedDate = '01.01.2003' 
update categories set CreatedDate = '01.01.2003'