using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGameXNA1
{
    class Object3D
    {
        Model model;
        public Vector3 Location { get;  set; }
        public Vector3 Scale { get;  set; }

        public Object3D(Vector3 location, Vector3 scale) 
        {
            this.Location = location;
            this.Scale = scale;
        }

        public virtual void LoadContent(Model m)
        {
            this.model = m;
        }

        public virtual void changeTexture(Texture2D tex)
        {

            ((BasicEffect)model.Meshes[0].Effects[0]).Texture = tex;
        }

        public virtual void Update()
        {
        }

        public virtual void Draw(Matrix world, Matrix view, Matrix projection, Camera cam)
        {
            Matrix change = world* Matrix.CreateScale(this.Scale) * Matrix.CreateTranslation(this.Location)  * Matrix.CreateRotationY(cam.Rotation.Y);

            model.Draw(change , view, projection);
        }
    }
}
