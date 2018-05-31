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
    class GameOver
    {
        private SpriteFont font;
        private SpriteFont titleFont;

        private float statsPosX = Constant.width / 4;
        private Color statsColor = Color.Gold;

        public GameOver()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            DrawGameOver(spriteBatch);
            DrawGameRecap(spriteBatch);
        }
        public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>(Constant.buttonFont);
            titleFont = content.Load<SpriteFont>(Constant.titleFont);

        }
        private void DrawGameOver(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameWorld.Instance.backGround, GameWorld.Instance.screenSize
            , null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 1);

            spriteBatch.DrawString(titleFont, "GameOver", new Vector2(statsPosX, 2), Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None,
                0.1f);
        }
        private void DrawGameRecap(SpriteBatch spriteBatch)
        {
            DrawEnemiesKilled(spriteBatch);
            DrawWeaponsFired(spriteBatch);
            DrawBulletsCreated(spriteBatch);
            DrawTowersCreated(spriteBatch);
        }


        private void DrawEnemiesKilled(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Enemies killed: ", new Vector2(statsPosX, 120), statsColor);

            spriteBatch.DrawString(font, "Basic Enemies killed: " + Stats.BasicEnemyKilled, 
                new Vector2(statsPosX, 140), Color.Gold);

            spriteBatch.DrawString(font, "Elite Enemies killed: " + Stats.BasicEliteEnemyKilled, new Vector2(statsPosX, 160), statsColor);

            spriteBatch.DrawString(font, "Spitter Enemies killed: " + Stats.SpitterKilled, new Vector2(statsPosX, 180), statsColor);


        }

        private void DrawWeaponsFired(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Weapons Fired: ", new Vector2(statsPosX, 220), statsColor);
            spriteBatch.DrawString(font, "Basic Weapons Fired: "+Stats.BasicWeaponFired, new Vector2(statsPosX, 240), statsColor);
            spriteBatch.DrawString(font, "Snipers Fired: " + Stats.SniperFired, new Vector2(statsPosX, 260), statsColor);
            spriteBatch.DrawString(font, "Shotguns Fired: " + Stats.ShotgunFired, new Vector2(statsPosX, 280), statsColor);
            spriteBatch.DrawString(font, "MachineGuns Fired: " + Stats.MachinegunFired, new Vector2(statsPosX, 300), statsColor);
            
        }

        private void DrawBulletsCreated(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Bullets Created: ", new Vector2(statsPosX, 340), statsColor);
            spriteBatch.DrawString(font, "Basic bullets fired: "+Stats.BasicBulletCounter
                , new Vector2(statsPosX, 360), statsColor);
            spriteBatch.DrawString(font, "Bigger Bullets fired: "+Stats.BiggerBulletCounter
                , new Vector2(statsPosX, 380), statsColor);
            spriteBatch.DrawString(font, "Sniper bullets fired: "+Stats.SniperBulletCounter,
                new Vector2(statsPosX, 400), statsColor);
            spriteBatch.DrawString(font, "Shotgun pellets fired: " + Stats.ShotgunPelletsCounter, 
                new Vector2(statsPosX, 420), statsColor);
            spriteBatch.DrawString(font, "Spitter bullets fired: " + Stats.SpitterBulletCounter,
                new Vector2(statsPosX, 440), statsColor);


        }
        private void DrawTowersCreated(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Towers Created: ", new Vector2(statsPosX, 480), Color.Gold);

            spriteBatch.DrawString(font, "Basic towers built: " + Stats.BasicTowerBuilt
                , new Vector2(statsPosX, 500), statsColor);
            spriteBatch.DrawString(font, "Sniper towers built:: " + Stats.SniperTowerBuilt
                , new Vector2(statsPosX, 520), statsColor);
            spriteBatch.DrawString(font, "Shotgun Towers built: " + Stats.ShotgunTowerbuilt,
                new Vector2(statsPosX, 540), statsColor);
            spriteBatch.DrawString(font, "Machinegun towers built: " + Stats.MachinegunTowerbuilt,
                new Vector2(statsPosX, 560), statsColor);
           
        }
        public void Update()
        {
        }
    }
}
