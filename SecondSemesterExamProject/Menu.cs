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
        private SpriteFont font;

        public Menu()
        {

        }

        /// <summary>
        /// updates the menu
        /// </summary>
        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Enter))
            {
                GameWorld.Instance.GetGameState = GameState.Game;
            }
        }

        /// <summary>
        /// draws the menu
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Press Enter to start", new Vector2(Constant.width / 2 - 50, Constant.higth / 2), Color.YellowGreen);
        }

        public virtual void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Stat");
        }
    }
}
