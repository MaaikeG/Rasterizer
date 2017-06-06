using OpenTK;

namespace template_P3
{
    class Light
    {
        public Vector3 position;
        public Vector3 color;

        public Light(Vector3 position, Vector3 color)
        {
            this.position = position;
            this.color = color;
        }
    }
}
