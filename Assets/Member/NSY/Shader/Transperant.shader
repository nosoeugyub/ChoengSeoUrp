﻿Shader "Custom/Transperant"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Alpha("Alpha" , Range(0, 1)) = 0.5

    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }

            //1pass
            ZWrite on    //! zwrite는 기본적으로 켜져있지만 확실하게 코드를 짜줍니다.
            ColorMask 0    //! 렌더하지 않습니다.
            CGPROGRAM

            #pragma surface surf _NoLit nolight keepalpha noambient noforwardadd nolightmap novertexlights noshadow        //! 최적화 코드

            struct Input
            {
                float2 color:COLOR;
            };

            void surf(Input IN, inout SurfaceOutput o)
            {
            }
            float4 Lighting_NoLit(SurfaceOutput s, float3 lightDir, float atten)    //! 커스텀 라이트 함수를 만들어 아무런 연산을 하지 않습니다.
            {
                return 0.0f;
            }
            ENDCG

                //2pass
            ZWrite off
            Cull off
            CGPROGRAM

            #pragma surface surf Lambert alpha:blend

            sampler2D _MainTex;
            fixed4 _Color;
            float _Alpha;

            struct Input
            {
                float2 uv_MainTex;
                float3 viewDir;
            };



            void surf(Input IN, inout SurfaceOutput o)
            {

                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
                o.Albedo = c.rgb;



                o.Alpha = _Alpha;
            }
            ENDCG
        }
            FallBack "Diffuse"
}
