#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

sampler2D SpriteTextureSampler;
float uTime;

struct PixelShaderInput
{
	float2 texPos : TEXCOORD0;
	float4 color : COLOR;
};

struct PixelShaderOutput
{
	float4 color : SV_TARGET0;
};


float TexWidth;
float TexHeight;
float RandValue;



PixelShaderOutput ScanlinesPS(PixelShaderInput coords)
{
	PixelShaderOutput output;
	float4 color = tex2D(SpriteTextureSampler, coords.texPos);
    float wave = 1 - frac(coords.texPos.y * TexHeight + uTime * 0.5f);
    float texY = coords.texPos.y * TexHeight;
    float rowDark = floor(texY % 0.5);
	float voidRand = 0.5 * RandValue;
	float fillRand = 1.0 - voidRand;
    float rowDarkRand = ((rowDark + wave) * fillRand) + voidRand;
    
	output.color = color * rowDarkRand;
    
	return output;
}



technique OverlayDraw
{
	pass P0
	{
		PixelShader = compile ps_2_0 ScanlinesPS();
	}
};