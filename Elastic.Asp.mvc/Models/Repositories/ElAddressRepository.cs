using Elastic.Asp.mvc.Models.Interfaces;
using System;

namespace Elastic.Asp.mvc.Models.Repositories
{
    public class ElAddressRepository : IAddressRepository
    {
        public void Add(Entities.Address address)
        {
            throw new NotImplementedException();
        }

        public Entities.Address GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Entities.Address address)
        {
            throw new NotImplementedException();
        }
    }
}
