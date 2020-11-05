using AnimalSpawn.Domain.Entities;
using AnimalSpawn.Domain.Interfaces;
using AnimalSpawn.Domain.QueryFilters;
using AnimalSpwan.Infraestructure.Data;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AnimalSpwan.Infraestructure.Repositories
{
    public class AnimalRepository : SQLRepository<Animal>, IAnimalRepository
    {
        private readonly AnimalSpawnContext _context;
        public AnimalRepository(AnimalSpawnContext context) : base(context)
        {
            this._context = context;
        }
        public IEnumerable<Animal> GetAnimals(AnimalQueryFilter filter)
        {
            
            Expression<Func<Animal, bool>> exprFinal = animal => animal.Id > 0;

            if (!string.IsNullOrEmpty(filter.Name) && !string.IsNullOrWhiteSpace(filter.Name))
            {
                Expression<Func<Animal, bool>> expr = animal => animal.Name.Contains(filter.Name);
                exprFinal = exprFinal.And(expr);
            }
            if (filter.Family.HasValue)
            {
                Expression<Func<Animal, bool>> expr = animal => animal.FamilyId == filter.Family.Value;
                exprFinal = exprFinal.And(expr);
            }
            if (filter.Specie.HasValue)
            {
                Expression<Func<Animal, bool>> expr = animal => animal.SpeciesId == filter.Specie.Value;
                exprFinal = exprFinal.And(expr);
            }
            if (filter.Genus.HasValue)
            {
                Expression<Func<Animal, bool>> expr = animal => animal.GenusId == filter.Genus.Value;
                exprFinal = exprFinal.And(expr);
            }
            if (filter.CaptureDateMax.HasValue && filter.CaptureDateMin.HasValue)
            {
                Expression<Func<Animal, bool>> expr = animal =>
                animal.CaptureDate.Value.Date >= filter.CaptureDateMin.Value.Date
                 && animal.CaptureDate.Value.Date <= filter.CaptureDateMax.Value.Date;
                exprFinal = exprFinal.And(expr);
            }
            if (!string.IsNullOrEmpty(filter.RfTag) && !string.IsNullOrWhiteSpace(filter.RfTag))
            {
                Expression<Func<RfidTag, bool>> expr = rfidTag => rfidTag.Tag == filter.RfTag;
                var animals = _context.RfidTag.Where(expr)
               .Include(x => x.IdNavigation).Select(x => x.IdNavigation);
                return animals.Where(exprFinal).AsEnumerable();
            }
            return FindByCondition(exprFinal);
        }

        //private readonly AnimalSpawnContext _context;
        //public AnimalRepository(AnimalSpawnContext context)
        //{
        //    _context = context; 
        //}

        //public async Task AddAnimal(Animal animal)
        //{
        //    _context.Animal.Add(animal);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<bool> DeleteAnimal(int id)
        //{
        //    var animal = await GetAnimal(id);
        //    _context.Animal.Remove(animal);
        //    var rowsDelete = await _context.SaveChangesAsync();
        //    return rowsDelete > 0;
        //}

        //public async Task<Animal> GetAnimal(int id)
        //{
        //    var animal = await _context.Animal.FirstOrDefaultAsync(animal => animal.Id == id);
        //    return animal;
        //}

        //public async Task<IEnumerable<Animal>> GetAnimals()
        //{
        //    var animals = await _context.Animal.ToListAsync();
        //    return animals;
        //}

        //public async Task<bool> UpdateAnimal(Animal animal)
        //{
        //    var current = await GetAnimal(animal.Id);
        //    current.GenusId = animal.GenusId;
        //    current.FamilyId = animal.FamilyId;
        //    current.Description = animal.Description;
        //    current.EstimatedAge = animal.EstimatedAge;
        //    current.Gender = animal.Gender;
        //    current.Height = animal.Height;
        //    current.Name = animal.Name;
        //    current.Photo = animal.Photo;
        //    current.SpeciesId = animal.SpeciesId;
        //    current.UpdateAt = DateTime.Now;
        //    current.UpdatedBy = 5;
        //    var rowsUpdate = await _context.SaveChangesAsync();
        //    return rowsUpdate > 0;
        //}

    }
}
