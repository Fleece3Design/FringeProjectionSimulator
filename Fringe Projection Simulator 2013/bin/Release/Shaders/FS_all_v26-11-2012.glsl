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
// Plane parameters parallel with grating through Eye
uniform float E1X;
uniform float E1Y;
uniform float E1Z;
uniform float E2X;
uniform float E2Y;
uniform float E2Z;
uniform float E3X;
uniform float E3Y;
uniform float E3Z;
uniform float BeamerGratingDistance;
uniform float TEX;
uniform float TEY;
uniform float TEZ;
uniform float TEnX;
uniform float TEnY;
uniform float TEnZ;
uniform float znear;
uniform float zfar;
uniform int ShadowMapSize;
//----------------------------------------------------------------------------------------------------------------------------------------
uniform sampler2D MyTexture0;
uniform sampler2D ShadowMapTexture;
in vec2 texture_coordinate;
uniform float BoxLength; //Length of the ortho viewing box
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
vec4 texel;
float shadowmultiplier;

float clrvalue;
float xvalue;
float yvalue;
float rico;
float projtarx;
float beta;
float xmin;
float xmax;
float ymin;
float ymax;
float xunknown;
float yunknown;
float zunknown;
float c1;
float c2;
float rico1;
float rico2;

float PixelCoordX;
float PixelCoordY;
float OLength;
float a;
float b;
float DepthLength;
float RealLength;
float VE;

vec3 VP1; //(Plane point 1 through V parallel to grating)
vec3 VP2; //(Plane point 2 through V parallel to grating)
vec3 VP3; //(Plane point 3 through V parallel to grating)
vec3 I;
vec3 R;
vec3 B;
mat3 A;

// First shadow test depending on light source used in the model
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
//- Fringe color lookup ---------------------------------------------------------------------------------------------------------------------------------------
//Find intersection between ray and grating to find if the ray passes the grating
//1. Define Matrices
A = mat3(
   V.x-locx, V.y-locy, V.z-locz, // first column (not row!)
   0.0, bp2y-bp1y, 0.0, // second column
   bp2x-bp1x, bp2y-bp1y, bp2z-bp1z  // third column
);
B = vec3(V.x-bp1x,V.y-bp1y,V.z-bp1z);

R = inverse(A)*B;

//2. Find the location where the ray passes through the grating
xunknown = V.x + R[0]*(locx-V.x);
yunknown = V.y + R[0]*(locy-V.y);

//3. Lookup the colorvalue (If sine wave is needed)
clrvalue = (sin((floor((xunknown-bp1x)/(bp2x-bp1x)*float(pixelbeamerwidth))/float(pixelbeamerwidth))*2.0*pi*beamerfrequency+beamershift)+1)/2;

//- Shadow if ray is not going through the grating ---------------------------------------------------------------------------------------------------------------------------------------
//If the light is not passing through the grating, no light is projected onto the model
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

//- Depth map shadow calculation for diverging projector lens ---------------------------------------------------------------------------------------------------------------------------------------
// 1. Calculate pixel coordinate of intersection between ray V->loc and grating
A = mat3(
   V.x-locx, V.y-locy, V.z-locz, // first column (not row!)
   0.0, bp2y-bp1y, 0.0, // second column
   bp2x-bp1x, bp2y-bp1y, bp2z-bp1z  // third column
);
B = vec3(V.x-bp1x,V.y-bp1y,V.z-bp1z);

R = inverse(A)*B;

xvalue = V.x + R[0]*(locx-V.x);
yvalue = V.y + R[0]*(locy-V.y);

xmin = bp1x;
xmax = bp2x;
ymin = bp1y;
ymax = bp2y;

PixelCoordX = float((0.5+floor(((xvalue-xmin)/(xmax-xmin))*float(ShadowMapSize)))/float(ShadowMapSize)); //between 0-1
PixelCoordY = float((0.5+floor(((yvalue-ymin)/(ymax-ymin))*float(ShadowMapSize)))/float(ShadowMapSize)); //between 0-1

// 2. Look-up distance from depth buffer
texel = texture2D(MyTexture0, vec2(PixelCoordX,PixelCoordY));

// 3. Calculate DepthLength
a = zfar/(zfar-znear);
b = (zfar*znear)/(znear-zfar);
DepthLength = b/(texel.x-a);

// 4. Translate grating to DepthLength
VP1[0] = E1X+DepthLength*TEnX;
VP1[1] = E1Y+DepthLength*TEnY;
VP1[2] = E1Z+DepthLength*TEnZ;
VP2[0] = E2X+DepthLength*TEnX;
VP2[1] = E2Y+DepthLength*TEnY;
VP2[2] = E2Z+DepthLength*TEnZ;
VP3[0] = E3X+DepthLength*TEnX;
VP3[1] = E3Y+DepthLength*TEnY;
VP3[2] = E3Z+DepthLength*TEnZ;

// 5. Find intersection between VP and V-Eye
A = mat3(
   V.x-locx, V.y-locy, V.z-locz, // first column (not row!)
   VP2[0]-VP1[0], VP2[1]-VP1[1], VP2[2]-VP1[2], // second column
   VP3[0]-VP1[0], VP3[1]-VP1[1], VP3[2]-VP1[2]  // third column
);
B = vec3(V.x-VP1[0],V.y-VP1[1],V.z-VP1[2]);

R = inverse(A)*B;

I[0] = V.x + R[0]*(locx-V.x);
I[1] = V.y + R[0]*(locy-V.y);
I[2] = V.z + R[0]*(locz-V.z);

// 6. Calculate norm(EI)
RealLength = sqrt((locx-I[0])*(locx-I[0])+(locy-I[1])*(locy-I[1])+(locz-I[2])*(locz-I[2]));

// 7. Calculate norm(VE)
VE = sqrt((V.x-locx)*(V.x-locx)+(V.y-locy)*(V.y-locy)+(V.z-locz)*(V.z-locz));

// 8. Calculate projected phase
clrvalue = (sin((floor((xvalue-xmin)/(xmax-xmin)*float(pixelbeamerwidth)))/float(pixelbeamerwidth)*2.0*pi*beamerfrequency+beamershift)+1)/2;

if (VE <= RealLength+0.5)
{	
	// no shadow
	shadowmultiplier = 1.0;
}
else
{
	// shadow
	shadowmultiplier = 0.0;
}

//- Main program for diverging lens ---------------------------------------------------------------------------------------------------------------------------------------
if (originalcolor[1] > 0.5)
{
	clrvalue = clrvalue*cutoff*colormultiplier*shadowmultiplier;
	gl_FragColor = vec4(clrvalue,clrvalue,clrvalue,1);
	
	//gl_FragColor = vec4((xunknown-bp1x)/(bp2x-bp1x),(xunknown-bp1x)/(bp2x-bp1x),(xunknown-bp1x)/(bp2x-bp1x),0);
	//gl_FragColor = vec4(PixelCoordX,PixelCoordX,PixelCoordX,0);
	//gl_FragColor = vec4(PixelCoordY,PixelCoordY,PixelCoordY,0);
	//gl_FragColor = vec4(texel.x,texel.x,texel.x,1.0);
	//gl_FragColor = vec4((I[0]+40)/80,(I[0]+40)/80,(I[0]+40)/80,1.0);
	//gl_FragColor = vec4((I[2]+20)/40,(I[2]+20)/40,(I[2]+20)/40,1.0);
	//gl_FragColor = vec4((V.y-/20,V.y/20,V.y/20,1.0);
	//gl_FragColor = vec4(V.z/40,V.z/40,V.z/40,1.0);
	//gl_FragColor = vec4((locz-V.z)/50,(locz-V.z)/50,(locz-V.z)/50,1.0);
	//gl_FragColor = vec4((locy-V.y)/20,(locy-V.y)/20,(locy-V.y)/20,1.0);
	//gl_FragColor = vec4(RealLength/200,RealLength/200,RealLength/200,1.0);
	//gl_FragColor = vec4(VE/100,VE/100,VE/100,1.0);
}
else
{
	gl_FragColor = vec4(0,0,0,alphavalue);
}
}
else //TELECENTRIC LENS - NOT CORRECT AT THIS MOMENT
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