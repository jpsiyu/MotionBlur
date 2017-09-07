// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/DiffuseLighting" {
    Properties{
        _Color ("Color", Color) = (1, 1, 1, 1)
    }
    CGINCLUDE
    #include "UnityCG.cginc"
    float4 _Color;
    uniform float4 _LightColor0;
    struct a2v{
        float4 vertex : POSITION;
        float3 normal : NORMAL;
    };
    struct v2f{
        float4 pos : SV_POSITION;
        float4 color : COLOR;
    };

    float3 calLightDir(a2v i){
        if (0 == _WorldSpaceLightPos0.w) 
            return normalize(_WorldSpaceLightPos0.xyz);
        else
            return normalize(_WorldSpaceLightPos0.xyz - mul(unity_ObjectToWorld, i.vertex.xyz));
    }

    float calAttenuation(a2v i){
        float attenuation;
        if (0 == _WorldSpaceLightPos0.w) 
            attenuation = 1;
        else{
            float vertex2LightSrc = _WorldSpaceLightPos0.xyz - mul(unity_ObjectToWorld, i.vertex).xyz;
            float dis = length(vertex2LightSrc);
            attenuation = 1.0 / dis;
        }
        return attenuation;
    }

    v2f vert(a2v i){
        v2f o;
        float3 ndir = normalize(mul(unity_ObjectToWorld, float4(i.normal, 0)).xyz);
        float3 ldir = calLightDir(i);
        float attenuation = calAttenuation(i);
        float3 diffRef = attenuation * _LightColor0.rgb * _Color.rgb * max(0, dot(ndir, ldir));
        o.color = float4(diffRef, 1.0);
        o.pos = UnityObjectToClipPos(i.vertex);
        return o;
    }
    float4 frag(v2f i) : COLOR{
        return i.color;
    }

    ENDCG

    SubShader{
        Pass{
            Tags {"LightMode" = "ForwardBase"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
        Pass{
            Tags {"LightMode" = "ForwardAdd"}
            Blend One One
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    }
    Fallback "Diffuse"
}