# OnlineRetailStore
Basic design using web api and EF

Problem Statement
Build the domain for a check out counter in an online retail store that scans products and generates an itemized bill.
The bill should also total the cost of all the products and the applicable salestax for each product.
The total cost and total sales tax should be printedSales tax varies based on the type of products
- category A products carry a levy of 10%
- category B products carry a levy of 20%
- category C products carry no levy

The solution should address the following aspects 
0. Self sufficient project that can be tested and run 
1. Must use best practices, design methodologies, patterns
2. Must use a dependency injection framework
3. Must be unit tested
4. May use ORM frameworks as required

Pending Items for Solution
- Unit tests
- Multiple scenario validations
- DB optimizations (No index or query cost considered)

Steps to execute the application
1. Use VS2017 (roslyn compiler)
2. Restore Nugets
3. Replace DB file connection string in StoreDbContext.cs file
4. Host Application
5. Use client console application to test

Share if any issues.
