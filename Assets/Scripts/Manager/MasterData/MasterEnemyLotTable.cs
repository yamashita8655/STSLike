using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterEnemyLotTable : SimpleSingleton<MasterEnemyLotTable>
{
	public class Data {
		public int Id { get; private set; }
		public List<int> EnemyIds { get; private set; }
		public List<int> LotWeights { get; private set; }

        public Data(
			int id,
			List<int> enemyIds,
			List<int> lotWeights
		)
		{
			Id			= id;
			EnemyIds	= enemyIds;
			LotWeights	= lotWeights;
		}
	};

	private readonly string FilePath = "csv/enemylottable";

	private Dictionary<int, Data> DataDict = new Dictionary<int, Data>();

	public void Initialize()
	{
		if (DataDict.Count > 0) {
			LogManager.Instance.Log("MasterEnemyLotTable:Initialize return.");
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

			List<string> enemyIdString = Functions.SplitString(paramList[1], split3);
			List<string> lotWeightString = Functions.SplitString(paramList[2], split3);

			List<int> enemyIds = new List<int>();
			for (int i2 = 0; i2 < enemyIdString.Count; i2++) {
				enemyIds.Add(int.Parse(enemyIdString[i2]));
			}
			
			List<int> lotWeights = new List<int>();
			for (int i2 = 0; i2 < lotWeightString.Count; i2++) {
				lotWeights.Add(int.Parse(lotWeightString[i2]));
			}

			Data data = new Data(
				int.Parse(paramList[0]),
				enemyIds,
				lotWeights
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


