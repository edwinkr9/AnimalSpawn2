using AnimalSpawn.Domain.Entities;
using AnimalSpawn.Domain.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimalSpawn.Domain.Interfaces
{
    public interface IAnimalRepository : IRepository<Animal>
    {
        IEnumerable<Animal> GetAnimals(AnimalQueryFilter filter);
        //public Task<IEnumerable<Animal>> GetAnimals();
        //public Task<Animal> GetAnimal(int id);
        //public Task AddAnimal(Animal animal);
        //public Task<bool> UpdateAnimal(Animal animal);
        //public Task<bool> DeleteAnimal(int id);
    }
}
