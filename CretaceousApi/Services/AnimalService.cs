// This file implements the business logic for the Animal entity


using CretaceousApi.Services;
using CretaceousApi.Models;
using CretaceousApi.Repositories;

namespace CretaceousApi.Services;
public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;

    public AnimalService(IAnimalRepository animalRepository)
    {   
        _animalRepository = animalRepository;
    }

    public Task<List<Animal>> Get()
    {
        return _animalRepository.Get();
    }

    // Is it compulsory to use the same names for AnimalService.cs and AnimalRepository.cs, I don't think it is. But the compulsory one is that in IAnimalService.GetAnimalById() must match AnimalService.GetAnimalById() i.e the method names must match with each other
    public Animal GetAnimalById(int id)
    {
        return _animalRepository.GetAnimal(id);
    }

    public async Task Post(Animal animal)
    {
        await _animalRepository.Post(animal);
    }

    public async Task Put(int id, Animal animal)
    {
        await _animalRepository.Put(id, animal);
    }

    public void DeleteAnimal(int id)
    {
        _animalRepository.DeleteAnimal(id);
    }
}