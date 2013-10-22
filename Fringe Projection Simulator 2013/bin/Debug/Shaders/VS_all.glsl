varying vec3 V;
varying vec4 diffuse;

void main()
{
vec3 normal = gl_Normal;
vec3 lightvector = normalize(gl_LightSource[0].position.xyz);
float nxdir = max(0.0,dot(normal,lightvector));
diffuse = gl_LightSource[0].diffuse*nxdir;

V = gl_ModelViewMatrix * gl_Vertex;
gl_Position = ftransform();
}