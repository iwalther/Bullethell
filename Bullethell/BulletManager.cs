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
    static class BulletManager
    {
        static List<Bullet> bullets = new List<Bullet>();

        public static void AddBullet (Texture2D texture, Vector2 startPosition, Vector2 dir, float speed, Vector2 scale, Bullet.Owner owner, Color color, float bulletRotation)
        {
            bullets.Add(new Bullet(texture, startPosition, dir, speed, scale, owner, color, bulletRotation));
        }


        public static void Update(float deltaTime, Player player, List<Enemy> enemies)
        {
          
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                if (bullets[i].GetIsAlive())
                {

                    bullets[i].Update(deltaTime);
                    Bullet.Owner owner = bullets[i].GetOwner();
                    float damage = 0;
                    
                    switch (owner)
                    {   
                        case Bullet.Owner.Player:

                            for (int j = 0; j < enemies.Count; j++)
                            {
                                damage = bullets[i].Damage(enemies[j].GetRectangle());
                                enemies[j].ChangeHealth(-damage);
                            }
                            if(bullets[i].AccessPosition.X >= 1500)
                            {
                                bullets.RemoveAt(i);
                            }
                            break;
                        case Bullet.Owner.Enemy:
                            damage = bullets[i].Damage(player.GetRectangle());
                            player.ChangeHealth(-damage);
                            if (bullets[i].AccessPosition.X <= 0)
                            {
                                bullets.RemoveAt(i);
                            }
                            break;
                    }
                }
                else
                {
                    bullets.RemoveAt(i);
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Draw(spriteBatch);
            }
        }

    }

}

