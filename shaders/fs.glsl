#version 330
 
// shader input
in vec2 uv;						// interpolated texture coordinates
in vec4 normal;					// interpolated normal
in vec4 worldPos;

// uniform variables
uniform sampler2D pixels;		// texture sampler
uniform vec3 ambientLight;
uniform vec3 lightPos;

// shader output
out vec4 outputColor;

// fragment shader
void main()
{
	// Ambient lighting
	outputColor = vec4(ambientLight, 0.0f);

	// Diffuse lighting
    vec3 lightVector = lightPos - worldPos.xyz;
	float dist = lightVector.length();
	lightVector = normalize( lightVector );
	vec3 lightColor = vec3( 10, 10, 8 );
	vec3 materialColor = texture( pixels, uv ).xyz;
	float attenuation = 1.0f / (dist * dist);
	outputColor = outputColor + vec4( materialColor * max( 0.0f, dot( lightVector, normal.xyz ) ) * attenuation * lightColor, 1 );

	// Specular lighting
	vec3 reflectionVector = normalize(reflect(-lightVector, normal.xyz));
}