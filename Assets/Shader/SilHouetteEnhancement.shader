// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SilhouetteEnhancement" {
    Properties{
        _Color ("Color", Color) = (1, 1, 1, 0.5)
    }
    SubShader{
        Tags {"Queue" = "Transparent"}
        Pass{
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            float4 _Color;
            struct a2v{
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct v2f{
                float4 pos : SV_POSITION;
                float3 normal : TEXCOORD;
                float3 viewDir : TEXCOORD1;
            };
            v2f vert(a2v i){
                v2f o;
                o.normal = normalize(mul(unity_ObjectToWorld, float4(i.normal, 0)).xyz);
                o.viewDir = normalize((_WorldSpaceCameraPos - mul(unity_ObjectToWorld, i.vertex)).xyz);
                o.pos = UnityObjectToClipPos(i.vertex);
                return o;
            }
            float4 frag(v2f i) : COLOR{
                float newOpacity = 1 - min(1.0, abs(dot(i.viewDir, i.normal)));
                return float4(_Color.rgb, newOpacity);
            }
            ENDCG
        }
    }
}