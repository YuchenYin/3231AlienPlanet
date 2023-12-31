﻿Shader "Custom/LavaSurface"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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

                    v2f vert(appdata v)
                    {
                        v2f o;
                        o.vertex = UnityObjectToClipPos(v.vertex);
                        o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                        o.uv += float2(_Time.y, _Time.y) * 0.1; //move texture by time
                        return o;
                    }

                    float4 frag(v2f i) : SV_Target
                    {
                        float2 uv = i.uv;

                        // Custom Lava Flow Logic
                        uv += float2(sin(_Time.y), cos(_Time.y)) * 0.1;

                        float4 color = tex2D(_MainTex, uv);
                        return color;
                    }
            ENDCG
        }
    }
}
