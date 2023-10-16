using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace Health_System_Charlie_Curry
{
    internal class Program
    {

        #region Variables
        //float smallEnemyScoreMultiplier = 0.1f;
        //float mediumEnemyScoreMultiplier = 0.2f;
        //float largeEnemyScoreMultiplier = 0.3f;
        //float bossScoreMultiplier = 0.5f;
        //float finalBossScoreMultiplier = 0.6f;
        //float deathScoreMultiplier = 0.2f;
        static int smallEnemyDamage = 10;
        static int mediumEnemyDamage = 25;
        static int largeEnemyDamage = 40;
        static int bossDamage = 50;
        static int finalBossDamage = 60;
        //float treasurePoints = 250f;
        //float smallEnemyPoints = 100f;
        //float mediumEnemyPoints = 200f;
        //float largeEnemyPoints = 300f;
        //float bossPoints = 1000f;
        //float finalBossPoints = 2000f;
        static int healthPack = 30;
        static string title = "Super Awesome Adventure Video Game";
        //float score = 0f;
        //string realName;
        //string gamerTag;
        static int health = 100;
        static int maxHealth = 100;
        static int lives = 3;
        static int oneUp = 1;
        //float scoreMultiplier = 0f;
        static int shield = 100;
        static int maxShield = 100;
        static int xp = 0;
        static int level = 1;
        static int xpCost = 100 * level;
        static int previousHealthChange;
        static int previousShieldChange;
        static int previousLivesChange;
        #endregion

        static void IncreaseXP(int experience)
        {
            xpCost = level * 100;
            //xp += experience;
                for (int i = experience / 100; i > 0; i--)
                {
                    xp += 100;
                    if (xp >= xpCost)
                    {
                         level++;
                         xp -= xpCost;
                         xpCost = 100 * level;
                    }
                }
            xp += experience % 100;
            if (xp >= xpCost)
            {
                level++;
                xp -= xpCost;
                xpCost = 100 * level;
            }
        }

        static void ResetGame()
        {
            //Console.WriteLine("Enter Your Name:");
            //realName = Console.ReadLine().Trim();
            //Console.WriteLine("Enter Your GamerTag:");
            //gamerTag = Console.ReadLine().Trim();
            health = maxHealth;
            shield = maxShield;
            //score = 0f;
            lives = 3;
            xp = 0;
            level = 1;
            xpCost = 100 * level;
            previousHealthChange = 0;
            previousShieldChange = 0;
            previousLivesChange = 0;
            //scoreMultiplier = 0f;
            Console.WriteLine("New Game");
        }

        static string HealthStatus(int healthVar)
        {
            if (healthVar == 100)
            {
                return "Perfect Health";
            }
            else if (healthVar < 100 && healthVar >= 75)
            {
                return "Healthy";
            }
            else if (healthVar < 75 && healthVar >= 50)
            {
                return "Hurt";
            }
            else if (healthVar < 50 && healthVar >= 10)
            {
                return "Badly Hurt";
            }
            else if (healthVar < 10)
            {
                return "Imminent Danger";
            }
            return null;
        }

        static void Revive()
        {
            previousHealthChange = 100;
            previousShieldChange = 100;
            previousLivesChange = -1;
            health = maxHealth;
            shield = maxShield;
            lives--;
            //scoreMultiplier -= deathScoreMultiplier;
        }

        static void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                Console.WriteLine("Error: Player Cannot Take " + damage + " Damage");
            }
            else if (health - damage <= 0)
            {
                previousHealthChange = - (health - damage);
                health = 0;
                shield = 0;
            //scoreMultiplier -= deathScoreMultiplier;
            }
            else if (shield - damage <= 0)
            {
                previousHealthChange = -(damage - shield);
                previousShieldChange = -shield;
                health -= (damage - shield);
                shield = 0;
            }
            else if (shield > 0)
            {
                previousShieldChange = -damage;
                previousHealthChange = 0;
                shield -= damage;

            }
            else if (health > 0)
            {
                previousHealthChange = -damage;
                previousShieldChange = 0;
                health -= damage;
            }

        }

        static void Heal(int hp)
        {
            if (hp < 0)
            {
                Console.WriteLine("Error: Player Cannot Heal " + hp + " HP");
            }
            else if (health + hp > 100)
            {
                previousHealthChange = +100 - (health + hp);
                health = maxHealth;
            }
            else
            {
                previousHealthChange = hp;
                health += hp;
            }

        }

        static void RegenerateShield(int hp)
        {
            if (hp < 0)
            {
                Console.WriteLine("Error: Player Cannot Regenerate " + hp + " Shield");
            }
            else if (shield + hp > 100)
            {
                previousShieldChange = +100 - (shield + hp);
                shield = maxShield;
            }
            else
            {
                previousShieldChange = hp;
                shield += hp;
            }
        }

        static void OneUp()
        {
            previousLivesChange = 1;
            lives++;
        }

        //void AddScore(float pointsEarned)
        //{
        //score += pointsEarned * scoreMultiplier;
        //}

        static void ShowHUD()
        {
            Console.WriteLine(title);
            Console.WriteLine("----------");

            //Console.WriteLine(realName + " AKA " + gamerTag);
            Console.WriteLine("Health: " + health + "% " + previousHealthChange + " | " + "Shield: " + shield + "% " + previousShieldChange);
            Console.WriteLine("Lives: " + lives + " " + previousLivesChange);
            Console.WriteLine(HealthStatus(health));
            Console.WriteLine("XP: " + xp + " | " + "Level: " + level + " | " + "XP to Next Level: " + (xpCost - xp));
            //Console.WriteLine("Score: " + score + " | " + "Score Multiplier: " + scoreMultiplier);

            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            UnitTestHealthSystem();
            UnitTestXPSystem();

            ResetGame();

            ShowHUD();

            Console.WriteLine("Player Defeats an Enemy!");
            Console.WriteLine("Player Loses " + smallEnemyDamage + " Health");
            //Console.WriteLine("Player Scores " + smallEnemyPoints + " Points");
            //AddScore(smallEnemyPoints);
            TakeDamage(smallEnemyDamage);
            //scoreMultiplier += smallEnemyScoreMultiplier;
            IncreaseXP(365);

            Console.WriteLine();

            ShowHUD();

            Console.WriteLine("Player Found Some Treasure!");
            //Console.WriteLine("Player Scores " + treasurePoints + " Points");
            //AddScore(treasurePoints);
            IncreaseXP(235);
            Console.WriteLine();

            ShowHUD();

            Console.WriteLine("Player Defeats a Boss!");
            Console.WriteLine("Player Loses " + bossDamage + " Health");
            //Console.WriteLine("Player Scores " + bossPoints + " Points");
            //AddScore(bossPoints);
            TakeDamage(bossDamage);
            //scoreMultiplier += bossScoreMultiplier;

            Console.WriteLine();

            ShowHUD();

            Console.WriteLine("Player is Defeated by an Enemy!");
            Console.WriteLine("Player Loses " + largeEnemyDamage + " Health");
            Console.WriteLine("Player Loses a Life");
            TakeDamage(largeEnemyDamage);

            Console.WriteLine();

            ShowHUD();

            Console.WriteLine("Player Defeats an Enemy!");
            Console.WriteLine("Player Loses " + largeEnemyDamage + " Health");
            //Console.WriteLine("Player Scores " + largeEnemyPoints + " Points");
            TakeDamage(largeEnemyDamage);
            //AddScore(largeEnemyPoints);
            //scoreMultiplier += largeEnemyScoreMultiplier;

            Console.WriteLine();

            ShowHUD();

            Console.WriteLine("Player Finds a One-Up!");
            Console.WriteLine("Player Gains a Life");
            OneUp();

            Console.WriteLine();

            ShowHUD();

            Console.WriteLine("Player Defeats an Enemy!");
            Console.WriteLine("Player Loses " + mediumEnemyDamage + " Health");
            //Console.WriteLine("Player Scores " + mediumEnemyPoints + " Points");
            TakeDamage(mediumEnemyDamage);
            //AddScore(mediumEnemyPoints);
            //scoreMultiplier += mediumEnemyScoreMultiplier;

            Console.WriteLine();

            ShowHUD();

            Console.WriteLine("Player Finds a Health Pack!");
            Console.WriteLine("Player Gains " + healthPack + " Health");
            Heal(healthPack);
            RegenerateShield(healthPack);

            Console.WriteLine();

            ShowHUD();

            Console.WriteLine("Player Encounters the Final Boss!");
            Console.WriteLine("Player Defeats the Final Boss!");
            Console.WriteLine("Player Loses " + finalBossDamage + " Health");
            //Console.WriteLine("Player Scores " + finalBossPoints + " Points");
            TakeDamage(finalBossDamage);
            //AddScore(finalBossPoints);
            //scoreMultiplier += finalBossScoreMultiplier;

            Console.WriteLine();

            ShowHUD();

            Console.WriteLine("You Win!");
            //Console.WriteLine("Final Score: " + score + " Points");

            Console.ReadLine();
        }

        static void UnitTestHealthSystem()
        {
            Debug.WriteLine("Unit testing Health System started...");

            // TakeDamage()

            // TakeDamage() - only shield
            shield = 100;
            health = 100;
            lives = 3;
            TakeDamage(10);
            Debug.Assert(shield == 90);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // TakeDamage() - shield and health
            shield = 10;
            health = 100;
            lives = 3;
            TakeDamage(50);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 60);
            Debug.Assert(lives == 3);

            // TakeDamage() - only health
            shield = 0;
            health = 50;
            lives = 3;
            TakeDamage(10);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 40);
            Debug.Assert(lives == 3);

            // TakeDamage() - health and lives
            shield = 0;
            health = 10;
            lives = 3;
            TakeDamage(25);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 0);
            Debug.Assert(lives == 3);

            // TakeDamage() - shield, health, and lives
            shield = 5;
            health = 100;
            lives = 3;
            TakeDamage(110);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 0);
            Debug.Assert(lives == 3);

            // TakeDamage() - negative input
            shield = 50;
            health = 50;
            lives = 3;
            TakeDamage(-10);
            Debug.Assert(shield == 50);
            Debug.Assert(health == 50);
            Debug.Assert(lives == 3);

            // Heal()

            // Heal() - normal
            shield = 0;
            health = 90;
            lives = 3;
            Heal(5);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 95);
            Debug.Assert(lives == 3);

            // Heal() - already max health
            shield = 90;
            health = 100;
            lives = 3;
            Heal(5);
            Debug.Assert(shield == 90);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // Heal() - negative input
            shield = 50;
            health = 50;
            lives = 3;
            Heal(-10);
            Debug.Assert(shield == 50);
            Debug.Assert(health == 50);
            Debug.Assert(lives == 3);

            // RegenerateShield()

            // RegenerateShield() - normal
            shield = 50;
            health = 100;
            lives = 3;
            RegenerateShield(10);
            Debug.Assert(shield == 60);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // RegenerateShield() - already max shield
            shield = 100;
            health = 100;
            lives = 3;
            RegenerateShield(10);
            Debug.Assert(shield == 100);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // RegenerateShield() - negative input
            shield = 50;
            health = 50;
            lives = 3;
            RegenerateShield(-10);
            Debug.Assert(shield == 50);
            Debug.Assert(health == 50);
            Debug.Assert(lives == 3);

            // Revive()

            // Revive()
            shield = 0;
            health = 0;
            lives = 2;
            Revive();
            Debug.Assert(shield == 100);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 1);

            Debug.WriteLine("Unit testing Health System completed.");
            Console.Clear();
        }

        static void UnitTestXPSystem()
        {
            Debug.WriteLine("Unit testing XP / Level Up System started...");

            // IncreaseXP()

            // IncreaseXP() - no level up; remain at level 1
            xp = 0;
            level = 1;
            IncreaseXP(10);
            Debug.Assert(xp == 10);
            Debug.Assert(level == 1);

            // IncreaseXP() - level up to level 2 (costs 100 xp)
            xp = 0;
            level = 1;
            IncreaseXP(105);
            Debug.Assert(xp == 5);
            Debug.Assert(level == 2);

            // IncreaseXP() - level up to level 3 (costs 200 xp)
            xp = 0;
            level = 2;
            IncreaseXP(210);
            Debug.Assert(xp == 10);
            Debug.Assert(level == 3);

            // IncreaseXP() - level up to level 4 (costs 300 xp)
            xp = 0;
            level = 3;
            IncreaseXP(315);
            Debug.Assert(xp == 15);
            Debug.Assert(level == 4);

            // IncreaseXP() - level up to level 5 (costs 400 xp)
            xp = 0;
            level = 4;
            IncreaseXP(499);
            Debug.Assert(xp == 99);
            Debug.Assert(level == 5);

            Debug.WriteLine("Unit testing XP / Level Up System completed.");
            Console.Clear();
        }
    }
}