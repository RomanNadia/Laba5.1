using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ch_lab5
{
    public class Doctor
    {
        public string name;
        public string qvalif;

        public Doctor(string name, string qvalif)
        {
            this.name = name;
            this.qvalif = qvalif;
        }

        public override string ToString()
        {
            return "Name: " + name + ", qvalif: " + qvalif;
        }
    }

    public class WorkDay : Doctor
    {
        public int dayInYear;
        public int patientsNum;
        public int startTimeH;

        public WorkDay(string name, string qvalif, int dayInYear, int patientsNum, int startTimeH) : base(name, qvalif)
        {
            this.dayInYear = dayInYear;
            this.patientsNum = patientsNum;
            this.startTimeH = startTimeH;
        }

        public override string ToString()
        {
            return base.ToString() + ", dayInYear: " + dayInYear + ", patientsNum: " + patientsNum + ", startTimeH: " + startTimeH;
        }
    }

    public class Processor
    {
        public static double avgPatientsNumber(List<WorkDay> workDays, int startDay, int endDay)
        {
            double sum = 0.0;
            int cout = 0;
            foreach (var day in workDays)
            {
                if (day.dayInYear >= startDay && day.dayInYear <= endDay)
                {
                    cout++;
                    sum += day.patientsNum;
                }
            }
            return sum / cout;
        }

        public static int maxCout(List<WorkDay> workDays)
        {
            int max = 0;
            foreach (var day in workDays)
            {
                if (day.patientsNum > max)
                {
                    max = day.patientsNum;
                }
            }

            int count = 0;
            foreach (var day in workDays)
            {
                if (day.patientsNum == max)
                {
                    count += 1;
                }
            }

            return count;
        }

        public static int nextDayStarted(List<WorkDay> workDays, int afterDay)
        {
            List<WorkDay> listCopy = workDays.OrderBy(x => x.dayInYear).ToList();

            bool found = false;
            foreach (var day in workDays)
            {
                if (found)
                {
                    return day.dayInYear;
                }
                if (day.dayInYear == afterDay)
                {
                    found = true;
                }
            }

            return -1;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            counsolTest();
            // test();
        }

        static void counsolTest()
        {
            List<WorkDay> workDays = new List<WorkDay>();


            Console.WriteLine("Enter name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter qalification: ");
            string qvalif = Console.ReadLine();

            Console.WriteLine("Enter day of a year,  patient number and firt working hour (separate with space), to end enter empty line");
            string st;
            while (true)
            {
                st = Console.ReadLine();
                if (st.Equals("")) break;
                String[] stArr = st.Split(" ");
                int dayInYear = Int32.Parse(stArr[0]);
                int patientsNum = Int32.Parse(stArr[1]);
                int startTimeH = Int32.Parse(stArr[2]);
                workDays.Add(new WorkDay(name, qvalif, dayInYear, patientsNum, startTimeH));
            }

            Console.WriteLine("Enter startDay: ");
            int startDay = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter endDay: ");
            int endDay = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter lastDay: ");
            int lastDay = Int32.Parse(Console.ReadLine());

            double avg = Processor.avgPatientsNumber(workDays, startDay, endDay);
            int maxCount = Processor.maxCout(workDays);
            int nextDay = Processor.nextDayStarted(workDays, lastDay);

            workDays.ForEach(Console.WriteLine);

            Console.WriteLine("avg: " + avg);
            Console.WriteLine("maxCount: " + maxCount);
            Console.WriteLine("nextDay: " + nextDay);
        }

        static void test()
        {
            List<WorkDay> workDays = new List<WorkDay>();

            string name = "Bob";
            string qvalif = "Soft";

            workDays.Add(new WorkDay(name, qvalif, 7, 10, 12));
            workDays.Add(new WorkDay(name, qvalif, 2, 15, 12));
            workDays.Add(new WorkDay(name, qvalif, 3, 20, 12));
            workDays.Add(new WorkDay(name, qvalif, 4, 10, 12));
            workDays.Add(new WorkDay(name, qvalif, 5, 20, 12));
            workDays.Add(new WorkDay(name, qvalif, 1, 10, 12));

            double avg = Processor.avgPatientsNumber(workDays, 3, 5);
            int maxCount = Processor.maxCout(workDays);
            int nextDay = Processor.nextDayStarted(workDays, 4);

            workDays.ForEach(Console.WriteLine);

            Console.WriteLine("avg: " + avg);
            Console.WriteLine("maxCount: " + maxCount);
            Console.WriteLine("nextDay: " + nextDay);
        }
    }
}

