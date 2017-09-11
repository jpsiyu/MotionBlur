// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "FresnelHighLight" {
    Properties{
        _Color("Diffuse Color", Color) = (1, 1, 1, 1)
        _SpecColor("Specular Color", Color) = (1, 1, 1, 1)
        _Shininess("Shininess", Float) = 10
    }
    CGINCLUDE
    #include "UnityCG.cginc"
    uniform float4 _LightColor0;
    uniform float4 _Color;
    uniform float4 _SpecColor;
    uniform float _Shininess;
    struct a2v{
        float4 vertex : POSITION;
        float3 normal : NORMAL;
    };
    struct v2f{
        float4 pos : SV_POSITION;
        float4 posWorld : TEXCOORD0;
        float3 normalDir : TEXCOORD1;
    };
    v2f vert(a2v i){
        v2f o;
        o.pos = UnityObjectToClipPos(i.vertex);
        o.posWorld = mul(unity_ObjectToWorld, i.vertex);
        o.normalDir = normalize(mul(unity_ObjectToWorld, i.normal));
        return o;
    }
    float4 frag(v2f i) : COLOR{
        float3 viewDir = normalize(_WorldSpaceCameraPos - i.posWorld.xyz);
        float3 lightDir;
        float attenuation;
        if (0.0 == _WorldSpaceLightPos0.w){
            attenuation = 1.0;
            lightDir = normalize(_WorldSpaceLightPos0.xyz);
        }else{
            float3 vertex2light = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
            float distance = length(vertex2light);
            attenuation = 1.0 / distance;
            lightDir = normalize(vertex2light);
        }
        float3 ambientLight = UNITY_LIGHTMODEL_AMBIENT.rgb * _Color.rgb;
        float3 diffuseReflection = attenuation * _LightColor0.rgb * _Color.rgb * max(0, dot(i.normalDir, lightDir));
        float3 specularReflection;
        if (dot(i.normalDir, lightDir) < 0.0){
            specularReflection = float3(0,0,0);
        }else{
            float3 halfwayDir = normalize(lightDir + viewDir);
            float w = pow(1.0 - max(0.0, dot(halfwayDir, viewDir)), 5.0);
            specularReflection = attenuation * _LightColor0.rgb * lerp(_SpecColor.rgb, float3(1.0, 1.0, 1.0), w) 
            * pow(max(0.0, dot(reflect(-lightDir, i.normalDir), viewDir)), _Shininess);
        }
        return float4(ambientLight + diffuseReflection + specularReflection, 1.0);
    }
    ENDCG

    SubShader{
        Pass{
            Tags{"LightMode" = "ForwardBase"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    }
    Fallback "Specul"
}