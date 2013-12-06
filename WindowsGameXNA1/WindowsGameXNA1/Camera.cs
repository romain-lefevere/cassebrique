using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGameXNA1
{
    class Camera
    {

        public Vector3 Position {get; private set;}
        public Vector3 Target { get; private set; }
        public Vector3 Up { get; private set; }

        public Vector3 Rotation { get; private set; }

        public Camera(Vector3 Position, Vector3 Target, Vector3 UP)
        {
            this.Position = Position;
            this.Target = Target;

            this.Up = UP;
            Rotation = Vector3.Zero;
        }

        public void UpdateRot(Vector3 rot)
        {
            this.Rotation = rot;
        }

        public void UpdateTarget(Vector3 tar)
        {
            this.Target = tar;
        }

        public void UpdatePos(Vector3 pos)
        {
            this.Position = pos;

            Vector3 target = pos;
            target.Z = 0;

            this.Target = target;
        }


    }
}
