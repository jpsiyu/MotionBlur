// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/AdditiveBlend" {
    Properties{
        _Color("Color", Color) = (1, 0, 0, 0)
        _Alpha("Alpha", Range(0, 1)) = 0.2
    }
    SubShader{
        Tags{"Queue"="Transparent"}
        Pass{
            Cull Off
            ZWrite Off
            Blend SrcAlpha One
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            float4 _Color;
            float _Alpha;
            float4 vert(float4 vertex : POSITION) : SV_POSITION{
                return UnityObjectToClipPos(vertex);
            }
            float4 frag(void) : COLOR{
                float4 c = _Color;
                c.a = _Alpha;
                return c;
            }
            ENDCG
        }
    }
}