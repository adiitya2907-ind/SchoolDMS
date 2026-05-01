# Document Management System (DMS) + Field Engineer App - Requirements Analysis

## Executive Summary
A two-tier system combining:
1. **Field Engineer Mobile App** - Daily work assistant for school visits and documentation
2. **Ops Team Web Portal** - Verification, audit, and reporting system

---

## SECTION 1: DOCUMENT MANAGEMENT SYSTEM (DMS) - OVERALL CONCEPT

### Business Context
**Objective:** End-to-end storage and retrieval system for:
- Installation Reports (IR)
- Delivery Challans (DC)

### Key Requirements (DMS Layer)
1. **PAN-India Coverage** - Multi-location stakeholder access
2. **High Volume Handling** - Support large-scale document storage
3. **Text & Image Recognition** - OCR capabilities for document indexing
4. **Multi-User Access** - Role-based access (Engineers, Ops Team, Vendors)
5. **Vendor Access** - External stakeholder visibility
6. **Folder Structure** - Organized document hierarchy
7. **Advanced Search & Indexing** - Quick retrieval of documents
8. **Document Lifecycle Management** - Track document status/history
9. **Edit & Security Features** - Document protection and audit trails

### Current Practice & Challenges
*(To be discussed in customer meeting)*
- Current manual/existing processes
- Pain points in document storage
- Retrieval difficulties
- Security/compliance concerns

---

## SECTION 2: FIELD ENGINEER APP (Mobile Application)

### App Philosophy
**"Daily work assistant for the engineer"** - Simple, field-friendly interface

### 2.1 Core Feature 1: Project Details

**Purpose:** Engineer knows which project/equipment type they're installing

**Project Types Supported:**
- Smart Classroom
- ISM (Interactive Smart Module)
- PM Shri Scheme Phase 1
- PM Shri Scheme Phase 2
- Language Lab
- Vocational Lab

**Implementation:** Dropdown/selection list before visiting school

---

### 2.2 Core Feature 2: School Visit List

**Purpose:** Daily visit schedule for the engineer

**Data Fields Displayed:**
- **District** - Geographic location
- **Block** - Sub-region
- **UDISE Code** - Unique School ID (Ministry database identifier)
- **School Name** - Display name
- **Visit Type** - Purpose of visit (5 types):
  1. **Installation & Demonstration** - New equipment setup + training
  2. **PMS (Pre-Maintenance Service)** - Preventive maintenance
  3. **Service Complaint** - Reactive maintenance
  4. **Performance Certificate** - Documentation of working condition
  5. **Site Inspection** - Audit/verification visit
  6. **Content Training** - Software/usage training

**Implementation:** List view with filters (District, Block, Visit Type)

---

### 2.3 Core Feature 3: School Visit Check-in (Geolocation Proof)

**Trigger:** When engineer selects a school and marks "Arrived"

**Mandatory Requirement:**
- ✅ **GPS Check-in is MANDATORY**
  - Captures latitude/longitude
  - Time-stamped
  - Used for fraud detection

**Photo Capture Required:**
- **School Sign Board Photo**
  - Must show UDISE code clearly
  - Proof of actual location
  - Cannot proceed without this

**Purpose:** Prevent fake visits, verify actual school presence

---

### 2.4 Core Feature 4: Document Collection (Structured DMS for Schools)

**Critical Concept:** Engineer uploads documents in CATEGORIZED manner (not random)

#### Document Upload Matrix:

| Document Type | Mandatory | Description | Purpose |
|---|---|---|---|
| **Before Installation/Repair Photo** | ✅ YES | Photo of school/location before work | Baseline documentation |
| **Installation/After Repair Photo** | ✅ YES | Device installed in classroom/lab with visible setup | Proof of installation |
| **Serial Number Image** | ❌ NO | Close-up of device serial number label | Equipment identification |
| **IR/Training/Performance Certificate** | ✅ YES | Installation Report or Training Certificate or Performance Cert | Official documentation |
| **Engineer Notes** | ❌ NO | Text notes about work done, observations, issues | Additional context |

**Rules:**
- Each document MUST be uploaded to correct category
- Cannot upload random files
- Mandatory documents block completion
- Document validation (blurriness, completeness)

**Implementation:** Tab-based/card-based UI with upload fields for each type

---

### 2.5 Core Feature 5: Visit Summary Form

**Final Step Before Submission**

**Field:**
- **Work Completed?** (Yes/No radio button)

**Logic:**
- If NO → Ask reason (dropdown/text)
- If YES → Ready for submission

**Purpose:** Quick status check before ops review

---

## SECTION 3: OPS TEAM VERIFICATION SYSTEM (Web Portal)

### Philosophy
**"Audit System"** - Comprehensive review and approval of field visits

---

### 3.1 Feature 1: School Visit Dashboard (Analytics/Overview)

**KPI Metrics Displayed:**
1. **Total Schools Visited** - Count of all visits entered
2. **Pending Verification** - Count of visits awaiting ops review
3. **Completed Visits** - Count of approved & closed visits
4. **Repeat Visits** - Count of same school visited multiple times

**Purpose:** High-level overview for ops manager

---

### 3.2 Feature 2: Approval Workflow (Audit & Rejection)

**Two-Step Process:**

#### Option A: ✅ APPROVE
- **Action:** Visit is marked as CLOSED
- **Status:** Locked from further editing
- **Outcome:** School visit is finalized in system

#### Option B: ❌ REJECT
- **Action:** Visit reverted back to engineer
- **Status:** Engineer gets notification to re-do/fix
- **Outcome:** Engineer must resubmit with corrections

**Rejection Reasons (Dropdown/Checklist):**
1. **Wrong School** - Engineer visited different school than assigned
2. **Fake Visit (GPS Mismatch)** - GPS location doesn't match school address
3. **Blurry Photos** - Document photos not clear/legible
4. **Missing Device Proof** - Serial number or device images absent
5. **Incorrect Serial Number** - Wrong device installed or recorded
6. **Training Pending** - Training not completed
7. **Faulty/Damaged/Missing Item** - Equipment issues not documented

**Implementation:** Rejection form with checkboxes + optional comments field

---

## SECTION 4: REPORTING & EXPORT FEATURES

### 4.1 Excel Export (Data Download)

**Purpose:** Summary data extraction for analysis

**Capability:**
- Ops team can generate custom data summaries
- Download as Excel format
- Include filters (date range, district, visit type, status, etc.)

**Expected Data:**
- School details
- Visit type
- Visit date
- Engineer name
- Status (Approved/Rejected/Pending)
- Rejection reasons (if any)

---

### 4.2 PDF Report Generation (Installation Reports)

#### Single School Report:
- **Format:** PDF
- **Content:** Individual IR/DC for one school
- **Page Limit:** 1-2 pages per school
- **Includes:** All visit details, photos (thumbnails), summary

#### Multiple Schools Merged Report:
- **Capability:** Download multiple schools' IRs in ONE merged PDF
- **Organization:** Each school section clearly separated
- **Page Management:** Each school limited to 1-2 pages
- **Outcome:** Single consolidated PDF instead of multiple files

**Example:** "Download IR for 15 schools in District X as single PDF"

---

## SECTION 5: SYSTEM ARCHITECTURE PERSPECTIVE

### Three Distinct Components

#### A. Field Engineer Mobile App (Android/iOS)
- **Users:** Field engineers
- **Purpose:** Daily visit documentation
- **Features:** Check-in, photo upload, form submission
- **Connectivity:** Works online/offline (sync when online)
- **Interaction:** Simple, fast, field-friendly

#### B. DMS Backend (Document Storage & Indexing)
- **Storage:** Secure file storage with OCR indexing
- **Retrieval:** Full-text search, metadata search
- **Lifecycle:** Version control, retention policies
- **Security:** Role-based access, audit logs

#### C. Ops Team Web Portal
- **Users:** Operations/verification team
- **Purpose:** Quality audit and approval
- **Features:** Dashboard, review, approval, export, reports
- **Connectivity:** Always online, web-based
- **Interaction:** Detailed review interface

---

## SECTION 6: USER ROLES & PERMISSIONS

### Role 1: Field Engineer
- **Access:** Mobile App
- **Can Do:**
  - View assigned school visits
  - Check-in with GPS
  - Upload photos & documents
  - Submit visit summary
  - View rejection feedback
- **Cannot Do:**
  - Approve/reject visits
  - Delete documents
  - Edit approved visits

### Role 2: Ops Verification Team
- **Access:** Web Portal
- **Can Do:**
  - View all submitted visits
  - Review photos & documents
  - Approve or reject with reasons
  - Generate reports & exports
  - View dashboards
  - Search documents
- **Cannot Do:**
  - Modify engineer submissions
  - Delete historical data
  - Approve their own visits

### Role 3: Vendor (Optional)
- **Access:** Web Portal (Read-only)
- **Can Do:**
  - View their device installations
  - Generate reports
  - Search by serial number
- **Cannot Do:**
  - Approve/reject
  - Modify data

---

## SECTION 7: TECHNICAL WORKFLOW FLOW

### Engineer's Workflow (Mobile App)

```
1. Login to App
   ↓
2. View "School Visit List" (assigned for today)
   ↓
3. Select Project Type (Smart Classroom, etc.)
   ↓
4. Tap School to Start Visit
   ↓
5. GPS Check-in + School Signboard Photo
   ↓
6. Complete Visit Details
   ↓
7. Upload Documents (Categorized):
   - Before photo (mandatory)
   - After photo (mandatory)
   - Serial number (optional)
   - Certificate/IR (mandatory)
   - Engineer notes (optional)
   ↓
8. Fill Visit Summary Form (Work Completed? Yes/No)
   ↓
9. Submit Visit
   ↓
10. Await Ops Verification
```

### Ops Team Workflow (Web Portal)

```
1. Login to Portal
   ↓
2. View Dashboard (metrics overview)
   ↓
3. Go to "Pending Verification" tab
   ↓
4. Select Visit to Review
   ↓
5. Review All Submitted Data:
   - GPS location
   - Photos
   - Documents
   - Notes
   ↓
6. Decision:
   A. APPROVE → Visit Closed
   B. REJECT → Select reasons → Send back to engineer
   ↓
7. Generate Reports/Exports as needed
```

---

## SECTION 8: KEY CONSTRAINTS & CONSIDERATIONS

### Data Security & Compliance
- ✅ Role-based access control
- ✅ Audit trail for all actions
- ✅ Document encryption at rest
- ✅ HTTPS for all transmissions
- ✅ Vendor access restrictions

### Performance & Scalability
- ✅ PAN-India support (multiple states/districts)
- ✅ High volume of visits (hundreds/thousands per day)
- ✅ Fast photo upload (3G/4G networks in India)
- ✅ Image optimization/compression

### Offline Capability
- ✅ Engineers should work offline and sync when connected
- ✅ Photos cached locally before upload

### Document Quality
- ✅ Image validation (not blurry)
- ✅ File size limits
- ✅ Supported formats (JPG, PNG, PDF)

---

## SECTION 9: FUTURE CONSIDERATIONS (Not in Current Scope)

### Mentioned but Needs Discussion:
- OCR capability for automatic text extraction
- Life cycle management (document retention/deletion policies)
- Advanced search with text recognition
- Mobile app offline sync strategy
- Document workflow approval chains

---

## SECTION 10: OPEN QUESTIONS FOR CUSTOMER MEETING

1. **Current System:** What system do you use now? (Excel, manual, cloud storage?)
2. **Volume:** Approximately how many visits per day/month/year?
3. **Users:** How many engineers, ops staff, vendors?
4. **Compliance:** Any regulatory requirements (GST, FSSAI, etc.)?
5. **Offline:** How long offline? What connectivity available?
6. **Timeline:** When should this be live?
7. **Budget:** Specific infrastructure preferences (Azure, AWS, on-premise)?
8. **Integration:** Any existing systems to integrate with?
9. **Document Types:** Any specific document formats/sizes?
10. **Retention:** How long to keep documents? Deletion policy?

---

## SUMMARY TABLE

| Component | Type | Users | Key Feature |
|---|---|---|---|
| **Field Engineer App** | Mobile | Engineers | Visit documentation with GPS |
| **DMS** | Backend Storage | All | Secure indexed document storage |
| **Ops Portal** | Web Application | Verification Team | Audit & approval workflow |
| **Reporting** | Export Module | Ops/Vendor | Excel & PDF generation |

---

## TECHNOLOGY STACK RECOMMENDATION (Aligned with Your Choice)

### Backend: .NET Core API ✅
- Controllers: Visit, Document, User management
- Services: Upload, Verification, Report generation
- Database: SQL Server for relational data
- Storage: Azure Blob/AWS S3 for images & PDFs
- Search: Elasticsearch for document indexing

### Frontend: Angular ✅
- **Web Portal:** Full feature set
- **Mobile:** Might use React Native/Flutter OR mobile-optimized Angular web app

### Database Design
```
Tables:
- Users (Engineer, Ops, Vendor)
- Schools (UDISE, Name, Location, Coordinates)
- Visits (School, Engineer, Date, Type, Status)
- Documents (Visit ID, Category, File URL, Upload Date)
- ApprovalWorkflow (Visit ID, Decision, Reason, Date)
```

---

## NEXT STEPS

1. ✅ Schedule customer meeting
2. ✅ Clarify current practice & pain points
3. ✅ Confirm all requirements above
4. ✅ Get answers to open questions
5. ✅ Define data retention/compliance policy
6. ✅ Discuss timeline & budget
7. ✅ Start technical architecture design
8. ✅ Create database schema
9. ✅ Develop API endpoints
10. ✅ Build mobile & web interfaces
