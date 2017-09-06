// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Cutaway"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Pass{
			Cull Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
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

			ENDCG
		}
	}
}
