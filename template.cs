﻿using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Template_P3
{

    public class OpenTKApp : GameWindow
    {
        static int screenID;
        static Game game;
        static bool terminated = false, mouseControl = false;
        private Vector2 lastMousePos = Vector2.Zero;

        protected override void OnLoad(EventArgs e)
        {
            // called upon app init
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.Texture2D);
            GL.Disable(EnableCap.DepthTest);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            ClientSize = new Size(640, 400);
            CursorVisible = true;
            game = new Game();
            game.screen = new Surface(Width, Height);
            Sprite.target = game.screen;
            screenID = game.screen.GenTexture();
            game.Init();
            this.KeyPress += new EventHandler<KeyPressEventArgs>(handleKeyPress);
        }

        private void handleKeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'w':
                    game.Move(0, 0.1f, 0);
                    break;
                case 's':
                    game.Move(0, -0.1f, 0);
                    break;
                case 'a':
                    game.Move(-0.1f, 0, 0);
                    break;
                case 'd':
                    game.Move(0.1f, 0, 0);
                    break;
                case 'e':
                    game.Move(0, 0, 0.1f);
                    break;
                case 'q':
                    game.Move(0, 0, -0.1f);
                    break;
                case 'm':
                    mouseControl = !mouseControl;
                    CursorVisible = !CursorVisible;
                    break;
            }
        }
        protected override void OnUnload(EventArgs e)
        {
            // called upon app close
            GL.DeleteTextures(1, ref screenID);
            Environment.Exit(0); // bypass wait for key on CTRL-F5
        }
        protected override void OnResize(EventArgs e)
        {
            // called upon window resize
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            // called once per frame; app logic
            var keyboard = OpenTK.Input.Keyboard.GetState();
            if (keyboard[OpenTK.Input.Key.Escape]) this.Exit();
            if (Focused)
            {
                if (mouseControl)
                {
                    Vector2 delta = lastMousePos - new Vector2(OpenTK.Input.Mouse.GetState().X, OpenTK.Input.Mouse.GetState().Y);
                    game.cam.AddRotation(delta.X, delta.Y);
                    ResetCursor();
                }
            }
        }

        private void ResetCursor()
        {
            OpenTK.Input.Mouse.SetPosition(Bounds.Left + Bounds.Width / 2, Bounds.Top + Bounds.Height / 2);
            lastMousePos = new Vector2(OpenTK.Input.Mouse.GetState().X, OpenTK.Input.Mouse.GetState().Y);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            // called once per frame; render
            game.Tick();
            if (terminated)
            {
                Exit();
                return;
            }
            // set the state for rendering the quad
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.Texture2D);
            GL.Disable(EnableCap.DepthTest);
            GL.Color3(1.0f, 1.0f, 1.0f);
            GL.BindTexture(TextureTarget.Texture2D, screenID);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                            game.screen.width, game.screen.height, 0,
                            OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                            PixelType.UnsignedByte, game.screen.pixels
                            );
            // GL.Clear( ClearBufferMask.ColorBufferBit ); /* not needed */
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(-1.0f, -1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(1.0f, -1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(1.0f, 1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(-1.0f, 1.0f);
            GL.End();
            // prepare for generic OpenGL rendering
            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            GL.Disable(EnableCap.Texture2D);
            // do OpenGL rendering
            game.RenderGL();
            // swap buffers
            SwapBuffers();
        }

        protected override void OnFocusedChanged(EventArgs e)
        {
            base.OnFocusedChanged(e);
            if (Focused) ResetCursor();
        }

        public static void Main(string[] args)
        {
            // entry point
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US"); // thanks Mathijs
            using (OpenTKApp app = new OpenTKApp()) { app.Run(30.0, 0.0); }
        }
    }

} // namespace Template_P3