// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ZooCube/Block"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_ShapeMask("ShapeMask", 2D) = "white" {}
		_BorderColor("BorderColor", Color) = (0.3843138,0.2196079,0.1058824,1)
		_BorderWidth("BorderWidth", Range( 0 , 1)) = 0.1
		_BorderXOffset("BorderXOffset", Range( -0.1 , 0.1)) = 0
		_BorderYOffset("BorderYOffset", Range( -0.1 , 0.1)) = 0
		_BackgroundColor("BackgroundColor", Color) = (0.6313726,0.4,0.2196079,1)
		[PerRendererData]_ID("ID", Float) = 0
		_Face("Face", 2D) = "white" {}
		_FaceScale("FaceScale", Range( 0.1 , 2)) = 1
		_FaceXOffset("FaceXOffset", Range( -1 , 1)) = 0
		_FaceYOffset("FaceYOffset", Range( -1 , 1)) = 0
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
			
			uniform sampler2D _MainTex;
			uniform sampler2D _AlphaTex;
			uniform sampler2D _Face;
			uniform float _FaceScale;
			uniform float _FaceXOffset;
			uniform float _FaceYOffset;
			uniform float4 _BackgroundColor;
			uniform sampler2D _ShapeMask;
			uniform float _BorderWidth;
			uniform float _BorderXOffset;
			uniform float _BorderYOffset;
			uniform float4 _BorderColor;
			uniform float4 _ShapeMask_ST;
			uniform float _ID;
			
			v2f vert( appdata_t IN  )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				
				
				IN.vertex.xyz +=  float3(0,0,0) ; 
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
			
			fixed4 frag(v2f IN  ) : SV_Target
			{
				float2 uv66 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
				float clampResult49_g103 = clamp( ase_objectScale.x , 0.0 , 1.0 );
				float clampResult50_g103 = clamp( ase_objectScale.y , 0.0 , 1.0 );
				float2 appendResult57_g103 = (float2(( ase_objectScale.x / clampResult50_g103 ) , 1.0));
				float2 clampResult61_g103 = clamp( (ase_objectScale).xy , float2( 1,1 ) , float2( 1000,1000 ) );
				float2 appendResult58_g103 = (float2(1.0 , ( ase_objectScale.y / clampResult49_g103 )));
				float2 ifLocalVar47_g103 = 0;
				if( clampResult49_g103 > clampResult50_g103 )
				ifLocalVar47_g103 = appendResult57_g103;
				else if( clampResult49_g103 == clampResult50_g103 )
				ifLocalVar47_g103 = clampResult61_g103;
				else if( clampResult49_g103 < clampResult50_g103 )
				ifLocalVar47_g103 = appendResult58_g103;
				float2 ScaleRatio78 = ifLocalVar47_g103;
				float2 temp_output_62_0 = ( ScaleRatio78 / _FaceScale );
				float2 break65 = temp_output_62_0;
				float2 appendResult69 = (float2(( uv66.x * break65.x ) , ( uv66.y * break65.y )));
				float2 appendResult73 = (float2(_FaceXOffset , _FaceYOffset));
				float4 tex2DNode4 = tex2D( _Face, ( ( appendResult69 - ( ( temp_output_62_0 - float2( 1,1 ) ) / float2( 2,2 ) ) ) - appendResult73 ) );
				float temp_output_12_0 = ( 1.0 - tex2DNode4.a );
				float4 appendResult77 = (float4(( ( tex2DNode4 * IN.color * tex2DNode4.a ) + ( _BackgroundColor * temp_output_12_0 ) ).rgb , ( ( tex2DNode4.a * IN.color.a ) + ( temp_output_12_0 * _BackgroundColor.a ) )));
				float4 temp_output_6_0_g102 = appendResult77;
				float2 uv11_g102 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 ScaleRatio8_g102 = ScaleRatio78;
				float2 temp_output_10_0_g102 = ( _BorderWidth / ScaleRatio8_g102 );
				float2 break12_g102 = ( temp_output_10_0_g102 + 1.0 );
				float2 appendResult17_g102 = (float2(( uv11_g102.x * break12_g102.x ) , ( uv11_g102.y * break12_g102.y )));
				float2 appendResult18_g102 = (float2(_BorderXOffset , _BorderYOffset));
				float2 temp_output_23_0_g102 = ( ( appendResult17_g102 - ( temp_output_10_0_g102 / 2.0 ) ) + ( appendResult18_g102 / ScaleRatio8_g102 ) );
				float2 break60_g102 = step( float2( 0,0 ) , ( 1.0 - temp_output_23_0_g102 ) );
				float2 break61_g102 = step( float2( 0,0 ) , temp_output_23_0_g102 );
				float clampResult68_g102 = clamp( ( tex2D( _ShapeMask, temp_output_23_0_g102 ).r + ( 1.0 - ( break60_g102.x * break60_g102.y * break61_g102.x * break61_g102.y ) ) ) , 0.0 , 1.0 );
				float2 uv_ShapeMask = IN.texcoord.xy * _ShapeMask_ST.xy + _ShapeMask_ST.zw;
				float clampResult34_g102 = clamp( ( (temp_output_6_0_g102).a * ( 1.0 - tex2D( _ShapeMask, uv_ShapeMask ).r ) * _BorderColor.a ) , 0.0 , 1.0 );
				float4 appendResult38_g102 = (float4((( float4( ( (temp_output_6_0_g102).rgb * ( 1.0 - clampResult68_g102 ) ) , 0.0 ) + ( _BorderColor * clampResult68_g102 ) )).rgb , clampResult34_g102));
				
				fixed4 c = ( appendResult38_g102 + ( _ID * 0.0 ) );
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15301
-1690;106;1627;892;3002.703;341.6707;1.812839;True;False
Node;AmplifyShaderEditor.FunctionNode;183;-740.7661,726.5918;Float;False;ScalingRatio;-1;;103;096dc3062cc962b49b5311cdb37df263;0;0;3;FLOAT2;0;FLOAT;27;FLOAT;28
Node;AmplifyShaderEditor.GetLocalVarNode;60;-2950.392,-140.3514;Float;False;78;0;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;78;-479.583,619.7949;Float;False;ScaleRatio;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;61;-3029.872,-25.54801;Float;False;Property;_FaceScale;FaceScale;9;0;Create;True;0;0;False;0;1;0;0.1;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;62;-2722.993,-89.5729;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.BreakToComponentsNode;65;-2500.01,-195.5454;Float;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.TextureCoordinatesNode;66;-2486.763,-332.4263;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;67;-2193.132,-308.141;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;63;-2506.786,3.152842;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;1,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;68;-2197.547,-195.5454;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;69;-2034.173,-255.1548;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;72;-2122.484,212.8895;Float;False;Property;_FaceYOffset;FaceYOffset;11;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;71;-2120.276,124.5792;Float;False;Property;_FaceXOffset;FaceXOffset;10;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;64;-2195.339,3.152842;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;2,2;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;73;-1844.306,162.111;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;70;-1822.229,-153.598;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;74;-1574.959,-3.470398;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;4;-1394.203,-32.06759;Float;True;Property;_Face;Face;8;0;Create;True;0;0;False;0;None;2e5f3a3c9a063494b924449bc184dd90;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;22;-1335.677,224.1994;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;9;-1349.593,456.7012;Float;False;Property;_BackgroundColor;BackgroundColor;6;0;Create;True;0;0;False;0;0.6313726,0.4,0.2196079,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;12;-1024.554,252.2601;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-797.134,404.6125;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-803.8657,32.13393;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-799.9449,531.1904;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-794.5129,264.8711;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;15;-588.7813,407.407;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;21;-578.4736,124.8902;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;173;-344.8201,719.7365;Float;False;Property;_ID;ID;7;1;[PerRendererData];Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;77;-405.8012,299.591;Float;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;179;-133.8201,614.7365;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;171;-185.6634,383.2658;Float;False;AddBorder;0;;102;e22a0ad25ec970f4d94f3a43eb80ec65;0;2;6;COLOR;0,0,0,0;False;7;FLOAT2;1,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;5
Node;AmplifyShaderEditor.SimpleAddOpNode;178;39.17993,521.7365;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;172;161.0808,522.2263;Float;False;True;2;Float;ASEMaterialInspector;0;7;ZooCube/Block;7b8ddc695d7cc78438993d5918a9e7b9;0;0;SubShader 0 Pass 0;2;True;3;One;OneMinusSrcAlpha;0;One;Zero;False;True;Off;False;False;True;2;False;False;True;5;Queue=Transparent;IgnoreProjector=True;RenderType=Transparent;PreviewType=Plane;CanUseSpriteAtlas=True;False;0;0;0;False;False;False;False;False;False;False;False;False;True;2;0;0;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;78;0;183;0
WireConnection;62;0;60;0
WireConnection;62;1;61;0
WireConnection;65;0;62;0
WireConnection;67;0;66;1
WireConnection;67;1;65;0
WireConnection;63;0;62;0
WireConnection;68;0;66;2
WireConnection;68;1;65;1
WireConnection;69;0;67;0
WireConnection;69;1;68;0
WireConnection;64;0;63;0
WireConnection;73;0;71;0
WireConnection;73;1;72;0
WireConnection;70;0;69;0
WireConnection;70;1;64;0
WireConnection;74;0;70;0
WireConnection;74;1;73;0
WireConnection;4;1;74;0
WireConnection;12;0;4;4
WireConnection;13;0;4;4
WireConnection;13;1;22;4
WireConnection;11;0;4;0
WireConnection;11;1;22;0
WireConnection;11;2;4;4
WireConnection;14;0;12;0
WireConnection;14;1;9;4
WireConnection;20;0;9;0
WireConnection;20;1;12;0
WireConnection;15;0;13;0
WireConnection;15;1;14;0
WireConnection;21;0;11;0
WireConnection;21;1;20;0
WireConnection;77;0;21;0
WireConnection;77;3;15;0
WireConnection;179;0;173;0
WireConnection;171;6;77;0
WireConnection;171;7;78;0
WireConnection;178;0;171;0
WireConnection;178;1;179;0
WireConnection;172;0;178;0
ASEEND*/
//CHKSM=11FF079CDDBA5FFA89853FFF85860B58F8D024F5