using Helper;

internal class Program
{
    private static IEnumerable<Employee> employees = Repository.LoadEmployees();
    private static IEnumerable<Department> departments = Repository.LoadDepartment();
    private static void Main(string[] args)
    {
        RunJoinExample01();
        clear();
        RubJoinExample02QuarySyntax();
        clear();

        RunGroupJoinExample01();
        clear();

        RunGroupJoinExample02QuarySyntax();
    }
    static void clear()
    {
        Console.WriteLine("press a key to continue..");
        Console.ReadKey();
        Console.Clear();
    }
    private static void RunGroupJoinExample02QuarySyntax()
    {
        var result = from d in departments
                     join e in employees
                     on d.Id equals e.DepartmentId
                     into empGroup
                     select empGroup;


        foreach (var group in result)
        {
            bool _do = true;
            foreach (var emp in group)
            {
                if (_do)
                {
                    Console.WriteLine("┌───────────────────────────────────────────────────────┐");
                    Console.WriteLine($"│   {departments.Where(d => d.Id == emp.DepartmentId).ToList()[0].Name.PadRight(52, ' ')}│");
                    Console.WriteLine("└───────────────────────────────────────────────────────┘");
                    _do = false;
                }
                Console.WriteLine(emp.FullName);
            }
        }
    }

    private static void RunGroupJoinExample01()
        {
            var departmentGroups = departments.GroupJoin(employees, dept => dept.Id, emp => emp.DepartmentId,
                (dept, emps) => new
                {
                    Employees = new List<string>(emps.Select(x => x.FullName)),
                    Department = dept.Name,
                });
            foreach (var department in departmentGroups)
            {
                Console.WriteLine("┌───────────────────────────────────────────────────────┐");
                Console.WriteLine($"│   {department.Department.PadRight(52, ' ')}│");
                Console.WriteLine("└───────────────────────────────────────────────────────┘");
                foreach (var emp in department.Employees)
                {
                    Console.WriteLine($"{emp}");
                }

            }
        }
    private static void RubJoinExample02QuarySyntax()
    {
        var results = from employee in employees
                      join department in departments on employee.DepartmentId equals department.Id
                      select new
                      {
                          Name = employee.FullName,
                          Department = department.Name
                      };
        foreach (var result in results)
        {
            Console.WriteLine($"{result.Name}\t{result.Department}");

        }
    }


    private static void RunJoinExample01()
    {
        var result = employees.Join(departments, emp => emp.DepartmentId, dept => dept.Id,
            (emp, dept) => new
            {
                Name = emp.FullName,
                Department = dept.Name

            });

        foreach (var item in result)
        {
            Console.WriteLine($"{item.Name}\t{item.Department}");
        }
    }
}