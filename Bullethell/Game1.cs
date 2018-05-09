using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Bullethell
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D rocket;
        Texture2D space1;
        Texture2D space2;
        Rectangle rocketRect;
        Vector2 moveDir;
        Vector2 position;
        Vector2 scale;
        Vector2 offset;
        Color color;
        float attackTimer;
        float spawnTimer;
        Background background = new Background();

        Texture2D gameOver;

        Player player;

        Texture2D enemy;
        List<Enemy> enemies;

        Random randomizer = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 900;
            graphics.PreferredBackBufferWidth = 1400;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            IsMouseVisible = true;
            position = new Vector2(100, 100);
            moveDir = Vector2.Zero;
            scale = new Vector2(0.10f, 0.10f);

            color = Color.White;
            offset = (rocket.Bounds.Size.ToVector2() / 2.0f);
            rocketRect = new Rectangle((position - offset).ToPoint(), (rocket.Bounds.Size.ToVector2() * scale).ToPoint());
            attackTimer = 0;
            background.Initialize(new Vector2(-1, 0), space1, space2, new Vector2(0, 0), 500);
            player = new Player(TextureLibrary.GetTexture("Rocket"), new Vector2(100, 100), 3000, new Vector2(0.1f, 0.1f), 0, Color.White, 100, 0.5f);
            UserInterface.AddButton(TextureLibrary.GetTexture("Button"), "Continue", Vector2.One, Vector2.One, Window.ClientBounds.Size.ToVector2());
            UserInterface.AddButton(TextureLibrary.GetTexture("Button"), "Exit", Vector2.One, Vector2.One, Window.ClientBounds.Size.ToVector2());


            spawnTimer = randomizer.Next(1, 5);

            enemies = new List<Enemy>();

            enemies.Add(new Enemy(enemy, new Vector2(randomizer.Next(750, 1450), randomizer.Next(50, 850)), 60, new Vector2(1, 1), 0, color, 100, 50, randomizer.Next(2, 5)));
            enemies.Add(new Enemy(enemy, new Vector2(randomizer.Next(750, 1450), randomizer.Next(50, 850)), 60, new Vector2(1, 1), 0, color, 100, 50, randomizer.Next(2, 5)));
            enemies.Add(new Enemy(enemy, new Vector2(randomizer.Next(750, 1450), randomizer.Next(50, 850)), 60, new Vector2(1, 1), 0, color, 100, 50, randomizer.Next(2, 5)));
            enemies.Add(new Enemy(enemy, new Vector2(randomizer.Next(750, 1450), randomizer.Next(50, 420)), 60, new Vector2(1, 1), 0, color, 100, 50, randomizer.Next(2, 5)));
            enemies.Add(new Enemy(enemy, new Vector2(randomizer.Next(750, 1450), randomizer.Next(50, 850)), 60, new Vector2(1, 1), 0, color, 100, 50, randomizer.Next(2, 5)));
            enemies.Add(new Enemy(enemy, new Vector2(randomizer.Next(750, 1450), randomizer.Next(50, 850)), 60, new Vector2(1, 1), 0, color, 100, 50, randomizer.Next(2, 5)));

        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            TextureLibrary.LoadTexture(Content, "Rocket");
            TextureLibrary.LoadTexture(Content, "Space1");
            TextureLibrary.LoadTexture(Content, "Space2");
            TextureLibrary.LoadTexture(Content, "bullet");
            TextureLibrary.LoadTexture(Content, "Enemy");
            TextureLibrary.LoadTexture(Content, "gameover");
            TextureLibrary.LoadTexture(Content, "Button");
            UserInterface.LoadSpriteFont(Content, "File");  

            rocket = Content.Load<Texture2D>("Rocket");
            space1 = Content.Load<Texture2D>("Space1");
            space2 = Content.Load<Texture2D>("Space2");
            enemy = Content.Load<Texture2D>("Enemy");
            gameOver = Content.Load<Texture2D>("gameover");

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();
            bool exit = UserInterface.Update(keyboardState, mouseState);
            if (exit)
            {
                Exit();
            }
            if (!UserInterface.GetPause())
            {
                player.Update(deltaTime, keyboardState, mouseState, Window.ClientBounds.Size);
            }


            if(enemies.Count < 6)
            {
                Spawn(deltaTime);
            }
            
            player.Update(deltaTime, Keyboard.GetState(), Mouse.GetState(), Window.ClientBounds.Size);

            attackTimer += deltaTime;

            BulletManager.Update(deltaTime, player, enemies);
            background.Update(deltaTime);       

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update(deltaTime, 480, player);
                if (enemies[i].GetIsAlive() == false)
                {
                    enemies.Remove(enemies[i]);
                    player.score++;
                }
                
            }
            color = Color.White;
            
            base.Update(gameTime);
        }

        public void Spawn(float deltaTime)
        {
            if (spawnTimer > 0)
            {
                spawnTimer += -deltaTime;
            }
            else
            {
                spawnTimer = randomizer.Next(1, 3);
                enemies.Add(new Enemy(enemy, new Vector2(randomizer.Next(750, 1450), randomizer.Next(50, 900)), 60, new Vector2(1, 1), 0, color, 100, 50, randomizer.Next(2, 5)));

            }
            if (player.GetIsAlive() == false)
            {
                spawnTimer = 1;
            }
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            player.Draw(spriteBatch);

            BulletManager.Draw(spriteBatch);
            UserInterface.Draw(spriteBatch, player);

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
