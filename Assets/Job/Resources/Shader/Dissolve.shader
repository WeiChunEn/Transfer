Shader "Chess/Dissolve"
{
   Properties
   {
		_Color("Color",Color)=(1,1,1,1)
		_MaskColor("MaskColor",Color)=(1,1,1,1)
		_AlphaScale("Alpha Scale",Range(0,1)) = 0.5
		_MainTextrue("Main Texture",2D)="white"{}
		_MaskTextrue("Mask Texture",2D) = "white"{}
		_DissloveSize("Dissolve Size",float) = 0
		_BumpMap("Normal Map", 2D) = "bump" {}
		_BumpScale("Bump Scale", Float) = 1.0
		_DissolveCutoff("Dissolve Cutoff",Range(0,1))=1
		_DissolveColorA("Dissolve Color A",Color) = (1,1,1,1)
		_DissolveColorB("Dissolve Color B",Color) = (1,1,1,1)
		_ColorFactorA("ColorFactorA",Range(0,1))=0
		_ColorFactroB("ColorFactorB",Range(0,1))=0

		_SpecularMask("Specular Mask", 2D) = "white" {}
	_SpecularScale("Specular Scale", Float) = 1.0
		_Specular("Specular", Color) = (1, 1, 1, 1)
		_Gloss("Gloss", Range(8.0, 256)) = 20
   }
   SubShader
   {
	   Tags{"Queue" = "Transparent" "IgnoreProjector" = "Ture""RenderType" = "Transparent"}
   	   Pass
	   {
		   Tags{ "LightMode" = "ForwardBase" "DisableBatching" = "true"}
		   //ZWrite Off
		   Blend SrcAlpha OneMinusSrcAlpha
	   	   CGPROGRAM

		   #pragma vertex vert
		   #pragma fragment frag
		   #include "UnityCG.cginc"
		   #include "Lighting.cginc" 
		   #include "SimplexNoise3D.hlsl"
		   struct a2v
		   {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 texcoord : TEXCOORD0;
		   };
		   struct v2f
		   {
				float4 pos : SV_POSITION;
				float4 uv : TEXCOORD0;
				
				float3 lightDir:TEXCOORD1;
				float3 viewDir : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				float4 localPos : TEXCOORD4;
				
		   };

		  
		float _DissloveSize;
		fixed4 _Color;
		fixed4 _MaskColor;
		float _AlphaScale;
		   
		sampler2D _MaskTextrue;
		float4 _MaskTextrue_ST;

		sampler2D _MainTextrue;
		float4 _MainTextrue_ST;

		   

		sampler2D _BumpMap;
		float4 _BumpMap_ST;

		float _BumpScale;
		

		float _DissolveCutoff;
		fixed4 _DissolveColorA;
		fixed4 _DissolveColorB;
		float _ColorFactorA;
		float _ColorFactroB;

		sampler2D _SpecularMask;
		float _SpecularScale;
		fixed4 _Specular;
		float _Gloss;
	
		v2f vert(a2v v)
		{
			v2f o;
			o.localPos = v.vertex;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.worldNormal = mul(v.vertex,unity_WorldToObject).xyz;
			o.uv.xy = v.texcoord.xy* _MainTextrue_ST.xy+_MainTextrue_ST.zw;
			//o.uv.zw = v.texcoord.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;

			TANGENT_SPACE_ROTATION;
			o.lightDir = mul(rotation,normalize(ObjSpaceLightDir(v.vertex))).xyz;
			o.viewDir = mul(rotation,normalize(ObjSpaceLightDir(v.vertex))).xyz;
			return o;
		}
		fixed4 frag(v2f i):SV_TARGET
		{
				fixed3 tangentLightDir = normalize(i.lightDir);
				fixed3 tangentViewDir = normalize(i.viewDir);
				//fixed3 LightDir = normalize(i.lightDir);
	
				fixed3 tangentNormal = UnpackNormal(tex2D(_BumpMap, i.uv));
				tangentNormal.xy *= _BumpScale;
				tangentNormal.z = sqrt(1.0 - saturate(dot(tangentNormal.xy, tangentNormal.xy)));
				float4 Tex = tex2D(_MainTextrue,i.uv);
				float4 Mask = tex2D(_MaskTextrue,i.uv);
					
				float opaque = 1;
				float noiseVal = snoise(i.localPos * _DissloveSize);
				float blueCol = 0;

				float a = _DissolveCutoff * 2;
				float4 r = lerp (opaque, noiseVal, saturate(a));
				a -= 1;
				r = lerp (r, blueCol, saturate(a));

				clip(r - 0.2);

				float lerpValue = r;
				
				if(lerpValue < _ColorFactorA)
				{
					return _DissolveColorA;
				}
				else if(lerpValue < _ColorFactroB)
				{
					return _DissolveColorB;
				}
					
				//float3 Final = (Tex.rgb*Mask.a)+(Mask.rgb*_MaskColor);
				fixed3 albedo = Tex.rgb*_Color.rgb;

				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT * albedo;

				fixed3 diffuse = _LightColor0.rgb * albedo * max(0, dot(tangentNormal, tangentLightDir));

				fixed3 halfDir = normalize(tangentLightDir + tangentViewDir);
				fixed specularMask = tex2D(_SpecularMask,i.uv).r * _SpecularScale;
				fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(max(0, dot(tangentNormal, halfDir)), _Gloss) * specularMask;
				fixed3 color = ambient +diffuse+specular ;
		   			
					
					
				

			return fixed4(color,Tex.a*_AlphaScale);
		}
		ENDCG
	   }
	 
   }Fallback "Specular"
}
