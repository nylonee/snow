Shader "Custom/Terrain"
{
	Properties
	{
		_Tint("Tint", Color) = (0, 0, 0, 0)
		_TerrainTexture("TerrainTexture", 2D) = "bump" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque" }

		CGPROGRAM
		#pragma surface surf Lambert

		struct Input
		{
			float2 uv_TerrainTexture;
		};

		float4 _Tint;
		sampler2D _TerrainTexture;

		void surf(Input IN, inout SurfaceOutput o)
		{
			float3 tintColor = _Tint.rgb;
			float tintAlpha = _Tint.a;

			float4 tex = tex2D(_TerrainTexture, IN.uv_TerrainTexture);
			float3 texColor = tex.rgb;
			float texAlpha = tex.a;

			o.Albedo = lerp(texColor, tintColor, tintAlpha/(texAlpha + tintAlpha));
			o.Normal = UnpackNormal(tex2D(_TerrainTexture, IN.uv_TerrainTexture));
		}

		ENDCG
	}

	Fallback "Diffuse"
}
