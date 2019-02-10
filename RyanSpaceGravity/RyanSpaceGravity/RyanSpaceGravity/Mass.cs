using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RyanSpaceGravity
{
    public class Mass
    {
        Texture2D image;
        Vector2 position;
        Color tint;

        public float mass;

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
        public float X
        {
            get
            {
                return position.X;
            }
            set
            {
                position.X = value;
            }
        }
        public float Y
        {
            get
            {
                return position.Y;
            }
            set
            {
                position.Y = value;
            }
        }

        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }

        public float Rotation {get; set;}
        public Vector2 Origin { get; set; }
        public float Scale { get; set; }
        public SpriteEffects SpriteEffect { get; set; }
        public Rectangle Hitbox { get; set; }

        public float Radius
        {
            get
            {
                return Hitbox.Width / 2;
            }
        }

        public Mass(Texture2D imgin, Vector2 posin, Color tintin, float massin)
        {
            image = imgin;
            position = posin;
            tint = tintin;

            mass = massin;
            

            Rotation = 0;
            Scale = massin * 0.005f;
            SpriteEffect = SpriteEffects.None;

            Velocity = Vector2.Zero;
            Acceleration = Vector2.Zero;

            Origin = new Vector2( (image.Width ) / 2, (image.Height) /2);
        }

        public void Update()
        {
            
            Scale = mass * 0.005f;
            Velocity += Acceleration;
            position += Velocity;
            Hitbox = new Rectangle((int)(position.X - (image.Width * Scale) / 2), (int)(position.Y - (image.Height * Scale) / 2), (int)(image.Width * Scale), (int)(image.Height * Scale));
        }



  




        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position, null, tint, Rotation, Origin, Scale, SpriteEffect, 0f);
            //spriteBatch.Draw(image, Hitbox, Color.Red);
        }
    }
}
