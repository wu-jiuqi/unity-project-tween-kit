using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
namespace ProjectTweenKit {
// v37 ships DOTween core without the optional UGUI shortcut module.
public static class AlphabetTweenExtensions {
 public static Tweener DOAnchorPos(this RectTransform r,Vector2 end,float duration)=>DOTween.To(()=>r.anchoredPosition,v=>r.anchoredPosition=v,end,duration).SetTarget(r);
 public static Tweener DOAnchorPosY(this RectTransform r,float end,float duration)=>DOTween.To(()=>r.anchoredPosition.y,v=>r.anchoredPosition=new Vector2(r.anchoredPosition.x,v),end,duration).SetTarget(r);
 public static Tweener DOFade(this Graphic graphic,float end,float duration)=>DOTween.To(()=>graphic.color.a,v=>{var c=graphic.color;c.a=v;graphic.color=c;},end,duration).SetTarget(graphic);
 public static Tweener DOFade(this CanvasGroup group,float end,float duration)=>DOTween.To(()=>group.alpha,v=>group.alpha=v,end,duration).SetTarget(group);
}
}
