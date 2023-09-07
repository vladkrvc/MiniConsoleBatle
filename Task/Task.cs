using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniConsoleGame
{
    public class Task
    {
        public static void Main(string[] args)
        {
            // Создаем список из персонажей для того, чтобы удобнее было с ними работать
            var units = new List<Unit>
            {
                new Vampire("Владимир", 70, 15),
                new Warrior("Утер", 100, 10),
                new Esquire("Гаррош", 90, 5),
            };
            
            // Пока не останется только один персонаж, мы будем выполнять атаку случайного персонажа на случайного персонажа
            while (units.Count > 1)
            {
                // Проходимся по всем персонажам в копии списка
                // units (чтобы избежать удаления элемента во время цикла foreach)
                foreach (var unit in units.ToList())
                {
                    // Записываем в переменную attacker атакующего персонажа
                    var attacker = unit;
                    // Записываем в переменную defender защищающегося персонажа
                    var defender = units.FirstOrDefault(u => u != attacker);
                    
                    // Аттакуем персонажа defender персонажем attacker
                    unit.Attack(defender);

                    // Если защищающийся персонаж погиб, удаляем его из списка
                    if (!defender.IsAlive)
                    {
                        units.Remove(defender);
                    }
                }
            }
            
            // Последний персонаж остается в списке, значит он победил
            var winner = units.First();
            // Выводим на экран сообщение о победителе
            Console.WriteLine($"Победитель - {winner.Name}. У него осталось {winner.Health} здоровья.");
        }
    }

    public class Unit
    {
        public string Name;
        public int Health;
        public int Damage;

        public bool IsAlive
        {
            get
            {
                return Health > 0;
            }
        }

        protected Unit(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            Damage = damage;
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
            
            if (IsAlive)
            {
                Console.WriteLine($"{Name} получил урон {damage} урона. Осталось здоровья - {Health}");
            }
            else
            {
                Console.WriteLine($"{Name} пал в бою");
            }
        }

        
        public virtual void Attack (Unit unit)
        {
            unit.TakeDamage(Damage);
        }
    }

    public class Vampire : Unit
    {
        public Vampire(string name, int health, int damage): base(name, health, damage)
        {
            Console.WriteLine($"Появился вампир {Name}. C {Health} здоровья. Урон - {Damage}");
        }
        
        public override void Attack(Unit unit)
        {
            Health += Damage / 2;
            base.Attack(unit);
        }
        
    }

    public class Warrior : Unit
    {
        public Warrior(string name, int health, int damage): base(name, health, damage)
        {
            Console.WriteLine($"Появился воин {Name}. C {Health} здоровья. Урон - {Damage}");
        }
    }

    public class Esquire : Unit
    {
        public Esquire(string name, int health, int damage): base(name, health, damage)
        {
            Console.WriteLine($"Появился щитоносец {Name}. C {Health} здоровья. Урон - {Damage}");
        }

        public override void TakeDamage(int damage)
        {
            damage /= 2;
            base.TakeDamage(damage);
        }
    }
}