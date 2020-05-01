// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chess/Stone Dissolve"
{
	Properties{
		_Diffuse("Diffuse", Color) = (1,1,1,1)
		_DissolveColor("Dissolve Color", Color) = (0,0,0,0)
		_DissolveEdgeColor("Dissolve Edge Color", Color) = (1,1,1,1)
		_MainTex("Base 2D", 2D) = "white"{}
		_BumpMap("Normal Map", 2D) = "bump" {}
		_BumpScale("Bump Scale", Float) = 1.0

		_DissolveMap("DissolveMap", 2D) = "white"{}
		_DissolveThreshold("DissolveThreshold", Range(0,1)) = 0
		_ColorFactor("ColorFactor", Range(0,1)) = 0.7
		_DissolveEdge("DissolveEdge", Range(0,1)) = 0.8
		_FlyThreshold("FlyThreshold",Range(0,1)) = 0
		_FlyFactor("FlyFactor",float) = 0
	}
	
	CGINCLUDE
	#include "Lighting.cginc"
	 #include "UnityCG.cginc"
	uniform fixed4 _Diffuse;
	uniform fixed4 _DissolveColor;
	uniform fixed4 _DissolveEdgeColor;
	uniform sampler2D _MainTex;
	uniform float4 _MainTex_ST;
	uniform sampler2D _DissolveMap;
	uniform float _DissolveThreshold;
	uniform float _ColorFactor;
	uniform float _DissolveEdge;
	uniform float _FlyThreshold;
	uniform float _FlyFactor;
	sampler2D _BumpMap;
	float4 _BumpMap_ST;

	float _BumpScale;
	
	struct a2v
    {
        float4 vertex : POSITION;    // 告诉Unity把模型空间下的顶点坐标填充给vertex属性
        float3 normal : NORMAL;        // 不再使用模型自带的法线。保留该变量是因为切线空间是通过（模型里的）法线和（模型里的）切线确定的。
        float4 tangent : TANGENT;    // tangent.w用来确定切线空间中坐标轴的方向的。
        float4 texcoord : TEXCOORD0; 
    };
	struct v2f
	{
		float4 pos : SV_POSITION;
		float3 worldNormal : TEXCOORD0;
		float4 uv : TEXCOORD1;
		float3 lightDir : TEXCOORD2;   // 切线空间下，平行光的方向

	};
	
	v2f vert(a2v v)
	{
		v2f o;
		v.vertex.xyz += v.normal * saturate(_DissolveThreshold - _FlyThreshold) * _FlyFactor;
		o.pos = UnityObjectToClipPos(v.vertex);
		
		o.worldNormal = mul(v.vertex,unity_WorldToObject).xyz;
		
        o.uv.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw; // 贴图的纹理坐标
        o.uv.zw = v.texcoord.xy * _BumpMap_ST.xy + _BumpMap_ST.zw; // 法线贴图的纹理坐标
		TANGENT_SPACE_ROTATION; // 调用这个宏会得到一个矩阵rotation，该矩阵用来把模型空间下的方向转换为切线空间下
        o.lightDir = mul(rotation, ObjSpaceLightDir(v.vertex)); // 切线空间下，平行光的方向

		return o;
	}
	
	fixed4 frag(v2f i) : SV_Target
	{
		//采样Dissolve Map
		fixed4 dissolveValue = tex2D(_DissolveMap, i.uv);
		//小于阈值的部分直接discard
		if (dissolveValue.r < _DissolveThreshold)
		{
			discard;
		}
		fixed3 tangentNormal = UnpackNormal(tex2D(_BumpMap, i.uv));
				tangentNormal.xy *= _BumpScale;
				tangentNormal.z = sqrt(1.0 - saturate(dot(tangentNormal.xy, tangentNormal.xy)));
		//Diffuse + Ambient光照计算
		fixed3 worldNormal = normalize(i.worldNormal);
		fixed3 worldLightDir = normalize(i.lightDir.xyz);
		fixed3 lambert = saturate(dot(tangentNormal, worldLightDir));
		fixed3 albedo = lambert * _Diffuse.xyz * _LightColor0.xyz + UNITY_LIGHTMODEL_AMBIENT.xyz;
		fixed3 color = tex2D(_MainTex, i.uv).rgb * albedo;
 
		//优化版本，尽量不在shader中用分支判断的版本,但是代码很难理解啊....
		float percentage = _DissolveThreshold / dissolveValue.r;
		//如果当前百分比 - 颜色权重 - 边缘颜色
		float lerpEdge = sign(percentage - _ColorFactor - _DissolveEdge);
		//貌似sign返回的值还得saturate一下，否则是一个很奇怪的值
		fixed3 edgeColor = lerp(_DissolveEdgeColor.rgb, _DissolveColor.rgb, saturate(lerpEdge));
		//最终输出颜色的lerp值
		float lerpOut = sign(percentage - _ColorFactor);
		//最终颜色在原颜色和上一步计算的颜色之间差值（其实经过saturate（sign（..））的lerpOut应该只能是0或1）
		fixed3 colorOut = lerp(color, edgeColor, saturate(lerpOut));
		return fixed4(colorOut, 1);
	}
	ENDCG
	
	SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag	
			ENDCG
		}
	}
	FallBack "Diffuse"
}
