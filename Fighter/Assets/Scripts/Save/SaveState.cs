using System;
using System.Collections.Generic;

public class SaveState {

	public bool isFirstOpenApp = true;

	public int TotalGold = 0;

	public int CurExp = 0;

	public int TotalDiamond = 0;

	public int TotalCardChar = 0;

	public bool isFirstPlay = true;

	public bool haveInternet;

	public int oldDay = DateTime.Now.Day;

	public int curProgressInDay = 0;

	public int isClaimedDailyReward = -1;

	public bool isOnSound = true;

	public bool isOnMusic = true;

	public bool isOnVoice = true;

	public bool isOnRing = true;

	public bool isRewardBonus = false;

	public int winCountLeft = 0;

	public int winCountRight = 0;
}
