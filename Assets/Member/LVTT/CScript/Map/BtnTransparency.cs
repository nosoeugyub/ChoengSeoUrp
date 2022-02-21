using UnityEngine;
using UnityEngine.UI;

namespace TT.MapTravel
{
    public class BtnTransparency : MonoBehaviour
    {
      
        void Start()
        {
            GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        }
        void Update()
        {

        }
    }
}

