using System;
using System.Collections.Generic;
using OpenTK;
using Template_P3;

namespace template_P3
{
    public class Node
    {
        public Mesh mesh;
        public Vector3 localRotate;
        public Vector3 localTranslate;
        internal float scale = 1;

        List<Node> children = new List<Node>();
        private float a;

        public int PI { get; private set; }

        internal void Render(Shader shader, Matrix4 parentTransform, Matrix4 world, float frameDuration)
        {
            if (IsInViewFrustrum())
            {
                Matrix4 transform = GetLocalTransform(world, parentTransform, frameDuration);
                world = transform * world;
                transform *= parentTransform;
                if (mesh != null) mesh.Render(shader, transform, world);

                foreach (Node child in children)
                {
                    child.Render(shader, transform, world, frameDuration);
                }
            }
        }

        internal bool IsInViewFrustrum()
        {
            if (mesh != null)
            {
                Sphere s = GetBoundingSphere();
            }
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

        private Matrix4 GetLocalTransform(Matrix4 world, Matrix4 parentTransform, float frameDuration)
        {
            Matrix4 transform = Matrix4.Identity;
       
            if (localRotate != Vector3.Zero)
            {
                transform = Matrix4.CreateFromAxisAngle(localRotate, a);
            }
            transform *= Matrix4.CreateScale(scale);
            transform *= Matrix4.CreateTranslation(localTranslate);

            // update rotation
            a += 0.001f * frameDuration;
            if (a > 2 * PI) a -= 2 * PI;

            return transform;
        }

        internal void AddChild(Node node)
        {
            children.Add(node);
        }
    }
}
