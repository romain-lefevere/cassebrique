using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGameXNA1
{
    class Cube3D : Object3D
    {
        public bool Visible { get; set; }

        public Cube3D(Vector3 location, Vector3 scale): base(location, scale)
        {
            this.Visible = true;
        }

        public override void Draw(Matrix world, Matrix view, Matrix projection, Camera cam)
        {
            if (this.Visible)
            {
                base.Draw(world, view, projection, cam);
            }
        }

    }
}
