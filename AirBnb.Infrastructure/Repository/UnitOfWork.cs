using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirBnb.Application.Common.Interfaces;
using AirBnb.Infrastructure.Data;

namespace AirBnb.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {   
        ApplicationDbContext _dbContext;
        public IVillaRepository villa { get; private set; }
        public IVillaNumberRepository villaNumber { get; private set; } 
        public IAmenityRepository amenity { get; private set; } 

        public UnitOfWork(ApplicationDbContext DbContext) 
        {
            _dbContext = DbContext;
            villa = new VillaRepository(_dbContext);
            villaNumber = new VillaNumberRepository(_dbContext);
            amenity = new AmenityRepository(_dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();   
        }
    }
}
