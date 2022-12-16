using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StudingWorkloadCalculator.Models
{
    public static class GenderHelper
    {
        public static Gender GetGender(this string gender)
        {
            return gender switch
            {
                "Мужчина" or "мужчина" or "М" or "м" or "male" or "Male" => Gender.Male,
                "Женщна" or "женщина" or "Ж" or "ж" or "female" or "Female" => Gender.Female,
                _ => Gender.Undefined
            };
        }
    }
}
