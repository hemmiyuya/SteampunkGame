Shader "Custom/EmissionSample"
{
    Properties{
        _Color("Diffuse Color", Color) = (1,1,1,1)
        _MyEmissionColor("Emission Color",Color) = (0,0,0,0) //Åy1Åz
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }

            CGPROGRAM
            #pragma surface surf Lambert

            float4 _Color;
            float4 _MyEmissionColor; //Åy2Åz

        struct Input { //Åy3Åz
            float2 Dummy; //Åy3Åz
        }; //Åy3Åz

        void surf(Input IN, inout SurfaceOutput o) {
            o.Albedo = _Color; //Åy4Åz
            o.Emission = _MyEmissionColor; //Åy5Åz
        }
        ENDCG
    }
        FallBack "Diffuse"
}
