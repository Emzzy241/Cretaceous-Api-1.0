using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CretaceousApi.Models;

// Since implicit using directives have already been included in the .csproj file, no need to include some of the very basic namespaces like System.Collections.Generic for lists, System.Threading.Tasks for async methods

namespace CretaceousApi.Controllers;



[Route("api/[controller]")]
// With the above [Route] attribute, we're specifying that the base request URL for the AnimalsController is /api/animals. So for example, when we make a GET request to http://localhost:5000/api/animals, we'll access the Get() action in our AnimalsController, which will then handle returning all of the animals in our database.
[ApiController]
public class AnimalsController : ControllerBase
{

    // We'll start by creating an AnimalsController.cs with two new methods. One will return all the animals in our application while the other will return just one animal based on its AnimalId property.
    private readonly CretaceousApiContext _db;

    public AnimalsController(CretaceousApiContext db)
    {
        _db = db;
    }


    // GET: api/animals
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<Animal>>> Get()
    // {   
    //     // Converting the objects in the database into a C# list
    //     // We Never Return Views​: Another key difference is that we aren't returning a view. Our Get() action returns an ActionResult of type <IEnumerable<Animal>>, and our GetAnimal() action returns an ActionResult of type <Animal>. In our web applications, we didn't need to specify a type for our ActionResult because we were always returning a view.


    //     return await _db.Animals.ToListAsync();
    // }    

    // Updating Get() action to handle querry strings so that we can return a filtered set of results based on species
      [HttpGet]
      public async Task<ActionResult<IEnumerable<Animal>>> Get(string species, string name, int minimumAge, int age)
      {   
          IQueryable<Animal> querry = _db.Animals.AsQueryable();

          if(species != null)
          {
            // The Where() method accpets a lambda expression
              querry = querry.Where(entry => entry.Species == species);
          }

          // Handling multiple Parameters; finding dinosaurs by their name
          if (name != null)
          {
              querry = querry.Where(entry => entry.Name == name);
          }

          // Adding Non string paramters... Since Parameter Source Binding inference in our web API works for any primitive datatype(string, int, float), we can then add another parameter of int type called minimumAge
          if(minimumAge > 0)
          {
              querry = querry.Where(entry => entry.Age >= minimumAge);
          }

          // Another example, if we want to create a query so users can query animals by their age, an integer, we would check to see if a value for our age parameter has been added, by checking if age !=0
          if(age != 0)
          {
              querry = querry.Where(entry => entry.Age == age);
          }

          return await querry.ToListAsync();
      }    

    // GET: api/animals/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Animal>> GetAnimal(int id)
    {
        Animal animal = await _db.Animals.FindAsync(id);
        if(animal == null)
        {
            return NotFound();
        }

        return animal;

        // We'll Use More HTTP Verb Templates, and These Contribute to Routing​
        // Another key difference is with the HTTP verb templates that we use, like [HttpGet] and [HttpPost]. While these aren't new, we'll now use more of them because we won't be making requests directly from an HTML5 form, which only supports GET and POST actions.

        // Also, these HTTP verb templates are meant to specify what HTTP action method is needed to make the request. This means that not only do we need the right request URL, like http://localhost:5000/api/animals/, we also need the right HTTP action method. For Get() and GetAnimal(), they both require a GET Http action.

        // Notice the GetAnimal() action's attribute: [HttpGet("{id}")]. As we can see, the HttpGet() method accepts an argument; when we include "{id}" as the argument, this configures the endpoint to expect another value added to the end of it: /api/animals/{id}, where {id} is a variable for a value. With the GetAnimal() action parameter int id, we further specify that the value of {id} should be an integer. This means we can now make a GET request with an animal's id in order to only get data for the animal that matches the specified id, just like this:

        // http://localhost:5000/api/animals/1

    }

    // Adding the ability to add animals to our database
    // POST api/animals
    [HttpPost]
    public async Task<ActionResult<Animal>> Post(Animal animal)
    {
      _db.Animals.Add(animal);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetAnimal), new { id = animal.AnimalId }, animal);

      /*
        EXPLAINING THE Create() Action
            First notice that the Post() method takes an Animal parameter, which we call animal. This animal object comes from the body of the request. Since we've specified that our AnimalsController is an [ApiController], our controller knows to infer that the value for the animal parameter should come from the request body, and how to bind that data to an Animal object. This is all thanks to built-in functionality for controllers marked with the [ApiController] attribute.

            We could optionally explicitly state how controller action parameters should be bound. For example, we could use the [FromBody] attribute to specify that the value of animal is found in the request body:

                [HttpPost]
                public async Task<ActionResult<Animal>> Post([FromBody] Animal animal)
                {
                ...
                }
            

            However doing so is optional with controllers marked with the [ApiController] attribute. To learn more about model binding, visit the MS docs on "Binding Source Parameter Inference" .

            The next thing we should cover is the new method CreatedAtAction() that our Post() action uses to return a response:

            return CreatedAtAction(nameof(GetAnimal), new { id = animal.AnimalId }, animal);
            

            CreatedAtAction() is another ControllerBase class method that specifically handles returning an HTTP status code of 201 Created. More than just that, this method lets us specify the Location of the newly created object. What this means is that our Post() controller action will return the newly created Animal object to the user along with a "201 Created" status code, rather than the default 200 OK with no animal object.

            The CreatedAtAction() method takes 3 arguments to specify the location of the new object:

            The name of the controller action, in this case the GetAnimal() controller action specified via nameof(GetAnimal).
            The route values required for the controller action in a new anonymous object, in this case an id parameter for the GetAnimal() controller action.
            The resource that was created in this action.
      */
    }


    // PUT: api/Animals/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Animal animal)
    {
      if (id != animal.AnimalId)
      {
        return BadRequest();
      }

      _db.Animals.Update(animal);

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!AnimalExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    private bool AnimalExists(int id)
    {
      return _db.Animals.Any(e => e.AnimalId == id);
      /*
        Once again, this looks similar to code we'd use in an MVC web application, with a few key differences:

We use the [HttpPut] verb template. This specifies that the request made to the Put() controller action needs to specify the PUT Http action verb.

We'll determine which animal will be updated based on the {id} parameter in the URL.

First, we check if the {id} in the request URL matches the animal.AnimalId property in the Animal object passed into our controller action by the request body. If they do not match, it means that the request is poorly formatted, so we use the ControllerBase.BadRequest() method to return a HTTP status code of 400 Bad Request.

If the request is formatted correctly, then we proceed to updating the animal in our database. The code to update an animal should already be familiar. We use EF Core's built-in methods to update the animal in the database and then save the changes.

We then ask our database to save changes asynchronously. In the process of doing this, we handle the possible error of the animal with the provided AnimalId not existing. To do this we use a try/catch block along with a newly created private method called AnimalExists() (for use within the controller, and to DRY up our code). If an animal by the specified id in the request URL does not exist, then we'll return a 404 Not Found HTTP status code via the ControllerBase.NotFound() method.

Finally, notice that we return NoContent(); at the end of the method. This will return a HTTP status code of 204 No Content, which means that the request has completed successfully, but there's no need to navigate away from the current page. To learn more about the 204 status code, visit the MDN docs.
      */
    }


    // DELETE: api/Animals/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnimal(int id)
    {
      Animal animal = await _db.Animals.FindAsync(id);
      if (animal == null)
      {
        return NotFound();
      }

      _db.Animals.Remove(animal);
      await _db.SaveChangesAsync();

      return NoContent();
    }
}