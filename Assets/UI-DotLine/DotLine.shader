Shader "Custom/UI/DotLine"
{
    Properties
    {
        _Color ("Tint", Color) = (1,1,1,1)
		_Cnt ("Cnt", float) = 100
		_Ratio ("Ratio", Range(0, 1.0)) = 0.5
		[Toggle(VERTICAL)] _Y ("Y？", float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend One OneMinusSrcAlpha

        Pass
        {
            Name "Default"
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
			#pragma multi_compile __ VERTICAL

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                float2 texcoord  : TEXCOORD0;
            };

            fixed4 _Color;
			float _Cnt;
			fixed _Ratio;

            v2f vert(appdata_t v)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(v.vertex);
                OUT.texcoord = v.texcoord;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
				fixed4 color = _Color;

				#if VERTICAL

				float y = IN.texcoord.y * _Cnt;
				int intY = int(y);
				color.a *= step(y-intY, _Ratio);

				#else

				float x = IN.texcoord.x * _Cnt;
				int intX = int(x);
				color.a *= step(x-intX, _Ratio);

				#endif

				color.rgb *= color.a;
                return color;
            }
        ENDCG
        }
    }
}
