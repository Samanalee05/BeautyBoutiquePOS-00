create database test;
use test;
show tables;

drop table categories;

create table categories( 
id int auto_increment primary key,
name varchar(2555),
description varchar(2555)
);

desc categories;

INSERT INTO categories (name, description) VALUES ("CategoryName","CategoryDescription");

select * from categories;

create table products( 
id int auto_increment primary key,
name varchar(255),
description varchar(255),
qty double default 0,
discount double default 0,
price float,
category varchar(255)
);

drop table products;
desc products;

INSERT INTO products (name, description, qty, discount, price) VALUES
('Product 1', 'Description of Product 1', 100, 0.05, 10.99),
('Product 2', 'Description of Product 2', 150, 0.1, 20.49),
('Product 3', 'Description of Product 3', 80, 0.0, 15.99),
('Product 4', 'Description of Product 4', 200, 0.2, 8.75),
('Product 5', 'Description of Product 5', 120, 0.15, 25.99);


select * from products;

create table customers( 
nic varchar(255) primary key,
name varchar(255),
age double,
address varchar(255),
contact varchar(255),
email varchar(255),
Career varchar(255)
);

drop table customers;



select * from customers;

desc customers;

create table checkout( 
id int auto_increment primary key,
customer varchar(255),
total double,
discount double
);

drop table checkout;

select * from checkout;

create table productsLine( 
id int,
name varchar(255),
description varchar(255),
qty double,
discount double,
price float,
total float
);

select * from productsLine;

drop table productsLine;

create table checkoutLine( 
id int auto_increment primary key,
date varchar(255),
customer varchar(255),
total double,
discount double,
itemQTY double
);

drop table checkoutLine;

drop table productsLine;

CREATE TABLE newOrder (
    id INT AUTO_INCREMENT PRIMARY KEY,
    vendor VARCHAR(255),
    total DOUBLE,
    date DATETIME
);

drop table newOrder;


CREATE TABLE gr (
    id INT AUTO_INCREMENT PRIMARY KEY,
    product_name VARCHAR(255),
    quantity INT,
    price DECIMAL(10, 2),
    total DECIMAL(10, 2),
    vendor VARCHAR(255),
    received_date DATE
);



