using OpenTK;

namespace template_P3
{
    public class Sphere
    {
        public Vector3 origin;
        public float r;

        public Sphere(Vector3 center, float r)
        {
            this.origin = center;
            this.r = r;
        }
    }
}