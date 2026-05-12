# MangaNya

MangaNya is a Windows Forms application developed in C# using .NET 10.0. It is designed for managing a manga store's inventory and sales, specifically handling products, customers, and invoicing.

## Project Overview

- **Type:** Windows Forms App (WinExe)
- **Target Framework:** .NET 10.0 (Windows)
- **Primary Technology:** C#, WinForms
- **Data Storage:** Persistent data is stored in text files (e.g., `productos.txt`) using a pipe-delimited format.

### Architecture

The application follows a simple data-centric architecture:
- **Models:** Defined in `Producto.cs`, `Cliente.cs`, `Factura.cs`, and `DetalleFactura.cs`.
- **Data Management:** `Datos.cs` serves as a static data repository and handles I/O operations (loading/saving from text files).
- **UI:** `Form1.cs` is the main entry point for the user interface, utilizing a `TabControl` to separate "Productos" and "Control" sections.

## Building and Running

### Prerequisites
- .NET 10.0 SDK
- Windows OS (required for WinForms)

### Commands
- **Build:** `dotnet build`
- **Run:** `dotnet run`
- **Restore Dependencies:** `dotnet restore`

## Development Conventions

- **Namespaces:** All core logic resides within the `MangaNya` namespace.
- **Data Access:** Global application state is managed through the static `Datos` class.
- **Persistence:** 
    - Products are saved to and loaded from `productos.txt`.
    - Fields are delimited by the pipe character (`|`).
- **Coding Style:**
    - Property names for models are typically lowercase (e.g., `p.codigo`, `p.nombre`).
    - Static lists are used for in-memory data storage during runtime.
- **Testing:** Currently, there are no automated tests. Manual verification through the UI is required.

## Key Files

- `Program.cs`: The entry point of the application.
- `Datos.cs`: Contains the logic for data persistence and global lists.
- `Form1.cs`: The main UI form.
- `MangaNya.csproj`: The project configuration file.
