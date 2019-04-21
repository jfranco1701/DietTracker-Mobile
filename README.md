# DietTracker-Mobile

DietTracker is an application that is used to track a user's daily intake of food and beverages. 


# Background

DietTracker was orginally developed as a web application with a REST API on the backend.  DietTracker Mobile adds a mobile based application that makes use of the same API.

# Software

### Backend
The backend for the application is a REST API that was developed using Django and the Django REST Framework.  The API makes use of Json Web Tokens (JWT) to secure backend.  It is currently being hosted on AWS and remains unchanged from when it was created in the original project.

#### Known Issues
There is one known issue with the backend.  There is a bug in the endpoint that calaculates the daily totals.  The total carbs count is always returned as 0.  Due to time constraints, this issue was not corrrected.  All focus was given to the mobile application.

#### Repository
The repository for the backend can be found here: https://github.com/jfranco1701/DietTracker-backend.

### Frontend
The mobile version of the frontend is a Universal Windows Platform (UWP) application.  UWP applications are designed to run on all of Microsoft's current family of operating systems.  This includes Windows 10, Windows IoT and Xbox.

The UI is developed using a declarative markup language called the Extensible Application Markup Language (XAML).  Each .xaml file has a corresponding .xaml.cs file that contains all of the code C# code.

The frontend application is also deployed with a SQLite database that contains a copy of the USDA Food Composition database.  Searches for food items are made against this local database and selections are then sent to the backend.

# Hardware





# Architecture Diagrams

### System Context Diagram
<img src="https://github.com/jfranco1701/DietTracker/blob/master/docs/System Context Diagram.jpg" width="65%">

### Container Diagram
<img src="https://github.com/jfranco1701/DietTracker/blob/master/docs/Container Diagram.png" width="100%">

### Single-Page Application Component Diagram
<img src="https://github.com/jfranco1701/DietTracker/blob/master/docs/SPA Component Diagram.png" width="100%">








