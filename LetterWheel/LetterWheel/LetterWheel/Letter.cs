using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LetterWheel
{
    class Letter
    {
        
        
        public char Value { get; set; }
        public float Rotation { get; set; }
        public Rectangle Bounds { get; set; }
        public SpriteFont Font { get; set; }
        public Vector2 Position { get; set; }
        public double Angle { get; set; }

        public Letter(char value, Vector2 position,float rotation, SpriteFont font)
        {
            Value = value;
            Font = font;
            Position = position;
            Rotation = rotation;
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, (int)Font.MeasureString(Value.ToString()).X, (int)Font.MeasureString(Value.ToString()).Y);
        }


        public void Draw(SpriteBatch sb)
        {
            Vector2 measure = Font.MeasureString(Value.ToString());
            sb.DrawString(Font, Value.ToString(), Position, Color.White,Rotation, measure/2, 1, SpriteEffects.None, 1);
        }

        
    }
}
