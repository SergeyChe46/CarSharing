using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSharing.Context;
using CarSharing.Entities.Car;
using CarSharing.Repositories;

namespace CarSharing.Core.Repository
{
    public class RepositoryManager
    {
        private readonly BaseRepository<Car> _repository;
        private readonly ApplicationContext _context;
        public RepositoryManager(ApplicationContext context)
        {
            _context = context;
            _repository = new CarRepository(_context);
        }
        public BaseRepository<Car> Cars => _repository;
        public Task Save => _context.SaveChangesAsync();
    }
}
