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
            textBox = new Rectangle(Constant.width / 2 - 200, Constant.hight / 2, 400, 50);//the textbox pos

            if (!(File.Exists(@"TankGameDatabase.db")))
            {
                SQLiteConnection.CreateFile("TankGameDatabase.db");
                CreateTables();
            }

            highscores.Add(new Highscore("Stefan", 9999, 50000));
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
            string totalStats = "Create table Total_stats (ID integer primary key, Total_bullets_fired int, Total_tower_build int, Total_player_deaths int, Total_enemy_killed int, Total_games_played int)";
            string player = "Create table Player (ID integer, Gold int, Accuracy int, PlayerID integer, Wave int, foreign key(ID) REFERENCES higscores(ID))";
            string tower = "Create table Tower (ID integer,PlayerID integer, Tower_name varchar, Tower_Build int, foreign key(PlayerID) REFERENCES player(PlayerID),foreign key(ID) REFERENCES higscores(ID))";
            string enemies = "Create table Enemies (ID integer, Enemy_name varchar, Enemy_kills int, Spitter_bullets_shot,foreign key(ID) REFERENCES higscores(ID))";
            string bullets = "Create table bullets (ID integer, PlayerID integer, bullets_shot int, bullet_Name, foreign key(PlayerID) REFERENCES player(PlayerID),foreign key(ID) REFERENCES higscores(ID))";

            WriteToDB(highscore);
            WriteToDB(totalStats);
            WriteToDB(tower);
            WriteToDB(player);
            WriteToDB(enemies);
            WriteToDB(bullets);

            string totalStatsSetup = "insert into Total_stats(ID, Total_bullets_fired, Total_tower_build, Total_player_deaths, Total_enemy_killed, Total_games_played) values(null,0,0,0,0,0); ";
            WriteToDB(totalStatsSetup);
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
            theBox = content.Load<Texture2D>(Constant.TexBoxButton);
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
                spriteBatch.Draw(theBox, new Vector2(textBox.X - 5, textBox.Y - 15), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);//Draws the box
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
                WriteTower(ID, 1);
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
                WriteTower(ID, 2);
            }
            WriteEnemies(ID);
            WriteToTotalStats();

            scoreSaved = true;
        }

        /// <summary>
        /// Updates the total Stats with the info from the current game
        /// </summary>
        private void WriteToTotalStats()
        {
            foreach (Vehicle VH in GameWorld.Instance.Vehicles)
            {
                if ((VH.Control == Controls.WASD) || (VH.Control == Controls.UDLR))
                {
                    int bulletsFiredThisGame = VH.Stats.BasicBulletCounter + VH.Stats.BiggerBulletCounter + VH.Stats.ShotgunPelletsCounter + VH.Stats.SniperBulletCounter;
                    string totalStatsUpdateBulletsFired = "UPDATE Total_stats SET Total_bullets_fired = Total_bullets_fired + " + bulletsFiredThisGame + " where ID = 1";
                    WriteToDB(totalStatsUpdateBulletsFired);

                    int towersBuildThisGame = VH.Stats.BasicTowerBuilt + VH.Stats.MachinegunTowerbuilt + VH.Stats.ShotgunTowerbuilt + VH.Stats.SniperTowerBuilt;
                    string totalStatsUpdateTowerBuild = "UPDATE Total_stats SET Total_tower_build = Total_tower_build + " + towersBuildThisGame + " where ID = 1";
                    WriteToDB(totalStatsUpdateTowerBuild);

                    int playerDeathAmountThisGame = VH.Stats.PlayerDeathAmmount;
                    string totalStatsUpdatePlayerDeaths = "UPDATE Total_stats SET Total_player_deaths = Total_player_deaths + " + playerDeathAmountThisGame + " where ID = 1";
                    WriteToDB(totalStatsUpdatePlayerDeaths);
                }
            }

            int enemiesKilled = Stats.BasicEnemyKilled + Stats.BasicEliteEnemyKilled + Stats.SwarmerKilled + Stats.SpitterKilled;
            string totalStatsUpdateEnemyKilled = "UPDATE Total_stats SET Total_enemy_killed = Total_enemy_killed + " + enemiesKilled + " where ID = 1";
            WriteToDB(totalStatsUpdateEnemyKilled);

            string totalStatsUpdateGamesPlayed = "UPDATE Total_stats SET Total_games_played = Total_games_played + 1 where ID = 1";
            WriteToDB(totalStatsUpdateGamesPlayed);
        }

        /// <summary>
        /// Writes the stats relevent to towers into the DB
        /// </summary>
        /// <param name="iD"></param>
        private void WriteTower(int iD, int playerID)
        {
            int towerBuildBasic = 0;
            int towerBuildShot = 0;
            int towerBuildSniper = 0;
            int towerBuildMachine = 0;

            foreach (Vehicle VH in GameWorld.Instance.Vehicles)
            {
                if ((playerID == 1 && VH.Control == Controls.WASD) || (playerID == 2 && VH.Control == Controls.UDLR))
                {
                    towerBuildBasic = VH.Stats.BasicTowerBuilt;
                    towerBuildShot = VH.Stats.ShotgunTowerbuilt;
                    towerBuildSniper = VH.Stats.SniperTowerBuilt;
                    towerBuildMachine = VH.Stats.MachinegunTowerbuilt;
                }
            }
            string BasicTower = "insert into tower (ID,PlayerID,Tower_name,Tower_Build) values (" + iD + "," + playerID + "," + "'BasicTower'" + "," + towerBuildBasic + ");";
            string ShotgunTower = "insert into tower (ID,PlayerID,Tower_name,Tower_Build) values (" + iD + "," + playerID + "," + "'ShotgunTower'" + "," + towerBuildShot + ");";
            string SniperTower = "insert into tower (ID,PlayerID,Tower_name,Tower_Build) values (" + iD + "," + playerID + "," + "'SniperTower'" + "," + towerBuildSniper + ");";
            string MachineGunTower = "insert into tower (ID,PlayerID,Tower_name,Tower_Build) values (" + iD + "," + playerID + "," + "'MachinegunTower'" + "," + towerBuildMachine + ");";
            WriteToDB(BasicTower);
            WriteToDB(ShotgunTower);
            WriteToDB(SniperTower);
            WriteToDB(MachineGunTower);
        }

        /// <summary>
        /// Writes the stats relevent to enemies into the DB
        /// </summary>
        /// <param name="iD"></param>
        private void WriteEnemies(int iD)
        {
            string BasicEnemy = "insert into enemies (ID, Enemy_name,Enemy_kills, Spitter_bullets_shot) values (" + iD + "," + "'BasicEnemy'" + "," + Stats.BasicEnemyKilled + ", null );";
            string BasicEliteEnemy = "insert into enemies (ID, Enemy_name, Enemy_kills, Spitter_bullets_shot) values (" + iD + "," + "'BasicEliteEnemy'" + "," + Stats.BasicEliteEnemyKilled + ", null );";
            string SwarmerEnemy = "insert into enemies (ID, Enemy_name, Enemy_kills, Spitter_bullets_shot) values (" + iD + "," + "'SwarmerEnemy'" + "," + Stats.SwarmerKilled + ", null );";
            string Spitter = "insert into enemies (ID, Enemy_name, Enemy_kills, Spitter_bullets_shot) values (" + iD + "," + "'Spitter'" + "," + Stats.SpitterKilled + "," + Stats.SpitterBulletCounter + ");";
            WriteToDB(BasicEnemy);
            WriteToDB(BasicEliteEnemy);
            WriteToDB(SwarmerEnemy);
            WriteToDB(Spitter);
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
            string basicBullet = "insert into bullets (ID,playerID, bullets_shot,bullet_Name) values (" + ID + "," + playerID + "," + basicBulletShot + "," + "'BasicBullet'" + "); ";
            string biggerBullet = "insert into bullets (ID,playerID, bullets_shot,bullet_Name) values (" + ID + "," + playerID + "," + biggerBulletShot + "," + "'biggerBullet'" + "); ";
            string sniperBullet = "insert into bullets (ID,playerID, bullets_shot,bullet_Name) values (" + ID + "," + playerID + "," + sniperBulletShot + "," + "'sniperBullet'" + "); ";
            string shotgunPellet = "insert into bullets (ID,playerID, bullets_shot,bullet_Name) values (" + ID + "," + playerID + "," + shotgunBulletShot + "," + "'shotgunPellet'" + "); ";
            WriteToDB(basicBullet);
            WriteToDB(biggerBullet);
            WriteToDB(sniperBullet);
            WriteToDB(shotgunPellet);
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

                gold.Add((int)highscoreReader["Gold"]);
            }
            DBConnect.Close();
            string enemyNameCommand = "";//command to get EnemyName
            enemyNames = ReadFromDB(enemyNameCommand, "Enemy name");

            string enemyKillCommand = "";//command to get EnemyName
            enemyKills = ReadFromDB(enemyKillCommand, "Enemy kills", 1);


            for (int i = 0; i < scoresCount; i++)
            {
                highscores.Add(new Highscore(names[i], scores[i], gold[i]));
            }
        }
    }
}
