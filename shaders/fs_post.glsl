#version 330

// shader input
in vec2 P;						// fragment position in screen space
in vec2 uv;						// interpolated texture coordinates
uniform sampler2D pixels;		// input texture (1st pass render target)
uniform sampler3D colorCube;

const float RADIUS = 0.55;
const float SOFTNESS = 0.45;

vec3 colorOffset = vec3 (0.05, 0.03, 0.01);

// shader output
out vec3 outputColor;

void main()
{
	// Calculate distance from screen centre
	float dx = P.x - 0.5, dy = P.y - 0.5;
	float distance = sqrt( dx * dx + dy * dy );
	colorOffset *= distance;

	// retrieve input pixel with chromatic abberation
	outputColor = vec3 ( 
      texture(pixels, vec2(uv.x + colorOffset.r, uv.y + colorOffset.r)).r, 
      texture(pixels, vec2(uv.x + colorOffset.g, uv.y + colorOffset.g)).g,
      texture(pixels, vec2(uv.x + colorOffset.b, uv.y + colorOffset.b)).b
	  );

	// apply vignetting
	float vignette = smoothstep(RADIUS, SOFTNESS, distance);
	outputColor = mix(outputColor.rgb, outputColor.rgb * vignette, 0.5);
	
	// Now get appropriate color from color cube.
	const vec3 offset = vec3(1.0 / (2.0 * 16));
	outputColor = mix(outputColor.rgb, texture3D(colorCube, vec3((16 - 1.0) / 16) * outputColor.xyz + offset).rgb, 1);
}


// EOF
