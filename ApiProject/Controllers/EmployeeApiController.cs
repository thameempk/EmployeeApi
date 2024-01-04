using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiProject.Models.Dto;
using ApiProject.Data;
using Microsoft.AspNetCore.JsonPatch;
namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeApiController : ControllerBase
    {
       
        [HttpGet]
        public ActionResult<IEnumerable<EmployeeDTO>>  GetEmployees()
        {
            return Ok( EmployeeData.employees );
        }

        [HttpGet("{id}", Name = "Employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EmployeeDTO>  GetEmployeeById (int id)
        {
            if(id == 0 )
            {
                return BadRequest();
            }
            var emp = EmployeeData.employees.FirstOrDefault(x => x.Id == id);
            if(emp == null )
            {
                return NotFound();
            }
            return Ok( emp );
            
        }

        [HttpPost]

        public ActionResult<EmployeeDTO> CreateEmployee ([FromBody] EmployeeDTO employeeDTO)
        {
            if( employeeDTO == null)
            {
                return BadRequest(employeeDTO);
            }
            if(employeeDTO.Id > 0 )
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            employeeDTO.Id = EmployeeData.employees.OrderByDescending(s => s.Id).FirstOrDefault().Id + 1;
            EmployeeData.employees.Add(employeeDTO);
            return CreatedAtRoute("Employee", new { id = employeeDTO.Id }, employeeDTO );

        }

        [HttpDelete("{id}")]

        public IActionResult DeleteEmployee ( int id)
        {
            if (id == 0 )
            {
                return BadRequest();
            }
            var emp = EmployeeData.employees.FirstOrDefault(s => s.Id == id);
            if ( emp == null  )
            {
                return NotFound();
            }
            EmployeeData.employees.Remove(emp);
            return NoContent();
        }

        [HttpPut("{id}")] 

        public IActionResult UpdateEmployee (int id, [FromBody] EmployeeDTO employeeDTO)
        {
            if( id != employeeDTO.Id || employeeDTO == null )
            {
                return BadRequest();
            }
            var emp = EmployeeData.employees.FirstOrDefault(e => e.Id == id);
            if( emp == null)
            {
                return NotFound();
            }
            emp.Name = employeeDTO.Name;
            emp.salary = employeeDTO.salary;
            return NoContent();
        }

        [HttpPatch("{id}")]

        public IActionResult UpdatePatchEmployee (int id , JsonPatchDocument<EmployeeDTO> patchDTO)
        {
            if( id == 0 || patchDTO == null)
            {
                return BadRequest();
            }
            var emp = EmployeeData.employees.FirstOrDefault(s => s.Id == id);
            if( emp == null )
            {
                return NotFound();
            }
            patchDTO.ApplyTo(emp, ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }



        
    }
}
