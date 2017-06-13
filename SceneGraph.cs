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
        List<Node> children = new List<Node>();

        public void Render(Shader shader, Matrix4 transform, float frameDuration)
        {
            for (int i = 0; i < children.Count; i++)
            {
                children[i].Render(shader, transform, frameDuration);
            }
        }

        internal void AddChild(Node node)
        {
            children.Add(node);
        }
    }
}
