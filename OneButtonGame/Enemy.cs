using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneButtonGame
{
    class Enemy : Player
    {
        //private string playerName = "";
        //private List<Attack> playerAttacks = new List<Attack>();
        //private string playerClass = "";
        //private int health = 10;
        public void randomInit()
        {
            this.playerAttacks = new List<Attack>();
            Attack fireBall = new Attack(1, "Fire Ball", "fire", 0);
            Attack fireFall = new Attack(2, "Fire Fall", "fire", 2);
            Attack waterBall = new Attack(1, "Water Ball", "water", 0);
            Attack shark = new Attack(3, "Shark", "water", 3);
            
            List<string> classes = new List<string>() { "fire", "water"};
            int randomIndex = new Random().Next(0, classes.Count);
            string randomClass = classes[randomIndex];
            
            this.health = 10;
            this.playerClass = randomClass;
            switch (randomClass)
            {
                case "fire":
                    this.addAttack(fireBall);
                    this.addAttack(fireFall);
                    break;
                default:
                    this.addAttack(waterBall);
                    this.addAttack(shark);
                    break;
            }

        }
        public Attack getRandomAvailableAttack()
        {

            List<Attack> attacks = this.getAllAttacks();
            List<Attack> availableAttacks = new List<Attack>();
            foreach(Attack attack in attacks)
            {
                if(attack.getWait() == 0)
                {
                    availableAttacks.Add(attack);
                }
                
            }

            int randomIndex = new Random().Next(0, availableAttacks.Count);
            Attack randomAttack= availableAttacks[randomIndex];
            return randomAttack;


        }
    }
}
