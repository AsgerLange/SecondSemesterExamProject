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
        private VehicleType p1 = VehicleType.Tank;
        private VehicleType p2 = VehicleType.None;
        private int p1TypeInt;
        private int p2TypeInt;
        private GameObject p1Choice;
        private GameObject p2Choice;
        private SpriteFont font;
        private List<Button> buttons = new List<Button>();

        public Menu()
        {
            p1TypeInt = (int)p1;
            p2TypeInt = (int)p2;


            AddVehiclesToBeDrawn();

            PlaceButtons();
        }

        /// <summary>
        /// initializes the vehicles to be drawn
        /// </summary>
        private void AddVehiclesToBeDrawn()
        {
            p1Choice = new GameObject();
            p1Choice.Transform.Position = new Vector2(50, Constant.higth / 2);
            p1Choice.AddComponent(new SpriteRenderer(p1Choice, Constant.tankSpriteSheet + "1", 0.05f));
            p1Choice.AddComponent(new Animator(p1Choice));
            p1Choice.AddComponent(new Tank(p1Choice, Controls.WASD, new Sniper(p1Choice), Constant.tankHealth, Constant.tankMoveSpeed,
                Constant.tankRotateSpeed, Constant.tankStartGold, TowerType.BasicTower));
            p1Choice.AddComponent(new Collider(p1Choice, Alignment.Friendly));

            p2Choice = new GameObject();
            p2Choice.Transform.Position = new Vector2(Constant.width - 250, Constant.higth / 2);
            p2Choice.AddComponent(new SpriteRenderer(p2Choice, Constant.tankSpriteSheet + "2", 0.05f));
            p2Choice.AddComponent(new Animator(p2Choice));
            p2Choice.AddComponent(new Tank(p2Choice, Controls.UDLR, new Sniper(p2Choice), Constant.tankHealth, Constant.tankMoveSpeed,
                Constant.tankRotateSpeed, Constant.tankStartGold, TowerType.BasicTower));
            p2Choice.AddComponent(new Collider(p2Choice, Alignment.Friendly));
        }

        /// <summary>
        /// places the buttons on the menu screen
        /// </summary>
        private void PlaceButtons()
        {
            string text = Constant.startGameButton;
            Button StartGame = new Button(new Vector2(Constant.width / 2 - 50, Constant.higth / 2), Constant.buttonTexture, Constant.buttonFont);
            StartGame.Text = text;
            StartGame.click += StartGame_click;
            buttons.Add(StartGame);

            Button p1Up = new Button(new Vector2(50, Constant.higth / 2 - 50), Constant.buttonTexture, Constant.buttonFont);
            p1Up.Text = " Up ";
            p1Up.click += P1Up_click;
            buttons.Add(p1Up);

            Button p1Down = new Button(new Vector2(50, Constant.higth / 2 + 50), Constant.buttonTexture, Constant.buttonFont);
            p1Down.Text = "Down";
            p1Down.click += P1Down_click;
            buttons.Add(p1Down);

            Button p2Up = new Button(new Vector2(Constant.width - 250, Constant.higth / 2 - 50), Constant.buttonTexture, Constant.buttonFont);
            p2Up.Text = " Up ";
            p2Up.click += P2Up_click;
            buttons.Add(p2Up);

            Button p2Down = new Button(new Vector2(Constant.width - 250, Constant.higth / 2 + 50), Constant.buttonTexture, Constant.buttonFont);
            p2Down.Text = "Down";
            p2Down.click += P2Down_click;
            buttons.Add(p2Down);

        }

        /// <summary>
        /// updates the menu
        /// </summary>
        public void Update()
        {
            if (buttons.Count > 0)
            {
                foreach (Button but in buttons)
                {
                    but.Update();
                }
            }
        }

        /// <summary>
        /// draws the menu
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (buttons.Count > 0)
            {
                foreach (Button but in buttons)
                {
                    but.Draw(spriteBatch);
                }
            }
            p1Choice.Draw(spriteBatch);
            p2Choice.Draw(spriteBatch);
        }

        /// <summary>
        /// loads the menu content
        /// </summary>
        /// <param name="content"></param>
        public virtual void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Stat");
            p1Choice.LoadContent(content);
            p2Choice.LoadContent(content);

            foreach (Button but in buttons)
            {
                but.LoadContent(content);
            }
        }

        /// <summary>
        /// Handles the functionality of the startgame button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartGame_click(object sender, EventArgs e)
        {
            if (!(p1 == VehicleType.None && p2 == VehicleType.None))
            {
                SpawnPlayers();
                GameWorld.Instance.GetGameState = GameState.Game;
                GameWorld.Instance.IsMouseVisible = false;
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
            if (p1TypeInt >= Enum.GetNames(typeof(VehicleType)).Length)
            {
                p1TypeInt = 0;
            }
            p1 = (VehicleType)p1TypeInt;
        }

        /// <summary>
        /// Handles what happens when the down button for the left player is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void P1Down_click(object sender, EventArgs e)
        {
            p1TypeInt--;
            if (p1TypeInt < 0)
            {
                p1TypeInt = Enum.GetNames(typeof(VehicleType)).Length - 1;
            }
            p1 = (VehicleType)p1TypeInt;
        }

        /// <summary>
        /// Handles what happens when the up button for the right player is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void P2Up_click(object sender, EventArgs e)
        {
            p2TypeInt++;
            if (p2TypeInt >= Enum.GetNames(typeof(VehicleType)).Length)
            {
                p2TypeInt = 0;
            }
            p2 = (VehicleType)p2TypeInt;
        }

        /// <summary>
        /// Handles what happens when the down button for the right player is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void P2Down_click(object sender, EventArgs e)
        {
            p2TypeInt--;
            if (p2TypeInt < 0)
            {
                p2TypeInt = Enum.GetNames(typeof(VehicleType)).Length - 1;
            }
            p2 = (VehicleType)p2TypeInt;
        }

        /// <summary>
        /// spawns the player(s)
        /// </summary>
        private void SpawnPlayers()
        {
            if (!(p1 == VehicleType.None))
            {
                GameObjectDirector.Instance.Construct(p1, Controls.WASD);
            }
            if (!(p2 == VehicleType.None))
            {
                GameObjectDirector.Instance.Construct(p2, Controls.UDLR);
            }
        }
    }
}
