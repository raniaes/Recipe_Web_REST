# Recipe_Web_REST

## Introduction
This program is a dedicated REST API for a recipe web application built using the .NET Entity Framework. 
These APIs handle client requests to a Microsoft SQL Server database to Get (Read), Post (Create), Put (Update), and Delete (Delete) data. 
Additionally, the APIs are customized to suit the specific data requirements of the recipe web application, ensuring efficient and accurate data management.
This project is team project. Hence, the contributions of the team members to this program are detailed in the `Endpoints` section below.

## Model Structure
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
- **contribution:** (Geunmok Song)

**2. Create Category**  
- **Endpoint:** `POST /api/Category`  
- **Description:** Create a new category.
- **contribution:** (Geunmok Song)

**3. Get Category By ID**  
- **Endpoint:** `GET /api/Category/{categoryId}`  
- **Description:** Retrieve a specific category by its ID.
- **contribution:** (Geunmok Song)

**4. Update Category**  
- **Endpoint:** `PUT /api/Category/{categoryId}`  
- **Description:** Update an existing category by its ID.
- **contribution:** (Geunmok Song)

**5. Delete Category**  
- **Endpoint:** `DELETE /api/Category/{categoryId}`  
- **Description:** Delete a specific category by its ID.
- **contribution:** (Geunmok Song) 

#### Ingredient

**1. Get All Ingredients**  
- **Endpoint:** `GET /api/Ingredient`  
- **Description:** Retrieve all ingredients.
- **contribution:** (Geunmok Song)

**2. Create Ingredient**  
- **Endpoint:** `POST /api/Ingredient`  
- **Description:** Create a new ingredient.
- **contribution:** (Geunmok Song)

**3. Get Ingredient By ID**  
- **Endpoint:** `GET /api/Ingredient/{ingredientId}`  
- **Description:** Retrieve a specific ingredient by its ID.
- **contribution:** (Geunmok Song)

**4. Update Ingredient**  
- **Endpoint:** `PUT /api/Ingredient/{ingredientId}`  
- **Description:** Update an existing ingredient by its ID.
- **contribution:** (Geunmok Song)

**5. Delete Ingredient**  
- **Endpoint:** `DELETE /api/Ingredient/{ingredientId}`  
- **Description:** Delete a specific ingredient by its ID.
- **contribution:** (Geunmok Song)

**6. Get Ingredients By recipeID** 
- **Endpoint:** `GET /api/Ingredient/{recipeId}/ingredient`  
- **Description:** Retrieve a specific ingredients by recipeID.
- **contribution:** (Geunmok Song)

#### Recipe

**1. Get All Recipes**  
- **Endpoint:** `GET /api/Recipe`  
- **Description:** Retrieve all recipes.
- **contribution:** (Gwantea Lee)

**2. Create Recipe**  
- **Endpoint:** `POST /api/Recipe`  
- **Description:** Create a new recipe.
- **contribution:** (Gwantea Lee: recipe information part), (Geunmok Song: food image part)

**3. Get Recipe By ID**  
- **Endpoint:** `GET /api/Recipe/{RecipeId}`  
- **Description:** Retrieve a specific recipe by its ID.
- **contribution:** (Gwantea Lee)

**4. Update Recipe**  
- **Endpoint:** `PUT /api/Recipe/{recipeId}`  
- **Description:** Update an existing recipe by its ID.
- **contribution:** (Gwantea Lee: recipe information update part), (Geunmok Song: food image update part)

**5. Delete Recipe**  
- **Endpoint:** `DELETE /api/Recipe/{recipeId}`  
- **Description:** Delete a specific recipe by its ID.
- **contribution:** (Gwantea Lee)

**6. Get Recipes By Input Text**  
- **Endpoint:** `GET /api/Recipe/search/{searchname}`  
- **Description:** Retrieve a specific recipes by input text of search.
- **contribution:** (Geunmok Song)

**7. Get Recipes By Category Name**  
- **Endpoint:** `GET /api/Recipe/filter/{categoryName}`  
- **Description:** Retrieve a specific recipes by categoryName.
- **contribution:** (Geunmok Song)

**8. Get Recipes By Category Name & Input Text**  
- **Endpoint:** `GET /api/Recipe/filter_search/{categoryName}/{searchname}`  
- **Description:** Retrieve a specific recipes by categoryName & input text of search.
- **contribution:** (Geunmok Song)

#### Review

**1. Get All Reviews**  
- **Endpoint:** `GET /api/Review`  
- **Description:** Retrieve all reviews.
- **contribution:** (Geunmok Song)

**2. Create Review**  
- **Endpoint:** `POST /api/Review`  
- **Description:** Create a new review.
- **contribution:** (Geunmok Song)

**3. Get Review By ID**  
- **Endpoint:** `GET /api/Review/{reviewId}`  
- **Description:** Retrieve a specific review by its ID.
- **contribution:** (Geunmok Song)

**4. Update Review**  
- **Endpoint:** `PUT /api/Review/{reviewId}`  
- **Description:** Update an existing review by its ID.
- **contribution:** (Geunmok Song)

**5. Delete Review**  
- **Endpoint:** `DELETE /api/Review/{reviewId}`  
- **Description:** Delete a specific review by its ID.
- **contribution:** (Geunmok Song)

**6. Get Reviews By recipeID**  
- **Endpoint:** `GET /api/Review/{recipeId}/review`  
- **Description:** Retrieve a specific reviews by recipeId.
- **contribution:** (Geunmok Song)

**7. Get Reviews By recipeID & userID**  
- **Endpoint:** `GET /api/Review/{recipeId}/{userId}/review`  
- **Description:** Retrieve a specific reviews by recipeId & userId.
- **contribution:** (Geunmok Song)

#### User

**1. Get All Users**  
- **Endpoint:** `GET /api/User`  
- **Description:** Retrieve all users.
- **contribution:** (Gwantea Lee)

**2. Create User**  
- **Endpoint:** `POST /api/User/Register`  
- **Description:** Create a new user.
- **contribution:** (Gwantea Lee)

**3. Update User**  
- **Endpoint:** `PUT /api/User/{uId}`  
- **Description:** Update an existing user by their userId.
- **contribution:** (Gwantea Lee)

**4. Delete User**  
- **Endpoint:** `DELETE /api/User/{UserId}`  
- **Description:** Delete a specific user by their UserId.
- **contribution:** (Gwantea Lee)

**5. Login**  
- **Endpoint:** `POST /api/User/Login`  
- **Description:** Login by userId and Password.
- **contribution:** (Geunmok Song)
