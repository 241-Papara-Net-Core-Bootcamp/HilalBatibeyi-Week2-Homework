using PaparaSecondWeek.Attributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PaparaSecondWeek.Models
{

    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
