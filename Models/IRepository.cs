using System.Collections.Generic;

namespace StudingWorkloadCalculator.Models
{
    internal interface IRepository<T>
    {
        public static Dictionary<object, T> Instances { get; private set; } = new Dictionary<object, T>();
    }
}
