// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Blend2" {
    Properties{
        _SrcAlpha("SrcAlpha", Range(0, 1)) = 0.3
    }
    
    CGINCLUDE
    float _SrcAlpha;
    struct v2f{
        float4 pos : SV_POSITION;
        float4 objectPos : TEXCOORD0;
    };
    v2f vert(float4 vertex : POSITION){
        v2f o;
        o.pos = UnityObjectToClipPos(vertex);
        o.objectPos = vertex;
        return o;
    }
    float4 frag(v2f i) : COLOR{
        if (i.objectPos.z == -0.5 )
            discard;
        return float4(1, 0, 0, _SrcAlpha);
    }
    float4 frag2(v2f i) : COLOR{
        if (i.objectPos.z == -0.5 )
            discard;
        return float4(0, 1, 0, _SrcAlpha);
    }
    ENDCG
    SubShader{
        Tags {"Queue"="Transparent"}
        Pass{
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Front
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
        Pass{
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Back
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag2
            ENDCG
        }
    }
}