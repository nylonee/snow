Shader "Custom/Posterization"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_NumLevels("Num Levels", Int) = 5
		_IsOutlineEnabled("Is Outline Enabled", Int) = 1
		_OutlineNormalThreshold("OutlineNormalThreshold", Float) = 0.2
		_OutlineDepthThreshold("OutlineDepthThreshold", Float) = 0.2
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
			uniform half4 _MainTex_TexelSize;
			uniform sampler2D _CameraDepthNormalsTexture;
			uniform int _NumLevels;
			uniform int _IsOutlineEnabled;
			uniform float _OutlineNormalThreshold;
			uniform float _OutlineDepthThreshold;

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

				// ---- Posterize ----
				fragColor.r = (float)floor((fragColor.r * _NumLevels)) / _NumLevels;
				fragColor.g = (float)floor((fragColor.g * _NumLevels)) / _NumLevels;
				fragColor.b = (float)floor((fragColor.b * _NumLevels)) / _NumLevels;

				if(_IsOutlineEnabled < 1)
				{
					return fragColor;
				}

				// ---- Outline ----
				// May have to invert y for depth texture
				float4 screenPosDepth = i.screenPos;
				#if UNITY_UV_STARTS_AT_TOP
				screenPosDepth.y = 1 - screenPosDepth.y;
				#endif

				float depthValue;
				float3 normalValues;
				DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, screenPosDepth.xy), depthValue, normalValues);

				float2 leftPos = float2(screenPosDepth.x + _MainTex_TexelSize.x, screenPosDepth.y);
				float2 topLeftPos = screenPosDepth.xy + _MainTex_TexelSize.xy;
				float2 topPos = float2(screenPosDepth.x, screenPosDepth.y + _MainTex_TexelSize.y);

				float leftDepthValue;
				float3 leftNormalValues;
				DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, leftPos), leftDepthValue, leftNormalValues);

				float topLeftDepthValue;
				float3 topLeftNormalValues;
				DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, topLeftPos), topLeftDepthValue, topLeftNormalValues);

				float topDepthValue;
				float3 topNormalValues;
				DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, topPos), topDepthValue, topNormalValues);

				if (   abs(depthValue - leftDepthValue) > _OutlineDepthThreshold
					|| abs(depthValue - topLeftDepthValue) > _OutlineDepthThreshold
					|| abs(depthValue - topDepthValue) > _OutlineDepthThreshold
					|| distance(normalValues, leftNormalValues) > _OutlineNormalThreshold
					|| distance(normalValues, topLeftNormalValues) > _OutlineNormalThreshold
					|| distance(normalValues, topNormalValues) > _OutlineNormalThreshold)
				{
					fragColor = float4(0, 0, 0, 1);
				}

				// End outline

				return fragColor;
			}
		ENDCG
		}
	}
	// Required to generate depth texture for image postprocessing
	// See: https://docs.unity3d.com/Manual/SL-CameraDepthTexture.html
	FallBack "Diffuse"
}