using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleList
{
    class Program
    {
        public class CycleList
        {
            Dictionary<int, int> cycleList = new Dictionary<int, int>(255);
            Dictionary<int, int> tempCycleList = new Dictionary<int, int>(255);

            /// <summary>
            /// Создание однонаправленного циклического списка.
            /// </summary>
            public void CreateCycleList()
            {
                Console.WriteLine("");
                Console.WriteLine("Введите целочисленные знаечния для списка через пробел. По окончании ввода нажмите Enter.");
                string[] text = Console.ReadLine().Split(' ');
                int[] NEWcycleLIST = new int[text.Length];

                for (int i = 0; i < NEWcycleLIST.Length; i++)
                    NEWcycleLIST[i] = Convert.ToInt32(text[i]);

                int key = 2;
                foreach (var item in NEWcycleLIST)
                {
                    cycleList.Add(key, item);
                    key++;
                }
                int y = NEWcycleLIST.Length + 1;
                var tempValue = cycleList[y];
                cycleList.Remove(y);
                cycleList.Add(1, tempValue);

                Print();
            }

            /// <summary>
            /// Поиск элемента по значению.
            /// </summary>
            /// <param name="value">Значение для поиска.</param>
            /// <returns>Первый найденный ключ для значения.</returns>
            public int SearchElement(int value)
            {
                int key = -1;
                foreach (var item in cycleList)
                {
                    if (value == item.Value)
                    {
                        key = item.Key;
                        return key;
                    }
                }
                return key;
            }

            /// <summary>
            /// Удаление ключа по значению
            /// </summary>
            /// <param name="value">Значение, которое надо удалить</param>
            public void DeleteElement(int value)
            {
                int key = SearchElement(value);
                if (key != -1)
                {
                    foreach (var item in cycleList)
                    {
                        if (item.Key < key)
                            tempCycleList.Add(item.Key, item.Value);

                        else if (item.Key == key)
                            continue;

                        else if (item.Key > key)
                            tempCycleList.Add(item.Key - 1, item.Value);
                    }
                    Print();
                }
                else
                    Console.WriteLine("Элемента для заданного значения не существует:");
            }

            /// <summary>
            /// Вставка элемента после заданного значения
            /// </summary>
            /// <param name="key">Ключ, после которого добавляем</param>
            /// <param name="value">Значение, которое добавляем</param>
            public void InsertElement(int key, int value)
            {
                foreach (var item in cycleList)
                {
                    if (item.Key <= key)
                        tempCycleList.Add(item.Key, item.Value);
                    else if (item.Key == key + 1)
                    {
                        tempCycleList.Add(item.Key, value);
                        tempCycleList.Add(item.Key + 1, item.Value);
                    }
                    else if (item.Key > key)
                        tempCycleList.Add(item.Key + 1, item.Value);
                }
                Print();
            }

            public void InsertElementInTheEnd(int value)
            {
                if (cycleList.Count == 0)
                    cycleList.Add(1, value);
                else
                {
                    int x = cycleList.Count + 1;
                    var tempValue = cycleList[1];
                    cycleList.Remove(1);
                    cycleList.Add(x, tempValue);
                    cycleList.Add(1, value);
                }
                Print();
            }

            /// <summary>
            /// Вывод на экран готового списка
            /// </summary>
            public void Print()
            {
                Console.WriteLine("");
                Console.WriteLine("Cписок имеет вид: ");
                if (tempCycleList.Count > 0)
                {
                    foreach (var item in tempCycleList)
                        Console.WriteLine("Ключ: " + item.Key + ". Значение: " + item.Value + ".");
                    Console.WriteLine("");
                }
                else
                {
                    foreach (var item in cycleList)
                        Console.WriteLine("Ключ: " + item.Key + ". Значение: " + item.Value + ".");
                    Console.WriteLine("");
                }
            }
        }

        static void Main(string[] args)
        {
            CycleList fg = new CycleList();

            Console.WriteLine("Выберите действие. Укажите его номер и нажмите Enter.");
            Console.WriteLine("1. Вставка элемента в конец.");
            Console.WriteLine("2. Поиск элемента в списке.");
            Console.WriteLine("3. Вставка значения после заданного узла списка.");
            Console.WriteLine("4. Удаление элемента по значению.");

            Console.WriteLine();
            int numberSorting = int.Parse(Console.ReadLine());

            if (numberSorting == 1)
            {
                Console.WriteLine("0 - если вставка в пустой список, 1 - если в заполненный. По окончании нажмите Enter.");
                int variant = int.Parse(Console.ReadLine());

                if (variant == 1)
                    fg.CreateCycleList();

                Console.WriteLine("Введите целочисленное значение элемента и нажмите Enter.");
                int val = int.Parse(Console.ReadLine());
                Console.WriteLine();

                fg.InsertElementInTheEnd(val);               
            }

            else if (numberSorting == 2)
            {           
                fg.CreateCycleList();

                Console.WriteLine("Введите целочисленное значение элемента и нажмите Enter.");
                int val = int.Parse(Console.ReadLine());
                Console.WriteLine();
                int result = fg.SearchElement(val);
                if (result != -1)
                    Console.WriteLine("Ключ элемента: " + result);
                else
                    Console.WriteLine("Ключа для заданного элемента не существует.");
            }

            else if (numberSorting == 3)
            {
                fg.CreateCycleList();

                Console.WriteLine("Введите номер узла и нажмите Enter.");
                int key = int.Parse(Console.ReadLine());

                Console.WriteLine("Введите целочисленное значение элемента и нажмите Enter.");
                int val = int.Parse(Console.ReadLine());
                Console.WriteLine();

                 fg.InsertElement(key, val);
            }

            else if (numberSorting == 4)
            {
                fg.CreateCycleList();

                Console.WriteLine("Введите целочисленное значение элемента и нажмите Enter.");
                int val = int.Parse(Console.ReadLine());
                Console.WriteLine();
                fg.DeleteElement(val);    
            }
        }
    }
}
