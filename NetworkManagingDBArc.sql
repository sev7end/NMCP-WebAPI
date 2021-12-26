--CREATE Database NetworkMarketingCP
USE NetworkMarketingCP
CREATE TABLE DistributorAuth(
	Id int NOT NULL,
	Email varchar(255) NOT NULL,
	UserName varchar(55) NOT NULL,
	SHA512Pwd varchar(255) NOT NULL,
	TempPwd varchar(255) NOT NULL
);

CREATE TABLE DistributorPersonalData(
	Id int NOT NULL,
	DocumentType tinyint NOT NULL,
	DocumentNumber varchar(10),
	DocumentSerial varchar(10),
	IssueDate varchar(55) NOT NULL,
	ExpirtyDate varchar(15) NOT NULL,
	PrivateNumber varchar(50) NOT NULL,
	IssuingAgency varchar(100)
);

CREATE TABLE DistributorData (
    Id int NOT NULL IDENTITY(1,1),
    LastName varchar(50) NOT NULL,
    FirstName varchar(50) NOT NULL,
    DateOfBirth varchar(15) NOT NULL,
	Gender int NOT NULL,
	PictureUrl varchar(2083) NOT NULL,
	IsVerifiedUser tinyint NOT NULL,
	ContactType tinyint NOT NULL,
	ContactInfo varchar(100) NOT NULL,
	AddressType tinyint NOT NULL,
	UserAddress varchar(100) NOT NULL,
	UniqueId varchar(50) NOT NULL
    PRIMARY KEY (Id)
);

CREATE TABLE ReferalData (
	Id int NOT NULL, -- curruserId
	ReferallId int, --find in db => set their referedUsers++()
	ReferedUsers tinyint, -- 0-3
	ReferallLevel tinyint -- 0-5
)


CREATE TABLE ProductData (
	Id int NOT NULL Identity(1,1),
	ProductName varchar(255) NOT NULL,
	UnitPrice decimal(15,2) NOT NULL
);

CREATE TABLE SalesData(
	SaleId varchar(255) NOT NULL,
	DistributorId int NOT NULL,
	SaleDate varchar(15) NOT NULL,
	ProductId int NOT NULL,
	UnitsSold int NOT NULL,
	UnitsTotalPrice decimal(15,2) NOT NULL
);

CREATE TABLE TransactionData(
	TowardsId int NOT NULL,
	Amount decimal NOT NULL,
	TransactionDate varchar(15) NOT NULL
);

CREATE TABLE PaidSales(
	ForId int NOT NULL,
	SaleId varchar(255) NOT NULL
);

CREATE TABLE DistributorWallet(
	DistributorId int NOT NULL,
	WalletMoney decimal NOT NULL
);