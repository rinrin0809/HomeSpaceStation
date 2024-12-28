Shader "Unlit/VerticalUVScroll"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}

    // X�����̃X�N���[���X�s�[�h
    _XSpeed("X Scroll Speed", Range(-50.0, 50.0)) = 0.1

        // Y�����̃X�N���[���X�s�[�h
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
            float _XSpeed; // X�����̃X�N���[���X�s�[�h
            float _YSpeed; // Y�����̃X�N���[���X�s�[�h

            // ���_�V�F�[�_�[
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            // �t���O�����g�V�F�[�_�[
            fixed4 frag(v2f i) : SV_Target
            {
                // UV�X�N���[����K�p
                float2 scrollUV = i.uv;
                scrollUV.x += _XSpeed * _Time.y; // ���Ԍo�߂ɂ��X�����X�N���[��
                scrollUV.y += _YSpeed * _Time.y; // ���Ԍo�߂ɂ��Y�����X�N���[��

                // UV���W��0�`1�͈̔͂ɐ������ă��[�v���ʂ�ǉ�
                scrollUV.x = frac(scrollUV.x);
                scrollUV.y = frac(scrollUV.y);

                // �e�N�X�`���J���[�擾
                fixed4 col = tex2D(_MainTex, scrollUV);
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
