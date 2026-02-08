using System.Runtime.InteropServices;

namespace Task4
{
    //Q3
    internal class Feature
    {
        public readonly int MinApp = 1;
        public bool IsEnabled { get; set; }
        public int MinVersion { get; set; }
        public Feature(bool enabled, int minVersion)
        {
            IsEnabled = enabled;
            MinVersion = minVersion;
        }
    }
    //Q4
    class User
    {
        public string Name { get; set; }
        public User(string name)
        {
            Name = name;
        }
    }
    struct UserSnapShot
    {
        public string SnapName { get; set; }
        public UserSnapShot(string snapName)
        {
            SnapName = snapName;
        }
    }
    //Q5
    class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException(string message) : base(message) { }
    }
    class PaymentTimeoutException : Exception
    {
        public PaymentTimeoutException(string message) : base(message) { }
    }
    //----------
    internal class Program
    {
        //Q2
        public const int MinLogin = 3;
        public const int MinExport = 0;
        public const int MinAdminPanel = 2;


        //------
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t\t\tQ1\n");

            #region Q1
            Console.WriteLine(RuntimeInformation.OSArchitecture);
            Console.WriteLine(RuntimeInformation.ProcessArchitecture);
            var version = RuntimeInformation.FrameworkDescription;
            Console.WriteLine(version);
            if (version.Contains(".NET") || version.Contains(".NET Core"))
                Console.WriteLine("Modern .NET Runtime");
            else
                Console.WriteLine("Legacy Runtime");
            #endregion

            Console.WriteLine("\t\t\t\t\tQ2\n");

            #region Q2
            Feature login = new Feature(true, MinLogin);
            Feature export = new Feature(true, MinExport);
            Feature adminPanel = new Feature(true, MinAdminPanel);

            if (login.IsEnabled && login.MinVersion > login.MinApp)
                Console.WriteLine("Login Is Running");
            else
                Console.WriteLine("Login Is Not Running");

            string ExportResult = (export.IsEnabled && export.MinVersion > export.MinApp) ? "Export Is Running" : "Export Isn't Running";
            Console.WriteLine(ExportResult);
            #endregion

            Console.WriteLine("\t\t\t\t\tQ3\n");

            #region Q3
            List<int> numbers = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            NumberClassifiaction(numbers);


            static void NumberClassifiaction(List<int> numbers)
            {
                var oddList = new List<int>();
                var evenList = new List<int>();
                var praimaryList = new List<int>();
                foreach (int num in numbers)
                {
                    if (IsEven(num))
                        evenList.Add(num);
                    else
                        oddList.Add(num);
                    if (IsPrimary(num))
                        praimaryList.Add(num);
                }
                Console.WriteLine("Even Numbers : ");
                foreach (int num in evenList) Console.Write($"{num} ");
                Console.WriteLine("\nodd Numbers : ");
                foreach (int num in oddList) Console.Write($"{num} ");
                Console.WriteLine("\nPrimary Numbers : ");
                foreach (int num in praimaryList) Console.Write($"{num} ");
            }
            static bool IsEven(int num) => num % 2 == 0;
            static bool IsPrimary(int num)
            {
                if (num < 2) return false;
                if (num == 2) return true;
                if (num % 2 == 0) return false;
                for (int i = 3; i < num - 1; i += 2)
                {
                    if (num % i == 0) return false;
                }
                return true;

            }
            #endregion

            Console.WriteLine("\t\t\t\t\tQ4\n");

            //Q4
            #region Q4
            User user = new User("Nada");
            UserSnapShot userSnap = new UserSnapShot("Nada");

            WithOutRef(user, userSnap);//With out Reference
            Console.WriteLine(user.Name); // CSharp => بعتنا الاوبجكيت ف عدل عادي
            Console.WriteLine(userSnap.SnapName); // Nada =>بعتنا نسخة من الاوبجيكت ف مش اتاثر 

            WithRef(ref user, ref userSnap);// With Reference 
            Console.WriteLine(user.Name); // CSharp => بعتنا الاوبجكيت ف عدل عادي
            Console.WriteLine(userSnap.SnapName); // CSharp =>بعتنا المكان بتاع الاوبجيكت ف اتاثر


            static void WithOutRef(User user, UserSnapShot userSnap)
            {
                user.Name = "CSharp";
                userSnap.SnapName = "CSharp";
            }
            static void WithRef(ref User user, ref UserSnapShot userSnap)
            {
                user.Name = "CSharp";
                userSnap.SnapName = "CSharp";
            }
            #endregion
            Console.WriteLine("\t\t\t\t\tQ5\n");

            #region Q5
            static void PaymentSystem(decimal balance, decimal amount, bool timeout)
            {
                if (balance < amount)
                    throw new InsufficientBalanceException("Balance is not Enough");
                else
                    Console.WriteLine("Balance is enough to pay");
                if (!timeout)
                    throw new PaymentTimeoutException("Payment Time Out.");
                else
                    Console.WriteLine("time is enough ");
            }

            try
            {
                PaymentSystem(0, 10, true);
            }
            catch (InsufficientBalanceException ex)
            {
                Console.WriteLine($"InsufficientBalanceException : {ex.Message}");
            }
            catch (PaymentTimeoutException ex)
            {
                Console.WriteLine($"PaymentTimeoutException : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Exception : {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Payment system is finished");
            }

            #endregion

            Console.WriteLine("\t\t\t\t\tPart2\n");
            //Part 2

            Console.WriteLine("\t\t\t\t\tQ1\n");
            #region Longest Commom Prefix

            //public string LongestCommonPrefix(string[] strs)
            //{
            //    if (strs == null || strs.Length == 0)
            //        return "";
            //    string prefix = strs[0];

            //    for (int i = 1; i < strs.Length; i++)
            //    {
            //        while (strs[i].IndexOf(prefix) != 0)
            //        {
            //            prefix = prefix.Substring(0, prefix.Length - 1);
            //            if (string.IsNullOrEmpty(prefix))
            //                return "";
            //        }
            //    }
            //    return prefix;
            //}

            #endregion

            Console.WriteLine("\t\t\t\t\tQ2\n");

            #region Contains Duplicate
            //     public bool ContainsDuplicate(int[] nums)
            //{
            //    Dictionary<int, int> freq = new Dictionary<int, int>();

            //    foreach (int num in nums)
            //    {
            //        if (freq.ContainsKey(num))
            //        {
            //            return true;
            //        }
            //        freq[num] = 1;
            //    }
            //    return false;
            //}
            //public bool ContainsDuplicate(int[] nums)
            //{
            //    Dictionary<int, int> freq = new Dictionary<int, int>();

            //    foreach (int num in nums)
            //    {
            //        if (freq.ContainsKey(num))
            //        {
            //            return true;
            //        }
            //        freq[num] = 1;
            //    }
            //    return false;
            //}
            #endregion

            Console.WriteLine("\t\t\t\t\tQ3\n");
            #region Valid Anagram
            //bool IsAnagram(string s, string t)
            //{
            //    if (s.Length != t.Length)
            //        return false;
            //    char[] arrs = s.ToCharArray();
            //    Array.Sort(arrs);
            //    string sorteds = new string(arrs);
            //    char[] arrt = t.ToCharArray();
            //    Array.Sort(arrt);
            //    string sortedt = new string(arrt);
            //    if (sortedt == sorteds)
            //        return true;
            //    return false;
            //}
            #endregion
        }

    }
}

