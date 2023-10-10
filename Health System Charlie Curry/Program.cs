using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace Health_System_Charlie_Curry
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Variables
            float smallEnemyScoreMultiplier = 0.1f;
            float mediumEnemyScoreMultiplier = 0.2f;
            float largeEnemyScoreMultiplier = 0.3f;
            float bossScoreMultiplier = 0.5f;
            float finalBossScoreMultiplier = 0.6f;
            float deathScoreMultiplier = 0.2f;
            int smallEnemyDamage = 10;
            int mediumEnemyDamage = 25;
            int largeEnemyDamage = 40;
            int bossDamage = 50;
            int finalBossDamage = 60;
            float treasurePoints = 250f;
            float smallEnemyPoints = 100f;
            float mediumEnemyPoints = 200f;
            float largeEnemyPoints = 300f;
            float bossPoints = 1000f;
            float finalBossPoints = 2000f;
            int healthPack = 30;
            string title = "Super Awesome Adventure Video Game";
            float score;
            //string realName;
            //string gamerTag;
            int health;
            int maxHealth = 100;
            int lives;
            int oneUp = 1;
            float scoreMultiplier;
            int shield;
            int maxShield = 100;
            int xp;
            int level;
            int xpThreshhold;
            #endregion

            void IncreaseXP(int experience)
            {
                if (experience + xp > xpThreshhold + 100 && experience + xp < xpThreshhold + 200)
                {
                    level += 2;
                    xp += experience;
                    xpThreshhold += 200;
                }
                else if (xp + experience > xpThreshhold && xp + experience < xpThreshhold + 100)
                {
                    level++;
                    xp += experience;
                    xpThreshhold += 100;
                }
                else if (experience + xp > xpThreshhold)
                {
                    level++;
                    xp += experience;
                    xpThreshhold += 100;
                }
                else
                {
                    xp += experience;
                }
            }

            void ResetGame()
            {
                //Console.WriteLine("Enter Your Name:");
                //realName = Console.ReadLine().Trim();
                //Console.WriteLine("Enter Your GamerTag:");
                //gamerTag = Console.ReadLine().Trim();
                health = maxHealth;
                shield = maxShield;
                score = 0f;
                lives = 3;
                xp = 0;
                level = 1;
                xpThreshhold = 100;
                scoreMultiplier = 0f;
                Console.WriteLine("New Game");
            }

            string HealthStatus(int healthVar)
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

            void TakeDamage(int damage)
            {
                if (damage < 0)
                {
                    Console.WriteLine("Error: Player Cannot Take " + damage + " Damage");
                }
                else if (health - damage <= 0)
                {
                    health = maxHealth;
                    shield = maxShield;
                    lives--;
                    scoreMultiplier -= deathScoreMultiplier;
                }
                else if (shield - damage <= 0)
                {
                    health -= (damage - shield);
                    shield = 0;
                }
                else if (shield > 0)
                {
                    shield -= damage;
                }
                else if (health > 0)
                {
                    health -= damage;
                }

            }

            void Heal(int hp)
            {
                if (hp < 0)
                {
                    Console.WriteLine("Error: Player Cannot Heal " + hp + " HP");
                }
                else if (health + hp > 100)
                {
                    health = maxHealth;
                }
                else
                {
                    health += hp;
                }

            }

            void RegenerateShield(int hp)
            {
                if (hp < 0)
                {
                    Console.WriteLine("Error: Player Cannot Regenerate " + hp + " Shield");
                }
                else if (shield + hp > 100)
                {
                    shield = maxShield;
                }
                else
                {
                    shield += hp;
                }
            }

            void AddScore(float pointsEarned)
            {
                score += pointsEarned * scoreMultiplier;
            }

            void ShowHUD()
            {
                Console.WriteLine(title);
                Console.WriteLine("----------");

                //Console.WriteLine(realName + " AKA " + gamerTag);
                Console.WriteLine("Health: " + health + "%" + " | " + "Shield: " + shield + "%");
                Console.WriteLine("Lives: " + lives);
                Console.WriteLine(HealthStatus(health));
                Console.WriteLine("XP: " + xp + " | " + "Level: " + level + " | " + "XP to Next Level: " + (xpThreshhold - xp));
                Console.WriteLine("Score: " + score + " | " + "Score Multiplier: " + scoreMultiplier);

                Console.WriteLine();
            }

            ResetGame();

            ShowHUD();

            Console.WriteLine("Player Defeats an Enemy!");
            Console.WriteLine("Player Loses " + smallEnemyDamage + " Health");
            Console.WriteLine("Player Scores " + smallEnemyPoints + " Points");
            AddScore(smallEnemyPoints);
            TakeDamage(smallEnemyDamage);
            scoreMultiplier += smallEnemyScoreMultiplier;
            IncreaseXP(300);

            Console.WriteLine();

            ShowHUD();

            Console.WriteLine("Player Found Some Treasure!");
            Console.WriteLine("Player Scores " + treasurePoints + " Points");
            AddScore(treasurePoints);

            Console.WriteLine();

            ShowHUD();

            Console.WriteLine("Player Defeats a Boss!");
            Console.WriteLine("Player Loses " + bossDamage + " Health");
            Console.WriteLine("Player Scores " + bossPoints + " Points");
            AddScore(bossPoints);
            TakeDamage(bossDamage);
            scoreMultiplier += bossScoreMultiplier;

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
            Console.WriteLine("Player Scores " + largeEnemyPoints + " Points");
            TakeDamage(largeEnemyDamage);
            AddScore(largeEnemyPoints);
            scoreMultiplier += largeEnemyScoreMultiplier;

            Console.WriteLine();

            ShowHUD();

            Console.WriteLine("Player Finds a One-Up!");
            Console.WriteLine("Player Gains a Life");
            lives += oneUp;

            Console.WriteLine();

            ShowHUD();

            Console.WriteLine("Player Defeats an Enemy!");
            Console.WriteLine("Player Loses " + mediumEnemyDamage + " Health");
            Console.WriteLine("Player Scores " + mediumEnemyPoints + " Points");
            TakeDamage(mediumEnemyDamage);
            AddScore(mediumEnemyPoints);
            scoreMultiplier += mediumEnemyScoreMultiplier;

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
            Console.WriteLine("Player Scores " + finalBossPoints + " Points");
            TakeDamage(finalBossDamage);
            AddScore(finalBossPoints);
            scoreMultiplier += finalBossScoreMultiplier;

            Console.WriteLine();

            ShowHUD();

            Console.WriteLine("You Win!");
            Console.WriteLine("Final Score: " + score + " Points");

            Console.ReadLine();
        }
    }
}
