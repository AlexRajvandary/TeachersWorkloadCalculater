﻿using System;
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
            return gender.ToLower() switch
            {
                "мужчина" or "м" or "муж"or "male" => Gender.Male,
                "женщина" or "ж" or "жен"or "female" or "Female" => Gender.Female,
                _ => Gender.Undefined
            };
        }
    }
}
