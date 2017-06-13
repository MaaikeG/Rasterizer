using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
