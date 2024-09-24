// CretaceousApi/Repositories/AnimalRepository.cs implements the repository for database operations(CRUD Operations on Animal)

using System.Threading.Tasks;
using CretaceousApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CretaceousApi.Models;
using CretaceousApi.Data;

namespace CretaceousApi.Repositories;

public class AnimalRepository : ControllerBase, IAnimalRepository
{
    public readonly CretaceousApiContext _db;

    public AnimalRepository(CretaceousApiContext db)
    {
        _db = db;
    }

    // public async Task<ActionResult<IEnumerable<Animal>>> Get()
    // {
    //     return _db.Animals.ToListAsync();
    // }

    public List<Animal> Get()
    {
      return _db.Animals.ToList();
    }

    public Animal GetAnimal(int id)
    {
        return _db.Animals.Find(id);
        
    
    // TODO: After this one works, update code with all of those querries i.e User should be able to search for the list of animals whose species are Tyrannosaurus Rex
    }

    // public async Task<ActionResult<Animal>> Post(Animal animal)
    // {
    //     _db.Animals.Add(animal);
        
    //     _db.SaveChangesAsync();
    //     return CreatedAtAction(nameof(GetAnimal), new { id = animal.AnimalId }, animal);

    // }

     public async void Post(Animal animal)
    {
        _db.Animals.Add(animal);
        await _db.SaveChangesAsync();
    }
    
     public async void Put(int id, Animal animal)
    {
      _db.Animals.Update(animal);
      await _db.SaveChangesAsync();
    }


    //  public async void Put(int id, Animal animal)
    // {
    //   if (id != animal.AnimalId)
    //   {
    //     return BadRequest();
    //   }

    //   _db.Animals.Update(animal);

    //   try
    //   {
    //     await _db.SaveChangesAsync();
    //   }
    //   catch (DbUpdateConcurrencyException)
    //   {
    //     if (!AnimalExists(id))
    //     {
    //       return NotFound();
    //     }
    //     else
    //     {
    //       throw;
    //     }
    //   }

    // }

    public async void DeleteAnimal(int id)
    {
        Animal animal = await _db.Animals.FindAsync(id);
        // if (animal == null)
        // {
        //     return NotFound();
        // }

        _db.Animals.Remove(animal);
        await _db.SaveChangesAsync();

    }

    public bool AnimalExists(int id)
    {
        return _db.Animals.Any(animal => animal.AnimalId == id);
    }



}



/* IQueryable<Animal> querry = _db.Animals.AsQueryable();
      public IActionResult Get(string species, string name, int minimumAge, int age)

          // if(species != null)
          // {
          //   // The Where() method accpets a lambda expression
          //     querry = querry.Where(entry => entry.Species == species);
          // }

          // // Handling multiple Parameters; finding dinosaurs by their name
          // if (name != null)
          // {
          //     querry = querry.Where(entry => entry.Name == name);
          // }

          // // Adding Non string paramters... Since Parameter Source Binding inference in our web API works for any primitive datatype(string, int, float), we can then add another parameter of int type called minimumAge
          // if(minimumAge > 0)
          // {
          //     querry = querry.Where(entry => entry.Age >= minimumAge);
          // }

          // // Another example, if we want to create a query so users can query animals by their age, an integer, we would check to see if a value for our age parameter has been added, by checking if age !=0
          // if(age != 0)
          // {
          //     querry = querry.Where(entry => entry.Age == age);
          // }

          // return await querry.ToListAsync();
          */