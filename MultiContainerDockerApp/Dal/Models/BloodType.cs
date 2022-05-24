using NodaTime;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiContainerDockerApp.Dal.Models
{
    public class BloodType
    {
        public int Id { get; set; }
        public LocalDateTime Created { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string RhFactor { get; set; }

    }
}
