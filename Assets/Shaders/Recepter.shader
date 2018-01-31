// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ZooCube/Recepter"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		[Header(AddBorder)]
		_ShapeMask("ShapeMask", 2D) = "white" {}
		_BorderColor("BorderColor", Color) = (0.3843138,0.2196079,0.1058824,1)
		_BorderWidth("BorderWidth", Range( 0 , 1)) = 0.1
		_BorderXOffset("BorderXOffset", Range( -0.1 , 0.1)) = 0
		_BorderYOffset("BorderYOffset", Range( -0.1 , 0.1)) = 0
		_BackgroundColor("BackgroundColor", Color) = (0,0,0,1)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
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
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnityCG.cginc"


			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
			};
			
			uniform fixed4 _Color;
			uniform float _EnableExternalAlpha;
			uniform sampler2D _MainTex;
			uniform sampler2D _AlphaTex;
			uniform float4 _BackgroundColor;
			uniform sampler2D _ShapeMask;
			uniform float _BorderWidth;
			uniform float _BorderXOffset;
			uniform float _BorderYOffset;
			uniform float4 _BorderColor;
			uniform float4 _ShapeMask_ST;
			
			v2f vert( appdata_t IN  )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				
				IN.vertex.xyz +=  float3(0,0,0) ; 
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if ETC1_EXTERNAL_ALPHA
				// get the color from an external texture (usecase: Alpha support for ETC1 on android)
				fixed4 alpha = tex2D (_AlphaTex, uv);
				color.a = lerp (color.a, alpha.r, _EnableExternalAlpha);
#endif //ETC1_EXTERNAL_ALPHA

				return color;
			}
			
			fixed4 frag(v2f IN  ) : SV_Target
			{
				float4 temp_output_6_0_g2 = _BackgroundColor;
				float2 uv11_g2 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
				float clampResult49_g3 = clamp( ase_objectScale.x , 0.0 , 1.0 );
				float clampResult50_g3 = clamp( ase_objectScale.y , 0.0 , 1.0 );
				float2 appendResult57_g3 = (float2(( ase_objectScale.x / clampResult50_g3 ) , 1.0));
				float2 appendResult58_g3 = (float2(1.0 , ( ase_objectScale.y / clampResult49_g3 )));
				float2 ifLocalVar47_g3 = 0;
				if( clampResult49_g3 > clampResult50_g3 )
				ifLocalVar47_g3 = appendResult57_g3;
				else if( clampResult49_g3 == clampResult50_g3 )
				ifLocalVar47_g3 = (ase_objectScale).xy;
				else if( clampResult49_g3 < clampResult50_g3 )
				ifLocalVar47_g3 = appendResult58_g3;
				float2 ScaleRatio8_g2 = ifLocalVar47_g3;
				float2 temp_output_10_0_g2 = ( _BorderWidth / ScaleRatio8_g2 );
				float2 appendResult17_g2 = (float2(( uv11_g2.x * ( temp_output_10_0_g2 + float2( 1,1 ) ).x ) , ( uv11_g2.y * ( temp_output_10_0_g2 + float2( 1,1 ) ).y )));
				float2 appendResult18_g2 = (float2(_BorderXOffset , _BorderYOffset));
				float4 tex2DNode24_g2 = tex2D( _ShapeMask, ( ( appendResult17_g2 - ( temp_output_10_0_g2 / float2( 2,2 ) ) ) + ( appendResult18_g2 / ScaleRatio8_g2 ) ) );
				float2 uv_ShapeMask = IN.texcoord.xy * _ShapeMask_ST.xy + _ShapeMask_ST.zw;
				float clampResult34_g2 = clamp( ( (temp_output_6_0_g2).a * ( 1.0 - tex2D( _ShapeMask, uv_ShapeMask ).r ) * _BorderColor.a ) , 0.0 , 1.0 );
				float4 appendResult38_g2 = (float4((( float4( ( (temp_output_6_0_g2).rgb * ( 1.0 - tex2DNode24_g2.r ) ) , 0.0 ) + ( _BorderColor * tex2DNode24_g2 ) )).rgb , clampResult34_g2));
				
				fixed4 c = appendResult38_g2;
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14201
-1781;84;1627;890;1068.5;393;1;True;False
Node;AmplifyShaderEditor.FunctionNode;5;-428.5,157;Float;False;ScalingRatio;-1;;3;096dc3062cc962b49b5311cdb37df263;0;3;FLOAT2;0;FLOAT;27;FLOAT;28
Node;AmplifyShaderEditor.ColorNode;2;-442.5,-119;Float;False;Property;_BackgroundColor;BackgroundColor;6;0;Create;0,0,0,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;4;-177.5,19;Float;False;AddBorder;0;;2;e22a0ad25ec970f4d94f3a43eb80ec65;2;6;COLOR;0,0,0,0;False;7;FLOAT2;1,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;5
Node;AmplifyShaderEditor.TemplateMasterNode;1;14,19;Float;False;True;2;Float;ASEMaterialInspector;0;4;ZooCube/Recepter;0f8ba0101102bb14ebf021ddadce9b49;Sprites Default;3;One;OneMinusSrcAlpha;0;One;Zero;Off;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;4;6;2;0
WireConnection;4;7;5;0
WireConnection;1;0;4;0
ASEEND*/
//CHKSM=A1EFD6CFD8678A20D67F4FED6FDC9CE443FC1EA9