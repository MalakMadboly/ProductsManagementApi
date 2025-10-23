

# 🧩 Products Management API



## 🎯 Objective

The objective of this project is to design and implement a clean, scalable, and maintainable **.NET 8 API** that manages **Products** and **Categories** using:

* Clean Architecture principles
* CQRS pattern with MediatR
* Domain Events for aggregate communication
* Repository pattern for persistence

---

## 🧱 Architecture Overview

The solution follows the **Clean Architecture** structure, separating the project into distinct layers:

```
ProductsManagement/
├── ProductsManagement.Domain/           # Entities, Value Objects, Domain Events, and Interfaces
├── ProductsManagement.Application/      # Commands, Queries, and Handlers (CQRS)
├── ProductsManagement.Infrastructure/   # EF Core DbContext, Repository Implementations
├── ProductsManagement/                  # API layer (Controllers, Dependency Injection)
└── ProductsManagement.sln               # Solution file
```

Each feature (Products, Categories) is organized by folder in each layer, ensuring modularity and separation of concerns.

---

## ⚙️ Technologies Used

* **.NET 8**
* **C# 12**
* **Entity Framework Core**
* **MediatR** (for CQRS and Domain Events)
* **SQLite**
* **Dependency Injection (DI)**

---

## 🔁 CQRS Implementation

All Create, Update, Delete, and Read operations for both **Products** and **Categories** are implemented using **CQRS** through **MediatR**.

* Commands handle write operations (e.g., `CreateProductCommand`, `DeleteCategoryCommand`).
* Queries handle read operations (e.g., `GetAllProductsQuery`, `GetCategoryByIdQuery`).
* Handlers are defined separately in the Application layer to ensure separation between behavior and request logic.

---

## ⚡ Domain Events

To maintain the **independence of aggregates**, Domain Events are used to synchronize actions:

* When a **Product** is **created**, a domain event (`ProductCreatedEvent`) is published, notifying the **Category** to increase its product count.
* When a **Product** is **deleted**, a domain event (`ProductDeletedEvent`) triggers a decrease in the associated category’s product count.
* This ensures the **Product** and **Category** aggregates remain decoupled — **no direct foreign keys** or navigation properties exist between them.

Example Domain Event flow:

```
ProductCreatedEvent → Handled by → UpdateCategoryProductCountHandler → CategoryRepository.UpdateAsync()
```

---

## 📡 API Endpoints

### Categories

| Method   | Endpoint               | Description           |
| -------- | ---------------------- | --------------------- |
| `GET`    | `/api/categories`      | Get all categories    |
| `GET`    | `/api/categories/{id}` | Get category by ID    |
| `POST`   | `/api/categories`      | Create a new category |
| `PUT`    | `/api/categories/{id}` | Update category       |
| `DELETE` | `/api/categories/{id}` | Delete category       |

### Products

| Method   | Endpoint             | Description                                         |
| -------- | -------------------- | --------------------------------------------------- |
| `GET`    | `/api/products`      | Get all products                                    |
| `GET`    | `/api/products/{id}` | Get product by ID                                   |
| `POST`   | `/api/products`      | Create a new product (triggers ProductCreatedEvent) |
| `PUT`    | `/api/products/{id}` | Update product                                      |
| `DELETE` | `/api/products/{id}` | Delete product (triggers ProductDeletedEvent)       |



## ▶️ How to Run

1. Clone the repository:

   ```bash
   git clone https://github.com/MalakMadboly/ProductsManagementApi.git
   cd ProductsManagementApi
   ```

2. Open the solution file `ProductsManagement.sln` in Visual Studio or VS Code.

3. Update the `appsettings.json` file with your SQL Server connection string.

4. Apply migrations (if needed):

   ```bash
   dotnet ef database update
   ```

5. Run the API:

   ```bash
   dotnet run --project ProductsManagement
   ```

6. Open Swagger UI:
   [https://localhost:5001/swagger](https://localhost:5001/swagger)
