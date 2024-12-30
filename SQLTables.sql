
create database shoppingCart

use shoppingCart


use shoppingCart

---------------------------------------------------------------------

CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50),
    Username NVARCHAR(50) UNIQUE,
    Password NVARCHAR(50),
    MobileNumber NVARCHAR(10)
);

---------------------------------------------------------------------

CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(100),
    Price DECIMAL(10,2),
    Quantity INT
);

---------------------------------------------------------------------

insert into Products values('Oppo',23599.00,5)
insert into Products values('Oneplus',55599.00,5)
insert into Products values('IPhone',99999.00,5)
insert into Products values('RedMi',13000.00,5)
insert into Products values('Motorola',23799.00,5)

---------------------------------------------------------------------

CREATE TABLE  Cart (
    CartID INT IDENTITY(1,1) PRIMARY KEY,
	ProductID INT foreign key references Products(ProductID),
    Username NVARCHAR(50),
	Quantity INT,
    TotalCost DECIMAL(10,2)
);

---------------------------------------------------------------------

create table OrderHistory (
UserName varchar(50), 
ProductID INT foreign key references Products(ProductID),
Quantity int,
Cost decimal(10,2))

---------------------------------------------------------------------

CREATE TABLE  Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    CartID INT FOREIGN KEY REFERENCES Cart(CartID),
    Username NVARCHAR(50),
    TotalCost DECIMAL(10,2),
    OrderDate DATETIME DEFAULT GETDATE()
);

---------------------------------------------------------------------
create procedure dbo.UserLogin
    @username nvarchar(50),
    @password nvarchar(50)
as
begin
    if exists (select 1 from users where username = @username and password = @password)
    begin
        select 1 as loginresult;
    end
    else
    begin
        select 0 as loginresult;
    end
end;

---------------------------------------------------------------------

alter procedure [dbo].[UserRegister]
    @name nvarchar(100),
    @username nvarchar(50),
    @password nvarchar(50),
    @mobilenumber nvarchar(15)
as
begin
    if exists (select count(username) from users where username = @username)
    begin
        select 0 as registrationstatus;
        return;
    end

    insert into users (name, username, password, mobilenumber)
    values (@name, @username, @password, @mobilenumber);

    select 1 as registrationstatus;
end;

------------------------------------------------------------------------

create procedure [dbo].[ProductDetails]
as
begin
    select ProductID, ProductName, Price, Quantity
    from products
end;

---------------------------------------------------------------------


alter  procedure [dbo].[AddItemsToCart]
    @productid int,
    @quantity int,
	@username varchar(50)
as
begin
    declare @price int, @avblquantity int;

    select  @price = Price, @avblquantity = Quantity 
    from products 
    where productid = @productid;

    if @price is null
    begin
        select 'please enter a valid productid' as result;
        return;
    end

    if @avblquantity <= 0
    begin
        select 'This product is out of stock' as result;
        return;
    end

    if @avblquantity < @quantity
    begin
        select 'insufficient stock.' as result;
        return;
    end

    declare @finalprice int = @price * @quantity;

    insert into cart (productid, username, quantity, TotalCost)
    values (@productid, @username, @quantity, @finalprice);

    update products
    set quantity = quantity - @quantity
    where productid = @productid;

    select 'successfully added product to your cart..!' as result;
end;


---------------------------------------------------------------------

	create procedure IsCartOk
	@cartok varchar(50),
	@username varchar(50)

	as
	begin
	if(@cartok = 'yes')
		begin
			select username ,sum(Quantity) as TotalQuantity, sum(totalcost) as TotalCost from cart where username = @username group by Username 
		end
	end

---------------------------------------------------------------------
	create procedure CartFinalList
	as
	begin
		select username ,ProductID, sum(Quantity) as Quantity, sum(totalcost) as Cost from cart group by username, ProductID
	end

---------------------------------------------------------------------

ALTER Procedure StoreOrdersandEmptyCart
as
begin

insert into OrderHistory select username, productid,Quantity,TotalCost  from cart

delete from cart
select 1
end

exec StoreOrdersandEmptyCart


create procedure mobilenumber 
@username varchar(50)
as
begin
select mobilenumber from users where username like @username;
end

exec mobilenumber 'vamshi'

---------------------------------------------------------------------

ALTER PROCEDURE IsCartNotOK
    @cartId INT,
    @quantity INT
AS
BEGIN
    DECLARE @productId INT, @qnty INT;

    IF NOT EXISTS (SELECT 1 FROM cart WHERE CartID = @cartId)
    BEGIN
        SELECT 'Enter a valid CartID' AS result;
        RETURN;
    END

    SELECT @qnty = Quantity, @productId = productid
    FROM cart
    WHERE CartID = @cartId;

    IF @quantity < @qnty
    BEGIN
        UPDATE cart SET Quantity = Quantity - @quantity WHERE CartID = @cartId;

        UPDATE Products SET Quantity = Quantity + @quantity WHERE ProductID = @productId;

        SELECT 'Item(s) Removed Successfully' AS result;
    END

    ELSE IF @quantity = @qnty
    BEGIN

        DELETE FROM cart WHERE CartID = @cartId;

        UPDATE Products SET Quantity = Quantity + @quantity WHERE ProductID = @productId;

        SELECT 'Item(s) Removed Successfully' AS result;
    END

    ELSE
    BEGIN
        SELECT 'Quantity selected is more than the Quantity in Cart' AS result;
    END
END;


exec IsCartNotOK 17,1