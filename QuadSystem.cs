using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System.Collections;

using Unity.Jobs;
using System.Linq;

namespace Snorlax.QuadrantSystem
{
    [System.Serializable]
    public static class QuadSystem
    {

        #region Variables
        public static Dictionary<int, List<QuadData>> QuadList = new Dictionary<int, List<QuadData>>();
        public static List<QuadData> entities = new List<QuadData>();
        private const int quadZMulti = 1000;
        private const int quadCellSize = 5;

        static int repeat;
        static int Column;
        static int Row;
        #endregion

        #region Public Methods
        public static void OnUpdate()
        {
            QuadList?.Clear();
            for (int i = 0; i < entities.Count; i++)
            {
                QuadEntityEntry(entities[i]);
            }
        }

        public static List<QuadData> FindTargets(QuadData transform, int radius)
        {
            return QuadrantsInRadius(transform, radius);
        }
        #endregion

        #region Private Methods

        public static void Init(int radius)
        {
            int loop = (int)math.ceil((float)radius / (float)quadCellSize);
            repeat = 2 * loop + 1;
            Column = -1 * loop;
            Row = -quadZMulti * loop;
        }

        private static float3 RemoveHeight(this float3 position)
        {
            return new float3(position.x, 0, position.z);
        }

        private static int GetPosition(Vector3 position)
        {
            return (int)(math.floor(position.x / quadCellSize) + (quadZMulti * math.floor(position.z / quadCellSize)));
        }
        private static void QuadEntityEntry(QuadData transform)
        {
            transform.quadID = GetPosition(transform.targetTransform.position);
            //Hashtable(transform, key);
            Dictionary(transform, transform.quadID);

            /*
            if (!QuadCell.Contains(key))
            {
                QuadCell.Add(key);
            }*/

            
        }

        private static List<QuadData> QuadrantsInRadius(QuadData position, int radius)
        {

            List<QuadData> transforms = new List<QuadData>();

            if (QuadList.TryGetValue(position.quadID, out List<QuadData> Out))
            {
                //int i = 0;
                int count = Out.Count;

                for (int i = 0; i < count; i++)
                {
                    transforms.Add(Out[i]);
                    if (i == 5) return transforms;

                }

            }

            return transforms;
            #region Old Code
            //QuadList[key]?.ForEach(e => transforms.AddTransform(e));

            /*
            for (int i = 0; i < repeat; i++)
            {


                int RowEdit = Row;
                for (int j = 0; j < repeat; j++)
                {
                    int calKey = key + RowEdit + Column;

                    if (calKey == key) continue;

                    if (QuadList.ContainsKey(calKey)) QuadList[calKey]?.ForEach(e => transforms.AddTransform(e));
                    RowEdit += quadZMulti;
                }
                Column++;
            }
            return transforms;*/
            #endregion
        }

        private static void Dictionary(QuadData transform, int key)
        {
            if (!QuadList.TryGetValue(key, out List<QuadData> value))
            {
                value = new List<QuadData>();
                QuadList.Add(key, value);
            }
            value.Add(transform);
        }

        #endregion
    }

}

public interface QuadData
{
    public Transform targetTransform { get; set; }
    public int quadID { get; set; }
}