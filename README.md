# Cretaceous Api 1.0
#### made by Mojiboye Emmanuel

#### Building an API for creatures during the Cretaceous Period

## Technologies Used
* Git
* C#
* .NET 6.0 Framework
* ASP.NET Core RESTful APIs
* Entity Framework Core
* Pomelo.EntityFrameworkCore.MySql
* Swagger/OpenAPI
* Layered (3-tier) Architecture
* Dependency Injection
* Asynchronous Programming
* JSON

## Description
_An API for animals that lived during the Cretacous Period._

## API Endpoints
```sh
* GET http://localhost:5000/api/animals/
* GET http://localhost:5000/api/animals/{id}
* POST http://localhost:5000/api/animals
* PUT http://localhost:5000/api/animals{id}
* DELETE http://localhost:5000/api/animals/{id}
```

### Optional Query String Parameters for GET Requests



### Example Querries

1. The following query will return all animals with a species value of Dinosaur i.e search for all animals that are dinosaurs:
```sh
GET http:localhost:5000/api/animals?species=dinosaur
```

2. The following query will return all animals with the name Matilda:
```sh
GET http:localhost:5000/api/animals?name=matilda
```

3. The following query will return all animals with an age of 10 or older:
```sh
    GET http:localhost:5000/api/animals?minimumAge=10
```

4. Its possible to combine multiple querries with &. The following query will return all animals with specie dinosaur, with an age of 10 or older:

```sh
    GET http:localhost/5000/api/animals?species=dinosaur&minimumAge=10
```

### Additional Requirement when making a POST request
* _When making a POST request to http://localhost:5000/api/animals/ you need to include a body. Please do not add in the id when making a POST i.e when creating an animal, the database will help out with that.Here is an example body in JSON:_
``` json
    {
        "species": "Tyrannosaurus Rex",
        "name": "Elizabeth",
        "age": 8
    }
```

### Additional Requirement when making a PUT request
* _When making a PUT request to http://localhost:5000/api/animals/{id} you need to include a body that includes the animalId property. A PUT request is to edit, pleas eadd in the id of the animal that you want to edit.Here's an example body in JSON:_
``` json
    {
        "animalId": 1,
        "species": "Tyrannosaurus Rex",
        "name": "Elizabeth",
        "age": 8
    }
```
And here is the PUT request we would send the previous body to:
```sh
    http://localhost:5000/api/animals/1
```
Notice that the value of animalId needs to match the id number in the URL in this example, they are both 1


### Documentation on Further Exploration
* _Further exploration is a section where new things will be added to our API to increase its capabilities_
* _As the WebAPI project grows, we would update developers on some of the new changes. Please stay tuned as features like authentication, pagination, versioning, CORS will soon be added_

## Setup/Installation
1. **Clone the Repository**: Open your Git Bash terminal and run the following command to clone the project:
    ```sh
    git clone _REPOSITORY_NAME_
    ```

2. **Install .NET 6.0 Framework**: Ensure you have the .NET 6.0 Framework installed (used .NET 6.0.402 for this application) on your PC. Make sure you have completed the essential steps to write C# code in the C# REPL Terminal.

3. **Create `appSettings.json`**: After cloning the repository, you need to create an `appSettings.json` file in the root directory of the project. Be sure to create it in the production directory of your project (`CretaceousApi.Solution/CretaceousApi/appSettings.json`). Use the following template for your `appSettings.json`:

    ```json
    {
        "ConnectionStrings": {
            "DefaultConnection": "Server=localhost;Port=3306;database=cretaceous_api;uid=[YOUR-USERNAME-HERE];pwd=[YOUR-PASSWORD-HERE];"
        }
    }
    ```

    Replace `[YOUR-USERNAME-HERE]` and `[YOUR-PASSWORD-HERE]` with your actual MySQL username and password.

4. **Build and Run the Application**:
    - If you're not interested in seeing the build messages, run the following command to build and run the application:
      ```sh
      dotnet run
      ```
      This command builds and runs the application for you.

    - If you are interested in seeing the build messages, follow these steps:
        1. **Build the Project**: Run the following command to build the project and add the essential directories to execute the application:
            ```sh
            dotnet build
            ```

        2. **Run the Project**: After building the application, run the following command to see all of the results outputted into the console:
            ```sh
            dotnet run
            ```
            Copy the link where the project is hosted on git bash or nay other terminal window you are use and paste the link in a browser

        3. **Run the Project with watch run**: This is the one we really recommend because it opens up the Swagger UI on your default browser, and there you can carry out all the API requests that have been defined inside the controller. Run the code below:
        ```sh
            dotnet watch run
        ```
        With the watch run, developers are able to simultaneously update the application's code and  also build the project.

        

## Detected Bugs/ Issues
* No detected bugs

## License 
Licensed under the GNU General Public License

## Contact Info
* _Email: emzzyoluwole@gmail.com_
* _Instagram @Emmanuel.9944_
* _Twitter: @Emzzy241 and Profile Name: Dynasty_
* _Github Username: Emzzy241_
