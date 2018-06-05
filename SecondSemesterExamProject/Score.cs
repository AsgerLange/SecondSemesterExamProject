﻿using System;
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
        #region Fields
        public static string name = string.Empty;//string.Empty;//Contains the string we need to use for player input
        private bool scoreSaved = false;
        private bool nameEntered = false;
        Rectangle textBox;
        Texture2D theBox;
        private Texture2D BackGround;
        SpriteFont font;
        private KeyboardState lastKeyboardState;//Checks the last key pressed
        private Keys[] lastKey;//Contains a array of the keys that has been pressed.
        private string parsedText;
        private double timer;
        private List<Highscore> highscores = new List<Highscore>();
        #endregion


        public Score()
        {
            textBox = new Rectangle(Constant.width / 2 - 150, Constant.hight / 2, 300, 50);//the textbox pos

            if (!(File.Exists(@"TankGameDatabase.db")))
            {
                SQLiteConnection.CreateFile("TankGameDatabase.db");
                CreateTables();
            }

            highscores.Add(new Highscore("Stefan", 9999, "BasicEnemy", 10000, 10, 50000, 10, 10, 10, 10, 9999, "BasicTower", 0, 100, 0, 9999, 100, 0, 0, 10000, 0));
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

        /// <summary>
        /// returns a list of strings from DB
        /// </summary>
        /// <param name="command"></param>
        public List<string> ReadFromDB(string command, string returnValue)
        {
            List<string> returnList = new List<string>();
            SQLiteConnection DBConnect = new SQLiteConnection("Data source = TankGameDatabase.db; Version = 3; ");
            DBConnect.Open();
            SQLiteCommand Command = new SQLiteCommand(command, DBConnect);
            SQLiteDataReader highscoreReader = Command.ExecuteReader();
            while (highscoreReader.Read())
            {
                returnList.Add((string)highscoreReader[returnValue]);
            }
            DBConnect.Close();

            return returnList;
        }

        /// <summary>
        /// returns a list of ints from DB
        /// </summary>
        /// <param name="command"></param>
        public List<int> ReadFromDB(string command, string returnValue, int overload)
        {
            List<int> returnList = new List<int>();
            SQLiteConnection DBConnect = new SQLiteConnection("Data source = TankGameDatabase.db; Version = 3; ");
            DBConnect.Open();
            SQLiteCommand Command = new SQLiteCommand(command, DBConnect);
            SQLiteDataReader highscoreReader = Command.ExecuteReader();
            while (highscoreReader.Read())
            {
                returnList.Add((int)highscoreReader[returnValue]);
            }
            DBConnect.Close();

            return returnList;
        }

        /// <summary>
        /// Creates the tables
        /// </summary>
        public void CreateTables()
        {
            string highscore = "Create table Highscores (ID integer primary key, Name varchar, Score int)";
            string totalStats = "Create table Total_stats (ID integer primary key, Total_bullets_fired int, Total_tower_build int, Total_tower_dead int, Total_tower_kills int, Total_player_kills int, Total_enemy_dead int)";
            string tower = "Create table Tower (ID integer, Tower_name varchar primary key, Tower_kills int, Tower_Build int, Tower_Dead int,foreign key(ID) REFERENCES higscores(ID))";
            string player = "Create table Player (ID integer, Gold int, Accuracy int, PlayerID integer, Wave int, foreign key(ID) REFERENCES higscores(ID))";
            string enemies = "Create table Enemies (ID integer, Enemy_name varchar primary key, Enemy_kills int, Spitter_bullets_shot,foreign key(ID) REFERENCES higscores(ID))";
            string bullets = "Create table bullets (ID integer, PlayerID integer, bullets_shot int, bullet_Name, foreign key(PlayerID) REFERENCES player(PlayerID),foreign key(ID) REFERENCES higscores(ID))";

            WriteToDB(highscore);
            WriteToDB(totalStats);
            WriteToDB(tower);
            WriteToDB(player);
            WriteToDB(enemies);
            WriteToDB(bullets);
        }

        /// <summary>
        /// Updates the pressed keys for score
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {
            if (scoreSaved == false && nameEntered == false)
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
                            {
                                HandleKey(gameTime, currentKeys);
                            }
                        }
                        else if (!lastKey.Contains(currentKeys))//If the next key is not the same then just write it out
                        {
                            HandleKey(gameTime, currentKeys);
                        }
                    }
                }
                lastKeyboardState = keyboardState;//Sets the lastkeystate to be the keyboard state we get
                lastKey = keys;//Saves the last key that was pressed.
            }
        }

        /// <summary>
        /// Handles spesific keys such as space, backspace, delete and enter.
        /// </summary>
        public void HandleKey(GameTime gameTime, Keys currentKey)
        {
            string keyString = currentKey.ToString();//Turns the currentkeys into a string
            if (currentKey == Keys.Space)
            {
                name += " ";
            }
            else if ((currentKey == Keys.Back || currentKey == Keys.Delete) && name.Length > 0)
            {
                name = name.Remove(name.Length - 1);
            }
            else if (currentKey == Keys.Enter)
            {
                nameEntered = true;
                CreateHighScore();
            }
            else if (keyString.Length < 21)
            {
                name += keyString;
            }
            //Set the timer to the current time
            timer = gameTime.TotalGameTime.TotalMilliseconds;
        }

        /// <summary>
        /// Loads the content for inputspace
        /// </summary>
        public virtual void LoadContent(ContentManager content)
        {
            theBox = content.Load<Texture2D>("Button");
            font = content.Load<SpriteFont>("Stat");
            BackGround = content.Load<Texture2D>(Constant.menuBackGround);
            foreach (Highscore HS in highscores)
            {
                HS.LoadContent(content);
            }
            parsedText = ParseText(name);
        }

        /// <summary>
        /// Draws the inputspace and the string that goes with it
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackGround, new Rectangle(0, 0, Constant.width, Constant.hight), null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 1);
            if (scoreSaved == false && nameEntered == false)
            {
                spriteBatch.Draw(theBox, new Vector2(textBox.X, textBox.Y - 10), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);//Draws the box
                spriteBatch.DrawString(font, ParseText(name), new Vector2(textBox.X + 5, textBox.Y), Color.Black);//Draws the text
            }
            if (scoreSaved && nameEntered)
            {
                for (int i = 0; i < highscores.Count; i++)
                {
                    highscores[i].Draw(spriteBatch, i);
                }
            }
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

        /// <summary>
        /// Creates a new HighScore
        /// </summary>
        public void CreateHighScore()
        {
            int id = 0;
            SQLiteConnection dbConnect = new SQLiteConnection("Data source=TankGameDatabase.db;Version=3;");
            string insert = "insert into highscores (ID,name, score) values (null, '" + name + "', " + GameWorld.Instance.GetGameOver.Score + ")";
            SQLiteConnection DBConnect = new SQLiteConnection("Data source = TankGameDatabase.db; Version = 3; ");
            DBConnect.Open();
            SQLiteCommand Command = new SQLiteCommand(insert, DBConnect);
            Command.ExecuteNonQuery();

            //get ID of the new Row
            string getID = @"SELECT last_insert_rowid()";
            SQLiteCommand cmd = new SQLiteCommand(getID, DBConnect);
            Int64 LastRowID64 = (Int64)cmd.ExecuteScalar();
            id = (int)LastRowID64;
            DBConnect.Close();

            InsertThings(id);
        }
        /// <summary>
        /// Inserts the rest of the things into the rest of the tables. 
        /// </summary>
        public void InsertThings(int ID)
        {
            if (GameWorld.Instance.GetMenu.P1 != VehicleType.None)
            {
                int gold = 0;
                int accuracy = 0;
                foreach (Vehicle VH in GameWorld.Instance.Vehicles)
                {
                    if (VH.Control == Controls.WASD)
                    {
                        gold = VH.Stats.TotalAmountOfGold;
                        accuracy = VH.Stats.CalculateAccuracy();
                    }
                }
                int wave = GameWorld.Instance.GetSpawn.Wave;
                string player1 = "insert into player (ID,gold, Accuracy,PlayerID,wave) values (" + ID + "," + gold + "," + accuracy + ",1," + wave + ");";
                WriteToDB(player1);
                WriteBullets(ID, 1);
            }
            if (GameWorld.Instance.GetMenu.P2 != VehicleType.None)
            {
                int gold = 0;
                int accuracy = 0;
                foreach (Vehicle VH in GameWorld.Instance.Vehicles)
                {
                    if (VH.Control == Controls.UDLR)
                    {
                        gold = VH.Stats.TotalAmountOfGold;
                        accuracy = VH.Stats.CalculateAccuracy();
                    }
                }
                int wave = GameWorld.Instance.GetSpawn.Wave;
                string player2 = "insert into player (ID,gold, Accuracy,PlayerID,wave) values (" + ID + "," + gold + "," + accuracy + ",2," + wave + ");";
                WriteToDB(player2);
                WriteBullets(ID, 2);
            }

            scoreSaved = true;
        }

        /// <summary>
        /// writes in the number bullets a player has shot
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="playerID"></param>
        private void WriteBullets(int ID, int playerID)
        {
            int basicBulletShot = 0;
            int biggerBulletShot = 0;
            int sniperBulletShot = 0;
            int shotgunBulletShot = 0;
            foreach (Vehicle VH in GameWorld.Instance.Vehicles)
            {
                if ((playerID == 1 && VH.Control == Controls.WASD) || (playerID == 2 && VH.Control == Controls.UDLR))
                {
                    basicBulletShot = VH.Stats.BasicBulletCounter;
                    biggerBulletShot = VH.Stats.BiggerBulletCounter;
                    sniperBulletShot = VH.Stats.SniperBulletCounter;
                    shotgunBulletShot = VH.Stats.ShotgunPelletsCounter;
                }
            }
            string basicBullet = "insert into bullets (ID,playerID, bullets_shot,bulletName) values (" + ID + "," + playerID + "," + basicBulletShot + "," + "BasicBullet" + "); ";
            string biggerBullet = "insert into bullets (ID,playerID, bullets_shot,bulletName) values (" + ID + "," + playerID + "," + biggerBulletShot + "," + "biggerBullet" + "); ";
            string sniperBullet = "insert into bullets (ID,playerID, bullets_shot,bulletName) values (" + ID + "," + playerID + "," + sniperBulletShot + "," + "sniperBullet" + "); ";
            string shotgunPellet = "insert into bullets (ID,playerID, bullets_shot,bulletName) values (" + ID + "," + playerID + "," + shotgunBulletShot + "," + "shotgunPellet" + "); ";
        }

        public void LoadScoreToScreen()
        {
            int scoresCount = 0;
            List<string> names = new List<string>();
            List<int> scores = new List<int>();
            List<int> ids = new List<int>();

            List<string> enemyNames = new List<string>();
            List<int> enemyKills = new List<int>();
            List<int> spitterBullets = new List<int>();
            List<int> gold = new List<int>();
            List<int> basicBullets = new List<int>();
            List<int> biggerBullets = new List<int>();
            List<int> sniperBullets = new List<int>();
            List<int> shotgunBullets = new List<int>();
            List<int> waves = new List<int>();
            List<string> towerNames = new List<string>();
            List<int> towerKills = new List<int>();
            List<int> towerBuild = new List<int>();
            List<int> towerDead = new List<int>();
            List<int> totalBullets = new List<int>();
            List<int> totalTowersBuild = new List<int>();
            List<int> totalTowersDead = new List<int>();
            List<int> totalTowerKills = new List<int>();
            List<int> totalEnemyDead = new List<int>();
            List<int> totalPlayerKills = new List<int>();

            SQLiteConnection DBConnect = new SQLiteConnection("Data source = TankGameDatabase.db; Version = 3; ");
            DBConnect.Open();
            string highscore = "select Highscore.Name, Highscore.Score, HighScore.ID from Highscore limit 10 order by score desc";
            SQLiteCommand command = new SQLiteCommand(highscore, DBConnect);
            SQLiteDataReader highscoreReader = command.ExecuteReader();
            while (highscoreReader.Read())
            {
                names.Add((string)highscoreReader["Name"]);
                scores.Add((int)highscoreReader["Score"]);
                ids.Add((int)highscoreReader["ID"]);

                enemyKills.Add((int)highscoreReader["Enemy kills"]);
                spitterBullets.Add((int)highscoreReader["Spitter bullets shot"]);
                gold.Add((int)highscoreReader["Gold"]);
                basicBullets.Add((int)highscoreReader["Basic bullets shot"]);
                biggerBullets.Add((int)highscoreReader["Bigger bullets shot"]);
                sniperBullets.Add((int)highscoreReader["Sniper bullets shot"]);
                shotgunBullets.Add((int)highscoreReader["Shotgun bullets shot"]);
                waves.Add((int)highscoreReader["Wave"]);
                towerNames.Add((string)highscoreReader["Tower name"]);
                towerKills.Add((int)highscoreReader["Tower kills"]);
                towerBuild.Add((int)highscoreReader["Tower build"]);
                towerDead.Add((int)highscoreReader["Tower dead"]);
                totalBullets.Add((int)highscoreReader["Total bullets fired"]);
                totalTowersBuild.Add((int)highscoreReader["Total tower build"]);
                totalTowersDead.Add((int)highscoreReader["Total tower dead"]);
                totalTowerKills.Add((int)highscoreReader["Total tower kills"]);
                totalEnemyDead.Add((int)highscoreReader["Total enemy dead"]);
                totalPlayerKills.Add((int)highscoreReader["Total player kills"]);
            }
            DBConnect.Close();
            string enemyNameCommand = "";//command to get EnemyName
            enemyNames = ReadFromDB(enemyNameCommand, "Enemy name");

            string enemyKillCommand = "";//command to get EnemyName
            enemyKills = ReadFromDB(enemyKillCommand, "Enemy kills", 1);


            for (int i = 0; i < scoresCount; i++)
            {
                highscores.Add(new Highscore(names[i], scores[i], enemyNames[i], enemyKills[i], spitterBullets[i], gold[i], basicBullets[i], biggerBullets[i], sniperBullets[i], shotgunBullets[i],
                waves[i], towerNames[i], towerKills[i], towerDead[i], towerBuild[i], totalBullets[i], totalTowersBuild[i], totalTowersDead[i], totalTowerKills[i], totalEnemyDead[i], totalPlayerKills[i]));
            }
        }
    }
}
