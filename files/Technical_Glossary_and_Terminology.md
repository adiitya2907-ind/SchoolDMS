# TECHNICAL GLOSSARY & TERMINOLOGY GUIDE

## A

### API (Application Programming Interface)
The interface that allows the Angular frontend to communicate with the .NET Core backend. Our system has 46 REST API endpoints.

**Example:** `POST /api/auth/login` is an API endpoint for user authentication.

### Async/Await
C# keywords for asynchronous programming, allowing non-blocking operations (especially database calls).

**Example:**
```csharp
public async Task<Visit> GetVisitAsync(int visitId)
{
    return await _context.Visits.FindAsync(visitId);
}
```

### Authorization
Checking if an authenticated user has permission to access a resource.

**Our System:** Role-based authorization (Engineer, OpsVerifier, Vendor, Admin)

### Authentication
Verifying who the user is (login process). In our system, JWT tokens are used.

**Example:** User enters email/password → System verifies → JWT token issued

---

## B

### Backup
Creating a copy of the database for disaster recovery.

**Our Strategy:** Daily full backups + hourly transaction log backups

### BCrypt
A password hashing algorithm that makes passwords unreadable and secure.

**Why It Matters:** Never store passwords in plain text. BCrypt is one-way encryption.

### Blob Storage
Cloud storage for large files (images, PDFs, documents).

**Our System:** Could use Azure Blob Storage or AWS S3 for document storage

### BIT (SQL Data Type)
Boolean in SQL Server (0 = False, 1 = True).

**Example:** IsActive BIT DEFAULT 1 means new users are active by default

---

## C

### CORS (Cross-Origin Resource Sharing)
Security policy that allows frontend to communicate with backend API.

**Our Configuration:**
```json
"Cors": {
  "AllowedOrigins": ["http://localhost:4200"]
}
```

### Cardinality (in Database)
The relationship between tables (1:1, 1:Many, Many:Many).

**Example:** One School has Many Visits (1:M relationship)

### Check Constraint
Database rule that ensures data validity.

**Example:** 
```sql
CHECK (Status IN ('Draft', 'Submitted', 'Approved', 'Rejected'))
-- Only these values allowed
```

### Clustered Index
Primary key index that determines physical order of rows in the table.

**Our System:** Each table has one clustered index (the primary key)

---

## D

### DTO (Data Transfer Object)
An object used to transfer data between the API and client, separate from database entities.

**Example:**
```csharp
// Entity (Database)
public class User { public int UserId { get; set; } }

// DTO (API Response)
public class UserDTO { public int Id { get; set; } }
```

### Database Normalization
Organizing data to reduce redundancy and improve integrity.

**Our System:** Fully normalized to prevent data duplication

### Dependency Injection (DI)
Pattern where dependencies are provided to classes rather than creating them internally.

**Example:**
```csharp
public VisitService(IRepository<Visit> repo)
{
    _repo = repo; // Injected dependency
}
```

### Decimal (SQL Data Type)
Precise numeric type for measurements like GPS coordinates.

**Example:** Latitude DECIMAL(10, 8) can store values like 28.59354827

---

## E

### Entity Framework Core (EF Core)
ORM (Object-Relational Mapping) that maps C# classes to database tables.

**Our System Uses:** EF Core for all database operations

**Example:**
```csharp
await _context.Visits.ToListAsync(); // LINQ query
// Converts to SQL behind the scenes
```

### Endpoint
A URL that represents an API function.

**Example:** 
- Endpoint: `GET /api/visits/1`
- Returns: Visit with ID 1

### Enum (Enumeration)
Set of predefined constant values.

**Example:**
```csharp
public enum VisitStatus
{
    Draft = 0,
    Submitted = 1,
    Approved = 2,
    Rejected = 3
}
```

### Extension Methods
Methods that extend existing classes without modifying original code.

**Our Usage:** Custom extension methods for service registration in Program.cs

---

## F

### Foreign Key (FK)
Database relationship linking one table to another.

**Example:** 
```sql
EngineerId INT NOT NULL
CONSTRAINT FK_Visits_Users FOREIGN KEY (EngineerId) REFERENCES Users(UserId)
```
This links Visits.EngineerId to Users.UserId

### Full-Text Search Index
Indexes text content for fast searching.

**Our System:** DocumentSearch table uses full-text indexing for OCR text

### FluentValidation
Library for building validation rules in C#.

**Example:**
```csharp
public class CreateUserValidator : AbstractValidator<CreateUserDTO>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).MinimumLength(8);
    }
}
```

---

## G

### GPS (Global Positioning System)
Location technology using latitude/longitude coordinates.

**Our System:** 
- Mandatory check-in with GPS coordinates
- Spatial index for location validation
- 500m tolerance for check-in radius

### Geospatial Query
Database query using location coordinates.

**Our Usage:** Finding schools within 5km of GPS location

---

## H

### Hash (Cryptographic)
One-way encryption of passwords using BCrypt.

**Example:** 
```
Plain: "MyPassword123"
Hashed: "$2a$11$ZIH/p4w4/Yxg1h8Qw..."
(Never reverse, compare at login)
```

### HTTP Status Codes
Numeric codes indicating request results.

**Common Codes in Our API:**
- 200 OK - Success
- 201 Created - Resource created
- 400 Bad Request - Invalid input
- 401 Unauthorized - No token
- 403 Forbidden - Insufficient permissions
- 404 Not Found - Resource doesn't exist
- 500 Internal Server Error - Server error

---

## I

### Identity (Database)
Auto-incrementing primary key that generates unique IDs automatically.

**Example:** 
```sql
UserId INT PRIMARY KEY IDENTITY(1,1)
-- First user gets ID 1, next gets 2, etc.
```

### Index
Data structure that speeds up database queries.

**Types:**
- Clustered Index (PK, one per table)
- Non-Clustered Index (multiple per table)
- Full-Text Index (text search)
- Spatial Index (GPS coordinates)

### INNER JOIN
SQL operation combining rows from two tables where condition matches.

**Example:**
```sql
SELECT * FROM Visits v
INNER JOIN Schools s ON v.SchoolId = s.SchoolId
-- Only visits with matching schools
```

### IRepository<T>
Generic interface for data access operations (CRUD).

**Our Usage:** All database access goes through this interface

---

## J

### JWT (JSON Web Token)
Token-based authentication mechanism.

**Structure:** `header.payload.signature`

**Example:**
```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.
eyJ1c2VySWQiOjEsImVtYWlsIjoiam9obkBleGFtcGxlLmNvbSJ9.
HQpyKxm8aW...
```

**Our Implementation:**
- 1 hour access token validity
- 7 days refresh token validity
- HS256 signature algorithm

---

## K

### Key (Database)
Unique identifier for each row.

**Types:**
- Primary Key (PK) - Unique identifier
- Foreign Key (FK) - Reference to another table
- Unique Key - Ensures uniqueness

---

## L

### LINQ (Language Integrated Query)
C# syntax for querying data.

**Example:**
```csharp
var approvedVisits = await _context.Visits
    .Where(v => v.Status == "Approved")
    .ToListAsync();
```

### LEFT JOIN
SQL operation keeping all rows from left table even if no match on right.

**Example:**
```sql
SELECT * FROM Users u
LEFT JOIN Visits v ON u.UserId = v.EngineerId
-- Shows all users, even those with no visits
```

---

## M

### Middleware
Components that process requests/responses in the API pipeline.

**Our Middlewares:**
- JWT Authentication Middleware
- Exception Handling Middleware
- Logging Middleware

### Migration
Code that creates/modifies database schema.

**Example:**
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## N

### NVARCHAR (SQL Data Type)
Unicode string type supporting multiple languages.

**Example:** NVARCHAR(255) for school names supporting Hindi, English, etc.

### Null
Absence of value in database (different from 0 or empty string).

**Our Usage:** Optional fields can be NULL, mandatory fields NOT NULL

### Non-Clustered Index
Additional indexes improving query performance without determining row order.

**Our System:** 30+ non-clustered indexes on frequently searched columns

---

## O

### ORM (Object-Relational Mapping)
Technology mapping database tables to C# classes automatically.

**Our System:** Entity Framework Core

### Offset
Pagination parameter skipping N records.

**Example:** `offset=20, limit=10` skips first 20, returns next 10

---

## P

### Pagination
Breaking large result sets into manageable pages.

**Our API:** All list endpoints support pagination (default 20, max 100)

### Parameter (SQL)
Placeholder in SQL query preventing SQL injection.

**Example:**
```csharp
var users = await _context.Users
    .Where(u => u.Email == userEmail) // Parameterized
    .ToListAsync();
```

### Primary Key (PK)
Unique identifier for each row in a table.

**Example:** UserId INT PRIMARY KEY

### Pragma (SQL Server)
Execution setting affecting query behavior.

**Example:** `PRAGMA table_info()` shows table structure

---

## Q

### Query Execution Plan
SQL Server's strategy for executing a query.

**Our Usage:** Analyze slow queries using execution plans

---

## R

### RBAC (Role-Based Access Control)
Authorization system granting permissions based on user roles.

**Our Roles:**
- Engineer (Create/update own visits)
- OpsVerifier (Approve/reject visits)
- Vendor (Read-only access)
- Admin (Full access)

### Relation/Relationship (Database)
Connection between tables using keys.

**Types:** 1:1, 1:Many, Many:Many

### Repository Pattern
Design pattern abstracting data access logic.

**Our Usage:** `IRepository<T>` for all database operations

### REST (Representational State Transfer)
Web service style using HTTP methods.

**HTTP Methods:**
- GET - Retrieve data
- POST - Create data
- PUT - Update data
- DELETE - Delete data
- PATCH - Partial update

---

## S

### Scalar Function
SQL function returning single value.

**Example:** `GETUTCDATE()` returns current UTC date/time

### Scope (Dependency Injection)
Lifetime of injected dependency.

**Scopes:**
- Transient - New instance every time
- Scoped - One instance per request
- Singleton - One instance for entire application

### Seed Data
Initial data inserted into database.

**Our Usage:** Pre-populate roles, projects, default admin user

### Spatial Index
Index for geographic data (latitude/longitude).

**Our System:** Spatial index on Schools and Visits GPS coordinates

### Stored Procedure
Pre-compiled SQL stored in database.

**Example:** `sp_GetDashboardMetrics` returns dashboard metrics

### String Interpolation
Embedding variables in C# strings.

**Example:** `$"Hello {userName}, your ID is {userId}"`

---

## T

### Table (Database)
Collection of related data organized in rows and columns.

**Our System:** 9 main tables (Users, Schools, Visits, Documents, etc.)

### Transaction
Atomic unit of work - all-or-nothing operation.

**Example:** Submit visit + create approval workflow must both succeed or both fail

### Trigger
Automatic action executed when data changes.

**Our System:** Triggers for updating `UpdatedAt` timestamp automatically

### Type Safety
Ensuring correct data types preventing errors.

**Our Usage:** C# strongly-typed with enums instead of strings

---

## U

### UDISE Code
Unique identifier for schools in India's education system.

**Example:** "UP000001" - Unique for each school

### Unique Index
Prevents duplicate values in one or more columns.

**Our Usage:** Email must be unique (no duplicate accounts)

### UNION
SQL operation combining results from multiple queries.

**Example:**
```sql
SELECT COUNT(*) FROM Visits WHERE Status = 'Approved'
UNION ALL
SELECT COUNT(*) FROM Visits WHERE Status = 'Rejected'
```

---

## V

### View (Database)
Virtual table based on SQL query results.

**Our Views:**
- `vw_PendingVerification` - Visits awaiting approval
- `vw_EngineerVisitSummary` - Engineer statistics
- `vw_SchoolVisitFrequency` - School visit tracking

### Validation
Checking data correctness before processing.

**Our Levels:**
- Client-side (Angular)
- Server-side (C# FluentValidation)
- Database (Constraints)

---

## W

### WHERE Clause
SQL filter limiting returned rows.

**Example:**
```sql
SELECT * FROM Visits WHERE Status = 'Approved'
-- Only approved visits
```

---

## X

### XML Comments
Documentation comments in C# code.

**Example:**
```csharp
/// <summary>Gets a visit by ID</summary>
/// <param name="id">Visit ID</param>
/// <returns>Visit object</returns>
public async Task<Visit> GetVisitAsync(int id)
```

---

## Z

### Zone (Database)
Geographic or logical area grouping data.

**Our System:** PAN-India (covers all states/districts)

---

## ACRONYMS GLOSSARY

| Acronym | Meaning | Usage |
|---------|---------|-------|
| API | Application Programming Interface | REST endpoints |
| ACID | Atomicity, Consistency, Isolation, Durability | Database transactions |
| BCRYPT | Blowfish Cryptography | Password hashing |
| CORS | Cross-Origin Resource Sharing | Security policy |
| CRUD | Create, Read, Update, Delete | Basic operations |
| DTO | Data Transfer Object | API objects |
| DRY | Don't Repeat Yourself | Code principle |
| DI | Dependency Injection | Design pattern |
| EF | Entity Framework | ORM |
| FK | Foreign Key | Database relationship |
| HTML | Hyper Text Markup Language | Frontend |
| HTTP | Hyper Text Transfer Protocol | Web protocol |
| JSON | JavaScript Object Notation | Data format |
| JWT | JSON Web Token | Authentication |
| LINQ | Language Integrated Query | Data querying |
| ORM | Object-Relational Mapping | EF Core |
| PK | Primary Key | Unique identifier |
| RBAC | Role-Based Access Control | Authorization |
| REST | Representational State Transfer | API style |
| SQL | Structured Query Language | Database |
| UDISE | Unique District Information System for Education | School ID |
| UQ | Unique | Constraint |

---

## COMMON PATTERNS & CONVENTIONS

### Naming Conventions

**Tables:** PascalCase, Plural
- `Users`, `Visits`, `Documents`, `ApprovalWorkflow`

**Columns:** PascalCase
- `UserId`, `FirstName`, `CreatedAt`, `IsActive`

**Stored Procedures:** `sp_` prefix
- `sp_GetDashboardMetrics`, `sp_ValidateVisitCompletion`

**Views:** `vw_` prefix
- `vw_PendingVerification`, `vw_EngineerVisitSummary`

**Indexes:** `IX_TableName_Columns`
- `IX_Users_Email`, `IX_Visits_EngineerId`

**Foreign Keys:** `FK_ChildTable_ParentTable`
- `FK_Visits_Schools`, `FK_Documents_Visits`

### Code Patterns

**Async Method Naming:** Suffix with `Async`
```csharp
public async Task<Visit> GetVisitAsync(int id)
```

**Interface Naming:** `I` prefix
```csharp
public interface IVisitService
```

**Enum Naming:** Singular
```csharp
public enum VisitStatus
public enum DocumentType
```

---

## TECHNICAL DECISION RATIONALE

### Why .NET Core?
- Enterprise-grade performance
- Strong type safety
- Excellent async/await support
- Rich ORM (Entity Framework)
- Cross-platform deployment

### Why JWT Tokens?
- Stateless authentication
- No session storage needed
- Perfect for mobile apps
- Industry standard for APIs
- Secure token-based model

### Why Repository Pattern?
- Abstraction of data access
- Easy to unit test
- Easy to change data source
- Loose coupling
- Clean code architecture

### Why Spatial Indexes?
- Fast GPS location queries
- Real-time verification
- Prevents fake check-ins
- Geographic filtering capability

### Why Full-Text Indexes?
- Fast document searching
- OCR text indexing
- Better user experience
- Compliance with search requirements

---

## TROUBLESHOOTING TERMINOLOGY

### Error Categories

**Authentication Errors:**
- 401 Unauthorized - Missing/invalid JWT token
- Solution: Include valid token in Authorization header

**Authorization Errors:**
- 403 Forbidden - User doesn't have permission
- Solution: Verify user role matches endpoint requirements

**Validation Errors:**
- 400 Bad Request - Invalid input data
- Solution: Check request body against API specification

**Database Errors:**
- 500 Internal Server Error - Connection or query issue
- Solution: Check connection string, database status, SQL syntax

**CORS Errors:**
- No 'Access-Control-Allow-Origin' header
- Solution: Add origin to CORS policy in Program.cs

---

## CONCLUSION

This glossary covers all technical terms, acronyms, and patterns used in the School DMS system. Refer to it whenever encountering unfamiliar terminology while implementing or maintaining the application.

**Key Takeaway:** Understanding these fundamentals ensures better code quality, security, and maintainability of the system.
