using System;
using UnityEngine;

namespace ProjectTweenKit {
// Approved v26 HTML: identical frames, registration and per-frame timing.
public static class FailureCatAnimation {
    public const int FrameCount = 16;
    public const float Duration = 2.32f;
    static readonly float[] durations = { .28f, .14f, .14f, .18f, .14f, .14f, .12f, .12f, .10f, .10f, .10f, .10f, .12f, .14f, .16f, .24f };
    static readonly Sprite[] frames = new Sprite[FrameCount];
    public static int FrameAt(float age) {
        for (int i = 0; i < FrameCount - 1; i++) {
            if (age < durations[i]) return i;
            age -= durations[i];
        }
        return FrameCount - 1;
    }
    public static Sprite Get(int index) {
        index = Mathf.Clamp(index, 0, FrameCount - 1);
        if (frames[index] != null) return frames[index];
        var atlas = Resources.Load<Texture2D>("Art/FailureCatV26/cat-atlas");
        if (atlas == null) throw new InvalidOperationException("Failure cat v26 atlas is missing.");
        for (int i = 0; i < FrameCount; i++) {
            float w=atlas.width/4f,h=atlas.height/4f;
            frames[i] = Sprite.Create(atlas, new Rect(i % 4*w, (3-i/4)*h,w,h), new Vector2(.5f, .5f), 100, 0, SpriteMeshType.FullRect);
            frames[i].name = "failure_cat_" + i.ToString("D2");
        }
        return frames[index];
    }
}
}
