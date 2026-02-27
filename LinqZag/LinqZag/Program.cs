using System.Linq;
using System.Xml.Linq;

namespace LinqZag
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Question Number 1
            Console.WriteLine("Question Number 1\n");
            List<int> numbers = [3, 18, 7, 42, 10, 5, 29, 14, 6, 100];

            //Query Syntax

            var result = from n in numbers
                         where (n % 2 == 0 && n > 10)
                         orderby n descending
                         select n;

            //Fluent Syntax
            var result1 = numbers.Where(n => (n % 2 == 0 && n > 10)).OrderByDescending(n => n);
            foreach (var item in result1)
            {
                Console.Write($"{item} , ");
            }
            Console.WriteLine("");
            #endregion

            Console.WriteLine("*************************************************************\n");

            List<Product> products =
            [
            new(1, "Laptop",1200m, "Electronics"),
            new(2, "Phone",800m, "Electronics"),
            new(3, "Desk",350m, "Furniture"),
            new(4, "Chair",150m, "Furniture"),
            new(5, "Headphones", 200m, "Electronics"),
            ];

            #region Question Number 2
            Console.WriteLine("Question Number 2");

            Console.WriteLine("1.Get the first Electronics product");
            //first
            try
            {
                var firstElectronicsProduct = products.Where(p => p.Name == "Electronics")
                                      .First();// must put it in try , if not found throw Exception
                Console.WriteLine($"the first Electronics product {firstElectronicsProduct}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("No matching product found");
            }

            //firstOrDefault
            var firstElectronicsProduct1 = products.Where(p => p.Name == "Electronics")
                                      .FirstOrDefault(); //if not found take null
            if (firstElectronicsProduct1 == null)
                Console.WriteLine("No matching product found");
            else
                Console.WriteLine($"the first Electronics product {firstElectronicsProduct1}");
            Console.WriteLine("");
            Console.WriteLine("Get the last product with Price > 1000 (use OrDefault — handle null)");

            //Last
            try
            {
                var LastProduct = products.Where(p => p.Price > 1000)
                                      .Last();// must put it in try , if not found throw Exception
                Console.WriteLine($"the last product with Price > 1000 {LastProduct}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("No matching product found");
            }

            //LastOrDefault
            var LastProduct1 = products.Where(p => p.Price > 1000)
                                      .LastOrDefault(); //if not found take null
            if (LastProduct1 == null)
                Console.WriteLine("No matching product found");
            else
                Console.WriteLine($"the last product with Price > 1000 {LastProduct1}");
            Console.WriteLine("");
            Console.WriteLine("3. Get the single Furniture item with Price > 300 (what if >1 match?) ");

            //Single
            try
            {
                var singleFurnitureItem = products.Where(p => (p.Name == "Furniture" && p.Price > 300))
                                      .Single();// must put it in try , if not found throw Exception
                Console.WriteLine($"the single Furniture item with Price > 300 {singleFurnitureItem}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("No matching product found");
            }

            //SingleOrDefault
            var singleFurnitureItem1 = products.Where(p => (p.Name == "Furniture" && p.Price > 300))
                                      .SingleOrDefault(); //if not found take null
            if (singleFurnitureItem1 == null)
                Console.WriteLine("No matching product found");
            else
                Console.WriteLine($"the single Furniture item with Price > 300 {singleFurnitureItem1}");

            Console.WriteLine("");
            Console.WriteLine("4. Get the element at index 3 ");
            //Element At

            var elementAtIndex = products.ElementAt(3);
            Console.WriteLine(elementAtIndex);

            #endregion

            Console.WriteLine("*************************************************************\n");

            #region Question Number 3
            Console.WriteLine("Question Number 3\n");

            Console.Write("1. Are ALL products priced above 100? ");
            bool allProducts = products.All(P => P.Price > 100);
            Console.WriteLine(allProducts);

            Console.Write("2. Is THERE ANY product in the Gaming category? ");
            bool anyProduct = products.Any(C => C.Category == "Gaming");
            Console.WriteLine(anyProduct);

            Console.Write("3. Does the collection CONTAIN a product named \"Chair\" ? ");
            var containsChair = products.Contains(
            new Product(4, "Chair", 150m, "Furniture"));
            Console.WriteLine(containsChair);

            Console.Write("4. Are ALL Electronics products priced above 500? ");
            bool CheckElectronics = products.All(P => (P.Name == "Electronics" && P.Price > 500));
            Console.WriteLine(CheckElectronics);

            Console.Write("5. Is there ANY product cheaper than 200? ");
            bool Cheaper = products.Any(p => p.Price < 200);
            Console.WriteLine(Cheaper);
            #endregion

            Console.WriteLine("*************************************************************\n");

            #region Question Number 4
            Console.WriteLine("Question Number 4");
            // 1.Convert to Array
            var ToArray = products.ToArray();

            Console.Write("2.Convert to Dictionary keyed by Id => ");
            var ToDictionary = products.ToDictionary(P => P.Id);
            Console.WriteLine(ToDictionary[3]);

            Console.Write("3.Convert to HashSet of product Names => ");
            HashSet<string> ToHashSet = products.Select(P => P.Name).ToHashSet();
            foreach (var item in ToHashSet)
            {
                Console.Write($"{item} ");
            }

            Console.Write("4.Convert to Lookup keyed by Category => ");
            var ToLookup = products.ToLookup(P => P.Category);
            var ElectronicsCategory = ToLookup["Electronics"];
            foreach (var item in ElectronicsCategory)
            {
                Console.Write($"{item} ");
            }


            // What exception does ToDictionary throw if keys are duplicated? => throw Exception ( ArgumentException )

            // How does ToLookup handle duplicate keys differently ? => ToLookup Not Throw Exception but Group All Element Shared With Same Key
            #endregion

            Console.WriteLine("*************************************************************\n");

            #region Question Number 5
            Console.WriteLine("\nQuestion Number 5");
            List<string> orders = ["ORD-001", "ORD-002", "ORD-003",
                                   "ORD-004", "ORD-005", "ORD-006", "ORD-007"];

            Console.Write("1. Get Page 1 (items 1–3) => ");
            var paginationPage1 = orders.Take(3);
            foreach (var item in paginationPage1)
            {
                Console.Write($"{item} , ");
            }

            Console.Write("\n 2. Get Page 2 (items 4–6) => ");
            var paginationPage2 = orders.Skip(3).Take(3);
            foreach (var item in paginationPage2)
            {
                Console.Write($"{item} , ");
            }

            Console.Write("\n 3. Get the last 2 orders using TakeLast => ");
            var last2Orders = orders.TakeLast(2);
            foreach (var item in last2Orders)
            {
                Console.Write($"{item} , ");
            }
            Console.Write("\n 4. Drop the first and last order using Skip + SkipLast => ");
            var dropFirstAndLast = orders.Skip(1).SkipLast(1);
            foreach (var item in dropFirstAndLast)
            {
                Console.Write($"{item} , ");
            }

            Console.Write("\n 5.Write a generic Paginate(source, pageNumber, pageSize) method =>");

            var paginat = Paginate(orders, 2, 3);
            foreach (var p in paginat)
            {
                Console.Write($"{p} , ");
            }
            Console.WriteLine();
            #endregion

            List<Employee> employees =
                                      [
                                      new("Ali","Engineering", 9000m),
                                      new("Sara", "Engineering", 8500m),
                                      new("Omar", "HR",6000m),
                                      new("Mona", "HR",6200m),
                                      new("Yara", "Marketing", 7000m),
                                      new("Karim", "Marketing", 7500m),
                                      new("Nada", "Engineering", 9500m),
                                      ];
            Console.WriteLine("*************************************************************\n");

            #region Question Number 6
            Console.WriteLine("Question Number 6 ");
            Console.Write(" 1. Project to anonymous type: { FullName = Name.ToUpper(), Salary } => ");
            var anonymous = employees.Select(E => new
            {
                FullName = E.Name.ToUpper(),
                Salary = E.Salary
            }).ToList();

            foreach (var emp in anonymous)
            {
                Console.Write($"{emp} ");
            }


            Console.Write(" 2. Project to a formatted string: Ali works in Engineering — EGP 9,000 => ");
            var FormattedString = employees.
                                  Select(E => $"{E.Name} works in {E.Department} - EGP {E.Salary}")
                                  .ToList();
            foreach (var emp in FormattedString)
            {
                Console.Write($"{emp} ");
            }


            Console.Write("3. Sort by Salary descending, then use indexed Select to add Rank:"); 
            var randedList = employees.OrderByDescending(E => E.Salary)
                                      .Select((emp, index) => new
                                      {
                                          Rank = index + 1,
                                          emp.Name,
                                          emp.Salary
                                      }).ToList();
            foreach (var emp in randedList) 
                Console.Write($"{emp} ");


            Console.Write(" BONUS: Project each employee to include a SeniorityLevel  property: => ");
            var levelOfEmployee = employees.OrderByDescending(E => E.Salary)
                                      .Select((emp, index) => new
                                      {
                                          Rank = index + 1,
                                          emp.Name,
                                          emp.Salary,
                                          SeniorityLevel = emp.Salary >= 9000 ? "Senior" :
                                                           emp.Salary < 9000 && emp.Salary > 7000 ? "Mid"
                                                           : "Junior"
                                      }).ToList();
            foreach (var emp in levelOfEmployee)
                Console.Write($"{emp} ");

            #endregion
            Console.WriteLine("*************************************************************\n");

            #region Question Number 7
            Console.WriteLine("Question Number 7 ");

            List<int> scores = [88, 92, 75, 60, 55, 80, 91, 45];
            Console.Write(" 1. TakeWhile score >= 70 → expected: [88, 92, 75] => ");
            var takeWhile = scores.TakeWhile(S => S >= 70);

            foreach (var item in takeWhile)
                Console.Write($"{item} ");

            Console.Write("2. SkipWhile score >= 70 → expected: [60, 55, 80, 91, 45] => ");
            var skipWhile = scores.SkipWhile(S => S >= 70);

            foreach (var item in skipWhile) 
                Console.Write($"{item} ");
            // 3. What is the difference between this and using Where? Explain in a comment.

            // TakeWhile => بتمشي علي العناصر من الاول لو ترو بتضيفه ف الليسته بتكمل لعند اما توصل عند عنصر مش بيطابق الشرط ف يعمل بريك

            // SkipWhile => بتمشي علي العناصر لو ترو بتعمله سكيب اول ما تلاقي عنصر فولس بتعمل بريك وتاخد من اول العنصر الفولس لعند الاخر 

            // Where => بتعدي علي كل العناصر تعمل فلتره لو العنصر بترو بتضيفه ف الليسته لو فولس بتخش علي العنصر الي بعده وهكذا
            #endregion
            Console.WriteLine("*************************************************************\n");

            #region Question Number 8
            Console.WriteLine("Question Number 8 ");
            Console.Write("1. Group by Department, print: \"Engineering → Count: 3, Avg: 9000\" => ");
            var groupByDepartment = employees.GroupBy(E => E.Department)
                                .Select(Emp => new
                                {
                                    Emp.Key,
                                    Count = Emp.Count(),
                                    Avg = Emp.Average(E => E.Salary)
                                });
            foreach (var item in groupByDepartment) 
                Console.Write($"{item} ");


            Console.Write("2. Find the department with the highest total salary budget => ");
            var HighestTotalSalary = employees.GroupBy(E => E.Department)
                                     .Select(emp => new
                                     {
                                         emp.Key,
                                         max = emp.Sum(E => E.Salary)
                                     }).OrderByDescending(emp => emp.max).First();
            Console.Write(HighestTotalSalary);


            Console.Write("3. List employees in each group ordered by Salary descending => ");
            var employeesDepartments = employees.GroupBy(D => D.Department)
                                        .Select(emp => new
                                        {
                                            emp.Key,
                                            Employees = emp.OrderByDescending(E => E.Salary).ToList()
                                        });
            foreach (var item in employeesDepartments)
            {
                Console.WriteLine($"Department : {item.Key}");
                foreach (var employee in item.Employees)
                    Console.Write($"     {employee}");
            }
            #endregion
            Console.WriteLine("*************************************************************\n");

            #region Question Number 9
            Console.WriteLine("Question Number 9 ");
            List<int> nums = [1, 2, 3, 4, 5];
            var query = nums.Where(n => n > 2); // ← query defined here
            nums.Add(10);
            foreach (var n in query)
                Console.Write(n + " ");
            //Q: What is printed? Why? => 3 , 4 , 5 ,10  / Where is Deferred Execution operators 
            // الكود اتنفذ في اللوب ف اعترف بال 10

            // Q: How would using .ToList() right after .Where(...) change the result?
            var query1 = nums.Where(n => n > 2).ToList(); // ← query defined here
            nums.Add(10);
            foreach (var n in query1)
                Console.Write(n + " ");
            //الكود اتنفذ وقت كتابته ف مش معترف بال 10

            // Q: Name 3 LINQ operators that trigger immediate execution.=> ToList(),ToArray(),Count()
            #endregion
            Console.WriteLine("*************************************************************\n");

            #region Question Number 10
            Console.WriteLine("Question Number 10 ");
            List<string> words = ["apple", "fig", "banana", "kiwi","grape", "mango", "pear", "plum"];

            Console.Write("1. Filter words longer than 4 characters => ");
            var WordsGreater4 = words.Where(W => W.Length > 4).ToList();
            foreach (var word in WordsGreater4)
            {
                Console.Write($"{word} ");
            }

            Console.Write("\n2. Filter words at even indexes (0, 2, 4, 6...) using (item, index) overload => ");
            var evenWords = words.Where((w, i) => i % 2 == 0).ToList();
            foreach (var eWord in evenWords)
            {
                Console.Write($"{eWord} ");
            }
            Console.Write("\n3. Filter words that are BOTH longer than 4 chars AND at an even index => ");
            var filtered = words.Where((w, i)=>i % 2 == 0 && w.Length>4 );
            foreach (var fWord in filtered)
            {
                Console.Write($"{fWord} ");
            }
            //4. What is the index of "mango" in the filtered result from step 1? 
                //manago doesnot exist because this index is 5 not even 
            Console.WriteLine();
            #endregion
            Console.WriteLine("*************************************************************\n");

            List<Course> courses =
                                  [
                                  new("C# Basics",["Ali", "Sara", "Omar"]),
                                  new("LINQ Mastery", ["Sara", "Mona", "Ali"]),
                                  new("ASP.NET Core", ["Yara", "Omar", "Karim"]),];
            #region Question Number 11
            Console.WriteLine("Question Number 11 ");

            Console.Write("1. Flatten to a single list of ALL student names (with duplicates)  => ");
            var allStudent = courses.SelectMany(S => S.Students).ToList();
            foreach (var stud in allStudent)
            {
                Console.Write($"{stud} ");
            }

            Console.Write("\n2. Get a distinct list of all student names  => ");
            var distinctName = courses.SelectMany(S => S.Students).Distinct().ToList();
            foreach (var name in distinctName)
            {
                Console.Write($"{name} ");
            }

            Console.Write("\n3. Find students who appear in MORE THAN ONE course =>  ");
            var repeatedName = courses.SelectMany(S => S.Students)
                                      .GroupBy(name => name)
                                      .Where(S => S.Count() > 1)
                                      .Select(S => S.Key)
                                      .ToList();
            foreach (var name in repeatedName)
            {
                Console.Write($"{name} ");
            }
            Console.WriteLine("\n4. Use SelectMany with result selector to get (CourseName, StudentName) pairs => ");
            var pairs = courses.SelectMany(course => course.Students,
                                           (course, student) => new { courseName = course.Title, studentName = student })
                               .ToList();
            foreach (var pair in pairs)
            {
                Console.WriteLine($"courseName : {pair.courseName} , studentName : {pair.studentName}");
            }
            Console.WriteLine();
            #endregion

            Console.WriteLine("*************************************************************\n");

            #region Question Number 12
            Console.WriteLine("Question Number 12 ");
            Console.WriteLine("1.From employees: get the TOP 2 highest - paid employees per department. => ");


            var Top2HighestPaidPerDepartment = employees.GroupBy(D => D.Department)
                                        .SelectMany(emp =>
                                        emp.OrderByDescending(e => e.Salary)
                                        .Take(2)).ToList();
            foreach (var emp in Top2HighestPaidPerDepartment)
            {
                Console.WriteLine($"{emp.Department} - {emp.Name} - {emp.Salary}");
            }


            Console.WriteLine("2.From courses: build a Dictionary<string, int> of { CourseName → StudentCount }");

            Dictionary<string, int> CourseWithMoreThan2Students = courses
                                                                    .Where(S => S.Students.Count() > 2)
                                                                    .ToDictionary(
                                                                         D => D.Title,
                                                                         D => D.Students.Count
                                                                     );
            foreach (var item in CourseWithMoreThan2Students)
                Console.WriteLine($"{item.Key} : {item.Value}");

            Console.WriteLine("3.Check: Does ANY employee in Engineering earn less than 8000 ? => ");
            bool EmployeeInEngEarnLessThan8000 = employees
                                                    .Where(E => E.Department == "Engineering")
                                                    .Any(E => E.Salary < 8000);
            Console.Write(EmployeeInEngEarnLessThan8000);

            Console.WriteLine(" Do ALL HR employees earn more than 5500 ? => ");
            bool EmpInHREarnMoreThan5500 = employees
                                                    .Where(E => E.Department == "HR")
                                                    .Any(E => E.Salary > 5500);
            Console.Write(EmpInHREarnMoreThan5500);


            Console.WriteLine(" 4.Project the top - 2 - per - dept result into: => ");

            var Top2PerDept = employees
                .GroupBy(e => e.Department)
                .SelectMany(emp => emp
                .OrderByDescending(e => e.Salary)
                .Take(2)
                .Select((e, index) => new
                {
                    Rank = index + 1,
                    e.Name,
                    e.Department,
                    e.Salary,
                    SeniorityLevel = e.Salary >= 9000 ? "Senior" :
                                                         e.Salary < 9000 && e.Salary > 7000 ? "Mid"
                                                         : "Junior"
                })
                ).ToList();
            foreach (var item in Top2PerDept) Console.WriteLine(item);




            // 5.For each step above — is execution deferred or immediate?
            //=> all deferred except .ToList() Immediate
            #endregion
        }

        private static IEnumerable<T> Paginate<T>(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                throw new ArgumentException("Check your pageNumber and pageSize , Must be > 0 ");

            return source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
