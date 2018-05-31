using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Data.SQLite;

namespace TankGame
{
    class Score
    {
        //Fields
        public static string name = string.Empty;//Contains the string we need to use for player input
        private bool databseState = true;
        Rectangle textBox;
        Texture2D theBox;
        SpriteFont font;
        private KeyboardState lastKeyboardState;//Checks the last key pressed
        private Keys[] lastKey;//Contains a array of the keys that has been pressed.
        private string parsedText;
        private double timer;



        public Score()
        {
            textBox = new Rectangle(20, 20, 150, 50);


        }

        public void SetupServer()
        {

            if (databseState == true)
            {
                SQLiteConnection.CreateFile("TankGameDatabase.db");
                databseState = false;
            }

        }
        public void CreateTables()
        {
            SQLiteConnection dbConnect = new SQLiteConnection("Data source=data.db;Version=3;");
            dbConnect.Open();
            string highscore = "Create table Highscores (ID varchar, Placing int, Name string, Score int)";
            string totalStats = "Create table Total stats (ID varchar,Total bullets fired int, Total tower build int, Total tower dead int, Total tower kills int,Total player kills int, Total enemy dead int )";
            string tower = "Create table Tower (ID varchar, Tower name string, Tower kills int, Tower Build int, Tower Dead int)";
            string player = "Create table Player (ID varchar,Basic bullets shot int,Bigger bullets shot int,Sniper bullets shot int,Shotgun bullets shot int, Gold int, Wave int)";
            string enemies = "Create table Enemies (ID varchar, Enemy name string, Enemy kills int, Spitter bullets shot";
            SQLiteCommand command = new SQLiteCommand(highscore, dbConnect);
            SQLiteCommand command2 = new SQLiteCommand(totalStats, dbConnect);
            SQLiteCommand command3 = new SQLiteCommand(tower, dbConnect);
            SQLiteCommand command4 = new SQLiteCommand(player, dbConnect);
            SQLiteCommand command5 = new SQLiteCommand(enemies, dbConnect);
            command.ExecuteNonQuery();
            command2.ExecuteNonQuery();
            command3.ExecuteNonQuery();
            command4.ExecuteNonQuery();
            command5.ExecuteNonQuery();
            dbConnect.Close();
        }

        /// <summary>
        /// Updates the pressed keys for score
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();//Gets the state
            Keys[] keys = keyboardState.GetPressedKeys();//Gets a array of what keys had been pressed

            foreach (Keys currentKeys in keys)//foreach key there is do something
            {
                if (currentKeys != Keys.None)//checks if the current is not none.
                {
                    if (lastKey.Contains(currentKeys))//If the lastkey containst the same value as the current key pressed.
                    {
                        if ((gameTime.TotalGameTime.TotalMilliseconds - timer > 200))//Makes sure that if we press the same key again that it will set a delay
                            HandleKey(gameTime, currentKeys);
                    }
                    else if (!lastKey.Contains(currentKeys))//If the next key is not the same then just write it out
                        HandleKey(gameTime, currentKeys);
                }

            }
            lastKeyboardState = keyboardState;//Sets the lastkeystate to be the keyboard state we get
            lastKey = keys;//Saves the last key that was pressed.

        }
        /// <summary>
        /// Handles spesific keys such as space, backspace, delete and enter.
        /// </summary>
        public void HandleKey(GameTime gameTime, Keys currentKey)
        {
            string keyString = currentKey.ToString();//Turns the currentkeys into a string
            if (currentKey == Keys.Space)
                name += " ";
            else if ((currentKey == Keys.OemPeriod || currentKey == Keys.Delete) && name.Length > 0)
                name = name.Remove(name.Length - 1);
            else if (currentKey == Keys.OemComma)
                InsertScore();
            else
                name += keyString;
            //Set the timer to the current time
            timer = gameTime.TotalGameTime.TotalMilliseconds;
        }

        /// <summary>
        /// Loads the content for inputspace
        /// </summary>
        public virtual void LoadContent(ContentManager contentManager)
        {
            theBox = contentManager.Load<Texture2D>("Button");
            font = contentManager.Load<SpriteFont>("Stat");

            parsedText = ParseText(name);
        }
        /// <summary>
        /// Draws the inputspace and the string that goes with it
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(theBox, textBox, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.5f);//Draws the box
            spriteBatch.DrawString(font, ParseText(name), new Vector2(textBox.X, textBox.Y), Color.Black);//Draws the text
        }
        /// <summary>
        /// Helps to split up text if the text lenght is bigger than the box lenght
        /// </summary>
        private string ParseText(string text)
        {
            string line = String.Empty;
            string returnString = String.Empty;
            string[] wordArray = text.Split(' ');//Splits up the array with space.

            foreach (string word in wordArray)
            {
                if (font.MeasureString(line + word).Length() > textBox.Width)//Checks if the line is longer than the box it self
                {
                    returnString = returnString + line + '\n';//Sets the new line
                    line = string.Empty;
                }
                line = line + word + ' ';
            }
            return returnString + line;
        }

        public void InsertScore()
        {
            SQLiteConnection dbConnect = new SQLiteConnection("Data source=data.db;Version=3;");
            dbConnect.Open();
            string insert = "insert into Higscores (name, score) values (null, " + name + "," + Stats.TotalAmountOfGold + ")";
            SQLiteCommand command = new SQLiteCommand(insert, dbConnect);
            command.ExecuteNonQuery();
            dbConnect.Close();
        }
        public void InsertThings()
        {
            string basicEnemy = "insert into Enemies (ID, Enemy name, Enemy kills,) values (null,Basic enemy,0)";
            string basicEliteEnemy = "insert into Enemies (ID, Enemy name, Enemy kills) values (null,Basic elite enemyy,0)";
            string spitterEnemy = "insert into Enemies (ID, Enemy name, Enemy kills, Spitter bullets shot) values (null,Spitter enemy, 0, 0)";
            string player = "insert into Player (ID, Bullets shot, Gold, Wave) values (null,0,0,0,0,100,0)";
            string player2 = "insert into Player (ID, Bullets shot, Gold, Wave) values (null,0,0,0,0,100,0)";
            string basicTower = "insert into Tower (ID, Tower name, Tower kills, Tower build, Tower dead) values (null,Basic tower,0,0,0)";
            string shotgunTower = "insert into Tower (ID, Tower name, Tower kills, Tower build, Tower dead) values (null,Shotgun tower,0,0,0)";
            string sniperTower = "insert into Tower (ID, Tower name, Tower kills, Tower build, Tower dead) values (null,Sniper tower,0,0,0)";
            string machinegunTower = "insert into Tower (ID, Tower name, Tower kills, Tower build, Tower dead) values1 values (null,Machinegun tower,0,0,0)";
            string totalData = "insert into Total stats (ID, Tower name, Total tower build, Total tower dead, Total tower kills, Total player kills, Total enemy dead) values(null,0,0,0,0,0)";

            SQLiteConnection insertConnection = new SQLiteConnection("Data source = data.db; Version = 3; ");
            insertConnection.Open();
            SQLiteCommand Enemy = new SQLiteCommand(basicEnemy, insertConnection);
            SQLiteCommand BasicEliteEnemy = new SQLiteCommand(basicEliteEnemy, insertConnection);
            SQLiteCommand SpitterEnemy = new SQLiteCommand(spitterEnemy, insertConnection);
            SQLiteCommand Player = new SQLiteCommand(player, insertConnection);
            SQLiteCommand Player2 = new SQLiteCommand(player2, insertConnection);
            SQLiteCommand BasicTower = new SQLiteCommand(basicTower, insertConnection);
            SQLiteCommand ShotgunTower = new SQLiteCommand(shotgunTower, insertConnection);
            SQLiteCommand SniperTower = new SQLiteCommand(sniperTower, insertConnection);
            SQLiteCommand MachinegunTower = new SQLiteCommand(machinegunTower, insertConnection);
            SQLiteCommand TotalData = new SQLiteCommand(totalData, insertConnection);
            Enemy.ExecuteNonQuery();
            BasicEliteEnemy.ExecuteNonQuery();
            SpitterEnemy.ExecuteNonQuery();
            Player.ExecuteNonQuery();
            Player2.ExecuteNonQuery();
            BasicTower.ExecuteNonQuery();
            ShotgunTower.ExecuteNonQuery();
            SniperTower.ExecuteNonQuery();
            MachinegunTower.ExecuteNonQuery();
            TotalData.ExecuteNonQuery();

            insertConnection.Close();
        }

        //public void UpdateData()
        //{
        //    SQLiteConnection updateTables = new SQLiteConnection("Data source = data.db; Version = 3; ");
        //    updateTables.Open();
        //    if (true)
        //    {
        //        string updateDeadEnemies = "Update Enemy set Enemy kills =Enemy kills + " + Stats.BasicEnemyKilled + "where Name = Basic enemy";

        ////        string updateBasicEliteEnemy = "Update Enemy set Enemy kills =Enemy kills + " + Stats.BasicEliteEnemyKilled + "where Name =Basic elite enemy";

        ////        string updateSpitterBulletCounter = "Update Enemies set Spitter bullets shot = Spitter bullets shot + " + Stats.BasicBulletCounter + "where ID = 3";

        //        string totalWaves = "Update Player set Wave = " + GameWorld.Instance.GetSpawn.Wave + "where ID = 1";

        //        string totalGold = "Update Player set Gold = " + Stats.TotalAmountOfGold + "where ID = 1";

        //        string updateBasicBulletCounter = "Update Player set Basic bullets shot = Basic bullets shot + " + Stats.BasicBulletCounter + "where ID = 1";

        //        string updateBiggerBulletCounter = "Update Player set Bigger bullets shot = Basic bullets shot + " + Stats.BiggerBulletCounter + "where ID = 1";

        ////        string updateSniperBulletCounter = "Update Player set Sniper bullets shot = Basic bullets shot + " + Stats.SniperBulletCounter + "where ID = 1";

        ////        string updateShotgunBulletCounter = "Update Player set Shotgun bullets shot = Basic bullets shot + " + Stats.ShotgunPelletsCounter + "where ID = 1";

        //        if (true)
        //        {
        //            string totalGoldPlayer2 = "Update Player set Gold " + Stats.TotalAmountOfGold + " where ID = 2";
        //            string totalWaves2 = "Update Player set Wave = " + GameWorld.Instance.GetSpawn.Wave + "where ID = 2";
        //            string updateBasicBulletCounter2 = "Update Player set Basic bullets shot = Basic bullets shot + " + Stats.BasicBulletCounter + "where ID = 2";
        //            string updateBiggerBulletCounter2 = "Update Player set Bigger bullets shot = Basic bullets shot + " + Stats.BiggerBulletCounter + "where ID = 2";
        //            string updateSniperBulletCounter2 = "Update Player set Sniper bullets shot = Basic bullets shot + " + Stats.SniperBulletCounter + "where ID = 2";
        //            string updateShotgunBulletCounter2 = "Update Player set Shotgun bullets shot = Basic bullets shot + " + Stats.ShotgunPelletsCounter + "where ID = 2";
        //        }

        ////        string totalPlayerKills = "Update Total stats set Total player kills = ";

        ////        string TotalEnemyDead = "Update Total stats set Total enemy dead = select sum (Enemy kills) from Enemies + Total enemy dead where ID = 1";

        ////        string totalBulletsFired = "Update Total stats set Total bullets fired = select sum(Basic bullets shot, Bigger bullets shot, Sniper bullets shot, Shotgun bullets shot) from Total stats where ID = 1";

        ////        string totalTowerKills = "Update Total stats set Total tower kills = select sum(Tower kills) from Tower + Total tower kills where ID = 1";

        ////        string totalTowerDead = "Update Total stats set Total tower dead = select sum(Tower dead) from Tower + Total tower dead";

        //        string totalTowerBuild = "Update Total stats set Total tower build = select sum (Tower build) from Tower + Total tower build";
        //    }
        //}

        //public void LoadScoreToScreen()
        //{
        //    string highscore = "select Highscore.Placing, Highscore.Name, Highscore.Score from Highscore limit 10 order by score desc";
        //    SQLiteCommand command = new SQLiteCommand(highscore);
        //    SQLiteDataReader highscoreReader = command.ExecuteReader();
        //    while (highscoreReader.Read())
        //    {
        //        //Draw out a highscorelist in the middle of the screen.  
        //    }
        //}
    }
}
