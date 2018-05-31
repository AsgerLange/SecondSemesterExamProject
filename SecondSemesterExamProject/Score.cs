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
using System.IO;

namespace TankGame
{
    class Score
    {
        //Fields
        public static string name = string.Empty;//Contains the string we need to use for player input
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

            if (!(File.Exists(@"TankGameDatabase.db")))
            {
                SQLiteConnection.CreateFile("TankGameDatabase.db");
            }
        }


        /// <summary>
        /// writes a command to the db
        /// </summary>
        /// <param name="command"></param>
        public void WriteToDB(string command)
        {
            SQLiteConnection DBConnect = new SQLiteConnection("Data source = TankGameDatabase.db; Version = 3; ");
            DBConnect.Open();
            SQLiteCommand Command = new SQLiteCommand(command, DBConnect);
            Command.ExecuteNonQuery();
            DBConnect.Close();
        }

        public void CreateTables()
        {

            string highscore = "Create table Highscores (ID varchar, Placing int, Name string, Score int)";
            string totalStats = "Create table Total stats (ID varchar,Total bullets fired int, Total tower build int, Total tower dead int, Total tower kills int,Total player kills int, Total enemy dead int )";
            string tower = "Create table Tower (ID varchar, Tower name string, Tower kills int, Tower Build int, Tower Dead int)";
            string player = "Create table Player (ID varchar,Basic bullets shot int,Bigger bullets shot int,Sniper bullets shot int,Shotgun bullets shot int, Gold int, Wave int)";
            string enemies = "Create table Enemies (ID varchar, Enemy name string, Enemy kills int, Spitter bullets shot";

            WriteToDB(highscore);
            WriteToDB(totalStats);
            WriteToDB(tower);
            WriteToDB(player);
            WriteToDB(enemies);
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
            SQLiteConnection dbConnect = new SQLiteConnection("Data source=TankGameDatabase.db;Version=3;");
            dbConnect.Open();
         //   string insert = "insert into Higscores (name, score) values (" + name + "," + Stats.TotalAmountOfGold + ")";
     //       SQLiteCommand command = new SQLiteCommand(insert, dbConnect);
            //command.ExecuteNonQuery();
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

            WriteToDB(basicEnemy);
            WriteToDB(basicEliteEnemy);
            WriteToDB(spitterEnemy);
            WriteToDB(player);
            WriteToDB(player2);
            WriteToDB(basicTower);
            WriteToDB(shotgunTower);
            WriteToDB(sniperTower);
            WriteToDB(machinegunTower);
            WriteToDB(totalData);

        }


        public void UpdateData()
        {


            if (GameWorld.Instance.GetGameState == GameState.GameOver)
            {
                string updateDeadEnemies = "Update Enemies set Enemy kills =Enemy kills "+" " + Stats.BasicEnemyKilled + "where Name = Basic enemy";
                WriteToDB(updateDeadEnemies);
                string updateBasicEliteEnemy = "Update Enemies set Enemy kills =Enemy kills "+" " + Stats.BasicEliteEnemyKilled + "where Name =Basic elite enemy";
                WriteToDB(updateBasicEliteEnemy);
                string updateSpitterBulletCounter = "Update Enemies set Spitter bullets shot = Spitter bullets shot "+" " + Stats.BasicBulletCounter + "where ID = 3";
                WriteToDB(updateSpitterBulletCounter);
                string totalWaves = "Update Player set Wave = " + GameWorld.Instance.GetSpawn.Wave + "where ID = 1";
                WriteToDB(totalWaves);
                string totalGold = "Update Player set Gold = " + Stats.TotalAmountOfGold + "where ID = 1";
                WriteToDB(totalGold);
                string updateBasicBulletCounter = "Update Player set Basic bullets shot = Basic bullets shot "+" " + Stats.BasicBulletCounter + "where ID = 1";
                WriteToDB(updateBasicBulletCounter);
                string updateBiggerBulletCounter = "Update Player set Bigger bullets shot = Basic bullets shot "+" " + Stats.BiggerBulletCounter + "where ID = 1";
                WriteToDB(updateBasicBulletCounter);
                string updateSniperBulletCounter = "Update Player set Sniper bullets shot = Basic bullets shot "+" " + Stats.SniperBulletCounter + "where ID = 1";
                WriteToDB(updateSniperBulletCounter);
                string updateShotgunBulletCounter = "Update Player set Shotgun bullets shot = Basic bullets shot "+" " + Stats.ShotgunPelletsCounter + "where ID = 1";
                WriteToDB(updateShotgunBulletCounter);
                if (GameWorld.Instance.GetMenu.PlayerAmount > 1)
                {
                    string totalGoldPlayer2 = "Update Player set Gold " + Stats.TotalAmountOfGold + " where ID = 2";
                    WriteToDB(totalGoldPlayer2);
                    string totalWaves2 = "Update Player set Wave = " + GameWorld.Instance.GetSpawn.Wave + "where ID = 2";
                    WriteToDB(totalWaves2);
                    string updateBasicBulletCounter2 = "Update Player set Basic bullets shot = Basic bullets shot "+" " + Stats.BasicBulletCounter + "where ID = 2";
                    WriteToDB(updateBasicBulletCounter2);
                    string updateBiggerBulletCounter2 = "Update Player set Bigger bullets shot = Basic bullets shot "+" " + Stats.BiggerBulletCounter + "where ID = 2";
                    WriteToDB(updateBiggerBulletCounter2);
                    string updateSniperBulletCounter2 = "Update Player set Sniper bullets shot = Basic bullets shot "+" " + Stats.SniperBulletCounter + "where ID = 2";
                    WriteToDB(updateSniperBulletCounter2);
                    string updateShotgunBulletCounter2 = "Update Player set Shotgun bullets shot = Basic bullets shot "+" " + Stats.ShotgunPelletsCounter + "where ID = 2";
                    WriteToDB(updateShotgunBulletCounter2);
                }

                string totalEnemyDead = "Update Total stats set Total enemy dead = select sum (Enemy kills) from Enemies "+" Total enemy dead where ID = 1";
                WriteToDB(totalEnemyDead);
                string totalBulletsFired = "Update Total stats set Total bullets fired = select sum(Basic bullets shot, Bigger bullets shot, Sniper bullets shot, Shotgun bullets shot) from Player "+" Total bullets fired where ID = 1";
                WriteToDB(totalBulletsFired);
                string totalTowerKills = "Update Total stats set Total tower kills = select sum(Tower kills) from Tower "+" Total tower kills where ID = 1";
                WriteToDB(totalTowerKills);
                string totalTowerDead = "Update Total stats set Total tower dead = select sum(Tower dead) from Tower "+" Total tower dead where ID = 1";
                WriteToDB(totalTowerDead);
                string totalTowerBuild = "Update Total stats set Total tower build = select sum (Tower build) from Tower "+" Total tower build where ID = 1";
                WriteToDB(totalTowerBuild);
            }
        }

        public void LoadScoreToScreen()
        {
            string highscore = "select Highscore.Name, Highscore.Score from Highscore limit 10 order by score desc";
            SQLiteCommand command = new SQLiteCommand(highscore);
            SQLiteDataReader highscoreReader = command.ExecuteReader();
            while (highscoreReader.Read())
            {
                
            }
        }
    }
}
