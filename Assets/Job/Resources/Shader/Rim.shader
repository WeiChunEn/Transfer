// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Unlit Transparent Cutout With Color" {
 Properties {
  _Color ("Color (RGB) Blending (A)", Color) = (1,0,0,0)
  _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
  _Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
 }
  
 // Fragment shader by Janus Huang
 SubShader {
  
  Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
  Blend SrcAlpha OneMinusSrcAlpha
  Lighting Off
   
  Pass {
   CGPROGRAM
   #pragma vertex vert
   #pragma fragment frag
   #include "UnityCG.cginc"
    
   struct appdata {
    fixed4 vertex : POSITION;
    fixed3 normal : NORMAL;
    fixed2 texcoord : TEXCOORD0;
   };
    
   struct v2f {
    fixed4 pos : SV_POSITION;
    fixed2 uv : TEXCOORD0;
    fixed4 color : COLOR;
   };
    
   fixed4 _MainTex_ST;
   fixed4 _Color;
   fixed _Cutoff;
    
   v2f vert (appdata_base v) {
    v2f o;
     
    o.pos = UnityObjectToClipPos (v.vertex);
    fixed3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
    // 越是面向camera的vertex在dot計算後會獲得越大的計算結果
    fixed dotProduct = dot(v.normal, viewDir);
    // smoothstep用法同lerp, 但其曲線在頭尾都會趨緩
    o.color = smoothstep(0.5, 1, dotProduct);
    o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
     
    return o;
   }
    
   sampler2D _MainTex;
    
   fixed4 frag(v2f i) : COLOR {
    fixed4 texcol = tex2D(_MainTex, i.uv);
    // clip同AlphaTest
   // clip( texcol.a - _Cutoff );
    // 受擊時的color計算 - 將貼圖加亮1.5倍後乘上dot計算後的黑白色(外黑內白)再加上_Color
    fixed3 hitColor = texcol.rgb * 1.5 * i.color.rgb + _Color.rgb;
    // 以_Color的alpha控制貼圖與受擊時的混合程度
    texcol.rgb = lerp( texcol.rgb, hitColor, _Color.a);
 
    return texcol;
   }   
   ENDCG
  }
 }
}