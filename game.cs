using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using template_P3;

// minimal OpenTK rendering framework for UU/INFOGR
// Jacco Bikker, 2016

namespace Template_P3
{

    class Game
    {
        // member variables
        public Surface screen;                  // background surface for printing etc.
        public SceneGraph scene;
        Stopwatch timer;                        // timer for measuring frame duration
        Shader shader;                          // shader to use for rendering
        Shader postproc;                        // shader to use for post processing
        RenderTarget target;                    // intermediate render target
        ScreenQuad quad;                        // screen filling quad for post processing
        
        // prepare matrix for vertex shader
        Matrix4 cameraPos = Matrix4.CreatePerspectiveFieldOfView(1.2f, 1.3f, .1f, 1000);

        // initialize
        public void Init()
        {
            // create shaders
            shader = new Shader("../../shaders/vs.glsl", "../../shaders/fs.glsl");
            postproc = new Shader("../../shaders/vs_post.glsl", "../../shaders/fs_post.glsl");
            // load a texture
            Texture wood = new Texture("../../assets/wood.jpg");
            // create the render target

            scene = new SceneGraph();
            Mesh teapot = new Mesh("../../assets/teapot.obj")
            {
                localTranslate = new Vector3(0, -4, -15),
                localRotate = new Vector3(0, 1, 0)
            };

            Mesh babyTeapot = new Mesh("../../assets/teapot.obj") {
                localTranslate = new Vector3(-5, 2, -7),
                scale = 0.3f,
                localRotate = new Vector3(0, 1, 0)
            };

            teapot.AddChild(babyTeapot, wood);
            scene.AddChild(teapot, wood);
            scene.AddChild(new Mesh("../../assets/floor.obj") { localTranslate = new Vector3(0, -4, -15) }, wood);
            // initialize stopwatch
            timer = new Stopwatch();
            timer.Reset();
            timer.Start();

            target = new RenderTarget(screen.width, screen.height);
            quad = new ScreenQuad();
        }

        // tick for background surface
        public void Tick()
        {
            screen.Clear(0);
            screen.Print("hello world", 2, 2, 0xffff00);
        }

        // tick for OpenGL rendering code
        public void RenderGL()
        {
            // measure frame duration
            float frameDuration = timer.ElapsedMilliseconds;
            timer.Reset();
            timer.Start();

            // enable render target
            target.Bind();

            // render the scene
            scene.Render(shader, cameraPos, frameDuration);

            // render quad
            target.Unbind();
            quad.Render(postproc, target.GetTextureID());
        }


        
        public void MoveCamera(float x, float y, float z)
        {
            Vector3 movement = new Vector3(x, y, z);
            this.cameraPos *= Matrix4.CreateTranslation(movement);
        }
    }

} // namespace Template_P3