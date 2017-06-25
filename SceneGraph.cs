using OpenTK;
using Template_P3;

namespace template_P3
{
    class SceneGraph
    {
        public Node world = new Node();

        public void Render(Shader shader, Matrix4 transform, float frameDuration)
        {
            var bb = this.GetSceneBoundingBox(transform);

            world.Render(shader, transform, Matrix4.Identity, bb, frameDuration);
        }


        private Plane[] GetSceneBoundingBox(Matrix4 transform)
        {
            Plane[] bb = new Plane[6];

            var mt = Matrix4.Invert(Matrix4.Transpose(transform));
            bb[0] = new Plane(Vector3.Normalize(Vector3.Transform(new Vector3(1, 0, 0), mt)), Vector3.Transform(new Vector3(-1, 0, 0), mt)); // left
            bb[1] = new Plane(Vector3.Normalize(Vector3.Transform(new Vector3(-1, 0, 0), mt)), Vector3.Transform(new Vector3(1, 0, 0), mt)); // right
            bb[2] = new Plane(Vector3.Normalize(Vector3.Transform(new Vector3(0, 1, 0), mt)), Vector3.Transform(new Vector3(0, -1, 0), mt));
            bb[3] = new Plane(Vector3.Normalize(Vector3.Transform(new Vector3(0, -1, 0), mt)), Vector3.Transform(new Vector3(0, 1, 0), mt));
            bb[4] = new Plane(Vector3.Normalize(Vector3.Transform(new Vector3(0, 0, 1), mt)), Vector3.Transform(new Vector3(0, 0, -1), mt)); 
            bb[5] = new Plane(Vector3.Normalize(Vector3.Transform(new Vector3(0, 0, -1), mt)), Vector3.Transform(new Vector3(0, 0, 1), mt)); 
            return bb;
        }
    }
}
