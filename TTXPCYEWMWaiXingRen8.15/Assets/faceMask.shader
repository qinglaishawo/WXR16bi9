Shader "UI/UI_Grey_mask"
{
	Properties
	{
		_MainTex ("_MainTex", 2D) = "white" {}
	    _MaskTex("_MaskTex", 2D) = "white" {}
	    _TsTex("_TsTex", 2D) = "white" {}	
	}

	SubShader
	{
		Cull Off 
		//ZWrite Off 
		//Lighting off
		//Blend off
		//AlphaTest GEqual[_Cutoff]
		//ZTest Always

		Tags
		{
			"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"
		}
	
		Blend SrcAlpha OneMinusSrcAlpha
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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _TsTex;
			sampler2D _MaskTex;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 colmain = tex2D(_MainTex, i.uv);
			    fixed4 colmask = tex2D(_MaskTex, i.uv);
				fixed4 colts = tex2D(_TsTex, i.uv);
				if(colmask.r+ colmask.g+ colmask.b>0.99f&&colmask.a>0)
					return  fixed4(colmain.r, colmain.g, colmain.b, colmain.a);				
				else
					return  fixed4(colts.r, colts.g, colts.b, colts.a);
			}
			ENDCG
		}
	}
}
