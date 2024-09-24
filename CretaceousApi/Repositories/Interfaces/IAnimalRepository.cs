// CretaceousApi/Repositories/Interfaces/IAnimalRepository.cs 
// Contains repository defining how the repository will interact with the database

using CretaceousApi.Repositories;
using System.Collections.Generic;
using CretaceousApi.Models;

namespace CretaceousApi.Repositories;
public interface IAnimalRepository
{
    Task<List<Animal>> Get();
    Animal GetAnimal(int id);
    Task Post(Animal animal);
    Task Put(int id, Animal animal);
    void DeleteAnimal(int id);
}