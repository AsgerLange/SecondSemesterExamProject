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
        private bool scoresLoaded = false;
        private Rectangle textBox;
        private Texture2D theBox;
        private Texture2D scoreBackGround;
        private Texture2D BackGround;
        private Vector2 scoreBackGroundPos = new Vector2(Constant.width / 2 - 150, 120);
        private SpriteFont font;
        private KeyboardState lastKeyboardState;//Checks the last key pressed
        private Keys[] lastKey;//Contains a array of the keys that has been pressed.
        private string parsedText;
        private double timer;
        private List<Highscore> highscores = new List<Highscore>();
        #endregion


        public Score()
        {
            textBox = new Rectangle(Constant.width / 2 - 150, Constant.hight / 2, 450, 50);//the textbox pos

            if (!(File.Exists(@"TankGameDatabase.db")))
            {
                SQLiteConnection.CreateFile("TankGameDatabase.db");
                CreateTables();
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
            if (scoreSaved && nameEntered)
            {
                if (scoresLoaded == false)
                {
                    LoadScoreToScreen();
                }
            }
        }
        /// <summary>
        /// Handles spesific keys such as space, backspace, delete and enter.
        /// </summary>
        public void HandleKey(GameTime gameTime, Keys currentKey)
        #region Keyboard input
        {
            KeyboardState keyboardState = Keyboard.GetState();
            string keyString;
            switch (currentKey)
            {
                case Keys.Tab:
                    break;
                case Keys.CapsLock:
                    break;
                case Keys.Escape:
                    break;
                case Keys.PageUp:
                    break;
                case Keys.PageDown:
                    break;
                case Keys.End:
                    break;
                case Keys.Home:
                    break;
                case Keys.Left:
                    break;
                case Keys.Up:
                    break;
                case Keys.Right:
                    break;
                case Keys.Down:
                    break;
                case Keys.Select:
                    break;
                case Keys.Print:
                    break;
                case Keys.Execute:
                    break;
                case Keys.PrintScreen:
                    break;
                case Keys.Insert:
                    break;
                case Keys.Help:
                    break;
                case Keys.D0:
                    keyString = "0";//Turns the currentkeys into a string
                    if (keyString.Length + name.Length < 21)
                    {
                        name += keyString;
                    }
                    break;
                case Keys.D1:
                    keyString = "1";//Turns the currentkeys into a string
                    if (keyString.Length + name.Length < 21)
                    {
                        name += keyString;
                    }
                    break;
                case Keys.D2:
                    keyString = "2";//Turns the currentkeys into a string
                    if (keyString.Length + name.Length < 21)
                    {
                        name += keyString;
                    }
                    break;
                case Keys.D3:
                    keyString = "3";//Turns the currentkeys into a string
                    if (keyString.Length + name.Length < 21)
                    {
                        name += keyString;
                    }
                    break;
                case Keys.D4:
                    keyString = "4";//Turns the currentkeys into a string
                    if (keyString.Length + name.Length < 21)
                    {
                        name += keyString;
                    }
                    break;
                case Keys.D5:
                    keyString = "5";//Turns the currentkeys into a string
                    if (keyString.Length + name.Length < 21)
                    {
                        name += keyString;
                    }
                    break; ;
                case Keys.D6:
                    keyString = "6";//Turns the currentkeys into a string
                    if (keyString.Length + name.Length < 21)
                    {
                        name += keyString;
                    }
                    break;
                case Keys.D7:
                    keyString = "7";//Turns the currentkeys into a string
                    if (keyString.Length + name.Length < 21)
                    {
                        name += keyString;
                    }
                    break;
                case Keys.D8:
                    keyString = "8";//Turns the currentkeys into a string
                    if (keyString.Length + name.Length < 21)
                    {
                        name += keyString;
                    }
                    break;
                case Keys.D9:
                    keyString = "9";//Turns the currentkeys into a string
                    if (keyString.Length + name.Length < 21)
                    {
                        name += keyString;
                    }
                    break;
                case Keys.LeftWindows:
                    break;
                case Keys.RightWindows:
                    break;
                case Keys.Apps:
                    break;
                case Keys.Sleep:
                    break;
                case Keys.NumPad0:
                    keyString = "0";//Turns the currentkeys into a string
                    if (keyString.Length + name.Length < 21)
                    {
                        name += keyString;
                    }
                    break;
                case Keys.NumPad1:
                    keyString = "1";//Turns the currentkeys into a string
                    if (keyString.Length + name.Length < 21)
                    {
                        name += keyString;
                    }
                    break;
                case Keys.NumPad2:
                    keyString = "2";//Turns the currentkeys into a string
                    if (keyString.Length + name.Length < 21)
                    {
                        name += keyString;
                    }
                    break;
                case Keys.NumPad3:
                    keyString = "3";//Turns the currentkeys into a string
                    if (keyString.Length + name.Length < 21)
                    {
                        name += keyString;
                    }
                    break;
                case Keys.NumPad4:
                    keyString = "4";//Turns the currentkeys into a string
                    if (keyString.Length + name.Length < 21)
                    {
                        name += keyString;
                    }
                    break;
                case Keys.NumPad5:
                    keyString = "5";//Turns the currentkeys into a string
                    if (keyString.Length + name.Length < 21)
                    {
                        name += keyString;
                    }
                    break;
                case Keys.NumPad6:
                    keyString = "6";//Turns the currentkeys into a string
                    if (keyString.Length + name.Length < 21)
                    {
                        name += keyString;
                    }
                    break;
                case Keys.NumPad7:
                    keyString = "7";//Turns the currentkeys into a string
                    if (keyString.Length + name.Length < 21)
                    {
                        name += keyString;
                    }
                    break;
                case Keys.NumPad8:
                    keyString = "8";//Turns the currentkeys into a string
                    if (keyString.Length + name.Length < 21)
                    {
                        name += keyString;
                    }
                    break;
                case Keys.NumPad9:
                    keyString = "9";//Turns the currentkeys into a string
                    if (keyString.Length + name.Length < 21)
                    {
                        name += keyString;
                    }
                    break;
                case Keys.Multiply:
                    break;
                case Keys.Add:
                    break;
                case Keys.Separator:
                    break;
                case Keys.Subtract:
                    break;
                case Keys.Decimal:
                    break;
                case Keys.Divide:
                    break;
                case Keys.F1:
                    break;
                case Keys.F2:
                    break;
                case Keys.F3:
                    break;
                case Keys.F4:
                    break;
                case Keys.F5:
                    break;
                case Keys.F6:
                    break;
                case Keys.F7:
                    break;
                case Keys.F8:
                    break;
                case Keys.F9:
                    break;
                case Keys.F10:
                    break;
                case Keys.F11:
                    break;
                case Keys.F12:
                    break;
                case Keys.F13:
                    break;
                case Keys.F14:
                    break;
                case Keys.F15:
                    break;
                case Keys.F16:
                    break;
                case Keys.F17:
                    break;
                case Keys.F18:
                    break;
                case Keys.F19:
                    break;
                case Keys.F20:
                    break;
                case Keys.F21:
                    break;
                case Keys.F22:
                    break;
                case Keys.F23:
                    break;
                case Keys.F24:
                    break;
                case Keys.NumLock:
                    break;
                case Keys.Scroll:
                    break;
                case Keys.LeftShift:
                    break;
                case Keys.RightShift:
                    break;
                case Keys.LeftControl:
                    break;
                case Keys.RightControl:
                    break;
                case Keys.LeftAlt:
                    break;
                case Keys.RightAlt:
                    break;
                case Keys.BrowserBack:
                    break;
                case Keys.VolumeMute:
                    break;
                case Keys.VolumeDown:
                    break;
                case Keys.VolumeUp:
                    break;
                case Keys.MediaNextTrack:
                    break;
                case Keys.MediaPreviousTrack:
                    break;
                case Keys.MediaStop:
                    break;
                case Keys.MediaPlayPause:
                    break;
                case Keys.LaunchMail:
                    break;
                case Keys.SelectMedia:
                    break;
                case Keys.LaunchApplication1:
                    break;
                case Keys.LaunchApplication2:
                    break;
                case Keys.OemSemicolon:
                    break;
                case Keys.OemPlus:
                    break;
                case Keys.OemComma:
                    break;
                case Keys.OemMinus:
                    break;
                case Keys.OemPeriod:
                    break;
                case Keys.OemQuestion:
                    break;
                case Keys.OemTilde:
                    break;
                case Keys.OemOpenBrackets:
                    break;
                case Keys.OemPipe:
                    break;
                case Keys.OemCloseBrackets:
                    break;
                case Keys.OemQuotes:
                    break;
                case Keys.Oem8:
                    break;
                case Keys.OemBackslash:
                    break;
                case Keys.ProcessKey:
                    break;
                case Keys.Attn:
                    break;
                case Keys.Crsel:
                    break;
                case Keys.Exsel:
                    break;
                case Keys.EraseEof:
                    break;
                case Keys.Play:
                    break;
                case Keys.Zoom:
                    break;
                case Keys.Pa1:
                    break;
                case Keys.OemClear:
                    break;
                case Keys.ChatPadGreen:
                    break;
                case Keys.ChatPadOrange:
                    break;
                case Keys.Pause:
                    break;
                case Keys.ImeConvert:
                    break;
                case Keys.ImeNoConvert:
                    break;
                case Keys.Kana:
                    break;
                case Keys.Kanji:
                    break;
                case Keys.OemAuto:
                    break;
                case Keys.OemCopy:
                    break;
                case Keys.OemEnlW:
                    break;
                case Keys.Enter:
                    if (name != string.Empty)
                    {
                        nameEntered = true;
                        CreateHighScore();
                    }
                    break;
                case Keys.Back:
                    if (name.Length > 0)
                    {
                        name = name.Remove(name.Length - 1);
                    }
                    break;
                case Keys.Delete:
                    if (name.Length > 0)
                    {
                        name = name.Remove(name.Length - 1);
                    }
                    break;
                case Keys.Space:
                    name += " ";
                    break;

                default:
                    if (keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift))
                    {
                        keyString = currentKey.ToString();//Turns the currentkeys into a string
                    }
                    else
                    {
                        keyString = currentKey.ToString().ToLower();//Turns the currentkeys into a string
                    }
                    if (keyString.Length + name.Length < 15)
                    {
                        name += keyString;
                    }
                    break;
            }


            //Set the timer to the current time
            timer = gameTime.TotalGameTime.TotalMilliseconds;
        }
        #endregion

        /// <summary>
        /// Loads the content for inputspace
        /// </summary>
        public virtual void LoadContent(ContentManager content)
        {
            theBox = content.Load<Texture2D>(Constant.TexBoxButton);
            font = content.Load<SpriteFont>("Stat");
            scoreBackGround = content.Load<Texture2D>("ScoreScreen");
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
            DrawUnlocksFromSession(spriteBatch);

            spriteBatch.Draw(BackGround, new Rectangle(0, 0, Constant.width, Constant.hight), null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 1);
            if (scoreSaved == false && nameEntered == false)
            {
                string nameText = "Enter your Name Here and press ENTER";
                spriteBatch.DrawString(font, nameText, new Vector2(textBox.X -45, textBox.Y - 50), Color.Gold);//Draws the text
                spriteBatch.Draw(theBox, new Vector2(textBox.X - 5, textBox.Y - 15), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);//Draws the box
                spriteBatch.DrawString(font, ParseText(name), new Vector2(textBox.X + 5, textBox.Y), Color.Gold);//Draws the text
            }
            if (scoreSaved && nameEntered && scoresLoaded)
            {//HigscoreScreen
                string text = "HIGH SCORES";
                spriteBatch.DrawString(font, text, new Vector2(Constant.width / 1.84f - font.MeasureString(text).X / 2, 135), Color.Gold, 0, Vector2.Zero, 1, SpriteEffects.None, 0.3f);
                spriteBatch.Draw(scoreBackGround, scoreBackGroundPos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.9f);
                for (int i = 0; i < highscores.Count; i++)
                {
                    highscores[i].Draw(spriteBatch, i);
                }
            }
        }


        private void DrawUnlocksFromSession(SpriteBatch spriteBatch)
        {
            if (scoreSaved && nameEntered && scoresLoaded)
            {

                if (GameWorld.Instance.GetSpawn.Wave >= 20 && GameWorld.Instance.GetMenu.monsterUnlocked == false)
                {
                    spriteBatch.DrawString(font, "+NEW VEHICLE UNLOCKED: SIEGEBREAKER!", new Vector2(Constant.width / 2 - 160, Constant.hight - 90), Color.Gold);

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
            string siegeBreaker = "insert into enemies (ID, Enemy_name, Enemy_kills, Spitter_bullets_shot) values (" + iD + "," + "'SiegeBreaker'" + "," + Stats.SiegeBreakerKilled + ", null );";
            WriteToDB(BasicEnemy);
            WriteToDB(BasicEliteEnemy);
            WriteToDB(SwarmerEnemy);
            WriteToDB(Spitter);
            WriteToDB(siegeBreaker);
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

        /// <summary>
        /// get the top 10 highscores
        /// </summary>
        public void LoadScoreToScreen()
        {
            List<string> names = new List<string>();
            List<int> scores = new List<int>();
            List<int> iDs = new List<int>();

            string nameString = "select Name from highscores ORDER BY Score desc limit 10;";
            names = ReadFromDB(nameString, "Name");

            string scoresString = "select Score from highscores ORDER BY Score desc limit 10;";
            scores = ReadFromDB(scoresString, "Score", 1);

            for (int i = 0; i < names.Count; i++)
            {
                highscores.Add(new Highscore(names[i], scores[i]));
            }
            foreach (Highscore HS in highscores)
            {
                HS.LoadContent(GameWorld.Instance.Content);
            }
            scoresLoaded = true;
        }
    }
}
