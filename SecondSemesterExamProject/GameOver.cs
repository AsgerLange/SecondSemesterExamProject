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


        private float p1StatsPosX = 500;
        private Color p1StatsColor = Color.CornflowerBlue;

        private float p2StatsPosX = 775;
        private Color p2StatsColor = Color.YellowGreen;

        private Color statsColor = Color.Gold;
        private float statsPosX = 225;
        private int playerAmount;

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

            spriteBatch.DrawString(titleFont, "GameOver", new Vector2(Constant.width/4, 2), Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None,
                0.1f);
        }
        private void DrawGameRecap(SpriteBatch spriteBatch)
        {
            DrawEnemiesKilled(spriteBatch);

            foreach (Vehicle vehicle in GameWorld.Instance.Vehicles)
            {
                DrawWeaponsFired(spriteBatch, vehicle);
                DrawTowersCreated(spriteBatch, vehicle);

            }
            DrawBulletsCreated(spriteBatch);
        }


        private void DrawEnemiesKilled(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Enemies killed: ", new Vector2(statsPosX, 120), statsColor);

            spriteBatch.DrawString(font, "Basic Enemies killed: " + Stats.BasicEnemyKilled,
                new Vector2(statsPosX, 140), Color.Gold);

            spriteBatch.DrawString(font, "Elite Enemies killed: " + Stats.BasicEliteEnemyKilled, new Vector2(statsPosX, 160), statsColor);

            spriteBatch.DrawString(font, "Spitter Enemies killed: " + Stats.SpitterKilled, new Vector2(statsPosX, 180), statsColor);


        }

        private void DrawWeaponsFired(SpriteBatch spriteBatch, Vehicle vehicle)
        {

            if (vehicle.Control == Controls.WASD)
            {
                spriteBatch.DrawString(font, "Player1 Weapons Fired: ", new Vector2(p1StatsPosX, 120), Color.Gold);
                spriteBatch.DrawString(font, "Basic Weapons Fired: " + vehicle.Stats.BasicWeaponFired, new Vector2(p1StatsPosX, 140), p1StatsColor);
                spriteBatch.DrawString(font, "Snipers Fired: " + vehicle.Stats.SniperFired, new Vector2(p1StatsPosX, 160), p1StatsColor);
                spriteBatch.DrawString(font, "Shotguns Fired: " + vehicle.Stats.ShotgunFired, new Vector2(p1StatsPosX, 180), p1StatsColor);
                spriteBatch.DrawString(font, "MachineGuns Fired: " + vehicle.Stats.MachinegunFired, new Vector2(p1StatsPosX, 200), p1StatsColor);

            }
            else if (vehicle.Control == Controls.UDLR)
            {
                spriteBatch.DrawString(font, "Player2 Weapons Fired: ", new Vector2(p2StatsPosX, 120), statsColor);
                spriteBatch.DrawString(font, "Basic Weapons Fired: " + vehicle.Stats.BasicWeaponFired, new Vector2(p2StatsPosX, 140), p2StatsColor);
                spriteBatch.DrawString(font, "Snipers Fired: " + vehicle.Stats.SniperFired, new Vector2(p2StatsPosX, 160), p2StatsColor);
                spriteBatch.DrawString(font, "Shotguns Fired: " + vehicle.Stats.ShotgunFired, new Vector2(p2StatsPosX, 180), p2StatsColor);
                spriteBatch.DrawString(font, "MachineGuns Fired: " + vehicle.Stats.MachinegunFired, new Vector2(p2StatsPosX, 200), p2StatsColor);

            }
        }

        private void DrawBulletsCreated(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Bullets Created: ", new Vector2(statsPosX, 240), statsColor);

            spriteBatch.DrawString(font, "Basic bullets fired: " + Stats.BasicBulletCounter
                , new Vector2(statsPosX, 260), statsColor);
            spriteBatch.DrawString(font, "Bigger Bullets fired: " + Stats.BiggerBulletCounter
                , new Vector2(statsPosX, 280), statsColor);
            spriteBatch.DrawString(font, "Sniper bullets fired: " + Stats.SniperBulletCounter,
                new Vector2(statsPosX, 300), statsColor);
            spriteBatch.DrawString(font, "Shotgun pellets fired: " + Stats.ShotgunPelletsCounter,
                new Vector2(statsPosX, 320), statsColor);
            //spriteBatch.DrawString(font, "Spitter bullets fired: " + Stats.SpitterBulletCounter,
            //    new Vector2(statsPosX, 340), statsColor);

            spriteBatch.DrawString(font, "Total bullets missed: " + Stats.BulletsMissed,
               new Vector2(statsPosX, 360), statsColor);
            spriteBatch.DrawString(font, "% of Bullets missed: " + Stats.CalculateAccuracy()+"%",
              new Vector2(statsPosX, 380), statsColor);


        }
        private void DrawTowersCreated(SpriteBatch spriteBatch, Vehicle vehicle)
        {
            if (vehicle.Control == Controls.WASD)
            {
                spriteBatch.DrawString(font, "Player1 Towers Built: ", new Vector2(p1StatsPosX, 240), Color.Gold);

                spriteBatch.DrawString(font, "Basic towers built: " + vehicle.Stats.BasicTowerBuilt
                    , new Vector2(p1StatsPosX, 260), p1StatsColor);
                spriteBatch.DrawString(font, "Sniper towers built:: " + vehicle.Stats.SniperTowerBuilt
                    , new Vector2(p1StatsPosX, 280), p1StatsColor);
                spriteBatch.DrawString(font, "Shotgun Towers built: " + vehicle.Stats.ShotgunTowerbuilt,
                    new Vector2(p1StatsPosX, 300), p1StatsColor);
                spriteBatch.DrawString(font, "Machinegun towers built: " + vehicle.Stats.MachinegunTowerbuilt,
                    new Vector2(p1StatsPosX, 320), p1StatsColor);
            }
            if (vehicle.Control == Controls.UDLR)
            {
                spriteBatch.DrawString(font, "Player2 Towers Built: ", new Vector2(p2StatsPosX, 240), Color.Gold);

                spriteBatch.DrawString(font, "Basic towers built: " + vehicle.Stats.BasicTowerBuilt
    , new Vector2(p2StatsPosX, 260), p2StatsColor);
                spriteBatch.DrawString(font, "Sniper towers built:: " + vehicle.Stats.SniperTowerBuilt
                    , new Vector2(p2StatsPosX, 280), p2StatsColor);
                spriteBatch.DrawString(font, "Shotgun Towers built: " + vehicle.Stats.ShotgunTowerbuilt,
                    new Vector2(p2StatsPosX, 300), p2StatsColor);
                spriteBatch.DrawString(font, "Machinegun towers built: " + vehicle.Stats.MachinegunTowerbuilt,
                    new Vector2(p2StatsPosX, 320), p2StatsColor);
            }
        }
        public void Update()
        {
        }
    }
}
