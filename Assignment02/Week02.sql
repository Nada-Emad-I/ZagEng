--Problem 1
Select P.product_name,S.year,S.price
From Sales S Inner Join Product P
On S.product_id = P.product_id


--Problem 2
Select V.customer_id ,Count(*) count_no_trans
From Visits V Left Join Transactions T
On V.visit_id= T.visit_id
Where T.transaction_id Is Null
Group By V.customer_id


--Problem 3
Select U.unique_id , E.name
From EmployeeUNI U Right Join  Employees E 
On U.id = E.id

--Problem 4
Select W.id 
From Weather W Inner Join Weather We
On DATEDIFF(day, We.recordDate, W.recordDate) = 1 
And W.temperature > We.temperature 

--Problem 5
Select E.emp_name , IsNull(dept_name,'Unassigned')
From Employees E Left Join Departments D
On E.dept_id = D.dept_id 

--Problem 6
Select P.product_name ,S.supplier_name
From Products P Left Join Suppliers S
On P.supplier_id = S.supplier_id
Where P.product_name Like '%Phone%'

--Problem 7
Select Concat(C.first_name+' '+C.last_name)[full_name],O.order_id,O.amount
From Customers C Full Join Orders O
On C.customer_id =O.customer_id


/* ============================================================================== 
   ADVANCED JOINS
=============================================================================== */

/* Get all customers who haven't placed any order */
Select *
From Customers C Left Join Orders O
On C.id =O.customer_id
Where O.customer_id Is Null

/* Get all orders without matching customers */
Select *
From Customers C Right Join Orders O
On C.id =O.customer_id
Where C.id Is Null


-- Alternative to RIGHT ANTI JOIN using LEFT JOIN
/* Get all orders without matching customers */    

Select *
From Orders O Left Join Customers C
On C.id =O.customer_id
Where C.id Is Null

/* Get all customers along with their orders, 
   but only for customers who have placed an order */

Select *
From Customers C Join Orders O
On C.id =O.customer_id

/* Find customers without orders and orders without customers */
Select *
From Customers C  Full Join Orders O
On C.id =O.customer_id
Where C.id Is Null Or O.customer_id Is Null

/* Generate all possible combinations of customers and orders */
Select *
From Customers Cross Join Orders


/* ============================================================================== 
   MULTIPLE TABLE JOINS (4 Tables)
=============================================================================== */

/* Task: Using SalesDB, Retrieve a list of all orders, along with the related customer, product, 
   and employee details. For each order, display:
   - Order ID
   - Customer's name
   - Product name
   - Sales amount
   - Product price
   - Salesperson's name */
   Use SalesDB
   Go

Select O.OrderID,C.FirstName + ' ' + IsNull(C.LastName, '') As CustomerName,
    P.Product As ProductName,O.Sales As SalesAmount,
    P.Price As ProductPrice,E.FirstName + ' ' + IsNull(E.LastName, '') As SalesPersonName
From Sales.Orders O Left Join Sales.Customers C 
            On O.CustomerID = C.CustomerID 
Left Join Sales.Products P 
            On O.ProductID = P.ProductID
Left Join Sales.Employees E 
            On O.SalesPersonID = E.EmployeeID
