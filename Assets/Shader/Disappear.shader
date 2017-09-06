Shader "Custom/Cutaway"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _Offset("Offset", Float) = 0
		_CenterY("CenterY", Float) = 0
	}
	SubShader
	{
		Pass{
			Cull Off
			CGPROGRAM
            #include "UnityCG.cginc"
			#include "UnityUI.cginc"
			#pragma vertex vert
			#pragma fragment frag

            sampler2D _MainTex;
            float4 _ClipRect;
            float _Offset;
			float _CenterY;
			struct a2v{
				float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
			};
			struct v2f{
				float4 pos : SV_POSITION;
				float4 posInObjectCoords : TEXCOORD0;
                float2 uv : TEXCOORD1;
			};
			v2f vert(a2v i){
				v2f o;
				o.pos = UnityObjectToClipPos(i.vertex);
				o.posInObjectCoords = i.vertex;
                o.uv = i.uv;
				return o;
			}
			float4 frag(v2f i) : COLOR{
				if(i.posInObjectCoords.y - _CenterY > _Offset)
					discard;
                float4 color = tex2D(_MainTex, i.uv);
				color.a *= UnityGet2DClipping(i.pos.xy, _ClipRect);
				clip(color.a - 0.001);
				return color;
			}

			ENDCG
		}
	}
}
