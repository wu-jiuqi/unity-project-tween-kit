using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTweenKit {
public sealed partial class VictoryPresentation : MonoBehaviour {
    public const float ActionsAt = 1.85f, ActionsDuration = .30f, IntroEnd = 2.34f;
    const float RewardsAt = 1.45f;
    readonly List<RectTransform> brandLetters = new List<RectTransform>();
    readonly List<Vector2> brandPositions = new List<Vector2>();
    readonly Sprite[] haloFrames = new Sprite[96], burstFrames = new Sprite[16];
    RectTransform rewards, actions;
    Image halo, burst, cat;
    CanvasGroup rewardAlpha, actionAlpha, subtitleAlpha;
    Func<RectTransform> createActions;
    CanvasGroup surfaceAlpha;
    float clock;
    bool initialized, running;
    public bool Ready { get; private set; }
    public float Elapsed => clock;
    public RectTransform CharacterRoot { get; private set; }
    public int CatFrame { get; private set; }

    public void Begin(Func<RectTransform> actionsFactory, int completedLevel = 1, bool firstReward = true, int coinReward = 20, int leagueMedals = 1) {
        createActions = actionsFactory;
        var body=UICraft.Root(transform,"Success page surface");surfaceAlpha=body.gameObject.AddComponent<CanvasGroup>();surfaceAlpha.alpha=0;
        for (int i = 0; i < haloFrames.Length; i++) haloFrames[i] = VictoryArt.Get("halo_" + i.ToString("D2"));
        for (int i = 0; i < burstFrames.Length; i++) burstFrames[i] = VictoryArt.Get("burst_" + i.ToString("D2"));
        UICraft.Box(body, "Victory dimmer", 0, 0, 1080, 2340, new Color(.22f, .17f, .15f, .36f), false, true);
        var panel = UICraft.Art(body, "pastel/modal_panel", 0, -3, 968, 2162);
        panel.type = Image.Type.Sliced; panel.preserveAspect = false;
        var lightClip = UICraft.Root(body, "Victory light clip", 0, -3, 884, 2050);
        lightClip.gameObject.AddComponent<RectMask2D>();
        halo = SpriteImage(lightClip, "Victory halo", haloFrames[0], 0, 93, 2200, 2200);
        var logo = PastelLogo.Create(body, 0, 936, 400, 156);
        foreach (Transform child in logo) brandLetters.Add((RectTransform)child);
        brandLetters.Sort((a, b) => a.anchoredPosition.x.CompareTo(b.anchoredPosition.x));
        foreach (var letter in brandLetters) brandPositions.Add(letter.anchoredPosition);
        BuildHeaderStars();

        var subtitle = UICraft.Label(body, "Level " + completedLevel, 0, 472, 930, 60, 48, UICraft.Ink);
        subtitleAlpha = subtitle.gameObject.AddComponent<CanvasGroup>();
        var hero = UICraft.Root(body, "Victory celebration", 0, 42, 820, 780);
        burst = SpriteImage(hero, "Victory burst", burstFrames[0], 0, 0, 943, 897);
        CharacterRoot = UICraft.Root(hero, "Victory character slot", 0, 0, 820, 780);
        // Fixed square canvas preserves the baked jump arc and foot registration.
        // Its full bounds sit below the subtitle and above the reward card.
        cat = SpriteImage(CharacterRoot, "Victory cat sequence", VictoryCatAnimation.Get(0), 0, 0, 720, 720);
        cat.raycastTarget = false;
        rewards = UICraft.Root(body, "Victory rewards", 0, -469, 832, 278);
        rewardAlpha = rewards.gameObject.AddComponent<CanvasGroup>();
        var rewardShadow = UICraft.Box(rewards, "Reward shadow", 0, -6, 832, 278, new Color(.65f, .50f, .35f, .12f));
        rewardShadow.pixelsPerUnitMultiplier = .375f;
        var border = UICraft.Box(rewards, "Reward border", 0, 0, 832, 278, new Color32(230, 214, 189, 255));
        border.pixelsPerUnitMultiplier = .375f;
        var backing = UICraft.Box(rewards, "Reward cream card", 0, 0, 824, 270, new Color32(247, 239, 224, 245));
        backing.pixelsPerUnitMultiplier = .375f;
        UICraft.Label(rewards, firstReward?"通关奖励":"作品已收藏", 0, 80, 750, 54, 40, UICraft.Ink);
        UICraft.Art(rewards, "pastel/icon_coin", -220, -35, 130, 130);
        UICraft.Label(rewards, firstReward?"+"+coinReward:"+0", -95, -35, 140, 90, 68, UICraft.Ink);
        UICraft.Box(rewards, "Reward divider", 15, -35, 4, 90, new Color32(226, 210, 190, 255));
        var medalIcon=UICraft.Art(rewards, "pastel/icon_medal", 130, -35, 130, 130);medalIcon.name="Victory medal icon";medalIcon.gameObject.SetActive(leagueMedals>=0);
        UICraft.Label(rewards, leagueMedals<0?"第10关解锁":("+"+(firstReward?leagueMedals:0)), leagueMedals<0?210:250, -35, leagueMedals<0?250:110, 90, leagueMedals<0?27:68, UICraft.Ink);
        gameObject.AddComponent<ResultAudioTimeline>().Initialize(true);
        initialized = running = true;
        Render();
    }
    static Image SpriteImage(Transform parent, string name, Sprite sprite, float x, float y, float w, float h) {
        var image = UICraft.Box(parent, name, x, y, w, h, Color.white);
        image.sprite = sprite; image.type = Image.Type.Simple; image.preserveAspect = false;
        return image;
    }
    void Update() {
        if (!running) return;
        clock += KitClock.Delta;
        if (!FeedbackProfile.Active.enabledFeedback) clock = Mathf.Max(clock, IntroEnd);
        Render();
    }
    void Render() {
        bool motion = FeedbackProfile.Active.enabledFeedback;
        CatFrame = motion ? VictoryCatAnimation.FrameAt(clock - .08f) : VictoryCatAnimation.FrameCount - 1;
        cat.sprite = VictoryCatAnimation.Get(CatFrame);
        surfaceAlpha.alpha=Mathf.Clamp01(clock/.28f);
        RenderHeaderStars(motion);
        for (int i = 0; i < brandLetters.Count; i++) {
            float p = ((clock % 5.5f) - .7f - i * .09f) / .48f;
            float wave = motion && p > 0 && p < 1 ? Mathf.Sin(p * Mathf.PI * 8) * Mathf.Sin(p * Mathf.PI) : 0;
            brandLetters[i].anchoredPosition = brandPositions[i] + new Vector2(wave * 1.2f, wave * 3.2f);
            brandLetters[i].localRotation = Quaternion.Euler(0, 0, wave * 2.2f);
            brandLetters[i].localScale = new Vector3(1 + wave * .018f, 1 - wave * .025f, 1);
        }
        float haloAge = motion ? Mathf.Max(0, clock - .35f) * 8 : 0;
        int haloIndex = Mathf.FloorToInt(haloAge);
        halo.sprite = haloFrames[haloIndex % haloFrames.Length];
        halo.rectTransform.localRotation = Quaternion.Euler(0, 0, -(haloAge - haloIndex) * 3.75f);
        halo.color = new Color(1, 1, 1, .78f * (motion ? Mathf.Clamp01((clock - .15f) / .45f) : 1));
        float burstAge = clock - 1;
        bool bursting = motion && burstAge >= 0 && burstAge < burstFrames.Length / 12f;
        burst.enabled = bursting;
        if (bursting) burst.sprite = burstFrames[Mathf.Min(burstFrames.Length - 1, Mathf.FloorToInt(burstAge * 12))];
        subtitleAlpha.alpha = Mathf.Clamp01((clock - .6f) / .3f);
        float reward = Mathf.Clamp01((clock - RewardsAt) / .3f);
        rewards.anchoredPosition = new Vector2(0, -469 - (1 - reward) * 28);
        rewards.localScale = Vector3.one * (.96f + .04f * UICraft.OutBack(reward));
        rewardAlpha.alpha = reward;
        if (clock >= ActionsAt && actions == null) {
            actions = createActions?.Invoke();
            if (actions != null) actionAlpha = actions.gameObject.AddComponent<CanvasGroup>();
        }
        Ready = false;
        if (actions != null) {
            float p = Mathf.Clamp01((clock - ActionsAt) / ActionsDuration);
            actions.anchoredPosition = Vector2.down * ((1 - p) * 28);
            actions.localScale = Vector3.one * (.96f + .04f * UICraft.OutBack(p));
            actionAlpha.alpha = p;
            Ready = p >= .999f;
            actionAlpha.interactable = actionAlpha.blocksRaycasts = Ready;
        }
    }
    public void Finish() { if (!initialized) return; GetComponent<ResultAudioTimeline>()?.Skip(); clock = Mathf.Max(clock, IntroEnd); Render(); }
    void OnEnable() { if (initialized) { running = true; Render(); } }
    void OnDisable() {
        running = false; Ready = false;
        if (actionAlpha != null) actionAlpha.interactable = actionAlpha.blocksRaycasts = false;
    }
}
}
