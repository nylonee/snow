Shader "Custom/Posterization"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_NumLevels("Num Levels", Int) = 5
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" }

		Cull Off
		ZWrite Off
		ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma target 3.0

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform int _NumLevels;

			struct v2f
			{
				float4 pos : SV_POSITION;
				float4 screenPos : TEXCOORD1;
			};

			v2f vert(appdata_base v)
			{

				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.screenPos = ComputeScreenPos(o.pos);
				return o;
			}

			float4 frag(v2f i) : COLOR
			{
				// Get current color
				float4 fragColor = tex2D(_MainTex, i.screenPos);

				// Posterize
				fragColor.r = (float)floor((fragColor.r * _NumLevels)) / _NumLevels;
				fragColor.g = (float)floor((fragColor.g * _NumLevels)) / _NumLevels;
				fragColor.b = (float)floor((fragColor.b * _NumLevels)) / _NumLevels;

				return fragColor;
			}
		ENDCG
		}
	}
}