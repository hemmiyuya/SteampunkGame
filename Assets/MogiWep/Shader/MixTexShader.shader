Shader "Custom/MixTexShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Main Texture", 2D) = "white" {}
        _SubTex("Sub Texture",2D) = "white"{}
        _MaskTex("Mask Texture",2D) = "white"{}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _SubTex;
        sampler2D _MaskTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;

        half _Glossiness;
        half _Metallic;

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c1 = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                fixed4 c2 = tex2D(_SubTex, IN.uv_MainTex);
                fixed4 p = tex2D(_MaskTex, IN.uv_MainTex);
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
                o.Albedo = lerp(c1, c2, p);

        }
        ENDCG
    }
        FallBack "Diffuse"
}
