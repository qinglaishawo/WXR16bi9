Shader "UI/UI_Cam_mask"
{
	Properties
	{
		_MainTex("_MainTex", 2D) = "white" {}
	    _MaskTexBG("_MaskTexBG", 2D) = "white" {}
		_MaskTexTX("_MaskTexTX", 2D) = "white" {}
		_MaskTexTX2("_MaskTexTX2", 2D) = "white" {}
	    _TsTex("_TsTex", 2D) = "white" {}
		_DisX("_DisX", Range(0,2.0)) = 1
	}

		SubShader
	{
		//Cull Off 
		ZWrite Off
		ZTest Always

		Tags
	{
		"Queue" = "Transparent"
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

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = v.uv;
		return o;
	}

	sampler2D _MainTex;
	sampler2D _TsTex;
	sampler2D _MaskTexBG;
	sampler2D _MaskTexTX;
	sampler2D _MaskTexTX2;
	float _DisX;
	fixed4 frag(v2f i) : SV_Target
	{
		fixed4 colmain = tex2D(_MainTex, i.uv);
	    fixed4 colmasktx2 = tex2D(_MaskTexTX2, float2(1, 1) - i.uv);
		fixed4 colmaskbg = tex2D(_MaskTexBG, i.uv);
		fixed4 colmasktx = tex2D(_MaskTexTX, float2(1,1)-i.uv);
		fixed4 colts = tex2D(_TsTex, float2(1, 1) - i.uv);

		if(colmasktx.a <= 0 && colmaskbg.a <= 0)
			return  fixed4(0,0,0,0);
		else if(colmasktx.a > 0&& colmasktx.r>0)
			return fixed4(colmain.r, colmain.g, colmain.b, 1);
		else if (colmasktx.a > 0 && colmasktx.r<=0)
			return  fixed4(colts.r, colts.g, colts.b, 1);
		else
		{
			//colmain *= 0.5f;
			fixed4 colout = colmain + (colmasktx2-colmain)*_DisX;
			return  fixed4(colout.r, colout.g, colout.b,1);
		}
	}
		ENDCG
	}
	}
}
