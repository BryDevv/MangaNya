# MangaNya - Project Overview

MangaNya is a C# Windows Forms application designed as a simple Point of Sale (POS) and inventory management system for a store. It allows for managing products, handling a shopping cart, and generating sales tickets.

## Project Structure

- **`Form1.cs`**: The main entry point for the user interface. It handles product display, cart management, and the checkout process.
- **`Datos.cs`**: A static utility class that serves as the data access layer. it handles:
    - In-memory storage for `Productos`, `Clientes`, `Facturas`, and a `CarritoTemporal`.
    - Data persistence to and from `productosv1.txt`.
    - Business logic such as calculating totals and updating stock.
- **Models**:
    - `Producto.cs`: Represents a store item (code, name, brand, prices, quantity).
    - `Cliente.cs`: Represents a customer (NIT, name).
    - `Factura.cs` / `DetalleFactura.cs`: Represent sales records and their line items.
- **`Program.cs`**: The application entry point that initializes and runs `Form1`.

## Technologies

- **Language**: C#
- **Framework**: .NET 10.0-windows
- **UI Framework**: Windows Forms (WinForms)
- **Persistence**: Flat-file database (`productosv1.txt`) using a pipe-delimited format.

## Building and Running

This project requires the .NET 10 SDK with Windows Desktop support.

### Key Commands

- **Build**: `dotnet build`
- **Run**: `dotnet run`
- **Clean**: `dotnet clean`

## Development Conventions

- **Implicit Usings**: Enabled in the `.csproj`, reducing the need for explicit `using` statements for common namespaces.
- **Nullable Reference Types**: Enabled for better type safety.
- **Data Persistence**: Data is saved to `productosv1.txt` whenever the inventory changes (e.g., after a sale or when a new product is added).
- **Ticket Generation**: Uses `System.Drawing.Printing` to "print" a formatted receipt to the default system printer or a print preview.

## Data Format (`productosv1.txt`)

The data is stored in the following format:
`codigo|nombre|marca|precioCompra|precioVenta|cantidad`
