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
uniform float tarx;
uniform float tary;
uniform float tarz;
uniform float locx;
uniform float locy;
uniform float locz;
uniform float bp1x;
uniform float bp1z;
uniform float bp2x;
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
float xunknown;
float zunknown;
float c1;
float c2;
float rico1;
float rico2;

shadowtest = abs(diffuse.x)+abs(diffuse.y)+abs(diffuse.z);
if (shadowtest > 0.0)
{
colormultiplier = 1.0;
}
else
{
colormultiplier = 0.0;
}

//Calculate width & height along X and Y
gratingwidth = abs(p2x-p1x);
gratingheight = abs(p2y-p1y);

//If sine wave is needed
alphavalue  = (sin((floor((V.x-p1x)/(p2x-p1x)*(float(pixelgratingwidth)))/(float(pixelgratingwidth)))*2.0*pi*gratingfrequency+gratingshift)+1)/2;

//----------------------------------------------------------------------------------------------------------------------------------------
//1. Calculate line of grating
rico1 = (bp2z-bp1z)/(bp2x-bp1x);
c1 = bp2z-rico1*bp2x;

//2. Calculate target line
rico2 = (V.z-locz)/(V.x-locx);
c2 = V.z-rico2*V.x;

//3. Calculate intersection
xunknown = (c2-c1)/(rico1-rico2);
zunknown = rico1*xunknown+c1;

if (xunknown < bp1x)
{
cutoff = 0.0;
}
if (xunknown > bp2x)
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
gl_FragColor = vec4(clrvalue,clrvalue,clrvalue,1);
}
else
{
gl_FragColor = vec4(0,0,0,alphavalue);
}
}