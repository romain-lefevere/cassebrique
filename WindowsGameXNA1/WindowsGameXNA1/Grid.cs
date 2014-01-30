using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGameXNA1
{
    class Grid
    {
        List<List<Cube3D>> elements;
        Vector2 Size;

        public float Width { get { return Size.X; } }
        public float Height { get { return Size.Y; } }

        public float Interval;
        public Vector3 UnitSize;

        public Grid(Vector2 size, Vector3 startLocation, Vector3 unitsize, float interval)
        {
            UnitSize = unitsize;
            Interval = interval;

            Vector3 start = startLocation;
            elements = new List<List<Cube3D>>();

            Size = size;

            for (int i = 0; i < Height; i++)
            {
                List<Cube3D> ls = new List<Cube3D>();

                for (int j = 0; j < Width; j++)
                {
                    Cube3D c = new Cube3D(Vector3.Zero, unitsize);

                    c.Location = new Vector3((j * (unitsize.X + Interval)), i * (unitsize.Y + Interval), 0.0f) + start;

                    ls.Add(c);
                }

                elements.Add(ls);
            }
        }

        public void changeTexture(Texture2D tex)
        {

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    elements[i][j].changeTexture(tex);
                }
            }
        }

        public void LoadContent(Model m)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    elements[i][j].LoadContent(m);
                }
            }
        }

        public void showAll()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    elements[i][j].Visible = true;
                }
            }

        }

        public void Update(ref Ball ball)
        {
            bool collision = false;

            for (int i = 0; i < Height && !collision; i++)
            {
                for (int j = 0; j < Width && !collision; j++)
                {
                    Cube3D c = elements[i][j];
                        //*

                    if (c.Visible)
                    {
                        // collision par le bas
                        if (ball.Location.Y > c.Location.Y - (c.Scale.Y / 2f) - (ball.Scale.Y / 2f) && ball.Location.Y < c.Location.Y - (c.Scale.Y / 2f) &&
                            ball.Location.X > c.Location.X - (c.Scale.X / 2f) && ball.Location.X < c.Location.X + (c.Scale.X / 2f))
                        {
                            Vector3 direction = Vector3.Zero;

                            direction.X = ball.Direction.X;
                            direction.Y = -ball.Direction.Y;

                            ball.Direction = direction;
                            collision = true;

                            ball.Update();
                            c.Visible = false;
                        }
                        // collision par le haut
                        else if (ball.Location.Y < c.Location.Y + (c.Scale.Y / 2f) + (ball.Scale.Y / 2f) && ball.Location.Y > c.Location.Y + (c.Scale.Y / 2f) &&
                                ball.Location.X > c.Location.X - (c.Scale.X / 2f) && ball.Location.X < c.Location.X + (c.Scale.X / 2f))
                        {
                            Vector3 direction = Vector3.Zero;

                            direction.X = ball.Direction.X;
                            direction.Y = -ball.Direction.Y;

                            ball.Direction = direction;
                            collision = true;
                            ball.Update();
                            c.Visible = false;
                        }

                        // collision par la gauche
                        else if (ball.Location.X > c.Location.X - (c.Scale.X / 2f) - (ball.Scale.X / 2f) && ball.Location.X < c.Location.X - (c.Scale.X / 2f) &&
                                ball.Location.Y > c.Location.Y - (c.Scale.Y / 2f) && ball.Location.Y < c.Location.Y + (c.Scale.Y / 2f))
                        {
                            Vector3 direction = Vector3.Zero;

                            direction.X = -ball.Direction.X;
                            direction.Y = ball.Direction.Y;

                            ball.Direction = direction;
                            collision = true;
                            ball.Update();
                            c.Visible = false;
                        }
                        // collision par la droite
                        else if (ball.Location.X < c.Location.X + (c.Scale.X / 2f) + (ball.Scale.X / 2f) && ball.Location.X > c.Location.X + (c.Scale.X / 2f) &&
                                ball.Location.Y > c.Location.Y - (c.Scale.Y / 2f) && ball.Location.Y < c.Location.Y + (c.Scale.Y / 2f))
                        {
                            Vector3 direction = Vector3.Zero;

                            direction.X = -ball.Direction.X;
                            direction.Y = ball.Direction.Y;

                            ball.Direction = direction;
                            collision = true;
                            ball.Update();
                            c.Visible = false;
                        }

                    }


                    
//*/
                }
            }
        }

        public void Draw(Matrix world, Matrix view, Matrix projection, Camera cam)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    elements[i][j].Draw(world, view, projection, cam);
                }
            }
        }


    }
}
