// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/BrushedMetal" {
    Properties{
        _Color ("Diuufse Color", Color) = (1, 1, 1, 1)
        _SpecColor ("Specular Color", Color) = (1, 1, 1, 1)
        _AlphaX ("Roughness in Brush Direction", Float) = 1.0
        _AlphaY ("Roughness orthogonal to Brush Direction", FLoat) = 1.0
    }

    CGINCLUDE
    #include "UnityCG.cginc"
    uniform float4 _LightColor0;
    uniform float4 _Color;
    uniform float4 _SpecColor;
    uniform float _AlphaX;
    uniform float _AlphaY;
    struct a2v{
        float4 vertex : POSITION;
        float3 normal : NORMAL;
        float4 tangent : TANGENT;
    };
    struct v2f{
        float4 pos : SV_POSITION;
        float4 posWorld : TEXCOORD0;
        float3 viewDir : TEXCOORD1;
        float3 normalDir : TEXCOORD2;
        float3 tangenDir : TEXCOORD3;
    };

    v2f vert(a2v i){
        v2f o;
        o.posWorld = mul(unity_ObjectToWorld, i.vertex);
        o.viewDir = normalize(_WorldSpaceCameraPos - o.posWorld.xyz);
        o.normalDir = normalize(mul(unity_ObjectToWorld, float4(i.normal, 0)).xyz);
        o.tangenDir = normalize(mul(unity_ObjectToWorld, float4(i.tangent.xyz, 0)).xyz);
        o.pos = UnityObjectToClipPos(i.vertex);
        return o;
    }

    float4 frag(v2f i) : COLOR{
        float3 lightDir;
        float attenuation;
        if (0 == _WorldSpaceLightPos0.w){
            attenuation = 1.0;
            lightDir = normalize(_WorldSpaceLightPos0.xyz);
        }else{
            float3 vertex2light = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
            float distance = length(vertex2light);
            attenuation = 1.0 / distance;
            lightDir = normalize(vertex2light);
        }
        float3 halfwayVector = normalize(lightDir + i.viewDir);
        float3 binormalDir = cross(i.normalDir, i.tangenDir);
        float dotLN = dot(lightDir, i.normalDir);

        float3 ambientLighting = UNITY_LIGHTMODEL_AMBIENT.rgb * _Color.rgb;
        float3 diffuseReflection = attenuation * _LightColor0.rgb * _Color.rgb * max(0, dotLN);
        float3 specularReflection;
        if (dotLN < 0.0)
            specularReflection = float3(0.0, 0.0, 0.0);
        else{
            float dotHN = dot(halfwayVector, i.normalDir);
            float dotVN = dot(i.viewDir, i.normalDir);
            float dotHTAlphaX = dot(halfwayVector, i.tangenDir) / _AlphaX;
            float dotHBAlphaY = dot(halfwayVector, binormalDir) / _AlphaY;
            specularReflection = attenuation * _LightColor0 * _SpecColor.rgb
            * sqrt(max(0.0, dotLN/dotVN))
            * exp(-2.0 * (dotHTAlphaX * dotHTAlphaX + dotHBAlphaY * dotHBAlphaY)/(1.0+dotHN));
        }
        return float4(ambientLighting + diffuseReflection + specularReflection, 1.0);
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
}