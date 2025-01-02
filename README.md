# Candidate-Assignment
 Client/server application that tracks quantity of products in their warehouse for each of their suppliers.

Overview

This project consists of two main components:

ASP.NET Core Web Application MVC

Backend API server with database and SignalR integration.
Includes a web page (Index.cshtml) for real-time updates.

WPF MVVM Application

Client application using WPF, following the MVVM pattern, to interact with the backend API.

Configuration:
Database Hosting: The PostgreSQL database is hosted on Railway.

HTTPS Certificate Information
The ASP.NET Core backend is already configured with a trusted HTTPS development certificate. No additional steps are required to configure the HTTPS certificate.

If you encounter issues related to HTTPS connections, ensure the following:

Your local development environment recognizes the trusted HTTPS certificate.
If necessary, manually trust the development certificate by running:
dotnet dev-certs https --trust
