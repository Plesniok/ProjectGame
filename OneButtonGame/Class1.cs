using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OneButtonGame
{
    class Player
    {
        public string playerName = "";
        public List<Attack> playerAttacks = new List<Attack>();
        public string playerClass = "";
        public int health = 10;
        public int winCount = 0;

        public void initPlayer(string newPlayerName)
        {
            this.playerName = newPlayerName;
        }
        public string getPlayerClass()
        {
            return this.playerClass;
        }

        public void addAttack(Attack newAttack)
        {
            this.playerAttacks.Add(newAttack);
        }

        public void initPlayerClass(string newPlayerClass)
        {
            this.playerClass = newPlayerClass;
        }
        public List<Attack> getAllAttacks()
        {
            
            List<Attack> availableAttacks = new List<Attack>();
            foreach (Attack attack in this.playerAttacks)
            {
                if (attack.getWait() == 0)
                {
                    availableAttacks.Add(attack);
                }
            }
            return this.playerAttacks;
        }
        public int getHealth()
        {
            return this.health;
        }

        public void giveDemage(int demage)
        {
            this.health -= demage;
        }

        public void resetPlayerState()
        {
            foreach(Attack attack in playerAttacks)
            {
                attack.waitReset();
            }
            this.health = 10;
        }
        public int getWinCount()
        {
            return this.winCount;
        }
        public void addWinCount()
        {
            this.winCount += 1;
        }
    }
}
