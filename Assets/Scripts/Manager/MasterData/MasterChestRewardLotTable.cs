using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterChestRewardLotTable : SimpleSingleton<MasterChestRewardLotTable>
{
	public class Data {
		public int Id { get; private set; }
		public List<int> RewardPoints { get; private set; }
		public List<int> RewardRatios { get; private set; }

        public Data(
			int id,
			List<int> rewardPoints,
			List<int> rewardRatios
		)
		{
			Id = id;
			RewardPoints = rewardPoints;
			RewardRatios = rewardRatios;
		}
	};

	private readonly string FilePath = "csv/chestrewardlottable";

	private Dictionary<int, Data> DataDict = new Dictionary<int, Data>();

	public void Initialize()
	{
		if (DataDict.Count > 0) {
			LogManager.Instance.Log("MasterChestRewardLotTable:Initialize return.");
			return;
		}
		TextAsset asset = Resources.Load<TextAsset>(FilePath);

		string text = asset.text;
		char[] split = {'\n'};
		List<string> lineList = Functions.SplitString(text, split);

		char[] split2 = { ',' };
		char[] split3 = { '-' };
		// 1行目はメタデータなので、読み飛ばす
		for (int i = 1; i < lineList.Count; i++) {
			List<string> paramList = Functions.SplitString(lineList[i], split2);

			List<string> rewardPointsString = Functions.SplitString(paramList[1], split3);
			List<int> rewardPoints = new List<int>();
			for (int i2 = 0; i2 < rewardPointsString.Count; i2++) {
				rewardPoints.Add(int.Parse(rewardPointsString[i2]));
			}
			
			List<string> rewardRatiosString = Functions.SplitString(paramList[2], split3);
			List<int> rewardRatios = new List<int>();
			for (int i2 = 0; i2 < rewardRatiosString.Count; i2++) {
				rewardRatios.Add(int.Parse(rewardRatiosString[i2]));
			}

			Data data = new Data(
				int.Parse(paramList[0]),
				rewardPoints,
				rewardRatios
			);

			DataDict.Add(int.Parse(paramList[0]), data);
		}
	}

	// DataはSet関数をpublicに用意していないので、クローンにしなくて良い
	public Data GetData(int id)
	{
		Data data = null;
		DataDict.TryGetValue(id, out data);

		return data;
	}
}
