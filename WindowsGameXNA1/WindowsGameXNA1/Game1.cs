using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGameXNA1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Matrix View;
        Matrix Projection;
        Matrix World;

        Matrix custom;

        Camera camera;
        Vector3 pos;
        Vector3 rot;

        List<Texture2D> textureList;
        List<Model> modelList;

        BasicEffect effect;

        Grid grid;
        Ball ball;
        Pong rack;
        float rackStepDIv = 5;

        Terrain terrain;

        SkyBox sky;

        //Création de la caméra
        private void CreateCamera()
        {
            View = Matrix.CreateLookAt(camera.Position, camera.Target, camera.Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.01f, 1000.0f);
            World = Matrix.Identity;
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 800;
            //graphics.IsFullScreen = true;
            
            graphics.PreferMultiSampling = true;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            camera = new Camera(new Vector3(0.0f, 6.0f, 25.0f), new Vector3(0f, 6f, 0f), Vector3.Up);
            pos = camera.Position;
            rot = Vector3.Zero;

            this.CreateCamera();
     
            float width = 10f;
            float height = 3f;

            float interval = 0f;
            float startX = - (width / 2f);
            float rackY = 0;
            float startY = rackY + 9;
            
            grid = new Grid(new Vector2(width, height), new Vector3(startX, startY, 0.0f), new Vector3(1f, 1f, 1f), interval);
            rack = new Pong(new Vector3(0, rackY, 0), new Vector3(1f, 1f, 1f));
            ball = new Ball(new Vector3(0, 0, 0), new Vector3(0.60f, 0.60f, 0.60f), 0.07f);

            terrain = new Terrain(new Vector3(-0.5f, 6, 0), new Vector3(12f, 15f, 0f));

            Vector3 loc = rack.Location;
            loc.Y = rack.Location.Y + rack.Scale.Y / 8 + ball.Scale.Y / 2;
            ball.Location = loc;

            sky = new SkyBox();
            sky.Initialize(this.GraphicsDevice);

            textureList = new List<Texture2D>();
            modelList = new List<Model>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            this.effect = new BasicEffect(this.GraphicsDevice);


            //grid.LoadContent(Content.Load<Model>("models/cube3D"));
            grid.LoadContent(Content.Load<Model>("models/newcube"));
            rack.LoadContent(Content.Load<Model>("models/rack"));
            ball.LoadContent(Content.Load<Model>("models/tennis-ball"));



            terrain.LoadContent(Content.Load<Model>("models/cube3D2"));
            terrain.changeTexture(Content.Load<Texture2D>("models/textures/cube3D2-terrain"));

            Effect skyEffect = Content.Load<Effect>("skyboxes/SkyEffect");
            TextureCube skyTex = Content.Load<TextureCube>("skyboxes/SkyBoxTex");
            sky.LoadContent(skyTex, skyEffect);

            textureList.Add(Content.Load<Texture2D>("models/textures/cube3D2-terrain"));
            //grid.changeTexture(textureList[0]);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyState = Keyboard.GetState();

            float step = 0.05f;

            /*
            if (gamepadState.IsConnected)
            {

                if (gamepadState.Buttons.Back == ButtonState.Pressed)
                    this.Exit();

                if (gamepadState.Buttons.Start == ButtonState.Pressed)
                {
                    pos = new Vector3(0f, 6f, 25f);

                    rack.Location = Vector3.Zero;

                    rot = Vector3.Zero;

                    camera.UpdateTarget(new Vector3(0f, 6f, pos.Z));
                    camera.UpdateRot(rot);
                    camera.UpdatePos(pos);

                    Vector3 loc = rack.Location;
                    loc.Y = rack.Location.Y + rack.Scale.Y / 8 + ball.Scale.Y / 2;
                    ball.Location = loc;
                    ball.Direction = Vector3.Zero;

                    View = Matrix.CreateLookAt(camera.Position, camera.Target, camera.Up);
                }

                if (gamepadState.DPad.Left == ButtonState.Pressed)
                {
                    pos.X -= 2 * step;
                    camera.UpdatePos(pos);
                    View = Matrix.CreateLookAt(camera.Position, camera.Target, camera.Up);
                }

                if (gamepadState.DPad.Right == ButtonState.Pressed)
                {
                    pos.X += 2 * step;
                    camera.UpdatePos(pos);
                    View = Matrix.CreateLookAt(camera.Position, camera.Target, camera.Up);
                }

                if (gamepadState.DPad.Up == ButtonState.Pressed)
                {
                    pos.Y += 2 * step;
                    camera.UpdatePos(pos);
                    View = Matrix.CreateLookAt(camera.Position, camera.Target, camera.Up);
                }

                if (gamepadState.DPad.Down == ButtonState.Pressed)
                {
                    pos.Y -= 2 * step;
                    camera.UpdatePos(pos);
                    View = Matrix.CreateLookAt(camera.Position, camera.Target, camera.Up);
                }

                if (gamepadState.Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    rot.Y += step;
                    camera.UpdateRot(rot);
                }

                if (gamepadState.Buttons.RightShoulder == ButtonState.Pressed)
                {
                    rot.Y -= step;
                    camera.UpdateRot(rot);

                }

                if (gamepadState.Buttons.X == ButtonState.Pressed)
                {
                    pos.Z += 2 * step;
                    camera.UpdatePos(pos);
                    View = Matrix.CreateLookAt(camera.Position, camera.Target, camera.Up);
                }

                if (gamepadState.Buttons.Y == ButtonState.Pressed)
                {
                    pos.Z -= 2 * step;
                    camera.UpdatePos(pos);
                    View = Matrix.CreateLookAt(camera.Position, camera.Target, camera.Up);
                }

                if (gamepadState.Buttons.A == ButtonState.Pressed)
                {
                    if (ball.Direction == Vector3.Zero)
                    {
                        Vector3 dir = new Vector3(ball.Speed, ball.Speed, 0);
                        ball.Direction = dir;
                    }
                }

                if (gamepadState.Triggers.Left > 0 && gamepadState.Triggers.Right == 0)
                {
                    if (rack.Location.X > terrain.Left.X + (terrain.UnitSize / 2f) + rack.Scale.X + (gamepadState.Triggers.Left / rackStepDIv))
                        rack.Location = new Vector3(rack.Location.X - (gamepadState.Triggers.Left / rackStepDIv), rack.Location.Y, rack.Location.Z);
                }

                if (gamepadState.Triggers.Right > 0 && gamepadState.Triggers.Left == 0)
                {
                    if (rack.Location.X < terrain.Right.X - (terrain.UnitSize / 2f) - rack.Scale.X - (gamepadState.Triggers.Right / rackStepDIv))
                        rack.Location = new Vector3(rack.Location.X + (gamepadState.Triggers.Right / rackStepDIv), rack.Location.Y, rack.Location.Z);
                }
            }

            else
            {
             */ 
                if (keyState.IsKeyDown(Keys.Escape))
                {
                    this.Exit();
                }

                if (keyState.IsKeyDown(Keys.C))
                {
                    pos = new Vector3(0f, 6f, 25f);

                    rack.Location = Vector3.Zero;

                    rot = Vector3.Zero;

                    camera.UpdateTarget(new Vector3(0f, 6f, pos.Z));
                    camera.UpdateRot(rot);
                    camera.UpdatePos(pos);

                    Vector3 loc = rack.Location;
                    loc.Y = rack.Location.Y + rack.Scale.Y / 8 + ball.Scale.Y / 2;
                    ball.Location = loc;
                    ball.Direction = Vector3.Zero;

                    View = Matrix.CreateLookAt(camera.Position, camera.Target, camera.Up);
                }

                if (keyState.IsKeyDown(Keys.A))
                {
                    rot.Y += step;
                    camera.UpdateRot(rot);
                }

                if (keyState.IsKeyDown(Keys.E))
                {
                    rot.Y -= step;
                    camera.UpdateRot(rot);
                }

                if (keyState.IsKeyDown(Keys.Z))
                {
                    pos.Z -= 2 * step;
                    camera.UpdatePos(pos);
                    View = Matrix.CreateLookAt(camera.Position, camera.Target, camera.Up);
                }

                if (keyState.IsKeyDown(Keys.S))
                {
                    pos.Z += 2 * step;
                    camera.UpdatePos(pos);
                    View = Matrix.CreateLookAt(camera.Position, camera.Target, camera.Up);
                }

                if (keyState.IsKeyDown(Keys.Q))
                {
                    pos.X -= 2 * step;
                    camera.UpdatePos(pos);
                    View = Matrix.CreateLookAt(camera.Position, camera.Target, camera.Up);
                }

                if (keyState.IsKeyDown(Keys.D))
                {
                    pos.X += 2 * step;
                    camera.UpdatePos(pos);
                    View = Matrix.CreateLookAt(camera.Position, camera.Target, camera.Up);
                }

                if (keyState.IsKeyDown(Keys.R))
                {
                    pos.Y += 2 * step;
                    camera.UpdatePos(pos);
                    View = Matrix.CreateLookAt(camera.Position, camera.Target, camera.Up);
                }

                if (keyState.IsKeyDown(Keys.F))
                {
                    pos.Y -= 2 * step;
                    camera.UpdatePos(pos);
                    View = Matrix.CreateLookAt(camera.Position, camera.Target, camera.Up);
                }

                if (keyState.IsKeyDown(Keys.Space))
                {
                    if (ball.Direction == Vector3.Zero)
                    {
                        Vector3 dir = new Vector3(ball.Speed, ball.Speed, 0);
                        ball.Direction = dir;
                    }
                }

                if (keyState.IsKeyDown(Keys.Left))
                {
                    if (rack.Location.X > terrain.Left.X + (terrain.UnitSize / 2f) + rack.Scale.X + (0.5f / rackStepDIv))
                        rack.Location = new Vector3(rack.Location.X - (0.5f / rackStepDIv), rack.Location.Y, rack.Location.Z);
                }


                if (keyState.IsKeyDown(Keys.Right))
                {
                    if (rack.Location.X < terrain.Right.X - (terrain.UnitSize / 2f) - rack.Scale.X - (0.5f / rackStepDIv))
                        rack.Location = new Vector3(rack.Location.X + (0.5f / rackStepDIv), rack.Location.Y, rack.Location.Z);
                }

            //}

            if (ball.Direction == Vector3.Zero)
            {
                ball.Location = new Vector3(rack.Location.X, rack.Location.Y + rack.Scale.Y / 8 + ball.Scale.Y / 2, 0f);
            }

            grid.Update(ref this.ball);

            Collision();

            ball.Update();

            base.Update(gameTime);
        }

        public void Collision()
        {
            Vector3 direction = Vector3.Zero;
            float margin = ball.Scale.X;

            if (ball.Location.Y < rack.Location.Y + (rack.Scale.Y / 8f) + (ball.Scale.X / 2f))
            {
                if (ball.Location.X > rack.Location.X - rack.Scale.X - margin && ball.Location.X < rack.Location.X)
                {
                    if (ball.Direction.X > 0)
                    {
                        direction.X = -ball.Direction.X;
                    }
                    else
                    {
                        direction.X = ball.Direction.X;
                    }

                    direction.Y = -ball.Direction.Y;
                    ball.Direction = direction;
                }
                else if (ball.Location.X > rack.Location.X && ball.Location.X < rack.Location.X + rack.Scale.X + margin)
                {
                    if (ball.Direction.X < 0)
                    {
                        direction.X = -ball.Direction.X;
                    }
                    else
                    {
                        direction.X = ball.Direction.X;
                    }

                    direction.Y = -ball.Direction.Y;
                    ball.Direction = direction;
                }
                else
                {
                    ball.Direction = Vector3.Zero;
                }       
            }
            else
            {
                if (ball.Location.X > terrain.Right.X - (terrain.UnitSize / 2f) - (ball.Scale.X / 2f) || ball.Location.X < terrain.Left.X + (terrain.UnitSize / 2f) + (ball.Scale.X / 2f))
                {
                    direction.X = -ball.Direction.X;
                    direction.Y = ball.Direction.Y;
                    ball.Direction = direction;
                }

                if (ball.Location.Y > (terrain.Up.Y - terrain.UnitSize / 2f) - (ball.Scale.X / 2f))
                {
                    direction.X = ball.Direction.X;
                    direction.Y = -ball.Direction.Y;
                    ball.Direction = direction;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            effect.View = View;
            effect.Projection = Projection;
            effect.VertexColorEnabled = false;
            effect.TextureEnabled = true;
            effect.World = World;

            custom = World * Matrix.CreateRotationY(camera.Rotation.Y);

            
            terrain.Draw(World, View, Projection, this.camera);

            ball.Draw(World, View, Projection, this.camera);
            rack.Draw(World, View, Projection, this.camera);
            grid.Draw(World, View, Projection, this.camera);
            sky.Draw(this.GraphicsDevice, custom * Matrix.CreateScale(100f, 100f, 100f), View, Projection);
            
            base.Draw(gameTime);
        }
    }
}
