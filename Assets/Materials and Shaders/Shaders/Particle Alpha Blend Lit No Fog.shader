Shader "Particles/Alpha Blended Lit No Fog" {
Properties {
	_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
	_MainTex ("Particle Texture", 2D) = "white" {}
	_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
	_FogStrength ("Fog Strength", float) = 1.0
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "ForwardBase"}
	Blend SrcAlpha OneMinusSrcAlpha
	AlphaTest Greater .01
	ColorMask RGB
	Cull Off Lighting Off ZWrite Off Fog {Mode Off}
	
	SubShader {
		Pass {
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_particles
			
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			fixed4 _TintColor;
			fixed4 _LightColor0;
			
			struct appdata_t 
			{
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f 
			{
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				#ifdef SOFTPARTICLES_ON
				float4 projPos : TEXCOORD1;
				#endif
				float4 vertLighting : TEXCOORD2;
			};
			
			float4 _MainTex_ST;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				#ifdef SOFTPARTICLES_ON
				o.projPos = ComputeScreenPos (o.vertex);
				COMPUTE_EYEDEPTH(o.projPos.z);
				#endif
				o.vertLighting = float4(_LightColor0.xyz*0.5,0) * (1 - _WorldSpaceLightPos0.w) + UNITY_LIGHTMODEL_AMBIENT * 2;
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}

			sampler2D _CameraDepthTexture;
			float _InvFade;
			
			fixed4 frag (v2f i) : COLOR
			{
				#ifdef SOFTPARTICLES_ON
				float sceneZ = LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos))));
				float partZ = i.projPos.z;
				float fade = saturate (_InvFade * (sceneZ-partZ));
				i.color.a *= fade;
				#endif
				
				return 2.0f * i.color * _TintColor * i.vertLighting * tex2D(_MainTex, i.texcoord);
			}
			ENDCG 
		}
		Pass
		{
			Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode" = "ForwardAdd"}
			Blend One One
			AlphaTest Greater .01
			ColorMask RGB
			Cull Off Lighting On ZWrite Off Fog { Color (0,0,0,0) }
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			
			uniform float4 _Color;
			uniform float4 _ObjPos;
			uniform float  _ZPosMult;
			uniform float4 _LightColor0;
			
			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};
			
			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float4 posWorld : TEXCOORD0;
				float3 normalDir : TEXCOORD1;
			};
			
			vertexOutput vert (vertexInput input)
			{
				vertexOutput o;
					
				float4x4 modelMatrix = _Object2World;
				float4x4 modelMatrixInverse = _World2Object;
					
				o.posWorld = mul(modelMatrix, input.vertex);
				o.normalDir = normalize( float3 ( mul( float4(input.normal, 0.0), modelMatrixInverse).xyz));
				o.pos = mul(UNITY_MATRIX_MVP, input.vertex);
				float4 posReorder = float4(o.posWorld.x * 0.2, o.posWorld.y * 0.2, o.posWorld.z * 0.05 , o.posWorld.w);
				o.posWorld = posReorder;
				return o;
				
			}
			
			float4 frag(vertexOutput i) : COLOR
			{				
				float3 fragmentToLight = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
				float  distanceToLight = length(fragmentToLight);
				float  atten = pow(2,-0.1*distanceToLight*distanceToLight);
				
				return float4 (_LightColor0*_WorldSpaceLightPos0.w * 2 * atten);
			}
			
			ENDCG
		}
	}	
}
}
