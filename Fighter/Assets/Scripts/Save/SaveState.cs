using System;
using System.Collections.Generic;

public class SaveState {

	public int TotalGold = 0;

	public int CurExp = 0;

	public int TotalDiamond = 0;

	public int TotalCardChar = 0;

	public bool isFirstPlay = true;

	public bool haveInternet;

	public int oldDay = DateTime.Now.Day;

	public int curProgressInDay = 0;

	public int isClaimedDailyReward = -1;
}
