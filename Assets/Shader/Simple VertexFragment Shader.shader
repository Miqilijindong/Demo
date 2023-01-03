// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Simple VertexFragment Shader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex("InputTex", 2D) = "white" {}
    }

    SubShader
    {
        Blend One Zero

        Pass
        {
            Name "Simple VertexFragment Shader"

            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            float4      _Color;
            sampler2D   _MainTex;

            float4 vert(float4 v : POSITION) : SV_POSITION 
            {
                return UnityObjectToClipPos (v);
            }

            float4 frag(v2f_customrendertexture IN) : COLOR
            {
                float2 uv = IN.localTexcoord.xy;
                float4 color = tex2D(_MainTex, uv) * _Color;

                // TODO: Replace this by actual code!
                uint2 p = uv.xy * 256;
                return countbits(~(p.x & p.y) + 1) % 2 * float4(uv, 1, 1) * color;
            }
            ENDCG
        }
    }
}
