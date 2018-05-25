using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TankGame
{
    class Score
    {
        //Fields
        private int score;
        public static string name = string.Empty;//Contains the string we need to use for player input
        private bool databseState = true;
        Keys[] lastKeys;//The list of all our keys pressed
        KeyboardState lastKeyboardState;//Checks last key pressed
        private float updateStamp;
        Rectangle textBox;
        Texture2D theBox;
        SpriteFont font;
        private string parsedText;

        //Properties
        public int GetScore
        {
            get { return score; }
            set { score = value; }
        }

        public Score()
        {
            textBox = new Rectangle(20, 20, 20, 20);
        }

        //public void SetupServer()
        //{

        //    if (databseState == true)
        //    {
        //        SQLiteConnection.CreateFile("TankGameDatabase.db");
        //        databseState = false;
        //    }

        //}
        //public void CreateTables()
        //{
        //    SQLiteConnection dbConnect = new SQLiteConnection("Data source=data.db;Version=3;");
        //    dbConnect.Open();
        //    string highscore = "Create table Highscores (ID varchar, Placing int, Name string, Score int)";
        //    string log = "Create table Log (ID varchar, Total int)";
        //    string tower = "Create table Tower (TotalKillTowers int, Tower1Kill int, Tower2Kill int, Tower3Kill int, TowerBuild int, TowerDead int)";
        //    string player = "Create table Player (TotalKills int)";
        //    string enemies = "Create table Enemies (Wave int, TotalKill int, Enemy1Kills int, Enemy2Kills int, Enemy3Kills int)";
        //    SQLiteCommand command = new SQLiteCommand(highscore, dbConnect);
        //    SQLiteCommand command2 = new SQLiteCommand(log, dbConnect);
        //    SQLiteCommand command3 = new SQLiteCommand(tower, dbConnect);
        //    SQLiteCommand command4 = new SQLiteCommand(player, dbConnect);
        //    SQLiteCommand command5 = new SQLiteCommand(enemies, dbConnect);
        //    command.ExecuteNonQuery();
        //    dbConnect.Close();
        //}
        public virtual void Update()
        {
            PlayerInput();
        }

        public void PlayerInput()
        {
            //Get the current keyboard state and keys that are pressed
            KeyboardState keyboardState = Keyboard.GetState();
            Keys[] keys = keyboardState.GetPressedKeys();

            foreach (Keys currentKey in keys)
            {
                if (currentKey != Keys.None)
                {
                    //If we have pressed the same key twice, wait atleast 125ms before adding it again
                    if (lastKeys.Contains(currentKey))
                    {
                        if ((GameWorld.Instance.TotalGameTime > updateStamp + 0.50))
                        {
                            HandleKey(GameWorld.Instance.TotalGameTime, currentKey);
                            updateStamp = GameWorld.Instance.TotalGameTime;
                        }
                    }
                    //If we press a new key, add it
                    else if (!lastKeys.Contains(currentKey))
                        HandleKey(GameWorld.Instance.TotalGameTime, currentKey);
                }
            }

            //Save the last keys and pressed keys array
            lastKeyboardState = keyboardState; //Puts the last keyboard state into the variable
            lastKeys = keys; //The Keys array here gives its value to the lastKeys variable
        }

        public void HandleKey(float totalGameTime, Keys currentKeys)
        {
            string playerInputString = currentKeys.ToString();//Makes the currentkeys into a string
            if (currentKeys == Keys.Space)
                playerInputString += " ";
            else if ((currentKeys == Keys.Back || currentKeys == Keys.Delete) && playerInputString.Length > 0)
                playerInputString = playerInputString.Remove(playerInputString.Length - 1);
            else if (currentKeys == Keys.Enter)
            {
                playerInputString += name;
                InsertScore();
            }
        }


        public virtual void LoadContent(ContentManager contentManager)
        {
            theBox = contentManager.Load<Texture2D>("Button");
            font = contentManager.Load<SpriteFont>("Stat");

            parsedText = ParseText(name);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(theBox, textBox, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(font, Score.name, new Vector2(textBox.X, textBox.Y), Color.Black);
        }
        private string ParseText(string text)
        {
            string line = String.Empty;
            string returnString = String.Empty;
            string[] wordArray = text.Split(' ');

            foreach (string word in wordArray)
            {
                if (font.MeasureString(line + word).Length() > textBox.Width)
                {
                    returnString = returnString + line + '\n';
                    line = string.Empty;
                }
                line = line + word + ' ';
            }
            return returnString + line;
        }

        public void InsertScore()
        {
            SQLiteConnection dbConnect = new SQLiteConnection("Data source=data.db;Version=3;");
            string insert = "insert into Higscores (name, score) values (" + name + "," + score + ")";
            SQLiteCommand command = new SQLiteCommand(insert, dbConnect);
            command.ExecuteNonQuery();
        }
        public void LoadScoreToScreen()
        {
            string highscore = "select Highscore.Placing, Highscore.Name, Highscore.Score from Highscore limit 10 order by score desc";
            SQLiteCommand command = new SQLiteCommand(highscore);
            SQLiteDataReader highscoreReader = command.ExecuteReader();
            while (highscoreReader.Read())
            {
                //Draw out a highscorelist in the middle of the screen.  
            }
        }
    }
}
