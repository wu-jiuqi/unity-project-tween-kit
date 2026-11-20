using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ProjectTweenKit {
public sealed class FailurePresentation : MonoBehaviour {
    sealed class Letter { public Graphic image; public Vector2 position; }
    readonly List<Letter> letters=new List<Letter>();
    readonly Sprite[] frames=new Sprite[96];
    Image background,cat;
    RectTransform actions;
    CanvasGroup actionAlpha;
    float clock;
    bool firstTick=true;
    public int CatFrame { get; private set; }
    public bool Ready => clock>=1.35f;
    public void Begin(Action retry,Action home,Action watch,bool canContinue) {
        gameObject.AddComponent<ResultAudioTimeline>().Initialize(false);
        UICraft.Box(transform,"Failure dimmer",0,0,1080,2340,new Color(.24f,.19f,.23f,.4f),false,true);
        var panel=UICraft.Art(transform,"pastel/modal_panel",0,-3,968,2162);panel.type=Image.Type.Sliced;panel.preserveAspect=false;
        for(int i=0;i<96;i++)frames[i]=FailureArt.Get("background_"+i.ToString("D3"));
        background=SpriteImage(transform,"Failure looping clouds",frames[0],0,18,1000,1000);
        PastelLogo.Create(transform,0,936,400,156);
        string title="就差一点！";
        for(int i=0;i<title.Length;i++){var letter=UICraft.Label(transform,title[i].ToString(),(i-2)*136,642,150,180,118);letter.color=new Color(.58f,.40f,.69f);letters.Add(new Letter{image=letter,position=letter.rectTransform.anchoredPosition});}

        UICraft.Label(transform,"再弹一次，就有新希望",0,486,900,72,48,UICraft.Ink);
        // Full 760px frame stays between subtitle (bottom 450) and ad (top -421).
        // Register all poses and idle breathing to the approved shared paw baseline.
        cat=SpriteImage(transform,"Failure cat sequence",FailureCatAnimation.Get(0),0,42,760,760);
        cat.raycastTarget=false;
        cat.rectTransform.pivot=new Vector2(.5f,42f/512f);
        cat.rectTransform.anchoredPosition=new Vector2(0,42-(470f/512f-.5f)*760);
        actions=UICraft.Root(transform,"Failure actions");actionAlpha=actions.gameObject.AddComponent<CanvasGroup>();
        var ad=ContentButton("看广告继续","再来一次机会","pastel/icon_video",-525,836,208,UICraft.Green,watch,48);
        ad.name="Button Watch ad continue";ad.interactable=canContinue;
        if(!canContinue){var disabled=ad.gameObject.AddComponent<CanvasGroup>();disabled.alpha=.5f;}
        var again=ContentButton("再试一次",null,"pastel/icon_restart",-735,650,152,UICraft.Gold,retry,54);again.name="Button Try again";
        var back=ContentButton("返回花园",null,"pastel/icon_home",-900,360,116,UICraft.Blue,home,44);back.name="Button Home";
        Render();
    }
    Button ContentButton(string text,string subtitle,string icon,float y,float width,float height,Color color,Action action,int size){
        var button=UICraft.Button(actions,"",0,y,width,height,color,action,size);
        var visual=button.transform.Find("ButtonVisual");
        foreach(var label in visual.GetComponentsInChildren<TMP_Text>())Destroy(label.gameObject);
        var main=UICraft.Label(visual,text,0,0,width,90,size,UICraft.Ink);
        main.enableAutoSizing=false;
        float textWidth=main.GetPreferredValues(text).x;
        float iconSize=subtitle==null?52:88,gap=subtitle==null?15:24;
        float available=width-(subtitle==null?70:76)-iconSize-gap;
        if(textWidth>available){main.fontSize*=available/textWidth;textWidth=available;}
        var group=UICraft.Root(visual,"Centered content",0,6,iconSize+gap+textWidth,subtitle==null?height*.7f:144);
        main.rectTransform.SetParent(group,false);main.rectTransform.sizeDelta=new Vector2(textWidth,subtitle==null?height*.7f:74);
        main.rectTransform.anchoredPosition=new Vector2((iconSize+gap)/2,subtitle==null?0:31);
        var art=UICraft.Art(group,icon,-(textWidth+gap)/2,0,iconSize,iconSize);
        if(art==null){var replay=UICraft.Label(group,"↻",-(textWidth+gap)/2,0,iconSize,iconSize,52,UICraft.Ink);replay.enableAutoSizing=false;}
        if(subtitle!=null){var detail=UICraft.Label(group,subtitle,(iconSize+gap)/2,-35,textWidth,50,34,new Color32(68,126,99,255));detail.enableAutoSizing=false;}
        return button;
    }
    static Image SpriteImage(Transform parent,string name,Sprite sprite,float x,float y,float w,float h){var image=UICraft.Box(parent,name,x,y,w,h,Color.white);image.sprite=sprite;image.type=Image.Type.Simple;return image;}
    void Update(){if(firstTick)firstTick=false;else clock+=Mathf.Min(KitClock.Delta,.1f);if(!FeedbackProfile.Active.enabledFeedback)clock=Mathf.Max(clock,1.35f);Render();}
    void Render(){
        bool motion=FeedbackProfile.Active.enabledFeedback;
        for(int i=0;i<letters.Count;i++){
            var l=letters[i];float p=motion?Mathf.Clamp01((clock-.12f-i*.055f)/.38f):1,s=UICraft.OutBack(p);
            l.image.rectTransform.anchoredPosition=l.position+Vector2.down*((1-p)*44);
            l.image.rectTransform.localScale=new Vector3(s,Mathf.Max(0,s+Mathf.Sin(p*Mathf.PI*2)*.09f),1);
            l.image.rectTransform.localRotation=Quaternion.Euler(0,0,-(1-p)*(i%2==1?5:-5));l.image.color=new Color(.58f,.40f,.69f,p>0?1:0);
        }
        background.sprite=frames[motion?Mathf.FloorToInt(clock*24)%96:0];
        CatFrame=motion?FailureCatAnimation.FrameAt(clock):FailureCatAnimation.FrameCount-1;
        cat.sprite=FailureCatAnimation.Get(CatFrame);
        float breathing=motion&&clock>FailureCatAnimation.Duration?.003f*(1-Mathf.Cos((clock-FailureCatAnimation.Duration)*Mathf.PI)):0;
        cat.rectTransform.localScale=new Vector3(1,1+breathing,1);
        float a=Mathf.Clamp01((clock-1.05f)/.3f);actions.anchoredPosition=Vector2.down*(1-UICraft.OutBack(a))*30;
        actionAlpha.alpha=a;actionAlpha.interactable=actionAlpha.blocksRaycasts=Ready;
    }
}
}
