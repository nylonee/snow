Shader "Custom/FogShader"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_FogStart("Fog Start", Float) = 0.5
		_FogColor("Fog Color", Color) = (1, 1, 1, 1)
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
			#pragma fragment frag#
			#pragma target 3.0

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform sampler2D _CameraDepthNormalsTexture;
			uniform float _FogStart;
			uniform float4 _FogColor;

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

				// May have to invert y for depth texture
				float4 screenPosDepth = i.screenPos;
				//#if UNITY_UV_STARTS_AT_TOP
				//screenPosDepth.y = 1 - screenPosDepth.y;
				//#endif

				// Get depth value from 0-1
				float depthValue = DecodeFloatRG(tex2D(_CameraDepthNormalsTexture, screenPosDepth.xy).zw);

				// If past starting point for fog, linearly interpolate to grey (ending at max camera depth)
				if(depthValue > _FogStart)
				{
					fragColor = lerp(fragColor, _FogColor, (depthValue - _FogStart) / (1.0 - _FogStart));
				}

				return fragColor;
			}
		ENDCG
		}
	}
	// Required to generate depth texture for image postprocessing
	// See: https://docs.unity3d.com/Manual/SL-CameraDepthTexture.html
	FallBack "Diffuse"
}