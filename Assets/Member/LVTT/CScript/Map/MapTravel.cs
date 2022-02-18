
using UnityEngine;

namespace TT.MapTravel
{
    public class MapTravel : MonoBehaviour
    {
        public Transform Player;

        public Transform[] AreaList;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //if (Input.GetKey(KeyCode.U))
            //{
            //    TravelToArea(0);
            //}
        }

        void TravelToArea(int AreaNum)
        {
            Vector3 newPos = AreaList[AreaNum].transform.position;
            newPos.y = Player.transform.position.y;
            Player.transform.position = newPos;
        }    
    }
}

