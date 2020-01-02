using Elastic.Asp.mvc.Models.Entities;
using Elastic.Asp.mvc.Models.Interfaces;
using Elastic.Asp.mvc.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Elastic.Asp.mvc.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }


        // GET: Student
        public ActionResult Index()
        {
            return View(studentRepository.GetAll());
        }

        // GET: Student/Details/5
        public ActionResult Details(string id)
        {
            var student = studentRepository.GetById(id);
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }
            StudentViewModel model = CreateStudentViewModel(student);
            return View(model);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert logic here
                var student = new Student
                {
                    Age = model.Age,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = new Address
                    {
                        Cuntry = model.Cuntry,
                        City = model.City,
                        Street = model.Street,
                        PostalCode = model.PostalCode
                    }
                };

                studentRepository.Add(student);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(string id)
        {
            var student = studentRepository.GetById(id);
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }
            StudentViewModel model = CreateStudentViewModel(student);
            return View(model);
        }

        private static StudentViewModel CreateStudentViewModel(Student student)
        {
            var model = new StudentViewModel
            {
                Age = student.Age,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Id = student.Id
            };
            if (student.Address != null)
            {
                model.City = student.Address.City;
                model.Cuntry = student.Address.Cuntry;
                model.PostalCode = student.Address.PostalCode;
                model.Street = student.Address.Street;
            }

            return model;
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (id!=model.Id)
                {
                    ModelState.AddModelError(string.Empty, "Id is Not Valid");
                    return View(model);
                }
                var student = new Student
                {
                    Id=model.Id,
                    Age = model.Age,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = new Address
                    {
                        Cuntry = model.Cuntry,
                        City = model.City,
                        Street = model.Street,
                        PostalCode = model.PostalCode
                    }
                };

                studentRepository.Update(student);
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(string id)
        {
            return View(studentRepository.GetById(id));
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteById(string id)
        {
            try
            {
                // TODO: Add delete logic here
                studentRepository.DeleteById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}