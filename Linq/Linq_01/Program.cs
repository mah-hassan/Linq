using EmployeeDataRepositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var employees = Repository.LoadEmployees();
        // 01
        var maleEmps01 = employees.Fillter(x => x.Gender == "male");
        maleEmps01.Print("Male Employees 01");

        // using linq

        // 02 extention method
        var maleEmps02 = employees.Where(x => x.Gender == "male");
        maleEmps02.Print("Male Employees 02");

        // 03 query style better when deleing with DBs

        var maleEmps03 = 
            from employee in employees
            where employee.Gender == "male"
            select employee;
        maleEmps03.Print("Male Employees 03");

        // 04 the CLR convert 02 and 03 to this form

        var maleEmps04 = Enumerable.Where(employees,x => x.Gender == "male");
        maleEmps04.Print("Male Employees 04");

        Console.WriteLine($"\nClick any button to run next example");
      
        Console.ReadKey();

        Console.Clear();

        List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

        var evenNumbers = numbers.Where(n => n % 2 == 0); // 2 4 6 8 10 12 14 16

        numbers.Add(18);
        numbers.Add(20);
        numbers.Remove(10);

        foreach (var n in evenNumbers)
        {
            // evenNumbers will contain 18 20 and remove 10 but why?
            /* will!.
             * first the evenNumbers stores predicate and arefrence to numbers not a copy 
             * it creats a copy just when iterating in evenNumbers
             */
            Console.Write($" {n}");
        }
    }
}