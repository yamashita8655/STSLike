using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterEnemyAITable : SimpleSingleton<MasterEnemyAITable>
{
	public class Data {
		public int Id { get; private set; }
		public EnumSelf.EnemyActionType EnemyActionType { get; private set; }
		public List<int> ActionIds { get; private set; }
		public EnumSelf.AIChangeType AIChangeType { get; private set; }
		public int AIChangeValue { get; private set; }
		public int AfterAIId { get; private set; }

        public Data(
			int id,
			EnumSelf.EnemyActionType enemyActionType,
			List<int> actionIds,
			EnumSelf.AIChangeType aIChangeType,
			int aIChangeValue,
			int afterAIId
		)
		{
			Id				= id;
			EnemyActionType = enemyActionType;
			ActionIds       = actionIds;
			AIChangeType    = aIChangeType;
			AIChangeValue   = aIChangeValue;
			AfterAIId       = afterAIId;
		}
	};

	private readonly string FilePath = "csv/enemyaitable";

	private Dictionary<int, Data> DataDict = new Dictionary<int, Data>();

	public void Initialize()
	{
		if (DataDict.Count > 0) {
			LogManager.Instance.Log("MasterEnemyAITable:Initialize return.");
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

			List<string> actionIdString = Functions.SplitString(paramList[2], split3);
			List<int> actionIds = new List<int>();
			for (int i2 = 0; i2 < actionIdString.Count; i2++) {
				actionIds.Add(int.Parse(actionIdString[i2]));
			}

			Data data = new Data(
				int.Parse(paramList[0]),
				BattleCalculationFunction.ConvertString2EnemyActionType(paramList[1]),
				actionIds,
				BattleCalculationFunction.ConvertString2AIChangeType(paramList[3]),
				int.Parse(paramList[4]),
				int.Parse(paramList[5])
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


