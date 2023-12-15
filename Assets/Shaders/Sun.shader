Shader"Custom/Sun"
{
	Properties{
        _MainTex ("Texture" , 2D)       = "white"{}
		_Opacity ("Opacity" , range( 0.0 , 1.0 ))  = 1.0
        _NoiseMap ("Noise map" , 2D)               = "gray"{}      //noise texture
        _NoiseInt ("Noise strength" , range(0.0 , 5.0))   = 1.0           //noise strength
        _FlowSpeed ("Flow speed", range(-10.0 , 10.0))= 5.0           //flow speed
        _BumpScale ("Scale", Float) = 1.000000
        _Cutoff ("Alpha Cutoff", Range(0.000000,1.000000)) = 0.500000
        _Glossiness ("Smoothness", Range(0.000000,1.000000)) = 0.500000
        _GlossMapScale ("Smoothness Factor", Range(0.000000,1.000000)) = 1.000000
        [Enum(Specular Alpha, 0,Albedo Alpha, 1)]
        _SmoothnessTextureChannel ("Smoothness texture channel", Float) = 0.000000
        _SpecColor ("Specular", Color) = (0.200000,0.200000,0.200000,1.000000)
        _SpecGlossMap ("Specular", 2D) = "white" { }
        [Normal]  _BumpMap ("Normal Map", 2D) = "bump" { }
        _Parallax ("Height Scale", Range(0.005000,0.080000)) = 0.020000
        _ParallaxMap ("Height Map", 2D) = "black" { }
        _OcclusionStrength ("Strength", Range(0.000000,1.000000)) = 1.000000
        _OcclusionMap ("Occlusion", 2D) = "white" { }
        _EmissionColor ("Emission Color", Color) = (0, 0, 0, 1) // Self-illuminating color, off by default
        _EmissionMap ("Emission Map", 2D) = "white" {} // Self-illuminating texture
        _EmissionStrength ("Emission Strength",Float) = 1.0


	}
	SubShader{
		Tags {
			"Queue" = "Transparent"              
            "RenderType" = "Transparent"         
                                                 
                                                 
            "IgnoreProjector" = "True"           //shader will not influence by projector
            "ForceNoShadowCasting" = "True"      //close shadow casting

		}
		Pass {
                Name"FORWARD"
			    Tags
                {
				    "LightMode" = "ForwardBase"
                }

                ZWrite Off

                Blend One
                OneMinusSrcAlpha //edit blend way


			    CGPROGRAM
			    #pragma vertex vert
			    #pragma fragment frag
                #include "UnityCG.cginc"
			    #pragma multi_compile_fwdbase_fullshadows
			    #pragma target 3.0

                uniform sampler2D _MainTex;
                uniform half _Opacity;
                uniform sampler2D _NoiseMap;
                uniform float4 _NoiseMap_ST;
                uniform half _NoiseInt;
                uniform half _FlowSpeed = 1.0;
                uniform float4 _EmissionColor;
                uniform sampler2D _EmissionMap;
                uniform float _EmissionStrength;

                struct VertexInput
                {
                    float4 vertex : POSITION;
                    float2 uv0 : TEXCOORD0;
                };
                struct VertexOutput
                {
                    float4 pos : SV_POSITION;
                    float4 uv0 : TEXCOORD0; //2 uv
                };
                VertexOutput vert(VertexInput v)
                {
                    VertexOutput o = (VertexOutput) 0;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv0.xy = v.uv0; //uv0.xy sampling MainTex
                    o.uv0.zw = v.uv0 * _NoiseMap_ST.xy + _NoiseMap_ST.zw; //uv0.zw sampling NoiseMap
                    o.uv0.w += frac(_Time.x * _FlowSpeed); //uv0.w will change by time
                    o.uv0.xy -= frac(_Time.x * _FlowSpeed * 0.5);
                    return o;
                }

                half4 frag(VertexOutput i) : SV_TARGET
                {

                    half4 var_MainTex = tex2D(_MainTex, i.uv0.xy);
                    half var_NoiseMap = tex2D(_NoiseMap, i.uv0.zw).r; //sampling noise
                    half4 emission = tex2D(_EmissionMap, i.uv0) * _EmissionColor; // sampling emission texture
                    half noise = lerp(1.0, var_NoiseMap * 2, _NoiseInt); //control noise strength
                    noise = max(0.0, noise); 
                    half opacity = var_MainTex.a * _Opacity * noise;
                    emission.rgb *= _EmissionStrength;
                    var_MainTex.rgb += emission.rgb;
                    return half4(var_MainTex.rgb * opacity, opacity);
                }
				                ENDCG
		                }
	        }
        FallBack"Diffuse"						
}