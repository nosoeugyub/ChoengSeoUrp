Shader "Unlit/NewUnlitShader"
{
   Properties 
  {
       _MainTex("Main Texture", 2D) = "white" {}
   }

       SubShader
   {
    Tags
   {
       "RenderPipeline" = "UniversalPipeline"
       "RenderType" = "Opaque"
       "Queue" = "Geometry"
   }
   Pass
   {
       Name "Universal Forward"
       Tags {"LightMode" = "UniversalForward"}

      
       HLSLPROGRAM
       #pragma prefer_hlslcc gles
       #pragma exclude_renderers d3d11_9x
       #pragma vertex vert
       #pragma fragment frag	

      #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
           #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
      
       // Receive Shadow
             #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
             #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
             #pragma multi_compile _ _ADDITIONAL_LIGHTS
             #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
             #pragma multi_compile _ _SHADOWS_SOFT


          
     
      

   

          struct VertexInput
          {
           	float4 vertex : POSITION;
	        float3 normal : NORMAL;// 노멀값불러오기
            float3 light : COLOR;
            float2 uv       : TEXCOORD0;
           

           
              
         
          };

          struct VertexOutput
          {
            float4 vertex  	: SV_POSITION;
	       float3 normal      : NORMAL;// 노멀값불러오기
           float3 light : COLOR;
           float2 uv      	: TEXCOORD0;
           float4 shadowCoord : TEXCOORD1;
           
        
      	  };

          CBUFFER_START(UnityPerMaterial)
              float4 _MainTex_ST;
             Texture2D _MainTex;
              SamplerState sampler_MainTex;
              CBUFFER_END
     
             VertexOutput vert(VertexInput v)
          	{
          	    VertexOutput o;   
            
          	    o.vertex = TransformObjectToHClip(v.vertex.xyz); 	
                o.normal = TransformObjectToWorldNormal(v.normal);//노멀값을 월드공간으로 변환
                o.uv = v.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw;
            
                ertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
                o.shadowCoord = GetShadowCoord(vertexInput);
          
            	return o;
          	}		

          
             half4 frag(VertexOutput i) : SV_Target
             {

                
                   Light mainLight = GetMainLight();// 실시간 그림자

                 float4 color = _MainTex.Sample(sampler_MainTex, i.uv );
                 half3 ambient = SampleSH(i.normal);
                 
                 float3 light = _MainLightPosition.xyz;
                 float3 lightcolor = _MainLightColor.rgb;
        
               
                float halflambert_term = LightingLambert(lightcolor, light, i.normal) * 0.5f + 0.7f;
                color.rgb *= pow(halflambert_term, 5) * lightcolor;

                 return color;
             }
void CustomLight_float( float3 worldoPos, out float3 Direction , out float3 Color, out float ShadowAtten){
    #ifdef SHADERGRAPH_PREVIEW
        Direction = float3(1,1,1);
        Color = float3(1,1,1);
        ShadowAtten = 1.0f;
    #else
    
    //shadow Coord 만들기 
    #if defined(_MAIN_LIGHT_SHADOWS_SCREEN) && !defined(_SURFACE_TYPE_TRANSPARENT)
    half4 clipPos = TransformWorldToHClip(worldPos);
    half4 shadowCoord =  ComputeScreenPos(clipPos);
    #else
    half4 shadowCoord =  TransformWorldToShadowCoord(worldoPos);
    #endif
     
    Light light = GetMainLight();
    Direction = light.direction;
    Color = light.color;

    //메인라이트가 없거나 리시브 셰도우 오프가 되어 있을때 
    #if !defined(_MAIN_LIGHT_SHADOWS) || defined(_RECEIVE_SHADOWS_OFF)
        ShadowAtten = 1.0f;
    #endif

    //ShadowAtten 받아와서 만들기 
    #if SHADOWS_SCREEN
        ShadowAtten = SampleScreenSpaceShadowmap(shadowCoord);
    #else
        ShadowSamplingData shadowSamplingData = GetMainLightShadowSamplingData();
        half shadowStrength = GetMainLightShadowStrength();
        ShadowAtten = SampleShadowmap(shadowCoord, TEXTURE2D_ARGS(_MainLightShadowmapTexture,
        sampler_MainLightShadowmapTexture),
        shadowSamplingData, shadowStrength, false);
    #endif

    #endif
}
            
            ENDHLSL  
  	    }
     }

}
