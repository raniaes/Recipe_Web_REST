# Recipe_Web_REST

## Introduction
This program is a dedicated REST API for a recipe web application built using the .NET Entity Framework. 
These APIs handle client requests to a Microsoft SQL Server database to Get (Read), Post (Create), Put (Update), and Delete (Delete) data. 
Additionally, the APIs are customized to suit the specific data requirements of the recipe web application, ensuring efficient and accurate data management.
Moreover, this program is a team project developed by two people, and the APIs created by the author are listed in the endpoints below.

## Structure
1. Category: This is the model for food categories, such as dinner, lunch, breakfast, dessert, etc.
2. Ingredient: This is the model for food ingredients, such as rice, water, apples, etc.
3. Recipe: This is the model for food redipe, such as cream pasta, miso soup, taco, etc.
4. Review: This is the model for food reviews, where each user's food ratings and comments are stored.
5. User: This is the model for user data.
6. Recipe_Ingredient: This model establishes a many-to-many relationship between the recipe model and the ingredient model.

![Description of Image](/dbDIagram.png)   

## REST API Documentation

### Base URL
The base URL is https://localhost:7230/api/, and it is currently running locally without deployment.

### Endpoints

#### Category

##### Get All Category
**Endpoint:** `GET /api/Category`

**Description:** Get all recipe

##### Post Category
**Endpoint:** `POST /api/Category`

**Description:** Create recipe
<br>
#### Ingredient

#### Recipe

#### Review

#### User
