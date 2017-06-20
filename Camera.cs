using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_P3
{
    // A camera as defined by Neo Kabuto in his tutorial
    // http://neokabuto.blogspot.nl/2014/01/opentk-tutorial-5-basic-camera.html
    class Camera
    {
        public Vector3 position = Vector3.Zero;
        public Vector3 orientation = new Vector3((float)Math.PI, 0f, 0f);
        public float moveSpeed = 0.2f;
        public float sensitivity = 0.01f;

        public Matrix4 GetViewMatrix()
        {
            Vector3 lookat = new Vector3();

            lookat.X = (float) (Math.Sin(orientation.X) * Math.Cos(orientation.Y));
            lookat.Y = (float) Math.Sin(orientation.Y);
            lookat.Z = (float) (Math.Cos(orientation.X) * Math.Cos(orientation.Y));

            return Matrix4.LookAt(position, position + lookat, Vector3.UnitY);
        }

        public void Move(float x, float y, float z)
        {
            Vector3 offset = new Vector3();

            Vector3 forward = new Vector3((float)Math.Sin((float)orientation.X), 0, (float)Math.Cos((float)orientation.X));
            Vector3 right = new Vector3(-forward.Z, 0, forward.X);

            offset += x * right;
            offset += y * forward;
            offset.Y += z;

            offset.NormalizeFast();
            offset = Vector3.Multiply(offset, moveSpeed);

            position += offset;
        }

        public void AddRotation(float x, float y)
        {
            x = x * sensitivity;
            y = y * sensitivity;

            orientation.X = (orientation.X + x) % ((float)Math.PI * 2.0f);
            orientation.Y = Math.Max(Math.Min(orientation.Y + y, (float)Math.PI / 2.0f - 0.1f), (float)-Math.PI / 2.0f + 0.1f);
        }

    }
}
