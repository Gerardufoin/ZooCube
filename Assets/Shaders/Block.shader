// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ZooCube/Block"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		_Face("Face", 2D) = "white" {}
		_BackgroundColor("BackgroundColor", Color) = (0,0,0,0)
		_ShapeMask("ShapeMask", 2D) = "white" {}
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
			uniform sampler2D _Face;
			uniform float4 _Face_ST;
			uniform float4 _BackgroundColor;
			uniform sampler2D _ShapeMask;
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
				float2 uv_Face = IN.texcoord.xy * _Face_ST.xy + _Face_ST.zw;
				float4 tex2DNode4 = tex2D( _Face, uv_Face );
				float temp_output_12_0 = ( 1.0 - tex2DNode4.a );
				float2 uv_ShapeMask = IN.texcoord.xy * _ShapeMask_ST.xy + _ShapeMask_ST.zw;
				float clampResult17 = clamp( ( ( ( tex2DNode4.a * IN.color.a ) + ( temp_output_12_0 * _BackgroundColor.a ) ) * ( 1.0 - tex2D( _ShapeMask, uv_ShapeMask ).r ) ) , 0.0 , 1.0 );
				float4 appendResult19 = (float4((( ( tex2DNode4 * IN.color * tex2DNode4.a ) + ( _BackgroundColor * temp_output_12_0 ) )).rgb , clampResult17));
				
				fixed4 c = appendResult19;
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
0;92;1311;593;2042.97;163.4015;1.997942;True;True
Node;AmplifyShaderEditor.SamplerNode;4;-1394.203,-32.06759;Float;True;Property;_Face;Face;0;0;Create;2e5f3a3c9a063494b924449bc184dd90;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;9;-1347.287,463.6196;Float;False;Property;_BackgroundColor;BackgroundColor;2;0;Create;0,0,0,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;12;-1009.43,201.8466;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;6;-1638.221,906.7415;Float;True;Property;_ShapeMask;ShapeMask;2;0;Create;4f9aa590ffa5c5f4eb66d6bf9c21c2c7;None;False;white;Auto;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.VertexColorNode;22;-1335.677,224.1994;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-872.2039,504.3033;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-1012.231,305.466;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;5;-1328.737,754.5868;Float;True;Property;_TextureSample1;Texture Sample 1;0;0;Create;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-877.8051,42.21663;Float;False;3;3;0;COLOR;0.0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;15;-718.1753,383.8807;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;7;-1025.275,782.7076;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-777.7087,211.0967;Float;False;2;2;0;COLOR;0.0;False;1;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-502.5349,560.3138;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;21;-596.5172,192.5495;Float;False;2;2;0;COLOR;0.0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;17;-297.3566,560.3137;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;18;-490.5927,67.42131;Float;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;19;-256.7247,111.5467;Float;False;FLOAT4;4;0;FLOAT3;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMasterNode;1;-68,14;Float;False;True;2;Float;ASEMaterialInspector;0;4;ZooCube/Block;0f8ba0101102bb14ebf021ddadce9b49;Sprites Default;3;One;OneMinusSrcAlpha;0;One;Zero;Off;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;12;0;4;4
WireConnection;14;0;12;0
WireConnection;14;1;9;4
WireConnection;13;0;4;4
WireConnection;13;1;22;4
WireConnection;5;0;6;0
WireConnection;11;0;4;0
WireConnection;11;1;22;0
WireConnection;11;2;4;4
WireConnection;15;0;13;0
WireConnection;15;1;14;0
WireConnection;7;0;5;1
WireConnection;20;0;9;0
WireConnection;20;1;12;0
WireConnection;16;0;15;0
WireConnection;16;1;7;0
WireConnection;21;0;11;0
WireConnection;21;1;20;0
WireConnection;17;0;16;0
WireConnection;18;0;21;0
WireConnection;19;0;18;0
WireConnection;19;3;17;0
WireConnection;1;0;19;0
ASEEND*/
//CHKSM=EFC12857A64E216EBE0282287FF0286D18A23F04