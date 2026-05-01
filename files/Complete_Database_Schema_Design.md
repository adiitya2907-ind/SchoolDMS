# COMPLETE DATABASE SCHEMA DESIGN - School DMS System

## DATABASE OVERVIEW

**Database Name:** SchoolDMS
**DBMS:** SQL Server
**Approach:** Relational with proper normalization
**Backup Strategy:** Daily full backups + transaction logs

---

## COMPLETE DATABASE DIAGRAM (ASCII Representation)

```
┌─────────────────┐         ┌──────────────┐         ┌──────────────┐
│   Users         │◄────────│    Roles     │         │   Schools    │
│                 │         │              │         │              │
│ UserId (PK)     │         │ RoleId (PK)  │         │ SchoolId(PK) │
│ FirstName       │         │ RoleName     │         │ UdiseCode    │
│ LastName        │         │ Description  │         │ SchoolName   │
│ Email (UQ)      │         └──────────────┘         │ District     │
│ PasswordHash    │                                   │ Block        │
│ Phone           │                                   │ State        │
│ RoleId (FK)     │                                   │ Latitude     │
│ IsActive        │                                   │ Longitude    │
│ CreatedAt       │                                   │ Address      │
│ UpdatedAt       │                                   │ ContactPhone │
└─────────────────┘                                   └──────────────┘
        │                                                     ▲
        │                                                     │
        │                    ┌────────────────────────────────┘
        │                    │
        │                    │     ┌─────────────────┐
        │                    │     │   Projects      │
        │                    │     │                 │
        │                    │     │ ProjectId (PK)  │
        │                    │     │ ProjectName     │
        │                    │     │ Description     │
        │                    │     │ Status          │
        │                    │     └─────────────────┘
        │                    │             ▲
        │                    │             │
        ▼                    ▼             │
   ┌──────────────────────────────────────────────┐
   │           Visits (Core Entity)               │
   │                                              │
   │ VisitId (PK)                                │
   │ SchoolId (FK)      ────────────────────────→│ Schools
   │ EngineerId (FK)    ────────────────────────→│ Users (Engineer)
   │ ProjectId (FK)     ────────────────────────→│ Projects
   │ VisitType                                   │
   │ VisitDate                                   │
   │ CheckInTime                                 │
   │ CheckOutTime                                │
   │ GpsLatitude                                 │
   │ GpsLongitude                                │
   │ IsGpsVerified                               │
   │ WorkCompleted                               │
   │ Notes                                       │
   │ Status                                      │
   │ CreatedAt                                   │
   │ UpdatedAt                                   │
   │ RejectionReason                             │
   └──────────────────────────────────────────────┘
        │
        │
        ├─────────────────────────────┬──────────────────────────┐
        │                             │                          │
        ▼                             ▼                          ▼
   ┌──────────────┐         ┌────────────────────┐    ┌──────────────────┐
   │  Documents   │         │ ApprovalWorkflow   │    │  DocumentSearch  │
   │              │         │                    │    │                  │
   │ DocumentId   │         │ ApprovalId (PK)   │    │ SearchId (PK)    │
   │ VisitId (FK) │────────→│ VisitId (FK)      │    │ DocumentId (FK)  │
   │ DocumentType │         │ VerifierId (FK)   │    │ ExtractedText    │
   │ IsMandatory  │         │ ApprovalStatus    │    │ SearchIndex      │
   │ FileUrl      │         │ RejectionReasons  │    │ LastIndexedAt    │
   │ FileName     │         │ Comments          │    └──────────────────┘
   │ FileSize     │         │ ApprovedAt        │
   │ UploadedAt   │         │ CreatedAt         │
   │ UploadedBy   │────────→│ VerifierId (FK)   │
   │ DocumentStatus│        └────────────────────┘
   └──────────────┘                ▲
        │                          │
        └──────────────────────────┘
   (VerifierId is FK to Users - OpsVerifier role)

   ┌──────────────────────┐
   │    AuditLog          │
   │                      │
   │ AuditId (PK)         │
   │ UserId (FK)         │─────────→ Users
   │ Action               │
   │ TableName            │
   │ RecordId             │
   │ OldValues (JSON)     │
   │ NewValues (JSON)     │
   │ CreatedAt            │
   └──────────────────────┘
```

---

## TABLE DEFINITIONS WITH SQL SCRIPTS

### 1. ROLES TABLE

```sql
-- Drop if exists (for development)
IF OBJECT_ID('dbo.Roles', 'U') IS NOT NULL
  DROP TABLE dbo.Roles;

CREATE TABLE Roles (
    RoleId INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(255),
    CreatedAt DATETIME DEFAULT GETUTCDATE()
);

-- INSERT default roles
INSERT INTO Roles (RoleName, Description) VALUES
('Engineer', 'Field engineer who performs installation and visits'),
('OpsVerifier', 'Operations team who verifies and approves visits'),
('Vendor', 'External vendor with read-only access'),
('Admin', 'System administrator with full access');

-- Create index
CREATE INDEX IX_Roles_RoleName ON Roles(RoleName);
```

---

### 2. USERS TABLE

```sql
-- Drop if exists
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL
  DROP TABLE dbo.Users;

CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(256) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    Phone NVARCHAR(20),
    RoleId INT NOT NULL,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    LastLoginAt DATETIME NULL,
    
    CONSTRAINT FK_Users_Roles 
        FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
        ON DELETE RESTRICT
);

-- Indexes
CREATE INDEX IX_Users_Email ON Users(Email);
CREATE INDEX IX_Users_RoleId ON Users(RoleId);
CREATE INDEX IX_Users_IsActive ON Users(IsActive);

-- Create default admin user (password: Admin@123 hashed)
-- Note: Password hash should be generated using BCrypt.Net
INSERT INTO Users (FirstName, LastName, Email, PasswordHash, RoleId, IsActive)
VALUES ('System', 'Admin', 'admin@schooldms.com', '$2a$11$ZIH/p4..', 4, 1);
```

---

### 3. SCHOOLS TABLE

```sql
-- Drop if exists
IF OBJECT_ID('dbo.Schools', 'U') IS NOT NULL
  DROP TABLE dbo.Schools;

CREATE TABLE Schools (
    SchoolId INT PRIMARY KEY IDENTITY(1,1),
    UdiseCode NVARCHAR(20) NOT NULL UNIQUE,
    SchoolName NVARCHAR(255) NOT NULL,
    District NVARCHAR(100) NOT NULL,
    Block NVARCHAR(100) NOT NULL,
    State NVARCHAR(100),
    Latitude DECIMAL(10, 8),
    Longitude DECIMAL(11, 8),
    Address NVARCHAR(500),
    ContactPerson NVARCHAR(150),
    ContactPhone NVARCHAR(20),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    
    -- Constraints
    CHECK (SchoolName != '' AND UdiseCode != '')
);

-- Indexes (frequently searched fields)
CREATE INDEX IX_Schools_UdiseCode ON Schools(UdiseCode);
CREATE INDEX IX_Schools_District ON Schools(District);
CREATE INDEX IX_Schools_Block ON Schools(Block);
CREATE INDEX IX_Schools_SchoolName ON Schools(SchoolName);
CREATE SPATIAL INDEX IX_Schools_Location 
    ON Schools(GEOMETRY::STGeomFromText('POINT(' + CAST(Longitude AS VARCHAR(20)) + ' ' + CAST(Latitude AS VARCHAR(20)) + ')', 4326));

-- Sample data
INSERT INTO Schools (UdiseCode, SchoolName, District, Block, State, Latitude, Longitude, Address, ContactPhone)
VALUES 
    ('UP000001', 'Delhi Public School', 'Noida', 'Noida', 'Uttar Pradesh', 28.5935, 77.3910, '123 Main Street', '9876543210'),
    ('UP000002', 'St. Xavier High School', 'Agra', 'Agra', 'Uttar Pradesh', 27.1767, 78.0081, '456 Oak Lane', '9876543211');
```

---

### 4. PROJECTS TABLE

```sql
-- Drop if exists
IF OBJECT_ID('dbo.Projects', 'U') IS NOT NULL
  DROP TABLE dbo.Projects;

CREATE TABLE Projects (
    ProjectId INT PRIMARY KEY IDENTITY(1,1),
    ProjectName NVARCHAR(150) NOT NULL UNIQUE,
    Description NVARCHAR(500),
    Status NVARCHAR(20) DEFAULT 'Active', -- Active, Inactive
    CreatedAt DATETIME DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    
    CHECK (Status IN ('Active', 'Inactive'))
);

-- Indexes
CREATE INDEX IX_Projects_ProjectName ON Projects(ProjectName);
CREATE INDEX IX_Projects_Status ON Projects(Status);

-- Insert default projects
INSERT INTO Projects (ProjectName, Description, Status) VALUES
('Smart Classroom', 'Interactive smart classroom setup with digital boards', 'Active'),
('ISM', 'Interactive Smart Module installation', 'Active'),
('PM Shri 1', 'Prime Minister Schools for Rising India - Phase 1', 'Active'),
('PM Shri 2', 'Prime Minister Schools for Rising India - Phase 2', 'Active'),
('Language Lab', 'Language learning laboratory with audio equipment', 'Active'),
('Vocational Lab', 'Vocational training laboratory setup', 'Active');
```

---

### 5. VISITS TABLE (Core Entity)

```sql
-- Drop if exists
IF OBJECT_ID('dbo.Visits', 'U') IS NOT NULL
  DROP TABLE dbo.Visits;

CREATE TABLE Visits (
    VisitId INT PRIMARY KEY IDENTITY(1,1),
    SchoolId INT NOT NULL,
    EngineerId INT NOT NULL,
    ProjectId INT NOT NULL,
    VisitType NVARCHAR(50) NOT NULL, -- Installation_Demonstration, PMS, Service_Complaint, Performance_Certificate, Site_Inspection, Content_Training
    VisitDate DATE NOT NULL,
    CheckInTime DATETIME,
    CheckOutTime DATETIME,
    GpsLatitude DECIMAL(10, 8),
    GpsLongitude DECIMAL(11, 8),
    IsGpsVerified BIT DEFAULT 0,
    WorkCompleted BIT DEFAULT 0,
    Notes NVARCHAR(MAX),
    Status NVARCHAR(50) DEFAULT 'Draft', -- Draft, Submitted, PendingVerification, Approved, Rejected
    RejectionReason NVARCHAR(500),
    CreatedAt DATETIME DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    
    -- Foreign Keys
    CONSTRAINT FK_Visits_Schools 
        FOREIGN KEY (SchoolId) REFERENCES Schools(SchoolId),
    CONSTRAINT FK_Visits_Users_Engineer 
        FOREIGN KEY (EngineerId) REFERENCES Users(UserId),
    CONSTRAINT FK_Visits_Projects 
        FOREIGN KEY (ProjectId) REFERENCES Projects(ProjectId),
    
    -- Constraints
    CHECK (VisitType IN ('Installation_Demonstration', 'PMS', 'Service_Complaint', 'Performance_Certificate', 'Site_Inspection', 'Content_Training')),
    CHECK (Status IN ('Draft', 'Submitted', 'PendingVerification', 'Approved', 'Rejected')),
    CHECK (CheckInTime IS NULL OR CheckOutTime IS NULL OR CheckInTime <= CheckOutTime)
);

-- Indexes (heavily queried)
CREATE INDEX IX_Visits_SchoolId ON Visits(SchoolId);
CREATE INDEX IX_Visits_EngineerId ON Visits(EngineerId);
CREATE INDEX IX_Visits_Status ON Visits(Status);
CREATE INDEX IX_Visits_VisitDate ON Visits(VisitDate);
CREATE INDEX IX_Visits_EngineerId_Status ON Visits(EngineerId, Status);
CREATE INDEX IX_Visits_Status_VisitDate ON Visits(Status, VisitDate);

-- Spatial index for GPS coordinates
CREATE SPATIAL INDEX IX_Visits_GpsLocation 
    ON Visits(GEOMETRY::STGeomFromText('POINT(' + CAST(GpsLongitude AS VARCHAR(20)) + ' ' + CAST(GpsLatitude AS VARCHAR(20)) + ')', 4326))
    WHERE GpsLatitude IS NOT NULL AND GpsLongitude IS NOT NULL;
```

---

### 6. DOCUMENTS TABLE (DMS Core)

```sql
-- Drop if exists
IF OBJECT_ID('dbo.Documents', 'U') IS NOT NULL
  DROP TABLE dbo.Documents;

CREATE TABLE Documents (
    DocumentId INT PRIMARY KEY IDENTITY(1,1),
    VisitId INT NOT NULL,
    DocumentType NVARCHAR(50) NOT NULL, -- BeforePhoto, AfterPhoto, SerialNumberImage, IR_Certificate, EngineersNotes
    IsMandatory BIT NOT NULL,
    FileUrl NVARCHAR(MAX) NOT NULL,
    FileName NVARCHAR(255) NOT NULL,
    FileSize BIGINT,
    FileExtension NVARCHAR(10),
    UploadedAt DATETIME DEFAULT GETUTCDATE(),
    UploadedBy INT NOT NULL,
    DocumentStatus NVARCHAR(50) DEFAULT 'Uploaded', -- Pending, Uploaded, Verified, Rejected
    VerifiedAt DATETIME NULL,
    VerifiedBy INT NULL,
    IsDeleted BIT DEFAULT 0,
    
    -- Foreign Keys
    CONSTRAINT FK_Documents_Visits 
        FOREIGN KEY (VisitId) REFERENCES Visits(VisitId) ON DELETE CASCADE,
    CONSTRAINT FK_Documents_Users_UploadedBy 
        FOREIGN KEY (UploadedBy) REFERENCES Users(UserId),
    CONSTRAINT FK_Documents_Users_VerifiedBy 
        FOREIGN KEY (VerifiedBy) REFERENCES Users(UserId),
    
    -- Constraints
    CHECK (DocumentType IN ('BeforePhoto', 'AfterPhoto', 'SerialNumberImage', 'IR_Certificate', 'EngineersNotes')),
    CHECK (DocumentStatus IN ('Pending', 'Uploaded', 'Verified', 'Rejected')),
    CHECK (FileSize > 0)
);

-- Indexes
CREATE INDEX IX_Documents_VisitId ON Documents(VisitId);
CREATE INDEX IX_Documents_DocumentType ON Documents(DocumentType);
CREATE INDEX IX_Documents_DocumentStatus ON Documents(DocumentStatus);
CREATE INDEX IX_Documents_UploadedBy ON Documents(UploadedBy);
CREATE INDEX IX_Documents_IsDeleted ON Documents(IsDeleted);
```

---

### 7. APPROVAL WORKFLOW TABLE

```sql
-- Drop if exists
IF OBJECT_ID('dbo.ApprovalWorkflow', 'U') IS NOT NULL
  DROP TABLE dbo.ApprovalWorkflow;

CREATE TABLE ApprovalWorkflow (
    ApprovalId INT PRIMARY KEY IDENTITY(1,1),
    VisitId INT NOT NULL UNIQUE,
    VerifierId INT NOT NULL,
    ApprovalStatus NVARCHAR(50) DEFAULT 'Pending', -- Pending, Approved, Rejected
    RejectionReasons NVARCHAR(MAX), -- Comma separated or JSON array
    Comments NVARCHAR(MAX),
    ApprovedAt DATETIME NULL,
    CreatedAt DATETIME DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    
    -- Foreign Keys
    CONSTRAINT FK_ApprovalWorkflow_Visits 
        FOREIGN KEY (VisitId) REFERENCES Visits(VisitId) ON DELETE CASCADE,
    CONSTRAINT FK_ApprovalWorkflow_Users_Verifier 
        FOREIGN KEY (VerifierId) REFERENCES Users(UserId),
    
    -- Constraints
    CHECK (ApprovalStatus IN ('Pending', 'Approved', 'Rejected')),
    CHECK (ApprovalStatus = 'Pending' OR ApprovedAt IS NOT NULL)
);

-- Indexes
CREATE INDEX IX_ApprovalWorkflow_VisitId ON ApprovalWorkflow(VisitId);
CREATE INDEX IX_ApprovalWorkflow_VerifierId ON ApprovalWorkflow(VerifierId);
CREATE INDEX IX_ApprovalWorkflow_ApprovalStatus ON ApprovalWorkflow(ApprovalStatus);
```

---

### 8. DOCUMENT SEARCH TABLE (For OCR & Indexing)

```sql
-- Drop if exists
IF OBJECT_ID('dbo.DocumentSearch', 'U') IS NOT NULL
  DROP TABLE dbo.DocumentSearch;

CREATE TABLE DocumentSearch (
    SearchId INT PRIMARY KEY IDENTITY(1,1),
    DocumentId INT NOT NULL UNIQUE,
    ExtractedText NVARCHAR(MAX),
    SearchIndex NVARCHAR(MAX), -- Full-text search index
    LastIndexedAt DATETIME DEFAULT GETUTCDATE(),
    
    -- Foreign Key
    CONSTRAINT FK_DocumentSearch_Documents 
        FOREIGN KEY (DocumentId) REFERENCES Documents(DocumentId) ON DELETE CASCADE
);

-- Create full-text search index
CREATE FULLTEXT CATALOG ftCatalogDMS;
CREATE FULLTEXT INDEX ON DocumentSearch(ExtractedText, SearchIndex) 
KEY INDEX PK_DocumentSearch 
ON ftCatalogDMS;

-- Indexes
CREATE INDEX IX_DocumentSearch_DocumentId ON DocumentSearch(DocumentId);
```

---

### 9. AUDIT LOG TABLE

```sql
-- Drop if exists
IF OBJECT_ID('dbo.AuditLog', 'U') IS NOT NULL
  DROP TABLE dbo.AuditLog;

CREATE TABLE AuditLog (
    AuditId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    Action NVARCHAR(50) NOT NULL, -- Created, Updated, Deleted, Approved, Rejected, Downloaded
    TableName NVARCHAR(100) NOT NULL,
    RecordId INT NOT NULL,
    OldValues NVARCHAR(MAX), -- JSON format
    NewValues NVARCHAR(MAX), -- JSON format
    IPAddress NVARCHAR(45),
    UserAgent NVARCHAR(500),
    CreatedAt DATETIME DEFAULT GETUTCDATE(),
    
    -- Foreign Key
    CONSTRAINT FK_AuditLog_Users 
        FOREIGN KEY (UserId) REFERENCES Users(UserId),
    
    -- Constraints
    CHECK (Action IN ('Created', 'Updated', 'Deleted', 'Approved', 'Rejected', 'Downloaded', 'LoggedIn', 'LoggedOut'))
);

-- Indexes
CREATE INDEX IX_AuditLog_UserId ON AuditLog(UserId);
CREATE INDEX IX_AuditLog_TableName ON AuditLog(TableName);
CREATE INDEX IX_AuditLog_CreatedAt ON AuditLog(CreatedAt DESC);
CREATE INDEX IX_AuditLog_UserId_CreatedAt ON AuditLog(UserId, CreatedAt DESC);
```

---

## RELATIONSHIPS & FOREIGN KEY CONSTRAINTS

```
Roles (1) ─────────────── (Many) Users
                            │
                            │
                            ├─ (1 Engineer to Many Visits)
                            │   └─ Visits ─── (1 to Many) Documents
                            │       └─ (1 to Many) ApprovalWorkflow
                            │
                            └─ (1 OpsVerifier to Many ApprovalWorkflow)

Schools (1) ─────────────── (Many) Visits

Projects (1) ─────────────── (Many) Visits

Documents (1) ─────────────── (1) DocumentSearch

Users (1) ─────────────── (Many) AuditLog
```

---

## INDEXES SUMMARY

### Clustered Indexes (Primary Keys):
- Roles.RoleId
- Users.UserId
- Schools.SchoolId
- Projects.ProjectId
- Visits.VisitId
- Documents.DocumentId
- ApprovalWorkflow.ApprovalId
- DocumentSearch.SearchId
- AuditLog.AuditId

### Non-Clustered Indexes:

| Table | Column(s) | Purpose |
|-------|-----------|---------|
| Users | Email | Login lookup |
| Users | RoleId | Role-based filtering |
| Users | IsActive | Active users only |
| Schools | UdiseCode | Quick school lookup |
| Schools | District, Block | Geographic filtering |
| Schools | SchoolName | Name-based search |
| Visits | SchoolId | School-specific visits |
| Visits | EngineerId | Engineer's visits |
| Visits | Status | Status filtering |
| Visits | VisitDate | Date range queries |
| Visits | EngineerId + Status | Engineer's pending visits |
| Documents | VisitId | Documents per visit |
| Documents | DocumentType | Type-based retrieval |
| Documents | DocumentStatus | Status filtering |
| ApprovalWorkflow | VisitId | Visit approval lookup |
| ApprovalWorkflow | VerifierId | Verifier's approvals |
| ApprovalWorkflow | ApprovalStatus | Pending approvals |
| AuditLog | UserId | User activity |
| AuditLog | CreatedAt | Time-based queries |
| AuditLog | UserId + CreatedAt | User activity timeline |

---

## SPATIAL INDEXES (For GPS)

```sql
-- Schools location index
CREATE SPATIAL INDEX IX_Schools_Location 
    ON Schools(GEOMETRY::STGeomFromText('POINT(' + CAST(Longitude AS VARCHAR(20)) + ' ' + CAST(Latitude AS VARCHAR(20)) + ')', 4326));

-- Visits location index
CREATE SPATIAL INDEX IX_Visits_GpsLocation 
    ON Visits(GEOMETRY::STGeomFromText('POINT(' + CAST(GpsLongitude AS VARCHAR(20)) + ' ' + CAST(GpsLatitude AS VARCHAR(20)) + ')', 4326))
    WHERE GpsLatitude IS NOT NULL AND GpsLongitude IS NOT NULL;

-- Query example: Find schools within 5km of GPS coordinates
SELECT *
FROM Schools
WHERE GEOMETRY::STGeomFromText('POINT(28.5935 77.3910)', 4326)
    .STDistance(GEOMETRY::STGeomFromText('POINT(' + CAST(Longitude AS VARCHAR(20)) + ' ' + CAST(Latitude AS VARCHAR(20)) + ')', 4326)) <= 5000; -- 5000 meters
```

---

## FULL-TEXT SEARCH INDEXES

```sql
-- Document search index (for OCR text)
CREATE FULLTEXT CATALOG ftCatalogDMS;
CREATE FULLTEXT INDEX ON DocumentSearch(ExtractedText, SearchIndex) 
KEY INDEX PK_DocumentSearch 
ON ftCatalogDMS;

-- Query example: Search documents by text
SELECT *
FROM DocumentSearch
WHERE CONTAINS(ExtractedText, 'Smart Classroom');
```

---

## VIEWS FOR COMMON QUERIES

### View 1: Pending Verification Visits

```sql
CREATE VIEW vw_PendingVerification AS
SELECT 
    v.VisitId,
    v.VisitDate,
    s.SchoolName,
    s.UdiseCode,
    s.District,
    u.FirstName + ' ' + u.LastName AS EngineerName,
    p.ProjectName,
    v.VisitType,
    v.Status,
    (SELECT COUNT(*) FROM Documents WHERE VisitId = v.VisitId) AS DocumentCount
FROM Visits v
INNER JOIN Schools s ON v.SchoolId = s.SchoolId
INNER JOIN Users u ON v.EngineerId = u.UserId
INNER JOIN Projects p ON v.ProjectId = p.ProjectId
WHERE v.Status = 'Submitted';
```

### View 2: Engineer Visit Summary

```sql
CREATE VIEW vw_EngineerVisitSummary AS
SELECT 
    u.UserId,
    u.FirstName + ' ' + u.LastName AS EngineerName,
    COUNT(CASE WHEN v.Status = 'Approved' THEN 1 END) AS ApprovedVisits,
    COUNT(CASE WHEN v.Status = 'Rejected' THEN 1 END) AS RejectedVisits,
    COUNT(CASE WHEN v.Status = 'Submitted' THEN 1 END) AS PendingVisits,
    COUNT(*) AS TotalVisits
FROM Users u
LEFT JOIN Visits v ON u.UserId = v.EngineerId
WHERE u.RoleId = (SELECT RoleId FROM Roles WHERE RoleName = 'Engineer')
GROUP BY u.UserId, u.FirstName, u.LastName;
```

### View 3: School Visit Frequency

```sql
CREATE VIEW vw_SchoolVisitFrequency AS
SELECT 
    s.SchoolId,
    s.UdiseCode,
    s.SchoolName,
    s.District,
    COUNT(*) AS VisitCount,
    COUNT(DISTINCT v.EngineerId) AS UniquEngineers,
    MAX(v.VisitDate) AS LastVisitDate
FROM Schools s
LEFT JOIN Visits v ON s.SchoolId = v.SchoolId
GROUP BY s.SchoolId, s.UdiseCode, s.SchoolName, s.District;
```

---

## TRANSACTION HANDLING

### Complex Operation: Submit Visit with Documents

```sql
BEGIN TRANSACTION;

BEGIN TRY
    -- 1. Update visit status
    UPDATE Visits 
    SET Status = 'Submitted', UpdatedAt = GETUTCDATE()
    WHERE VisitId = @VisitId;
    
    -- 2. Create approval workflow record
    INSERT INTO ApprovalWorkflow (VisitId, VerifierId, ApprovalStatus, CreatedAt)
    VALUES (@VisitId, NULL, 'Pending', GETUTCDATE());
    
    -- 3. Log audit
    INSERT INTO AuditLog (UserId, Action, TableName, RecordId, NewValues, CreatedAt)
    VALUES (@UserId, 'Submitted', 'Visits', @VisitId, JSON_OBJECT(...), GETUTCDATE());
    
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    THROW;
END CATCH;
```

---

## STORED PROCEDURES (For Complex Operations)

### Stored Procedure 1: Get Dashboard Metrics

```sql
CREATE PROCEDURE sp_GetDashboardMetrics
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT 
        'TotalVisits' AS MetricName,
        COUNT(*) AS MetricValue
    FROM Visits
    WHERE CAST(VisitDate AS DATE) BETWEEN @StartDate AND @EndDate
    
    UNION ALL
    
    SELECT 
        'PendingVerification' AS MetricName,
        COUNT(*) AS MetricValue
    FROM Visits
    WHERE Status = 'Submitted' AND CAST(VisitDate AS DATE) BETWEEN @StartDate AND @EndDate
    
    UNION ALL
    
    SELECT 
        'ApprovedVisits' AS MetricName,
        COUNT(*) AS MetricValue
    FROM Visits
    WHERE Status = 'Approved' AND CAST(VisitDate AS DATE) BETWEEN @StartDate AND @EndDate
    
    UNION ALL
    
    SELECT 
        'RejectedVisits' AS MetricName,
        COUNT(*) AS MetricValue
    FROM Visits
    WHERE Status = 'Rejected' AND CAST(VisitDate AS DATE) BETWEEN @StartDate AND @EndDate
    
    UNION ALL
    
    SELECT 
        'RepeatVisits' AS MetricName,
        COUNT(*) AS MetricValue
    FROM (
        SELECT SchoolId
        FROM Visits
        WHERE CAST(VisitDate AS DATE) BETWEEN @StartDate AND @EndDate
        GROUP BY SchoolId
        HAVING COUNT(*) > 1
    ) AS RepeatSchools;
END;
```

### Stored Procedure 2: Validate Visit Completion

```sql
CREATE PROCEDURE sp_ValidateVisitCompletion
    @VisitId INT,
    @IsValid BIT OUTPUT,
    @Message NVARCHAR(MAX) OUTPUT
AS
BEGIN
    DECLARE @MissingDocs NVARCHAR(MAX);
    DECLARE @HasGps BIT;
    
    -- Check GPS verification
    SELECT @HasGps = IsGpsVerified FROM Visits WHERE VisitId = @VisitId;
    
    -- Check mandatory documents
    SELECT @MissingDocs = STRING_AGG(DocumentType, ', ')
    FROM Documents
    WHERE VisitId = @VisitId AND IsMandatory = 1 AND DocumentStatus != 'Uploaded';
    
    IF @HasGps = 0
    BEGIN
        SET @IsValid = 0;
        SET @Message = 'GPS verification is required.';
        RETURN;
    END
    
    IF @MissingDocs IS NOT NULL
    BEGIN
        SET @IsValid = 0;
        SET @Message = 'Missing mandatory documents: ' + @MissingDocs;
        RETURN;
    END
    
    SET @IsValid = 1;
    SET @Message = 'All validations passed. Visit is ready for submission.';
END;
```

---

## DATA RETENTION & ARCHIVAL POLICY

```sql
-- Archive old completed visits (older than 2 years)
CREATE PROCEDURE sp_ArchiveOldVisits
    @ArchiveDate DATE
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Create backup table if not exists
        IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'VisitsArchive')
        BEGIN
            SELECT * INTO VisitsArchive FROM Visits WHERE 1=0;
        END
        
        -- Move old records
        INSERT INTO VisitsArchive
        SELECT * FROM Visits
        WHERE Status = 'Approved' AND VisitDate < @ArchiveDate;
        
        DELETE FROM Visits
        WHERE Status = 'Approved' AND VisitDate < @ArchiveDate;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
```

---

## BACKUP & RECOVERY STRATEGY

```sql
-- Full backup
BACKUP DATABASE SchoolDMS 
TO DISK = 'D:\Backups\SchoolDMS_Full_' + FORMAT(GETDATE(), 'yyyyMMdd_HHmmss') + '.bak'
WITH INIT, COMPRESSION;

-- Transaction log backup (hourly)
BACKUP LOG SchoolDMS 
TO DISK = 'D:\Backups\SchoolDMS_Log_' + FORMAT(GETDATE(), 'yyyyMMdd_HHmmss') + '.trn'
WITH COMPRESSION;

-- Differential backup (daily)
BACKUP DATABASE SchoolDMS 
TO DISK = 'D:\Backups\SchoolDMS_Diff_' + FORMAT(GETDATE(), 'yyyyMMdd') + '.bak'
WITH DIFFERENTIAL, INIT, COMPRESSION;
```

---

## PERFORMANCE TUNING RECOMMENDATIONS

1. **Index Maintenance:**
   ```sql
   -- Rebuild fragmented indexes
   ALTER INDEX ALL ON Visits REBUILD;
   
   -- Reorganize slightly fragmented indexes
   ALTER INDEX ALL ON Visits REORGANIZE;
   ```

2. **Statistics Update:**
   ```sql
   UPDATE STATISTICS Visits;
   UPDATE STATISTICS Documents;
   UPDATE STATISTICS ApprovalWorkflow;
   ```

3. **Query Optimization:**
   - Use NOLOCK hint for read-only queries
   - Avoid N+1 queries
   - Use appropriate JOIN types
   - Implement pagination (avoid SELECT * on large tables)

4. **Partitioning Strategy (for very large scale):**
   ```sql
   -- Partition Visits table by VisitDate
   CREATE PARTITION FUNCTION pf_VisitDateRange (DATE)
   AS RANGE LEFT FOR VALUES ('2023-01-01', '2024-01-01', '2025-01-01');
   ```

---

## COMPLETE SCHEMA GENERATION SCRIPT

To create the entire database schema at once, use this combined script:

```sql
-- Run this script to create complete database schema

-- Drop existing database (dev only)
-- DROP DATABASE IF EXISTS SchoolDMS;

-- Create database
CREATE DATABASE SchoolDMS;
GO

USE SchoolDMS;
GO

-- Create all tables (copy all CREATE TABLE statements from above)
-- Then create all indexes
-- Then create views
-- Then create stored procedures
-- Then insert default data
```

---

## ENTITY RELATIONSHIPS SUMMARY

| Parent Table | Child Table | Relationship | Cardinality | Action |
|---|---|---|---|---|
| Roles | Users | Role_User | 1:Many | Restrict Delete |
| Schools | Visits | School_Visit | 1:Many | Cascade Delete |
| Projects | Visits | Project_Visit | 1:Many | Restrict Delete |
| Users | Visits | Engineer_Visit | 1:Many | Cascade Delete |
| Visits | Documents | Visit_Document | 1:Many | Cascade Delete |
| Visits | ApprovalWorkflow | Visit_Approval | 1:1 | Cascade Delete |
| Users | Documents | User_Document | 1:Many | No Action |
| Users | ApprovalWorkflow | Verifier_Approval | 1:Many | Restrict Delete |
| Users | AuditLog | User_Audit | 1:Many | No Action |
| Documents | DocumentSearch | Document_Search | 1:1 | Cascade Delete |

---

## MIGRATION STRATEGY (Entity Framework Core)

```csharp
// In Program.cs or Startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    // Apply migrations
    context.Database.Migrate();
    
    // Seed default data
    SeedDefaultData(context);
}

private static void SeedDefaultData(ApplicationDbContext context)
{
    // Insert roles
    if (!context.Roles.Any())
    {
        context.Roles.AddRange(new List<Role>
        {
            new Role { RoleName = "Engineer", Description = "Field Engineer" },
            new Role { RoleName = "OpsVerifier", Description = "Operations Verifier" },
            new Role { RoleName = "Vendor", Description = "External Vendor" },
            new Role { RoleName = "Admin", Description = "System Administrator" }
        });
        context.SaveChanges();
    }
    
    // Insert projects
    if (!context.Projects.Any())
    {
        context.Projects.AddRange(new List<Project>
        {
            new Project { ProjectName = "Smart Classroom", Status = "Active" },
            new Project { ProjectName = "ISM", Status = "Active" },
            new Project { ProjectName = "PM Shri 1", Status = "Active" },
            new Project { ProjectName = "PM Shri 2", Status = "Active" },
            new Project { ProjectName = "Language Lab", Status = "Active" },
            new Project { ProjectName = "Vocational Lab", Status = "Active" }
        });
        context.SaveChanges();
    }
}
```

---

## MONITORING & MAINTENANCE QUERIES

```sql
-- Check database size
EXEC sp_spaceused;

-- Check index fragmentation
SELECT 
    OBJECT_NAME(ps.object_id) AS TableName,
    i.name AS IndexName,
    ps.avg_fragmentation_in_percent
FROM sys.dm_db_index_physical_stats(DB_ID(), NULL, NULL, NULL, 'LIMITED') ps
INNER JOIN sys.indexes i ON ps.object_id = i.object_id
    AND ps.index_id = i.index_id
WHERE ps.avg_fragmentation_in_percent > 10
ORDER BY ps.avg_fragmentation_in_percent DESC;

-- Check slow queries
SELECT TOP 10
    total_elapsed_time/execution_count AS [Average IO],
    execution_count,
    text
FROM sys.dm_exec_query_stats
CROSS APPLY sys.dm_exec_sql_text(sql_handle)
ORDER BY total_elapsed_time DESC;
```

---

## CONCLUSION

This complete database schema design provides:
- ✅ Proper normalization
- ✅ Role-based data segregation
- ✅ Comprehensive audit trail
- ✅ Efficient indexing for common queries
- ✅ Spatial indexes for GPS validation
- ✅ Full-text search capability
- ✅ Transactional integrity
- ✅ Security through foreign keys and constraints
- ✅ Scalability for PAN-India operations
- ✅ Compliance and regulatory support
