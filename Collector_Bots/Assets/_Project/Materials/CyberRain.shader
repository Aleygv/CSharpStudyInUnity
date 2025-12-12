Shader "Unlit/CyberRain"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScrollSpeed("Scroll Speed", Range(0.1, 10)) = 2.0
        _TrailStrength("Trail Strength", Range(0.1, 5)) = 2.0
        _Color("Glow Color", Color) = (0, 0.8, 1, 1)
    }

    SubShader
    {
        Tags {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
            "IgnoreProjector" = "True"
        }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off // Если хочешь видеть внутреннюю часть объекта

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _ScrollSpeed;
            float _TrailStrength;
            fixed4 _Color;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;

                // Движение вниз
                uv.y += _Time.y * _ScrollSpeed;

                // Шлейф: три слоя с разными смещениями
                fixed4 col1 = tex2D(_MainTex, uv);
                fixed4 col2 = tex2D(_MainTex, uv + float2(0, -0.05 * _TrailStrength));
                fixed4 col3 = tex2D(_MainTex, uv + float2(0, -0.1 * _TrailStrength));

                // Уменьшаем прозрачность для шлейфа
                col2.a *= 0.7;
                col3.a *= 0.4;

                // Суммируем
                fixed4 finalCol = col1 + col2 + col3;
                finalCol.rgb *= _Color.rgb; // Умножаем на цвет свечения
                finalCol.a = max(finalCol.a, 0.01); // Минимальная альфа, чтобы не исчезал

                return finalCol;
            }
            ENDCG
        }
    }
}