using System.Collections.Generic;
using UnityEngine;

namespace DM.Building
{
    public class BuildingItemObj : ItemObject
    {
        [SerializeField] BuildObjAttribute attributes;

        [Tooltip("이 오브젝트를 채집할 수 있는 도구 타입")]
        public InItemType toolType;

        float MaxScale = 3f;
        float MinScale = 0.2f;
        public int breakCount;
        int originbreakCount;

        [SerializeField] private bool itemisSet;
        [SerializeField] private bool isFirstDrop;
        [SerializeField] private bool isBroken;

        [SerializeField] private ParticleSystem particle;

        float MaxX;
        float MinX;
        float MaxY;
        float MinY;
       [SerializeField] float _areaWidthsize;
        [SerializeField] float _areaHeightsize;

        [SerializeField]  Vector3 _houseBuildPos;
        Vector3 ObjOriginPos;

        BuildingHandyObjSpawn SpawnHandyObjParent;

        [SerializeField] int myOrder;
        public int MyOrder { get { return myOrder; } set { myOrder = value; } }

        public bool IsFirstDrop
        {
            get
            {
                return isFirstDrop;
            }
            set
            {
                isFirstDrop = value;
            }
        }
        public bool ItemisSet
        {
            get
            {
                return itemisSet;
            }
            set
            {
                itemisSet = value;
                if (itemisSet)
                {
                    if (IsFirstDrop)
                    {
                        PlayerData.AddValue((int)GetItem().InItemType, (int)ItemBehaviorEnum.builditem, PlayerData.ItemData, (int)ItemBehaviorEnum.length);
                        IsFirstDrop = false;
                    }
                }
            }
        }
        public BuildObjAttribute GetAttribute()
        {
            return attributes;
        }
        private new void Awake()
        {
            SpawnHandyObjParent = FindObjectOfType<BuildingHandyObjSpawn>();

            base.Awake();
            if (isBroken)
            {
                ItemisSet = true;
                isFirstDrop = false;
            }
            else
            {
                ItemisSet = false;
                isFirstDrop = true;
            }

            originbreakCount = breakCount;
        }

        internal void InitDestroyCount()
        {
            breakCount = originbreakCount;
        }

        private void Start()
        {
            MaxScale = 2f;
            MinScale = 0.1f;
            if (transform.parent)
                SetPivotPos(transform.parent.position);
 

        }
        public void CallUpdate(float _distanceToNowBuildItemToNewSort)
        {
            ItemMove(_distanceToNowBuildItemToNewSort);
        }
        public void SetAreaSize(float areaWidthsize, float areaHeightsize)
        {
            _areaWidthsize = areaWidthsize;
            _areaHeightsize = areaHeightsize;
        }
        private void ItemMove(float _distanceToNowBuildItemToNewSort)
        {
            Vector3 movePos = Input.mousePosition;
            movePos.z = _distanceToNowBuildItemToNewSort;
            movePos = Camera.main.ScreenToWorldPoint(movePos);
            HouseBuildAreaCal();

            if (movePos.y >= MaxY) movePos.y = MaxY;
            if (movePos.y <= MinY) movePos.y = MinY;

            if (DistanceToNowBuildItem(movePos) > MaxX)
            {
                movePos.x = transform.position.x;
                movePos.z = transform.position.z;
            }
            transform.position = movePos;
        }
        float DistanceToNowBuildItem(Vector3 movePos)
        {
            Vector3 VecY = new Vector3(_houseBuildPos.x, 0, _houseBuildPos.z);
            Vector3 moveposY = new Vector3(movePos.x, 0, movePos.z);
            float dist = Vector3.Distance(moveposY, VecY);
            return dist;
        }
        void HouseBuildAreaCal()
        {
            MaxX = _areaWidthsize / 2 - (quad.transform.localScale.x * transform.localScale.x) / 2;
            MaxY = ObjOriginPos.y + _areaHeightsize / 2 - quad.transform.localScale.y * transform.localScale.y / 2;
            MinY = ObjOriginPos.y - _areaHeightsize / 2 + quad.transform.localScale.y * transform.localScale.y / 2;
        }

        public void SetPivotPos(Vector3 housebuildpos)
        {
            _houseBuildPos = housebuildpos;
            ObjOriginPos = gameObject.transform.position;

            ObjOriginPos.y = housebuildpos.y + (_areaHeightsize / 2);
            ObjOriginPos.x = housebuildpos.x;
        }
        private void SetBuildingItemScaleMinMax(Vector3 scalenum)
        {
            if (scalenum.x >= MaxScale) scalenum.x = MaxScale;
            if (scalenum.x <= MinScale) scalenum.x = MinScale;
            if (scalenum.y >= MaxScale) scalenum.y = MaxScale;
            if (scalenum.y <= MinScale) scalenum.y = MinScale;
            transform.localScale = scalenum;
        }
        public void SetBuildingItemScale(float BuildItemScaleVar)
        {
            Vector3 var = transform.localScale;
            var.x += BuildItemScaleVar;
            var.y += BuildItemScaleVar;
            SetBuildingItemScaleMinMax(var);
        }

        public void SetBuildItemRotation(float scalenum)
        {
            transform.Rotate(new Vector3(0, 0, scalenum));
        }
        public override int CanInteract()
        {
            return (int)CursorType.Build;
        }

        public bool Demolish()
        {
            breakCount--;
            //이펙트 등
            particle.Play();

            if (breakCount == 0)
            {
                DropItems();
                PlayerData.AddValue(0, (int)BuildInputBehaviorEnum.Demolish, PlayerData.BuildInputData, (int)BuildInputBehaviorEnum.length);
                return true;
            }
            return false;

        }
        public void DropItems()
        {
            GameObject instantiateItem;

            foreach (DropItem item in item.DropItems)
            {
                float randnum = Random.Range(0, 100);//50( 10 30 60)
                float sumtemp = 0;

                if (item.percent >= randnum)
                    continue;

                randnum = Random.Range(0, 100);

                int i = 0;
                for (i = 0; i < item.itemObjs.Length; i++)
                {
                    sumtemp += item.itemObjs[i].percent;
                    if (sumtemp >= randnum)
                        break;
                }
                //i = 결정된 오브젝트

                for (int j = 0; j < item.itemObjs[i].count; ++j)
                {
                    Vector3 randVec = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f));
                    instantiateItem = ObjectPooler.SpawnFromPool(item.itemObjs[i].itemObj.name, gameObject.transform.position + randVec);
                    instantiateItem.GetComponent<DropCollectObject>().PlayStartSound();
                }
            }
        }
        public void SwitchBuildingItemObjZPos(bool isUp, List<GameObject> buildItemList, float buildItemGap)
        {
            GameObject nearObj = null;
            float curObjZ = transform.localPosition.z;//선택한 오브젝트의 z값
            float bujildItemZ = 10000;
            //float minDIst = 10000;
            if (isUp)
            {
                foreach (GameObject item in buildItemList)
                {
                    bujildItemZ = item.transform.localPosition.z;
                    if (!nearObj)
                    {
                        if (curObjZ > bujildItemZ)
                        {
                            nearObj = item;
                        }
                    }
                    else
                    {
                        if (curObjZ > bujildItemZ && nearObj.transform.localPosition.z < bujildItemZ)//해당 자재가 나보다 더 가깝고, 현재 가까운 오브젝트보다 
                        {
                            nearObj = item;
                        }
                    }
                }
                if (nearObj)
                {
                    //print("Up Near Obj is " + nearObj.name);
                    nearObj.transform.position += nearObj.transform.forward * buildItemGap; //가장 가까운 자재후진
                    nearObj.GetComponent<BuildingItemObj>().MyOrder--;
                    transform.position -= transform.forward * buildItemGap; //선택중인 자재 전진
                    MyOrder++;
                }
            }
            else
            {
                foreach (GameObject item in buildItemList)
                {
                    bujildItemZ = item.transform.localPosition.z;
                    if (!nearObj)
                    {
                        if (curObjZ < bujildItemZ)
                        {
                            nearObj = item;
                        }
                    }
                    else
                    {
                        if (curObjZ < bujildItemZ && nearObj.transform.localPosition.z > bujildItemZ)//해당 자재가 나보다 더 가깝고, 현재 가까운 오브젝트보다 
                        {
                            nearObj = item;
                        }
                    }
                }
                if (nearObj)
                {
                    nearObj.transform.position -= nearObj.transform.forward * buildItemGap; //가장 가까운 자재전진
                    nearObj.GetComponent<BuildingItemObj>().MyOrder++;
                    transform.position += transform.forward * buildItemGap; //선택중인 자재 후진
                    MyOrder--;
                }
            }
        }
        public void PutDownBuildingItemObj(float areaWidthSize, float areaHeightSize)
        {
            //위치 저장
            //좌중우

            if (transform.localPosition.x >= (areaWidthSize / (int)BuildHPos.Length) * 0 - (areaWidthSize / 2)
                && transform.localPosition.x <= areaWidthSize / (int)BuildHPos.Length * 1 - (areaWidthSize / 2))
                attributes.buildHPos = BuildHPos.Left;
            else if (transform.localPosition.x > (areaWidthSize / (int)BuildHPos.Length) * 1 - (areaWidthSize / 2)
                && transform.localPosition.x < areaWidthSize / (int)BuildHPos.Length * 2 - (areaWidthSize / 2))
                attributes.buildHPos = BuildHPos.Mid;
            else
                attributes.buildHPos = BuildHPos.Right;

            //하중상
            if (transform.localPosition.y >= (areaHeightSize / (int)BuildHPos.Length) * 0  //생략 가능할듯
                && transform.localPosition.y <= areaHeightSize / (int)BuildHPos.Length * 1)
                attributes.buildVPos = BuildVPos.Bottom;
            else if (transform.localPosition.y > (areaHeightSize / (int)BuildHPos.Length) * 1
                && transform.localPosition.y < areaHeightSize / (int)BuildHPos.Length * 2)
                attributes.buildVPos = BuildVPos.Mid;
            else
                attributes.buildVPos = BuildVPos.Top;

            //크기 저장
            float scaleRange = MaxScale - MinScale; // 1.5 0.3     1.2

            if (transform.localScale.x >= MinScale  //생략 가능할듯 //0.3
                && transform.localScale.x <= scaleRange / (int)BuildSize.Length * 1 + MinScale) //0.4 + 0.3  0.7
                attributes.buildSize = BuildSize.Small;
            else if (transform.localScale.x > (scaleRange / (int)BuildSize.Length) * 1 + MinScale //0.7
                && transform.localScale.x < scaleRange / (int)BuildSize.Length * 2 + MinScale)    //1.1
                attributes.buildSize = BuildSize.Normal;
            else
                attributes.buildSize = BuildSize.Big;
        }

        internal void SetOrder(int count)
        {
            myOrder = count;
        }
    }

}
