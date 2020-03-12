Shader "Unlit/K_TestShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_DistortSkyTex("Sky", 2D) = "white" {}
		_DistorTransArea("TransArea",2D) = "white"{}
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
			sampler2D _DistortSkyTex;
			sampler2D _DistorTransArea;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
				fixed4 sky_offset = tex2D(_DistortSkyTex, i.uv );
				sky_offset = tex2D(_DistortSkyTex, i.uv + sky_offset.xyz*0.05 );
				fixed4 trans_offset = tex2D(_DistorTransArea,i.uv);
				fixed4 col = tex2D(_MainTex, i.uv+trans_offset.xyz*0.007 );
				if(col.r == 0)
				{
					return sky_offset;
				}
				else
				{
					return col;
				}
            }
            ENDCG
        }

    }
}
