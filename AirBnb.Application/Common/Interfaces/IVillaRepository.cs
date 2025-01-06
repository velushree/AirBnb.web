using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AirBnb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Application.Common.Interfaces
{
    public interface IVillaRepository:IRepository<Villa>
    {
        void Update(Villa entity);
    }
}
