# Restaurant Orders Manager

This web application main task is to help with managing orders in a restaurane as well as to archive the data of all orders. Project is made in C# using ASP.NET MVC framework. Main functionalities: browsing orders based on criteria (order status, table name/number), adding new order, adding new position to existing order, editing positions (meal, quantity) in order, removing positions from order, removing orders (only open ones, closed orders are kept as archive data and cannot be deleted), paying for order (includes tip in a total price and adds tip position, displays a page with order summed up). Data is stored in session variables. 

## Technologies
- ASP.NET MVC framework
- Front-end: JavaScript/jQuery, Bootstrap, Ajax

# Running the application

Application can be run by opening solution file in Visual Studio, compiling/building the app and running it (default shortcut ctrl+F5)

## Planned changes
- Adding dynamic data display (json sent/recieved) to all functionalities without that implemented
- Adding data base and services to get data from it instead of storing data in session variables

