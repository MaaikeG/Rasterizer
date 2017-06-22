#version 330

// shader input
in vec2 P;						// fragment position in screen space
in vec2 uv;						// interpolated texture coordinates
uniform sampler2D pixels;		// input texture (1st pass render target)
uniform sampler3D colorCube;

const float RADIUS = 0.55;
const float SOFTNESS = 0.45;

// shader output
out vec3 outputColor;

void main()
{
	// retrieve input pixel
	outputColor = texture( pixels, uv ).rgb;
	// apply vignetting
	float dx = P.x - 0.5, dy = P.y - 0.5;
	float distance = sqrt( dx * dx + dy * dy );
	float vignette = smoothstep(RADIUS, SOFTNESS, distance);
	outputColor = mix(outputColor.rgb, outputColor.rgb * vignette, 0.5);
	
	const vec3 offset = vec3(1.0 / (2.0 * 16));
	outputColor = mix(outputColor.rgb, texture3D(colorCube, vec3((16 - 1.0) / 16) * outputColor.xyz + offset).rgb, 1);
}


// EOF