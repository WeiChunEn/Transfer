

Shader "Chess/Rim" 
{
	//-----------------------------------【屬性 || Properties】------------------------------------------  
	Properties
	{
		//主顏色 || Main Color
		_MainColor("【主顏色】Main Color", Color) = (0.5,0.5,0.5,1)
		//漫反射紋理 || Diffuse Texture
		_TextureDiffuse("【漫反射紋理】Texture Diffuse", 2D) = "white" {}	
		//邊緣發光顏色 || Rim Color
		_RimColor("【邊緣發光顏色】Rim Color", Color) = (0.5,0.5,0.5,1)
		//邊緣發光強度 ||Rim Power
		_RimPower("【邊緣發光強度】Rim Power", Range(0.0, 36)) = 0.1
		//邊緣發光強度系數 || Rim Intensity Factor
		_RimIntensity("【邊緣發光強度系數】Rim Intensity", Range(0.0, 100)) = 3
	}

	//----------------------------------【子著色器 || SubShader】---------------------------------------  
	SubShader
	{
		//渲染類型為Opaque，不透明 || RenderType Opaque
		Tags
		{
			"RenderType" = "Opaque"
		}

		//---------------------------------------【唯一的通道 || Pass】------------------------------------
		Pass
		{
			//設定通道名稱 || Set Pass Name
			Name "ForwardBase"

			//設置光照模式 || LightMode ForwardBase
			Tags
			{
				"LightMode" = "ForwardBase"
			}

			//-------------------------開啟CG著色器編程語言段 || Begin CG Programming Part----------------------  
			CGPROGRAM

				//【1】指定頂點和片段著色函數名稱 || Set the name of vertex and fragment shader function
				#pragma vertex vert
				#pragma fragment frag

				//【2】頭文件包含 || include
				#include "UnityCG.cginc"
				#include "AutoLight.cginc"

				//【3】指定Shader Model 3.0 || Set Shader Model 3.0
				#pragma target 3.0

				//【4】變量聲明 || Variable Declaration
				//系統光照顏色
				uniform float4 _LightColor0;
				//主顏色
				uniform float4 _MainColor;
				//漫反射紋理
				uniform sampler2D _TextureDiffuse; 
				//漫反射紋理_ST後綴版
				uniform float4 _TextureDiffuse_ST;
				//邊緣光顏色
				uniform float4 _RimColor;
				//邊緣光強度
				uniform float _RimPower;
				//邊緣光強度系數
				uniform float _RimIntensity;

				//【5】頂點輸入結構體 || Vertex Input Struct
				struct VertexInput 
				{
					//頂點位置 || Vertex position
					float4 vertex : POSITION;
					//法線向量坐標 || Normal vector coordinates
					float3 normal : NORMAL;
					//一級紋理坐標 || Primary texture coordinates
					float4 texcoord : TEXCOORD0;
				};

				//【6】頂點輸出結構體 || Vertex Output Struct
				struct VertexOutput 
				{
					//像素位置 || Pixel position
					float4 pos : SV_POSITION;
					//一級紋理坐標 || Primary texture coordinates
					float4 texcoord : TEXCOORD0;
					//法線向量坐標 || Normal vector coordinates
					float3 normal : NORMAL;
					//世界空間中的坐標位置 || Coordinate position in world space
					float4 posWorld : TEXCOORD1;
					//創建光源坐標,用於內置的光照 || Function in AutoLight.cginc to create light coordinates
					LIGHTING_COORDS(3,4)
				};

				//【7】頂點著色函數 || Vertex Shader Function
				VertexOutput vert(VertexInput v) 
				{
					//【1】聲明一個頂點輸出結構對象 || Declares a vertex output structure object
					VertexOutput o;

					//【2】填充此輸出結構 || Fill the output structure
					//將輸入紋理坐標賦值給輸出紋理坐標
					o.texcoord = v.texcoord;
					//獲取頂點在世界空間中的法線向量坐標  
					o.normal = mul(float4(v.normal,0), unity_WorldToObject).xyz;
					//獲得頂點在世界空間中的位置坐標  
					o.posWorld = mul(unity_ObjectToWorld, v.vertex);
					//獲取像素位置
					o.pos = UnityObjectToClipPos(v.vertex);

					//【3】返回此輸出結構對象  || Returns the output structure
					return o;
				}

				//【8】片段著色函數 || Fragment Shader Function
				fixed4 frag(VertexOutput i) : COLOR
				{
					//【8.1】方向參數準備 || Direction
					//視角方向
					float3 ViewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
					//法線方向
					float3 Normalection = normalize(i.normal);
					//光照方向
					float3 LightDirection = normalize(_WorldSpaceLightPos0.xyz);

					//【8.2】計算光照的衰減 || Lighting attenuation
					//衰減值
					float Attenuation = LIGHT_ATTENUATION(i);
					//衰減後顏色值
					float3 AttenColor = Attenuation * _LightColor0.xyz;

					//【8.3】計算漫反射 || Diffuse
					float NdotL = dot(Normalection, LightDirection);
					float3 Diffuse = max(0.0, NdotL) * AttenColor + UNITY_LIGHTMODEL_AMBIENT.xyz;

					//【8.4】準備自發光參數 || Emissive
					//計算邊緣強度
					half Rim = 1.0 - max(0, dot(i.normal, ViewDirection));
					//計算出邊緣自發光強度
					float3 Emissive = _RimColor.rgb * pow(Rim,_RimPower) *_RimIntensity;

					//【8.5】計在最終顏色中加入自發光顏色 || Calculate the final color
					//最終顏色 = （漫反射系數 x 紋理顏色 x rgb顏色）+自發光顏色 || Final Color=(Diffuse x Texture x rgbColor)+Emissive
					float3 finalColor = Diffuse * (tex2D(_TextureDiffuse,TRANSFORM_TEX(i.texcoord.rg, _TextureDiffuse)).rgb*_MainColor.rgb) + Emissive;
				
					//【8.6】返回最終顏色 || Return final color
					return fixed4(finalColor,1);
				}

			//-------------------結束CG著色器編程語言段 || End CG Programming Part------------------  
			ENDCG
		}
	}

	//後備著色器為普通漫反射 || Fallback use Diffuse
	FallBack "Diffuse"
}