using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeDataRepositories
{
    public static class ExtensionFunctional
    {

        public static IEnumerable<Employee> Fillter(this IEnumerable<Employee> employees , Predicate<Employee> predicate)
        {
            if (employees is null)
            {
                throw new NullReferenceException();
            }
            foreach (Employee emp in employees) 
            {
                if (predicate(emp))
                {
                    yield return emp;
                }
                else
                    continue;
            }
        }



        public static void Print<T>(this IEnumerable<T> source, string title)
        {
            if (source == null)
                return;
            Console.WriteLine();
            Console.WriteLine("┌───────────────────────────────────────────────────────┐");
            Console.WriteLine($"│   {title.PadRight(52, ' ')}│");
            Console.WriteLine("└───────────────────────────────────────────────────────┘");
            Console.WriteLine();
            foreach (var item in source)
            {
                if (typeof(T).IsValueType) 
                    Console.Write($" {item} "); // 1, 2, 3
                else
                    Console.WriteLine(item);
            }
                
         
        }
    }
}
