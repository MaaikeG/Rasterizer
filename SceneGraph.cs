using OpenTK;
using Template_P3;

namespace template_P3
{
    class SceneGraph
    {
        public Node world = new Node();

        public void Render(Shader shader, Matrix4 transform, float frameDuration)
        {
            world.Render(shader, transform, frameDuration);
        }
    }
}
