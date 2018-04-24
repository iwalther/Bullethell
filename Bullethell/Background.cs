using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Bullethell
{
    class Background
    {
        Texture2D space1;
        Texture2D space2;
        Vector2 dir;
        Vector2 position;
        Vector2 position2;
        float deltaTime;
        float speed;

        public void Initialize(Vector2 dir, Texture2D bg1, Texture2D bg2, Vector2 position, float speed)
        {
            space1 = bg1;
            space2 = bg2;
            this.dir = dir;
            this.position = position;
            position2 = new Vector2(position.X + bg1.Width, position.Y);
            this.speed = speed;
        }

        public void Update(float deltaTime)
        {
            position += dir * deltaTime * speed;
            position2 += dir * deltaTime * speed;
            Debug.Print(position.ToString());

            if(position2.X < 0)
            {
                position.X = position2.X + space2.Width;
            }

            if (position.X < 0)
            {
                position2.X = position.X + space1.Width;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(space1, position, Color.White);
            spriteBatch.Draw(space2, position2, Color.White);
        }

    }
}
