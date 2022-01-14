using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Задание_22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите размерность масиива:  ");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, int> func2 = new Func<Task<int[]>, int>(SumArray);
            Task<int> task2 = task1.ContinueWith<int>(func2);

            Func<Task<int[]>, int> func3 = new Func<Task<int[]>, int>(MaxArray);
            Task<int> task3 = task1.ContinueWith<int>(func3);

            Action<Task<int>> action = new Action<Task<int>>(PrintArray);
            Task task4 = task2.ContinueWith(action);

            Action<Task<int>> action1 = new Action<Task<int>>(PrintArray1);
            Task task5 = task3.ContinueWith(action1);

            task1.Start();
            Console.ReadKey();
        }

        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 100);
                Console.Write($"{array[i]}  ");
            }
            return array;
        }

        static int SumArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }
            return sum;
        }
        static int MaxArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > max)
                    max = array[i];
            }
            return max;
        }

        static void PrintArray(Task<int> task)
        {
            int n = task.Result;
            Console.WriteLine();
            Console.WriteLine($"Сумма равна: {n} ");
        }
        static void PrintArray1(Task<int> task)
        {
            int n = task.Result;
            Console.WriteLine();
            Console.WriteLine($"Максимальное число в массиве: {n} ");
        }
    }/* очень мало времени на учебу и не смог толком разобраться в последних темах,
      * поэтому не понимаю, как ноhvально вывести на экран результаты (не используя метод Print1). Если можете, пожалуйста, объясните в ответе, спасибо!*/
}
