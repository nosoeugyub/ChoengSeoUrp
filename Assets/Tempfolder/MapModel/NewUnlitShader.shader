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
   float4 _MainTex_ST;
    Texture2D _MainTex;
    SamplerState sampler_MainTex;


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
      	  };
             VertexOutput vert(VertexInput v)
          	{
          	    VertexOutput o;      
          	    o.vertex = TransformObjectToHClip(v.vertex.xyz); 	
                o.uv = v.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw;
	            o.normal = TransformObjectToWorldNormal(v.normal);//노멀값을 월드공간으로 변환
            	return o;
          	}					

             half4 frag(VertexOutput i) : SV_Target
             {
                 float4 color = _MainTex.Sample(sampler_MainTex, i.uv );
                 float3 light = _MainLightPosition.xyz;
                 float3 lightcolor = _MainLightColor.rgb;
                 float halflambert_term = saturate(dot(i.normal , light)*0.5f+ 0.7f);
                 color.rgb *= halflambert_term * lightcolor ;
                 return color;
               }
            ENDHLSL  
  	    }
     }

}
