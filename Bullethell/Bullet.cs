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
    class Bullet
    {
        public enum Owner {Player, Enemy};
        Owner owner;
        Texture2D texture;
        Rectangle rectangle;
        Vector2 dir;
        Vector2 position;
        Vector2 scale;
        Vector2 offset;
        Color color;
        float speed;
        float rotation;
        float damage;
        bool alive;

        public Vector2 AccessPosition { get => position; }

        public Bullet(Texture2D bulletTexture, Vector2 startPosition, Vector2 bulletDir, float bulletSpeed, Vector2 bulletScale, Owner bulletOwner, Color bulletColor, float bulletRotation)
        {
            texture = bulletTexture;
            position = startPosition;
            speed = bulletSpeed;
            dir = bulletDir;
            dir.Normalize();
            scale = bulletScale;
            offset = (bulletTexture.Bounds.Size.ToVector2() * 0.5f);
            rectangle = new Rectangle((startPosition - offset * scale).ToPoint(), (bulletTexture.Bounds.Size.ToVector2() * scale).ToPoint());
            rotation = bulletRotation;
            color = bulletColor;
            damage = 100;
            alive = true;
            owner = bulletOwner;

        }

        public void Update(float deltaTime)
        {
            position += dir * speed * deltaTime;
            rectangle.Location = position.ToPoint();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(texture, position, null, Color.White, rotation, offset, scale, SpriteEffects.None, 1);
        }

        public float Damage(Rectangle otherRectangle)
        {
            float damageToDeal = 0;
            if (rectangle.Intersects(otherRectangle))
            {
                damageToDeal = damage;
                
                alive = false;
                
                
            }

            return damageToDeal;
        }
        
        public bool GetIsAlive()
        {
            return alive;
        }

        public Owner GetOwner()
        {
            return owner;
        }

        //public void DestroyBullet(List<Bullet> bullets)
        //{
        //    if( position.X > 800)
        //    {
        //        bullets.Remove(this);
        //        //Debug.Print("Bullet destroyed");
        //    }
        //    else if (position.X <= 1)
        //    {
        //        bullets.Remove(this);
        //        Debug.Print("Bullet destroyed");
        //    }
        //}
    }
}
