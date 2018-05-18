Shader /*ase_name*/"ZooCube/Sprites"/*end*/
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		/*ase_props*/
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
			/*ase_tags*/
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		/*ase_pass*/
		
		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#include "UnityCG.cginc"
			/*ase_pragma*/

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				/*ase_vdata:p=p;uv0=tc0.xy;c=c*/
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
				/*ase_interp(1,7):sp=sp.xyzw;uv0=tc0.xy;c=c*/
			};
			
			uniform sampler2D _MainTex;
			uniform sampler2D _AlphaTex;
			/*ase_globals*/
			
			v2f vert( appdata_t IN /*ase_vert_input*/ )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				/*ase_vert_code:IN=appdata_t;OUT=v2f*/
				
				IN.vertex.xyz += /*ase_vert_out:Offset;Float3*/ float3(0,0,0) /*end*/; 
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;

				return OUT;
			}

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

				return color;
			}
			
			fixed4 frag(v2f IN /*ase_frag_input*/ ) : SV_Target
			{
				/*ase_frag_code:IN=v2f*/
				fixed4 c = /*ase_frag_out:Color;Float4*/SampleSpriteTexture (IN.texcoord) * IN.color/*end*/;
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
}
