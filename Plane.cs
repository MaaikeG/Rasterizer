using OpenTK;

namespace template_P3
{
    class Plane
    {
        public Vector3 normal;
        public float d; // distance from origin

        public Plane(Vector3 normal, Vector3 origin)
        {
            this.normal = normal;
            this.d = origin.Length;
        }
    }
}
