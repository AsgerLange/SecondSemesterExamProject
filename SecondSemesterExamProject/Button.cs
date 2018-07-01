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
    public class Button
    {
        private bool isLoaded;
        private MouseState currentMouse;
        private MouseState previousMouse;
        private bool isHovering;
        private string texture;
        private string font;
        private Texture2D Texture;
        private SpriteFont Font;

        public EventHandler click;
        public bool Clicked { get; private set; }
        public Color PenColour { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }
        public string Text { get; set; }

        public Button(Vector2 position, string texture, string font)
        {
            Position = position;
            this.texture = texture;
            this.font = font;
            PenColour = Color.Black;
        }

        /// <summary>
        /// keeps track of the button
        /// </summary>
        public void Update()
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            Rectangle mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                isHovering = true;

                if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                {
                    click?.Invoke(this, new EventArgs());
                }
            }
            
        }

        /// <summary>
        /// draws the button
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Color color = Color.White;

            if (isHovering)
            {
                color = Color.Gray;
            }

            spriteBatch.Draw(Texture, Rectangle, null, color, 0, Vector2.Zero, SpriteEffects.None, 0.10f);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - Font.MeasureString(Text).X / 2;
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - Font.MeasureString(Text).Y / 2;

                spriteBatch.DrawString(Font, Text, new Vector2(x, y), PenColour, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
        }

        /// <summary>
        /// loads the button content
        /// </summary>
        /// <param name="content"></param>
        public virtual void LoadContent(ContentManager content)
        {
            if (!isLoaded)
            {
                Texture = content.Load<Texture2D>(texture);
                Font = content.Load<SpriteFont>(font);
                isLoaded = true;
            }
        }
    }
}
