Shader "Custom/VertexLighting" {
    Properties{
        _Color ("Diffuse Color", Color) = (1, 1, 1, 1)
        _SpecColor("Specular Color", Color) = (1, 1, 1, 1)
        _Shininess("Shininess", FLoat) = 10
    }
    CGINCLUDE
    #include "UnityCG.cginc"
    float4 _Color;
    float4 _SpecColor;
    Float _Shininess;
    uniform float4 _LightColor0;
    struct a2v{
        float4 vertex : POSITION;
        float3 normal : NORMAL;
    };
    struct v2f{
        float4 pos : SV_POSITION;
        float4 color : COLOR;
    };

    float3 calLightDir(float3 viewDir){
        if (0 == _WorldSpaceLightPos0.w) 
            return normalize(_WorldSpaceLightPos0.xyz);
        else
            return normalize(viewDir);
    }

    float calAttenuation(float3 viewDir){
        float attenuation;
        if (0 == _WorldSpaceLightPos0.w) 
            attenuation = 1;
        else{
            float dis = length(viewDir);
            attenuation = 1.0 / dis;
        }
        return attenuation;
    }

    v2f vert(a2v i){
        v2f o;
        float3 viewDir = _WorldSpaceLightPos0.xyz - mul(unity_ObjectToWorld, i.vertex.xyz);
        float3 viewDirNor = normalize(viewDir);
        float3 ndir = normalize(mul(unity_ObjectToWorld, float4(i.normal, 0)).xyz);
        float3 ldir = calLightDir(viewDir);
        float attenuation = calAttenuation(viewDir);

        //diffuse reflection
        float3 diffRef = attenuation * _LightColor0.rgb * _Color.rgb * max(0, dot(ndir, ldir));
        //ambient light
        float3 ambLight = UNITY_LIGHTMODEL_AMBIENT.rbg * _Color.rgb;
        //specular reflection
        float3 specularRef;
        if (dot(ndir, ldir) < 0)
            specularRef = float3(0, 0, 0);
        else{
            specularRef = attenuation * _LightColor0.rgb * _SpecColor.rgb * pow(max(0, dot(reflect(-ldir, ndir), viewDirNor)), _Shininess);
        }

        o.color = float4(ambLight + diffRef + specularRef, 1.0);
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