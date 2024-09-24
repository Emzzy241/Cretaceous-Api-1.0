using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CretaceousApi.Models;
using CretaceousApi.Services;

// Since implicit using directives have already been included in the .csproj file, no need to include some of the very basic namespaces like System.Collections.Generic for lists, System.Threading.Tasks for async methods

namespace CretaceousApi.Controllers;



[Route("api/[controller]")]
// With the above [Route] attribute, we're specifying that the base request URL for the AnimalsController is /api/animals. So for example, when we make a GET request to http://localhost:5000/api/animals, we'll access the Get() action in our AnimalsController, which will then handle returning all of the animals in our database.
[ApiController]
public class AnimalsController : ControllerBase, IAnimalService
{
    private readonly AnimalService _animalService;
    
    public AnimalsController(AnimalService animalService)
    {
        _animalService = animalService;
    }


    [HttpGet]
    public List<Animal> Get()
    {   
      return _animalService.Get();
    }    

    [HttpGet("{id}")]
    public Animal GetAnimalById(int id)
    {
       var animal = _animalService.GetAnimalById(id);
        // if (animal == null)
        // {
        //     return NotFound();
        // }
        return animal;
    }

    

    [HttpPost]
    public void Post(Animal animal)
    {
      _animalService.Post(animal);
   
    }


    [HttpPut("{id}")]
    public void Put(int id, Animal animal)
    {
      _animalService.Put(id, animal);
    }

    [HttpDelete("{id}")]
    public void DeleteAnimal(int id)
    {
        Animal animal = _animalService.DeleteAnimal(id);
        if (animal == null)
          {
              return NotFound();
          }
          
        return NoContent();
    }
}