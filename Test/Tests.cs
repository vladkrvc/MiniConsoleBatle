using Polymorphism;
using NUnit.Framework;

namespace Test
{
    public class Tests
    {
        /// <summary>
        /// Тестирование метода Attack у класса Warrior
        /// Метод должен нанести урон цели
        /// </summary>        
        [TestCase(20)]
        [TestCase(30)]
        [TestCase(10)]
        public void Attack_WithUnit_ShouldDecreaseUnitHealthByDamageValue(int damage)
        {
            var warrior = new Warrior("Test", 100, damage);
            var secondWarrior = new Warrior("Test_2", 100, 20);
            var secondWarriorStartHealth = secondWarrior.Health;

            warrior.Attack(secondWarrior);

            Assert.AreEqual(secondWarrior.Health, secondWarriorStartHealth - warrior.Damage,
                $"Значение Health не уменьшилось на {damage}. " +
                $"Ожидалось: {secondWarriorStartHealth - damage}, получено: {secondWarrior.Health}");
        }

        /// <summary>
        /// Тестирование метода Attack у класса Vampire
        /// Метод должен нанести урон цели, а также исцелить вампиру
        /// половину от нанесенного урона
        /// </summary>        
        [TestCase(20)]
        [TestCase(30)]
        [TestCase(10)]
        public void Attack_WithUnit_ShouldDecreaseHealthAndRestoreHalfOfDamageToVampire(int damage)
        {
            var vampire = new Vampire("Test", 100, damage);
            var warrior = new Warrior("Test_2", 100, 20);

            var vampireStartHealth = vampire.Health;
            var warriorStartHealth = warrior.Health;

            vampire.Attack(warrior);

            Assert.AreEqual(warrior.Health, warriorStartHealth - vampire.Damage,
                $"Значение Health у Warrior не уменьшилось на {damage}. " +
                $"Ожидалось: {warriorStartHealth - damage}, получено: {warrior.Health}");
            Assert.AreEqual(vampire.Health, vampireStartHealth + vampire.Damage / 2,
                $"Значение Health у Vampire после атаки не увеличилось на {vampire.Damage / 2}." +
                $"Ожидалось: {vampireStartHealth + vampire.Damage / 2}, а получено: {vampire.Health}");
        }
    }
}