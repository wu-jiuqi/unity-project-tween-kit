Shader "Garden/StarChargeUI" {
 Properties {[PerRendererData]_MainTex("Star",2D)="white"{} _Charge("Charge",Range(0,1))=0 _Clock("Particle clock",Float)=0}
 SubShader {Tags{"Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True"} Cull Off Lighting Off ZWrite Off ZTest [unity_GUIZTestMode] Blend SrcAlpha OneMinusSrcAlpha
 Pass {CGPROGRAM
 #pragma vertex vert
 #pragma fragment frag
 #include "UnityCG.cginc"
 struct appdata{float4 vertex:POSITION;float2 uv:TEXCOORD0;fixed4 color:COLOR;};struct v2f{float4 vertex:SV_POSITION;float2 uv:TEXCOORD0;fixed4 color:COLOR;};
 sampler2D _MainTex;float _Charge,_Clock;
 v2f vert(appdata v){v2f o;o.vertex=UnityObjectToClipPos(v.vertex);o.uv=v.uv;o.color=v.color;return o;}
 fixed4 frag(v2f i):SV_Target{fixed4 s=tex2D(_MainTex,i.uv);float h=lerp(.034,.966,_Charge);float cut=1-smoothstep(h-.004,h+.004,i.uv.y);float band=1-smoothstep(.003,.026,abs(i.uv.y-h+.004));float particles=0;
  for(int k=0;k<7;k++){float x=frac(_Clock*(.16+k*.009)+k*.143);float y=h-.005+sin(_Clock*5+k*1.9)*.009;float2 delta=i.uv-float2(x,y);particles+=exp(-dot(delta*float2(65,95),delta*float2(65,95)))*(.4+.6*abs(sin(_Clock*7+k)));}
  float charging=step(.005,_Charge)*(1-step(.995,_Charge));float glow=saturate((band*.58+particles)*charging);float3 rgb=lerp(s.rgb,float3(1,1,.88),glow);return fixed4(rgb*i.color.rgb,s.a*cut*step(.001,_Charge)*i.color.a);
 }
 ENDCG}
 }
}
