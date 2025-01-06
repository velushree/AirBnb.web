using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnb.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IVillaRepository villa {  get; }
        IVillaNumberRepository villaNumber { get; }
        IAmenityRepository amenity { get; }
        void Save();
    };
}
