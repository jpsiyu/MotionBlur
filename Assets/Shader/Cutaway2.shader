Shader "Custom/Cutaway2"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}

    CGINCLUDE
    struct a2v{
        float4 vertex : POSITION;
    };
    struct v2f{
        float4 pos : SV_POSITION;
        float4 posInObjectCoords : TEXCOORD0;
    };
    v2f vert(a2v i){
        v2f o;
        o.pos = UnityObjectToClipPos(i.vertex);
        o.posInObjectCoords = i.vertex;
        return o;
    }
    float4 frag(v2f i) : COLOR{
        if(i.posInObjectCoords.y > 0.0)
            discard;
        return float4(0, 1, 0, 1);
    }
    float4 frag2(v2f i) : COLOR{
        if(i.posInObjectCoords.y > 0.0)
            discard;
        return float4(1, 0, 0, 1);
    }
    ENDCG

	SubShader
	{
		Pass{
			Cull Front
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}

		Pass{
			Cull Back 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag2
			ENDCG
		}
	}
}
