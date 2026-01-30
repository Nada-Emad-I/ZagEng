-- The Observability Bottleneck Composite Indexes)
Create Index idx_ServiceInDate
On AppLogs (service_name, created_at Desc)

--The "Key Lookup" Mystery Covering Indexes)
/*
Query is Slow Because in Select statement Columns not Found in indexes

Do Covering Index 
*/
Create Index idx_ordersOfCustomers
On Orders (order_date)
Include (order_id, customer_id, total_amount)

--The "Dynamic Flash Sale" Stored Procedures)

Create Proc sp_ApplyCategoryDiscount
    @CatID int,
    @DiscountPercent Decimal(5,2)
As
Begin
    Update Products
    Set price = 
        Case
            When price * (1 - @DiscountPercent / 100.0) < min_price
            Then min_price
            Else price * (1 - @DiscountPercent / 100.0)
        End
    Where category_id = @CatID;
End




--The "Marketing VIP Dashboard" Views)
Create Or Alter View v_VIPCustomers
As
Select
    C.name,
    C.email,
    Sum(O.total_amount) total_spent
From Customers C
Join Orders O
  On C.customer_id = O.customer_id
Group By C.name, C.email
Having Sum(O.total_amount) > 5000
