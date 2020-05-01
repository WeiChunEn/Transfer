Shader "Chess/OutLine" {
 Properties {
  _MainTex ("Base (RGB)", 2D) = "white" {}
  _BumpMap ("Bumpmap", 2D) = "bump" {} 
    _Color("Color", Color) = (0.26,0.19, 0.13, 0.0)
  _RimColor("Rim Color", Color) = (0.26,0.19, 0.13, 0.0)
  _RimPower("Rim Power", Range(0.1, 8.0)) = 3.0
 }
 SubShader {
  Tags { "RenderType"="Opaque" }
  LOD 200
  
  CGPROGRAM
  #pragma surface surf Lambert
  
  sampler2D _MainTex;
  sampler2D _BumpMap; 
  float4 _RimColor;
  float4 _Color;
  float _RimPower;
  struct Input {
   float2 uv_MainTex; 
   float2 uv_BumpMap; 
   float3 viewDir; 
  };
  void surf (Input IN, inout SurfaceOutput o) {
   half4 c = tex2D (_MainTex, IN.uv_MainTex);
   o.Albedo = c.rgb*_Color;
   o.Alpha = c.a;
   //获取法线值【UnpackNormal是unity自带的标准解压法线用的】
   o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap)); 
   // viewDir点到相机的向量归一 与 该点的法线 求点乘。得出余弦值【0-1】，如果两条线平行方向一样值1，相反-1，垂直0
   // 所以越靠近越靠近边缘，值越小，saturate相当于mathf.clamp(value,0,1)
   // 如果1-这个值，越靠近边缘，rim值越大，自发光就越强
           half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal)); 
           // pow是求幂函数，所以rim越大，效果越明显
           o.Emission = _RimColor.rgb * pow (rim, _RimPower); 
  }
  ENDCG
 }
 FallBack "Diffuse"
}