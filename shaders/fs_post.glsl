#version 330

// shader input
in vec2 P;						// fragment position in screen space
in vec2 uv;						// interpolated texture coordinates
uniform sampler2D pixels;		// input texture (1st pass render target)
uniform sampler3D colorCube;

const float lutSize = 16.0;
const vec3 scale = vec3((lutSize - 1.0) / lutSize);
const vec3 offset = vec3(1.0 / (2 * lutSize));

const float RADIUS = 0.55;
const float SOFTNESS = 0.45;

vec3 colorOffset = vec3 (0.03, 0.02, 0.01);

// shader output
out vec3 outputColor;

void main()
{
	// Calculate distance from screen centre
	float dx = P.x - 0.5, dy = P.y - 0.5;
	float distance = sqrt( dx * dx + dy * dy );
	
	vec2 v = vec2(dx, dy);

	//retrieve input pixel with chromatic abberation
	outputColor = vec3 ( 
      texture(pixels, vec2(uv + colorOffset.r * v)).r, 
      texture(pixels, vec2(uv + colorOffset.g * v)).b,
      texture(pixels, vec2(uv + colorOffset.b * v)).g
	  );

	// apply vignetting
	float vignette = smoothstep(RADIUS, SOFTNESS, distance);
	outputColor = mix(outputColor.rgb, outputColor.rgb * vignette, 0.5);
	
	// Now get appropriate color from color cube.
	outputColor = texture3D(colorCube, scale * outputColor.xyz + offset).rgb;
}

// EOF