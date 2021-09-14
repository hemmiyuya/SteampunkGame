Shader "Custom/EmissionSample"
{
    Properties{
        _Color("Diffuse Color", Color) = (1,1,1,1)
        _MyEmissionColor("Emission Color",Color) = (0,0,0,0) //�y1�z
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }

            CGPROGRAM
            #pragma surface surf Lambert

            float4 _Color;
            float4 _MyEmissionColor; //�y2�z

        struct Input { //�y3�z
            float2 Dummy; //�y3�z
        }; //�y3�z

        void surf(Input IN, inout SurfaceOutput o) {
            o.Albedo = _Color; //�y4�z
            o.Emission = _MyEmissionColor; //�y5�z
        }
        ENDCG
    }
        FallBack "Diffuse"
}
