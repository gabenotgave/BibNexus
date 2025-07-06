# BibNexus - Library Management System

BibNexus is a full-stack university library management system developed with .NET 6 and ASP.NET Core MVC. It provides a comprehensive solution for managing books, authors, genres, and user roles within a library environment. The system is backed by a Microsoft SQL Server database and utilizes Entity Framework Core for data access.

---

## Table of Contents

- [Features](#features)
- [User Roles](#user-roles)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation & Setup](#installation--setup)
  - [Database Seeding](#database-seeding)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)

---

## Features

-   **Book Management**: Admins can perform full CRUD (Create, Read, Update, Delete) operations on books, including details like title, author, publisher, and description.
-   **Author & Genre Management**: Admins can manage the authors and genres available in the system.
-   **User Authentication**: Secure user registration and login system powered by ASP.NET Core Identity.
-   **Role-Based Access Control**: The application distinguishes between two primary roles: Admin and Member.
-   **Interactive UI**: A clean and responsive user interface built with Bootstrap, featuring functionalities like data tables with sorting and pagination.
-   **Image Uploads**: Support for uploading and displaying book cover images.

## User Roles

-   **Admin**: Has full control over the application. Admins can manage books, authors, genres, and view all registered users.
-   **Member**: Can view the library's collection of books, authors, and genres.

---

## Technologies Used

-   **Backend**: .NET 6, ASP.NET Core MVC
-   **Database**: Microsoft SQL Server
-   **ORM**: Entity Framework Core 6
-   **Authentication**: ASP.NET Core Identity
-   **Frontend**: Razor Views, HTML, CSS
-   **UI Framework**: Bootstrap 5
-   **JavaScript Libraries**: jQuery, DataTables.js for enhanced HTML tables.

---

## Getting Started

Follow these instructions to get a copy of the project up and running on your local machine for development and testing.

### Prerequisites

-   [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
-   A SQL Server instance (like the free [SQL Server Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads))
-   A code editor like [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/).

### Installation & Setup

1.  **Clone the repository:**
    ```bash
    git clone [URL_to_your_BibNexus_repo]
    cd [BibNexus-repo-name]
    ```

2.  **Configure the Database Connection:**
    Open the `appsettings.json` file located in the `BibNexus` project folder. Modify the `DefaultConnection` string to point to your local SQL Server instance.

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=BibNexus;Trusted_Connection=True;MultipleActiveResultSets=true"
      },
      // ...
    }
    ```
    > **Note:** Replace `YOUR_SERVER_NAME` with the name of your SQL Server instance (e.g., `.` for a local default instance, or `(localdb)\\mssqllocaldb`).

3.  **Apply Database Migrations:**
    The database schema is managed using EF Core Migrations. To create and initialize your local database, open a terminal in the root directory of the project (`BibNexus/`) and run the following command:

    ```bash
    dotnet ef database update
    ```
    This command will apply all existing migrations and create the necessary tables for the application.

### Database Seeding

The application is configured to automatically seed the database with an initial Admin user and essential roles when it first runs.

-   **Default Admin Credentials:**
    -   **Email**: `admin@bibnexus.com`
    -   **Password**: `P@ssword123`

You can log in with these credentials to access the administrative features of the application.

---

## Project Structure

The solution is organized into two main projects:

-   **`BibNexus.Core`**: A class library containing the domain models (Entities) and ViewModels. This project represents the core business logic and data structures.
-   **`BibNexus`**: The main ASP.NET Core MVC web application. It contains the Controllers, Views, `wwwroot` assets, and the `ApplicationDbContext` for data access.

This separation helps to keep the core domain logic independent of the web application's implementation details.

---

## Contributing

Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".

1.  Fork the Project
2.  Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3.  Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4.  Push to the Branch (`git push origin feature/AmazingFeature`)
5.  Open a Pull Request
