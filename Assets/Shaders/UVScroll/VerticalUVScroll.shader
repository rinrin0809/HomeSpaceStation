Shader "Unlit/VerticalUVScroll"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}

    // X方向のスクロールスピード
    _XSpeed("X Scroll Speed", Range(-50.0, 50.0)) = 0.1

        // Y方向のスクロールスピード
        _YSpeed("Y Scroll Speed", Range(-50.0, 50.0)) = 0.1
    }
        SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
        }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _XSpeed; // X方向のスクロールスピード
            float _YSpeed; // Y方向のスクロールスピード

            // 頂点シェーダー
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            // フラグメントシェーダー
            fixed4 frag(v2f i) : SV_Target
            {
                // UVスクロールを適用
                float2 scrollUV = i.uv;
                scrollUV.x += _XSpeed * _Time.y; // 時間経過によるX方向スクロール
                scrollUV.y += _YSpeed * _Time.y; // 時間経過によるY方向スクロール

                // UV座標を0～1の範囲に制限してループ効果を追加
                scrollUV.x = frac(scrollUV.x);
                scrollUV.y = frac(scrollUV.y);

                // テクスチャカラー取得
                fixed4 col = tex2D(_MainTex, scrollUV);
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
