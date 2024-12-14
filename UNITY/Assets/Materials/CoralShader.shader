Shader "Custom/GrassColorShader"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1, 1, 1, 1)
        _Color1 ("Color 1", Color) = (0, 1, 0, 1)
        _Color2 ("Color 2", Color) = (0, 0, 1, 1)
        _Color3 ("Color 3", Color) = (1, 1, 0, 1)
        _BlendAmount ("Blend Amount", Range(0, 1)) = 0.5
        _MainTex ("Base Texture", 2D) = "white" { }
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _BaseColor;
            float4 _Color1;
            float4 _Color2;
            float4 _Color3;
            float _BlendAmount;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                // Sample the base texture
                half4 baseColor = tex2D(_MainTex, i.uv);

                // Blend multiple colors
                half3 blendedColor = lerp(_Color1.rgb, _Color2.rgb, _BlendAmount);
                blendedColor = lerp(blendedColor, _Color3.rgb, _BlendAmount);

                // Apply base color influence
                return half4(baseColor.rgb * blendedColor, baseColor.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
