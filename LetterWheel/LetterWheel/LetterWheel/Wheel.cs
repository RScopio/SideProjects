using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LetterWheel
{
    class Wheel
    {

        public Vector2 Position { get; set; }
        public char[] Alphabet { get; set; }
        public float Radius { get; set; }

        public bool Turnt { get; set; }

        public bool Poop { get; set; }

        public float RotationSpeed { get; set; }

        List<Letter> letters;


        public Wheel(Vector2 position, float radius, char[] alphabet, SpriteFont font, bool Turnt = true)
        {
            Position = position;
            Alphabet = alphabet;
            Radius = radius;

            letters = new List<Letter>();

            //radius is how far away from position
            //location is determined by circumference / 27

            double angle = 0;
            float rot = MathHelper.ToRadians(90);

            for (int i = 0; i < alphabet.Length; i++)
            {
                float dx = (float)Math.Cos(angle) * radius + Position.X;
                float dy = (float)Math.Sin(angle) * radius + Position.Y;

                Letter l = new Letter(alphabet[i], new Vector2(dx, dy), Turnt ? rot : 0, font);
                l.Angle = angle;
                letters.Add(l);

                rot += MathHelper.ToRadians(360 / alphabet.Length + 0.75f);
                angle += MathHelper.ToRadians(360 / alphabet.Length + 0.75f);
            }


        }

        public void Update(KeyboardState ks, KeyboardState lastKs)
        {
            if (ks.IsKeyDown(Keys.T) && lastKs.IsKeyUp(Keys.T))
            {
                Turnt = !Turnt;
                float rot = MathHelper.ToRadians(90);
                foreach (Letter l in letters)
                {
                    if (Turnt)
                    {
                        l.Rotation = rot;
                        rot += MathHelper.ToRadians(360 / letters.Count + 0.75f);
                    }
                    else
                    {
                        l.Rotation = 0;
                    }
                }
            }

            if (ks.IsKeyDown(Keys.R) && lastKs.IsKeyUp(Keys.R))
            {
                Poop = !Poop;
            }

            if (Poop)
            {
                double angle = letters[0].Angle;
                angle += MathHelper.ToRadians(1);
                foreach (Letter l in letters)
                {
                    float dx = (float)Math.Cos(angle) * Radius + Position.X;
                    float dy = (float)Math.Sin(angle) * Radius + Position.Y;
                    l.Position = new Vector2(dx, dy);
                    l.Angle = angle;
                    angle += MathHelper.ToRadians(360 / letters.Count + 0.75f);

                }
            }


            
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (Letter l in letters)
            {
                l.Draw(sb);
            }
        }

    }
}
