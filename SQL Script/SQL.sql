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
discount_percentage double default 0,
price float,
category varchar(255)
);

drop table products;
desc products;

INSERT INTO products (name, description, qty, discount_percentage, price, category) VALUES
('Product A', 'Description for Product A', 10, 5.00, 50.00, 'Category 1'),
('Product B', 'Description for Product B', 20, 0.00, 30.00, 'Category 2'),
('Product C', 'Description for Product C', 15, 10.00, 40.00, 'Category 1'),
('Product D', 'Description for Product D', 8, 3.00, 70.00, 'Category 2'),
('Product E', 'Description for Product E', 12, 0.00, 25.00, 'Category 3');



select * from products;

create table customers( 
nic varchar(255) primary key,
name varchar(255),
age double,
address varchar(255),
contact varchar(255),
email varchar(255),
Career varchar(255),
date_join date
);

drop table customers;

INSERT INTO customers (nic, name, age, address, contact, email, Career, date_join) 
VALUES 
('NIC123457', 'John Doe', 30, '123 Main St', '123-456-7890', 'john.doe@example.com', 'Developer', '2024-04-18'),
('NIC123456', 'John Doe', 30, '123 Main St', '123-456-7890', 'john.doe@example.com', 'Developer', '2024-04-23'),
('NIC789012', 'Jane Smith', 25, '456 Elm St', '987-654-3210', 'jane.smith@example.com', 'Designer', '2024-04-23'),
('NIC345678', 'Michael Brown', 35, '789 Oak St', '456-789-0123', 'michael.brown@example.com', 'Manager', '2024-04-22'),
('NIC901234', 'Emily Johnson', 28, '101 Maple St', '789-012-3456', 'emily.johnson@example.com', 'Engineer', '2024-04-22'),
('NIC567890', 'Sarah Clark', 32, '202 Pine St', '012-345-6789', 'sarah.clark@example.com', 'Analyst', '2024-04-21');


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
discount_percentage double,
itemQTY double
);

INSERT INTO checkoutLine (date, customer, total, discount, itemQTY) VALUES
('2024-04-01', 'John Doe', 100.00, 0.00, 3),
('2024-04-01', 'Alice Smith', 150.00, 10.00, 2),
('2024-04-02', 'Bob Johnson', 200.00, 20.00, 4),
('2024-04-02', 'Emily Davis', 120.00, 5.00, 1),
('2024-04-02', 'Michael Brown', 180.00, 15.00, 3),
('2024-04-03', 'Sarah Wilson', 90.00, 0.00, 2),
('2024-04-03', 'David Martinez', 250.00, 25.00, 5),
('2024-04-03', 'David Martinez', 250.00, 25.00, 5),
('2024-04-03', 'David Martinez', 250.00, 25.00, 5),
('2024-04-03', 'David Martinez', 250.00, 25.00, 5),
('2024-04-03', 'David Martinez', 250.00, 25.00, 5),
('2024-04-04', 'David Martinez', 250.00, 25.00, 5),
('2024-04-04', 'David Martinez', 250.00, 25.00, 5),
('2024-04-04', 'David Martinez', 250.00, 25.00, 5),
('2024-04-04', 'David Martinez', 250.00, 25.00, 5),
('2024-04-04', 'David Martinez', 250.00, 25.00, 5),
('2024-04-04', 'David Martinez', 250.00, 25.00, 5),
('2024-04-04', 'David Martinez', 250.00, 25.00, 5),
('2024-04-04', 'David Martinez', 250.00, 25.00, 5),
('2024-04-04', 'David Martinez', 250.00, 25.00, 5),
('2024-04-04', 'David Martinez', 250.00, 25.00, 5),
('2024-04-05', 'David Martinez', 250.00, 25.00, 5),
('2024-04-05', 'David Martinez', 250.00, 25.00, 5),
('2024-04-06', 'David Martinez', 250.00, 25.00, 5),
('2024-04-06', 'David Martinez', 250.00, 25.00, 5),
('2024-04-06', 'David Martinez', 250.00, 25.00, 5),
('2024-04-06', 'David Martinez', 250.00, 25.00, 5),
('2024-04-06', 'David Martinez', 250.00, 25.00, 5),
('2024-04-06', 'David Martinez', 250.00, 25.00, 5);


select * from checkoutLine;

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

CREATE TABLE inventory (
    itemcode INT,
    QTY DOUBLE DEFAULT 0,
    discount_percentage DOUBLE DEFAULT 0,
    selling_price FLOAT,
    cost FLOAT,
    total float
);

drop table inventory;

INSERT INTO inventory (itemcode, QTY, discount_percentage, selling_price, cost) VALUES
(1001, 50, 0.1, 50.00, 40.00),
(1002, 100, 0.15, 100.00, 80.00),
(1003, 75, 0.2, 75.00, 60.00),
(1004, 120, 0.25, 120.00, 90.00),
(1005, 90, 0.18, 90.00, 70.00);

CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nic VARCHAR(20),
    name VARCHAR(100),
    age INT,
    address VARCHAR(255),
    contact VARCHAR(20),
    type VARCHAR(50),
    username VARCHAR(50),
    password VARCHAR(100),
    date_join DATE
);

drop table users;






