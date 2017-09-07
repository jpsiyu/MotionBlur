// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/PixelLighting" {
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
        o.posWorld = mul(unity_ObjectToWorld, i.vertex);
        o.normalDir = normalize(mul(unity_ObjectToWorld, float4(i.normal, 0)).xyz);
        o.pos = UnityObjectToClipPos(i.vertex);
        return o;
    }
    float4 frag(v2f i) : COLOR{
        float3 normalDirection = normalize(i.normalDir);
        float3 viewDirection = normalize(_WorldSpaceCameraPos - i.posWorld.xyz);
        float3 lightDirection;
        float attenuation;

        if (0.0 == _WorldSpaceLightPos0.w){
            attenuation = 1.0;
            lightDirection = normalize(_WorldSpaceLightPos0.xyz);
        } else{
            float3 vertexToLightSource = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
            float distance = length(vertexToLightSource);
            attenuation = 1.0 / distance; // linear attenuation 
            lightDirection = normalize(vertexToLightSource);
        }

        float3 ambientLighting = UNITY_LIGHTMODEL_AMBIENT.rgb * _Color.rgb;

        float3 diffuseReflection = attenuation * _LightColor0.rgb * _Color.rgb * max(0.0, dot(normalDirection, lightDirection));

        float3 specularReflection;
        if (dot(normalDirection, lightDirection) < 0.0) {
            specularReflection = float3(0.0, 0.0, 0.0); 
        }else{
            specularReflection = attenuation * _LightColor0.rgb * _SpecColor.rgb * pow(max(0.0, dot(reflect(-lightDirection, normalDirection), viewDirection)), _Shininess);
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
    Fallback "Specular"
}