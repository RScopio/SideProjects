using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DeleteMe3D
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;

        BasicEffect effect;

        Matrix projection;
        Matrix view;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // 18 circle points, 1 core point, 19 per face, 38 total

            var verticies = new VertexPosition[38];
            verticies[0] = new VertexPosition(new Vector3(0, -100, 0));
            // x z plane around point 0
            verticies[19] = new VertexPosition(new Vector3(0, 100, 0));
            // x z plane around point 19

            //18 triangles per circle, 36 triangls for sides, 54 triangles total, 162 indexes total
            var indicies = new int[162];

            //var vertices = new[]
            //{
            //    new VertexPositionColor(new Vector3(-100, -100, -100), Color.Red), // 0 Left Top Front
            //    new VertexPositionColor(new Vector3(-100, 100, -100), Color.Green), // 1 Left Bot Front
            //    new VertexPositionColor(new Vector3(-100, 100, 100), Color.Blue), // 2 Left Bot Back
            //    new VertexPositionColor(new Vector3(-100, -100, 100), Color.Yellow), // 3 Left Top Back

            //    new VertexPositionColor(new Vector3(100, -100, 100), Color.DarkRed), // 4 Right Top Back
            //    new VertexPositionColor(new Vector3(100, 100, 100), Color.DarkGreen), // 5 Right Bot Back
            //    new VertexPositionColor(new Vector3(100, 100, -100), Color.DarkBlue), // 6 Right Bot Front
            //    new VertexPositionColor(new Vector3(100, -100, -100), Color.Orange) // 7 Right Top Front
            //};

            //var indicies = new[]
            //{
            //    // a b c
            //    // c d a

            //    //Left 0 1 2 3
            //    0, 1, 2,
            //    2, 3, 0,
            //    //Right 4 5 6 7
            //    4, 5, 6,
            //    6, 7, 4,
            //    //Top 0 3 4 7
            //    0, 3, 4,
            //    4, 7, 0,
            //    //Bot 1 2 5 6
            //    5, 2, 1,
            //    1, 6, 5,
            //    //Front 0 1 6 7
            //    0, 7, 6,
            //    6, 1, 0,
            //    //Back 2 3 4 5
            //    4, 3, 2,
            //    2, 5, 4
            //};

            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), vertices.Length, BufferUsage.None);
            indexBuffer = new IndexBuffer(GraphicsDevice, IndexElementSize.ThirtyTwoBits, indicies.Length, BufferUsage.None);

            float screenScale = 1f;
            projection = Matrix.CreateOrthographic(GraphicsDevice.Viewport.Width * screenScale, GraphicsDevice.Viewport.Height * screenScale, 1, 1200);
            view = Matrix.CreateLookAt(new Vector3(0, 0, 1000), Vector3.Zero, Vector3.Up);

            effect = new BasicEffect(GraphicsDevice)
            {
                VertexColorEnabled = true,
                TextureEnabled = false,
                Projection = projection,
                View = view,
                World = Matrix.Identity
            };

            vertexBuffer.SetData(vertices);
            indexBuffer.SetData(indicies);
        }



        protected override void Update(GameTime gameTime)
        {
            effect.World *= Matrix.CreateRotationZ(0.01f) * Matrix.CreateRotationX(0.02f) * Matrix.CreateRotationY(0.03f);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.SetVertexBuffer(vertexBuffer);
            GraphicsDevice.Indices = indexBuffer;

            GraphicsDevice.RasterizerState = new RasterizerState() { FillMode = FillMode.WireFrame };

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, indexBuffer.IndexCount / 3);
            }

            base.Draw(gameTime);
        }
    }
}
