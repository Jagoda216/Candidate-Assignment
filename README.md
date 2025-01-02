# Candidate-Assignment
 Client/server application that tracks quantity of products in their warehouse for each of their suppliers.

## Getting Started

Follow these steps to set up and run the project:

### 1. Clone the Repository
Clone the repository to your local machine:

git clone https://github.com/Jagoda216/Candidate-Assignment.git
cd Candidate-Assignment

### 2. Start the Server
Navigate to: Candidate-Assignment\Server\bin\Debug\net8.0\Server.exe

### 3. Start the Client
Navigate to: Candidate-Assignment\Client\bin\Debug\net8.0-windows\Client.exe

## Overview

This project consists of two main components:

ASP.NET Core Web Application MVC

Backend API server with database and SignalR integration.
Includes a web page (Index.cshtml) for real-time updates.

WPF MVVM Application

Client application using WPF, following the MVVM pattern, to interact with the backend API.

## Configuration:
Database Hosting: The PostgreSQL database is hosted on Railway.

## HTTPS Certificate Information
The ASP.NET Core backend is already configured with a trusted HTTPS development certificate. No additional steps are required to configure the HTTPS certificate.

If you encounter issues related to HTTPS connections, ensure the following:

Your local development environment recognizes the trusted HTTPS certificate.
If necessary, manually trust the development certificate by running:
dotnet dev-certs https --trust

## Test Users
To facilitate testing of the application, I have pre-created four users in the database. You can use these credentials to log in and test the application's functionality.

Username	Password
Ikea	test1234
Bauhaus	1234test
Lesnina	testTest
Jysk	Testtest
Use these accounts to log in to the WPF client application.
