#version 150
in vec3 V;
in vec4 diffuse;

out vec4 gl_FragColor;

struct gl_MaterialParameters
{
vec4 emission;
vec4 ambient;
vec4 diffuse;
vec4 specular;
float shininess;
};
uniform gl_MaterialParameters gl_FrontMaterial;
uniform gl_MaterialParameters gl_BackMaterial;

uniform float pi;
uniform float calibration;
uniform float blackout;
uniform int beamerlenstype;
// 0 = diverging
// 1 = telecentric

//----------------------------------------------------------------------------------------------------------------------------------------
// Grating parameters
uniform int blnGratingEnabled;
uniform float gratingfrequency;
uniform float gratingshift;
uniform float p1x;
uniform float p1y;
uniform float p1z;
uniform float p2x;
uniform float p2y;
uniform float p2z;
uniform int pixelgratingwidth;
uniform int pixelgratingheight;

//----------------------------------------------------------------------------------------------------------------------------------------
// Projection parameters
uniform float beamerfov; //X direction
uniform float beamerfrequency;
uniform float beamershift;
uniform float beamerwidth;
uniform float tarx;
uniform float tary;
uniform float tarz;
uniform float locx;
uniform float locy;
uniform float locz;
uniform float bp1x;
uniform float bp1y;
uniform float bp1z;
uniform float bp2x;
uniform float bp2y;
uniform float bp2z;
uniform int pixelbeamerwidth;
uniform int pixelbeamerheight;
//----------------------------------------------------------------------------------------------------------------------------------------

void main()
{ 
float color;
float colormultiplier;
float shadowtest;
//----------------------------------------------------------------------------------------------------------------------------------------
//Declarations
float alphavalue;
float gratingwidth;
float gratingheight;
vec4 originalcolor;
originalcolor = gl_FrontMaterial.diffuse;
vec4 FragColor;
int i;
float cutoff;
cutoff = 1.0;

float clrvalue;
float xvalue;
float rico;
float projtarx;
float beta;
float xmin;
float xmax;
float xunknown;
float yunknown;
float zunknown;
float c1;
float c2;
float rico1;
float rico2;

vec3 R;
vec3 B;
mat3 A;

shadowtest = abs(diffuse.x)+abs(diffuse.y)+abs(diffuse.z);
if (shadowtest > 0.0)
{
colormultiplier = 1.0;
}
else
{
colormultiplier = 0.0;
}

if (beamerlenstype == 0) // DIVERGING LENS
{
//Calculate width & height along X and Y
gratingwidth = abs(p2x-p1x);
gratingheight = abs(p2y-p1y);

//If sine wave is needed
alphavalue  = (sin((floor((V.x-p1x)/(p2x-p1x)*(float(pixelgratingwidth)))/(float(pixelgratingwidth)))*2.0*pi*gratingfrequency+gratingshift)+1)/2;

//----------------------------------------------------------------------------------------------------------------------------------------
//Find intersection between ray and grating to find if the ray passes the grating
//1. Define Matrices
A = mat3(
   V.x-locx, V.y-locy, V.z-locz, // first column (not row!)
   0.0, bp2y-bp1y, 0.0, // second column
   bp2x-bp1x, bp2y-bp1y, bp2z-bp1z  // third column
);
B = vec3(V.x-bp1x,V.y-bp1y,V.z-bp1z);

R = inverse(A)*B;

xunknown = V.x + R[0]*(locx-V.x);
yunknown = V.y + R[0]*(locy-V.y);

////1. Calculate line of grating x
//rico1 = (bp2z-bp1z)/(bp2x-bp1x);
//c1 = bp2z-rico1*bp2x;

////2. Calculate target line x
//rico2 = (V.z-locz)/(V.x-locx);
//c2 = V.z-rico2*V.x;

////3. Calculate intersection x
//xunknown = (c2-c1)/(rico1-rico2);
//zunknown = rico1*xunknown+c1;

if (xunknown < bp1x)
{
cutoff = 0.0;
}
if (xunknown > bp2x)
{
cutoff = 0.0;
}
if (yunknown < bp1y)
{
cutoff = 0.0;
}
if (yunknown > bp2y)
{
cutoff = 0.0;
}

//4. Lookup the colorvalue
//If sine wave is needed
clrvalue = (sin((floor((xunknown-bp1x)/(bp2x-bp1x)*float(pixelbeamerwidth))/float(pixelbeamerwidth))*2.0*pi*beamerfrequency+beamershift)+1)/2;

//vec4 myColor = vec4(clrvalue,clrvalue,clrvalue,1.0);
//gl_FragColor  = myColor;
//----------------------------------------------------------------------------------------------------------------------------------------
//Main program
if (originalcolor[1] > 0.5)
{
clrvalue = clrvalue*cutoff*colormultiplier;
//gl_FragColor = vec4(clrvalue,clrvalue,clrvalue,1);
gl_FragColor = vec4(clrvalue,clrvalue,clrvalue,1);
}
else
{
//gl_FragColor = vec4(0,0,0,alphavalue);
gl_FragColor = vec4(0,0,0,alphavalue);
}
}
else //TELECENTRIC LENS
{
//----------------------------------------------------------------------------------------------------------------------------------------
//Calculate width & height along X and Y
gratingwidth = abs(p2x-p1x);
gratingheight = abs(p2y-p1y);

//If sine wave is needed
alphavalue  = (sin((floor((V.x-p1x)/(p2x-p1x)*(float(pixelgratingwidth)))/(float(pixelgratingwidth)))*2.0*pi*gratingfrequency+gratingshift)+1)/2;

//----------------------------------------------------------------------------------------------------------------------------------------
//1. Calculate rico
rico = (tarz-locz)/(tarx-locx);

//2a. Project tar on ref plane
projtarx = (0.0-locz)/rico+locx;

//2b. Project fragment value on Z=0 plane
xvalue = (0.0-V.z)/rico+V.x;

//3a. Calculate beta (loc-tar vs ref (Z=0) plane)
beta = atan(locz/(projtarx-locx));

//3b. Calculate xmin and xmax;
xmax = projtarx-(beamerwidth/2.0)/sin(beta);
xmin = projtarx+(beamerwidth/2.0)/sin(beta);

//4. Lookup the colorvalue
//If sine wave is needed
clrvalue = (sin((floor((xvalue-xmin)/(xmax-xmin)*float(pixelbeamerwidth)))/float(pixelbeamerwidth)*2.0*pi*beamerfrequency+beamershift)+1)/2;

//vec4 myColor = vec4(clrvalue,clrvalue,clrvalue,1.0);
//gl_FragColor  = myColor;
//----------------------------------------------------------------------------------------------------------------------------------------
//Main program
if (originalcolor[1] > 0.5)
{

//3c. Less than xmin, higher than xmax is cutoff
if (xvalue < xmin)
{
cutoff = 0.0;
}
if (xvalue > xmax)
{
cutoff = 0.0;
}

clrvalue = clrvalue*cutoff*colormultiplier*0;
gl_FragColor = vec4(clrvalue,clrvalue,clrvalue,1);

}
else
{
//gl_FragColor = vec4(0,0,0,alphavalue);
gl_FragColor = vec4(0.0,1.0,0.0,alphavalue);
}
}
}