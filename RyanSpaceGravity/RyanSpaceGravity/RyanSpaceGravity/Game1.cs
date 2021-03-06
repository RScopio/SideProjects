using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RyanSpaceGravity
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MouseState ms;
        KeyboardState ks;

        List<Mass> space;
        Texture2D massImage;

        Vector2 start;
        Vector2 end;
        bool hasLine = false;

        Texture2D t;

        float spawnMass = 5;
        float gravity = 0.5f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 600;
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
            space = new List<Mass>();
            massImage = Content.Load<Texture2D>("CircleMain");

            // create 1x1 texture for line drawing
            t = new Texture2D(GraphicsDevice, 1, 1);
            t.SetData<Color>(
                new Color[] { Color.White });// fill the texture with white
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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

            KeyboardState lastKs = ks;
            MouseState lastMs = ms;
            ks = Keyboard.GetState();
            ms = Mouse.GetState();

            

            if (ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
            {
                //start path
                start = new Vector2(ms.X, ms.Y);
                hasLine = true;
            }

            if (hasLine) end = new Vector2(ms.X, ms.Y);

            if (ms.LeftButton == ButtonState.Released && lastMs.LeftButton == ButtonState.Pressed)
            {
                //end path. determine velocity via path
                end = new Vector2(ms.X, ms.Y);

                space.Add(new Mass(massImage, new Vector2(start.X, start.Y), Color.Yellow, spawnMass));

                space[space.Count - 1].Velocity = new Vector2(end.X - start.X, end.Y - start.Y) / 15f;
                hasLine = false;
            }

            

            for (int i = 0; i < space.Count; i++)
            {
                for (int j = 0; j < space.Count; j++)
                {
                    //distance, force of gravity, angle
                    if (i != j)
                    {
                        float distance = Vector2.Distance(space[i].Position, space[j].Position);
                        double gravityForce = (gravity * space[i].mass * space[j].mass) / Math.Pow(distance, 2);                        
                        //double angle = Math.Atan2(space[j].Y - space[i].Y, space[j].X - space[i].X);
                        //Vector2 force = new Vector2(gravity * (float)Math.Cos(angle), gravity * (float)Math.Sin(angle));
                        Vector2 force = space[j].Position - space[i].Position;
                        force.Normalize();
                        space[i].Acceleration = Vector2.Multiply(force, 1 / space[i].mass);

                        if (space[i].Hitbox.Intersects(space[j].Hitbox))
                        {
                            if (distance < space[i].Radius + space[j].Radius)
                            {
                                //combine mass
                                if (space[i].mass >= space[j].mass)
                                {
                                    space[i].mass += space[j].mass;
                                    //result vector
                                    space[i].Velocity = Vector2.Zero;
                                    space[i].Acceleration = Vector2.Zero;

                                    space.RemoveAt(j);
                                }
                                else
                                {
                                    space[j].mass += space[j].mass;
                                    //result vector
                                    space[j].Velocity = Vector2.Zero;
                                    space[j].Acceleration = Vector2.Zero;

                                    space.RemoveAt(i);
                                }

                                break;
                                

                            }
                        }
                    }
                }
            }

            for (int i = 0; i < space.Count; i++)
            {
                space[i].Update();
            }
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            foreach (Mass m in space)
            {
                m.Draw(spriteBatch);
            }

            


            if (hasLine)
            {
                DrawLine(spriteBatch, start, end);
                spriteBatch.Draw(massImage, start, null, Color.White, 0f, new Vector2(massImage.Width / 2, massImage.Height / 2), 0.005f * spawnMass, SpriteEffects.None, 0f);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);


            sb.Draw(t,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    1), //width of line, change this to make thicker line
                null,
                Color.White, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }
    }
}
