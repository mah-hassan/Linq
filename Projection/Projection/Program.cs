using DataRepository;
using Projection;

internal class Program
{
    static IEnumerable<Employee> employees = Repository.LoadEmployees();

    private static void Main(string[] args)
    {
        runSelectExample01();
        clear();
        var empsDto = runSelectExample02();
        empsDto.Print<EmployeeDto>("Dto");
        clear();
        runSelectManyExample01();
        clear();
        var commonSkillsBetweenEmps = runSelectManyExample02();
        commonSkillsBetweenEmps.Print("Common Skills Between Employees");
        clear();
        runZipExample();

    }

    private static void runZipExample()
    {
        var emps = employees.ToArray();
        var first10Employees = emps[..10];
        var last10Employees = emps[^10..];
        var teams = first10Employees.Zip(last10Employees, (first, last) => $"{first.FullName}'forward' with {last.FullName} 'backward'");
        foreach (var team in teams)
            Console.WriteLine(team);
    }

    static void clear()
    {
        Console.WriteLine($"\nclick a button to clear terminal and run next example");
        Console.ReadKey();
        Console.Clear();
    }
    static void runSelectExample01()
    {
        
        List<string> words = new List<string>() { "i","love","asp.net","core" };
        // var result = words.Select(x => x.ToUpper());
        var result = from word in words
                     select word.ToLower();

        foreach (var word in result)
        {
            Console.WriteLine(word);
        }
    }
    static IEnumerable<EmployeeDto> runSelectExample02()
    {
        var result = employees.Select(x =>
        {
            return new EmployeeDto(TotalSkills: x.Skills.Count(), Name : x.FullName);
        }

        );
        foreach (var employee in result)
        {
            yield return employee;
        }
    }


    static void runSelectManyExample01()
    {
        string[] sentences = new string[]
        {
            "her we go\n",
            "one piece is the best anime erver\n",
            "game of thrones is the best series ever\n"
        };

        var sentence = sentences.SelectMany(x => x.Split(' '));
        var result = sentence.Select(x => x.ToUpper());
        foreach (var item in result)
        {
            Console.Write($"{item} ");
        }
    }
    static IEnumerable<string> runSelectManyExample02()
    {

        var skills = employees.SelectMany(x => x.Skills);
        var _skills = from employee in employees
                      from each_skills in employee.Skills // employee.Skills all skills lists from employees => each_skills one list of skills
                      select each_skills;
        HashSet<string> commonSkills = new HashSet<string>();
        foreach (var skill in skills)
        {
            commonSkills.Add(skill);
        }
        foreach (var skill in commonSkills) 
            yield return skill;
    }
}