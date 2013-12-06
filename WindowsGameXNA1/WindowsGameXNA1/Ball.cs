using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGameXNA1
{
    class Ball : Object3D
    {
        public Vector3 Direction { get; set; }
        public float Speed;

        public Ball(Vector3 location, Vector3 scale, float speed) : base(location, scale)
        {
            this.Direction = Vector3.Zero;
            this.Speed = speed;
        }

        public override void Update()
        {
            this.Location += this.Direction;
        }
    }
}
