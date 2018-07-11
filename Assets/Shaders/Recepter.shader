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
		_ShapeMask("ShapeMask", 2D) = "white" {}
		_BorderColor("BorderColor", Color) = (0.3843138,0.2196079,0.1058824,1)
		_BackgroundColor("BackgroundColor", Color) = (0,0,0,1)
		_BorderXOffset("BorderXOffset", Float) = 0
		_BorderYOffset("BorderYOffset", Float) = 0
		[Toggle]_KeepScale("KeepScale", Float) = 0
		_BordersWidth("BordersWidth", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		
		
		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
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
			uniform float4 _BordersWidth;
			uniform float _KeepScale;
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
				float4 temp_output_6_0_g11 = _BackgroundColor;
				float2 uv11_g11 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult100_g11 = (float2(( _BordersWidth.x + _BordersWidth.z ) , ( _BordersWidth.y + _BordersWidth.w )));
				float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
				float clampResult49_g5 = clamp( ase_objectScale.x , 0.0 , 1.0 );
				float clampResult50_g5 = clamp( ase_objectScale.y , 0.0 , 1.0 );
				float2 appendResult57_g5 = (float2(( ase_objectScale.x / clampResult50_g5 ) , 1.0));
				float2 clampResult61_g5 = clamp( (ase_objectScale).xy , float2( 1,1 ) , float2( 1000,1000 ) );
				float2 appendResult58_g5 = (float2(1.0 , ( ase_objectScale.y / clampResult49_g5 )));
				float2 ifLocalVar47_g5 = 0;
				if( clampResult49_g5 > clampResult50_g5 )
				ifLocalVar47_g5 = appendResult57_g5;
				else if( clampResult49_g5 == clampResult50_g5 )
				ifLocalVar47_g5 = clampResult61_g5;
				else if( clampResult49_g5 < clampResult50_g5 )
				ifLocalVar47_g5 = appendResult58_g5;
				float2 temp_output_7_0_g11 = ifLocalVar47_g5;
				float2 break89_g11 = temp_output_7_0_g11;
				float2 appendResult88_g11 = (float2(break89_g11.x , break89_g11.x));
				float2 appendResult91_g11 = (float2(break89_g11.y , break89_g11.y));
				float2 ifLocalVar90_g11 = 0;
				if( break89_g11.x >= break89_g11.y )
				ifLocalVar90_g11 = appendResult88_g11;
				else
				ifLocalVar90_g11 = appendResult91_g11;
				float2 lerpResult95_g11 = lerp( temp_output_7_0_g11 , ifLocalVar90_g11 , _KeepScale);
				float2 ScaleRatio8_g11 = lerpResult95_g11;
				float2 break12_g11 = ( ( appendResult100_g11 / ScaleRatio8_g11 ) + 1.0 );
				float2 appendResult17_g11 = (float2(( uv11_g11.x * break12_g11.x ) , ( uv11_g11.y * break12_g11.y )));
				float2 appendResult101_g11 = (float2(_BordersWidth.x , _BordersWidth.y));
				float2 BorderCoordinates102_g11 = ( appendResult101_g11 / ScaleRatio8_g11 );
				float2 appendResult18_g11 = (float2(_BorderXOffset , _BorderYOffset));
				float2 temp_output_23_0_g11 = ( ( appendResult17_g11 - BorderCoordinates102_g11 ) + ( appendResult18_g11 / ScaleRatio8_g11 ) );
				float2 break60_g11 = step( float2( 0,0 ) , ( 1.0 - temp_output_23_0_g11 ) );
				float2 break61_g11 = step( float2( 0,0 ) , temp_output_23_0_g11 );
				float clampResult68_g11 = clamp( ( tex2D( _ShapeMask, temp_output_23_0_g11 ).r + ( 1.0 - ( break60_g11.x * break60_g11.y * break61_g11.x * break61_g11.y ) ) ) , 0.0 , 1.0 );
				float2 uv_ShapeMask = IN.texcoord.xy * _ShapeMask_ST.xy + _ShapeMask_ST.zw;
				float clampResult34_g11 = clamp( ( (temp_output_6_0_g11).a * ( 1.0 - tex2D( _ShapeMask, uv_ShapeMask ).r ) * _BorderColor.a ) , 0.0 , 1.0 );
				float4 appendResult38_g11 = (float4((( float4( ( (temp_output_6_0_g11).rgb * ( 1.0 - clampResult68_g11 ) ) , 0.0 ) + ( _BorderColor * clampResult68_g11 ) )).rgb , clampResult34_g11));
				
				fixed4 c = appendResult38_g11;
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=15401
-1709;102;1627;892;1049.007;381.5017;1;True;False
Node;AmplifyShaderEditor.FunctionNode;6;-428.5,157;Float;False;ScalingRatio;-1;;5;096dc3062cc962b49b5311cdb37df263;0;0;3;FLOAT2;0;FLOAT;27;FLOAT;28
Node;AmplifyShaderEditor.ColorNode;2;-442.5,-119;Float;False;Property;_BackgroundColor;BackgroundColor;1;0;Create;True;0;0;False;0;0,0,0,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;12;-177.5,19;Float;False;AddBorder;0;;11;e22a0ad25ec970f4d94f3a43eb80ec65;0;2;6;COLOR;0,0,0,0;False;7;FLOAT2;1,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;5
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;1;14,19;Float;False;True;2;Float;ASEMaterialInspector;0;5;ZooCube/Recepter;0f8ba0101102bb14ebf021ddadce9b49;0;0;;2;True;3;1;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;True;2;False;-1;False;False;True;2;False;-1;False;False;True;5;Queue=Transparent;IgnoreProjector=True;RenderType=Transparent;PreviewType=Plane;CanUseSpriteAtlas=True;False;0;False;False;False;False;False;False;False;False;False;True;2;0;;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;12;6;2;0
WireConnection;12;7;6;0
WireConnection;1;0;12;0
ASEEND*/
//CHKSM=DC78F62B58036F62D5F1BE96E9BF998E9742A409