using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneButtonGame
{
    class Attack
    {
        private int demage;
        private string name;
        private string type;
        private int duration;
        private int waitFor;
        public Attack(int demage, string name, string type, int duration)
        {
            this.demage = demage;
            this.name = name;
            this.type = type;
            this.duration = duration;
        }

        public string getAttackName()
        {
            return this.name;
        }

        public void minusWaitValue()
        {
            if(this.waitFor > 0)
            {
                this.waitFor -= 1;
            }
            
        }
        public void addWait()
        {
            this.waitFor += 1;
        }

        public int getWait()
        {
            return this.waitFor;
        }
        public void waitReset()
        {
            this.waitFor = 0;
        }

        public void waitMinus()
        {
            if(this.waitFor > 0)
            {
                this.waitFor -= 1;
            } 
        }
        public int getDemage()
        {
            return this.demage;

        }

        public int getDemageForUse()
        {
            this.waitFor += this.duration;
            return this.demage;
        }
    }
}
