Multi-Tenant CRM System (ASP.NET Core Web API)

An enterprise-grade Customer Relationship Management (CRM) backend built using ASP.NET Core Web API following Onion Architecture principles.
  
This system supports multi-tenant isolation, JWT-based authentication with refresh tokens, audit logging, and email-based user invitation workflows
designed for scalability, security, and clean separation of concerns. 
 
 Features 
  
-  Multi-Tenant Data Isolation (Tenant-based data filtering)           
-  JWT Authentication with Refresh Token Mechanism        
-  Role-Based Access Control (RBAC)    
-  Audit Logging (Create, Update, Delete tracking)     
-  Clean Onion Architecture      
-  Repository Pattern    
-  Entity Framework Core
-  Fluent Apis    
-  Soft Deletes
-  API Versioning
-  Structured Logging

Domain Layer 

Entities
Value Objects
Repository Interfaces
Domain Rules

Application Layer
DTOs
Service Interfaces 
Business Logic
Validation 

Infrastructure Layer
DbContext
Repository Implementations
Authentication Services
Audit Implementation
API Layer
Controllers
Middleware
Filters
Dependency Injection

Authentication Flow

Access Token (short-lived)
Refresh Token (long-lived)
Secure token rotation
Tokens stored securely in database
Token revocation support
Multi-Tenant Isolation
TenantId applied to all tenant-based entities
Global Query Filters enforce tenant-level isolation
Prevents cross-tenant data access
Middleware resolves tenant context per request
Audit Logging

Tracks Create, Update, and Delete operations
Captures:
UserId
TenantId

Tech Stack:
ASP.NET Core Web API
C#
Entity Framework Core
SQL Server
JWT Authentication
Onion Architecture (Clean Architecture principles)
