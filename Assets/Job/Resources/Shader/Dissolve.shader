Shader "Chess/Dissolve"
{
   Properties
   {
		
   	   _Color("Color",Color)=(1,1,1,1)
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
	   
	  
   }
   SubShader
   {
   	   Pass
	   {
	   Tags{ "LightMode" = "ForwardBase" "DisableBatching" = "true"}
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

			
		   v2f vert(a2v v)
		   {
				v2f o;
				o.localPos = v.vertex;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.worldNormal = mul(v.vertex,unity_WorldToObject).xyz;
				o.uv.xy = v.texcoord.xy* _MainTextrue_ST.xy+_MainTextrue_ST.zw;
				o.uv.zw = v.texcoord.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;
				TANGENT_SPACE_ROTATION;
				o.lightDir = mul(rotation,normalize(ObjSpaceLightDir(v.vertex))).xyz;
				//o.viewDir = mul(rotation,normalize(ObjSpaceLightDir(v.vertex))).xyz;
				return o;
		   }
		   fixed4 frag(v2f i):SV_TARGET
		   {
					fixed3 LightDir = normalize(i.lightDir);
	
					fixed4 packedNormal = tex2D(_BumpMap, i.uv.zw);
					fixed3 tangentNormal;
	
					tangentNormal = UnpackNormal(packedNormal);
					tangentNormal.xy *= _BumpScale;
					
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
					
					//float3 Final = (Tex.rgb*Mask.a)+(Mask.rgb*_Color);
					fixed3 albedo = Tex.rgb;

					fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;

					fixed3 diffuse = _LightColor0.rgb * albedo * max(0, dot(tangentNormal, LightDir));

					
					fixed3 color = ambient +diffuse;
		   			
					
					
				

			   return fixed4(color,1.0);
		   }
		   ENDCG
	   }
	 
   }
}
