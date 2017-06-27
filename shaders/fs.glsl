#version 330
 
// shader input
in vec2 uv;						// interpolated texture coordinates
in vec4 normal;					// interpolated normal
in vec4 worldPos;               // transformed world position

// uniform variables
uniform sampler2D pixels;		// texture sampler
uniform vec3 ambientLight;      // ambient light color
uniform vec3 lightColor;        // light color
uniform vec3 lightPos;          // light position
uniform mat4 transform;         // transformation matrix
uniform vec3 cameraPosition;    // camera position

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
    float diffuse = max( 0.0f, dot( lightVector, normal.xyz ) );
    outputColor = outputColor + vec4( materialColor * diffuse * attenuation * lightColor, 1 );
    
    // Specular lighting
    vec3 reflectionVector = normalize(reflect(-lightVector, normal.xyz));
    vec3 viewVector = normalize(cameraPosition - worldPos.xyz);
    float specularExponent = 20;
    float specularIntensity = 0.8;
    float specularReflection = pow(max(dot(reflectionVector, viewVector), 0.0), specularExponent);
    outputColor = outputColor + vec4(specularIntensity * lightColor, 0.0) * specularReflection * diffuse;
}