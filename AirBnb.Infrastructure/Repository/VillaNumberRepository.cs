﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirBnb.Application.Common.Interfaces;
using AirBnb.Domain.Entities;
using AirBnb.Infrastructure.Data;

namespace AirBnb.Infrastructure.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VillaNumberRepository(ApplicationDbContext dbContext) :base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public void Update(VillaNumber entity)
        {
            _dbContext.VillaNumbers.Update(entity);
        }
    }
}