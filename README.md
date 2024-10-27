<div id="top"></div>





<!-- PROJECT LOGO -->
<br />
<div align="center">
  <h3 align="center">Shop API</h3>

  <p align="center">
    API Project About Basic Shop App
    <br />
    <!--
    <a href="https://github.com/othneildrew/Best-README-Template"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/othneildrew/Best-README-Template">View Demo</a>
    ·
    <a href="https://github.com/othneildrew/Best-README-Template/issues">Report Bug</a>
    ·
    <a href="https://github.com/othneildrew/Best-README-Template/issues">Request Feature</a>
    -->
  </p>
</div>

<!-- ABOUT THE PROJECT -->

##  About The Project

![Product Name Screen Shot](https://kinsta.com/wp-content/uploads/2019/12/wordpress-rest-api-1024x512.jpg)

This project was developed with .Net 8 framework. While developing the project, I used the N-Layer Architecture approach as the architectural approach.

There are 2 layers in the developed project:
* **Repositories Layer**<br>
  ```sh
   In this layer, the models in the database were created. In addition interfaces 
   of repository and service classes have been added.
  
   DbContext class was created and migration operations were performed. 
   In addition, the models created in the core layer were configured with the help of fluent api.
   
   In addition, in this layer, the interface of the UnitOfWork class, which will 
   perform all operations to be done with the Database through a single channel 
   and keep it in memory, has been developed.
   
   
   
   ```
   
* **Service Layer**<br>
  ```sh
   Contains business logic.
   Data Transfer Objects were created in order to request and respond to the data appropriately.
   Improved mapping classes used to transform data.
   ```
   

<p align="right">(<a href="#top">Back To Top</a>)</p>



## Built With

I used the following technologies while creating this API project.

* [.Net Core 8](https://docs.microsoft.com/tr-tr/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-8.0)
* [Entity Framework Core 6](https://docs.microsoft.com/tr-tr/ef/core/)
* [SQL Server](https://www.google.com/search?client=opera&q=sql+server&sourceid=opera&ie=UTF-8&oe=UTF-8)
* [Visual Studio 2022](https://visualstudio.microsoft.com/tr/vs/)

<p align="right">(<a href="#top">Back To Top</a>)</p>




