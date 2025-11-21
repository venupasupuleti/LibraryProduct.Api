````markdown
# LibraryProduct.Api

A complete ASP.NET Core Web API for **Library Management + Product Management**,  
built using **.NET 8**, **EF Core**, **SQL Server**, and **Code-First Migrations**.

This README explains how anyone can **clone, configure, migrate, and run** the project successfully.

---

# 🔹 1️⃣ Clone the Repository

```bash
git clone https://github.com/venupasupuleti/LibraryProduct.Api
cd LibraryProduct.Api
````

---

# 🔹 2️⃣ Configure Database Connection

Create a file named:

```
appsettings.json
```

Paste this inside:

```json
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
```

### ✔ If using LocalDB instead:

```json
"Server=(localdb)\\MSSQLLocalDB;Database=LibraryProductDb;Trusted_Connection=True"
```

---

# 🔹 3️⃣ Install Required Tools

Install EF Core CLI tools:

```bash
dotnet tool install --global dotnet-ef
```

---

# 🔹 4️⃣ Restore NuGet Packages

```bash
dotnet restore
```

---

# 🔹 5️⃣ Apply EF Core Migrations

This will automatically create all tables needed.

```bash
dotnet ef database update
```

### If you want to add a new migration:

```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

---

# 🔹 6️⃣ Run the API

```bash
dotnet run
```

API will run at:

```
https://localhost:<port>/
```

Swagger UI:

```
https://localhost:<port>/swagger
```

Use Swagger to test all API endpoints easily.

---

# 🔹 7️⃣ Postman Collection

Import the file inside:

```
/postman/LibraryProduct.postman_collection.json
```

### Set Postman environment variable:

```
baseUrl = https://localhost:<port>
```

Now all Postman requests will work automatically.

---

# 🔹 8️⃣ Project Structure

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

# 🔹 9️⃣ Included Deliverables

✔ GitHub Source Code
✔ README (setup, DB config, migrations, run instructions)
✔ Postman Collection
✔ EF Core Migrations

---

# 🔹 🔟 Support / Troubleshooting

If something doesn't work:

1. Check SQL Server is running
2. Verify connection string in `appsettings.json`
3. Run migrations again:

   ```bash
   dotnet ef database update
   ```
4. Run the API:

   ```bash
   dotnet run
   ```
5. Open Swagger in the browser

---

# 🎉 Project is Ready to Use!

Anyone can clone → configure → migrate → run → test your API in minutes.
