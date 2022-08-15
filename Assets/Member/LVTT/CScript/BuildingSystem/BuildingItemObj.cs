﻿using NSY.Manager;
using UnityEngine;

namespace DM.Building
{

    public class BuildingItemObj : ItemObject//, IDropable
    {
        [SerializeField] BuildObjAttribute attributes;

        [Tooltip("이 오브젝트를 채집할 수 있는 도구 타입")]
        public InItemType toolType;

        float MaxScale = 3f;
        float MinScale = 0.1f;
        public int breakCount;

        [SerializeField] private bool itemisSet;
        [SerializeField] private bool isFirstDrop;
        [SerializeField] private bool isBroken;

        [SerializeField] private ParticleSystem particle;

        float MaxX;
        float MinX;
        float MaxY;
        float MinY;

        Vector3 ObjOriginPos;

        [SerializeField] BuildingBlock parentBuildArea;
        BuildingHandyObjSpawn SpawnHandyObjParent;

        RaycastHit hit;
        Ray ray;
        int layerMask;
        [SerializeField] int myOrder;
        public int MyOrder { get { return myOrder; } set { myOrder = value; } }

        public BuildingBlock ParentBuildArea { get { return parentBuildArea; } }
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
            itemisSet = false;
            isFirstDrop = true;
        }
        private void Start()
        {
            MaxScale = 2f;
            MinScale = 0.1f;
            layerMask = 1 << LayerMask.NameToLayer("Ground");
            if (isBroken)
            {
                SetParentBuildArea(parentBuildArea, parentBuildArea.HouseBuild.position);
                itemisSet = true;
                isFirstDrop = false;
            }
        }
        private void Update()
        {
            if (!itemisSet && parentBuildArea == BuildingBlock.nowBuildingBlock)
            {
                ItemMove();
            }
            if (IsFirstDrop && parentBuildArea == BuildingBlock.nowBuildingBlock)
            {
                // print("isFirstDrop");

                if (Input.GetMouseButtonDown(1))
                {
                    BackToInventory();
                }
            }
        }
        /// <summary>
        /////////////////////////Update
        private void ItemMove()
        { //onBuildItem interact during BuildMode
            if (BuildingBlock.isBuildMode)
            {
                var movePos = Input.mousePosition;
                movePos.z = parentBuildArea.DistanceToNowBuildItemToNewSort(Camera.main.transform.position);
                print(movePos.z);
                movePos = Camera.main.ScreenToWorldPoint(movePos);

                HouseBuildAreaCal();
                print(MaxY + " " + MinY);

                if (movePos.y >= MaxY) movePos.y = MaxY;
                if (movePos.y <= MinY) movePos.y = MinY;

                if (parentBuildArea.DistanceToNowBuildItem(movePos) > MaxX)
                {
                    //print(" 여어 멈추라고");
                    movePos.x = transform.position.x;
                    movePos.z = transform.position.z;
                }


                //if (movePos.x >= MaxX) movePos.x = MaxX;
                //if (movePos.x <= MinX) movePos.x = MinX;


                //print(parentBuildArea.DistanceFromHouseBuildTo(movePos));
                transform.position = movePos;
            }

            //onBuildItem interact when not in BuildMode
            else
            {




                ////SpawnHandyObjParent.curInteractHandyObj.gameObject.GetComponent<Billboard>().enabled = true;
                //var movePos = Input.mousePosition;
                //movePos.z = SpawnHandyObjParent.DistanceFromCharacterTo(Camera.main.transform.position);
                //movePos = Camera.main.ScreenToWorldPoint(movePos);
                //transform.position = movePos;
            }

        }
        void HouseBuildAreaCal()
        {
            //원래 위치 + 건축영역가로 반 길이 - 스케일 길이 
            MaxX = /*ObjOriginPos.x + */ parentBuildArea.AreaWidthsize / 2 - (quad.transform.localScale.x * transform.localScale.x) / 2;
            MinX = ObjOriginPos.x - parentBuildArea.AreaWidthsize / 2 + (quad.transform.localScale.x * transform.localScale.x) / 2;
            MaxY = ObjOriginPos.y + parentBuildArea.AreaHeightsize / 2 - quad.transform.localScale.y * transform.localScale.y / 2;
            MinY = ObjOriginPos.y - parentBuildArea.AreaHeightsize / 2 + quad.transform.localScale.y * transform.localScale.y / 2;
        }
        /// 
        /// </summary>
        /// 
        public void BackToInventory()
        {
            parentBuildArea.CancleUI(false);
            SuperManager.Instance.inventoryManager.AddItem(item);

            parentBuildArea.InvenSlotResetCanBuildMode();

            parentBuildArea.RemoveBuildItemToList(gameObject);
            parentBuildArea.DeleteBuildingItemObjSorting(gameObject);

            Destroy(gameObject);
        }
        public void SetParentBuildArea(BuildingBlock pb, Vector3 housebuildpos)
        {
            parentBuildArea = pb;
            parentBuildArea.SetCurInteractObj(this);
            ObjOriginPos = gameObject.transform.position;

            ObjOriginPos.y = housebuildpos.y + (parentBuildArea.AreaHeightsize / 2);
            ObjOriginPos.x = housebuildpos.x;
            //ObjOriginPos.z = housebuildpos.z;
        }
        public void SetBuildItemScale(Vector3 scalenum)
        {
            if (scalenum.x >= MaxScale) scalenum.x = MaxScale;
            if (scalenum.x <= MinScale) scalenum.x = MinScale;
            if (scalenum.y >= MaxScale) scalenum.y = MaxScale;
            if (scalenum.y <= MinScale) scalenum.y = MinScale;
            transform.localScale = scalenum;
        }
        public void SetBuildItemRotation(float scalenum)
        {
            transform.Rotate(new Vector3(0, 0, scalenum));
            print(transform.rotation);
        }
        public override int CanInteract()
        {
            return (int)CursorType.Build;
        }
        public void Demolish()
        {
            breakCount--;
            //이펙트 등
            particle.Play();

            if (breakCount == 0)
            {
                //if (item.InItemType == InItemType.BuildWall)
                //    parentBuildArea.hasWall = false;
                //else if (item.InItemType == InItemType.BuildSign)
                //    parentBuildArea.hasSign = false;
                //파괴 임시 처리
                DropItems();
                parentBuildArea.RemoveBuildItemToList(gameObject);
                parentBuildArea.DeleteBuildingItemObjSorting(gameObject);
                PlayerData.AddValue(0, (int)BuildInputBehaviorEnum.Demolish, PlayerData.BuildInputData, (int)BuildInputBehaviorEnum.length);
                Destroy(gameObject);
            }
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

            //print(attributes.buildHPos.ToString());
            //print(attributes.buildVPos.ToString());

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
    }

}
