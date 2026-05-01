# COMPREHENSIVE GEMINI PROMPT FOR .NET CORE BACKEND GENERATION

Copy the entire prompt below and paste it in Gemini in VS Code:

---

## PROMPT START HERE:

I need you to generate a complete production-ready .NET Core 7.0+ Web API application with the following requirements. This is for a Document Management System (DMS) with a Field Engineer App and Ops Team verification system.

### PROJECT DETAILS:
- **Project Name:** SchoolDMS.API
- **Framework:** .NET Core 7.0+
- **Database:** SQL Server (Entity Framework Core)
- **Authentication:** JWT Token-Based
- **Architecture:** Clean Architecture with Repository Pattern, Dependency Injection, and Service Layer

### DATABASE SCHEMA:

Create complete database models for these tables:

1. **Users Table:**
   - UserId (PK, int, identity)
   - FirstName (string, required)
   - LastName (string, required)
   - Email (string, required, unique)
   - PasswordHash (string, required)
   - Phone (string)
   - IsActive (bool, default: true)
   - Role (enum: Engineer, OpsVerifier, Vendor, Admin) - FK to Role table
   - CreatedAt (DateTime)
   - UpdatedAt (DateTime)

2. **Roles Table:**
   - RoleId (PK, int)
   - RoleName (string: Engineer, OpsVerifier, Vendor, Admin)
   - Description (string)

3. **Schools Table:**
   - SchoolId (PK, int, identity)
   - UdiseCode (string, required, unique)
   - SchoolName (string, required)
   - District (string, required)
   - Block (string, required)
   - State (string)
   - Latitude (decimal)
   - Longitude (decimal)
   - Address (string)
   - ContactPerson (string)
   - ContactPhone (string)
   - CreatedAt (DateTime)

4. **Projects Table:**
   - ProjectId (PK, int, identity)
   - ProjectName (string, required - Smart Classroom, ISM, PM Shri 1, PM Shri 2, Language Lab, Vocational Lab)
   - Description (string)
   - Status (enum: Active, Inactive)

5. **Visits Table (Core Entity):**
   - VisitId (PK, int, identity)
   - SchoolId (FK to Schools, required)
   - EngineerId (FK to Users, required)
   - ProjectId (FK to Projects, required)
   - VisitType (enum: Installation_Demonstration, PMS, Service_Complaint, Performance_Certificate, Site_Inspection, Content_Training)
   - VisitDate (DateTime, required)
   - CheckInTime (DateTime)
   - CheckOutTime (DateTime)
   - GpsLatitude (decimal, required)
   - GpsLongitude (decimal)
   - IsGpsVerified (bool)
   - WorkCompleted (bool)
   - Notes (string)
   - Status (enum: Draft, Submitted, PendingVerification, Approved, Rejected)
   - CreatedAt (DateTime)
   - UpdatedAt (DateTime)
   - RejectionReason (string) - for rejected visits

6. **Documents Table (DMS Core):**
   - DocumentId (PK, int, identity)
   - VisitId (FK to Visits, required)
   - DocumentType (enum: BeforePhoto, AfterPhoto, SerialNumberImage, IR_Certificate, EngineersNotes)
   - IsMandatory (bool)
   - FileUrl (string - stored file path/blob URL)
   - FileName (string)
   - FileSize (long)
   - UploadedAt (DateTime)
   - UploadedBy (FK to Users)
   - DocumentStatus (enum: Pending, Uploaded, Verified, Rejected)

7. **ApprovalWorkflow Table:**
   - ApprovalId (PK, int, identity)
   - VisitId (FK to Visits, required)
   - VerifierId (FK to Users - OpsVerifier, required)
   - ApprovalStatus (enum: Approved, Rejected, Pending)
   - RejectionReasons (string - comma separated: WrongSchool, FakeVisit_GPSMismatch, BlurryPhotos, MissingDeviceProof, IncorrectSerialNumber, TrainingPending, Faulty_Damaged_Missing)
   - Comments (string)
   - ApprovedAt (DateTime)
   - CreatedAt (DateTime)

8. **DocumentSearch Table (for OCR/Indexing):**
   - SearchId (PK, int, identity)
   - DocumentId (FK to Documents)
   - ExtractedText (string - OCR text)
   - SearchIndex (string - full-text search index)
   - LastIndexedAt (DateTime)

9. **AuditLog Table:**
   - AuditId (PK, int, identity)
   - UserId (FK to Users)
   - Action (string - Created, Updated, Deleted, Approved, Rejected)
   - TableName (string)
   - RecordId (int)
   - OldValues (string - JSON)
   - NewValues (string - JSON)
   - CreatedAt (DateTime)

### REQUIRED FOLDER STRUCTURE:

```
SchoolDMS.API/
├── Controllers/
│   ├── AuthController.cs
│   ├── UsersController.cs
│   ├── SchoolsController.cs
│   ├── ProjectsController.cs
│   ├── VisitsController.cs
│   ├── DocumentsController.cs
│   └── ReportsController.cs
├── Models/
│   ├── Entities/
│   │   ├── User.cs
│   │   ├── Role.cs
│   │   ├── School.cs
│   │   ├── Project.cs
│   │   ├── Visit.cs
│   │   ├── Document.cs
│   │   ├── ApprovalWorkflow.cs
│   │   ├── DocumentSearch.cs
│   │   └── AuditLog.cs
│   ├── DTOs/
│   │   ├── Auth/
│   │   │   ├── LoginRequestDTO.cs
│   │   │   ├── LoginResponseDTO.cs
│   │   │   └── RegisterDTO.cs
│   │   ├── Users/
│   │   │   ├── UserDTO.cs
│   │   │   ├── CreateUserDTO.cs
│   │   │   └── UpdateUserDTO.cs
│   │   ├── Schools/
│   │   │   ├── SchoolDTO.cs
│   │   │   ├── CreateSchoolDTO.cs
│   │   │   └── UpdateSchoolDTO.cs
│   │   ├── Visits/
│   │   │   ├── VisitDTO.cs
│   │   │   ├── CreateVisitDTO.cs
│   │   │   ├── VisitListDTO.cs
│   │   │   └── VisitApprovalDTO.cs
│   │   ├── Documents/
│   │   │   ├── DocumentDTO.cs
│   │   │   └── DocumentUploadDTO.cs
│   │   └── Reports/
│   │       ├── DashboardDTO.cs
│   │       └── ExcelExportDTO.cs
│   ├── Enums/
│   │   ├── RoleEnum.cs
│   │   ├── VisitTypeEnum.cs
│   │   ├── VisitStatusEnum.cs
│   │   ├── DocumentTypeEnum.cs
│   │   └── ApprovalStatusEnum.cs
│   └── Responses/
│       ├── ApiResponse.cs
│       └── PaginatedResponse.cs
├── Services/
│   ├── Interfaces/
│   │   ├── IAuthService.cs
│   │   ├── IUserService.cs
│   │   ├── ISchoolService.cs
│   │   ├── IProjectService.cs
│   │   ├── IVisitService.cs
│   │   ├── IDocumentService.cs
│   │   ├── IApprovalService.cs
│   │   ├── IReportService.cs
│   │   ├── IAuditService.cs
│   │   └── ITokenService.cs
│   ├── AuthService.cs
│   ├── UserService.cs
│   ├── SchoolService.cs
│   ├── ProjectService.cs
│   ├── VisitService.cs
│   ├── DocumentService.cs
│   ├── ApprovalService.cs
│   ├── ReportService.cs
│   ├── AuditService.cs
│   └── TokenService.cs
├── Repositories/
│   ├── Interfaces/
│   │   ├── IRepository.cs
│   │   ├── IUserRepository.cs
│   │   ├── IVisitRepository.cs
│   │   ├── IDocumentRepository.cs
│   │   └── IApprovalRepository.cs
│   ├── GenericRepository.cs
│   ├── UserRepository.cs
│   ├── VisitRepository.cs
│   ├── DocumentRepository.cs
│   └── ApprovalRepository.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Middleware/
│   ├── ExceptionHandlingMiddleware.cs
│   ├── JwtMiddleware.cs
│   └── LoggingMiddleware.cs
├── Configuration/
│   ├── JwtSettings.cs
│   ├── FileUploadSettings.cs
│   └── AppSettings.cs
├── Helpers/
│   ├── JwtTokenHelper.cs
│   ├── PasswordHelper.cs
│   ├── GeoLocationHelper.cs
│   └── ValidationHelper.cs
├── Validators/
│   ├── UserValidator.cs
│   ├── SchoolValidator.cs
│   ├── VisitValidator.cs
│   └── DocumentValidator.cs
├── Extensions/
│   ├── ServiceCollectionExtensions.cs
│   ├── CorsPolicyExtensions.cs
│   └── FluentValidationExtensions.cs
├── Migrations/
│   └── InitialCreate.cs
├── appsettings.json
├── appsettings.Development.json
├── Program.cs
├── SchoolDMS.API.csproj
└── README.md
```

### REQUIRED API ENDPOINTS:

#### 1. AUTHENTICATION ENDPOINTS:
- POST /api/auth/register - Register new user
- POST /api/auth/login - Login (returns JWT token)
- POST /api/auth/refresh - Refresh token
- POST /api/auth/logout - Logout
- GET /api/auth/verify-token - Verify token validity

#### 2. USER MANAGEMENT ENDPOINTS:
- GET /api/users - List all users (Admin only)
- GET /api/users/{id} - Get user details
- POST /api/users - Create user (Admin only)
- PUT /api/users/{id} - Update user
- DELETE /api/users/{id} - Delete user (Admin only)
- GET /api/users/by-role/{role} - Get users by role (Admin/OpsVerifier)

#### 3. SCHOOL MANAGEMENT ENDPOINTS:
- GET /api/schools - List all schools (with pagination)
- GET /api/schools/{id} - Get school details
- GET /api/schools/search - Search schools by UDISE/name/district
- POST /api/schools - Create school (Admin only)
- PUT /api/schools/{id} - Update school
- DELETE /api/schools/{id} - Delete school (Admin only)

#### 4. PROJECT ENDPOINTS:
- GET /api/projects - List all projects
- GET /api/projects/{id} - Get project details
- POST /api/projects - Create project (Admin only)
- PUT /api/projects/{id} - Update project (Admin only)
- DELETE /api/projects/{id} - Delete project (Admin only)

#### 5. VISIT MANAGEMENT ENDPOINTS (Core):
- GET /api/visits - List visits (filters: userId, schoolId, status, date range) - Pagination
- GET /api/visits/{id} - Get visit details
- POST /api/visits - Create visit (Engineer)
- PUT /api/visits/{id} - Update visit (Engineer - only draft status)
- DELETE /api/visits/{id} - Delete visit (Engineer - only draft)
- GET /api/visits/by-engineer/{engineerId} - Engineer's visits
- GET /api/visits/pending-verification - Pending visits (OpsVerifier)
- PATCH /api/visits/{id}/check-in - GPS check-in (Engineer)
- PATCH /api/visits/{id}/check-out - Check-out (Engineer)
- PATCH /api/visits/{id}/submit - Submit visit for verification (Engineer)

#### 6. DOCUMENT ENDPOINTS:
- GET /api/documents - List documents
- GET /api/documents/{id} - Get document details
- POST /api/documents/upload - Upload document (Engineer) - Multipart form
- GET /api/documents/by-visit/{visitId} - Get documents by visit
- DELETE /api/documents/{id} - Delete document (Engineer - draft only)
- PATCH /api/documents/{id}/verify - Verify document (OpsVerifier)
- GET /api/documents/search - Full-text search documents

#### 7. APPROVAL/VERIFICATION ENDPOINTS:
- GET /api/approvals/pending - Pending approvals (OpsVerifier)
- GET /api/approvals/{visitId} - Get approval status
- POST /api/approvals/{visitId}/approve - Approve visit (OpsVerifier)
- POST /api/approvals/{visitId}/reject - Reject visit with reasons (OpsVerifier)
- GET /api/approvals/history/{visitId} - Approval history

#### 8. REPORTING ENDPOINTS:
- GET /api/reports/dashboard - Dashboard metrics (OpsVerifier)
  - Total visits
  - Pending verification count
  - Completed visits count
  - Repeat visits count
- GET /api/reports/export-excel - Export to Excel (OpsVerifier)
  - Filters: date range, status, district, school
  - Returns downloadable Excel file
- GET /api/reports/generate-pdf/{visitId} - Generate PDF for single visit
- GET /api/reports/generate-merged-pdf - Generate merged PDF (multiple visits)
  - Query params: visitIds (comma separated) or filters
  - Returns single merged PDF

#### 9. AUDIT LOG ENDPOINTS:
- GET /api/audit-logs - List audit logs (Admin only)
- GET /api/audit-logs/by-user/{userId} - Audit logs by user
- GET /api/audit-logs/by-record - Audit logs for specific record

### AUTHENTICATION & AUTHORIZATION REQUIREMENTS:

1. **JWT Token Implementation:**
   - Token claims: UserId, Email, Role, IssuedAt, ExpiresAt
   - Refresh token mechanism
   - Token expiration: 1 hour access, 7 days refresh
   - Issuer & Audience validation

2. **Role-Based Access Control (RBAC):**
   - Engineer: Create/update own visits, upload documents, view own data
   - OpsVerifier: View all visits, approve/reject, generate reports
   - Vendor: View-only access to their equipment installations
   - Admin: Full access to all resources, user management

3. **Custom Authorization Attributes:**
   - [Authorize] - Authenticated users only
   - [Authorize(Roles = "Engineer")] - Engineer only
   - [Authorize(Roles = "OpsVerifier")] - OpsVerifier only
   - [Authorize(Roles = "Admin")] - Admin only

4. **Custom Claims/Policies:**
   - CanApproveVisits - OpsVerifier only
   - CanManageUsers - Admin only
   - CanViewOwnDataOnly - Engineers can only see their own visits

### PROGRAM.CS CONFIGURATION:

Include the following in Program.cs:
1. Database context registration with SQL Server
2. Entity Framework Core configuration
3. JWT authentication setup with custom scheme
4. CORS policy configuration (localhost:4200 for Angular)
5. Dependency injection for all services and repositories
6. AutoMapper configuration
7. FluentValidation registration
8. Exception handling middleware registration
9. Custom logging/Serilog setup
10. Swagger/OpenAPI setup with JWT authentication
11. Health checks endpoint

### CRITICAL IMPLEMENTATION REQUIREMENTS:

1. **Checkpoints in Visit Submission Flow:**
   - ✅ Mandatory fields validation (GPS, school signboard photo)
   - ✅ Document category validation (each doc must go to correct category)
   - ✅ All mandatory documents must be uploaded before submission
   - ✅ Document quality checks (file size, format, not blurry)
   - ✅ GPS location validation against school coordinates (allow 500m tolerance)
   - ✅ Visit status transitions validated (Draft → Submitted → Approved/Rejected)
   - ✅ Timestamp validation (check-in before check-out)

2. **Approval Workflow Validations:**
   - ✅ Only OpsVerifier can approve/reject
   - ✅ Cannot approve own visits
   - ✅ Rejection must include reasons
   - ✅ Approved visits locked from further edits
   - ✅ Rejected visits revert to Draft with feedback for engineer

3. **Security Checkpoints:**
   - ✅ All endpoints require authentication except register/login
   - ✅ Engineers can only view/edit their own visits
   - ✅ All data modifications logged to AuditLog table
   - ✅ File uploads validated (type, size, virus scan if possible)
   - ✅ GPS coordinates cannot be manually edited (read-only from check-in)
   - ✅ Sensitive data (passwords) never logged

4. **Business Logic Checkpoints:**
   - ✅ Cannot create visit for past dates
   - ✅ Cannot delete submitted/approved visits
   - ✅ Cannot reject same visit twice without re-submission
   - ✅ Engineer can only check-in within 5km of actual school location
   - ✅ Repeat visits tracked (same school visited multiple times)

5. **Performance Considerations:**
   - ✅ Pagination on all list endpoints (default 20, max 100)
   - ✅ Database indexes on frequently queried fields (UserId, SchoolId, VisitId)
   - ✅ Document file compression before storage
   - ✅ Cached dashboard metrics (refresh every 5 minutes)
   - ✅ Async/await for all I/O operations

### ERROR HANDLING:

Implement comprehensive error handling with HTTP status codes:
- 200 OK - Successful GET, PUT
- 201 Created - Successful POST
- 204 No Content - Successful DELETE
- 400 Bad Request - Validation errors
- 401 Unauthorized - Missing/invalid token
- 403 Forbidden - Insufficient permissions
- 404 Not Found - Resource not found
- 409 Conflict - Duplicate data
- 500 Internal Server Error - Unhandled exceptions

Return standardized error response format:
```json
{
  "success": false,
  "message": "Error message",
  "statusCode": 400,
  "errors": {
    "fieldName": ["error message"]
  }
}
```

### RESPONSE FORMAT:

All API responses should follow this format:
```json
{
  "success": true,
  "message": "Operation successful",
  "data": {},
  "statusCode": 200
}
```

For paginated responses:
```json
{
  "success": true,
  "message": "Fetch successful",
  "data": {
    "items": [],
    "pageNumber": 1,
    "pageSize": 20,
    "totalRecords": 100,
    "totalPages": 5
  },
  "statusCode": 200
}
```

### NuGET PACKAGES REQUIRED:

- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.AspNetCore.Authentication.JwtBearer
- System.IdentityModel.Tokens.Jwt
- AutoMapper.Extensions.Microsoft.DependencyInjection
- FluentValidation.DependencyInjectionExtensions
- Serilog.AspNetCore
- Swashbuckle.AspNetCore
- EPPlus (for Excel export)
- iTextSharp or SelectPdf (for PDF generation)
- BCrypt.Net-Next (for password hashing)

### APPSETTINGS.JSON STRUCTURE:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=SchoolDMS;Trusted_Connection=true;"
  },
  "Jwt": {
    "SecretKey": "YOUR_SUPER_SECRET_KEY_AT_LEAST_32_CHARACTERS",
    "Issuer": "SchoolDMS.API",
    "Audience": "SchoolDMS.Angular",
    "ExpirationMinutes": 60,
    "RefreshTokenExpirationDays": 7
  },
  "FileUpload": {
    "MaxFileSize": 10485760,
    "AllowedExtensions": [".jpg", ".jpeg", ".png", ".pdf"],
    "StoragePath": "uploads/"
  },
  "Cors": {
    "AllowedOrigins": ["http://localhost:4200"],
    "AllowedMethods": ["GET", "POST", "PUT", "DELETE", "PATCH"],
    "AllowedHeaders": ["*"]
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### ADDITIONAL REQUIREMENTS:

1. **Database Seeding:**
   - Create admin user during migration
   - Pre-populate roles
   - Add sample schools/projects (optional)

2. **Email Notifications (Future):**
   - Prepare service structure for email notifications
   - Plan: Engineer gets notification when visit rejected
   - Plan: Ops gets notification when visit submitted

3. **Logging & Monitoring:**
   - Log all API requests with timestamp, user, endpoint, response status
   - Log all data modifications
   - Exception logging with stack traces
   - Audit trail for compliance

4. **Validation:**
   - Client-side validation messages should be included in responses
   - Custom FluentValidation rules for complex scenarios
   - GPS coordinate validation (valid lat/long format)
   - File upload validation (type, size, extension)

5. **Testing Preparation:**
   - Structure services for unit testing
   - Use interfaces for dependency injection
   - Prepare mock repositories for testing

### GENERATE:

Please generate:
1. All entity models with proper relationships and annotations
2. All DTOs with data annotations
3. ApplicationDbContext with proper configuration
4. All repository interfaces and implementations
5. All service interfaces and implementations
6. Complete Program.cs with all configurations
7. All controller classes with endpoint implementations
8. Custom middleware classes
9. Helper classes for JWT, passwords, validation
10. FluentValidation validators for all DTOs
11. Extension methods for service registration
12. Configuration classes
13. Complete appsettings.json
14. Complete project structure

### IMPORTANT NOTES:

- Use async/await throughout
- Implement null checks and proper error handling
- Add XML comments to all public methods
- Follow C# coding standards and conventions
- Use dependency injection everywhere
- Implement proper logging
- Make all queries case-insensitive for search
- Use transactions for multi-step operations (visit submission + document upload)
- Implement soft deletes for sensitive entities
- Add created/updated timestamps to all entities
- Use enums for all status fields (not strings)

---

## PROMPT END

---

## INSTRUCTIONS FOR USING THIS PROMPT:

1. **Open Gemini in VS Code** (Using Google's Gemini API or Gemini Chat)

2. **Copy the entire prompt above** (from "PROMPT START HERE" to "PROMPT END")

3. **Paste it in Gemini chat**

4. **Gemini will generate:**
   - All C# code files
   - Database models
   - Services and repositories
   - Controllers with all endpoints
   - DTOs and request/response models
   - Middleware and helpers
   - Complete Program.cs configuration
   - appsettings.json

5. **After generation:**
   - Create the folder structure as specified
   - Copy generated files to appropriate folders
   - Run migrations: `dotnet ef database update`
   - Start the API: `dotnet run`
   - API will run on: https://localhost:5001 (or http://localhost:5000)

6. **Next Steps:**
   - Test with Swagger UI at: http://localhost:5000/swagger
   - Register user, get JWT token, test endpoints
   - Build Angular frontend to consume these APIs
   - Implement file upload service for documents
