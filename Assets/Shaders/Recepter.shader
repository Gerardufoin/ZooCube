// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:1,cusa:True,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:True,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1873,x:34119,y:32808,varname:node_1873,prsc:2|emission-915-OUT,alpha-7493-OUT;n:type:ShaderForge.SFN_Color,id:5983,x:32421,y:33118,ptovrint:False,ptlb:BackgroundColor,ptin:_BackgroundColor,varname:_BackgroundColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.6313726,c2:0.4,c3:0.2235294,c4:1;n:type:ShaderForge.SFN_ObjectScale,id:2162,x:30407,y:32617,varname:node_2162,prsc:2,rcp:False;n:type:ShaderForge.SFN_Divide,id:4767,x:32204,y:32662,cmnt:Final Y scaling --- Max Y scale divided by Z2 if X1 More than Y1 or by 1 otherwise,varname:node_4767,prsc:2|A-8492-OUT,B-8233-OUT;n:type:ShaderForge.SFN_Clamp01,id:769,x:30687,y:32392,cmnt:X between 0-1 -- X1,varname:node_769,prsc:2|IN-2162-X;n:type:ShaderForge.SFN_Clamp01,id:8278,x:30670,y:32901,cmnt:Y between 0-1 -- Y1,varname:node_8278,prsc:2|IN-2162-Y;n:type:ShaderForge.SFN_Divide,id:9288,x:32204,y:32482,cmnt:Final X scaling --- Max X scale divided by Z1 if Y1 more than X1 or by 1 otherwise,varname:node_9288,prsc:2|A-9057-OUT,B-2899-OUT;n:type:ShaderForge.SFN_Power,id:8492,x:31969,y:32941,cmnt:If Y More than 1 or Diff2 More than 0 -- Y else -- 1,varname:node_8492,prsc:2|VAL-2162-Y,EXP-4095-OUT;n:type:ShaderForge.SFN_Power,id:9057,x:32012,y:32197,cmnt:If X More than 1 or Diff1 More than 0 -- X else -- 1,varname:node_9057,prsc:2|VAL-2162-X,EXP-4554-OUT;n:type:ShaderForge.SFN_Floor,id:8438,x:31221,y:32282,cmnt:If X More than 1 -- 1 else -- 0,varname:node_8438,prsc:2|IN-769-OUT;n:type:ShaderForge.SFN_Floor,id:6101,x:31210,y:33137,cmnt:If Y More than 1 -- 1 else -- 0,varname:node_6101,prsc:2|IN-8278-OUT;n:type:ShaderForge.SFN_Subtract,id:6289,x:31003,y:32580,cmnt:X1 - Y1 -- Diff1,varname:node_6289,prsc:2|A-769-OUT,B-8278-OUT;n:type:ShaderForge.SFN_Subtract,id:1469,x:31221,y:32443,cmnt:Diff1 - X1 -- Z1,varname:node_1469,prsc:2|A-769-OUT,B-6289-OUT;n:type:ShaderForge.SFN_Power,id:2899,x:31775,y:32462,cmnt:If diff1 pos -- Z1 else -- 1,varname:node_2899,prsc:2|VAL-1469-OUT,EXP-1645-OUT;n:type:ShaderForge.SFN_Clamp01,id:9467,x:31221,y:32580,varname:node_9467,prsc:2|IN-6289-OUT;n:type:ShaderForge.SFN_Ceil,id:1645,x:31384,y:32580,cmnt:If diff1 LessEq than 0 -- 0 else -- 1,varname:node_1645,prsc:2|IN-9467-OUT;n:type:ShaderForge.SFN_Subtract,id:3570,x:31003,y:32825,cmnt:Y1 - X1 -- Diff2,varname:node_3570,prsc:2|A-8278-OUT,B-769-OUT;n:type:ShaderForge.SFN_Clamp01,id:4696,x:31210,y:32825,varname:node_4696,prsc:2|IN-3570-OUT;n:type:ShaderForge.SFN_Subtract,id:9139,x:31210,y:32979,cmnt:Diff2 - Y1 -- Z1,varname:node_9139,prsc:2|A-8278-OUT,B-3570-OUT;n:type:ShaderForge.SFN_Ceil,id:4250,x:31384,y:32825,cmnt:If diff2 LessEq than 0 -- 0 else -- 1,varname:node_4250,prsc:2|IN-4696-OUT;n:type:ShaderForge.SFN_Power,id:8233,x:31765,y:32829,cmnt:If diff2 pos -- Z1 else -- 1,varname:node_8233,prsc:2|VAL-9139-OUT,EXP-4250-OUT;n:type:ShaderForge.SFN_Add,id:1012,x:31572,y:32280,varname:node_1012,prsc:2|A-8438-OUT,B-1645-OUT;n:type:ShaderForge.SFN_Clamp01,id:4554,x:31775,y:32280,varname:node_4554,prsc:2|IN-1012-OUT;n:type:ShaderForge.SFN_Add,id:5743,x:31561,y:33034,varname:node_5743,prsc:2|A-4250-OUT,B-6101-OUT;n:type:ShaderForge.SFN_Clamp01,id:4095,x:31765,y:33034,varname:node_4095,prsc:2|IN-5743-OUT;n:type:ShaderForge.SFN_Clamp01,id:7493,x:33661,y:33168,varname:node_7493,prsc:2|IN-8081-OUT;n:type:ShaderForge.SFN_Tex2d,id:8617,x:32421,y:33291,varname:node_8617,prsc:2,tex:6d492f15dd2f99148b7782e4a7f6b352,ntxv:0,isnm:False|TEX-4448-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:3202,x:32197,y:33483,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_3202,glob:False,taghide:False,taghdr:False,tagprd:True,tagnsco:False,tagnrm:False,tex:6d492f15dd2f99148b7782e4a7f6b352,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:2208,x:30735,y:33747,varname:node_2208,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:2764,x:32421,y:33612,varname:node_2764,prsc:2,tex:6d492f15dd2f99148b7782e4a7f6b352,ntxv:0,isnm:False|UVIN-1117-OUT,TEX-4448-TEX;n:type:ShaderForge.SFN_Multiply,id:5901,x:31093,y:34052,varname:node_5901,prsc:2|A-2208-V,B-3680-OUT;n:type:ShaderForge.SFN_Subtract,id:5980,x:30996,y:33869,varname:node_5980,prsc:2|A-3680-OUT,B-1015-OUT;n:type:ShaderForge.SFN_Divide,id:4122,x:31205,y:33869,varname:node_4122,prsc:2|A-5980-OUT,B-1669-OUT;n:type:ShaderForge.SFN_Subtract,id:7695,x:31511,y:33792,varname:node_7695,prsc:2|A-5901-OUT,B-4122-OUT;n:type:ShaderForge.SFN_Slider,id:2016,x:30159,y:33788,ptovrint:False,ptlb:BorderWidth,ptin:_BorderWidth,varname:node_2016,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1,max:1;n:type:ShaderForge.SFN_Color,id:2038,x:32421,y:33447,ptovrint:False,ptlb:BorderColor,ptin:_BorderColor,varname:node_2038,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.3843138,c2:0.2235294,c3:0.1058824,c4:1;n:type:ShaderForge.SFN_Multiply,id:8081,x:33416,y:33168,varname:node_8081,prsc:2|A-5983-A,B-1256-OUT,C-2038-A;n:type:ShaderForge.SFN_Multiply,id:4486,x:33044,y:33653,varname:node_4486,prsc:2|A-2038-RGB,B-2764-RGB;n:type:ShaderForge.SFN_ComponentMask,id:3009,x:33051,y:33367,varname:node_3009,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-8532-OUT;n:type:ShaderForge.SFN_Multiply,id:8391,x:33596,y:32912,varname:node_8391,prsc:2|A-5983-RGB,B-3009-OUT;n:type:ShaderForge.SFN_Add,id:915,x:33878,y:32934,varname:node_915,prsc:2|A-8391-OUT,B-4486-OUT;n:type:ShaderForge.SFN_OneMinus,id:8532,x:32849,y:33498,varname:node_8532,prsc:2|IN-2764-RGB;n:type:ShaderForge.SFN_OneMinus,id:1256,x:32709,y:33279,varname:node_1256,prsc:2|IN-8617-R;n:type:ShaderForge.SFN_Append,id:9824,x:31767,y:33703,varname:node_9824,prsc:2|A-3077-OUT,B-7695-OUT;n:type:ShaderForge.SFN_Multiply,id:9834,x:31082,y:33510,varname:node_9834,prsc:2|A-2208-U,B-4588-OUT;n:type:ShaderForge.SFN_Subtract,id:3077,x:31511,y:33611,varname:node_3077,prsc:2|A-9834-OUT,B-6-OUT;n:type:ShaderForge.SFN_Subtract,id:4720,x:30996,y:33663,varname:node_4720,prsc:2|A-4588-OUT,B-1015-OUT;n:type:ShaderForge.SFN_Vector1,id:1015,x:30996,y:33795,varname:node_1015,prsc:2,v1:1;n:type:ShaderForge.SFN_Divide,id:6,x:31205,y:33663,varname:node_6,prsc:2|A-4720-OUT,B-1669-OUT;n:type:ShaderForge.SFN_Vector1,id:1669,x:31205,y:33795,varname:node_1669,prsc:2,v1:2;n:type:ShaderForge.SFN_Divide,id:8866,x:30531,y:33936,varname:node_8866,prsc:2|A-2016-OUT,B-5446-OUT;n:type:ShaderForge.SFN_Add,id:3680,x:30735,y:33936,varname:node_3680,prsc:2|A-3736-OUT,B-8866-OUT;n:type:ShaderForge.SFN_Vector1,id:3736,x:30531,y:33791,varname:node_3736,prsc:2,v1:1;n:type:ShaderForge.SFN_Add,id:4588,x:30735,y:33584,varname:node_4588,prsc:2|A-5349-OUT,B-3736-OUT;n:type:ShaderForge.SFN_Divide,id:5349,x:30531,y:33584,varname:node_5349,prsc:2|A-2016-OUT,B-902-OUT;n:type:ShaderForge.SFN_Set,id:6274,x:32377,y:32662,varname:Y_Scale,prsc:2|IN-4767-OUT;n:type:ShaderForge.SFN_Set,id:1369,x:32377,y:32482,varname:X_Scale,prsc:2|IN-9288-OUT;n:type:ShaderForge.SFN_Get,id:902,x:30295,y:33584,varname:node_902,prsc:2|IN-1369-OUT;n:type:ShaderForge.SFN_Get,id:5446,x:30295,y:33956,varname:node_5446,prsc:2|IN-6274-OUT;n:type:ShaderForge.SFN_Append,id:9019,x:31774,y:34282,varname:node_9019,prsc:2|A-1826-OUT,B-6409-OUT;n:type:ShaderForge.SFN_Add,id:1117,x:32065,y:33871,varname:node_1117,prsc:2|A-9824-OUT,B-9019-OUT;n:type:ShaderForge.SFN_Slider,id:6342,x:31044,y:34254,ptovrint:False,ptlb:BorderXOffset,ptin:_BorderXOffset,varname:node_6342,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-0.1,cur:0,max:0.1;n:type:ShaderForge.SFN_Slider,id:3026,x:31044,y:34422,ptovrint:False,ptlb:BorderYOffset,ptin:_BorderYOffset,varname:_node_6342_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-0.1,cur:0,max:0.1;n:type:ShaderForge.SFN_Get,id:447,x:31506,y:34501,varname:node_447,prsc:2|IN-6274-OUT;n:type:ShaderForge.SFN_Divide,id:6409,x:31506,y:34376,varname:node_6409,prsc:2|A-3026-OUT,B-447-OUT;n:type:ShaderForge.SFN_Get,id:6891,x:31506,y:34304,varname:node_6891,prsc:2|IN-1369-OUT;n:type:ShaderForge.SFN_Divide,id:1826,x:31506,y:34181,varname:node_1826,prsc:2|A-6342-OUT,B-6891-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:4448,x:32197,y:33274,ptovrint:False,ptlb:Map,ptin:_Map,varname:node_4448,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:6d492f15dd2f99148b7782e4a7f6b352,ntxv:0,isnm:False;proporder:3202-5983-2038-2016-6342-3026-4448;pass:END;sub:END;*/

Shader "Shader Forge/Recepter" {
    Properties {
        [PerRendererData]_MainTex ("MainTex", 2D) = "white" {}
        _BackgroundColor ("BackgroundColor", Color) = (0.6313726,0.4,0.2235294,1)
        _BorderColor ("BorderColor", Color) = (0.3843138,0.2235294,0.1058824,1)
        _BorderWidth ("BorderWidth", Range(0, 1)) = 0.1
        _BorderXOffset ("BorderXOffset", Range(-0.1, 0.1)) = 0
        _BorderYOffset ("BorderYOffset", Range(-0.1, 0.1)) = 0
        _Map ("Map", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
            "PreviewType"="Plane"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _BackgroundColor;
            uniform float _BorderWidth;
            uniform float4 _BorderColor;
            uniform float _BorderXOffset;
            uniform float _BorderYOffset;
            uniform sampler2D _Map; uniform float4 _Map_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                o.pos = UnityObjectToClipPos( v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
////// Lighting:
////// Emissive:
                float node_769 = saturate(objScale.r); // X between 0-1 -- X1
                float node_8278 = saturate(objScale.g); // Y between 0-1 -- Y1
                float node_6289 = (node_769-node_8278); // X1 - Y1 -- Diff1
                float node_1645 = ceil(saturate(node_6289)); // If diff1 LessEq than 0 -- 0 else -- 1
                float X_Scale = (pow(objScale.r,saturate((floor(node_769)+node_1645)))/pow((node_769-node_6289),node_1645));
                float node_3736 = 1.0;
                float node_4588 = ((_BorderWidth/X_Scale)+node_3736);
                float node_1015 = 1.0;
                float node_1669 = 2.0;
                float node_3570 = (node_8278-node_769); // Y1 - X1 -- Diff2
                float node_4250 = ceil(saturate(node_3570)); // If diff2 LessEq than 0 -- 0 else -- 1
                float Y_Scale = (pow(objScale.g,saturate((node_4250+floor(node_8278))))/pow((node_8278-node_3570),node_4250));
                float node_3680 = (node_3736+(_BorderWidth/Y_Scale));
                float2 node_1117 = (float2(((i.uv0.r*node_4588)-((node_4588-node_1015)/node_1669)),((i.uv0.g*node_3680)-((node_3680-node_1015)/node_1669)))+float2((_BorderXOffset/X_Scale),(_BorderYOffset/Y_Scale)));
                float4 node_2764 = tex2D(_Map,TRANSFORM_TEX(node_1117, _Map));
                float3 emissive = ((_BackgroundColor.rgb*(1.0 - node_2764.rgb).r)+(_BorderColor.rgb*node_2764.rgb));
                float3 finalColor = emissive;
                float4 node_8617 = tex2D(_Map,TRANSFORM_TEX(i.uv0, _Map));
                return fixed4(finalColor,saturate((_BackgroundColor.a*(1.0 - node_8617.r)*_BorderColor.a)));
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
