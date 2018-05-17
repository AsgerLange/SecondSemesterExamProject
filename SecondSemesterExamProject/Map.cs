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
            GameObject rock;
            rock = GameObjectDirector.Instance.Construct(new Vector2(rnd.Next(100, 200), 100), rnd.Next(50, 150), rnd.Next(0, 361));
            rock.LoadContent(GameWorld.Instance.Content);
            GameWorld.Instance.GameObjects.Add(rock);

            rock = GameObjectDirector.Instance.Construct(new Vector2(rnd.Next(150, 250), rnd.Next(200, 300)), rnd.Next(50, 150), rnd.Next(0, 361));
            rock.LoadContent(GameWorld.Instance.Content);
            GameWorld.Instance.GameObjects.Add(rock);

            rock = GameObjectDirector.Instance.Construct(new Vector2(rnd.Next(700, 750), rnd.Next(80, 190)), rnd.Next(50, 150), rnd.Next(0, 361));
            rock.LoadContent(GameWorld.Instance.Content);
            GameWorld.Instance.GameObjects.Add(rock);

            rock = GameObjectDirector.Instance.Construct(new Vector2(rnd.Next(600, 800), rnd.Next(450, 600)), rnd.Next(50, 150), rnd.Next(0, 361));
            rock.LoadContent(GameWorld.Instance.Content);
            GameWorld.Instance.GameObjects.Add(rock);
        }

        /// <summary>
        /// places the hq
        /// </summary>
        public void PlaceHQ()
        {
            GameObject hq;
            hq = new GameObject();
            hq.Transform.Position = new Vector2(20, 20);
            hq.AddComponent(new SpriteRenderer(hq, Constant.HQSpriteSheet, 0.8f));
            hq.AddComponent(new Animator(hq));
            hq.AddComponent(new HQ(hq, Constant.HQFireRate,Constant.HQHealth,Constant.HQAttackRange));
            hq.AddComponent(new Collider(hq, Alignment.Friendly));
            GameWorld.Instance.GameObjects.Add(hq);
        }
    }
}
