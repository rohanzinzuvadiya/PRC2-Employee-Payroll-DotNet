using System;
using System.Collections.Generic;

namespace EmployeePayrollSystem
{
    // Interface: a contract that every payroll class must follow
    interface IPayroll
    {
        double CalculateSalary();
    }

    // Base class (Inheritance)
    class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Employee(int id, string name)
        {
            Id = id;
            Name = name;
        }

        // virtual so that child classes can override it (Polymorphism)
        public virtual void ShowDetails()
        {
            Console.WriteLine("Employee ID   : " + Id);
            Console.WriteLine("Employee Name : " + Name);
        }
    }

    // Derived class 1
    class FullTimeEmployee : Employee, IPayroll
    {
        public double BasicPay { get; set; }
        public double Allowance { get; set; }

        public FullTimeEmployee(int id, string name, double basic, double allowance)
            : base(id, name)
        {
            BasicPay = basic;
            Allowance = allowance;
        }

        public virtual double CalculateSalary()
        {
            return BasicPay + Allowance;
        }

        public override void ShowDetails()
        {
            base.ShowDetails();
            Console.WriteLine("Type          : Full Time");
            Console.WriteLine("Salary        : Rs. " + CalculateSalary());
        }
    }

    // Derived class 2
    class PartTimeEmployee : Employee, IPayroll
    {
        public int HoursWorked { get; set; }
        public double HourlyRate { get; set; }

        public PartTimeEmployee(int id, string name, int hours, double rate)
            : base(id, name)
        {
            HoursWorked = hours;
            HourlyRate = rate;
        }

        public double CalculateSalary()
        {
            return HoursWorked * HourlyRate;
        }

        public override void ShowDetails()
        {
            base.ShowDetails();
            Console.WriteLine("Type          : Part Time");
            Console.WriteLine("Salary        : Rs. " + CalculateSalary());
        }
    }

    // Derived class 3 (extra bonus on top of full time salary)
    class Manager : FullTimeEmployee
    {
        public double Bonus { get; set; }

        public Manager(int id, string name, double basic, double allowance, double bonus)
            : base(id, name, basic, allowance)
        {
            Bonus = bonus;
        }

        public override double CalculateSalary()
        {
            return base.CalculateSalary() + Bonus;
        }

        public override void ShowDetails()
        {
            Console.WriteLine("Employee ID   : " + Id);
            Console.WriteLine("Employee Name : " + Name);
            Console.WriteLine("Type          : Manager");
            Console.WriteLine("Salary        : Rs. " + CalculateSalary() + " (includes bonus)");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== EMPLOYEE PAYROLL SYSTEM =====\n");

            // One list holding different kinds of employees (Polymorphism)
            List<Employee> employees = new List<Employee>
            {
                new FullTimeEmployee(101, "Rohan Zinzuvadiya", 30000, 5000),
                new PartTimeEmployee(102, "Aarav Patel", 80, 250),
                new Manager(103, "Meera Shah", 50000, 10000, 8000)
            };

            double totalPayroll = 0;

            foreach (Employee e in employees)
            {
                e.ShowDetails();                 // runtime decides which version runs

                if (e is IPayroll p)
                    totalPayroll += p.CalculateSalary();   // interface call, correct version runs

                Console.WriteLine("-----------------------------------");
            }

            Console.WriteLine("Total Monthly Payroll : Rs. " + totalPayroll);
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
