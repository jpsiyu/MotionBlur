// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SimpleBlend"{
    Properties{
        _SrcAlpha("SrcAlpha", Range(0, 1)) = 0.3
    }
    
    SubShader{
        Tags {"Queue" = "Transparent"} 
        Pass{
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            float _SrcAlpha;

            float4 vert(float4 vertex:POSITION) : SV_POSITION{
                return UnityObjectToClipPos(vertex);
            }
            float4 frag(void) : COLOR {
                return float4(0, 1, 0, _SrcAlpha);
            }
            ENDCG
        }
    }
}