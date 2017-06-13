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
                if (this.mesh != null) this.mesh.Render(shader, transform, frameDuration);

                foreach (Node child in this.children)
                {
                    child.Render(shader, transform, frameDuration);
                }
            }
        }

        internal bool IsInViewFrustrum()
        {
            Sphere s = GetBoundingSphere();
            
            return true;
        }

        private Sphere GetBoundingSphere()
        {
            Vector3 o = new Vector3(); // Average of all vectors in mesh is the sphere origin
            foreach (var vertex in mesh.vertices)
            {
                o += vertex.Vertex;
            }
            Vector3.Divide(o, mesh.vertices.Length);

            // Get largest disdance from center
            float r2 = 0;
            float dis = 0;
            foreach (var vertex in mesh.vertices)
            {
                dis = (vertex.Vertex.X - o.X) * (vertex.Vertex.X - o.X)
                    + (vertex.Vertex.Y - o.Y) * (vertex.Vertex.Y - o.Y)
                    + (vertex.Vertex.Z - o.Z) * (vertex.Vertex.Z - o.Z);
                if (dis > r2) r2 = dis;
            }

            return new Sphere(o, (float)Math.Sqrt(r2));
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
