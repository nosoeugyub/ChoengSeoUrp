using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//상태 eum
public enum Bug_BugStats { RestAndStart = 0, Sit, fly  }



    public class Bug_fly : BaseGameBug
    {
		private int distance;          // 거리
	
		private Locations currentLocation;  // 현재 위치

		// Student가 가지고 있는 모든 상태, 현재 상태
		private Bugstats[] states;
		private Bugstats currentState;

		public int Distance
		{
			set => distance = Mathf.Max(0, value);
			get => distance;
		}
	
	

		public override void Setup(string name)
		{
			// 기반 클래스의 Setup 메소드 호출 (ID, 이름, 색상 설정)
			base.Setup(name);

			// 생성되는 오브젝트 이름 설정
			gameObject.name = $"{BugID:D2} {name}";

			// Student가 가질 수 있는 상태 개수만큼 메모리 할당, 각 상태에 클래스 메모리 할당
			states = new Bugstats[2];
			states[(int)Bug_BugStats.RestAndStart] = new NSY.Bug.RestAndStart();
			states[(int)Bug_BugStats.Sit] = new NSY.Bug.Sit();
		

			// 현재 상태를 집에서 쉬는 "RestAndSleep" 상태로 설정
			ChangeState(Bug_BugStats.RestAndStart);
			Debug.Log(" aaa");
			distance = 0;
		}

		public override void Updated()
		{
			if (currentState != null)
			{
				Debug.Log("  간다.");
				currentState.Execute(this);
			}
		}

		public void ChangeState(Bug_BugStats newState)
		{
			// 새로 바꾸려는 상태가 비어있으면 상태를 바꾸지 않는다
			if (states[(int)newState] == null) return;

			// 현재 재생중인 상태가 있으면 Exit() 메소드 호출
			if (currentState != null)
			{
				currentState.Exit(this);
			}

			// 새로운 상태로 변경하고, 새로 바뀐 상태의 Enter() 메소드 호출
			currentState = states[(int)newState];
			currentState.Enter(this);
		}
	}



