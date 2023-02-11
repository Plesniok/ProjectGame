using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneButtonGame
{
    public partial class Form1 : Form
    {
        
        private string currentPage = "initPlayer";
        private Player currentPlayer = new Player();
        private Enemy enemy = new Enemy();
        private List<PictureBox> playerHearts = new List<PictureBox>();
        private List<PictureBox> enemyHearts = new List<PictureBox>();
        private List<Label> attackTexts = new List<Label>() {};
        private List<Button> attackButtons = new List<Button>() { };



        public Form1()
        {
            InitializeComponent();
        }

        //player init button click
        private void button1_Click(object sender, EventArgs e)
        {
            
            this.currentPlayer.initPlayer(textBox2.Text);
            initPlayerOff();
            chooseAttacksOn();
            this.currentPage = "initPlayerClass";

        }
        //Controll key down
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)

            {
                FireClass.PerformClick();
                WaterClass.PerformClick();
                AttackButton1.PerformClick();
                AttackButton2.PerformClick();
            }
        }
        //player class buttons START
        private void FireClass_Click(object sender, EventArgs e)
        {
            Attack fireBall = new Attack(1, "Fire Ball", "fire", 0);
            Attack fireFall = new Attack(2, "Fire Fall", "fire", 2);
            this.enemy.randomInit(); 
            this.currentPlayer.addAttack(fireBall);
            this.currentPlayer.addAttack(fireFall);
            this.currentPlayer.initPlayerClass("fire");
            PlayerImage.Image = Properties.Resources.FireMonster1;
            EnemyImage.Image = Properties.Resources.FireMonster1;
            chooseAttacksOff();
            fightViewOn();
        }

        private void WaterClass_Click(object sender, EventArgs e)
        {
            Attack waterBall= new Attack(1, "Water Ball", "water", 0);
            Attack shark= new Attack(3, "Shark", "water", 3);
            this.enemy.randomInit();
            this.currentPlayer.addAttack(waterBall);
            this.currentPlayer.addAttack(shark);
            this.currentPlayer.initPlayerClass("water");
            PlayerImage.Image = Properties.Resources.WaterMonster1;
            EnemyImage.Image = Properties.Resources.WaterMonster1;

            chooseAttacksOff();
            fightViewOn();
        }
        //player class buttons END
        //handleStatus START
        private void startNewRound(bool playerWin)
        {
            for (int i = 0; i < 10; i++)
            {
                this.enemyHearts[i].Visible = true;
                this.playerHearts[i].Visible = true;
                this.currentPlayer.resetPlayerState();
                this.enemy.randomInit();
                switch (this.enemy.playerClass)
                {
                    case "fire":    
                        EnemyImage.Image = Properties.Resources.FireMonster1;
                        break;
                    case "water":
                        EnemyImage.Image = Properties.Resources.WaterMonster1;
                        break;
                }
                
            }
            if(!playerWin)
            {
                this.enemy.addWinCount();
                EnemyWinCount.Text = this.enemy.getWinCount().ToString();
            }
            else
            {
                this.currentPlayer.addWinCount();
                PlayerWinCount.Text = this.currentPlayer.getWinCount().ToString();

            }

            attacksWaitControll();


        }
        
        private void refreshStatus()
        {
            int enemyHealth = this.enemy.getHealth();
            if (enemyHealth > 0)
            {
                for (int i = enemyHealth; i < 10; i++)
                {
                    this.enemyHearts[i].Visible = false;
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    this.enemyHearts[i].Visible = false;
                }
                startNewRound(true);
            }
            int playerHealth = this.currentPlayer.getHealth();
            if (playerHealth > 0)
            {
                for (int i = playerHealth; i < 10; i++)
                {
                    this.playerHearts[i].Visible = false;
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    this.playerHearts[i].Visible = false;
                }
                startNewRound(false);
            }

        }
        //handleStatus END
        //player attack buttons START
        private void AttackButton1_Click(object sender, EventArgs e)
        {
            Attack currentAttack = this.currentPlayer.getAllAttacks()[0];
            Attack currentEnemyAttack = this.enemy.getRandomAvailableAttack();
            int waitValue = currentAttack.getWait();
            
            

            
            attackProcedure(currentAttack, currentEnemyAttack);
        }

        private void AttackButton2_Click(object sender, EventArgs e)
        {
            Attack currentAttack = this.currentPlayer.getAllAttacks()[1];
            Attack currentEnemyAttack = this.enemy.getRandomAvailableAttack();
            int waitValue = currentAttack.getWait();
            
            
            
            attackProcedure(currentAttack, currentEnemyAttack);



        }
        private void attacksWaitControll()
        {
            List<Attack> playerAttacks = this.currentPlayer.getAllAttacks();
            for (int i = 0; i < playerAttacks.Count(); i++)
            {
                playerAttacks[i].waitMinus();
                int waitValue = playerAttacks[i].getWait();
                this.attackTexts[i].Text = "Wait for " + waitValue.ToString() + "rounds";
                if (waitValue > 0)
                {
                    this.attackButtons[i].Enabled = false;
                }
                else
                {
                    this.attackButtons[i].Enabled = true;
                }
            }

            List<Attack> enemyAttacks = this.enemy.getAllAttacks();
            for (int i = 0; i < enemyAttacks.Count(); i++)
            {
                enemyAttacks[i].waitMinus();
                
            }
        }

        private void changeAttackAnimation(Attack attack)
        {
            switch (attack.getAttackName()){
                case "Fire Ball":
                    AttackAnimation.Image = Properties.Resources.fireball;
                    break;
                case "Fire Fall":
                    AttackAnimation.Image = Properties.Resources.fireAttack1;
                    break;
                case "Shark":
                    AttackAnimation.Image = Properties.Resources.SharkAttack1;
                    break;
                case "Water Ball":
                    AttackAnimation.Image = Properties.Resources.waterBall1;
                    break;
                default:
                    break;
            }
        }

        async private void attackProcedure(Attack playerAttack, Attack enemyAttack)
        {
            if(playerAttack.getWait() == 0)
            {
                foreach(Button attackButton in this.attackButtons)
                {
                    attackButton.Enabled = false;
                }
                this.enemy.giveDemage(playerAttack.getDemageForUse());
                playerAttack.addWait();
                changeAttackAnimation(playerAttack);
                await Task.Delay(1000);
                AttackAnimation.Visible = true;
                await Task.Delay(1000);
                AttackAnimation.Visible = false;
                refreshStatus();
                changeAttackAnimation(enemyAttack);
                this.currentPlayer.giveDemage(enemyAttack.getDemageForUse());
                enemyAttack.addWait();
                await Task.Delay(1000);
                AttackAnimation.Visible = true;
                await Task.Delay(1000);
                AttackAnimation.Visible = false;
                refreshStatus();
                foreach (Button attackButton in this.attackButtons)
                {
                    attackButton.Enabled = true;
                }
                attacksWaitControll();
            }

            
        }
        //player attack buttons END
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void initPlayerOff()
        {
            PlayerNameLabel.Visible = false;
            textBox2.Visible = false;
            StartGameButton.Visible = false;
        }
        private void initPlayerOn()
        {
            PlayerNameLabel.Visible = true;
            textBox2.Visible = true;
            StartGameButton.Visible = true;
        }
        private void chooseAttacksOn()
        {
            WaterClass.Visible = true;
            FireClass.Visible = true;
            ChooseAttackLabel.Visible = true;
            FireClassDescription.Visible = true;
            WaterClassDescription.Visible = true;
            UserTutorialText.Visible = true;

        }
        private void chooseAttacksOff()
        {
            WaterClass.Visible = false;
            FireClass.Visible = false;
            ChooseAttackLabel.Visible = false;
            FireClassDescription.Visible = false;
            WaterClassDescription.Visible = false;
            UserTutorialText.Visible = false;
        }

        private void fightViewOn()
        {
            List<Attack> playerAttacks = this.currentPlayer.getAllAttacks();
            AttackButton1.Text = playerAttacks[0].getAttackName();
            AttackButton2.Text = playerAttacks[1].getAttackName();
            this.playerHearts.Add(Hearth1);
            this.playerHearts.Add(Hearth2);
            this.playerHearts.Add(Hearth3);
            this.playerHearts.Add(Hearth4);
            this.playerHearts.Add(Hearth5);
            this.playerHearts.Add(Hearth6);
            this.playerHearts.Add(Hearth7);
            this.playerHearts.Add(Hearth8);
            this.playerHearts.Add(Hearth9);
            this.playerHearts.Add(Hearth10);
            this.enemyHearts.Add(enemyHearth1);
            this.enemyHearts.Add(enemyHearth2);
            this.enemyHearts.Add(enemyHearth3);
            this.enemyHearts.Add(enemyHearth4);
            this.enemyHearts.Add(enemyHearth5);
            this.enemyHearts.Add(enemyHearth6);
            this.enemyHearts.Add(enemyHearth7);
            this.enemyHearts.Add(enemyHearth8);
            this.enemyHearts.Add(enemyHearth9);
            this.enemyHearts.Add(enemyHearth10);
            this.attackTexts.Add(AttackText1);
            this.attackTexts.Add(AttackText2);
            this.attackButtons.Add(AttackButton1);
            this.attackButtons.Add(AttackButton2);
            foreach (PictureBox heart in this.enemyHearts)
            {
                heart.Visible = true;
            }

            foreach (PictureBox heart in this.playerHearts)
            {
                heart.Visible = true;
            }

            for (int i = 0; i < playerAttacks.Count(); i++)
            {
                this.attackTexts[i].Text = "Wait for " + playerAttacks[i].getWait().ToString() + "rounds";
            }
            EnemyImage.Visible = true;
            AttackButton1.Visible = true;
            AttackButton2.Visible = true;
            PlayerImage.Visible = true;
            PlayerWinCount.Visible = true;
            EnemyWinCount.Visible = true;
            AttackText1.Visible = true;
            AttackText2.Visible = true;
            AttackButton1.Focus();
        }
        private void fightViewOff()
        {
            foreach (PictureBox heart in this.playerHearts)
            {
                heart.Visible = false;
            }
            foreach (PictureBox heart in this.enemyHearts)
            {
                heart.Visible = false;
            }
            AttackButton1.Visible = false;
            AttackButton2.Visible = false;
            PlayerImage.Visible = false;
            PlayerWinCount.Visible = true;
            EnemyWinCount.Visible = true;
            AttackText1.Visible = true;
            AttackText2.Visible = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void AttackAnimation_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void RestartGameLabel_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }
    }
}
