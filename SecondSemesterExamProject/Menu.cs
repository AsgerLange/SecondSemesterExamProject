using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Menu
    {
        private bool checkedForUnlockables = false;

        public bool monsterUnlocked = false;
        private VehicleType p1 = VehicleType.Tank;
        private VehicleType p2 = VehicleType.None;
        private int p1TypeInt;
        private int p2TypeInt;
        private string title = Constant.title;
        private Vector2 titlePos;
        private Vector2 p1Pos = new Vector2(450, Constant.hight / 2);
        private Vector2 p1UpPos = new Vector2(435, Constant.hight / 2 - 45);
        private Vector2 startGamePos = new Vector2(Constant.width / 2 - 65, Constant.hight / 2 - 25);
        private Vector2 p1DownPos = new Vector2(435, Constant.hight / 2 + 30);
        private Vector2 p2Pos = new Vector2(Constant.width - 450, Constant.hight / 2);
        private Vector2 p2UpPos = new Vector2(Constant.width - 465, Constant.hight / 2 - 45);
        private Vector2 p2DownPos = new Vector2(Constant.width - 465, Constant.hight / 2 + 30);
        private Vector2 p1ControlsPos = new Vector2(200, Constant.hight / 2 - 75);
        private Vector2 p2ControlsPos = new Vector2(Constant.width / 2 + 250, Constant.hight / 2 - 100);

        private GameObject p1Choice;
        private GameObject p2Choice;
        private SpriteFont titleFont;
        private SpriteFont font;
        private List<Button> buttons = new List<Button>();
        private Texture2D menuBackGround;
        private Texture2D p1ControlsImage;
        private Texture2D p2ControlsImage;

        private Button pvpButton;


        public VehicleType P1
        {
            get { return p1; }
        }

        public VehicleType P2
        {
            get { return p2; }
        }

        public Menu()
        {
            p1TypeInt = (int)p1;
            p2TypeInt = (int)p2;

            p1Choice = ChangeVehicle(p1, 1);
            p1Choice.Transform.Position = p1Pos;
            p2Choice = ChangeVehicle(p2, 2);
            p2Choice.Transform.Position = p2Pos;

            PlaceButtons();
        }

        /// <summary>
        /// initializes the vehicles to be 3n
        /// </summary>
        private void AddVehiclesToBeDrawn()
        {
            p1Choice = new GameObject();
            p1Choice.Transform.Position = new Vector2(20, Constant.hight / 2);
            p1Choice.AddComponent(new SpriteRenderer(p1Choice, Constant.tankSpriteSheet + "1", 0.05f));
            p1Choice.AddComponent(new Animator(p1Choice));
            p1Choice.AddComponent(new Tank(p1Choice, Controls.WASD, Constant.tankHealth, Constant.tankMoveSpeed,
                Constant.tankRotateSpeed, Constant.tankStartGold, TowerType.BasicTower, 1));
            ((Tank)p1Choice.GetComponent("Tank")).IsAlive = false;

            p2Choice = new GameObject();
            p2Choice.Transform.Position = new Vector2(Constant.width - 250, Constant.hight / 2);
            p2Choice.AddComponent(new SpriteRenderer(p2Choice, Constant.tankSpriteSheet + "2", 0.05f));
            p2Choice.AddComponent(new Animator(p2Choice));
            p2Choice.AddComponent(new Tank(p2Choice, Controls.WASD, Constant.tankHealth, Constant.tankMoveSpeed,
                Constant.tankRotateSpeed, Constant.tankStartGold, TowerType.BasicTower, 2));
            ((Tank)p2Choice.GetComponent("Tank")).IsAlive = false;

            p1Choice = ChangeVehicle(p1, 1);
            p2Choice = ChangeVehicle(p2, 2);
        }

        /// <summary>
        /// changes the model that is drawn for the chocen vehicle
        /// </summary>
        /// <param name="type"></param>
        /// <param name="player"></param>
        /// <param name="playerDraw"></param>
        private GameObject ChangeVehicle(VehicleType type, int player)
        {
            GameObject playerDraw = new GameObject();

            switch (type)
            {
                case VehicleType.None:
                    break;
                case VehicleType.Tank:
                    playerDraw.Transform.Position = new Vector2(0, 0);
                    playerDraw.AddComponent(new SpriteRenderer(playerDraw, Constant.tankSpriteSheet + player, 0.05f));
                    playerDraw.AddComponent(new Animator(playerDraw));
                    playerDraw.AddComponent(new Tank(playerDraw, Controls.WASD, Constant.tankHealth, Constant.tankMoveSpeed,
                        Constant.tankRotateSpeed, Constant.tankStartGold, TowerType.BasicTower, player));
                    ((Tank)playerDraw.GetComponent("Tank")).IsAlive = false;
                    break;

                case VehicleType.Bike:
                    playerDraw.Transform.Position = new Vector2(0, 0);
                    playerDraw.AddComponent(new SpriteRenderer(p1Choice, Constant.bikeSpriteSheet + player, 0.05f));
                    playerDraw.AddComponent(new Animator(playerDraw));
                    playerDraw.AddComponent(new Bike(playerDraw, Controls.WASD, Constant.tankHealth, Constant.tankMoveSpeed,
                        Constant.tankRotateSpeed, Constant.tankStartGold, TowerType.BasicTower, player));
                    ((Bike)playerDraw.GetComponent("Bike")).IsAlive = false;
                    break;

                case VehicleType.Plane:
                    playerDraw.Transform.Position = new Vector2(0, 0);
                    playerDraw.AddComponent(new SpriteRenderer(p1Choice, Constant.planeSpriteSheet + player, 0.05f));
                    playerDraw.AddComponent(new Animator(playerDraw));
                    playerDraw.AddComponent(new Plane(playerDraw, Controls.WASD, Constant.tankHealth, Constant.tankMoveSpeed,
                        Constant.tankRotateSpeed, Constant.tankStartGold, TowerType.BasicTower, player));
                    ((Plane)playerDraw.GetComponent("Plane")).IsAlive = false;
                    break;

                case VehicleType.MonsterVehicle:
                    playerDraw.Transform.Position = new Vector2(0, 0);
                    playerDraw.AddComponent(new SpriteRenderer(p1Choice, Constant.monsterSpriteSheet + player, 0.05f));
                    playerDraw.AddComponent(new Animator(playerDraw));
                    playerDraw.AddComponent(new MonsterVehicle(playerDraw, Controls.WASD, Constant.monsterHealth, Constant.monsterMoveSpeed,
                        Constant.monsterRotateSpeed, Constant.monsterStartGold, TowerType.BasicTower, player));
                    ((MonsterVehicle)playerDraw.GetComponent("MonsterVehicle")).IsAlive = false;
                    break;

                default:
                    break;
            }
            playerDraw.LoadContent(GameWorld.Instance.Content);
            return playerDraw;
        }

        private void UnlockMonsterVehicle()
        {
            if (monsterUnlocked == false && checkedForUnlockables == false)
            {
                string command = "select wave from player ORDER BY wave desc;";
                string collumn = "wave";
                List<int> output = new List<int>();
                output = GameWorld.Instance.score.ReadFromDB(command, collumn, 1);

                foreach (int item in output)
                {
                    if (item >= 20)
                    {
                        monsterUnlocked = true;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// places the buttons on the menu screen
        /// </summary>
        private void PlaceButtons()
        {
            string text = Constant.startGameButton;
            Button StartGame = new Button(startGamePos, Constant.RedButtonTexture, Constant.buttonFont)
            {
                Text = text
            };
            StartGame.PenColour = Color.Gold;
            StartGame.click += StartGame_click;
            buttons.Add(StartGame);

            Button p1Up = new Button(p1UpPos, Constant.BlueButtonUpTexture, Constant.buttonFont);
            p1Up.click += P1Up_click;
            buttons.Add(p1Up);

            Button p1Down = new Button(p1DownPos, Constant.BlueButtonDownTexture, Constant.buttonFont);
            p1Down.click += P1Down_click;
            buttons.Add(p1Down);

            Button p2Up = new Button(p2UpPos, Constant.GreenButtonUpTexture, Constant.buttonFont);
            p2Up.click += P2Up_click;
            buttons.Add(p2Up);

            Button p2Down = new Button(p2DownPos, Constant.GreenButtonDownTexture, Constant.buttonFont);
            p2Down.click += P2Down_click;
            buttons.Add(p2Down);

            pvpButton = new Button(new Vector2(startGamePos.X, startGamePos.Y + 50), Constant.RedButtonTexture, Constant.buttonFont)
            {
                Text = "Start PVP"
            };
            pvpButton.PenColour = Color.Gold;
            pvpButton.click += Pvp_click;
            buttons.Add(pvpButton);
        }

        /// <summary>
        /// Checks ealier sessions for unlockables
        /// </summary>
        private void CheckIfUnlocked()
        {
            if (checkedForUnlockables == false)
            {

                UnlockMonsterVehicle();

                checkedForUnlockables = true;
            }
        }
        /// <summary>
        /// updates the menu
        /// </summary>
        public void Update()
        {
            CheckIfUnlocked();
            if (buttons.Count > 0)
            {
                foreach (Button but in buttons)
                {
                    but.Update();
                }
            }
            if (p1 != VehicleType.None)
            {
                p1Choice.Update();
            }
            if (p2 != VehicleType.None)
            {
                p2Choice.Update();
            }
        }

        /// <summary>
        /// draws the menu
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(titleFont, title, titlePos, Color.LightSteelBlue, 0, Vector2.Zero, 1, SpriteEffects.None, 0.3f);

            if (buttons.Count > 0)
            {
                foreach (Button but in buttons)
                {
                    but.Draw(spriteBatch);
                }
            }


            if (p1 != VehicleType.None)
            {
                spriteBatch.Draw(p1ControlsImage, p1ControlsPos, null, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0.5f);

                p1Choice.Draw(spriteBatch);
            }
            if (p2 != VehicleType.None)
            {
                spriteBatch.Draw(p2ControlsImage, p2ControlsPos, null, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0.5f);

                p2Choice.Draw(spriteBatch);
            }
            spriteBatch.Draw(menuBackGround, new Rectangle(0, 0, Constant.width, Constant.hight),
                null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 1);
        }

        /// <summary>
        /// loads the menu content
        /// </summary>
        /// <param name="content"></param>
        public virtual void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>(Constant.buttonFont);
            titleFont = content.Load<SpriteFont>(Constant.titleFont);
            titlePos = new Vector2(Constant.width / 2 - titleFont.MeasureString(title).X / 2, 30);
            menuBackGround = content.Load<Texture2D>(Constant.menuBackGround);
            p1ControlsImage = content.Load<Texture2D>(Constant.p1ControlImagePath);
            p2ControlsImage = content.Load<Texture2D>(Constant.p2ControlImagePath);

            foreach (Button but in buttons)
            {
                but.LoadContent(content);
            }
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartGame_click(object sender, EventArgs e)
        {
            if (!(p1 == VehicleType.None && p2 == VehicleType.None))
            {
                SpawnPlayers();
                GameWorld.Instance.GetGameState = GameState.Game;


                //adds objects to the map
                GameWorld.Instance.Map = new Map();


                //Creates the new spawner that spawns the waves
                GameWorld.Instance.GetSpawn = new Spawn(Constant.width, Constant.hight);



            }
        }

        /// <summary>
        /// Handles what happens when the up button for the left player is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void P1Up_click(object sender, EventArgs e)
        {
            p1TypeInt++;
            int maxLenght = Enum.GetNames(typeof(VehicleType)).Length;
            if (monsterUnlocked == false)
            {
                maxLenght -= 1;
            }

            if (p1TypeInt >= maxLenght)
            {
                p1TypeInt = 0;
            }
            p1 = (VehicleType)p1TypeInt;
            p1Choice = ChangeVehicle(p1, 1);
            p1Choice.Transform.Position = p1Pos;
        }

        /// <summary>
        /// Handles what happens when the down button for the left player is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void P1Down_click(object sender, EventArgs e)
        {
            p1TypeInt--;
            int maxLenght = Enum.GetNames(typeof(VehicleType)).Length;
            if (monsterUnlocked == false)
            {
                maxLenght -= 1;
            }
            if (p1TypeInt < 0)
            {
                p1TypeInt = maxLenght - 1;
            }
            p1 = (VehicleType)p1TypeInt;
            p1Choice = ChangeVehicle(p1, 1);
            p1Choice.Transform.Position = p1Pos;
        }

        /// <summary>
        /// Handles what happens when the up button for the right player is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void P2Up_click(object sender, EventArgs e)
        {
            p2TypeInt++;
            int maxLenght = Enum.GetNames(typeof(VehicleType)).Length;
            if (monsterUnlocked == false)
            {
                maxLenght -= 1;
            }
            if (p2TypeInt >= maxLenght)
            {
                p2TypeInt = 0;
            }
            p2 = (VehicleType)p2TypeInt;
            p2Choice = ChangeVehicle(p2, 2);
            p2Choice.Transform.Position = p2Pos;
        }

        /// <summary>
        /// Handles what happens when the down button for the right player is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void P2Down_click(object sender, EventArgs e)
        {
            p2TypeInt--;
            int maxLenght = Enum.GetNames(typeof(VehicleType)).Length;
            if (monsterUnlocked == false)
            {
                maxLenght -= 1;
            }
            if (p2TypeInt < 0)
            {
                p2TypeInt = maxLenght - 1;
            }
            p2 = (VehicleType)p2TypeInt;
            p2Choice = ChangeVehicle(p2, 2);
            p2Choice.Transform.Position = p2Pos;
        }
        private void Pvp_click(object sender, EventArgs e)
        {
            if ((!(p1 == VehicleType.None) && !(p2 == VehicleType.None)))
            {
                GameWorld.Instance.pvp = true;
                SpawnPlayers();
                GameWorld.Instance.GetGameState = GameState.Game;


                //adds objects to the map
                GameWorld.Instance.Map = new Map();


                //Creates the new spawner that spawns the waves
                GameWorld.Instance.GetSpawn = new Spawn(Constant.width, Constant.hight);

            }
        }
        /// <summary>
        /// spawns the player(s)
        /// </summary>
        private void SpawnPlayers()
        {
            if (!(p1 == VehicleType.None))
            {
                GameObjectDirector.Instance.Construct(p1, Controls.WASD, 1, Alignment.Friendly);

            }
            if (!(p2 == VehicleType.None))
            {
                if (GameWorld.Instance.pvp == false)
                {
                    GameObjectDirector.Instance.Construct(p2, Controls.UDLR, 2, Alignment.Friendly);

                }
                else
                {
                    GameObjectDirector.Instance.Construct(p2, Controls.UDLR, 2, Alignment.Enemy);

                }
            }
        }
    }
}
