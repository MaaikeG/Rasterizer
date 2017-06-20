#version 330
 
// shader input
in vec2 uv;						// interpolated texture coordinates
in vec4 normal;					// interpolated normal
in vec4 worldPos;               // transformed world position
in vec3 normal3;

// uniform variables
uniform sampler2D pixels;		// texture sampler
uniform vec3 ambientLight;      // ambient light color
uniform vec3 lightColor;        // light color
uniform vec3 lightPos;          // light position
uniform mat4 transform;         // camera matrix

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
    vec3 materialColor = texture( pixels, uv ).xyz;
    float attenuation = 1.0f / (dist * dist);
    outputColor = outputColor + vec4( materialColor * max( 0.0f, dot( lightVector, normal.xyz ) ) * attenuation * lightColor, 1 );
    
    // Specular lighting
    vec3 reflectionVector = normalize(reflect(-lightVector, normal.xyz));
    vec3 viewVector = normalize(vec3(inverse(transform) * vec4(0,0,0,1)) - worldPos.xyz);
    float specularExponent = 20;
    float specularIntensity = 0.2;
    float specularReflection = max(dot(normal3, lightVector), 0.0) * pow(max(dot(reflectionVector, viewVector), 0.0), specularExponent);
    outputColor = outputColor + vec4(specularIntensity * lightColor, 0.0) * specularReflection;
}