using System;
using System.Collections.Generic;
using System.Linq;

namespace SF.Space
{
    public class MissileRacksState
    {
        public MissileRack[] Racks { get; private set; }
        public int TotalCount { get; private set; }
        public double[] Reloading { get; set; }

        public MissileRacksState(MissileRack[] racks)
        {
            Racks = racks;
            TotalCount = Racks.Sum(rack => rack.Count);
            Reloading = new double[TotalCount];
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

        public bool Fire(int number)
        {
            bool failed = number >= TotalCount || !MathUtils.NearlyEqual(Reloading[number], 0);
            if (failed)
                return false;
            var rack = GetRack(number);
            Reloading[number] = rack.MissileClass.ReloadTime;
            return true;
        }

        public KeyValuePair<MissileRack, double[]>[] GetReloadingTimes()
        {
            var result = new KeyValuePair<MissileRack, double[]>[Racks.Length];
            int k = 0;
            for (int i = 0; i < Racks.Length; i++)
            {
                var rack = Racks[i];
                var reload = new double[rack.Count];
                for (int j = 0; j < rack.Count; j++)
                    reload[j] = Reloading[k + j];
                k += rack.Count;
                result[i] = new KeyValuePair<MissileRack, double[]>(rack, reload);
            }
            return result;
        }

        public MissileRack GetRack(int index)
        {
            foreach (var rack in Racks)
                if (index < rack.Count)
                    return rack;
                else
                    index -= rack.Count;
            throw new IndexOutOfRangeException("Invalid missile tube number");
        }
    }
}
