sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float3 uSecondaryColor;
float uOpacity;
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;
int count;
float4 Scanlines(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float wave = 1 - frac(coords.x + uTime * 0.5f);
    float pixelY = (coords.y * wave) * 100;
    if (coords.y != 0 && wave == 0 && color.a != 0)
    {
       color.rgb = 0;
       color.a = 0.25;
    }
    return color * sampleColor * sampleColor.a;
}

technique Technique1
{
    pass Scanlines
    {
        PixelShader = compile ps_2_0 Scanlines();
    }
}