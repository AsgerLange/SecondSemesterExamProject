using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Map
    {
        Random rnd = new Random();

        public Map()
        {

            PlaceHQ();


            PlaceRocks();
        }

        /// <summary>
        /// places rocks on the map
        /// </summary>
        public void PlaceRocks()
        {
            if (GameWorld.Instance.pvp == false)
            {
                GameObject terrain;

                terrain = GameObjectDirector.Instance.Construct(new Vector2(550, 150), 115, rnd.Next(0, 361));
                terrain.LoadContent(GameWorld.Instance.Content);
                GameWorld.Instance.GameObjects.Add(terrain);

                terrain = GameObjectDirector.Instance.Construct(new Vector2(777, 164), 65, rnd.Next(0, 361));
                terrain.LoadContent(GameWorld.Instance.Content);
                GameWorld.Instance.GameObjects.Add(terrain);
                terrain = GameObjectDirector.Instance.Construct(new Vector2(785, 185), 60, rnd.Next(0, 361));
                terrain.LoadContent(GameWorld.Instance.Content);
                GameWorld.Instance.GameObjects.Add(terrain);
                terrain = GameObjectDirector.Instance.Construct(new Vector2(750, 170), 70, rnd.Next(0, 361));
                terrain.LoadContent(GameWorld.Instance.Content);
                GameWorld.Instance.GameObjects.Add(terrain);

                terrain = GameObjectDirector.Instance.Construct(new Vector2(975, 227), 60, rnd.Next(0, 361));
                terrain.LoadContent(GameWorld.Instance.Content);
                GameWorld.Instance.GameObjects.Add(terrain);

                terrain = GameObjectDirector.Instance.Construct(new Vector2(630, 600), 140, rnd.Next(0, 361));
                terrain.LoadContent(GameWorld.Instance.Content);
                GameWorld.Instance.GameObjects.Add(terrain);

                terrain = GameObjectDirector.Instance.Construct(new Vector2(320, 470), 60, rnd.Next(0, 361));
                terrain.LoadContent(GameWorld.Instance.Content);
                GameWorld.Instance.GameObjects.Add(terrain);
                terrain = GameObjectDirector.Instance.Construct(new Vector2(278, 437), 60, rnd.Next(0, 361));
                terrain.LoadContent(GameWorld.Instance.Content);
                GameWorld.Instance.GameObjects.Add(terrain);
                terrain = GameObjectDirector.Instance.Construct(new Vector2(285, 415), 65, rnd.Next(0, 361));
                terrain.LoadContent(GameWorld.Instance.Content);
                GameWorld.Instance.GameObjects.Add(terrain);
                terrain = GameObjectDirector.Instance.Construct(new Vector2(305, 450), 80, rnd.Next(0, 361));
                terrain.LoadContent(GameWorld.Instance.Content);
                GameWorld.Instance.GameObjects.Add(terrain);

                terrain = GameObjectDirector.Instance.Construct(new Vector2(905, 450), 95, rnd.Next(0, 361));
                terrain.LoadContent(GameWorld.Instance.Content);
                GameWorld.Instance.GameObjects.Add(terrain);

                terrain = GameObjectDirector.Instance.Construct(new Vector2(125, 145), 100, rnd.Next(0, 361));
                terrain.LoadContent(GameWorld.Instance.Content);
                GameWorld.Instance.GameObjects.Add(terrain);

                terrain = GameObjectDirector.Instance.Construct(new Vector2(825, 575), 70, rnd.Next(0, 361));
                terrain.LoadContent(GameWorld.Instance.Content);
                GameWorld.Instance.GameObjects.Add(terrain);
            }
            else
            {
                for (int i = 0; i < 50; i++)
                {
                    GameObject terrain;
                    terrain = GameObjectDirector.Instance.Construct(new Vector2(GameWorld.Instance.Rnd.Next(Constant.width), GameWorld.Instance.Rnd.Next(Constant.hight)), GameWorld.Instance.Rnd.Next(10, 115), rnd.Next(0, 361));
                    terrain.LoadContent(GameWorld.Instance.Content);
                    GameWorld.Instance.GameObjects.Add(terrain);
                }
            }

        }

        /// <summary>
        /// places the hq
        /// </summary>
        public void PlaceHQ()
        {
            if (GameWorld.Instance.pvp == false)
            {
                GameObject hq;
                hq = new GameObject();
                hq.Transform.Position = new Vector2(Constant.width / 2, Constant.hight / 2);
                hq.AddComponent(new SpriteRenderer(hq, Constant.HQSpriteSheet, 0.4f));
                hq.AddComponent(new Animator(hq));
                hq.AddComponent(new HQ(hq));
                hq.AddComponent(new Collider(hq, Alignment.Friendly));
                hq.LoadContent(GameWorld.Instance.Content);
                GameWorld.Instance.GameObjects.Add(hq);
            }
        }
    }
}
