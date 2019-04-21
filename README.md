# DietTracker Mobile

DietTracker is an application that is used as a diary to track both consumed food items and weight.  The app makes use of the USDA Food Composition database to provide nutrition information and allow accurate tracking of food consumed each day.  A daily calander allows for entry of food items into four seperate meals (breakfast, lunch, dinner and snack).  Any items found in a search of the food composition database can also be added to a stored list of favorite foods that allows for eaiser retrevial in the future.


# Background

DietTracker was orginally developed for the class CYBR 8470 - Secure Web Developlment.  It consisted of a web application and a REST API. The DietTracker Mobile application was proposed as an independent study project.  The goal of this project was to develop an application that builds upon the previous work.  The proposal called for a Raspberry Pi with an attached touch screen to be used as the hardware and for Windows Iot to be used for the operating system. 

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

This project makes use of a Raspberry PI 3 and a 7" touchscreen panel. Windows IOT build xxxx is installed as the operating system.

Note: Windows IOT build xxxxx is not compatible with the Raspbery PI 3B.  This discovered the hard way.  That version of Windows IOT is compatabile with late model Raspbery PI 2 devices and early model 3 devices.

# Deployment





# Architecture Diagrams

### System Context Diagram
<img src="https://github.com/jfranco1701/DietTracker/blob/master/docs/System Context Diagram.jpg" width="65%">

### Container Diagram
<img src="https://github.com/jfranco1701/DietTracker/blob/master/docs/Container Diagram.png" width="100%">

### Single-Page Application Component Diagram
<img src="https://github.com/jfranco1701/DietTracker/blob/master/docs/SPA Component Diagram.png" width="100%">








