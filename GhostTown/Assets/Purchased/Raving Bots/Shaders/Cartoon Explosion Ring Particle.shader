Shader "FX/Cartoon Explosion/Ring Particle"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_TintColor ("Particle Tint", Color) = (1,1,1,0.5)
		
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
		[HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
		[PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		[PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
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
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex TintVert
			#pragma fragment MaskedFrag
			#pragma target 2.0
			#pragma multi_compile_instancing
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnitySprites.cginc"
			
			fixed4 _TintColor;
			
			v2f TintVert(appdata_t IN)
			{
				v2f OUT;

				UNITY_SETUP_INSTANCE_ID (IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
			
				OUT.vertex = UnityFlipSprite(IN.vertex, _Flip);
				OUT.vertex = UnityObjectToClipPos(OUT.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _TintColor * _RendererColor;
			
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif
			
				return OUT;
			}
			
			fixed SampleMask(float2 texcoord, fixed a)
			{
				texcoord = texcoord/a - (0.5/a - 0.5) * float2(1, 1);
				
				if (texcoord.x < 0 || texcoord.x > 1 || texcoord.y < 0 || texcoord.y > 1)
					return 0;
				
				return SampleSpriteTexture(texcoord).a;
			}
			
			fixed4 MaskedFrag(v2f IN) : SV_Target
			{
				fixed a = IN.color.a < 1 ? SampleMask(IN.texcoord, 1 - IN.color.a) : 0;
				IN.color.a = 1;
			
				fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
				
				c.a -= a;
				
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
}
