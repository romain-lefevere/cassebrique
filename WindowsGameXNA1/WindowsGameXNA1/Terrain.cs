using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGameXNA1
{
    class Terrain : Object3D
    {
        Object3D left;
        Object3D right;
        Object3D up;
        Object3D down;

        public Vector3 Left 
        {
            get { return this.left.Location; }
        }

        public Vector3 Right
        {
            get { return this.right.Location; }
        }

        public Vector3 Up
        {
            get { return this.up.Location; }
        }

        public Vector3 Down
        {
            get { return this.down.Location; }
        }

        public float UnitSize 
        { 
            get { return 1f; } 
        }

        public Terrain(Vector3 location, Vector3 interval): base(location, new Vector3(1f, 1f, 1f))
        {

            left = new Object3D(new Vector3(location.X - (interval.X / 2) - 0.5f, location.Y, location.Z), new Vector3(1f, interval.Y, 1f));
            right = new Object3D(new Vector3(location.X + (interval.X / 2) + 0.5f, location.Y, location.Z), new Vector3(1f, interval.Y, 1f));

            up = new Object3D(new Vector3(location.X, location.Y + (interval.Y / 2) + 0.5f, location.Z), new Vector3(interval.X + 2f, 1f, 1f));
            down = new Object3D(new Vector3(location.X, location.Y - (interval.Y / 2) - 0.5f, location.Z), new Vector3(interval.X + 2f, 1f, 1f));
        }

        public override void LoadContent(Model m)
        {
            left.LoadContent(m);
            right.LoadContent(m);

            up.LoadContent(m);
            down.LoadContent(m);
        }

        public override void changeTexture(Texture2D tex)
        {
            left.changeTexture(tex);
            right.changeTexture(tex);
            
            up.changeTexture(tex);
            down.changeTexture(tex);
        }

        public override void Draw(Matrix world, Matrix view, Matrix projection, Camera cam)
        {
            left.Draw(world, view, projection, cam);
            right.Draw(world, view, projection, cam);

            up.Draw(world, view, projection, cam);
            down.Draw(world, view, projection, cam);
        }
        
    }
}
