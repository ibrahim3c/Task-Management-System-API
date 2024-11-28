# Task Management System API

## Overview
The **Task Management System API** is designed to help users manage tasks within a project-based structure. It includes features such as user roles, task status tracking, integrations with third-party services, and logging using Serilog for better diagnostics.

---

## Features
- **Task and Project Management:**
  - CRUD operations for tasks and projects.
  - Task status tracking (e.g., Pending, In Progress, Completed).
- **User Authentication:**
  - JWT-based authentication.
  - Repository Pattern With Unit of Work
  - JWT Refresh Token
  - Role-based access control (Admin, User).
- **Notifications:**
  - **SendGrid** integration for email notifications.
  - **Twilio** integration for phone notifications.
- **Logging:**
  - Integrated **Serilog** for structured and file-based logging.

---

## Architecture

```plaintext
+-------------------------------+                     +----------------------------------+
|           API Gateway         |                     | Notification Service / |
+-------------------------------+                     | SendGrid)                       |
              |                                     / +----------------------------------+
              v                                    /
+-------------------------------+                 /
| Authentication Service (JWT)  | <--------------/
+-------------------------------+               /
              |                                 /
              v                                /
+-------------------------------+             /
|          User Service         | <----------/
+-------------------------------+           /
              |                             /
              v                            /
+-------------------------------+         /
|         Project Service       | <------/
+-------------------------------+       /
              |                         /
              v                        /
+-------------------------------+     /
|          Task Service        | <--/
+-------------------------------+     
              |                         /
              v                        /
+-------------------------------+     /
|          File Service         | <--/
+-------------------------------+
              |
              v
+-------------------------------+
| Unit of Work & Repository     |
| (Abstraction Layer)           |
+-------------------------------+
              |
              v
+-------------------------------+
| Database (SQL Server)  |
+-------------------------------+
              ^
              |
              |
  +----------------------------------+


 
