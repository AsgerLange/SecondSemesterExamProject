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
        private string enemyName;
        private string towerName;
        private int score;
        private int enemyKills;
        private int spitterBullets;
        private int gold;
        private int basicBulletsShot;
        private int biggerBulletsShot;
        private int sniperBulletsShot;
        private int shotgunBulletsShot;
        private int wave;
        private int towerKills;
        private int towerBuild;
        private int towerDead;
        private int totalBullets;
        private int totalTowerBuild;
        private int totalTowerDead;
        private int totalTowerKills;
        private int totalEnemyDead;
        private int totalPlayerKills;
        #endregion

        public Highscore(string highscoreName, int score, string enemyName, int enemyKills, int spitterBullets, int gold,
            int basicBulletsShot, int biggerBulletsShot, int sniperBulletsShot, int shotgunBulletsShot,
            int wave, string towerName, int towerKills, int towerBuild, int towerDead, int totalBullets, int totalTowerBuild,
            int totalTowerDead, int totalTowerKills, int totalEnemyDead, int totalPlayerKills)
        {
            this.highscoreName = highscoreName;
            this.score = score;
            this.enemyName = enemyName;
            this.enemyKills = enemyKills;
            this.spitterBullets = spitterBullets;
            this.gold = gold;
            this.basicBulletsShot = basicBulletsShot;
            this.biggerBulletsShot = biggerBulletsShot;
            this.sniperBulletsShot = sniperBulletsShot;
            this.shotgunBulletsShot = shotgunBulletsShot;
            this.wave = wave;
            this.towerName = towerName;
            this.towerKills = towerKills;
            this.towerBuild = towerBuild;
            this.towerDead = towerDead;
            this.totalBullets = totalBullets;
            this.totalTowerBuild = totalTowerBuild;
            this.totalTowerDead = totalTowerDead;
            this.totalTowerKills = totalTowerKills;
            this.totalEnemyDead = totalEnemyDead;
            this.totalPlayerKills = totalPlayerKills;
        }

        public void Draw(SpriteBatch spriteBatch, int number)
        {
            string text = "" + (number + 1) + "   " + highscoreName + "   " + score;
            spriteBatch.DrawString(font, text, new Vector2(Constant.width / 2 - font.MeasureString(text).X / 2, 200 + ((font.MeasureString(text).Y) + 10) * number), Color.Gold, 0, Vector2.Zero, 1, SpriteEffects.None, 0.3f);
        }

        public virtual void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Stat");
        }
    }
}
