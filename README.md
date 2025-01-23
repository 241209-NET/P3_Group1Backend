# PLEY: The Reverse Yelp App

## Overview
PLEY is a unique app that lets stores rate and review their customers. It’s a helpful tool for businesses to share feedback with each other about customer behavior, making it easier to spot and avoid problematic customers. By working together, stores can create a better experience for everyone and keep their spaces safe and enjoyable.

## Project Management System
Trello

## Members
- Ludia Park
- Joseph Savage
- Praween Pongpat
- Eldhose Salby
- Rohit Rathor
- Kashyap Bathani
- Justin Theyskens

## ERD

![image](https://github.com/user-attachments/assets/4fa443e4-ba61-46fd-ae8e-600c7e0a6563)



## User Stories
- Stores should be able to create a new Store Profile
- stores can edit their login info
- Stores can create review (rating + comment).
- Stores can edits reviews
- Stores can delete reviews
- Stores should be able to find Customers by Id
- Stores should be able to see all customers

## MVP
- implement CI/CD with Github Actions
- dockerize the app
- create Store Profile
- login authentication 
- edit login for store
- create / edit / delete reviews
- create Customer Profile
- get customer by Id
- get all customers
- sort customers by ratings
- Stores can sort customers by ratings and by number of reviews

  
## Requirements
- Application Must build and run
- Unit Testing (70% branch coverage for Services and Utilities/Business Logic)
- CI/CD Pipeline
- Implement Secure Authentication (user login / registration)
- Frontend
    - React
    - Styling: plain CSS, Tailwind, bootstrap, etc.
    - Hosted on Azure
- Backend
    - ASP.NET API
    - Docker containerized
    - Hosted on Azure
- SQLServer DB hosted on Azure


## Technology
- C# (Backend programming language)
- EF Core (ORM)
- SQL Server (Azure hosted)
- ASP.NET Core (Web API Framework)
- xUnit/Moq (Backend Testing)
- Azure (for application hosting)
- React as front end
- Github Action or Azure Pipeline for CICD Pipeline
- Docker for containerization

## Stretch goals
- Make it pretty
- sort customers by number of reviews
- display top customers (hall of fame)
- display worst customers (hall of shame)
- Adding address of Store and display it on google map