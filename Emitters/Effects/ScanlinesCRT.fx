#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif


sampler2D SpriteTextureSampler;
float TexWidth;
float TexHeight;
float RandValue;

float CyclePercent;

float Frame;
float FrameMax;
float4 UserColor;
float WaveScale;



struct PixelShaderInput {
	float2 texPos : TEXCOORD0;
	float4 color : COLOR;
};

struct PixelShaderOutput {
	float4 color : SV_TARGET0;
};



PixelShaderOutput ScanlinesCRT( PixelShaderInput coords ) {
	PixelShaderOutput output;
	float4 color = tex2D( SpriteTextureSampler, coords.texPos );

	float frameStartPerc = Frame / FrameMax;
	float frameHeight = TexHeight / FrameMax;
	float outFrameYPosStart = TexHeight * frameStartPerc;
	float outFrameYPosCurr = TexHeight * coords.texPos.y;
	float inFrameYPosCurr = outFrameYPosCurr - outFrameYPosStart;
	float inFrameYPosPerc = inFrameYPosCurr / frameHeight;

    float rowBlinds = floor( inFrameYPosCurr % 2.0 );

	float cyclePerc = max( frac(CyclePercent), 0.001 );
	float wave = frac( inFrameYPosPerc / cyclePerc );
	float scaledWave = (1.0 - WaveScale) + (wave * WaveScale);
    
	output.color = UserColor * color * rowBlinds * scaledWave;
	//output.color = output.color * UserColor.W;	//?
    
	return output;
}



technique Technique1 {
	pass P0 {
		PixelShader = compile ps_2_0 ScanlinesCRT();
	}
};