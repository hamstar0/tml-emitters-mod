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

float Time;

float Frame;
float FrameMax;
float4 UserColor;



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

	float frameHeightPerc = 1.0 / FrameMax;
	float frameYPosPerc = min( (coords.texPos.y - (frameHeightPerc * Frame)) / frameHeightPerc, 1.0 );

    float texY = frameYPosPerc * TexHeight;
    float rowDark = 1 - floor( texY % 2.0 );

	float timeWave = max( frac(Time), 0.001 );
	float wave = frac( frameYPosPerc / timeWave );
	wave = 0.5 + (wave * 0.5);
    
	output.color = UserColor * color * rowDark * wave;
    
	return output;
}



technique Technique1 {
	pass P0 {
		PixelShader = compile ps_2_0 ScanlinesCRT();
	}
};