using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="{0} Requerido")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Tamanho do nome deve ter entre {2} e {1} caracteres")]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Entre com um email valido")]
        [Required(ErrorMessage = "{0} Requerido")]
        public string Email { get; set; }
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "{0} Requerido")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Required(ErrorMessage = "{0} Requerido")]
        [Range(100.0, 50000.0, ErrorMessage = "Salario entre {1} e {2}")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }//essa propriedade e a de baixo ajudam o framework a criar o objeto
        public int DepartmentId { get; set; }//passado via post
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(f => f.Date >= initial && f.Date <= final).Sum(s => s.Amount);
        }
    }
}
