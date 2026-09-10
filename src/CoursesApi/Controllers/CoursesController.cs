using CoursesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoursesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private static readonly List<Course> Courses =
    [
        new Course
        {
            Id = 1,
            Name = "Web Services",
            Teacher = "Teacher 1",
            Credits = 5
        },
        new Course
        {
            Id = 2,
            Name = "Databases",
            Teacher = "Teacher 2",
            Credits = 4
        }
    ];

    [HttpGet]
    public ActionResult<List<Course>> GetAll()
    {
        return Ok(Courses);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Course> GetById(int id)
    {
        var course = Courses.FirstOrDefault(item => item.Id == id);

        if (course is null)
        {
            return NotFound();
        }

        return Ok(course);
    }

    [HttpPost]
    public ActionResult<Course> Add(Course course)
    {
        course.Id = Courses.Count == 0 ? 1 : Courses.Max(item => item.Id) + 1;
        Courses.Add(course);

        return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, Course updatedCourse)
    {
        var course = Courses.FirstOrDefault(item => item.Id == id);

        if (course is null)
        {
            return NotFound();
        }

        course.Name = updatedCourse.Name;
        course.Teacher = updatedCourse.Teacher;
        course.Credits = updatedCourse.Credits;

        return Ok(course);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var course = Courses.FirstOrDefault(item => item.Id == id);

        if (course is null)
        {
            return NotFound();
        }

        Courses.Remove(course);

        return NoContent();
    }
}
