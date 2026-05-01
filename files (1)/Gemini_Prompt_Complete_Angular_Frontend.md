# COMPREHENSIVE GEMINI PROMPT FOR COMPLETE ANGULAR FRONTEND APPLICATION

Copy the entire prompt below and paste it in Gemini:

---

## PROMPT START HERE:

I need you to generate a complete, production-ready Angular 17+ web application with the following specifications. This is the frontend for a Document Management System (DMS) with two main applications integrated with a .NET Core backend API.

### PROJECT DETAILS:
- **Project Name:** SchoolDMS.Frontend
- **Framework:** Angular 17+
- **CSS Framework:** Tailwind CSS with shadcn/ui components
- **HTTP Client:** Angular HttpClient with Interceptors
- **State Management:** RxJS (reactive programming)
- **Routing:** Lazy-loaded modules
- **Backend API:** .NET Core API running on http://localhost:5000/api
- **Design Philosophy:** Minimal, clean, modern with excellent UX
- **Target Browsers:** Chrome, Firefox, Safari (latest versions)

### DESIGN REQUIREMENTS:

**Color Scheme (Minimal):**
- Primary: #3B82F6 (Blue)
- Secondary: #8B5CF6 (Purple)
- Success: #10B981 (Green)
- Danger: #EF4444 (Red)
- Warning: #F59E0B (Amber)
- Background: #F9FAFB (Light Gray)
- Surface: #FFFFFF (White)
- Text: #1F2937 (Dark Gray)
- Border: #E5E7EB (Light Border)

**Typography:**
- Font Family: Inter, -apple-system, BlinkMacSystemFont, "Segoe UI"
- Heading: Bold, Size 24-32px
- Subheading: Semi-bold, Size 18-20px
- Body: Regular, Size 14-16px
- Caption: Regular, Size 12px

**Spacing:**
- Use Tailwind's spacing scale (4px, 8px, 12px, 16px, 24px, 32px, etc.)
- Consistent margins/padding throughout
- Maximum content width: 1280px (xl breakpoint)

**UI Components (Use shadcn/ui):**
- Button (primary, secondary, danger, outline variants)
- Input fields with validation
- Select/Dropdown menus
- Data tables with sorting/filtering
- Cards for content grouping
- Modal dialogs
- Toast notifications
- Loading spinners
- Empty states
- Error boundaries

**Responsive Design:**
- Mobile First Approach
- Breakpoints: sm (640px), md (768px), lg (1024px), xl (1280px)
- Mobile: Single column, stacked layout
- Tablet: 2-column layout
- Desktop: Full multi-column layout

---

### APPLICATION STRUCTURE:

**Two Main Applications:**

#### 1. FIELD ENGINEER APP (Mobile-First, Responsive)
**Purpose:** Daily work assistant for field engineers
**Primary Users:** Engineers in field
**Access:** http://localhost:4200/engineer

#### 2. OPS VERIFICATION PORTAL (Desktop-Focused)
**Purpose:** Verification and approval system
**Primary Users:** Operations team
**Access:** http://localhost:4200/ops

#### 3. SHARED/CORE COMPONENTS & SERVICES
**For Both Applications**

---

### PROJECT FOLDER STRUCTURE:

```
SchoolDMS.Frontend/
│
├── src/
│   ├── app/
│   │   ├── core/
│   │   │   ├── interceptors/
│   │   │   │   ├── auth.interceptor.ts
│   │   │   │   ├── error.interceptor.ts
│   │   │   │   ├── loader.interceptor.ts
│   │   │   │   └── request.interceptor.ts
│   │   │   │
│   │   │   ├── guards/
│   │   │   │   ├── auth.guard.ts
│   │   │   │   └── role.guard.ts
│   │   │   │
│   │   │   ├── services/
│   │   │   │   ├── api/
│   │   │   │   │   ├── api.service.ts
│   │   │   │   │   ├── auth.service.ts
│   │   │   │   │   ├── visit.service.ts
│   │   │   │   │   ├── document.service.ts
│   │   │   │   │   ├── school.service.ts
│   │   │   │   │   ├── project.service.ts
│   │   │   │   │   ├── approval.service.ts
│   │   │   │   │   ├── report.service.ts
│   │   │   │   │   └── user.service.ts
│   │   │   │   │
│   │   │   │   ├── state/
│   │   │   │   │   ├── auth.service.ts (State management)
│   │   │   │   │   ├── user.service.ts (Current user)
│   │   │   │   │   └── loading.service.ts
│   │   │   │   │
│   │   │   │   ├── storage/
│   │   │   │   │   ├── local-storage.service.ts
│   │   │   │   │   └── session-storage.service.ts
│   │   │   │   │
│   │   │   │   └── notification/
│   │   │   │       └── notification.service.ts
│   │   │   │
│   │   │   ├── models/
│   │   │   │   ├── auth.model.ts
│   │   │   │   ├── user.model.ts
│   │   │   │   ├── school.model.ts
│   │   │   │   ├── visit.model.ts
│   │   │   │   ├── document.model.ts
│   │   │   │   ├── approval.model.ts
│   │   │   │   └── api-response.model.ts
│   │   │   │
│   │   │   └── core.module.ts
│   │   │
│   │   ├── shared/
│   │   │   ├── components/
│   │   │   │   ├── layout/
│   │   │   │   │   ├── navbar/
│   │   │   │   │   │   ├── navbar.component.ts
│   │   │   │   │   │   ├── navbar.component.html
│   │   │   │   │   │   └── navbar.component.css
│   │   │   │   │   ├── sidebar/
│   │   │   │   │   │   ├── sidebar.component.ts
│   │   │   │   │   │   ├── sidebar.component.html
│   │   │   │   │   │   └── sidebar.component.css
│   │   │   │   │   └── footer/
│   │   │   │   │       ├── footer.component.ts
│   │   │   │   │       ├── footer.component.html
│   │   │   │   │       └── footer.component.css
│   │   │   │   │
│   │   │   │   ├── common/
│   │   │   │   │   ├── loading-spinner/
│   │   │   │   │   ├── error-alert/
│   │   │   │   │   ├── success-alert/
│   │   │   │   │   ├── confirmation-dialog/
│   │   │   │   │   ├── empty-state/
│   │   │   │   │   ├── page-header/
│   │   │   │   │   └── breadcrumb/
│   │   │   │   │
│   │   │   │   ├── forms/
│   │   │   │   │   ├── form-input/
│   │   │   │   │   ├── form-select/
│   │   │   │   │   ├── form-textarea/
│   │   │   │   │   ├── form-checkbox/
│   │   │   │   │   ├── form-radio/
│   │   │   │   │   └── form-date-picker/
│   │   │   │   │
│   │   │   │   └── data-display/
│   │   │   │       ├── data-table/
│   │   │   │       ├── card/
│   │   │   │       ├── badge/
│   │   │   │       └── status-indicator/
│   │   │   │
│   │   │   ├── pipes/
│   │   │   │   ├── date-format.pipe.ts
│   │   │   │   ├── currency.pipe.ts
│   │   │   │   ├── truncate.pipe.ts
│   │   │   │   └── safe-html.pipe.ts
│   │   │   │
│   │   │   ├── directives/
│   │   │   │   ├── has-role.directive.ts
│   │   │   │   ├── app-debounce.directive.ts
│   │   │   │   └── auto-focus.directive.ts
│   │   │   │
│   │   │   └── shared.module.ts
│   │   │
│   │   ├── modules/
│   │   │   │
│   │   │   ├── auth/
│   │   │   │   ├── pages/
│   │   │   │   │   ├── login/
│   │   │   │   │   │   ├── login.component.ts
│   │   │   │   │   │   ├── login.component.html
│   │   │   │   │   │   └── login.component.css
│   │   │   │   │   ├── register/
│   │   │   │   │   │   ├── register.component.ts
│   │   │   │   │   │   ├── register.component.html
│   │   │   │   │   │   └── register.component.css
│   │   │   │   │   └── forgot-password/
│   │   │   │   │       ├── forgot-password.component.ts
│   │   │   │   │       ├── forgot-password.component.html
│   │   │   │   │       └── forgot-password.component.css
│   │   │   │   │
│   │   │   │   └── auth.module.ts
│   │   │   │   └── auth-routing.module.ts
│   │   │   │
│   │   │   ├── engineer/
│   │   │   │   ├── pages/
│   │   │   │   │   ├── dashboard/
│   │   │   │   │   │   ├── dashboard.component.ts
│   │   │   │   │   │   ├── dashboard.component.html
│   │   │   │   │   │   └── dashboard.component.css
│   │   │   │   │   │
│   │   │   │   │   ├── visit-list/
│   │   │   │   │   │   ├── visit-list.component.ts
│   │   │   │   │   │   ├── visit-list.component.html
│   │   │   │   │   │   └── visit-list.component.css
│   │   │   │   │   │
│   │   │   │   │   ├── visit-detail/
│   │   │   │   │   │   ├── visit-detail.component.ts
│   │   │   │   │   │   ├── visit-detail.component.html
│   │   │   │   │   │   └── visit-detail.component.css
│   │   │   │   │   │
│   │   │   │   │   ├── visit-form/
│   │   │   │   │   │   ├── visit-form.component.ts
│   │   │   │   │   │   ├── visit-form.component.html
│   │   │   │   │   │   └── visit-form.component.css
│   │   │   │   │   │
│   │   │   │   │   ├── document-upload/
│   │   │   │   │   │   ├── document-upload.component.ts
│   │   │   │   │   │   ├── document-upload.component.html
│   │   │   │   │   │   └── document-upload.component.css
│   │   │   │   │   │
│   │   │   │   │   ├── gps-checkin/
│   │   │   │   │   │   ├── gps-checkin.component.ts
│   │   │   │   │   │   ├── gps-checkin.component.html
│   │   │   │   │   │   └── gps-checkin.component.css
│   │   │   │   │   │
│   │   │   │   │   ├── visit-status/
│   │   │   │   │   │   ├── visit-status.component.ts
│   │   │   │   │   │   ├── visit-status.component.html
│   │   │   │   │   │   └── visit-status.component.css
│   │   │   │   │   │
│   │   │   │   │   └── profile/
│   │   │   │   │       ├── profile.component.ts
│   │   │   │   │       ├── profile.component.html
│   │   │   │   │       └── profile.component.css
│   │   │   │   │
│   │   │   │   ├── components/
│   │   │   │   │   └── visit-card/
│   │   │   │   │
│   │   │   │   └── engineer.module.ts
│   │   │   │   └── engineer-routing.module.ts
│   │   │   │
│   │   │   ├── ops/
│   │   │   │   ├── pages/
│   │   │   │   │   ├── dashboard/
│   │   │   │   │   │   ├── dashboard.component.ts
│   │   │   │   │   │   ├── dashboard.component.html
│   │   │   │   │   │   └── dashboard.component.css
│   │   │   │   │   │
│   │   │   │   │   ├── visits-pending/
│   │   │   │   │   │   ├── visits-pending.component.ts
│   │   │   │   │   │   ├── visits-pending.component.html
│   │   │   │   │   │   └── visits-pending.component.css
│   │   │   │   │   │
│   │   │   │   │   ├── visit-review/
│   │   │   │   │   │   ├── visit-review.component.ts
│   │   │   │   │   │   ├── visit-review.component.html
│   │   │   │   │   │   └── visit-review.component.css
│   │   │   │   │   │
│   │   │   │   │   ├── approve-dialog/
│   │   │   │   │   │   ├── approve-dialog.component.ts
│   │   │   │   │   │   ├── approve-dialog.component.html
│   │   │   │   │   │   └── approve-dialog.component.css
│   │   │   │   │   │
│   │   │   │   │   ├── reject-dialog/
│   │   │   │   │   │   ├── reject-dialog.component.ts
│   │   │   │   │   │   ├── reject-dialog.component.html
│   │   │   │   │   │   └── reject-dialog.component.css
│   │   │   │   │   │
│   │   │   │   │   ├── reports/
│   │   │   │   │   │   ├── dashboard/
│   │   │   │   │   │   │   ├── reports-dashboard.component.ts
│   │   │   │   │   │   │   ├── reports-dashboard.component.html
│   │   │   │   │   │   │   └── reports-dashboard.component.css
│   │   │   │   │   │   │
│   │   │   │   │   │   ├── export-excel/
│   │   │   │   │   │   │   ├── export-excel.component.ts
│   │   │   │   │   │   │   ├── export-excel.component.html
│   │   │   │   │   │   │   └── export-excel.component.css
│   │   │   │   │   │   │
│   │   │   │   │   │   └── export-pdf/
│   │   │   │   │   │       ├── export-pdf.component.ts
│   │   │   │   │   │       ├── export-pdf.component.html
│   │   │   │   │   │       └── export-pdf.component.css
│   │   │   │   │   │
│   │   │   │   │   ├── audit-logs/
│   │   │   │   │   │   ├── audit-logs.component.ts
│   │   │   │   │   │   ├── audit-logs.component.html
│   │   │   │   │   │   └── audit-logs.component.css
│   │   │   │   │   │
│   │   │   │   │   ├── schools-management/
│   │   │   │   │   │   ├── schools-list.component.ts
│   │   │   │   │   │   ├── schools-list.component.html
│   │   │   │   │   │   └── schools-list.component.css
│   │   │   │   │   │
│   │   │   │   │   ├── users-management/
│   │   │   │   │   │   ├── users-list.component.ts
│   │   │   │   │   │   ├── users-list.component.html
│   │   │   │   │   │   └── users-list.component.css
│   │   │   │   │   │
│   │   │   │   │   └── settings/
│   │   │   │   │       ├── settings.component.ts
│   │   │   │   │       ├── settings.component.html
│   │   │   │   │       └── settings.component.css
│   │   │   │   │
│   │   │   │   ├── components/
│   │   │   │   │   ├── visit-card/
│   │   │   │   │   ├── photo-viewer/
│   │   │   │   │   └── document-preview/
│   │   │   │   │
│   │   │   │   └── ops.module.ts
│   │   │   │   └── ops-routing.module.ts
│   │   │   │
│   │   │   ├── admin/
│   │   │   │   ├── pages/
│   │   │   │   │   ├── admin-dashboard/
│   │   │   │   │   ├── manage-users/
│   │   │   │   │   ├── manage-schools/
│   │   │   │   │   ├── manage-projects/
│   │   │   │   │   └── system-settings/
│   │   │   │   │
│   │   │   │   └── admin.module.ts
│   │   │   │   └── admin-routing.module.ts
│   │   │   │
│   │   │   └── settings/
│   │   │       ├── pages/
│   │   │       │   ├── profile-settings/
│   │   │       │   ├── change-password/
│   │   │       │   └── notification-settings/
│   │   │       │
│   │   │       └── settings.module.ts
│   │   │       └── settings-routing.module.ts
│   │   │
│   │   ├── app-routing.module.ts
│   │   ├── app.component.ts
│   │   ├── app.component.html
│   │   ├── app.component.css
│   │   └── app.module.ts
│   │
│   ├── assets/
│   │   ├── images/
│   │   │   ├── logo.svg
│   │   │   ├── hero-bg.svg
│   │   │   └── illustrations/
│   │   ├── icons/
│   │   │   ├── navigation.svg
│   │   │   ├── actions.svg
│   │   │   └── status.svg
│   │   └── data/
│   │       └── mock-data.json
│   │
│   ├── environments/
│   │   ├── environment.ts
│   │   └── environment.prod.ts
│   │
│   ├── styles/
│   │   ├── global.css
│   │   ├── variables.css
│   │   ├── typography.css
│   │   ├── utilities.css
│   │   ├── responsive.css
│   │   └── animations.css
│   │
│   ├── index.html
│   ├── main.ts
│   ├── polyfills.ts
│   └── styles.css
│
├── angular.json
├── tsconfig.json
├── tsconfig.app.json
├── tsconfig.spec.json
├── tailwind.config.js
├── package.json
├── package-lock.json
├── README.md
└── .gitignore
```

---

### FEATURE REQUIREMENTS:

#### A. ENGINEER APPLICATION (/engineer)

**1. Authentication Pages:**
- Login page with email/password (remember me option)
- Register page for new engineers
- Forgot password functionality
- Session management with JWT token storage

**2. Engineer Dashboard:**
- Quick stats card (visits today, pending, completed, rejected)
- Today's visit list with status
- Quick actions: New visit, View rejections, My profile
- Visit map view showing assigned schools
- Notifications (rejection feedback)

**3. Visit Management:**
- **List View:**
  - All assigned visits with filters
  - Status indicators (Draft, Submitted, Pending, Approved, Rejected)
  - School name, district, visit type
  - Sorting by date, status, school
  - Search functionality
  - Pagination
  
- **Create Visit:**
  - Select school (with search)
  - Select project type
  - Select visit type
  - Save as draft
  - Auto-save every 30 seconds
  
- **Visit Detail:**
  - Visit information summary
  - Current status display
  - All documents uploaded
  - Rejection feedback (if rejected)
  - Action buttons based on status
  - Edit/delete for draft visits only

**4. GPS Check-in:**
- Real-time GPS location capture
- School location map (visual confirmation)
- Distance calculation from school
- School signboard photo capture (mandatory)
- Timestamp recording
- Geolocation error handling

**5. Document Upload:**
- Structured upload interface by document type:
  - Before Installation Photo (Mandatory)
  - After Installation Photo (Mandatory)
  - Serial Number Image (Optional)
  - Installation Report/Certificate (Mandatory)
  - Engineer Notes (Optional)
- File type validation
- File size limits
- Progress bar for uploads
- Thumbnail preview
- Drag & drop upload
- Delete/replace uploaded files
- Document status indicators

**6. Visit Submission:**
- Validation checklist before submission:
  - GPS verified ✓
  - School photo captured ✓
  - All mandatory documents uploaded ✓
  - Work completed status selected ✓
- Confirmation dialog before submission
- Success/error messages
- Post-submission status: "Awaiting Verification"

**7. Visit Status Tracking:**
- Timeline view of visit status changes
- Rejection feedback with specific reasons
- Re-submission capability for rejected visits
- Approval confirmation

**8. Profile Page:**
- User information display (read-only except edit)
- Change password form
- Visit statistics (total, approved, rejected, pending)
- Contact information
- Last login information

**9. Offline Support (Nice to have):**
- Store draft visits locally
- Queue document uploads
- Sync when connectivity restored

**10. Mobile Optimization:**
- Touch-friendly buttons (48px minimum)
- Vertical layout for small screens
- Large form inputs
- Readable font sizes
- Landscape orientation support for camera

---

#### B. OPS VERIFICATION PORTAL (/ops)

**1. Ops Dashboard:**
- Key metrics cards:
  - Total visits (this month/all time)
  - Pending verification (real-time count)
  - Completed/approved visits
  - Repeat visits by school
  - Rejection rate
  - Engineer performance (top performers, most rejections)
- Charts & graphs:
  - Visits by district (bar chart)
  - Visits by status (pie chart)
  - Visits by project type (bar chart)
  - Daily submission trend (line chart)
- Quick filters (date range, district, status)

**2. Pending Verification List:**
- Data table showing:
  - Submission ID
  - School name & UDISE code
  - Engineer name
  - Visit date
  - Visit type
  - Time pending
  - Status indicator
- Filtering: District, Block, Visit Type, Date Range, Engineer
- Sorting: By date (newest first), engineer, school, time pending
- Bulk actions: Select multiple, approve multiple, reject multiple
- Quick view button (expand inline)

**3. Visit Review Page:**
- Split layout:
  - **Left side:** Visit details
    - School info (name, UDISE, address, contact)
    - Engineer info (name, contact)
    - Project type
    - Visit type
    - Visit date & time
    - GPS coordinates with map
    - Work completed status
    - Engineer notes
  
  - **Right side:** Document viewer
    - Document gallery (thumbnails)
    - Click to view full image
    - Zoom capability
    - Document type label
    - Document status
    - Full-screen view option

- **Bottom:** Document checklist
  - Before photo: ✓/✗
  - After photo: ✓/✗
  - Serial number: ✓/✗
  - Certificate: ✓/✗
  - Notes: ✓/✗

**4. Approval Dialog (Modal):**
- Simple "Approve" button
- Confirmation message
- Load spinner while processing
- Success notification
- Visit status changes to "Approved"

**5. Rejection Dialog (Modal):**
- Multi-select rejection reasons:
  - ☐ Wrong school
  - ☐ Fake visit (GPS mismatch)
  - ☐ Blurry photos
  - ☐ Missing device proof
  - ☐ Incorrect serial number
  - ☐ Training pending
  - ☐ Faulty/Damaged/Missing item
- Comments textarea (optional)
- Send feedback to engineer
- Confirmation before rejecting
- Visit status changes to "Rejected"

**6. Reports Section:**

- **Dashboard:**
  - Date range picker
  - Various metric cards
  - Charts showing trends
  - Export buttons

- **Export to Excel:**
  - Filter options: Date range, status, district, school, engineer
  - Column selection
  - Preview before export
  - Export button
  - Downloaded as .xlsx file
  - Columns: Visit ID, School, UDISE, District, Engineer, Date, Type, Status

- **Generate PDF:**
  - Single visit PDF:
    - Visit summary
    - School info
    - All documents (with images)
    - Engineer signature area
    - Page limit: 1-2 pages
  
  - Multiple visits merged PDF:
    - Select multiple visits
    - Preview total pages
    - Each visit 1-2 pages
    - Single merged PDF file
    - Bookmarks for each school

**7. Audit Logs:**
- Table view showing:
  - Timestamp
  - User name
  - Action (Created, Updated, Approved, Rejected, Downloaded)
  - Table affected
  - Record ID
  - Details (clickable for more info)
- Filters: User, Action, Date range, Table
- Sorting: By date (newest first), user, action
- Pagination
- Export audit trail to CSV

**8. School Management (Admin only):**
- List all schools
- Search by UDISE, name, district
- Filter by district, block
- Add new school
- Edit school details
- View visit history for each school
- Delete school (soft delete)

**9. User Management (Admin only):**
- List all users
- Filter by role: Engineer, OpsVerifier, Vendor, Admin
- Search by name, email
- Add new user
- Edit user details
- Change user role
- Enable/disable user
- Reset password
- View user activity

**10. Settings Page (Admin only):**
- System configuration
- Email settings
- Notification settings
- Document settings
- GPS settings (check-in radius)

---

### ANGULAR ARCHITECTURE REQUIREMENTS:

**1. Module Structure:**
```typescript
// Lazy-loaded modules with routing
CoreModule (providers only, imported once in root)
SharedModule (imported by feature modules)
AuthModule (login, register, forgot password)
EngineerModule (lazy-loaded at /engineer)
OpsModule (lazy-loaded at /ops)
AdminModule (lazy-loaded at /admin)
SettingsModule (lazy-loaded at /settings)
```

**2. Services (In core/services/api/):**
```typescript
// Base API Service
ApiService (handles HTTP requests)

// Feature Services
AuthService (login, register, token management)
VisitService (CRUD for visits, check-in, submit)
DocumentService (upload, download, delete)
SchoolService (CRUD for schools)
ProjectService (get projects)
ApprovalService (approve, reject visits)
ReportService (generate excel, pdf, dashboard data)
UserService (manage users)
AuditService (get audit logs)

// State Services
AuthStateService (current user, authentication state)
UserStateService (current user details)
LoadingService (global loading state)

// Utility Services
StorageService (localStorage management)
NotificationService (toast notifications)
```

**3. HTTP Interceptors:**
```typescript
// auth.interceptor.ts - Add JWT token to requests
// error.interceptor.ts - Handle errors globally (401, 403, 500)
// loader.interceptor.ts - Show/hide global loader
// request.interceptor.ts - Add timestamps, headers
```

**4. Guards:**
```typescript
// auth.guard.ts - Check if user is authenticated
// role.guard.ts - Check if user has required role
// unsaved.guard.ts - Prevent navigation without saving
```

**5. Models/Interfaces:**
```typescript
// Exactly matching backend DTOs
AuthModels:
  - LoginRequest
  - LoginResponse
  - RegisterRequest
  - AuthToken

VisitModels:
  - Visit
  - CreateVisitRequest
  - UpdateVisitRequest
  - VisitListItem
  
DocumentModels:
  - Document
  - DocumentUploadRequest
  - DocumentDTO

ApprovalModels:
  - ApprovalRequest
  - RejectionRequest
  - ApprovalResponse

ReportModels:
  - DashboardMetrics
  - ExcelExportFilter
  - PDFGenerateRequest

SchoolModels:
  - School
  - SchoolDTO
  - CreateSchoolRequest

UserModels:
  - User
  - UserDTO
  - CreateUserRequest
  - UpdateUserRequest
```

**6. Reactive State Management (RxJS):**
```typescript
// Use BehaviorSubject for state
// Use shareReplay for HTTP calls
// Use combineLatest for dependent data
// Use switchMap for sequential operations
// Proper unsubscription (OnDestroy, takeUntil)
```

**7. Validation:**
- Reactive Forms with FormBuilder
- Custom validators
- Real-time validation feedback
- Validation messages in UI
- File size/type validation

**8. Error Handling:**
- Global error interceptor
- User-friendly error messages
- Error boundary components
- Retry logic for failed requests
- Offline detection & notification

---

### UI/UX REQUIREMENTS:

**1. Design System:**
- Use Tailwind CSS utility classes
- Use shadcn/ui components
- Consistent spacing throughout
- Proper contrast ratios (WCAG AA)
- Accessible form labels
- Keyboard navigation support

**2. Component Library (shadcn/ui):**
```
Implement:
- Button (variants: primary, secondary, danger, outline)
- Input (with error states)
- Select (dropdown)
- Textarea
- Checkbox
- Radio
- Dialog/Modal
- Toast notifications
- Card
- Table
- Badge
- Loading spinner
- Tabs
- Accordion
```

**3. Animations & Transitions:**
- Smooth page transitions (300-400ms)
- Button hover effects (subtle)
- Loading spinners (rotating)
- Toast notifications slide-in/out
- Fade in/out for modals
- Page skeleton loaders
- No jarring jumps or flashes

**4. Empty States:**
- When no visits exist
- When no documents uploaded
- When no search results
- When no notifications
- Helpful text + illustration
- Call-to-action button

**5. Loading States:**
- Loading skeleton for cards
- Spinner for buttons
- Spinner for page transitions
- "Loading..." text
- Estimated load time if long

**6. Error States:**
- Error message boxes
- Error illustrations
- Retry buttons
- Contact support link
- Error codes for debugging

**7. Success States:**
- Success toast notifications
- Success checkmarks
- Confirmation messages
- Next action suggestions

**8. Responsive Design:**
- Mobile: Single column, vertical stack
- Tablet: 2-column, adjusted spacing
- Desktop: Full multi-column layout
- Navigation: Hamburger menu on mobile, sidebar on desktop
- Tables: Horizontal scroll on mobile
- Buttons: Full-width on mobile, inline on desktop

---

### SPECIFIC PAGE REQUIREMENTS:

#### ENGINEER APP:

**Login Page:**
```
Hero section with illustration
Form fields: Email, Password
"Remember me" checkbox
"Forgot password?" link
"Sign up" link
Login button
Social login (optional)
Responsive to mobile
```

**Engineer Dashboard:**
```
Header: Welcome message, date/time, profile avatar
Quick stats (4 cards):
  - Visits Today
  - Completed
  - Pending Approval
  - Rejected (with latest)

Recent Visits Table:
  - School Name
  - Visit Type
  - Status (color-coded badge)
  - Date
  - Action (View button)

Quick Actions (3 buttons):
  - New Visit
  - View Rejections
  - View Profile

Map view (optional):
  - Show assigned schools
  - Distance from current location
```

**Visit List:**
```
Header with search and filters:
  - Status filter (Draft, Submitted, Pending, Approved, Rejected)
  - Date range picker
  - School search

Visits displayed as:
  Option 1: Card layout (mobile-friendly)
    - School name, district
    - Visit type badge
    - Status badge (color-coded)
    - Date, engineer, distance
    - Action buttons (View, Edit, Delete for drafts)
  
  Option 2: Table layout (desktop)
    - Sortable columns
    - Pagination
    - Inline action buttons

Pagination: 10 items per page
```

**Create/Edit Visit:**
```
Form with sections:
1. Project & Visit Type
   - Project dropdown (Smart Classroom, ISM, etc.)
   - Visit type dropdown
   
2. School Selection
   - School search with autocomplete
   - School details displayed (UDISE, address, distance)
   
3. Visit Details
   - Date picker (default today)
   - Time
   - Notes textarea

Buttons:
  - Save as Draft
  - Clear Form
  - Cancel

Auto-save every 30 seconds
```

**GPS Check-in Page:**
```
Header: "School Check-in"

Current location display:
  - Latitude, Longitude
  - Accuracy
  - "Get Current Location" button

School location display:
  - Expected location
  - Distance calculation

Map View:
  - Current location marker (blue)
  - School location marker (red)
  - Distance line
  - Visual confirmation

Camera section:
  - "Take Photo" button
  - School name overlay
  - UDISE code overlay
  - Photo preview (thumbnail)
  - "Retake" button

Verification indicator:
  - GPS verified ✓
  - Photo captured ✓

Proceed button:
  - Enabled only when GPS verified and photo captured
```

**Document Upload Page:**
```
Grouped by document type:

1. Before Installation Photo (Mandatory)
   - Upload area (drag-drop)
   - File type validation
   - Size limit (10MB)
   - Preview thumbnail
   - Delete button

2. After Installation Photo (Mandatory)
   - Same as above

3. Serial Number Image (Optional)
   - Same as above

4. Installation Report/Certificate (Mandatory)
   - Same as above (supports PDF)

5. Engineer Notes (Optional)
   - Large textarea
   - Character counter
   - Auto-save

Upload progress:
  - Progress bar per file
  - Percentage

Validation:
  - Mandatory document checklist
  - Visual indicators: pending, uploaded, verified

Buttons:
  - Upload more files
  - Continue to submission
```

**Visit Submission Page:**
```
Pre-submission checklist:
  ✓ GPS verified
  ✓ School photo captured
  ✓ All mandatory documents uploaded
  ✓ Work completed: Yes/No (radio)

Confirmation dialog:
  "Are you sure you want to submit this visit?"
  "Once submitted, you cannot edit."
  
Buttons:
  - Go Back
  - Submit Visit

After submission:
  "Visit submitted successfully!"
  "Status: Awaiting Verification"
  "Next step: Ops team will review and approve"
  
Button: "Back to Dashboard"
```

**Engineer Profile Page:**
```
Left sidebar:
  - Avatar (editable)
  - Name
  - Email
  - Role badge

Main content:
1. Personal Information:
   - Name (editable)
   - Email (read-only)
   - Phone (editable)
   - Contact address (editable)
   - Save button

2. Change Password:
   - Current password
   - New password (with strength indicator)
   - Confirm password
   - Update button

3. Statistics:
   - Total visits
   - Approved visits
   - Rejected visits
   - Pending visits
   - Approval rate %
   - Average completion time

4. Activity:
   - Last login date/time
   - Recent actions list
```

---

#### OPS VERIFICATION PORTAL:

**Ops Dashboard:**
```
Header: "Dashboard" with date range picker

4 Key Metric Cards (grid):
  1. Total Visits This Month
     - Large number
     - Percentage change indicator (up/down)
     - Trend icon
  
  2. Pending Verification
     - Large number (red if > 10)
     - "View pending" button
  
  3. Approved Visits
     - Large number
     - Percentage of total
  
  4. Rejection Rate
     - Percentage
     - Trend indicator

Charts section (responsive grid):
  1. Visits by District (Bar chart)
     - Top 10 districts
     - Sort by count
  
  2. Visits by Status (Pie chart)
     - Draft, Submitted, Approved, Rejected
     - Color-coded
  
  3. Visits by Project Type (Bar chart)
     - All project types
     - Count per type
  
  4. Daily Submission Trend (Line chart)
     - Last 30 days
     - Approve/reject overlay

Engineer Performance:
  - Top 5 performers (by approved visits)
  - Bottom 5 (by rejection rate)
  - Quick links to engineer profiles

Buttons:
  - View pending visits
  - Generate report
  - View audit logs
```

**Pending Verification List:**
```
Header with filters:
  - District dropdown
  - Block dropdown
  - Visit Type dropdown
  - Date range picker
  - Engineer search
  - Clear filters button

Data table:
  Columns: Submission ID | School Name (UDISE) | Engineer | Visit Date | Visit Type | Time Pending | Status
  - Sortable columns
  - Clickable rows (view detail)
  - Color-coded status badges
  - Pagination (20 items per page)

Bulk actions:
  - Checkbox to select rows
  - "Select all" checkbox
  - "Approve selected" button
  - "Reject selected" button

Empty state:
  "No pending verifications!"
  "Great work! All visits have been reviewed."
```

**Visit Review Page:**
```
Header:
  School name | UDISE | Engineer name
  Status badge

Split layout (desktop) / Stacked (mobile):

Left Panel (60%):
  School Information Card:
    - School name & UDISE
    - District, Block
    - Address
    - Contact person & phone
    - GPS coordinates
    - Map with location pin

  Engineer Information Card:
    - Engineer name
    - Email, phone
    - Average approval rate
    - Total visits completed

  Visit Details Card:
    - Visit type
    - Visit date & time
    - GPS check-in coordinates
    - Is GPS verified? ✓/✗
    - Work completed? Yes/No
    - Engineer notes (if any)

Right Panel (40%):
  Document Gallery:
    - Thumbnail grid
    - Click to enlarge
    - Fullscreen option
    - Document type label
    - Status indicator (✓ verified / ✗ missing)
  
  Documents:
    - Before installation photo
    - After installation photo
    - Serial number image (if uploaded)
    - Certificate/Report
    - Engineer notes

Bottom:
  Action Buttons (sticky):
    - Approve button (green)
    - Reject button (red)
    - Back button

Modals (shown when clicking approve/reject):
  - Approval confirmation
  - Rejection with reasons
```

**Rejection Dialog:**
```
Title: "Reject Visit Submission"

Rejection reasons (checkboxes):
  ☐ Wrong school
  ☐ Fake visit (GPS mismatch)
  ☐ Blurry photos
  ☐ Missing device proof
  ☐ Incorrect serial number
  ☐ Training pending
  ☐ Faulty/Damaged/Missing item

Comments field (textarea):
  "Additional feedback for engineer (optional)"

Buttons:
  - Cancel
  - Send Rejection

Success message:
  "Visit rejected and engineer notified"
  "Status: Rejected"
  "Next: Wait for engineer re-submission"
```

**Reports Dashboard:**
```
Date range filter (top)

Report options (cards with buttons):

1. Excel Export:
   - Filter options section
     * Status (dropdown)
     * District (dropdown)
     * Date range (picker)
     * Engineer (search)
   - Preview button
   - Export button
   - Downloaded as: SchoolDMS_Report_[date].xlsx

2. PDF Generation:
   - Single visit PDF:
     * Visit search/select
     * Preview
     * Download PDF button
   
   - Multiple visits merged:
     * Select multiple visits
     * Show page count preview
     * Merge button
     * Download merged PDF

3. Custom Report:
   - Various metric cards
   - Export as table
```

**Audit Logs Page:**
```
Header: "Audit Logs"

Filters:
  - User dropdown
  - Action dropdown (Created, Updated, Deleted, Approved, Rejected)
  - Table dropdown
  - Date range picker

Data table:
  Columns: Timestamp | User | Action | Table | Record ID | Details
  - Sortable
  - Pagination
  - Expandable details row

Details modal:
  - Full timestamp
  - User details
  - Action performed
  - Old values (JSON)
  - New values (JSON)
  - IP address
  - User agent

Export button:
  - Download as CSV
```

---

### BACKEND API INTEGRATION:

**Authentication Flow:**
```
1. Login page → POST /api/auth/login
   Request: { email, password }
   Response: { accessToken, refreshToken, expiresIn, user }

2. Store tokens:
   - accessToken → localStorage (or secure storage)
   - refreshToken → localStorage (or httpOnly cookie)

3. All requests:
   - Add Authorization header: "Bearer {accessToken}"

4. Token expiry:
   - Intercept 401 responses
   - Call POST /api/auth/refresh
   - Retry original request
   - If refresh fails, redirect to login
```

**API Call Examples:**

```typescript
// Get engineer's visits
GET /api/visits/by-engineer/{engineerId}
Response: Visit[]

// Create visit
POST /api/visits
Body: CreateVisitDTO
Response: Visit

// Upload document
POST /api/documents/upload
Content-Type: multipart/form-data
Body: { visitId, documentType, file }
Response: Document

// Check-in (GPS)
PATCH /api/visits/{visitId}/check-in
Body: { latitude, longitude, schoolPhotoUrl }
Response: Visit

// Submit visit
PATCH /api/visits/{visitId}/submit
Body: { workCompleted, notes }
Response: Visit

// Get pending verifications (Ops only)
GET /api/visits/pending-verification
Response: Visit[]

// Approve visit (Ops only)
POST /api/approvals/{visitId}/approve
Response: Approval

// Reject visit (Ops only)
POST /api/approvals/{visitId}/reject
Body: { rejectionReasons[], comments }
Response: Approval

// Get dashboard metrics
GET /api/reports/dashboard
Response: DashboardMetrics

// Export Excel
GET /api/reports/export-excel?status=Approved&district=Delhi
Response: Binary file (xlsx)

// Generate PDF
GET /api/reports/generate-pdf/{visitId}
Response: Binary file (pdf)
```

---

### STYLING & THEME:

**Tailwind Configuration (tailwind.config.js):**
```javascript
module.exports = {
  theme: {
    colors: {
      primary: '#3B82F6',
      secondary: '#8B5CF6',
      success: '#10B981',
      danger: '#EF4444',
      warning: '#F59E0B',
      info: '#06B6D4',
      gray: {
        50: '#F9FAFB',
        100: '#F3F4F6',
        200: '#E5E7EB',
        300: '#D1D5DB',
        400: '#9CA3AF',
        500: '#6B7280',
        600: '#4B5563',
        700: '#374151',
        800: '#1F2937',
        900: '#111827',
      }
    },
    extend: {
      spacing: {
        xs: '0.5rem',
        sm: '1rem',
        md: '1.5rem',
        lg: '2rem',
        xl: '3rem',
        xxl: '4rem',
      }
    }
  }
}
```

**Global CSS:**
```css
* {
  @apply transition-colors duration-300;
}

body {
  @apply bg-gray-50 text-gray-900 font-sans;
}

h1 { @apply text-3xl font-bold mb-4; }
h2 { @apply text-2xl font-bold mb-3; }
h3 { @apply text-xl font-semibold mb-2; }

button {
  @apply py-2 px-4 rounded-lg font-medium transition-all;
  &:disabled { @apply opacity-50 cursor-not-allowed; }
}

input, textarea, select {
  @apply border border-gray-300 rounded-lg px-3 py-2 focus:outline-none focus:ring-2 focus:ring-primary;
}
```

---

### PACKAGE.JSON DEPENDENCIES:

```json
{
  "dependencies": {
    "@angular/animations": "^17.0.0",
    "@angular/common": "^17.0.0",
    "@angular/compiler": "^17.0.0",
    "@angular/core": "^17.0.0",
    "@angular/forms": "^17.0.0",
    "@angular/platform-browser": "^17.0.0",
    "@angular/platform-browser-dynamic": "^17.0.0",
    "@angular/router": "^17.0.0",
    "rxjs": "^7.8.0",
    "tslib": "^2.6.0",
    "zone.js": "^0.14.0",
    "@tailwindcss/forms": "^0.5.6",
    "tailwindcss": "^3.3.0",
    "autoprefixer": "^10.4.16",
    "postcss": "^8.4.31",
    "@angular/cdk": "^17.0.0",
    "ng-lazyload-image": "^15.0.0",
    "ngx-device-detector": "^15.0.0",
    "ngx-mat-file-input": "^3.0.0",
    "date-fns": "^2.30.0"
  },
  "devDependencies": {
    "@angular-devkit/build-angular": "^17.0.0",
    "@angular/cli": "^17.0.0",
    "@angular/compiler-cli": "^17.0.0",
    "@types/jasmine": "~5.1.0",
    "jasmine-core": "~5.1.0",
    "karma": "~6.4.0",
    "karma-chrome-launcher": "~3.2.0",
    "karma-coverage": "~2.2.0",
    "karma-jasmine": "~5.1.0",
    "karma-jasmine-html-reporter": "~2.1.0",
    "typescript": "~5.2.2"
  }
}
```

---

### ENVIRONMENT CONFIGURATION:

**environment.ts (Development):**
```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000/api',
  appUrl: 'http://localhost:4200',
  tokenKey: 'auth_token',
  refreshTokenKey: 'refresh_token',
  logRequests: true,
  enableDebug: true
};
```

**environment.prod.ts (Production):**
```typescript
export const environment = {
  production: true,
  apiUrl: 'https://api.yourschoolsdms.com/api',
  appUrl: 'https://yourschoolsdms.com',
  tokenKey: 'auth_token',
  refreshTokenKey: 'refresh_token',
  logRequests: false,
  enableDebug: false
};
```

---

### IMPORTANT IMPLEMENTATION DETAILS:

**1. Responsive Design:**
- Mobile first approach
- Tested on: iPhone SE, iPad, Desktop
- Touch targets minimum 48px
- Readable font sizes (minimum 14px)
- Landscape support for mobile

**2. Accessibility:**
- Proper ARIA labels
- Semantic HTML
- Keyboard navigation (Tab, Enter, Escape)
- Color contrast ratios WCAG AA
- Form error messages linked to inputs
- Focus indicators visible

**3. Performance:**
- Lazy load modules
- OnPush change detection
- Unsubscribe from observables (takeUntil pattern)
- Image optimization
- Efficient data tables (virtual scroll for large lists)
- Cache API responses appropriately

**4. Code Quality:**
- TypeScript strict mode
- ESLint configuration
- Prettier formatting
- Component follows OnPush pattern
- Proper typing (no any)
- Constants in separate files

**5. Error Handling:**
- User-friendly error messages
- Network error detection
- Offline notification
- Retry logic for failed requests
- Error logging to console (dev) / service (prod)

**6. Security:**
- Store JWT in secure storage
- HTTPS enforcement (prod)
- Sanitize user inputs
- Prevent XSS attacks
- CSRF protection headers
- Content Security Policy headers

**7. Testing Preparation:**
- Services isolated for testing
- Mock services provided
- Dependency injection throughout
- Helper functions in utilities
- Test data in assets/data

**8. Documentation:**
- JSDoc comments for functions
- README for setup instructions
- Inline comments for complex logic
- Component usage examples

---

### GENERATE:

Please generate:
1. All TypeScript component classes (.ts files)
2. All HTML templates (.html files)
3. All CSS styles (.css files)
4. All service classes (API, state, utility)
5. All interceptors and guards
6. All models/interfaces
7. All modules and routing modules
8. SharedModule with all shared components
9. CoreModule with all services
10. Global styles and Tailwind configuration
11. Environment configuration files
12. Module barrel exports (index.ts files)
13. Complete package.json with all dependencies
14. Angular.json with proper configuration
15. tsconfig files (app, spec, base)

### IMPORTANT NOTES:

- Use standalone components where appropriate (Angular 14+)
- Implement proper error boundaries
- Add loading states for all async operations
- Use trackBy in *ngFor loops
- Implement proper form validation with feedback
- Use OnPush change detection strategy
- Proper TypeScript typing (no any types)
- Implement lazy loading for routes
- Add proper logging throughout
- Create utility functions for common operations
- Use shadcn/ui components consistently
- Ensure dark mode support (optional but nice to have)
- Implement proper keyboard navigation
- Add ARIA labels for accessibility
- Implement proper mobile responsiveness
- Use Tailwind utility classes for styling
- Create reusable component library
- Implement proper state management with RxJS
- Use HttpClient with proper interceptors
- Implement proper JWT token management
- Add loading skeletons for data tables
- Implement infinite scroll or pagination
- Add proper image optimization
- Use content-security-policy headers
- Implement proper error logging
- Add analytics (optional)
- Implement proper performance monitoring
- Create helper functions for API calls
- Implement proper caching strategy
- Add proper unit test examples
- Implement E2E test structure
- Create comprehensive documentation
- Add JSDoc comments to all functions
- Implement proper version management
- Create deployment guide
- Implement health check endpoint

---

## PROMPT END

---

## HOW TO USE THIS PROMPT:

1. **Open Gemini** (web.google.com/gemini or VS Code extension)

2. **Copy the entire prompt** (from "PROMPT START HERE" to "PROMPT END")

3. **Paste in Gemini chat**

4. **Press Send** and wait for generation

5. **Expected Output:**
   - Complete Angular 17+ application
   - All components (TypeScript + HTML + CSS)
   - All services with API integration
   - All models and interfaces
   - Complete routing setup
   - Tailwind CSS configuration
   - Responsive design
   - Full backend API integration
   - Proper error handling
   - Loading states
   - Mobile-optimized UI
   - Minimal, clean design

6. **After Generation:**
   - Create new Angular project: `ng new SchoolDMS.Frontend`
   - Copy generated files to appropriate folders
   - Run: `npm install`
   - Update environment.ts with your API URL
   - Run: `ng serve`
   - Access at: http://localhost:4200

---

## EXPECTED RESULTS:

✅ Complete Engineer Application with:
- Login/Register
- Visit management (create, list, detail, edit)
- GPS check-in with photo capture
- Structured document upload (5 document types)
- Visit submission workflow
- Status tracking
- Profile management
- Mobile-optimized UI

✅ Complete Ops Verification Portal with:
- Dashboard with analytics
- Pending verification list
- Visit review with document viewer
- Approve/Reject workflow
- Excel/PDF export
- Audit logs
- School management
- User management
- Settings

✅ Full Backend Integration:
- JWT authentication
- Role-based access control
- API interceptors
- Error handling
- Loading states
- Token refresh mechanism

✅ Professional Design:
- Minimal, clean interface
- Proper spacing and typography
- Color-coded status indicators
- Responsive mobile design
- Accessible components
- Smooth animations

✅ Production-Ready Code:
- TypeScript strict mode
- Proper error handling
- Security best practices
- Performance optimized
- Code documentation
- Modular architecture
