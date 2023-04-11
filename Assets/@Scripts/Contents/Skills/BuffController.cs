using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : SkillController
{
	public override bool Init()
	{
		base.Init();

		return true;
	}

	// 데이터시트를 이용해서 조절할 수 있게 만들 것인가?
	// 상속 구조로 하드코딩 할 것인가?
	// 누가 들고 있을 것인가?
	// 종특 (패시브, 영구적) 분리할 것인가?
	// 소환수 + 버프 조합으로 상상 이상의 모든 것을 구현할 수 있음.
}
