using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Template_P3;

namespace template_P3
{
    class Node
    {
        public Mesh mesh;
        public Vector3 localRotate;
        public Vector3 localTranslate;
        internal float scale = 1;

        List<Node> children = new List<Node>();
        private float a;

        public int PI { get; private set; }

        internal void Render(Shader shader, Matrix4 parentTransform, float frameDuration)
        {
            if (this.IsInViewFrustrum())
            {
                Matrix4 transform = GetLocalTransform(parentTransform, frameDuration);
                this.mesh.Render(shader, transform, frameDuration);

                foreach (Node child in this.children)
                {
                    child.Render(shader, transform, frameDuration);
                }
            }
        }

        internal bool IsInViewFrustrum()
        {
            float minX= 0, maxX = 0 , minY = 0, maxY = 0, minZ = 0, maxZ = 0;
            foreach(var vertex in mesh.vertices)
            {
                if (vertex.Vertex.X < minX) minX = vertex.Vertex.X;
                if (vertex.Vertex.X > maxX) maxX = vertex.Vertex.X;
                if (vertex.Vertex.Y < minY) minY = vertex.Vertex.Y;
                if (vertex.Vertex.Y > maxY) maxY = vertex.Vertex.Y;
                if (vertex.Vertex.Z < minZ) minZ = vertex.Vertex.Z;
                if (vertex.Vertex.Z > maxZ) maxZ = vertex.Vertex.Z;
            }
            return true;
        }


        private Matrix4 GetLocalTransform(Matrix4 parentTransform, float frameDuration)
        {
            Matrix4 transform = Matrix4.Identity;
            if (this.localRotate != new Vector3())
            {
                transform = Matrix4.CreateFromAxisAngle(this.localRotate, a);
            }
            transform *= Matrix4.CreateScale(this.scale);
            transform *= Matrix4.CreateTranslation(this.localTranslate);
            transform *= parentTransform;

            // update rotation
            a += 0.001f * frameDuration;
            if (a > 2 * PI) a -= 2 * PI;

            return transform;
        }

        internal void AddChild(Node node)
        {
            this.children.Add(node);
        }
    }
}
