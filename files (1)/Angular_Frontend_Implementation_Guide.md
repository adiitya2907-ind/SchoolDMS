# ANGULAR FRONTEND - COMPLETE IMPLEMENTATION GUIDE

## QUICK START FOR ANGULAR FRONTEND

### Step 1: Use the Gemini Prompt
**File:** `Gemini_Prompt_Complete_Angular_Frontend.md`

1. Open Gemini (web.google.com/gemini)
2. Copy the entire prompt from the document
3. Paste in Gemini chat
4. Let Gemini generate complete Angular application
5. Download/copy all generated files

### Step 2: Project Setup
```bash
# Create new Angular project
ng new SchoolDMS.Frontend

# Navigate to project
cd SchoolDMS.Frontend

# Install dependencies (after copying generated files)
npm install

# Update environment.ts
# Set apiUrl: 'http://localhost:5000/api'

# Start development server
ng serve

# Access at http://localhost:4200
```

---

## TWO MAIN APPLICATIONS OVERVIEW

### 1. ENGINEER APP (/engineer) - Mobile-First
**Purpose:** Daily work assistant for field engineers
**Key Features:**
- Visit management (CRUD)
- GPS check-in with geolocation
- Document upload (5 structured types)
- Visit submission workflow
- Status tracking & notifications

**Key Pages:**
- Dashboard (overview of visits)
- Visit List (all assigned visits)
- Create/Edit Visit (form)
- GPS Check-in (location + photo)
- Document Upload (structured)
- Visit Submission (confirmation)
- Status Tracking (timeline)
- Profile (settings)

**Mobile Optimized:**
- Touch-friendly buttons (48px min)
- Full-width layout on small screens
- Large form inputs
- Readable fonts
- Landscape support

---

### 2. OPS VERIFICATION PORTAL (/ops) - Desktop-Focused
**Purpose:** Verify and approve field visits
**Key Features:**
- Dashboard with analytics
- Pending verification list
- Visit review & document viewing
- Approval/rejection workflow
- Excel & PDF export
- Audit logging
- Admin functions

**Key Pages:**
- Dashboard (metrics & charts)
- Pending Verification (queue)
- Visit Review (detail + documents)
- Reports (excel, pdf, custom)
- Audit Logs (history)
- Schools Management (admin)
- Users Management (admin)
- Settings (admin)

**Desktop Optimized:**
- Multi-column layout
- Large data tables
- Advanced filtering
- Detailed reports
- Side-by-side document viewer

---

## DESIGN SYSTEM & COLOR PALETTE

### Colors (Tailwind)
```
Primary:       #3B82F6 (Blue)     - Actions, buttons, links
Secondary:    #8B5CF6 (Purple)   - Secondary actions
Success:      #10B981 (Green)    - Approved, success
Danger:       #EF4444 (Red)      - Rejected, errors, delete
Warning:      #F59E0B (Amber)    - Pending, warnings
Info:         #06B6D4 (Cyan)     - Information
Background:   #F9FAFB (Light)    - Page background
Surface:      #FFFFFF (White)    - Cards, panels
Text:         #1F2937 (Dark)     - Main text
```

### Typography
```
Headings:     Inter, Bold 24-32px
Subheadings:  Inter, Semi-bold 18-20px
Body:         Inter, Regular 14-16px
Captions:     Inter, Regular 12px
Monospace:    Courier, Regular 12px (code, IDs)
```

### Spacing System
```
xs:  0.5rem  (4px)
sm:  1rem    (8px)
md:  1.5rem  (12px)
lg:  2rem    (16px)
xl:  3rem    (24px)
xxl: 4rem    (32px)
```

---

## COMPONENT HIERARCHY

### Engineer App Component Tree
```
app-root
├── navbar (top navigation)
├── sidebar (left navigation - collapsible on mobile)
├── main-content
│   ├── engineer-dashboard
│   │   ├── stats-card (4x)
│   │   ├── recent-visits-table
│   │   ├── quick-actions
│   │   └── notifications
│   │
│   ├── visit-list
│   │   ├── filters-bar
│   │   ├── search-input
│   │   ├── visit-card (repeated)
│   │   └── pagination
│   │
│   ├── visit-detail
│   │   ├── visit-header
│   │   ├── visit-info
│   │   ├── documents-section
│   │   ├── rejection-feedback (if rejected)
│   │   └── action-buttons
│   │
│   ├── visit-form
│   │   ├── form-field (project)
│   │   ├── form-field (school)
│   │   ├── form-field (visit-type)
│   │   ├── form-field (notes)
│   │   └── form-buttons
│   │
│   ├── gps-checkin
│   │   ├── location-display
│   │   ├── map-view
│   │   ├── camera-section
│   │   ├── verification-checklist
│   │   └── proceed-button
│   │
│   ├── document-upload
│   │   ├── upload-group (x5)
│   │   │   ├── upload-area
│   │   │   ├── file-preview
│   │   │   └── delete-button
│   │   ├── mandatory-checklist
│   │   └── action-buttons
│   │
│   ├── visit-submission
│   │   ├── checklist-review
│   │   ├── confirmation-dialog
│   │   └── success-message
│   │
│   └── profile-page
│       ├── avatar-section
│       ├── info-form
│       ├── password-section
│       └── statistics
│
└── footer
```

### Ops Portal Component Tree
```
app-root
├── navbar (top navigation)
├── sidebar (left navigation - always visible)
├── main-content
│   ├── ops-dashboard
│   │   ├── metric-cards (4x)
│   │   ├── charts-section
│   │   │   ├── bar-chart (district)
│   │   │   ├── pie-chart (status)
│   │   │   ├── bar-chart (project)
│   │   │   └── line-chart (trend)
│   │   ├── engineer-performance
│   │   └── quick-actions
│   │
│   ├── pending-verification
│   │   ├── filters-bar
│   │   ├── data-table
│   │   │   ├── table-header
│   │   │   ├── table-rows (sorted/paginated)
│   │   │   └── action-buttons
│   │   ├── bulk-actions
│   │   └── pagination
│   │
│   ├── visit-review
│   │   ├── visit-header
│   │   ├── left-panel
│   │   │   ├── school-info-card
│   │   │   ├── engineer-info-card
│   │   │   └── visit-details-card
│   │   ├── right-panel
│   │   │   ├── document-gallery
│   │   │   └── document-list
│   │   ├── document-modal (fullscreen viewer)
│   │   └── action-buttons
│   │
│   ├── approval-dialogs
│   │   ├── approve-dialog
│   │   └── reject-dialog (with reasons)
│   │
│   ├── reports
│   │   ├── dashboard
│   │   │   ├── metric-cards
│   │   │   └── charts
│   │   ├── export-excel
│   │   │   ├── filters-section
│   │   │   ├── preview-table
│   │   │   └── export-button
│   │   └── export-pdf
│   │       ├── visit-selector
│   │       ├── preview-section
│   │       └── download-button
│   │
│   ├── audit-logs
│   │   ├── filters-bar
│   │   ├── data-table
│   │   ├── details-modal
│   │   └── export-button
│   │
│   ├── schools-management (admin)
│   │   ├── schools-list (table)
│   │   ├── search-bar
│   │   ├── add-school-button
│   │   ├── school-form (modal)
│   │   └── school-detail-modal
│   │
│   ├── users-management (admin)
│   │   ├── users-list (table)
│   │   ├── search-bar
│   │   ├── add-user-button
│   │   ├── user-form (modal)
│   │   └── user-detail-modal
│   │
│   └── settings (admin)
│       ├── system-config-form
│       ├── email-config-form
│       ├── notification-settings
│       └── gps-settings
│
└── footer
```

---

## KEY PAGE LAYOUTS

### Engineer App - Login Page
```
┌─────────────────────────────────────────┐
│     School DMS Logo                     │
│                                         │
│     ╔═══════════════════════════╗      │
│     ║   Sign In                 ║      │
│     ╠═══════════════════════════╣      │
│     ║ Email:                    ║      │
│     ║ ┌─────────────────────────┐║      │
│     ║ │                         ││      │
│     ║ └─────────────────────────┘║      │
│     ║                           ║      │
│     ║ Password:                 ║      │
│     ║ ┌─────────────────────────┐║      │
│     ║ │                         ││      │
│     ║ └─────────────────────────┘║      │
│     ║                           ║      │
│     ║ ☐ Remember me            ║      │
│     ║ ┌─────────────────────────┐║      │
│     ║ │     SIGN IN             ││      │
│     ║ └─────────────────────────┘║      │
│     ║                           ║      │
│     ║ Forgot password? | Sign up ║      │
│     ╚═══════════════════════════╝      │
│                                         │
└─────────────────────────────────────────┘
```

### Engineer App - Dashboard
```
┌──────────────────────────────────────────────┐
│ ☰  Engineer Dashboard        👤 Profile 🔔 │
├──────────────────────────────────────────────┤
│ Good Morning, John!                          │
│ Tuesday, January 15, 2024 | 9:30 AM          │
│                                              │
│ ┌──────────┐ ┌──────────┐ ┌──────────┐     │
│ │Visits    │ │Completed │ │Pending   │     │
│ │Today     │ │          │ │Approval  │     │
│ │    5     │ │    3     │ │    2     │     │
│ └──────────┘ └──────────┘ └──────────┘     │
│                                              │
│ ┌────────────────────────────────────────┐  │
│ │ Recent Visits                          │  │
│ ├────────────────────────────────────────┤  │
│ │ Delhi Public School      Installation │  │
│ │ 28 Jan, 2024            ✓ Approved   │  │
│ │                                  VIEW │  │
│ ├────────────────────────────────────────┤  │
│ │ St. Xavier High School   PMS         │  │
│ │ 27 Jan, 2024            🕐 Pending   │  │
│ │                                  VIEW │  │
│ └────────────────────────────────────────┘  │
│                                              │
│ ┌──────────┐ ┌──────────┐ ┌──────────┐    │
│ │+ NEW     │ │REJECTION │ │PROFILE   │    │
│ │VISIT     │ │FEEDBACK  │ │SETTINGS  │    │
│ └──────────┘ └──────────┘ └──────────┘    │
│                                              │
└──────────────────────────────────────────────┘
```

### Engineer App - Visit List
```
┌─────────────────────────────────────────────┐
│ ☰  My Visits              👤 Profile 🔔   │
├─────────────────────────────────────────────┤
│ Filters: [Status ▼] [Date ▼] [School ▼]    │
│ Search: [Search schools...          ]       │
│                                             │
│ ┌─────────────────────────────────────────┐│
│ │ Delhi Public School                     ││
│ │ Smart Classroom | Installation & Demo   ││
│ │ 28 Jan, 2024 | ✓ Approved              ││
│ │                                  VIEW   ││
│ └─────────────────────────────────────────┘│
│                                             │
│ ┌─────────────────────────────────────────┐│
│ │ St. Xavier High School                  ││
│ │ Language Lab | PMS                      ││
│ │ 27 Jan, 2024 | 🕐 Pending               ││
│ │                                  VIEW   ││
│ └─────────────────────────────────────────┘│
│                                             │
│ ┌─────────────────────────────────────────┐│
│ │ ABC School                              ││
│ │ Vocational Lab | Service Complaint      ││
│ │ 26 Jan, 2024 | ✗ Rejected (Re-do)       ││
│ │                            VIEW | EDIT   ││
│ └─────────────────────────────────────────┘│
│                                             │
│ Page 1 of 5 | < 1 2 3 4 5 >               │
│                                             │
└─────────────────────────────────────────────┘
```

### Engineer App - GPS Check-in
```
┌──────────────────────────────────┐
│ ← School Check-in               │
├──────────────────────────────────┤
│                                  │
│  Your Location:                  │
│  📍 28.5935°N, 77.3910°E         │
│  Accuracy: ±15 meters            │
│                                  │
│  [📍 GET CURRENT LOCATION]       │
│                                  │
│  School Location:                │
│  📌 28.5935°N, 77.3910°E         │
│  Distance: 0.2 km away           │
│                                  │
│  ┌──────────────────────────────┐│
│  │  [MAP VIEW]                  ││
│  │  🔵 You  🔴 School           ││
│  │  ├─── Distance: 200m          ││
│  └──────────────────────────────┘│
│                                  │
│  School Signboard Photo:         │
│  [📸 TAKE PHOTO]                 │
│  [Thumb: School name + UDISE]    │
│  [RETAKE] [CONFIRM]              │
│                                  │
│  ✓ GPS Verified                  │
│  ✓ Photo Captured                │
│                                  │
│  [CONTINUE TO DOCUMENTS]         │
│                                  │
└──────────────────────────────────┘
```

### Engineer App - Document Upload
```
┌───────────────────────────────────────┐
│ ← Document Upload                     │
├───────────────────────────────────────┤
│                                       │
│ Before Installation Photo (Required)  │
│ ┌─────────────────────────────────┐  │
│ │ ⬆️  Drag & drop or click        │  │
│ │ ┌─────────┐                     │  │
│ │ │Thumb:   │  [Delete]           │  │
│ │ │Before   │                     │  │
│ │ └─────────┘ ✓ Uploaded          │  │
│ └─────────────────────────────────┘  │
│                                       │
│ After Installation Photo (Required)   │
│ ┌─────────────────────────────────┐  │
│ │ ⬆️  Drag & drop or click        │  │
│ │ [Upload area empty]             │  │
│ └─────────────────────────────────┘  │
│                                       │
│ Serial Number Image (Optional)        │
│ ┌─────────────────────────────────┐  │
│ │ ⬆️  Drag & drop or click        │  │
│ │ [Upload area empty]             │  │
│ └─────────────────────────────────┘  │
│                                       │
│ Installation Report (Required)        │
│ ┌─────────────────────────────────┐  │
│ │ ⬆️  Drag & drop or click        │  │
│ │ [Upload area empty]             │  │
│ └─────────────────────────────────┘  │
│                                       │
│ Mandatory Checklist:                  │
│ ✓ Before Photo                        │
│ ✗ After Photo                         │
│ - Serial Number                       │
│ ✗ Certificate                         │
│ - Notes                               │
│                                       │
│ [UPLOAD MORE] [CONTINUE]              │
│                                       │
└───────────────────────────────────────┘
```

### Ops Portal - Dashboard
```
┌─────────────────────────────────────────────────────────────┐
│ ☰  Dashboard          📊 Admin | Filter: [Jan 2024 ▼]      │
├─────────────────────────────────────────────────────────────┤
│                                                             │
│ ┌──────────────┐ ┌──────────────┐ ┌──────────────┐ ┌──────┐
│ │ Total Visits │ │ Pending      │ │ Approved     │ │Reject│
│ │ This Month   │ │ Verification │ │ Visits       │ │Rate  │
│ │    247 ↑     │ │     23       │ │    215  ✓    │ │ 5%   │
│ │    +12%      │ │  [VIEW >]    │ │              │ │      │
│ └──────────────┘ └──────────────┘ └──────────────┘ └──────┘
│                                                             │
│ ┌────────────────────────┐  ┌────────────────────────┐    │
│ │ Visits by District     │  │ Visits by Status       │    │
│ │ ┌──────────────────┐   │  │ ┌─────────────────┐    │    │
│ │ │▮▮▮▮▮▮ Delhi   180│   │  │ │⬤ Approved 215  │    │    │
│ │ │▮▮▮▮ UP          95│   │  │ │⬤ Rejected   12  │    │    │
│ │ │▮▮▮ Punjab      45│   │  │ │⬤ Pending    23  │    │    │
│ │ │▮▮ Haryana      32│   │  │ └─────────────────┘    │    │
│ │ └──────────────────┘   │  │                        │    │
│ └────────────────────────┘  └────────────────────────┘    │
│                                                             │
│ ┌────────────────────────────────────────────────────┐    │
│ │ Daily Submission Trend (Last 30 Days)              │    │
│ │  📈                                                │    │
│ │  40 ┤        ╱╲      ╱╲                           │    │
│ │  35 ┤      ╱   ╲    ╱   ╲                         │    │
│ │  30 ┤    ╱      ╲╱       ╲                        │    │
│ │  25 ┤  ╱                   ╲╱╲                    │    │
│ │     └────────────────────────────→ Days           │    │
│ └────────────────────────────────────────────────────┘    │
│                                                             │
└─────────────────────────────────────────────────────────────┘
```

### Ops Portal - Pending Verification
```
┌──────────────────────────────────────────────────────────┐
│ ☰  Pending Verification (23)      📊 Admin | [Filter ▼] │
├──────────────────────────────────────────────────────────┤
│ District: [All ▼] Block: [All ▼] Visit Type: [All ▼]    │
│ Date: [All time ▼] Engineer: [Search...    ] [Clear X]   │
│                                                          │
│ ☐ Select All                                            │
│ [APPROVE SELECTED] [REJECT SELECTED]                    │
│                                                          │
│ ┌────────────────────────────────────────────────────┐  │
│ │☐│ID  │School        │UDISE   │Engineer│Type  │Pending│
│ ├─┼────┼───────────────┼────────┼────────┼──────┼───────┤
│ │☐│245 │Delhi Public..│UP000001│John    │Inst. │2 days │
│ │  │    │              │        │        │      │ [VIEW]│
│ ├─┼────┼───────────────┼────────┼────────┼──────┼───────┤
│ │☐│244 │St. Xavier     │UP000002│Sarah   │PMS   │1 day  │
│ │  │    │High School    │        │        │      │ [VIEW]│
│ ├─┼────┼───────────────┼────────┼────────┼──────┼───────┤
│ │☐│243 │ABC School     │UP000003│Mike    │Compl.│3 hrs  │
│ │  │    │               │        │        │      │ [VIEW]│
│ └─┴────┴───────────────┴────────┴────────┴──────┴───────┘
│                                                          │
│ Page 1 of 3 | [< 1 2 3 >]                               │
│                                                          │
└──────────────────────────────────────────────────────────┘
```

### Ops Portal - Visit Review
```
┌────────────────────────────────────────────────────────────┐
│ ← Delhi Public School (UDISE: UP000001) | ✓ APPROVED      │
├────────────────────────────────────────────────────────────┤
│                                                            │
│ ┌─────────────────────────┐ ┌────────────────────────┐   │
│ │ School Information      │ │ Document Gallery       │   │
│ ├─────────────────────────┤ ├────────────────────────┤   │
│ │ School: Delhi Public... │ │ ┌───┐ ┌───┐ ┌───┐     │   │
│ │ UDISE: UP000001         │ │ │ B │ │ A │ │ S │     │   │
│ │ District: Delhi         │ │ │   │ │   │ │   │     │   │
│ │ Block: Central Delhi    │ │ │PRE │ │AFT│ │SER│     │   │
│ │ Address: 123 Main St    │ │ └───┘ └───┘ └───┘     │   │
│ │ Contact: John +9876.... │ │ ┌───┐ ┌───┐           │   │
│ │                         │ │ │ C │ │ N │           │   │
│ │ Engineer Information    │ │ │   │ │   │           │   │
│ ├─────────────────────────┤ │ │CER │ │NOT│           │   │
│ │ Engineer: Mike          │ │ └───┘ └───┘           │   │
│ │ Email: mike@...         │ │                        │   │
│ │ Phone: +987654...       │ │ [Fullscreen Viewer]    │   │
│ │ Performance: 95%        │ │                        │   │
│ │                         │ │ Selected Image:        │   │
│ │ Visit Details           │ │ [BEFORE PHOTO]         │   │
│ ├─────────────────────────┤ │ [Large preview]        │   │
│ │ Visit Type: Installation│ │ [Zoom | Fullscreen]    │   │
│ │ Date: 28 Jan, 2024      │ │                        │   │
│ │ Time: 10:30 AM          │ │ ✓ Before Photo        │   │
│ │ Work Completed: Yes     │ │ ✓ After Photo         │   │
│ │ Notes: "Installation... │ │ ✓ Serial Number       │   │
│ │                         │ │ ✓ Certificate         │   │
│ │ GPS Verified: ✓         │ │ - Notes               │   │
│ │ Coordinates: 28.593,... │ │                        │   │
│ └─────────────────────────┘ └────────────────────────┘   │
│                                                            │
│ [APPROVE] [REJECT]                                       │
│                                                            │
└────────────────────────────────────────────────────────────┘
```

---

## API INTEGRATION FLOW

### Authentication Flow
```
1. User enters credentials
   └─> LoginComponent
       └─> AuthService.login()
           └─> POST /api/auth/login
               ├─> Success: Store tokens
               │   ├─> localStorage['auth_token'] = accessToken
               │   ├─> localStorage['refresh_token'] = refreshToken
               │   └─> Redirect to engineer/ops dashboard
               └─> Error: Show error message

2. All subsequent requests
   └─> AuthInterceptor
       ├─> Add header: Authorization: Bearer {accessToken}
       └─> Send request
           ├─> Success: Return data
           └─> 401 Unauthorized
               └─> Call POST /api/auth/refresh
                   ├─> Success: Get new token
                   │   └─> Retry original request
                   └─> Fail: Redirect to login

3. Logout
   └─> AuthService.logout()
       └─> DELETE /api/auth/logout
           └─> Clear tokens
               └─> Redirect to login
```

### Visit Creation & Submission Flow
```
1. Engineer creates visit
   └─> VisitFormComponent
       └─> Submit form
           └─> VisitService.createVisit(formData)
               └─> POST /api/visits
                   └─> Response: Visit { id, status: 'Draft' }
                       └─> Route to visit detail

2. Engineer adds documents
   └─> DocumentUploadComponent
       └─> Upload file
           └─> DocumentService.uploadDocument(visitId, file)
               └─> POST /api/documents/upload (multipart)
                   └─> Response: Document { id, fileUrl }
                       └─> Add to visit documents list

3. Engineer checks in with GPS
   └─> GPSCheckinComponent
       └─> Get location + take photo
           └─> VisitService.checkIn(visitId, lat, lng, photoUrl)
               └─> PATCH /api/visits/{id}/check-in
                   └─> Response: Visit { isGpsVerified: true }

4. Engineer submits visit
   └─> VisitSubmissionComponent
       └─> Validate all documents uploaded
           └─> VisitService.submitVisit(visitId)
               └─> PATCH /api/visits/{id}/submit
                   └─> Response: Visit { status: 'Submitted' }
                       └─> Show success message
                           └─> Route to visit list
```

### Approval Workflow
```
1. Ops view pending visits
   └─> PendingVerificationComponent
       └─> ApprovalService.getPendingVisits()
           └─> GET /api/visits/pending-verification
               └─> Response: Visit[]

2. Ops review visit
   └─> VisitReviewComponent
       └─> Get visit detail + documents
           └─> VisitService.getVisitDetail(visitId)
               └─> GET /api/visits/{id}
                   └─> Response: Visit { documents[], approvalStatus }

3. Ops approve visit
   └─> ApproveDialog
       └─> ApprovalService.approveVisit(visitId)
           └─> POST /api/approvals/{id}/approve
               └─> Response: Approval { status: 'Approved' }
                   └─> Update visit status
                       └─> Show success + navigate

4. Ops reject visit
   └─> RejectDialog
       └─> Select rejection reasons
           └─> ApprovalService.rejectVisit(visitId, reasons)
               └─> POST /api/approvals/{id}/reject
                   └─> Response: Approval { status: 'Rejected' }
                       └─> Send notification to engineer
                           └─> Engineer can re-submit
```

---

## SERVICE STRUCTURE & API CALLS

### AuthService
```typescript
login(email, password): Observable<LoginResponse>
  └─> POST /api/auth/login

register(data): Observable<User>
  └─> POST /api/auth/register

refresh(): Observable<AuthToken>
  └─> POST /api/auth/refresh

logout(): Observable<void>
  └─> POST /api/auth/logout

verifyToken(): Observable<boolean>
  └─> GET /api/auth/verify-token
```

### VisitService
```typescript
getAllVisits(filters): Observable<Visit[]>
  └─> GET /api/visits?status=...&date=...

getVisitById(id): Observable<Visit>
  └─> GET /api/visits/{id}

createVisit(data): Observable<Visit>
  └─> POST /api/visits

updateVisit(id, data): Observable<Visit>
  └─> PUT /api/visits/{id}

deleteVisit(id): Observable<void>
  └─> DELETE /api/visits/{id}

checkIn(id, lat, lng, photoUrl): Observable<Visit>
  └─> PATCH /api/visits/{id}/check-in

submitVisit(id, workCompleted, notes): Observable<Visit>
  └─> PATCH /api/visits/{id}/submit

getEngineerVisits(engineerId): Observable<Visit[]>
  └─> GET /api/visits/by-engineer/{id}

getPendingVerification(): Observable<Visit[]>
  └─> GET /api/visits/pending-verification
```

### DocumentService
```typescript
uploadDocument(visitId, file, type): Observable<Document>
  └─> POST /api/documents/upload (multipart)

getDocumentsByVisit(visitId): Observable<Document[]>
  └─> GET /api/documents/by-visit/{id}

deleteDocument(id): Observable<void>
  └─> DELETE /api/documents/{id}

downloadDocument(id): Observable<Blob>
  └─> GET /api/documents/{id}/download
```

### ApprovalService
```typescript
approveVisit(visitId): Observable<Approval>
  └─> POST /api/approvals/{id}/approve

rejectVisit(visitId, reasons, comments): Observable<Approval>
  └─> POST /api/approvals/{id}/reject

getApprovalStatus(visitId): Observable<Approval>
  └─> GET /api/approvals/{id}
```

### ReportService
```typescript
getDashboardMetrics(filters): Observable<DashboardMetrics>
  └─> GET /api/reports/dashboard

exportToExcel(filters): Observable<Blob>
  └─> GET /api/reports/export-excel

generatePDF(visitIds): Observable<Blob>
  └─> GET /api/reports/generate-pdf

generateMergedPDF(visitIds): Observable<Blob>
  └─> GET /api/reports/generate-merged-pdf
```

---

## STATE MANAGEMENT (RxJS)

### AuthStateService
```typescript
currentUser$: BehaviorSubject<User>
isAuthenticated$: BehaviorSubject<boolean>
userRole$: BehaviorSubject<UserRole>

setUser(user: User): void
setAuthenticated(value: boolean): void
getCurrentUser(): User
getUserRole(): UserRole
```

### LoadingService
```typescript
isLoading$: BehaviorSubject<boolean>
setLoading(value: boolean): void
getLoading$(): Observable<boolean>
```

### NotificationService
```typescript
showSuccess(message: string): void
showError(message: string): void
showWarning(message: string): void
showInfo(message: string): void
```

---

## FORM VALIDATION

### Reactive Forms with Validation

```typescript
// Engineer Registration Form
registrationForm = this.fb.group({
  firstName: ['', Validators.required],
  lastName: ['', Validators.required],
  email: ['', [Validators.required, Validators.email]],
  password: ['', [Validators.required, Validators.minLength(8), this.passwordStrengthValidator]],
  confirmPassword: ['', Validators.required],
  phone: ['', Validators.pattern(/^[0-9]{10}$/)],
}, {
  validators: this.passwordMatchValidator
});
```

### Custom Validators
```typescript
passwordStrengthValidator(control: AbstractControl): ValidationErrors | null {
  // Must contain uppercase, lowercase, number, special char
}

passwordMatchValidator(group: FormGroup): ValidationErrors | null {
  // Password must match confirmPassword
}

fileTypeValidator(allowedTypes: string[]): ValidatorFn {
  // Validate file type
}
```

---

## ERROR HANDLING

### Global Error Interceptor
```typescript
intercept(req, next):Observable<HttpEvent<any>> {
  return next.handle(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401) {
        // Unauthorized - redirect to login
        this.authService.logout();
        this.router.navigate(['/login']);
      } else if (error.status === 403) {
        // Forbidden - show permission error
        this.notificationService.showError('Access denied');
      } else if (error.status === 404) {
        // Not found
        this.notificationService.showError('Resource not found');
      } else if (error.status === 500) {
        // Server error
        this.notificationService.showError('Server error occurred');
      }
      return throwError(() => error);
    })
  );
}
```

---

## ROUTER CONFIGURATION

```typescript
// App routing
const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  {
    path: 'auth',
    loadChildren: () => import('./modules/auth/auth.module')
      .then(m => m.AuthModule)
  },
  {
    path: 'engineer',
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: ['Engineer'] },
    loadChildren: () => import('./modules/engineer/engineer.module')
      .then(m => m.EngineerModule)
  },
  {
    path: 'ops',
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: ['OpsVerifier'] },
    loadChildren: () => import('./modules/ops/ops.module')
      .then(m => m.OpsModule)
  },
  {
    path: 'admin',
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: ['Admin'] },
    loadChildren: () => import('./modules/admin/admin.module')
      .then(m => m.AdminModule)
  },
  { path: '**', redirectTo: '/engineer' }
];
```

---

## PERFORMANCE OPTIMIZATION

### Change Detection Strategy
```typescript
@Component({
  selector: 'app-visit-card',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class VisitCardComponent {
  @Input() visit: Visit;
  // Only updates when input changes
}
```

### TrackBy in *ngFor
```html
<div *ngFor="let visit of visits; trackBy: trackByVisitId">
  {{ visit.name }}
</div>

trackByVisitId(index: number, visit: Visit): number {
  return visit.id;
}
```

### HTTP Caching
```typescript
getVisits(): Observable<Visit[]> {
  return this.http.get<Visit[]>('/api/visits').pipe(
    shareReplay(1) // Cache the result
  );
}
```

---

## TESTING SETUP

### Unit Test Example
```typescript
describe('VisitService', () => {
  let service: VisitService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [VisitService],
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(VisitService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should fetch visits', () => {
    const mockVisits = [
      { id: 1, schoolName: 'School 1' },
      { id: 2, schoolName: 'School 2' }
    ];

    service.getAllVisits().subscribe(visits => {
      expect(visits.length).toBe(2);
    });

    const req = httpMock.expectOne('/api/visits');
    expect(req.request.method).toBe('GET');
    req.flush(mockVisits);
  });
});
```

---

## DEPLOYMENT CHECKLIST

- [ ] Environment configuration set correctly
- [ ] API URLs updated for production
- [ ] JWT secret key handled securely
- [ ] CORS configured for production domain
- [ ] HTTPS enforced
- [ ] Analytics configured
- [ ] Error logging enabled
- [ ] Service workers configured
- [ ] Bundle optimization complete
- [ ] Lazy loading working
- [ ] Images optimized
- [ ] Caching headers configured
- [ ] Security headers added
- [ ] CORS headers configured
- [ ] Content Security Policy set
- [ ] Performance monitoring enabled

---

## NEXT STEPS

1. **Copy the Gemini Prompt** from `Gemini_Prompt_Complete_Angular_Frontend.md`
2. **Generate Complete Frontend** using Gemini AI
3. **Set up Angular Project** with generated files
4. **Update Environment Configuration** with API URLs
5. **Run Development Server** (`ng serve`)
6. **Test All Features** with running backend
7. **Deploy to Production**

---

**Your complete Angular frontend is ready to be generated!** 🚀
