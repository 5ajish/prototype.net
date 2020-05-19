# Prototype
A simple and clean usage of Generic repository with .net core API and identity framework

# Implementation
1. Multi-layered architecture
2. Generic repository & Unit of work
3. Code first approach
4. REST API based on .net core
5. Identity authentication & JWT tokens
6. Example controller for login / register / get users
7. Swagger

# Architecture
1. Domain - Contains models and DTO's
2. DatabaseMigration - Contains migration script as per code first approach
3. Core - Contains generic repository (CRUD with Search, Count, Paging & Sorting) and unit of work
4. Service - Contains service classes where business logic is defined
5. API - Contains controllers

# Usage
1. Create and update database - prototype using DatabaseMigration project
2. Build and execute the project, you will see swagger page
3. Utilize method like sign up / sign in to insert user / get auth token
4. Once you get auth token, you will be able to call get methods
5. Enjoy coding
