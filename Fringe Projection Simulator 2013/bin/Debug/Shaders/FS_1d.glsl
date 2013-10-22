varying vec3 V;
varying vec4 diffuse;

uniform float pi;
uniform float calibration;
uniform float blackout;

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

shadowtest = diffuse.x+diffuse.y+diffuse.z;
if (shadowtest > 0.0)
{
colormultiplier = 1.0;
}
else
{
colormultiplier = 1.0;
}

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

clrvalue = clrvalue*cutoff*colormultiplier;
gl_FragColor = vec4(clrvalue,clrvalue,clrvalue,1);
}
else
{
gl_FragColor = vec4(0,0,0,alphavalue);
}
}