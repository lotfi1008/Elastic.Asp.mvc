using Elastic.Asp.mvc.Models.Entities;
using System.Collections.Generic;

namespace Elastic.Asp.mvc.Models.Interfaces
{
    public interface IStudentRepository
    {
        void Add(Student student);
        void Update(Student student);
        Student GetById(string id);
        void DeleteById(string id);
        IEnumerable<Student> GetAll();
    }
}
