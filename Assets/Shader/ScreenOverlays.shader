Shader "Custom/ScreenOverlays" {
    Properties{
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _X("X", Float) = 0
        _Y("Y", Float) = 0
        _Width("Width", Float) = 128
        _Height("Height", Float) = 128
    }
    SubShader{
        Tags{"Queue"="Overlay"}
        Pass{
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest Always
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float4 _Color;
            float _X;
            float _Y;
            float _Width;
            float _Height;
            struct a2v{
                float4 vertex : POSITION;
                float4 texcoord : TEXCOORD0;
            };
            struct v2f{
                float4 pos : SV_POSITION;
                float4 tex : TEXCOORD0;
            };
            v2f vert(a2v i){
                v2f o;
                float rx = _X + _ScreenParams.x / 2 + _Width * (i.vertex.x + 0.5);
                float ry = _Y + _ScreenParams.y / 2 + _Height * (i.vertex.y + 0.5);
                float2 rasterPos = float2(rx, ry);
                o.pos = float4(
                    2.0 * rasterPos.x / _ScreenParams.x - 1.0,
                    _ProjectionParams.x * (2.0 * rasterPos.y / _ScreenParams.y - 1.0),
                    _ProjectionParams.y,
                    1.0
                );
                o.tex = float4(i.vertex.x + 0.5, i.vertex.y + 0.5, 0, 0);
                return o;
            }
            float4 frag(v2f i) : COLOR{
                return _Color * tex2D(_MainTex, i.tex.xy);
            }
            ENDCG
        }
    }
}