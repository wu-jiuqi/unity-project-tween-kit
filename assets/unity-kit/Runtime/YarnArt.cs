using UnityEngine;
namespace ProjectTweenKit {
// The teaching view needs only the projectile illustration; no launcher or physics.
public static class YarnArt {
 static Sprite ball;
 public static Sprite Ball(int variant){if(ball==null){var t=Resources.Load<Texture2D>("Art/YarnV19/ball_yarn_apricot");ball=Sprite.Create(t,new Rect(0,0,t.width,t.height),Vector2.one*.5f,100,0,SpriteMeshType.FullRect);}return ball;}
}
}
