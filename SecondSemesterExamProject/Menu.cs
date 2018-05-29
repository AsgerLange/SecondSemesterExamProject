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
        private SpriteFont font;
        private List<Button> buttons = new List<Button>();

        public Menu()
        {
            PlaceButtons();
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
        }

        /// <summary>
        /// loads the menu content
        /// </summary>
        /// <param name="content"></param>
        public virtual void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Stat");

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
        /// spawns the player(s)
        /// </summary>
        private void SpawnPlayers()
        {
            if (!(p1 == VehicleType.None))
            {
                GameObjectDirector.Instance.Construct(p1);
            }
            if (!(p2 == VehicleType.None))
            {
                GameObjectDirector.Instance.Construct(p2);
            }
        }
    }
}
