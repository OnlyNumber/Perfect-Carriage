using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSkinContainer : MonoBehaviour
{
    public List<Sprite> MainBackgrounds;

    public List<Sprite> TrainBackgrounds;

    public List<AudioClip> BackgroundMusics;

    public int GetCount(SkinType type)
    {
        switch (type)
        {
            case SkinType.music:
                {
                    return BackgroundMusics.Count;
                    break;
                }
            case SkinType.mainBackground:
                {
                    return MainBackgrounds.Count;
                    break;
                }
            case SkinType.trainBackground:
                {
                    return TrainBackgrounds.Count;
                    break;
                }

        }

        return 0;
    }

    

}
