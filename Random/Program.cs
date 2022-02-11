using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Random
{
    class Program
    {
        static int totalCount = 12;
        static int lastRandom = 12;
        public static int Random(int rand_max)
        {
            int t_ms = DateTime.Now.Millisecond;
            int s = (t_ms * t_ms * (Math.Abs(lastRandom) + 14) * totalCount + 1000);
            totalCount++;
            lastRandom = getMiddle(s) % rand_max;
            return lastRandom;
        }

        public static int getMiddle(int number)
        {
            int digit = getDigitCount(number);
            number = number / 10;
            int middles;
            if (digit > 2)
            {
                middles = number % Convert.ToInt32(Math.Pow(10, digit - 2));
                return middles;
            }
            else
            {
                return number;
            }
        }

        public static int getDigitCount(int number)
        {
            int basamak = 1;
            while (number > 0)
            {
                number = number / 10;
                if (number > 0) basamak++;
                else
                    return basamak;
            }
            return 0; ;
        }
        public static void Random_Array(int sizeOfArray)
        {
            int ArraySize = sizeOfArray;
            int[] R_Array = new int[ArraySize];

            List<int> index_list = new List<int>();

            for (int i = 0; i < ArraySize; i++)
            {
                int index = Math.Abs(Random(ArraySize));
                if (index_list.Contains(index) == false)
                {
                    index_list.Add(index);
                    R_Array[index] = i;
                }
                else i--;
            }
            for (int i = 0; i < R_Array.Length; i++)
                Console.Write(R_Array[i] + "  ");
        }
        public static void Sort(int ArraySize)
        {
            int[] Array1 = new int[ArraySize];
            for (int i = 0; i < ArraySize; i++)
            {
                Array1[i] = Math.Abs(Random(ArraySize));
            }

            for (int i = 0; i < Array1.Length; i++)
                Console.Write(Array1[i] + "  ");


            for (int i = 0; i < Array1.Length; i++)
            {
                for (int j = i + 1; j < Array1.Length; j++)
                {
                    if (Array1[i] > Array1[j])
                    {
                        int tempswap = Array1[i];
                        Array1[i] = Array1[j];
                        Array1[j] = tempswap;
                    }
                    //else if(Array1[i] == Array1[j])
                    //{
                    //    int tempswap = Array1[i + 1];
                    //    Array1[i + 1] = Array1[j];
                    //    Array1[j] = tempswap;
                    //}
                    else continue;
                }
            }
            Console.WriteLine("\n");
            for (int i = 0; i < Array1.Length; i++)
                Console.Write(Array1[i] + "  ");
        }

        public static string[] Permutation(string text)
        {

            int tl = text.Length;
            List<string> rtnList = new List<string>();
            if (tl <= 1)
            {
                return new string[] { text };
            }
            else
            {
                foreach (var item in text)
                {
                    string shortStr = text.Remove(text.IndexOf(item), 1);
                    string[] subPerm = Permutation(shortStr);

                    for (int i = 0; i < subPerm.Length; i++)
                    {
                        rtnList.Add(item + subPerm[i]);
                    }
                }
                //return rtnList.FindAll(s => s.Length == tl).ToArray();
                return rtnList.ToArray();
            }
        }

        public static string[] Combination2(string text, int k)
        {
            int n = text.Length;
            List<string> rtnList = new List<string>();
            if (n == k)
            {
                return new string[] { text };
            }
            else if (n < k)
            {
                return new string[] { };
            }
            else if (k == 1)
            {
                List<string> localRtn = new List<string>();
                for (int i = 0; i < text.Length; i++)
                {
                    localRtn.Add(text[i].ToString());
                }

                return localRtn.ToArray();
            }
            else
            {
                foreach (var item in text)
                {
                    string shortStr = text.Substring(text.IndexOf(item) + 1);
                    string[] subComb = Combination2(shortStr, k - 1);

                    for (int i = 0; i < subComb.Length; i++)
                    {
                        rtnList.Add(item + subComb[i]);
                    }
                }
                return rtnList.ToArray();
            }
        }

        public static string[] Combination(string text, int k)
        {
            int n = text.Length;
            List<string> combinationList = new List<string>();

            if (n <= 1 || k <= 1 || k == n)
            {
                combinationList.Add(text);
                return new string[] { text };
            }
            else
            {
                foreach (var item in text)
                {
                    string newText = text.Remove(text.IndexOf(item), 1);
                    string[] subComb = Combination(newText, k);
                    for (int s = 0; s < subComb.Length; s++)
                    {
                        if (!combinationList.Contains(subComb[s]))
                        {
                            combinationList.Add(subComb[s]);
                        }
                    }
                }
                return combinationList.ToArray();
            }
        }

        public static int CountPattern(string text, string miniText)
        {
            int counter = 0;
            for (int i = 0; i <= text.Length - miniText.Length; i++)
            {
                int j;
                for (j = 0; j < miniText.Length; j++)
                {
                    if (text[i + j] != miniText[j])
                        break;
                }
                if (j == miniText.Length)
                {
                    counter++;
                }
            }
            return counter;
        }

        public static string Replace(string text, string toBeReplaced, string replacedWith)
        {
            string rtnString = "";

            if (CountPattern(text, toBeReplaced) == 0 || toBeReplaced.Length != replacedWith.Length)
            {
                return text;
            }

            for (int i = 0; i <= text.Length - toBeReplaced.Length; i++)
            {
                int j;
                for (j = 0; j < toBeReplaced.Length; j++)
                {
                    if (text[i + j] != toBeReplaced[j])
                        break;
                }

                if (j == toBeReplaced.Length)
                {
                    //DEgistir
                    rtnString += replacedWith;
                    i += toBeReplaced.Length - 1;
                }
                else
                {
                    rtnString += text[i];
                }
            }
            return rtnString;
        }

        public static bool isExistsAtPosition(string bigStr, string smallStr, int index)
        {
            if (smallStr.Length > bigStr.Length)
                return false;

            if (smallStr.Length + index > bigStr.Length)
                return false;

            for (int i = 0; i < smallStr.Length; i++)
            {
                if (bigStr[index + i] != smallStr[i])
                    return false;
            }
            return true;
        }

        public static int getWordCount(string bigStr, string smallStr)
        {
            int counter = 0;
            
            for (int i = 0; i < bigStr.Length - 1; i++)
            {                   
                if (isExistsAtPosition(bigStr, smallStr, i))
                {                    
                    counter++;
                    i = i + smallStr.Length;                    
                }
            }
            return counter;
        }

        public static int biggestNumber(List<int> list)
        {
            int biggest = 0;
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (list[i + 1] > list[i])
                {
                    biggest = list[i + 1];
                }
                biggest = list[i];
            }
            return biggest;
        }

        public static int PatternLenght(string text)
        {
            List<int> list = new List<int>();

            for (int s = 0; s < text.Length - 1; s++)
            {
                text.Remove(text.Length - 1);

                for (int i = 0; i < text.Length; i++)
                {
                    string shortStr = text.Substring(i);
                    if (getWordCount(text, shortStr) > 1)
                    {
                        list.Add(shortStr.Length);
                    }
                }
            }
            return biggestNumber(list);
        }

        static void Main(string[] args)
        {
            //string inStr = "abcdabcdxxxxxxabcdabcdxxxxxxabcdacbd";
            string inStr = "asdasdabcddhfghjklhacdlkdjfgkljabocdd";
            int ptrCpunt = getMaximumDublicatedPatternLength(inStr);
            Console.WriteLine(ptrCpunt);

            //Console.WriteLine(CountPattern("abababab", "ab"));

            //Console.WriteLine(getWordCount("xabcyabzabcvab", "abc" ));

            //Console.WriteLine(PatternLenght("abcdhytyyyyyyyyyujterttertkıofabterttertcdouthydaterttertbcdayyyyyyyyybcd"));

            //for (int i = 0; i < combList.Length; i++)
            //    Console.Write(combList[i] + " \n");


            //string datetime_str = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
            //while (true)
            //{
            //Console.WriteLine("enter the rand_max ");
            //int max = Convert.ToInt32(Console.ReadLine());
            //if (max < 0)
            //{
            //    Console.WriteLine("onlyy positive int");
            //}
            //else
            //{
            //   Console.WriteLine(Random(max));
            //}

            //for (int i = 0; i < 12300; i++)
            //{
            //    int result = Random(20);
            //    if (result < 0)
            //    {
            //        Console.WriteLine(result);
            //    }
            //}

            //for (int i = 9434; i < 9999; i++)
            //{
            //    Console.WriteLine(getMiddle(i));
            //}


            //}

        }

        private static int getMaximumDublicatedPatternLength(string inStr)
        {
            int ptnCount = 0;
            for (int i = 1; i < 1 + inStr.Length / 2; i++)
            {
                List<string> patternList = getPatternList(inStr, i);
                foreach (var ptn in patternList)
                {
                    bool isDouble = checkIsDouble(inStr, ptn);
                    if (isDouble)
                    {
                        ptnCount = i;
                    }
                }
            }

            return ptnCount;
        }

        private static bool checkIsDouble(string inStr, string ptn)
        {
            if (getWordCount(inStr, ptn)>1)
            {
                return true;
            }
            return false;
        }

        private static List<string> getPatternList(string inStr, int patternLength)
        {
            List<string> rtnArray = new List<string>();

            for (int i = 0; i < inStr.Length - patternLength; i++)
            {
                string substr = inStr.Substring(i, patternLength);
                rtnArray.Add(substr);
            }

            return rtnArray;
        }
    }
}

