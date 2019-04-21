# DietTracker-Mobile

DietTracker is an application that is used to track a user's daily intake of food and beverages. 

The mobile version is a Universal Windows Platform application that can be run on a Windows 10 computer or a device running Windows IOT.  The mobile version makes use of the same backend that that is used by the Angular frontend.  The repository for the backend is located here: https://github.com/jfranco1701/DietTracker-backend.

The application contains a local copy of the USDA Food Composition database.  This data is stored in a SQLite database and deployed along with the app.  






# Background

DietTracker was orginally developed as a web application with a REST API on the backend.  DietTracker Mobile adds a mobile based application that makes use of the same API.

The mobile app was developed in 


# Software

### Backend
The backend for the application is a REST API that was developed using Django and the Django REST Framework.  The API makes use of Json Web Tokens (JWT) to secure backend.  It is currently being hosted on AWS and remains unchanged from when it was created in the original project.

#### Known Issues
There is one known issue with the backend.  There is a bug in the endpoint that calaculates the daily totals.  The total carbs count is always returned as 0.  Due to time constraints, this issue was not corrrect.  All focus was given to the mobile application.

#### Repository
The repository for the backend can be found here: https://github.com/jfranco1701/DietTracker-backend.

### Frontend

The mobile version is a Universal Windows Platform application that can be run on a Windows 10 computer or a device running Windows IOT. 

# Hardware





# Architecture Diagrams

### System Context Diagram
<img src="https://github.com/jfranco1701/DietTracker/blob/master/docs/System Context Diagram.jpg" width="65%">

### Container Diagram
<img src="https://github.com/jfranco1701/DietTracker/blob/master/docs/Container Diagram.png" width="100%">

### Single-Page Application Component Diagram
<img src="https://github.com/jfranco1701/DietTracker/blob/master/docs/SPA Component Diagram.png" width="100%">








