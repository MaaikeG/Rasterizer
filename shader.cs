﻿using System;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace Template_P3 {

    public class Shader
    {
        // data members
        public int programID, vsID, fsID;
        public int attribute_vpos;
        public int attribute_vnrm;
        public int attribute_vuvs;
        public int uniform_mview;
        public int uniform_toWorld;
        public int uniform_ambientLight;

        public int lightPosition;
        public int lightColor;
        public int camPosition;

        // constructor
        public Shader( String vertexShader, String fragmentShader )
        {
            // compile shaders
            programID = GL.CreateProgram();
            Load( vertexShader, ShaderType.VertexShader, programID, out vsID );
            Load( fragmentShader, ShaderType.FragmentShader, programID, out fsID );
            GL.LinkProgram( programID );
            Console.WriteLine( GL.GetProgramInfoLog( programID ) );

            // get locations of shader parameters
            attribute_vpos = GL.GetAttribLocation( programID, "vPosition" );
            attribute_vnrm = GL.GetAttribLocation( programID, "vNormal" );
            attribute_vuvs = GL.GetAttribLocation( programID, "vUV" );
            uniform_mview = GL.GetUniformLocation( programID, "transform" );
            uniform_toWorld = GL.GetUniformLocation(programID, "toWorld");
            uniform_ambientLight = GL.GetUniformLocation(programID, "ambientLight");
            lightPosition = GL.GetUniformLocation(programID, "lightPos");
            lightColor = GL.GetUniformLocation(programID, "lightColor");
            camPosition = GL.GetUniformLocation(programID, "cameraPosition");
        }

        // loading shaders
        void Load(string filename, ShaderType type, int program, out int ID )
        {
            // source: http://neokabuto.blogspot.nl/2013/03/opentk-tutorial-2-drawing-triangle.html
            ID = GL.CreateShader( type );
            using (StreamReader sr = new StreamReader( filename )) GL.ShaderSource( ID, sr.ReadToEnd() );
            GL.CompileShader( ID );
            GL.AttachShader( program, ID );
            Console.WriteLine( GL.GetShaderInfoLog( ID ) );
        }
    }

} // namespace Template_P3
