using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower.Infrastructure
{
    public class SpriteManager
    {
        protected Texture2D m_texture;
        public Vector2 m_position = Vector2.Zero;
        public Color m_color = Color.White;
        public Vector2 m_origin;
        public float m_rotation = 0f;
        public float m_scale = 1f;
        public SpriteEffects m_spriteEffect;
        protected Dictionary<string, Rectangle[]> Animations = new Dictionary<string, Rectangle[]>();
        protected int FrameIndex = 0;
        public string Animation;
        protected int m_frames;
        public int m_width;
        public int m_height;

        public SpriteManager(Texture2D texture, int frames, int animations)
        {
            this.m_texture = texture;
            this.m_frames = frames;
            m_width = texture.Width / frames;
            m_height = texture.Height / animations;
        }

        public void AddAnimation(string name, int row)
        {
            Rectangle[] recs = new Rectangle[m_frames];
            for (int i = 0; i < m_frames; i++)
            {
                recs[i] = new Rectangle(i * m_width, (row - 1) * m_height, m_width, m_height);
            }

            Animations.Add(name, recs);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_texture, m_position,
            Animations[Animation][FrameIndex],
            m_color, m_rotation, m_origin, m_scale, m_spriteEffect, 0f);
        }
    }

    public class SpriteAnimation : SpriteManager
    {
        private float m_timeElapsed;
        public bool m_isLooping = false;

        // default to 20 frames per second
        private float m_timeToUpdate = 0.05f;
        public int m_framesPerSecond
        {
            set { m_timeToUpdate = (1f / value); }
        }

        public SpriteAnimation(Texture2D Texture, int frames, int animations)
            : base(Texture, frames, animations)
        {
        }

        public void Update(GameTime gameTime)
        {
            m_timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (m_timeElapsed > m_timeToUpdate)
            {
                m_timeElapsed -= m_timeToUpdate;

                if (FrameIndex < m_frames - 1)
                    FrameIndex++;
                else if (m_isLooping)
                    FrameIndex = 0;
            }
        }
    }
}
