// Services/IAnimalService.cs: Contains Service interfaces(e.g IAnimalService.cs) for defining the contracts for the services

using CretaceousApi.Services;
using CretaceousApi.Models;


namespace CretaceousApi.Services;
public interface IAnimalService
{
    Task<List<Animal>> Get();
    Animal GetAnimalById(int id);
    Task Post(Animal animal);
    Task Put(int id, Animal animal);
    void DeleteAnimal(int id);
}