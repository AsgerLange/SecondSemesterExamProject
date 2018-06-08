using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Highscore
    {
        SpriteFont font;
        #region StatFields
        private string highscoreName;
        private int score;
        #endregion

        public Highscore(string highscoreName, int score)
        {
            this.highscoreName = highscoreName;
            this.score = score;
        }

        public void Draw(SpriteBatch spriteBatch, int number)
        {
            string text="";
            int numberToScreen = number + 1;
            if (numberToScreen > 9)
            {
                text = "[ " + numberToScreen + " ]  " + highscoreName;
            }
            else
            {
                text = " [ " + numberToScreen + " ]   " + highscoreName;
            }
            spriteBatch.DrawString(font, text, new Vector2(Constant.width / 2 - 138, 200 + ((font.MeasureString(text).Y) + 20) * number), Color.Gold, 0, Vector2.Zero, 1, SpriteEffects.None, 0.3f);

            string scorePoint = "" + score;
            spriteBatch.DrawString(font, scorePoint, new Vector2(Constant.width / 2 + 140, 200 + ((font.MeasureString(scorePoint).Y) + 20) * number), Color.Gold, 0, Vector2.Zero, 1, SpriteEffects.None, 0.3f);
        }

        public virtual void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Stat");
        }
    }
}
