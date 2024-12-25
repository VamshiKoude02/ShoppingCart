use shoppingCart

CREATE TABLE UserRegistration (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50),
    Username NVARCHAR(50) UNIQUE,
    Password NVARCHAR(50),
    MobileNumber NVARCHAR(10)
);

CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(100),
    Price DECIMAL(10,2),
    Quantity INT
);


CREATE TABLE Cart (
    CartID INT IDENTITY(1,1) PRIMARY KEY,
	ProductID INT foreign key references Products(ProductID),
    Username NVARCHAR(50),
    TotalCost DECIMAL(10,2)
);

CREATE TABLE CartItems (
    CartItemID INT IDENTITY(1,1) PRIMARY KEY,
    CartID INT FOREIGN KEY REFERENCES Cart(CartID),
    ProductID INT FOREIGN KEY REFERENCES Products(ProductID),
    Quantity INT,
    FinalPrice DECIMAL(10,2),
);


CREATE TABLE Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    CartID INT FOREIGN KEY REFERENCES Cart(CartID),
    Username NVARCHAR(50),
    TotalCost DECIMAL(10,2),
    OrderDate DATETIME DEFAULT GETDATE()
);
