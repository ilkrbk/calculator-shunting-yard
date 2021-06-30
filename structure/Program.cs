using System;
using System.Collections.Generic;

namespace structure
{
    public class Stack<T>
    {
        private List<T> _items = new List<T>();
        public int Count => _items.Count;
        public void Push(T item)
        {
            _items.Add(item);
        }
        public T Pop()
        {
            var item = _items[_items.Count - 1];
            _items.RemoveAt(_items.Count - 1);
            return item;
        }
        public T Peek()
        {
            var item = _items[_items.Count - 1];
            return item;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string str = Console.ReadLine();
            Console.WriteLine(OperationResult(
                ShuntingYard(
                    ConcatCharInStr(
                        RemoveSpaceInArrayChar(
                            SplitToArrayChar(str))))));
            // 3+43 * 3 - 48 /2 * 4
        }
        static List<char> SplitToArrayChar(string str)
        {
            List<char> arrayChar = new List<char>();
            if (str != null)
                foreach (var item in str)
                    arrayChar.Add(item);
            return arrayChar;
        }
        static List<char> RemoveSpaceInArrayChar(List<char> arrayChar)
        {
            for (int i = 0; i < arrayChar.Count; i++)
                if (arrayChar[i] == ' ')
                    arrayChar.RemoveAt(i);
            return arrayChar;
        }
        static List<string> ConcatCharInStr(List<char> arrayChar)
        {
            List<string> arrayStr = new List<string>();
            string str = "";
            for (int i = 0; i < arrayChar.Count; i++)
                if (arrayChar[i] != '+' && arrayChar[i] != '-' && arrayChar[i] != '*' && arrayChar[i] != '/')
                {
                    str += arrayChar[i];
                    if (i == arrayChar.Count - 1)
                        arrayStr.Add(str);
                }
                else
                {
                    arrayStr.Add(str);
                    str = "";
                    arrayStr.Add(Convert.ToString(arrayChar[i]));
                }
            return arrayStr;
        }
        static List<string> ShuntingYard(List<string> arrayStr)
        {
            Stack<string> operation = new Stack<string>();
            List<string> output = new List<string>();
            int varCheckNum = 0;
            int a = 0;
            for (int i = 0; i < arrayStr.Count; i++)
            {
                if (arrayStr[i] == "+" || arrayStr[i] == "-" || arrayStr[i] == "*" || arrayStr[i] == "/")
                {
                    (string, int) resultSearchPriority = CheckPriority(arrayStr[i]);
                    if (resultSearchPriority.Item2 < varCheckNum)
                    {
                        GetOutStack(ref operation, ref output);
                        operation.Push(resultSearchPriority.Item1);
                    }
                    else if (resultSearchPriority.Item2 == varCheckNum)
                    {
                        output.Add(arrayStr[a]);
                        operation.Pop();
                        operation.Push(arrayStr[i]);
                    }
                    else
                        operation.Push(arrayStr[i]);
                    varCheckNum = resultSearchPriority.Item2;
                    a = i;
                }
                else if (i == arrayStr.Count - 1)
                {
                    operation.Push(arrayStr[i]);
                    GetOutStack(ref operation, ref output);
                }
                else
                    output.Add(arrayStr[i]);
            }
            return output;
        }
        static double OperationResult(List<string> list)
        {
            double result = 0;
            for (int i = 0; i < list.Count; i++)
            {
                switch (list[i])
                {
                    case "+":
                    {
                        result = (Convert.ToDouble(list[i - 2]) + Convert.ToDouble(list[i - 1]));
                        list[i] = Convert.ToString(result);
                        list.RemoveAt(i - 2);
                        list.RemoveAt(i - 2);
                        i = i - 2;
                        break;
                    }
                    case "-":
                    {
                        result = (Convert.ToDouble(list[i - 2]) - Convert.ToDouble(list[i - 1]));
                        list[i] = Convert.ToString(result);
                        list.RemoveAt(i - 2);
                        list.RemoveAt(i - 2);
                        i = i - 2;
                        break;
                    }
                    case "*":
                    {
                        result = (Convert.ToDouble(list[i - 2]) * Convert.ToDouble(list[i - 1]));
                        list[i] = Convert.ToString(result);
                        list.RemoveAt(i - 2);
                        list.RemoveAt(i - 2);
                        i = i - 2;
                        break;
                    }
                    case "/":
                    {
                        result = (Convert.ToDouble(list[i - 2]) / Convert.ToDouble(list[i - 1]));
                        list[i] = Convert.ToString(result);
                        list.RemoveAt(i - 2);
                        list.RemoveAt(i - 2);
                        i = i - 2;
                        break;
                    }
                }
            }
            return result;
        }
        static void GetOutStack(ref Stack<string> stack, ref List<string> output)
        {
            while (stack.Count > 0)
            {
                output.Add(stack.Pop());
            }
        }
        static (string, int) CheckPriority(string varForCheck)
        {
            (string, int) result = ("", 0);
            switch (varForCheck)
            {
                case "+":
                    result = ("+", 1);
                    return result;
                case "-":
                    result = ("-", 1);
                    return result;
                case "*":
                    result = ("*", 2);
                    return result;
                case "/":
                    result = ("/", 2);
                    return result;
                default: return result;
            }
        }
    }
}