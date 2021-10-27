Shader "Custom/NoiseEmissionShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _EmissionColor("Emission Color",Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _EmissionTex("Emission Tex",2D)="white"{}
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert

        float _Emission00FN;
        half4 _MainColor;
        half4 _EmissionColor;
        sampler2D _MainTex;
        sampler2D _EmissionTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            half4 c = tex2D(_MainTex, IN.uv_MainTex) * _MainColor;
            float e = tex2D(_EmissionTex, IN.uv_MainTex).a * _Emission00FN;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
            o.Emission = _EmissionColor * e;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
