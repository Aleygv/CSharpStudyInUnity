Shader "Unlit/CyberRain"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (0.0, 0.9, 1.0, 1.0)
        _Speed ("Fall Speed", Range(0.1, 10)) = 3.0
        _Density ("Dot Density", Range(1, 100)) = 30
        _Glow ("Glow Intensity", Range(0, 5)) = 1.5
        _Trail ("Trail Length", Range(0, 1)) = 0.3
        _Transparency ("Transparency", Range(0,1)) = 0.7
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

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

            float4 _MainColor;
            float _Speed;
            float _Density;
            float _Glow;
            float _Trail;
            float _Transparency;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Хэш-функция для генерации случайного шума
            float hash21(float2 p)
            {
                p = frac(p * float2(123.34, 456.21));
                p += dot(p, p + 45.32);
                return frac(p.x * p.y);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv * _Density;

                // "Падение" точек вниз
                uv.y += _Time.y * _Speed;

                // Определяем блеск точек
                float cell = floor(uv.y);
                float localY = frac(uv.y);
                float rnd = hash21(float2(floor(uv.x), cell));

                float brightness = smoothstep(0.0, 0.2, 1.0 - localY) * step(0.98, rnd);

                // Эффект шлейфа — затухание
                brightness += pow(max(0, 1.0 - localY / _Trail), 2.0) * step(0.95, rnd);

                float glow = brightness * _Glow;
                fixed4 col = _MainColor * glow;
                col.a = brightness * _Transparency;

                return col;
            }
            ENDCG
        }
    }
}