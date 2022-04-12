// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Unlit/BlackHole"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Twirl("twirl", float) = 1
            _UV(" 유브이" , float) = 0
               _Offset(" 위치 " , float) = 0
         _AlphaCut("투명처리", Color) = (1,1,1,1)
    }   
        SubShader
        {

            Pass
            {
                Tags
            {
                 "RenderPipeline" = "UniversalPipeline"
           "RenderType" = "Transparent"
           "Queue" = "Transparent"
        }
            blend SrcAlpha OneMinusSrcAlpha    //! Blending 옵션 설정

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                    float3 normal : NORMAL;
                  
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;

                 
                };

               




                v2f vert(appdata v)
                {
                    v2f o;
                 
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;


                   
                    return o;
                }
             
                 sampler2D _MainTex;
                 float _Twirl , _UV , _Offset , _AlphaCut;

                 //노말맵
                 sampler2D _Nomarlmap;
                 float4 _BumpTex_ST;

                 fixed4 frag(v2f i) : SV_Target
                 {
                     _Twirl *= _Time.y * 3;
                    // center UV coords
                    float2 uv = i.uv - 0.5;
                    // get angle from the center to uv coordinate
                    float a = atan2(uv.y, uv.x);
                    // get distance from the center
                    float d = length(uv);
                    // offset angle based on twirl strength and distance from the center.
                    // affect the center the most
                    a += (0.5 - d) * _Twirl;
                  
                    // recalculate new uv coordinate with new angle.
                    uv.x = cos(a) * d;
                    uv.y = sin(a) * d;
                    uv += 0.5 ;
                    _UV += _Time.x;
                    

                    fixed4 col = tex2D(_MainTex, uv * _UV - _Offset) ; //scale..
                  

                   

                    return col;
                }
                ENDCG
            }
        }
}

/*

*/