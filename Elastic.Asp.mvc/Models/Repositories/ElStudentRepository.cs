using Elastic.Asp.mvc.Models.DataModels;
using Elastic.Asp.mvc.Models.Entities;
using Elastic.Asp.mvc.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Elastic.Asp.mvc.Models.Repositories
{
    public class ElStudentRepository : IStudentRepository
    {
        HttpClient client;
        public ElStudentRepository()
        {
            client = new HttpClient();
        }
        public void Add(Student student)
        {
            var json = JsonConvert.SerializeObject(student);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client
                .PostAsync("http://localhost:9200/students/_doc", data)
                .Result;
            if (!response.IsSuccessStatusCode)
            {
                //LogError
            }
        }

        public void DeleteById(string id)
        {
            var response = client.DeleteAsync($"http://localhost:9200/students/_doc/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var stringRes = response.Content.ReadAsStringAsync().Result;    
            }
        }

        public IEnumerable<Student> GetAll()
        {
            var response = client.GetAsync("http://localhost:9200/students/_search").Result;
            if (response.IsSuccessStatusCode)
            {
                var stringRes = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<RootObjectList<Student>>(stringRes);
                if (result != null && result.timed_out == false && result.took > 0)
                {
                    return result.THits.THitsList.Select(c => new Student
                    {
                        Address = c.TSource.Address,
                        Age = c.TSource.Age,
                        FirstName = c.TSource.FirstName,
                        LastName = c.TSource.LastName,
                        Id = c._id
                    }).ToList();
                }
            }
            return null;
        }

        public Student GetById(string id)
        {
            var response = client.GetAsync($"http://localhost:9200/students/_doc/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var stringRes = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<RootObject<Student>>(stringRes);
                if (result != null && result.found== true && result.TSource!=null)
                {
                    result.TSource.Id = result._id;
                    return result.TSource;
                }
            }
            return null;
        }

        public void Update(Student student)
        {
            var old = GetById(student.Id);
            if (old==null)
            {
                return;
            }

            old = ModifyStudent(old,student);

            var json = JsonConvert.SerializeObject(student);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client
                .PutAsync($"http://localhost:9200/students/_doc/{student.Id}", data)
                .Result;
            if (!response.IsSuccessStatusCode)
            {
                //LogError
            }
        }

        private Student ModifyStudent(Student old,Student @new)
        {
            old.Age= @new.Age;
            old.FirstName= @new.FirstName;
            old.LastName= @new.LastName;
            if (@new.Address!=null)
            {
                old.Address = @new.Address;
            }
            return old;
        }
    }
}
