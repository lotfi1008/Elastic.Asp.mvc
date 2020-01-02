using Elastic.Asp.mvc.Models.Entities;

namespace Elastic.Asp.mvc.Models.Interfaces
{
    public interface IAddressRepository
    {
        void Add(Address address);
        void Update(Address address);
        Address GetById(int id);
        
    }
}
