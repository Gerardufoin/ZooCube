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
		_ImageScale("ImageScale", Range( 0.1 , 2)) = 1
		_Face("Face", 2D) = "white" {}
		_ShapeMask("ShapeMask", 2D) = "white" {}
		_BackgroundColor("BackgroundColor", Color) = (0.6313726,0.4,0.2196079,1)
		_BorderColor("BorderColor", Color) = (0.3843138,0.2196079,0.1058824,1)
		_BorderWidth("BorderWidth", Range( 0 , 1)) = 0.1
		_BorderXOffset("BorderXOffset", Range( -0.1 , 0.1)) = 0
		_BorderYOffset("BorderYOffset", Range( -0.1 , 0.1)) = 0
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
				float clampResult2_g2 = clamp( ase_objectScale.x , 0.0 , 1.0 );
				float clampResult3_g2 = clamp( ase_objectScale.y , 0.0 , 1.0 );
				float temp_output_4_0_g2 = ( clampResult2_g2 - clampResult3_g2 );
				float clampResult9_g2 = clamp( temp_output_4_0_g2 , 0.0 , 1.0 );
				float temp_output_10_0_g2 = ceil( clampResult9_g2 );
				float clampResult12_g2 = clamp( ( floor( clampResult2_g2 ) + temp_output_10_0_g2 ) , 0.0 , 1.0 );
				float temp_output_16_0_g2 = ( pow( ase_objectScale.x , clampResult12_g2 ) / pow( ( clampResult2_g2 - temp_output_4_0_g2 ) , temp_output_10_0_g2 ) );
				float temp_output_5_0_g2 = ( clampResult3_g2 - clampResult2_g2 );
				float clampResult18_g2 = clamp( temp_output_5_0_g2 , 0.0 , 1.0 );
				float temp_output_20_0_g2 = ceil( clampResult18_g2 );
				float clampResult23_g2 = clamp( ( temp_output_20_0_g2 + floor( clampResult3_g2 ) ) , 0.0 , 1.0 );
				float temp_output_26_0_g2 = ( pow( ase_objectScale.y , clampResult23_g2 ) / pow( ( clampResult3_g2 - temp_output_5_0_g2 ) , temp_output_20_0_g2 ) );
				float2 appendResult17_g2 = (float2(temp_output_16_0_g2 , temp_output_26_0_g2));
				float2 ScaleRatio57 = appendResult17_g2;
				float2 temp_output_62_0 = ( ScaleRatio57 / _ImageScale );
				float2 appendResult69 = (float2(( uv66.x * temp_output_62_0.x ) , ( uv66.y * temp_output_62_0.y )));
				float2 appendResult73 = (float2(_ImageXOffset , _ImageYOffset));
				float4 tex2DNode4 = tex2D( _Face, ( ( appendResult69 - ( ( temp_output_62_0 - float2( 1,1 ) ) / float2( 2,2 ) ) ) - appendResult73 ) );
				float temp_output_12_0 = ( 1.0 - tex2DNode4.a );
				float2 uv44 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 temp_output_33_0 = ( _BorderWidth / ScaleRatio57 );
				float2 appendResult47 = (float2(( uv44.x * ( temp_output_33_0 + float2( 1,1 ) ).x ) , ( uv44.y * ( temp_output_33_0 + float2( 1,1 ) ).y )));
				float2 appendResult52 = (float2(_BorderXOffset , _BorderYOffset));
				float4 tex2DNode24 = tex2D( _ShapeMask, ( ( appendResult47 - ( temp_output_33_0 / float2( 2,2 ) ) ) + ( appendResult52 / ScaleRatio57 ) ) );
				float2 uv_ShapeMask = IN.texcoord.xy * _ShapeMask_ST.xy + _ShapeMask_ST.zw;
				float clampResult17 = clamp( ( ( ( tex2DNode4.a * IN.color.a ) + ( temp_output_12_0 * _BackgroundColor.a ) ) * ( 1.0 - tex2D( _ShapeMask, uv_ShapeMask ).r ) * _BorderColor.a ) , 0.0 , 1.0 );
				float4 appendResult19 = (float4((( ( ( ( tex2DNode4 * IN.color * tex2DNode4.a ) + ( _BackgroundColor * temp_output_12_0 ) ) * ( 1.0 - tex2DNode24.r ) ) + ( _BorderColor * tex2DNode24 ) )).rgb , clampResult17));
				
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
-1907;33;1800;971;4022.726;599.9858;2.619292;True;False
Node;AmplifyShaderEditor.FunctionNode;31;-3260.062,1320.567;Float;False;ScalingRatio;-1;;2;096dc3062cc962b49b5311cdb37df263;0;3;FLOAT2;0;FLOAT;27;FLOAT;28
Node;AmplifyShaderEditor.RangedFloatNode;61;-3029.872,-25.54801;Float;False;Property;_ImageScale;ImageScale;0;0;Create;1;0;0.1;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;57;-3062.899,1315.431;Float;False;ScaleRatio;-1;True;1;0;FLOAT2;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;60;-2950.392,-140.3514;Float;False;57;0;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-3122.694,1190.635;Float;False;Property;_BorderWidth;BorderWidth;5;0;Create;0.1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;62;-2722.993,-89.5729;Float;False;2;0;FLOAT2;0.0;False;1;FLOAT;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;33;-2718.586,1248.365;Float;False;2;0;FLOAT;0.0;False;1;FLOAT2;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.BreakToComponentsNode;65;-2500.01,-195.5454;Float;False;FLOAT2;1;0;FLOAT2;0.0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.TextureCoordinatesNode;66;-2486.763,-332.4263;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;34;-2538.369,1178.958;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;1,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;68;-2197.547,-195.5454;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;67;-2193.132,-308.141;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;63;-2504.425,3.152842;Float;False;2;0;FLOAT2;0.0;False;1;FLOAT2;1,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;69;-2034.173,-255.1548;Float;False;FLOAT2;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;64;-2195.339,3.152842;Float;False;2;0;FLOAT2;0.0;False;1;FLOAT2;2,2;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;44;-2396.799,1046.228;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;43;-2405.112,1180.9;Float;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;72;-2122.484,212.8895;Float;False;Property;_ImageYOffset;ImageYOffset;9;0;Create;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;71;-2120.276,124.5792;Float;False;Property;_ImageXOffset;ImageXOffset;8;0;Create;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-2090.88,1071.167;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;50;-2343.598,1498.458;Float;False;Property;_BorderXOffset;BorderXOffset;6;0;Create;0;0;-0.1;0.1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;73;-1844.306,162.111;Float;False;FLOAT2;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;-2092.542,1182.562;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;70;-1822.229,-153.598;Float;False;2;0;FLOAT2;0.0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;51;-2343.597,1578.263;Float;False;Property;_BorderYOffset;BorderYOffset;7;0;Create;0;0;-0.1;0.1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;47;-1944.571,1121.046;Float;False;FLOAT2;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;74;-1574.959,-3.470398;Float;False;2;0;FLOAT2;0.0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;52;-2045.99,1521.735;Float;False;FLOAT2;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;58;-2067.521,1667.933;Float;False;57;0;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;40;-2194.814,1329.486;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;2,2;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;54;-1834.839,1584.913;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;41;-1753.167,1206.566;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;4;-1394.203,-32.06759;Float;True;Property;_Face;Face;1;0;Create;2e5f3a3c9a063494b924449bc184dd90;2e5f3a3c9a063494b924449bc184dd90;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;55;-1550.533,1367.112;Float;False;2;2;0;FLOAT2;0.0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;9;-1349.593,456.7012;Float;False;Property;_BackgroundColor;BackgroundColor;3;0;Create;0.6313726,0.4,0.2196079,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;6;-1638.221,906.7415;Float;True;Property;_ShapeMask;ShapeMask;2;0;Create;4f9aa590ffa5c5f4eb66d6bf9c21c2c7;4f9aa590ffa5c5f4eb66d6bf9c21c2c7;False;white;Auto;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.VertexColorNode;22;-1335.677,224.1994;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;12;-1009.43,201.8466;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;24;-1340.699,1131.335;Float;True;Property;_TextureSample0;Texture Sample 0;3;0;Create;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-777.7087,211.0967;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-877.8051,42.21663;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;5;-1328.737,754.5868;Float;True;Property;_TextureSample1;Texture Sample 1;0;0;Create;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-872.2039,504.3033;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;25;-1306.927,943.2877;Float;False;Property;_BorderColor;BorderColor;4;0;Create;0.3843138,0.2196079,0.1058824,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;21;-502.8535,286.2133;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-1012.231,305.466;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;28;-728.5315,918.6789;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-775.2938,1082.371;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-397.2135,421.003;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;15;-718.1753,383.8807;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;7;-1025.275,782.7076;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-509.9839,614.3188;Float;False;3;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;30;-239.2435,461.544;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;18;-96.3868,545.9952;Float;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ClampOpNode;17;-282.4586,619.9055;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;19;122.0778,597.0004;Float;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMasterNode;1;295.5918,595.6017;Float;False;True;2;Float;ASEMaterialInspector;0;4;ZooCube/Block;0f8ba0101102bb14ebf021ddadce9b49;Sprites Default;3;One;OneMinusSrcAlpha;0;One;Zero;Off;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;57;0;31;0
WireConnection;62;0;60;0
WireConnection;62;1;61;0
WireConnection;33;0;32;0
WireConnection;33;1;57;0
WireConnection;65;0;62;0
WireConnection;34;0;33;0
WireConnection;68;0;66;2
WireConnection;68;1;65;1
WireConnection;67;0;66;1
WireConnection;67;1;65;0
WireConnection;63;0;62;0
WireConnection;69;0;67;0
WireConnection;69;1;68;0
WireConnection;64;0;63;0
WireConnection;43;0;34;0
WireConnection;45;0;44;1
WireConnection;45;1;43;0
WireConnection;73;0;71;0
WireConnection;73;1;72;0
WireConnection;46;0;44;2
WireConnection;46;1;43;1
WireConnection;70;0;69;0
WireConnection;70;1;64;0
WireConnection;47;0;45;0
WireConnection;47;1;46;0
WireConnection;74;0;70;0
WireConnection;74;1;73;0
WireConnection;52;0;50;0
WireConnection;52;1;51;0
WireConnection;40;0;33;0
WireConnection;54;0;52;0
WireConnection;54;1;58;0
WireConnection;41;0;47;0
WireConnection;41;1;40;0
WireConnection;4;1;74;0
WireConnection;55;0;41;0
WireConnection;55;1;54;0
WireConnection;12;0;4;4
WireConnection;24;0;6;0
WireConnection;24;1;55;0
WireConnection;20;0;9;0
WireConnection;20;1;12;0
WireConnection;11;0;4;0
WireConnection;11;1;22;0
WireConnection;11;2;4;4
WireConnection;5;0;6;0
WireConnection;14;0;12;0
WireConnection;14;1;9;4
WireConnection;21;0;11;0
WireConnection;21;1;20;0
WireConnection;13;0;4;4
WireConnection;13;1;22;4
WireConnection;28;0;24;1
WireConnection;26;0;25;0
WireConnection;26;1;24;0
WireConnection;29;0;21;0
WireConnection;29;1;28;0
WireConnection;15;0;13;0
WireConnection;15;1;14;0
WireConnection;7;0;5;1
WireConnection;16;0;15;0
WireConnection;16;1;7;0
WireConnection;16;2;25;4
WireConnection;30;0;29;0
WireConnection;30;1;26;0
WireConnection;18;0;30;0
WireConnection;17;0;16;0
WireConnection;19;0;18;0
WireConnection;19;3;17;0
WireConnection;1;0;19;0
ASEEND*/
//CHKSM=E024B15326FA1DB501B76155895E54FF9C974D23