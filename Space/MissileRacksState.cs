using System;
using System.Collections.Generic;
using System.Linq;

namespace SF.Space
{
    public class MissileRacksState
    {
        public MissileRack[] Racks { get; private set; }
        public int TotalCount { get; private set; }
        public double[] Reloading { get; private set; }

        public MissileRacksState(MissileRack[] racks)
        {
            Racks = racks;
            TotalCount = Racks.Sum(rack => rack.Count);
            SetStatePairs(null);
        }

        public KeyValuePair<int, double>[] GetStatePairs()
        {
            var result = Reloading.
                Select((value, index) => new KeyValuePair<int, double>(index, value)).
                Where(pair => !MathUtils.NearlyEqual(pair.Value, 0)).
                ToArray();
            return result.Length == 0 ? null : result;
        }

        public void SetStatePairs(KeyValuePair<int, double>[] pairs)
        {
            if (Reloading == null || Reloading.Length != TotalCount)
                Reloading = new double[TotalCount];
            else
                for (int i = 0; i < Reloading.Length; i++)
                    Reloading[i] = 0;
            if (pairs == null || pairs.Length == 0)
                return;
            foreach (var pair in pairs)
                Reloading[pair.Key] = pair.Value;
        }

        public void Reload(double dt)
        {
            for (int i = 0; i < Reloading.Length; i++)
            {
                var r = Reloading[i];
                if (r <= 0)
                    continue;
                Reloading[i] = Math.Max(r - dt, 0);
            }
        }
    }
}
