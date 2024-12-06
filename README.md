# TaskManagementAPI

TaskManagementAPI is a simple API for managing tasks. It allows users to create, update, delete, and retrieve tasks. Each task has attributes such as `Title`, `Description`, `Status`, and `DueDate`. The API is built using ASP.NET Core and utilizes a SQL Server database for storage.

## Features

- Create a task
- Update an existing task
- Delete a task
- Retrieve tasks with pagination
- Filter tasks by status and due date

## API Endpoints

### 1. **Get All Tasks**

- **URL:** `/api/tasks`
- **Method:** `GET`
- **Query Parameters:**
  - `status` (optional) - Filter tasks by status (Pending, InProgress, Completed)
  - `dueDate` (optional) - Filter tasks by due date (format: YYYY-MM-DD)
  - `pageNumber` (required) - Page number for pagination
  - `pageSize` (required) - Number of tasks per page
- **Response:**
  - A list of tasks with pagination.

### 2. **Get Task By ID**

- **URL:** `/api/tasks/{id}`
- **Method:** `GET`
- **Response:**
  - A single task with the given `id`.

### 3. **Create Task**

- **URL:** `/api/tasks`
- **Method:** `POST`
- **Request Body:**
  ```json
  {
    "title": "Task title",
    "description": "Task description",
    "status": "Pending",
    "dueDate": "2024-12-06T06:12:46.874Z"
  }



The newly created task with id and other details.

### 4. **Update Task**
- **URL:** `/api/tasks/{id}`
- **Method:** `PUT`
- **Request Body:**
  ```json
  {
    "title": "Updated title",
    "description": "Updated description",
    "status": "InProgress",
    "dueDate": "2024-12-10T06:12:46.874Z"
  }



Response:
The updated task with the given id.
### 5. Delete Task
- **URL:** `/api/tasks/{id}`
- **Method:** `DELETE`


  ### Technologies Used
- **ASP.NET Core - Web API framework**
- **Entity Framework Core - ORM for database interaction**
- **SQL Server - Database to store task data**
