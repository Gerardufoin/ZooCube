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
		[Header(AddBorder)]
		_ShapeMask("ShapeMask", 2D) = "white" {}
		_BorderColor("BorderColor", Color) = (0.3843138,0.2196079,0.1058824,1)
		_BorderWidth("BorderWidth", Range( 0 , 1)) = 0.1
		_BorderXOffset("BorderXOffset", Range( -0.1 , 0.1)) = 0
		_BorderYOffset("BorderYOffset", Range( -0.1 , 0.1)) = 0
		_ImageScale("ImageScale", Range( 0.1 , 2)) = 1
		_Face("Face", 2D) = "white" {}
		_BackgroundColor("BackgroundColor", Color) = (0.6313726,0.4,0.2196079,1)
		_ImageXOffset("ImageXOffset", Range( -1 , 1)) = 0
		_ImageYOffset("ImageYOffset", Range( -1 , 1)) = 0
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
			uniform float _ImageScale;
			uniform float _ImageXOffset;
			uniform float _ImageYOffset;
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
				float2 uv66 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
				float clampResult49_g22 = clamp( ase_objectScale.x , 0.0 , 1.0 );
				float clampResult50_g22 = clamp( ase_objectScale.y , 0.0 , 1.0 );
				float2 appendResult57_g22 = (float2(( ase_objectScale.x / clampResult50_g22 ) , 1.0));
				float2 appendResult58_g22 = (float2(1.0 , ( ase_objectScale.y / clampResult49_g22 )));
				float2 ifLocalVar47_g22 = 0;
				if( clampResult49_g22 > clampResult50_g22 )
				ifLocalVar47_g22 = appendResult57_g22;
				else if( clampResult49_g22 == clampResult50_g22 )
				ifLocalVar47_g22 = (ase_objectScale).xy;
				else if( clampResult49_g22 < clampResult50_g22 )
				ifLocalVar47_g22 = appendResult58_g22;
				float2 ScaleRatio78 = ifLocalVar47_g22;
				float2 temp_output_62_0 = ( ScaleRatio78 / _ImageScale );
				float2 appendResult69 = (float2(( uv66.x * temp_output_62_0.x ) , ( uv66.y * temp_output_62_0.y )));
				float2 appendResult73 = (float2(_ImageXOffset , _ImageYOffset));
				float4 tex2DNode4 = tex2D( _Face, ( ( appendResult69 - ( ( temp_output_62_0 - float2( 1,1 ) ) / float2( 2,2 ) ) ) - appendResult73 ) );
				float temp_output_12_0 = ( 1.0 - tex2DNode4.a );
				float4 appendResult77 = (float4(( ( tex2DNode4 * IN.color * tex2DNode4.a ) + ( _BackgroundColor * temp_output_12_0 ) ).rgb , ( ( tex2DNode4.a * IN.color.a ) + ( temp_output_12_0 * _BackgroundColor.a ) )));
				float4 temp_output_6_0_g16 = appendResult77;
				float2 uv11_g16 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 ScaleRatio8_g16 = ScaleRatio78;
				float2 temp_output_10_0_g16 = ( _BorderWidth / ScaleRatio8_g16 );
				float2 appendResult17_g16 = (float2(( uv11_g16.x * ( temp_output_10_0_g16 + float2( 1,1 ) ).x ) , ( uv11_g16.y * ( temp_output_10_0_g16 + float2( 1,1 ) ).y )));
				float2 appendResult18_g16 = (float2(_BorderXOffset , _BorderYOffset));
				float4 tex2DNode24_g16 = tex2D( _ShapeMask, ( ( appendResult17_g16 - ( temp_output_10_0_g16 / float2( 2,2 ) ) ) + ( appendResult18_g16 / ScaleRatio8_g16 ) ) );
				float2 uv_ShapeMask = IN.texcoord.xy * _ShapeMask_ST.xy + _ShapeMask_ST.zw;
				float clampResult34_g16 = clamp( ( (temp_output_6_0_g16).a * ( 1.0 - tex2D( _ShapeMask, uv_ShapeMask ).r ) * _BorderColor.a ) , 0.0 , 1.0 );
				float4 appendResult38_g16 = (float4((( float4( ( (temp_output_6_0_g16).rgb * ( 1.0 - tex2DNode24_g16.r ) ) , 0.0 ) + ( _BorderColor * tex2DNode24_g16 ) )).rgb , clampResult34_g16));
				
				fixed4 c = appendResult38_g16;
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
-1781;84;1627;890;2757.201;417.7562;1.659961;True;False
Node;AmplifyShaderEditor.FunctionNode;96;-740.7661,726.5918;Float;False;ScalingRatio;-1;;22;096dc3062cc962b49b5311cdb37df263;0;3;FLOAT2;0;FLOAT;27;FLOAT;28
Node;AmplifyShaderEditor.RangedFloatNode;61;-3029.872,-25.54801;Float;False;Property;_ImageScale;ImageScale;6;0;Create;1;0;0.1;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;60;-2950.392,-140.3514;Float;False;78;0;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;78;-479.583,619.7949;Float;False;ScaleRatio;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;62;-2722.993,-89.5729;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.BreakToComponentsNode;65;-2500.01,-195.5454;Float;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.TextureCoordinatesNode;66;-2486.763,-332.4263;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;68;-2197.547,-195.5454;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;67;-2193.132,-308.141;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;63;-2506.786,3.152842;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;1,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;64;-2195.339,3.152842;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;2,2;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;72;-2122.484,212.8895;Float;False;Property;_ImageYOffset;ImageYOffset;10;0;Create;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;69;-2034.173,-255.1548;Float;False;FLOAT2;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;71;-2120.276,124.5792;Float;False;Property;_ImageXOffset;ImageXOffset;9;0;Create;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;73;-1844.306,162.111;Float;False;FLOAT2;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;70;-1822.229,-153.598;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;74;-1574.959,-3.470398;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;4;-1394.203,-32.06759;Float;True;Property;_Face;Face;7;0;Create;2e5f3a3c9a063494b924449bc184dd90;2e5f3a3c9a063494b924449bc184dd90;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;22;-1335.677,224.1994;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;9;-1349.593,456.7012;Float;False;Property;_BackgroundColor;BackgroundColor;8;0;Create;0.6313726,0.4,0.2196079,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;12;-1024.554,252.2601;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-794.5129,264.8711;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-803.8657,32.13393;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-797.134,404.6125;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-799.9449,531.1904;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;21;-578.4736,124.8902;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;15;-588.7813,407.407;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;77;-405.8012,299.591;Float;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.FunctionNode;88;-185.6634,383.2658;Float;False;AddBorder;0;;16;e22a0ad25ec970f4d94f3a43eb80ec65;2;6;COLOR;0,0,0,0;False;7;FLOAT2;1,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;5
Node;AmplifyShaderEditor.TemplateMasterNode;1;30.08085,387.2263;Float;False;True;2;Float;ASEMaterialInspector;0;4;ZooCube/Block;0f8ba0101102bb14ebf021ddadce9b49;Sprites Default;3;One;OneMinusSrcAlpha;0;One;Zero;Off;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;78;0;96;0
WireConnection;62;0;60;0
WireConnection;62;1;61;0
WireConnection;65;0;62;0
WireConnection;68;0;66;2
WireConnection;68;1;65;1
WireConnection;67;0;66;1
WireConnection;67;1;65;0
WireConnection;63;0;62;0
WireConnection;64;0;63;0
WireConnection;69;0;67;0
WireConnection;69;1;68;0
WireConnection;73;0;71;0
WireConnection;73;1;72;0
WireConnection;70;0;69;0
WireConnection;70;1;64;0
WireConnection;74;0;70;0
WireConnection;74;1;73;0
WireConnection;4;1;74;0
WireConnection;12;0;4;4
WireConnection;20;0;9;0
WireConnection;20;1;12;0
WireConnection;11;0;4;0
WireConnection;11;1;22;0
WireConnection;11;2;4;4
WireConnection;13;0;4;4
WireConnection;13;1;22;4
WireConnection;14;0;12;0
WireConnection;14;1;9;4
WireConnection;21;0;11;0
WireConnection;21;1;20;0
WireConnection;15;0;13;0
WireConnection;15;1;14;0
WireConnection;77;0;21;0
WireConnection;77;3;15;0
WireConnection;88;6;77;0
WireConnection;88;7;78;0
WireConnection;1;0;88;0
ASEEND*/
//CHKSM=35C50EDA571170DC47D457154B91ACAAB0FA36AB