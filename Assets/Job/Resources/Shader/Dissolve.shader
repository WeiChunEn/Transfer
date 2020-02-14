Shader "Chess/Dissolve"
{
   Properties
   {
   	   _Color("Color",Color)=(1,1,1,1)
	   _MainTextrue("Main Texture",2D)="white"{}
	   _DissolveTextur("Dissolve Texture",2D)="white"{}
	   _DissolveCutoff("Dissolve Cutoff",Range(0,1))=1
	   _DissolveColorA("Dissolve Color A",Color) = (1,1,1,1)
	   _DissolveColorB("Dissolve Color B",Color) = (1,1,1,1)
	   _ColorFactorA("ColorFactorA",Range(0,1))=0
	   _ColorFactroB("ColorFactorB",Range(0,1))=0
	   _Size("Size", Float) = 0.1 //光暈範圍
       _OutLightPow("Falloff",Float) = 5 //光暈平方參數
       _OutLightStrength("Transparency", Float) = 15 //光暈強度
   }
   SubShader
   {
   	   Pass
	   {
	   Tags{ "LightMode" = "ForwardBase" }
	   	   CGPROGRAM

		   #pragma vertex vert
		   #pragma fragment frag
		   #include "UnityCG.cginc"
		   #include "Lighting.cginc" 

		   struct v2f
		   {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float worldNormal :TEXCOORD1;
		   };


		   fixed4 _Color;
		   sampler2D _MainTextrue;
		   float4 _MainTextrue_ST;
		   sampler2D _DissolveTextur;
		   float4 _DissolveTextur_ST;
		   float _DissolveCutoff;
		  fixed4 _DissolveColorA;
			fixed4 _DissolveColorB;
			float _ColorFactorA;
			float _ColorFactroB;

			
		   v2f vert(appdata_base v)
		   {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord,_MainTextrue);
				o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
				return o;
		   }
		   fixed4 frag(v2f i):SV_TARGET
		   {
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
				fixed3 worldNormal = normalize(i.worldNormal);
				fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
				fixed3 diffuse = _LightColor0.rgb*_Color.rgb*saturate(dot(worldNormal,worldLightDir));
		   	   float4 Tex = tex2D(_MainTextrue,i.uv);
			   float4 Dissolve = tex2D(_DissolveTextur,i.uv);
		       clip(Dissolve.rgb-_DissolveCutoff);
			   float lerpValue = _DissolveCutoff/Dissolve.rgb;
		       if(lerpValue >_ColorFactorA)
			   {
			     	if(lerpValue>_ColorFactroB)
					{
						return _DissolveColorB;
					}
					return _DissolveColorA;
			   }
			   fixed3 color = diffuse + ambient;
			   /*float percentage = _DissolveThreshold / dissolveValue.r;
		
				float lerpEdge = sign(percentage - _ColorFactor - _DissolveEdge);
		
				fixed3 edgeColor = lerp(_DissolveEdgeColor.rgb, _DissolveColor.rgb, saturate(lerpEdge));
		
				float lerpOut = sign(percentage - _ColorFactor);
		
				fixed3 colorOut = lerp(color, edgeColor, saturate(lerpOut));*/
				

			   return fixed4(Tex*_Color);
		   }
		   ENDCG
	   }
	  /* Pass
	   {
	   	  
			 Tags{ "LightMode" = "Always" }
            Cull Front
            Blend SrcAlpha One
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			float4 _Color;
			 uniform float _Size;
            uniform float _OutLightPow;
            uniform float _OutLightStrength;
			struct v2f
			{
				float4 pos:SV_POSITION;
				float3 worldNormal:TEXCOORD0;
				float3 worldPos:TEXCOORD1;
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				v.vertex.xyz += v.normal*_Size;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.worldNormal = v.normal;
				o.worldPos = mul(unity_ObjectToWorld,v.vertex);
				return o;
			}
			float4 frag(v2f i):COLOR
			{
				i.worldNormal = normalize(i.worldNormal);
				float3 viewDir = normalize(-UnityWorldSpaceViewDir(i.worldPos));
				float4 Atom = _Color;
				Atom.a = pow(saturate(dot(viewDir,i.worldNormal)),_OutLightPow);
				Atom.a *=_OutLightStrength*dot(viewDir,i.worldNormal);
				return Atom;
			}
			ENDCG
	   }*/
   }
}
