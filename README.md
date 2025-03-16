# RealEstateApp

##  Project Overview
RealEstateApp is a web-based platform for buying, selling, and renting properties. It includes an AI-powered price prediction system to help investors make informed decisions.

## 🚀 Features
- User authentication (Register, Login, Forgot Password)
- Property listing and search with filters
- Favorites & bookings
- AI-driven property price predictions
- Notifications system

## Installation & Setup

### 1️⃣ Clone the Repository
```sh
git clone https://github.com/aishahmr/RealEstateApp.git
cd RealEstateApp
```

### 2️⃣ Set Up the Database
- Install **SQL Server** and **SQL Server Management Studio (SSMS)**.
- Update your `appsettings.json` with your database connection string.
- Run migrations:
  ```sh
  dotnet ef database update
  ```

### 3️⃣ Run the Project
```sh
dotnet run
```
Now, visit **Swagger** at: [https://localhost:7030/swagger](https://localhost:7030/swagger)

##  API Endpoints (Example)
- `POST /api/Account/Register` → Register a new user
- `POST /api/Account/Login` → Authenticate user
- `GET /api/Property/GetAllProperties` → Fetch all properties
- `POST /api/Booking/BookProperty` → Book a property

## Contributing
1. Fork the repository
2. Create a feature branch (`git checkout -b feature-name`)
3. Commit changes (`git commit -m "Add new feature"`)
4. Push to GitHub and create a PR!

## 📄 License
This project is licensed under the MIT License.

---


