#version 330

// shader input
in vec2 P;						// fragment position in screen space
in vec2 uv;						// interpolated texture coordinates
uniform sampler2D pixels;		// input texture (1st pass render target)
uniform sampler3D colorCube;

// shader output
out vec3 outputColor;

void main()
{
	// retrieve input pixel
	outputColor = texture( pixels, uv ).rgb;
	
	const vec3 offset = vec3(1.0 / (2.0 * 16));
	outputColor = texture3D(colorCube, vec3((16 - 1.0) / 16) * outputColor.xyz + offset).rgb;
}


// EOF