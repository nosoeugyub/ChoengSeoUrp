Shader "Unlit/22"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
        Tags {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Opaque"
            "Queue" = "Geometry"
        }
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

          

            // Receive Shadow
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
           // #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _ADDITIONAL_LIGHTS
            #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
       //   #pragma shader_feature _ALPHATEST_ON

            #pragma multi_compile _ _SHADOWS_SOFT

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
             
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float fogCoord : TEXCOORD1;
                float3 normal : NORMAL;
                float4 shadowCoord : TEXCOORD2;
               
            };

            CBUFFER_START(UnityPerMaterial)

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            half4 _MainTex_ST;

            CBUFFER_END

            v2f vert(appdata v)
            {
                v2f o;
               

                o.vertex = TransformObjectToHClip(v.vertex.xyz); //MVP랑 같음 이름만 달라짐.
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = TransformObjectToWorldNormal(v.normal); // Normal Transform은 따로 행렬이 필요.
              

                VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
                o.shadowCoord = GetShadowCoord(vertexInput);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
               

                Light mainLight = GetMainLight(i.shadowCoord);

                float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv) ;

                float NdotL = saturate(dot(_MainLightPosition.xyz, i.normal))*0.5f + 0.7f; // NdotL로 간단히 라이팅한다.
                half3 ambient = SampleSH(i.normal);

                col.rgb *= NdotL * _MainLightColor.rgb * mainLight.shadowAttenuation * mainLight.distanceAttenuation + ambient;
               

                return col;
            }
            ENDHLSL
        }

        //

                 Pass
        {
            Name "ShadowCaster"
            Tags{"LightMode" = "ShadowCaster"}

            ZWrite On
            ZTest LEqual
            Cull Back

            HLSLPROGRAM
            // Required to compile gles 2.0 with standard srp library
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature _ALPHATEST_ON

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            #pragma shader_feature _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            #pragma vertex ShadowPassVertex
            #pragma fragment ShadowPassFragment

            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitInput.hlsl"
           
            ENDHLSL
        }

        Pass
        {
            Name "DepthOnly"
            Tags{"LightMode" = "DepthOnly"}

            ZWrite On
            ColorMask 0
          Cull Back

            HLSLPROGRAM
            // Required to compile gles 2.0 with standard srp library
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0

            #pragma vertex DepthOnlyVertex
            #pragma fragment DepthOnlyFragment

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature _ALPHATEST_ON
           

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing

            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/DepthOnlyPass.hlsl"

            ENDHLSL
        }
    }
}
