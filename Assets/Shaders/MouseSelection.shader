// Shader created with Shader Forge v1.36 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.36;sub:START;pass:START;ps:flbk:,iptp:1,cusa:True,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:True,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1873,x:33229,y:32719,varname:node_1873,prsc:2|emission-8607-OUT,alpha-7698-OUT;n:type:ShaderForge.SFN_Color,id:5983,x:32623,y:32665,ptovrint:False,ptlb:Background,ptin:_Background,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:1,c4:1;n:type:ShaderForge.SFN_TexCoord,id:9358,x:31319,y:32835,varname:node_9358,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Min,id:6523,x:31726,y:32851,varname:node_6523,prsc:2|A-9358-U,B-9358-V;n:type:ShaderForge.SFN_OneMinus,id:9327,x:31548,y:32943,varname:node_9327,prsc:2|IN-9358-U;n:type:ShaderForge.SFN_OneMinus,id:7958,x:31548,y:33065,varname:node_7958,prsc:2|IN-9358-V;n:type:ShaderForge.SFN_Min,id:2598,x:31726,y:32991,varname:node_2598,prsc:2|A-9327-OUT,B-7958-OUT;n:type:ShaderForge.SFN_Min,id:3473,x:31930,y:32893,varname:node_3473,prsc:2|A-6523-OUT,B-2598-OUT;n:type:ShaderForge.SFN_Add,id:9348,x:32131,y:32997,varname:node_9348,prsc:2|A-3473-OUT,B-7645-OUT;n:type:ShaderForge.SFN_Vector1,id:7645,x:31930,y:33101,varname:node_7645,prsc:2,v1:0.47;n:type:ShaderForge.SFN_Round,id:1345,x:32306,y:32997,varname:node_1345,prsc:2|IN-9348-OUT;n:type:ShaderForge.SFN_Multiply,id:5885,x:32615,y:32996,varname:node_5885,prsc:2|A-2818-OUT,B-1345-OUT;n:type:ShaderForge.SFN_Slider,id:2818,x:32497,y:32891,ptovrint:False,ptlb:BackgroundOpacity,ptin:_BackgroundOpacity,varname:node_2818,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.33,max:1;n:type:ShaderForge.SFN_OneMinus,id:2332,x:32306,y:33133,varname:node_2332,prsc:2|IN-1345-OUT;n:type:ShaderForge.SFN_Multiply,id:8351,x:32615,y:33142,varname:node_8351,prsc:2|A-2332-OUT,B-5041-OUT;n:type:ShaderForge.SFN_Slider,id:5041,x:32497,y:33355,ptovrint:False,ptlb:BorderOpacity,ptin:_BorderOpacity,varname:node_5041,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.6,max:1;n:type:ShaderForge.SFN_Add,id:7698,x:32832,y:33055,varname:node_7698,prsc:2|A-5885-OUT,B-8351-OUT;n:type:ShaderForge.SFN_Multiply,id:8607,x:33031,y:32777,varname:node_8607,prsc:2|A-5983-RGB,B-7698-OUT;proporder:5983-2818-5041;pass:END;sub:END;*/

Shader "Shader Forge/MouseSelection" {
    Properties {
        _Background ("Background", Color) = (0,0,1,1)
        _BackgroundOpacity ("BackgroundOpacity", Range(0, 1)) = 0.33
        _BorderOpacity ("BorderOpacity", Range(0, 1)) = 0.6
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
            uniform float4 _Background;
            uniform float _BackgroundOpacity;
            uniform float _BorderOpacity;
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
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float node_1345 = round((min(min(i.uv0.r,i.uv0.g),min((1.0 - i.uv0.r),(1.0 - i.uv0.g)))+0.47));
                float node_7698 = ((_BackgroundOpacity*node_1345)+((1.0 - node_1345)*_BorderOpacity));
                float3 emissive = (_Background.rgb*node_7698);
                float3 finalColor = emissive;
                return fixed4(finalColor,node_7698);
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
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
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
