using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Test
{
    public class Root
    {
        public string data { get; set; }
    }
    class Program
    {

        static void Main(string[] args)
        {
          
            #region Problem1
            //Determine any two number in the array can sum up to the first element in the array
            Console.WriteLine("Please provide array input values comma separated which requires sum check for the first element in the array(Eg:  7,3,5,2,-4,8,11  ):");
            var sumArray = Console.ReadLine().Split(',').Select(int.Parse).ToArray();
            string Output = TwoSum(sumArray);
            Console.WriteLine("Determine any two number in the array can sum up to the first element in the array: " + Output);
            #endregion

            #region Problem2
            //WordSplit
            string wordSplitText = "baseball";
            string[] arrToCheck = new string[] { "a", "all", "b", "ball", "bas", "base", "cat", "code", "d", "e", "quit", "z" };
            //string wordSplitText = "abcgefd";
            //string[] arrToCheck = new string[] { "a", "ab", "abc", "abcg", "b", "c", "dog", "e", "efd", "zzzz" };
            //string wordSplitText = "hellocat";
            //string[] arrToCheck = new string[] { "apple", "bat", "cat", "goodbye", "hello", "yellow", "why" };
            string Output1 = string.Empty;
            for (int wordSplit = 1; wordSplit <= 2; wordSplit++)
            {
                string response = WordSplit(wordSplitText, arrToCheck, wordSplit);
                if (!string.IsNullOrEmpty(response) && !string.IsNullOrEmpty(response.Split(',').FirstOrDefault()))
                {
                    if (!string.IsNullOrEmpty(Output1))
                    {
                        Output1 = string.Concat(Output1, ",", response.Split(',').FirstOrDefault());
                    }
                    else
                    {
                        Output1 = response.Split(',').FirstOrDefault();
                    }
                    wordSplitText = response.Split(',').LastOrDefault();
                }
                else
                {
                    Output1 = "not possible";
                }
            }
            Console.WriteLine("WordSplit Output: " + Output1.Trim());
            #endregion

            #region Problem3
            //string reduction
            String str = "bcab";
            int countReduction = stringReduction(str);
            Console.WriteLine("String Reduction Count=" + countReduction);
            #endregion

            #region Problem4
            //GroupTotals
            string[] GroupValues = new string[] { "B:1", "A:1", "B:3", "A:5" };
            string Output4 = GroupTotals(GroupValues);
            Console.WriteLine("GroupTotal Output: " + Output4.Trim());
            #endregion

            #region Problem5
            //Wild Card Characters
            string inputwildCard = "+++++* abcdemmmmmm";
            //string inputwildCard = "**+*{2} mmmrrrkbb";
            // string inputwildCard = "++*{5} gheeeee";
            bool Output5 = WildcardCharacters(inputwildCard);
            Console.WriteLine("Wild Card Characters Output: " + Output5);
            #endregion'

            #region Problem6
            //Palindrome or not
            Console.WriteLine("Please enter string to check palindrome or not:");
            string PalindromeCheck = Console.ReadLine();
            char[] ch = PalindromeCheck.ToCharArray();
            Array.Reverse(ch);
            string rev = new string(ch);
            bool b = PalindromeCheck.Equals(rev, StringComparison.OrdinalIgnoreCase);
            if (b == true)
            {
                Console.WriteLine("" + PalindromeCheck + " is a Palindrome!");
            }
            else
            {
                Console.WriteLine(" " + PalindromeCheck + " is not a Palindrome!");
            }
            #endregion

            #region Problem7
            Console.WriteLine("Problem 7 Coder Byte Service Invokin Process Started...");
            //count how many items exist that have an age equal to or greater than 50, and print final value.
            string url = "https://coderbyte.com/api/challenges/json/age-counting";
            JavaScriptSerializer js = new JavaScriptSerializer();
            var jsonResponse = ServiceHelper(url);
            // jsonResponse = "{ 'data':'key=IAfpK, age=58, key=WNVdi, age=64, key=jp9zt, age=47'}";
            var obj = js.Deserialize<Root>(jsonResponse);
            Regex rx = new Regex("^[0-9]+$");
            int count = obj.data.Split(new string[] { ",", "=" }, StringSplitOptions.None).Where(i => rx.IsMatch(i)).ToList().Where(m => Convert.ToInt32(m) >= 50).Count();
            Console.WriteLine("Count Age greater than 50 =" + count);
            #endregion

        }


        #region Helper Methods

        #region Problem1
        public static string TwoSum(int[] sumArray)
        {
            string output = string.Empty;
            if (sumArray.Count() > 0)
            {
                int numbertocheck = sumArray[0];
                sumArray = sumArray.Skip(1).ToArray();
                Array.Sort(sumArray);
                for (int arrIndex = 0; arrIndex < sumArray.Count(); arrIndex++)
                {
                    if (sumArray[arrIndex] <= numbertocheck)
                    {
                        string searchoutput = ArraySearch(sumArray[arrIndex], sumArray, numbertocheck, arrIndex, sumArray.Count() - 1);
                        if (!string.IsNullOrEmpty(searchoutput))
                        {
                            output = string.Concat(output, searchoutput);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (string.IsNullOrEmpty(output))
            {
                output = "-1";
            }
            return output.Trim();
        }

        public static string ArraySearch(int input, int[] arraytosearch, int compareValue, int startIndex, int endIndex)
        {
            string output = string.Empty;
            for (int end = endIndex; end > startIndex; end--)
            {
                if (arraytosearch[end] + input == compareValue)
                {
                    if (!string.IsNullOrEmpty(output))
                    {
                        output = string.Concat(output, " ", input, ",", arraytosearch[end]);
                    }
                    else
                    {
                        output = string.Concat(" ", input, ",", arraytosearch[end]);
                    }
                }
                else if (arraytosearch[end] + input < compareValue)
                {
                    break;
                }
            }
            return output;
        }

        #endregion

        #region Problem2
        public static string WordSplit(string input, string[] arr, int count)
        {

            string response = string.Empty;
            int index = 0;
            string finder = string.Empty;
            char[] manip = input.ToCharArray();
            for (int z = 0; z < manip.Length; z++)
            {
                finder += manip[z];
                if (arr.Where(m => m.Equals(finder)).Count() > 0)
                {
                    if (!string.IsNullOrEmpty(response))
                    {
                        index = z;
                        response = string.Concat(response, ",", finder);
                    }
                    else
                    {
                        index = z;
                        response = finder;
                    }

                }
                else if (count == 2)
                {
                    response = string.Empty;
                }

            }
            manip = manip.Skip(index + 1).ToArray();
            response = string.Concat(response.Split(',').LastOrDefault(), ",", new string(manip));
            return response;
        }
        #endregion

        #region Problem3
        static int stringReduction(String str)
        {
            Dictionary<string, string> Mappings = new Dictionary<string, string>();
            Mappings.Add("ab", "c");
            Mappings.Add("ac", "b");
            Mappings.Add("bc", "a");
            Mappings.Add("ba", "c");
            Mappings.Add("ca", "b");
            Mappings.Add("cb", "a");
            str = recursivecheck(Mappings, str);
            return str.Length;

        }
        static string recursivecheck(Dictionary<string, string> Mappings, string str)
        {
            foreach (var item in Mappings)
            {
                str = str.Replace(item.Key, item.Value);
            }
            if (str.Length < 2 || str.Distinct().Count() == 1)
            {
                return str;
            }
            else
            {
                return recursivecheck(Mappings, str);
            }
        }
        #endregion

        #region Problem4
        public static string GroupTotals(string[] strArr)
        {
            Dictionary<string, int> GroupConsolidation = new Dictionary<string, int>();
            foreach (var item in strArr)
            {
                if (GroupConsolidation.ContainsKey(item.Split(':').FirstOrDefault()))
                {
                    GroupConsolidation[item.Split(':').FirstOrDefault()] = Convert.ToInt32(GroupConsolidation[item.Split(':').FirstOrDefault()]) + Convert.ToInt32(item.Split(':').LastOrDefault());
                }
                else
                {
                    GroupConsolidation.Add(item.Split(':').FirstOrDefault(), Convert.ToInt32(item.Split(':').LastOrDefault()));
                }
            }
            return string.Join(",", GroupConsolidation.OrderBy(m => m.Key).Where(x => x.Value > 0)
                                     .Select(k => k.Key + ":" + Convert.ToString(k.Value))
                                     .ToArray());
        }
        #endregion

        #region Problem5
        public static bool WildcardCharacters(string inputwildCard)
        {
            char[] pattern = inputwildCard.Split(' ').FirstOrDefault().ToCharArray();
            char[] inputString = inputwildCard.Split(' ').LastOrDefault().ToCharArray();
            int index = 0;
            bool PatternSuccess = true;
            for (int patternTraverse = 0; patternTraverse < pattern.Length; patternTraverse++)
            {
                switch (pattern[patternTraverse])
                {
                    case '+':
                        {
                            if (isValid(inputString[index].ToString()))
                            {
                                index = index + 1;
                                if (patternTraverse + 1 == pattern.Length && index < inputString.Length)
                                {
                                    PatternSuccess = false;
                                }
                            }
                            else
                            {
                                PatternSuccess = false;
                            }
                            break;
                        }
                    case '*':
                        {
                            string charatertoCheck = string.Empty;
                            if (pattern.Length > patternTraverse + 1 && pattern[patternTraverse + 1] == '{')
                            {
                                int counttoMatch = Convert.ToInt32(pattern[patternTraverse + 2].ToString()) + index;
                                patternTraverse = patternTraverse + 3;
                                for (int inputValidation = index; inputValidation < counttoMatch; inputValidation++)
                                {
                                    if (string.IsNullOrEmpty(charatertoCheck))
                                    {
                                        charatertoCheck = inputString[inputValidation].ToString();
                                    }
                                    else
                                    {
                                        if (inputValidation < inputString.Length)
                                        {
                                            if (charatertoCheck != inputString[inputValidation].ToString())
                                            {
                                                PatternSuccess = false;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            PatternSuccess = false;
                                            break;
                                        }
                                    }
                                }
                                index = counttoMatch;
                            }
                            else
                            {
                                for (int inputValidation = index; inputValidation < index + 3; inputValidation++)
                                {
                                    if (string.IsNullOrEmpty(charatertoCheck))
                                    {
                                        charatertoCheck = inputString[inputValidation].ToString();
                                    }
                                    else
                                    {
                                        if (charatertoCheck != inputString[inputValidation].ToString())
                                        {
                                            PatternSuccess = false;
                                            break;
                                        }
                                    }
                                }
                                index = index + 3;
                            }

                            if (patternTraverse + 1 == pattern.Length && index < inputString.Length)
                            {
                                PatternSuccess = false;
                            }
                            break;
                        }
                }
                if (!PatternSuccess)
                {
                    break;
                }
            }
            return PatternSuccess;
        }

        private static bool isValid(String str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z]+$");
        }


        #endregion

        #region Problem7
        public static string ServiceHelper(string url)
        {
            string json = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            return json;
        }
        #endregion

        #endregion
    }
}
