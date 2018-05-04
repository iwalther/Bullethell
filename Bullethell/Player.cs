using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Bullethell
{
    class Player
    {
        public static Vector2 centerPosition { get; set; }
        Texture2D texture;
        Rectangle rectangle;
        Vector2 moveDir;
        Vector2 position;
        Vector2 scale;
        Vector2 offset;
        Color color;
        float rotation;
        float speed;
        float attackSpeed;
        float attackTimer;
        float health;
        bool alive = true;

        public Player(Texture2D playerTexture, Vector2 playerStartPos, float playerSpeed, Vector2 playerScale, float playerRotation, Color playerColor, float playerHealth, float playerAttackSpeed)
        {
            texture = playerTexture;
            position = playerStartPos;
            speed = playerSpeed;
            moveDir = Vector2.Zero;
            scale = playerScale;
            offset = playerTexture.Bounds.Size.ToVector2() * 0.5f;
            rectangle = new Rectangle((playerStartPos - offset * scale).ToPoint(), (playerTexture.Bounds.Size.ToVector2() * scale).ToPoint());
            color = playerColor;
            rotation = playerRotation;
            health = playerHealth;
            attackSpeed = playerAttackSpeed;
            attackTimer = 0;
        }

        public void Update(float deltaTime, KeyboardState keyboardState, MouseState mouseState, Point windowSize)
        {
            centerPosition = rectangle.Center.ToVector2();

            if (alive)
            {
                //Player Movement

                attackTimer += deltaTime;
                if (attackTimer <= attackSpeed)
                {
                    attackTimer += deltaTime;
                }

                if (mouseState.LeftButton == ButtonState.Pressed && attackTimer >= attackSpeed)
                {
                    Vector2 bulletDir = mouseState.Position.ToVector2() - position;
                    BulletManager.AddBullet(TextureLibrary.GetTexture("Red"), position, bulletDir, 400, new Vector2(0.2f, 0.2f), Bullet.Owner.Player, color);
                    attackTimer = 0;
                }
            }
            else
            {
                color = Color.Black;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, rotation, offset, scale, SpriteEffects.None, 0);
        }

        public void ChangeHealth(float healthMod)
        {
            health += healthMod;
            if (health <= 0)
            {
                alive = false;
            }
        }

        public Rectangle GetRectangle()
        {
            return rectangle;
        }

    }
}
