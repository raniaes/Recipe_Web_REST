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

**1. Get All Categories**  
- **Endpoint:** `GET /api/Category`  
- **Description:** Retrieve all categories of recipes.

**2. Create Category**  
- **Endpoint:** `POST /api/Category`  
- **Description:** Create a new category.

#### Ingredient

**1. Get All Ingredients**  
- **Endpoint:** `GET /api/Ingredient`  
- **Description:** Retrieve all ingredients.

**2. Create Ingredient**  
- **Endpoint:** `POST /api/Ingredient`  
- **Description:** Add a new ingredient.

#### Recipe

**1. Get All Recipes**  
- **Endpoint:** `GET /api/Recipe`  
- **Description:** Retrieve all recipes.

**2. Get Recipe By ID**  
- **Endpoint:** `GET /api/Recipe/{id}`  
- **Description:** Retrieve a specific recipe by its ID.

**3. Create Recipe**  
- **Endpoint:** `POST /api/Recipe`  
- **Description:** Create a new recipe.

**4. Update Recipe**  
- **Endpoint:** `PUT /api/Recipe/{id}`  
- **Description:** Update an existing recipe by its ID.

**5. Delete Recipe**  
- **Endpoint:** `DELETE /api/Recipe/{id}`  
- **Description:** Delete a specific recipe by its ID.

#### Review

**1. Get All Reviews**  
- **Endpoint:** `GET /api/Review`  
- **Description:** Retrieve all reviews.

**2. Get Review By ID**  
- **Endpoint:** `GET /api/Review/{id}`  
- **Description:** Retrieve a specific review by its ID.

**3. Create Review**  
- **Endpoint:** `POST /api/Review`  
- **Description:** Add a new review.

**4. Update Review**  
- **Endpoint:** `PUT /api/Review/{id}`  
- **Description:** Update an existing review by its ID.

**5. Delete Review**  
- **Endpoint:** `DELETE /api/Review/{id}`  
- **Description:** Delete a specific review by its ID.

#### User

**1. Get All Users**  
- **Endpoint:** `GET /api/User`  
- **Description:** Retrieve all users.

**2. Get User By ID**  
- **Endpoint:** `GET /api/User/{id}`  
- **Description:** Retrieve a specific user by their ID.

**3. Create User**  
- **Endpoint:** `POST /api/User`  
- **Description:** Create a new user.

**4. Update User**  
- **Endpoint:** `PUT /api/User/{id}`  
- **Description:** Update an existing user by their ID.

**5. Delete User**  
- **Endpoint:** `DELETE /api/User/{id}`  
- **Description:** Delete a specific user by their ID.
