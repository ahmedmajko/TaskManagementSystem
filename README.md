# Task Management System API

## 📌 Overview
This is a **Task Management System API** built with **.NET 9**, **JWT Authentication**, and **Entity Framework Core**. It provides user authentication, task management, and secure API endpoints.

---

## 🚀 Installation & Setup

### **1️⃣ Prerequisites**
Ensure you have the following installed:
- **.NET 9 SDK** ([Download](https://dotnet.microsoft.com/en-us/download))
- **SQL Server** (LocalDB or SQL Server Express)
- **Postman** (for API testing) *(optional)*

### **2️⃣ Clone the Repository**
```sh
 git clone https://github.com/your-repo/task-management-api.git
 cd task-management-api
```

### **3️⃣ Configure the Database**
1. Open **`appsettings.json`** and update the database connection string:

   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=TaskDb;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
   }
   ```


### **4️⃣ Install Dependencies**
Run the following command to install required dependencies:
```sh
dotnet restore
```

### **5️⃣ Run the Application**
```sh
dotnet run
```
The API will be available at **`http://localhost:5000`** (or **`https://localhost:5001`** for HTTPS).

---

## 🔐 Authentication & Authorization
The API uses **JWT (JSON Web Token)** for authentication. Users must log in to receive a token.

### **1️⃣ Register a New User**
```http
POST /api/auth/register
```
**Request Body:**
```json
{
  "username": "admin@example.com",
  "password": "Admin@123"
}
```
**Response:**
```json
{
  "message": "User registered successfully"
}
```

### **2️⃣ User Login (Get JWT Token)**
```http
POST /api/auth/login
```
**Request Body:**
```json
{
  "username": "testuser",
  "password": "Test@1234"
}
```
**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1..."
}
```

### **3️⃣ Use JWT Token in API Requests**
Include the token in the `Authorization` header for protected routes:
```http
Authorization: Bearer YOUR_JWT_TOKEN
```

---

## 📌 API Endpoints

### **🔹 Task Management**

#### **1️⃣ Get All Tasks** *(Requires Authentication)*
```http
GET /api/tasks
```
**Response:**
```json
[
  {
    "id": 1,
    "title": "Complete project",
    "status": "Pending"
  }
]
```

#### **2️⃣ Create a New Task** *(Requires Authentication)*
```http
POST /api/tasks
```
**Request Body:**
```json
{
  "title": "New Task",
  "status": "In Progress"
}
```

#### **3️⃣ Update a Task**
```http
PUT /api/tasks/{id}
```

#### **4️⃣ Delete a Task**
```http
DELETE /api/tasks/{id}
```

---

## 📌 Swagger API Documentation
Swagger is enabled for API documentation and testing.
- Open: **`http://localhost:5000/swagger`**

---

## 🔧 Troubleshooting
- **Database connection error?** → Ensure SQL Server is running and the connection string is correct.
- **JWT Token Expired?** → Log in again to get a new token.
- **CORS Issues?** → Add `app.UseCors(...)` in `Program.cs` if needed.

---

## 📜 License
This project is licensed under the **MIT License**.

---

## 📬 Contact
For issues or suggestions, contact **your@email.com** or open an issue on GitHub.

Happy Coding! 🚀

