# Task Management API (Backend)

## Overview

This is a **Task Management RESTful API** built with **.NET 8**. It provides endpoints to create, read, update, delete, and search tasks. The API is designed for simplicity, maintainability, and quick local setup.

---

## Features

* Full **CRUD** operations for tasks
* Task properties include:

  * **Title** (required, max 100 characters)
  * **Description** (optional, max 500 characters)
  * **Status** (Pending / In-Progress / Completed)
  * **Priority** (Low / Medium / High)
  * **Due Date** (optional)
  * **CreatedAt** and **UpdatedAt** timestamps
* Filtering and sorting by status, priority, due date, and creation date
* Search tasks by title or description
* Input validation and error handling
* RESTful API design with proper HTTP status codes

---

## Tech Stack

* **Backend:** .NET 8 Web API
* **Language:** C#
* **Database:** SQLite (or JSON persistence)
* **Testing:** xUnit and FakeItEasy for unit tests
* **Documentation:** Inline XML comments and markdown

---

## Prerequisites

Make sure the following are installed on your machine:

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* [SQLite](https://www.sqlite.org/download.html) (if using SQLite for persistence)
* Git (optional, for cloning repository)
* Postman / curl (optional, for testing API endpoints)

---

## Getting Started (Local Setup)

### 1. Clone the Repository

```bash
git clone https://github.com/jvdignadice/TaskManagementApi.git
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Database Setup

If using SQLite, the database file is automatically created on first run.
Alternatively, you can seed initial data with:

```bash
dotnet run --seed
```

### 4. Run the Application

```bash
dotnet run
```

The API should now be running on:

```
https://localhost:7043;
http://localhost:5108
```

### 5. Test Endpoints

You can test the API using **Postman**, **curl**, or your frontend app.

Example endpoints:

| Method | Endpoint          | Description         |
| ------ | ----------------- | ------------------- |
| GET    | /api/tasks        | Get all tasks       |
| GET    | /api/tasks/{id}   | Get a task by ID    |
| POST   | /api/tasks        | Create a new task   |
| PUT    | /api/tasks/{id}   | Update a task       |
| DELETE | /api/tasks/{id}   | Delete a task       |
| GET    | /api/tasks/search | Search/filter tasks |

---

## API Usage Example (curl)

```bash
# Get all tasks
curl -X GET https://localhost:7043/api/tasks

# Create a new task
curl -X POST https://localhost:7043/api/tasks \
-H "Content-Type: application/json" \
-d '{"title": "New Task", "description": "Test", "status": 0, "priority": 1}'
```

---

## Testing

To run unit tests:

```bash
dotnet test
```

---

## Project Structure

```
/src/taskmanagement.api.ms.domain
/src/taskmanagement.api.ms.infrastructure
/src/taskmanagement.api.ms. (serves as the application/presentation)

/test/taskmanagement.api.test (holds the unit tests)
```

---

## Known Limitations / Notes

* No authentication implemented (can be added as a bonus feature)
* Filtering is basic (status, priority, and search by title/description)
* Frontend not included in this repo



