using AnimalSpawn.Domain.Entities;
using AnimalSpawn.Domain.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnimalSpawn.Domain.Interfaces
{
    public interface IAnimalService 
    {
        Task AddAnimal(Animal animal);
        Task DeleteAnimal(int id);
        IEnumerable<Animal> GetAnimals(AnimalQueryFilter filters);
        Task<Animal> GetAnimal(int id);
        Task UpdateAnimal(Animal animal);
    }
}
