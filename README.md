# Team Task Management API

A secure and collaborative task management RESTful API built with ASP.NET Core and SQL Server. This API allows users to register, create teams, assign tasks, and manage them securely using JWT authentication.

## Setup Steps

### 1. Clone the Repository

```bash
git clone https://github.com/ojotobar/team-task-management-api.git
```

or

```bash
git clone git@github.com:ojotobar/team-task-management-api.git
```

then run

```bash
cd team-task-management-api
```

### 2. Set Up SQL Server

Ensure SQL Server is running. Update the connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "Default": "Server=YOUR_SERVER_NAME;Database=TaskManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 3. Apply Migrations & Seed Database

```bash
dotnet ef database update
```

or

```bash
Update-Database
```

### 4. Run the Application

```bash
dotnet run
```

On Visual Studio, the run button should launch the swagger UI

### 5. Access Swagger UI

Visit [https://localhost:7052/swagger/index.html](https://localhost:7052/swagger/index.html) or [http://localhost:5052/swagger/index.html](http://localhost:5052/swagger/index.html) to explore and test the API endpoints.

<span style="color:red;"><strong>Version: 1</strong></span>

## Tech Stack

- **Backend:** ASP.NET Core 6
- **Authentication:** JWT Bearer Tokens
- **Database:** SQL Server + Entity Framework Core (Code-First)
- **Migrations:** EF Core CLI
- **Documentation:** Swagger / Swashbuckle
- **Logging:** Serilog
- **Testing:** xUnit

## API Usage Examples (Note that the version number is 1 for all endpoints)

### Authentication

#### POST /auth/register

**Request:**

```bash
curl -X 'POST' \
  'https://localhost:7052/api/v1/auth/register' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json-patch+json' \
  -d '{
  "name": "name",
  "email": "name@email.com",
  "password": "Password",
  "confirmPassword": "Password",
  "role": 1
}'
```

<span style="color:red;"><strong>Version: 1</strong></span>

```json
{
	"name": "string",
	"email": "string",
	"password": "string",
	"confirmPassword": "string",
	"role": 1
}
```

**Response:**

```json
{
	"Id": "guid",
	"Name": "name",
	"Email": "name@email.com"
}
```

#### POST /auth/login

**Request:**

```bash
curl -X 'POST' \
  'https://localhost:7052/api/v1/auth/login' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json-patch+json' \
  -d '{
  "email": "name@email.com",
  "password": "Password"
}'
```

<span style="color:red;"><strong>Version: 1</strong></span>

```json
{
	"email": "name@email.com",
	"password": "Password"
}
```

**Response:**

```json
{
	"AccessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...."
}
```

### User Info

#### GET /users/me

<span style="color:red;"><strong>Version: 1</strong></span>

**Headers:**

```
Authorization: Bearer {token}
```

```bash
curl -X 'GET' \
  'https://localhost:7052/api/v1/users/me' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9....'
```

**Response:**

```json
{
	{
		"Id": "guid",
		"Name": "Name",
		"Email": "name@email.com",
		"Teams": [
			{
				"Id": "guid",
				"Name": "Team A"
			}
		]
	},
}
```

### Team Management

#### POST /teams

**Request:**

```bash
curl -X 'POST' \
  'https://localhost:7052/api/v1/teams' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9....' \
  -H 'Content-Type: application/json-patch+json' \
  -d '{
  "teamName": "Team A"
}'
```

<span style="color:red;"><strong>Version: 1</strong></span>

```json
{
	"teamName": "Team A"
}
```

**Response:**

```json
{
	"Id": "guid",
	"Name": "Team A"
}
```

#### POST /teams/{teamId}/users

**Request:**

```bash
curl -X 'POST' \
  'https://localhost:7052/api/v1/teams/{team-guid}/users' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9....' \
  -H 'Content-Type: application/json-patch+json' \
  -d '{
  "userIds": [
    "guid"
  ]
}'
```

<span style="color:red;"><strong>Version: 1</strong></span>

```json
{
	"userIds": ["guid"]
}
```

**Response:**

```json
{
	"Result": "1 user successfully invited to the Team A team.",
	"Success": true
}
```

### Task Management

#### POST /teams/{teamId}/tasks

```bash
curl -X 'POST' \
  'https://localhost:7052/api/v1/teams/{team-guid}/tasks' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9....' \
  -H 'Content-Type: application/json-patch+json' \
  -d '[
  {
    "taskTitle": "Fix Bug #3",
    "description": "Fix the bug with registration validation",
    "assignTo": "{assigned-user-guid}",
    "dueOn": "2025-05-31"
  }
]'
```

<span style="color:red;"><strong>Version: 1</strong></span>

```json
[
	{
		"taskTitle": "Fix Bug #3",
		"description": "Fix the bug with registration validation",
		"assignTo": "{assigned-user-guid}",
		"dueOn": "2025-05-31"
	}
]
```

**Response:**

```json
[
	{
		"Id": "{task-guid}",
		"CreatedAt": "2025-05-24T14:38:38.02791Z",
		"Title": "Fix Bug #3",
		"Description": "Fix the bug with registration validation",
		"DueDate": "2025-05-31T00:00:00",
		"Status": "Pending"
	}
]
```

#### GET /teams/{teamId}/tasks

**Request:**
<span style="color:red;"><strong>Version: 1</strong></span>

```bash
curl -X 'GET' \
  'https://localhost:7052/api/v1/teams/{team-guid}/tasks' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9....'
```

**Response:**

```json
[
	{
		"Id": "{task-guid}",
		"Title": "Fix Bug #3",
		"Description": "Fix the bug with registration validation",
		"DueDate": "2025-05-31T00:00:00",
		"CreatedAt": "2025-05-24T14:38:38.02791",
		"Status": "Pending",
		"CreatedByUser": {
			"Id": "{created-by-guid}",
			"Name": "Name",
			"Email": "name@email.com"
		},
		"AssignedToUser": {
			"Id": "{assigned-guid}",
			"Name": "Name",
			"Email": "name@email.com"
		},
		"Team": {
			"Id": "{team-guid}",
			"Name": "Team A"
		}
	}
]
```

#### PUT /tasks/{taskId}

**Request:**

```bash
curl -X 'PUT' \
  'https://localhost:7052/api/v1/tasks/{task-guid}' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9....' \
  -H 'Content-Type: application/json-patch+json' \
  -d '{
  "taskTitle": "Bug #4",
  "description": "Refactor validation logic",
  "assignTo": "{assigned-user-guid}",
  "dueOn": "2025-06-05"
}'
```

<span style="color:red;"><strong>Version: 1</strong></span>

```json
{
	"taskTitle": "Bug #4",
	"description": "Refactor validation logic",
	"assignTo": "{assigned-user-guid}",
	"dueOn": "2025-06-05"
}
```

**Response:**

```json
{
	"Id": "{task-guid}",
	"CreatedAt": "2025-05-24T14:38:38.02791",
	"Title": "Bug #4",
	"Description": "Refactor validation logic",
	"DueDate": "2025-06-05T00:00:00",
	"Status": "Pending"
}
```

#### DELETE /tasks/{taskId}

<span style="color:red;"><strong>Version: 1</strong></span>

```bash
curl -X 'DELETE' \
  'https://localhost:7052/api/v1/tasks/{task-guid}' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9....'
```

**Response:**

```json
{
	"Result": "Task record successfully deleted",
	"Success": true
}
```

#### PATCH /tasks/{taskId}/status

**Request:**

```bash
curl -X 'PATCH' \
  'https://localhost:7052/api/v1/tasks/{task-guid}' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9....' \
  -H 'Content-Type: application/json-patch+json' \
  -d '{
  "status": 1
}'
```

<span style="color:red;"><strong>Version: 1</strong></span>

```json
{
	"status": 1
}
```

**Response:**

```json
{
	"Id": "{task-guid}",
	"CreatedAt": "2025-05-24T14:38:38.02791",
	"Title": "Bug #4",
	"Description": "Refactor validation logic",
	"DueDate": "2025-06-05T00:00:00",
	"Status": "In Progress"
}
```

## Running Unit Tests

Make sure you're in the project solution root:

```bash
dotnet test
```

Example output:

```bash
Passed!  - Failed:     0, Passed:    38, Skipped:     0, Total:    38, Duration: 47 ms - TeakTaskManagerApi.Tests.dll (net6.0)
```

## Assumptions

- A user must be authenticated to access any endpoint.
- A user can belong to multiple teams.
- Only team admins can create a team (Team creator becomes a member by default).
- Only team members can invite other users, view or modify tasks in their own teams.
- Task status is limited to `Pending` - 0, `InProgress` - 1, and `Completed` - 2.
- User role is limited to `TeamAdmin` - 1, `Member` - 2.
- Task assignment is restricted to members of the team the task belongs to.
- No frontend is provided — the API is backend-only.
