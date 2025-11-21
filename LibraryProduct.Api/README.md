# LibraryProduct.Api

A complete ASP.NET Core Web API for **Library Management + Product Management**,  
built using **.NET 8**, **EF Core**, **SQL Server**, and **Code-First Migrations**.

This README explains how anyone can **clone, configure, migrate, and run** the project successfully.

---

# 1️⃣ Clone the Repository


git clone https://github.com/venupasupuleti/LibraryProduct.Api
cd LibraryProduct.Api


---

# 2️⃣ Configure Database Connection

Create a file:


appsettings.json


Copy this into it:


{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=LibraryProductDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

### ✔ If you use LocalDB, replace with:


"Server=(localdb)\\MSSQLLocalDB;Database=LibraryProductDb;Trusted_Connection=True"


---

# 3️⃣ Install Required Tools

### Install EF Core CLI tools:


dotnet tool install --global dotnet-ef




# 4️⃣ Restore NuGet Packages


dotnet restore


---

# 5️⃣ Apply EF Core Migrations

This will automatically create all tables needed.

dotnet ef database update


### If you want to add a new migration:


dotnet ef migrations add MigrationName
dotnet ef database update


---


Run the project
it's Open Swagger



# 6️⃣ Run the API 


dotnet run


API runs at:


https://localhost:<port>/


Open Swagger UI:


https://localhost:<port>/swagger


Use this to test all endpoints.

---

# 7️⃣ Postman Collection

Import the file provided inside this folder:

```
/postman/LibraryProduct.postman_collection.json
```

### Set Environment Variable:


baseUrl = https://localhost:<port>


Now your Postman calls will work automatically.



# 8️⃣ Project Structure

```
/Controllers
/Services
/Models
/Data
/Migrations
/postman
README.md
```

---

# 9️⃣ Includes All Required Deliverables

✔ GitHub Source Code
✔ README (setup, DB config, migrations, run instructions)
✔ Postman Collection
✔ EF Core Migrations

---

# 🔟 Support


1. Check DB connection
2. Run `dotnet ef database update`
3. Run `dotnet run`
4. Open Swagger



