# 📦 File Exporter

A project to complete the challenge of building a simple Excel and PDF export application using ASP.NET.

There are 3 branches to cover this project’s development:

- tahap-1 -> completing the first step which is a simple data conversion with hardcoded data to excel or pdf.
- tahap 2 -> is integrating web app to ms sql server, use CTE (Common Table Expression) in SQL Procedure.
- Main branch -> last commit and final polished code to delivered

# 🚀 Tech Stack

- ASP.NET Core 9
- Rotativa.AspNetCore
- ClosedXML
- wkhtmltopdf

# ⚠️ Pre requisite

- .NET 9 SDK Installed
- MS SQL Server Installed

# 🗄️Database Configurations

- you can either put db connection to appsettings.json like this

```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=FileExporterDb;Trusted_Connection=True;"
  }
}

```

or easily using .NET secret manager

1. init user-secret

```bash
dotnet user-secrets init
```

2. store a connection to secret manager

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=FileExporterDb;Trusted_Connection=True;TrustServerCertificate=True;"

```

# 💻 Setup & Run App

1. Clone the repos

```bash
git clone https://github.com/yourusername/FileExporter.git
```

```bash
cd dotnet-file-exporter
```

2. Checkout the branch

- tahap-1 for first challenge

```bash
git checkout tahap-1
```

- tahap-2 or main for second challenge

```bash
git checkout tahap-2
```

```bash
git checkout main
```

3. Install Dependencies

```bash
dotnet restore
```

4. run the app

```bash
dotnet run
```

# 📚 References

- https://github.com/webgio/Rotativa.AspNetCore
- https://github.com/ClosedXML/ClosedXML
- https://wkhtmltopdf.org/downloads.html
