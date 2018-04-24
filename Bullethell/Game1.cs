using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Bullethell
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D rocket;
        Texture2D space;
        Rectangle rocketRect;
        Vector2 moveDir;
        Vector2 position;
        Vector2 scale;
        Vector2 offset;
        float speed;
        float rotation;
        Color color;
        float attackSpeed;
        float attackTimer;


        List<Bullet> bullets;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            IsMouseVisible = true;
            position = new Vector2(100, 100);
            moveDir = Vector2.Zero;
            speed = 3000;
            rotation = 0;
            scale = new Vector2(0.10f, 0.10f);
            color = Color.White;
            offset = (rocket.Bounds.Size.ToVector2() / 2.0f);
            rocketRect = new Rectangle((position - offset).ToPoint(), (rocket.Bounds.Size.ToVector2() * scale).ToPoint());
            attackSpeed = 1;
            attackTimer = 0;

            bullets = new List<Bullet>();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            TextureLibrary.LoadTexture(Content, "Rocket");
            TextureLibrary.LoadTexture(Content, "Space");
            TextureLibrary.LoadTexture(Content, "bullet");

            rocket = Content.Load<Texture2D>("Rocket");
            space = Content.Load<Texture2D>("Space");
            //bullet = Content.Load<Texture2D>("bullet");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePos = mouseState.Position.ToVector2();
            moveDir = mousePos - position;
            float pixelsToMove = speed * deltaTime;

            attackTimer += deltaTime;

            if (attackTimer >= attackSpeed)
            {

                attackTimer = 0;
                bullets.Add(new Bullet(new Vector2(1, 0), 400, TextureLibrary.GetTexture("bullet"), position));
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update(deltaTime);
            }

            //KeyboardState keyState = Keyboard.GetState();

            //if (keyState.IsKeyDown(Keys.Right))
            //{
            //    moveDir.X = 1;
            //}
            //if (keyState.IsKeyDown(Keys.Left))
            //{
            //    moveDir.X = -1;
            //}
            //if (keyState.IsKeyDown(Keys.Up))
            //{
            //    moveDir.Y = -1;
            //}
            //if (keyState.IsKeyDown(Keys.Down))
            //{
            //    moveDir.Y = 1;
            //}

            if (moveDir != Vector2.Zero)
            {
                moveDir.Normalize();
                rocketRect.Location += (moveDir * speed * deltaTime).ToPoint();
            }

            if (moveDir != Vector2.Zero)
            {
                moveDir.Normalize();

                //rotation = (float)Math.Atan2(moveDir.Y, moveDir.X);
                if (Vector2.Distance(position, mousePos) < pixelsToMove)
                {
                    position = mousePos;
                }
                else
                {
                    position += moveDir * pixelsToMove;
                }
                rocketRect.Location = (position - offset).ToPoint();
            }

            if (position.X <= 0)
            {
                position.X = 0;
            }
            if(position.X >= 800)
            {
                position.X = 800;
            }
            if(position.Y >= 480)
            {
                position.Y = 480;
            }
            if (position.Y <= 0)
            {
                position.Y = 0;
            }
           
            color = Color.White;


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(space, GraphicsDevice.Viewport.Bounds, Color.White);
            spriteBatch.Draw(rocket, position, null, color, rotation, offset, scale, SpriteEffects.None, 1);
           for(int i =0; i< bullets.Count;i++)
            {
                bullets[i].Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
