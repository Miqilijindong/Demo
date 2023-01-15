Shader "Custom/Geometry/GeoDotLine"
{
	Properties
	{
		_MainTex ("Main Tex", 2D) = "white" {}
		_LineColor ("Line Color", color) = (0, 1, 0, 1)
		_LineWidth ("Line Width", float) = 2
		_Cnt ("Cnt", float) = 30
		_Ratio ("Ratio", Range(0, 1.0)) = 0.5
	}

SubShader
{
	Cull Off
	Blend SrcAlpha OneMinusSrcAlpha
	Tags {"Queue" = "Transparent"}

	LOD 100
	Pass
	{
		CGPROGRAM
		#pragma target 4.0
		#pragma vertex vert
		#pragma geometry geo
		#pragma fragment frag

		#include "UnityCG.cginc"

		struct appdata
		{
			float4 vertex : POSITION;
			float2 uv : TEXCOORD0;
		};

		struct v2f
		{
			float2 uv : TEXCOORD0;
			float4 vertex : SV_POSITION;
		};

		sampler2D _MainTex;
		float4 _MainTex_ST;
		fixed4 _LineColor;
		float _LineWidth;
		float _Cnt;
		float _Ratio;

		v2f vert (appdata v)
		{
			v2f o;
			o.vertex = v.vertex;
			o.uv = v.uv;
			return o;
		}

		//产生最大的顶点数，append到triStream的次数应该小于此。最大好像是64
		[maxvertexcount(6)]
		void geo(line appdata l[2], inout TriangleStream<v2f> triStream)
		{
			v2f pIn;
			v2f v[4];								//	先生成四个顶点，构造两个三角形

			float4 a = l[0].vertex;
			float4 b = l[1].vertex;

			float halfLineWidth =  0.5 * _LineWidth;

			v[0].vertex = UnityObjectToClipPos(a + float4(halfLineWidth, 0, 0, 0));
			v[0].uv = float2(0.0f, 1.0f);
			v[1].vertex = UnityObjectToClipPos(b + float4(halfLineWidth, 0, 0, 0));
			v[1].uv = float2(1.0f, 1.0f);
			v[2].vertex = UnityObjectToClipPos(b + float4(-halfLineWidth, 0, 0, 0));
			v[2].uv = float2(1.0f, 0.0f);
			v[3].vertex = UnityObjectToClipPos(a + float4(-halfLineWidth, 0, 0, 0));
			v[3].uv = float2(0.0f, 0.0f);

			triStream.Append(v[0]);
			triStream.Append(v[1]);
			triStream.Append(v[2]);
			triStream.RestartStrip();			// 重置三角形计数，提交三角形
			triStream.Append(v[2]);
			triStream.Append(v[3]);
			triStream.Append(v[0]);
			triStream.RestartStrip();
		}

		fixed4 frag (v2f i) : SV_Target
		{
			fixed4 color = _LineColor;
			float x = i.uv.x * _Cnt;
			int intX = int(x);
			color.a *= step(x-intX, _Ratio);
			return color;
		}
		ENDCG
		}
	}
}