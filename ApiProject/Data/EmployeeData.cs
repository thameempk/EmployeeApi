using ApiProject.Models.Dto;

namespace ApiProject.Data
{
    public class EmployeeData
    {
        public static List<EmployeeDTO> employees = new List<EmployeeDTO>()
        {
            new EmployeeDTO { Id = 1, Name = "thameem" , salary = 12000},
            new EmployeeDTO { Id = 2, Name = "arun", salary = 23222},
            new EmployeeDTO { Id  =3 , Name ="villa", salary = 43445}
        };
    }
}
