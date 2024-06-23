sampler uImage0 : register(s0);
float2 RectLT;
float2 RectRB;
float2 MatX;
float2 MatY;
float2 MatZ;
float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    //float3 trans = mul(float3x3(MatX.x, MatY.x, MatZ.x,MatX.y, MatY.y, MatZ.y,0     , 0     , 1),float3(coords.x,coords.y,1));
    //coords = float2(trans.x, trans.y);
    float2 transed = MatX * coords.x + MatY * coords.y + MatZ;
    if (transed.x >= RectLT.x && transed.x < RectRB.x && transed.y >= RectLT.y && transed.y < RectRB.y)
    {
        return tex2D(uImage0, coords);
    }
    else
    {
        return float4(255, 0, 0, 127) * tex2D(uImage0, coords);
    }
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}