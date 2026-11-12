using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolAppliction.Models;
using SchoolAppliction.Services;
using System.Net.Http.Headers;

namespace SchoolAppliction.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApiService _apiservice;

        public StudentsController()
        {
            _apiservice = new ApiService();
        }

        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index()
        {
            List<StudentEntity> lstStudents = new List<StudentEntity>();
            lstStudents = await _apiservice.GetAllStudents(HttpContext.Session.GetString("ApiToken"));
            return View(lstStudents);
            
        }
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> CreateStudent(StudentEntity studentEntity)
        {
            await _apiservice.AddStudent(studentEntity, HttpContext.Session.GetString("ApiToken"));
            return RedirectToAction("Index");

          
        }
        [Authorize(Roles = "Admin,User")]

        public async Task<IActionResult> Details(int Id)
        {
            StudentEntity studentDetails = new StudentEntity();
            studentDetails=await _apiservice.GetStudentDetailsById(Id, HttpContext.Session.GetString("ApiToken"));
            return View(studentDetails);
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateStudentDetails(int Id, StudentEntity studentEntity)
        {
            await _apiservice.UpdateStudent(Id, studentEntity, HttpContext.Session.GetString("ApiToken"));
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int Id)
        {
            StudentEntity studentDetails = new StudentEntity();
            studentDetails=await _apiservice.GetStudentDetailsById(Id, HttpContext.Session.GetString("ApiToken"));

            return View(studentDetails);
        }
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(int Id)
        {
            await _apiservice.DeleteStudent(Id, HttpContext.Session.GetString("ApiToken"));
            return RedirectToAction("Index");
           
        }
        public IActionResult ErrorPage()
        {
            return View();
        }

    }
}
