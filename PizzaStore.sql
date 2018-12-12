-- initialization commands for Pizza Store App database

create database PizzaStoreDB;

go

create schema PS -- PS stands for Pizza Store

go


-- drop table PS.Store

create table PS.Store
(
	StoreID int identity not null,
	StoreName nvarchar(100) not null unique,
	Street nvarchar(100) not null,
	Street2 nvarchar(100) null,
	City nvarchar(100) not null,
	Zip int not null,
	State nvarchar(100) not null,
	constraint PS_Store_ID primary key (StoreID)
);


-- drop table PS.CustomerAddress

create table PS.CustomerAddress
(
	CustomerAddressID int identity not null,
	Street nvarchar(100) not null,
	Street2 nvarchar(100) null,
	City nvarchar(100) not null,
	Zip int not null,
	State nvarchar(100) not null,
	StoreID int not null, -- ID of store
	CustomerID int not null,
	constraint PS_CustomerAddress_ID primary key (CustomerAddressID)
);

alter table PS.CustomerAddress
	add constraint FK_Address_Store Foreign key (StoreID) references PS.Store (StoreID);


-- drop table PS.Customer

create table PS.Customer
(
	CustomerID int identity not null,
	Username nvarchar(100) not null,
	Password nvarchar(100) not null,
	FirstName nvarchar(100) not null,
	LastName nvarchar(100) not null,
	FavoriteStoreID int,

	constraint PS_Customer_ID primary key (CustomerID)
);

alter table PS.CustomerAddress
	add constraint FK_Address_Customer Foreign key (CustomerID) references PS.Customer (CustomerID);




-- drop table PS.Invantory

create table PS.Invantory -- stores the ingrediant invantory for each store
(
	StoreID int not null,
	IngrediantID int not null,
	Quantity int not null
);

alter table PS.Invantory
	add constraint FK_Store_Invantory Foreign key (StoreID) references PS.Store (StoreID);

-- drop table PS.PizzaOrder

create table PS.PizzaOrder
(
	PizzaOrderID int identity not null,
	StoreID int not null,
	CustomerID int not null,
	CustomerAddressID int not null,
	TotalDue money not null,
	DatePlaced datetime2 not null default getutcdate(),
	constraint PS_PizzaOrder_ID primary key (PizzaOrderID)
);


alter table PS.PizzaOrder
	add constraint FK_PO_CustAdd Foreign Key (CustomerAddressID) references PS.CustomerAddress (CustomerAddressID);
alter table PS.PizzaOrder
	add constraint FK_PO_Store Foreign key (StoreID) references PS.Store (StoreID);
alter table PS.PizzaOrder
	add constraint FK_PO_Customer Foreign key (CustomerID) references PS.Customer (CustomerID);

-- drop table PS.PizzasInOrder

create table PS.PizzasInOrder
(
	PizzaID int not null,
	PizzaOrderID int not null,
	Quantity int not null default 1
);


-- drop table PS.Pizza

create table PS.Pizza
(
	PizzaID int identity not null,
	Size int not null,
	Cost money not null,
	constraint PS_Pizza_ID primary key (PizzaID)
);

alter table PS.PizzasInOrder
	add constraint FK_PiO_Order Foreign key (PizzaOrderID) references PS.PizzaOrder (PizzaOrderID);
alter table PS.PizzasInOrder
	add constraint FK_PiO_Pizza Foreign key (PizzaID) references PS.Pizza (PizzaID);


-- drop table PS.IngrediantList

create table PS.IngrediantList
(
	IngrediantID int identity not null,
	IngrediantName nvarchar(100),
	constraint PS_Ingrediant_ID primary key (IngrediantID)
);

-- drop table PS.IngrediantsOnPizza

alter table PS.Invantory
	add constraint FK_Ingrediant_Invantory Foreign key (IngrediantID) references PS.IngrediantList (IngrediantID);


create table PS.IngrediantsOnPizza
(
	PizzaID int not null,
	IngrediantID int not null,
);

alter table PS.IngrediantsOnPizza
	add constraint FK_IoP_Pizza Foreign key (PizzaID) references PS.Pizza (PizzaID);
alter table PS.IngrediantsOnPizza
	add constraint FK_IoP_Ingrediants foreign key (IngrediantID) references PS.IngrediantList (IngrediantID);

alter table PS.Invantory
	add constraint PK_Invantory primary key (StoreID, IngrediantID);

alter table PS.IngrediantsOnPizza
	add constraint PK_IoP primary key (PizzaID, IngrediantID);

alter table PS.PizzasInOrder
	add constraint PK_PiO primary key (PizzaID, PizzaOrderID);


-- Add entries to database


insert into PS.Store (StoreName, Street, Street2, City, Zip, State) values
	('Closed','Order came from closed location',null,'Not a City',00000,'Not a State'),
	('Austin Main', '1234 Main Street',null,'Austin',73301,'Texas'),
	('SD Main', '4321 Yerba Verde Bulivard',null,'San Diego',91932,'California'),
	('State Street Location','333 State Street',null,'Madison',53701,'Wisconsin'),
	('SLC Store','2733 East 2100 South','Suite 2B','Salt Lake City',84044,'Utah'),
	('Headquarters','5421 Redridge Road',null,'Seattle',98101,'Washington'),
	('Augusta Branch','999 Main Street',null,'Augusta',30805,'Georgia')
	;

insert into PS.Customer(FirstName, LastName, Username, Password) values
	('Lara', 'Croft', 'TombRaider69','SodIndianaJones'),
	('Adam', 'Jensen', 'DeusExMyFist','Pizza From The Machine'),
	('Booker', 'DeWitt', 'ReallyHatesChicken','Ill have plasmids on mine'),
	('John', 'Marston', 'CowboyBebop1911','10GallonHat'),
	('Cher', 'NLN', 'Cher','I Believe in Love after Pizza'),
	('Bill', 'Gates', 'admin','password')
	;

-- update ps.Customer set	FirstName ='Lara' where FirstName = 'Laura';

insert into PS.IngrediantList(IngrediantName) values
	('Sausage'), ('Peperoni'), ('Black Olives'), ('Green Olives'), ('Bell Peppers'), ('Jalapenos'), ('Chicken'), ('Hot Sauce'), ('Mushrooms'),
            ('Pineapple'), ('Onions'), ('Tomatoes'), ('Extra Cheese')
	;

insert into ps.Invantory(StoreID,IngrediantID,Quantity)
	select s.StoreID,i.IngrediantID,30 from PS.Store as s cross join PS.IngrediantList as i;

select * from PS.Customer;
-- haven't run this yet.
insert into PS.CustomerAddress(Street, Street2, City, Zip, State, StoreID, CustomerID) values
	('5421 Redridge Road',null,'Seattle',98101,'Washington'),
;

