using OpenTK;

namespace template_P3
{
    class Light : Node
    {
        public Vector3 color;

        public Light(Vector3 position, Vector3 color)
        {
            localTranslate = position;
            this.color = color;
        }
    }
}
