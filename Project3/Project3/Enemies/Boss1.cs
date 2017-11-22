﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Xaria.Projectiles;
using Xaria.Drops;

namespace Xaria.Enemies
{
    class Boss1 : Enemy
    {
        private const double NEXT_TELEPORT = 4000; //4 seconds
        private const double SHOOT = 1500; //1.5 seconds after teleporting shoot
        private const int HEALTH = 5000;
        private double Countdown = NEXT_TELEPORT;
        private bool CanShoot = false;

        public Boss1(Vector2 position)
        {
            Health = HEALTH;
            Position = position;
            Texture = Game1.textureDictionary["boss1"];//texture changes
            NextShoot = SHOOT;
        }


        public override void Shoot(GameTime gameTime, ref List<Projectile> Projectiles)
        {
            if (CanShoot)
            {
                NextShoot -= gameTime.ElapsedGameTime.Milliseconds;
                if (NextShoot <= 0)
                {
                    NextShoot = SHOOT;
                    Projectiles.Add(new Beam(Position + new Vector2(Texture.Width / 2f, Texture.Height + 5f), new Vector2(0, 50), 100, false));
                    CanShoot = false;
                }
            }
        }

        internal override void OnDeath(ref List<Drop> drops)
        {
            drops.Add(new Life(Position + new Vector2(Texture.Width / 2f, Texture.Height + 5f)));
            if (Level.random.Next(2) == 1) //1/2 chance to drop
            {
                drops.Add(new BeamAmmo(Position + new Vector2(Texture.Width / 2f, Texture.Height + 5f), 5));
            }
        }

        public override void UpdateMovement(Level level, GameTime gameTime)
        {
            Countdown -= gameTime.ElapsedGameTime.Milliseconds;
            if (Countdown <= 0)
            {
                Position = new Vector2(Level.random.Next(((int)Game1.screenSize.X - Texture.Width)), Level.random.Next((500 + Texture.Height)));
                Countdown = NEXT_TELEPORT;
                CanShoot = true;
            }
        }
    }
}