// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "ShaderToy/Maze" {
    CGINCLUDE
    #include "UnityCG.cginc"

    struct v2f{
        float4 pos : SV_POSITION;
        float4 screenPos : TEXCOORD0;
    };

    v2f vert(appdata_base i){
        v2f o;
        o.pos = UnityObjectToClipPos(i.vertex);
        o.screenPos = ComputeScreenPos(o.pos);
        return o;
    }
    float4 frag(v2f i) : COLOR {
        float4 color = float4(0,0,0,0);
        float2 u = i.screenPos;
        color += 0.1 / frac(sin(1e5*length(ceil(u/=8.0))) < 0.0 ? u.x : u.y) - color;
        return color;
    }
    ENDCG

    SubShader{
        Pass{
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    }
}