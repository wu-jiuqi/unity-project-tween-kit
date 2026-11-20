using System;
using UnityEngine;
namespace ProjectTweenKit {
// LeagueRankingView.Refresh: final ranks are host inputs, not recalculated here.
public sealed class RankReorder:MonoBehaviour {
 public RectTransform[] Rows;public float Duration=.48f,Stride=150;
 Vector2[] rest,from,to;float age,top;bool running;
 public void Reorder(int[] ranks){if(ranks==null||ranks.Length!=Rows.Length)throw new ArgumentException("One rank per row is required");var seen=new bool[Rows.Length];foreach(int rank in ranks){if(rank<0||rank>=Rows.Length||seen[rank])throw new ArgumentException("Ranks must form a permutation");seen[rank]=true;}
  if(rest==null){rest=new Vector2[Rows.Length];top=float.NegativeInfinity;for(int i=0;i<Rows.Length;i++){rest[i]=Rows[i].anchoredPosition;top=Mathf.Max(top,rest[i].y);}}
  from=new Vector2[Rows.Length];to=new Vector2[Rows.Length];for(int i=0;i<Rows.Length;i++){from[i]=Rows[i].anchoredPosition;to[i]=new Vector2(rest[i].x,top-ranks[i]*Stride);}age=0;running=true;
 }
 void Update(){if(!running)return;age+=KitClock.Delta;float t=Mathf.Clamp01(age/Mathf.Max(.001f,Duration));for(int i=0;i<Rows.Length;i++)Rows[i].anchoredPosition=Vector2.LerpUnclamped(from[i],to[i],1-Mathf.Pow(1-t,3));if(t>=1)running=false;}
 public void StopAndRestore(){running=false;if(rest==null)return;for(int i=0;i<Rows.Length;i++)if(Rows[i]!=null)Rows[i].anchoredPosition=rest[i];}
 void OnDisable(){StopAndRestore();}
}
}
