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

        public int Score { get; set; }

        private float p1StatsPosX = 500;
        private Color p1StatsColor = Color.CornflowerBlue;

        private float p2StatsPosX = 775;
        private Color p2StatsColor = Color.YellowGreen;

        private Color statsColor = Color.Gold;
        private float statsPosX = 225;

        Button ContinueButton;

        public GameOver()
        {
            string text = "Continue";
            Button Continue = new Button(new Vector2(Constant.width / 2 - 100, Constant.hight - 100), Constant.RedButtonTexture, Constant.buttonFont)
            {
                Text = text
            };
            Continue.PenColour = Color.Gold;
            Continue.click += Continue_click;
            ContinueButton = Continue;

        }

        /// <summary>
        /// handles what happens when the player presses continue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Continue_click(object sender, EventArgs e)
        {
            GameWorld.Instance.GetGameState = GameState.Score;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (ContinueButton != null)
            {
                ContinueButton.Draw(spriteBatch);
            }
            DrawGameOver(spriteBatch);
            DrawGameRecap(spriteBatch);

            if (GameWorld.Instance.pvp)
            {
                DrawVictory(spriteBatch);
            }
        }

        public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>(Constant.buttonFont);
            titleFont = content.Load<SpriteFont>(Constant.titleFont);
            if (ContinueButton != null)
            {
                ContinueButton.LoadContent(content);
            }
        }

        private void DrawGameOver(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameWorld.Instance.backGround, GameWorld.Instance.screenSize, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 1);

            spriteBatch.DrawString(titleFont, "GameOver", new Vector2(Constant.width / 4, 2), Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0.1f);
        }

        private void DrawGameRecap(SpriteBatch spriteBatch)
        {
            if (GameWorld.Instance.pvp == false)
            {
                DrawEnemiesKilled(spriteBatch);
            }

            foreach (Vehicle vehicle in GameWorld.Instance.Vehicles)
            {
                DrawWeaponsFired(spriteBatch, vehicle);
                DrawTowersCreated(spriteBatch, vehicle);
                DrawMisc(spriteBatch, vehicle);
                DrawBulletsCreated(spriteBatch, vehicle);
            }
        }

        private void DrawEnemiesKilled(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Enemies killed", new Vector2(statsPosX, 160), statsColor);

            spriteBatch.DrawString(font, "Basic Enemies killed: " + Stats.BasicEnemyKilled,
                new Vector2(statsPosX, 180), Color.Gold);

            spriteBatch.DrawString(font, "Elite Enemies killed: " +
                Stats.BasicEliteEnemyKilled, new Vector2(statsPosX, 200), statsColor);

            spriteBatch.DrawString(font, "Spitters killed: " + Stats.SpitterKilled, new Vector2(statsPosX, 220), statsColor);

            spriteBatch.DrawString(font, "Swarmers killed: " + Stats.SwarmerKilled, new Vector2(statsPosX, 240), statsColor);

            spriteBatch.DrawString(font, "Siegebreakers killed: "
                + Stats.SiegeBreakerKilled, new Vector2(statsPosX, 260), statsColor);

            //spitter bullets
            spriteBatch.DrawString(font, "Spitter bullets fired: " + Stats.SpitterBulletCounter,
            new Vector2(statsPosX, 300), statsColor);
        }

        private void DrawVictory(SpriteBatch spriteBatch)
        {
            foreach (Vehicle vehicle in GameWorld.Instance.Vehicles)
            {
                if (vehicle.Control == Controls.WASD)
                {
                    spriteBatch.DrawString(font, "Green score: " + vehicle.Stats.PlayerDeathAmmount, new Vector2(statsPosX, 180), Color.YellowGreen);

                    if (vehicle.Stats.PlayerDeathAmmount==Constant.maxDeaths)
                    {
                        spriteBatch.DrawString(font, "Green VICTORY!", new Vector2(statsPosX, 120), Color.YellowGreen);

                    }
                }
                else
                {
                    spriteBatch.DrawString(font, "Blue score: " + vehicle.Stats.PlayerDeathAmmount, new Vector2(statsPosX, 160), Color.CornflowerBlue);

                    if (vehicle.Stats.PlayerDeathAmmount == Constant.maxDeaths)
                    {
                        spriteBatch.DrawString(font, "Blue VICTORY! ", new Vector2(statsPosX, 120), Color.CornflowerBlue);

                    }
                }

            }

        }

        private void DrawWeaponsFired(SpriteBatch spriteBatch, Vehicle vehicle)
        {
            if (vehicle.Control == Controls.WASD)
            {
                spriteBatch.DrawString(font, "Player1 weapons fired ", new Vector2(p1StatsPosX, 120), Color.Gold);
                spriteBatch.DrawString(font, "Basic weapon fired: " + vehicle.Stats.BasicWeaponFired, new Vector2(p1StatsPosX, 140), p1StatsColor);
                spriteBatch.DrawString(font, "Snipers fired: " + vehicle.Stats.SniperFired, new Vector2(p1StatsPosX, 160), p1StatsColor);
                spriteBatch.DrawString(font, "Shotguns fired: " + vehicle.Stats.ShotgunFired, new Vector2(p1StatsPosX, 180), p1StatsColor);
                spriteBatch.DrawString(font, "Machineguns fired: " + vehicle.Stats.MachinegunFired, new Vector2(p1StatsPosX, 200), p1StatsColor);

            }
            else if (vehicle.Control == Controls.UDLR)
            {
                spriteBatch.DrawString(font, "Player2 weapons fired ", new Vector2(p2StatsPosX, 120), statsColor);
                spriteBatch.DrawString(font, "Basic weapon fired: " + vehicle.Stats.BasicWeaponFired, new Vector2(p2StatsPosX, 140), p2StatsColor);
                spriteBatch.DrawString(font, "Snipers fired: " + vehicle.Stats.SniperFired, new Vector2(p2StatsPosX, 160), p2StatsColor);
                spriteBatch.DrawString(font, "Shotguns fired: " + vehicle.Stats.ShotgunFired, new Vector2(p2StatsPosX, 180), p2StatsColor);
                spriteBatch.DrawString(font, "Machineguns fired: " + vehicle.Stats.MachinegunFired, new Vector2(p2StatsPosX, 200), p2StatsColor);

            }
        }
        private void DrawMisc(SpriteBatch spriteBatch, Vehicle vehicle)
        {
            if (vehicle.Control == Controls.WASD)
            {
                spriteBatch.DrawString(font, "Miscellaneous", new Vector2(p1StatsPosX, 540), statsColor);
                spriteBatch.DrawString(font, "Player 1 gold earned: "
                    + vehicle.Stats.TotalAmountOfGold, new Vector2(p1StatsPosX, 560), p1StatsColor);
                spriteBatch.DrawString(font, "Player 1 deaths: "
                    + vehicle.Stats.PlayerDeathAmmount, new Vector2(p1StatsPosX, 580), p1StatsColor);

            }
            else if (vehicle.Control == Controls.UDLR)
            {
                spriteBatch.DrawString(font, "Miscellaneous"
                    , new Vector2(p2StatsPosX, 540), statsColor);
                spriteBatch.DrawString(font, "Player 2 gold earned: "
                    + vehicle.Stats.TotalAmountOfGold, new Vector2(p2StatsPosX, 560), p2StatsColor);
                spriteBatch.DrawString(font, "Player 2 deaths: "
            + vehicle.Stats.PlayerDeathAmmount, new Vector2(p2StatsPosX, 580), p2StatsColor);
            }
            if (GameWorld.Instance.pvp == false)
            {
                spriteBatch.DrawString(font, "Wave Number: " + GameWorld.Instance.GetSpawn.Wave,
                 new Vector2(statsPosX, 120), statsColor);

            }
        }
        private void DrawBulletsCreated(SpriteBatch spriteBatch, Vehicle vehicle)
        {

            if (vehicle.Control == Controls.WASD)
            {
                spriteBatch.DrawString(font, "Bullets created ", new Vector2(p1StatsPosX, 240), statsColor);

                spriteBatch.DrawString(font, "Basic bullets fired: " + vehicle.Stats.BasicBulletCounter
                    , new Vector2(p1StatsPosX, 260), p1StatsColor);
                spriteBatch.DrawString(font, "Bigger bullets fired: " + vehicle.Stats.BiggerBulletCounter
                    , new Vector2(p1StatsPosX, 280), p1StatsColor);
                spriteBatch.DrawString(font, "Sniper bullets fired: " + vehicle.Stats.SniperBulletCounter,
                    new Vector2(p1StatsPosX, 300), p1StatsColor);
                spriteBatch.DrawString(font, "Shotgun pellets fired: " + vehicle.Stats.ShotgunPelletsCounter,
                    new Vector2(p1StatsPosX, 320), p1StatsColor);


                spriteBatch.DrawString(font, "Total bullets missed: " + vehicle.Stats.BulletsMissed,
                   new Vector2(p1StatsPosX, 360), p1StatsColor);
                spriteBatch.DrawString(font, "Total bullet accuracy: " + vehicle.Stats.CalculateAccuracy() + "%",
                  new Vector2(p1StatsPosX, 380), p1StatsColor);

            }
            else if (vehicle.Control == Controls.UDLR)
            {

                spriteBatch.DrawString(font, "Bullets created ", new Vector2(p2StatsPosX, 240), statsColor);

                spriteBatch.DrawString(font, "Basic bullets fired: " + vehicle.Stats.BasicBulletCounter
                    , new Vector2(p2StatsPosX, 260), p2StatsColor);
                spriteBatch.DrawString(font, "Bigger Bullets fired: " + vehicle.Stats.BiggerBulletCounter
                    , new Vector2(p2StatsPosX, 280), p2StatsColor);
                spriteBatch.DrawString(font, "Sniper bullets fired: " + vehicle.Stats.SniperBulletCounter,
                    new Vector2(p2StatsPosX, 300), p2StatsColor);
                spriteBatch.DrawString(font, "Shotgun pellets fired: " + vehicle.Stats.ShotgunPelletsCounter,
                    new Vector2(p2StatsPosX, 320), p2StatsColor);


                spriteBatch.DrawString(font, "Total bullets missed: " + vehicle.Stats.BulletsMissed,
                   new Vector2(p2StatsPosX, 360), p2StatsColor);
                spriteBatch.DrawString(font, "Total bullet accuracy: " + vehicle.Stats.CalculateAccuracy() + "%",
                  new Vector2(p2StatsPosX, 380), p2StatsColor);
            }

        }
        private void DrawTowersCreated(SpriteBatch spriteBatch, Vehicle vehicle)
        {
            if (vehicle.Control == Controls.WASD)
            {
                spriteBatch.DrawString(font, "Player1 Towers built", new Vector2(p1StatsPosX, 420), Color.Gold);

                spriteBatch.DrawString(font, "Basic towers built: " + vehicle.Stats.BasicTowerBuilt
                    , new Vector2(p1StatsPosX, 440), p1StatsColor);
                spriteBatch.DrawString(font, "Sniper towers built:: " + vehicle.Stats.SniperTowerBuilt
                    , new Vector2(p1StatsPosX, 460), p1StatsColor);
                spriteBatch.DrawString(font, "Shotgun Towers built: " + vehicle.Stats.ShotgunTowerbuilt,
                    new Vector2(p1StatsPosX, 480), p1StatsColor);
                spriteBatch.DrawString(font, "Machinegun towers built: " + vehicle.Stats.MachinegunTowerbuilt,
                    new Vector2(p1StatsPosX, 500), p1StatsColor);
            }
            if (vehicle.Control == Controls.UDLR)
            {
                spriteBatch.DrawString(font, "Player2 Towers built", new Vector2(p2StatsPosX, 420), Color.Gold);

                spriteBatch.DrawString(font, "Basic towers built: " + vehicle.Stats.BasicTowerBuilt
    , new Vector2(p2StatsPosX, 440), p2StatsColor);
                spriteBatch.DrawString(font, "Sniper towers built:: " + vehicle.Stats.SniperTowerBuilt
                    , new Vector2(p2StatsPosX, 460), p2StatsColor);
                spriteBatch.DrawString(font, "Shotgun Towers built: " + vehicle.Stats.ShotgunTowerbuilt,
                    new Vector2(p2StatsPosX, 480), p2StatsColor);
                spriteBatch.DrawString(font, "Machinegun towers built: " + vehicle.Stats.MachinegunTowerbuilt,
                    new Vector2(p2StatsPosX, 500), p2StatsColor);
            }

        }
        public void Update()
        {
            if (ContinueButton != null)
            {
                ContinueButton.Update();
            }
            int cal = 0;
            foreach (Vehicle VH in GameWorld.Instance.Vehicles)
            {
                cal += VH.Stats.TotalAmountOfGold;
            }

            if (GameWorld.Instance.pvp == false)
            {

                int waveModifier = GameWorld.Instance.GetSpawn.Wave;
                if (waveModifier > 10)
                {
                    waveModifier = 10;
                }
                Score = cal * waveModifier + Stats.BasicEnemyKilled * 1 + Stats.SwarmerKilled * 1 + Stats.SpitterKilled * 5 + Stats.BasicEliteEnemyKilled * 10 + Stats.SiegeBreakerKilled * 50;
            }
        }
    }
}
