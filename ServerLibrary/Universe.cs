﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

using SF.Space;

namespace SF.ServerLibrary
{
    public sealed class Universe
    {
        private static readonly Random Random = new Random();

        private static readonly SynchronizationContext Context = SynchronizationContext.Current;

        public const int SmallDelay = 100;

        private readonly System.Diagnostics.Stopwatch m_stopWatch = new System.Diagnostics.Stopwatch();

        private readonly IDictionary<string, IHelm> m_helms;
        private readonly IList<IMissile> m_missiles;
        private readonly Thread m_backgroundWorker;

        private string SerializeObject<T>(T instance)
        {
            var serializer = new XmlSerializer(instance.GetType());
            var writer = new StringWriter();
            serializer.Serialize(writer, instance);
            writer.Close();
            return writer.ToString();
        }

        private T DeserializeObject<T>(string source)
        {
            var serializer = new XmlSerializer(typeof(T));
            var reader = new StringReader(source);
            var read = (T)serializer.Deserialize(reader);
            reader.Close();
            return read;
        }

        private string SerializeCollection<T, U>(IEnumerable<T> collection) where U : T
        {
            var list = collection.Cast<U>().ToArray();
            var serializer = new XmlSerializer(list.GetType());
            var writer = new StringWriter();
            serializer.Serialize(writer, list);
            writer.Close();
            return writer.ToString();
        }

        private U[] DeserializeCollection<U>(string source)
        {
            var serializer = new XmlSerializer(typeof(U[]));
            var reader = new StringReader(source);
            var read = (U[])serializer.Deserialize(reader);
            return read;
        }

        public Universe()
        {
            var catalog = File.ReadAllText("catalog.xml");
            var catalogDefinition = this.DeserializeObject<CatalogDefinition>(catalog);
            Catalog.Create(catalogDefinition);
            var ships = File.ReadAllText("helms.xml");
            var helms = this.DeserializeCollection<HelmDefinition>(ships);
            foreach (var h in helms)
            {
                h.MissileName = "Дротик";
                h.MissileNumber = 6;
            }
            File.WriteAllText("helms2.xml", this.SerializeCollection<HelmDefinition, HelmDefinition>(helms));
            m_helms = helms.Select(Helm.Load).ToDictionary(ship => ship.Ship.Name);
            m_missiles = new List<IMissile>();
            this.m_backgroundWorker = new Thread(this.TimingThreadStart) { IsBackground = true };
        }

        public bool IsRunning
        {
            get { return this.m_stopWatch.IsRunning; }
            set
            {
                if (this.m_stopWatch.IsRunning == value)
                    return;
                if (value)
                {
                    this.m_stopWatch.Start();
                    if (!this.m_backgroundWorker.IsAlive)
                        this.m_backgroundWorker.Start();
                }
                else
                    this.m_stopWatch.Stop();
            }
        }

        public TimeSpan Time
        {
            get { return this.m_stopWatch.Elapsed; }
        }

        public CatalogDefinition GetCatalog(string nation)
        {
            var shipClassesByNation = Catalog.Instance.ShipClasses.Values.Where(c => c.Nation == nation);
            var shipClassesByShips = this.m_helms.Where(i => i.Value.Ship.Nation == nation).Select(i => i.Value.Ship.Class);
            var missileClassesByNation = Catalog.Instance.MissileClasses.Values.Where(c => c.Nation == nation);
            var missileClassesByShips = this.m_helms.Where(i => i.Value.Ship.Nation == nation).Select(i => i.Value.Ship.Missile);
            return new CatalogDefinition
            {
                ShipClasses = shipClassesByNation.Union(shipClassesByShips).Distinct().ToArray(),
                MissileClasses = missileClassesByNation.Union(missileClassesByShips).Distinct().ToArray(),
            };
        }

        public IHelm GetHelm(string name)
        {
            IHelm result;
            if (!m_helms.TryGetValue(name, out result))
                return null;
            return result;
        }

        public IHelm GetHelm(string nation, string name)
        {
            var helm = GetHelm(name);
            if (helm.Ship.Nation != nation)
                return null;
            return helm;
        }

        public IEnumerable<IShip> GetVisibleShips(IHelm me)
        {
            return this.m_helms.Where(i => i.Value != me).Select(i => i.Value.Ship); 
        }

        public IEnumerable<IMissile> GetVisibleMissiles(IHelm me)
        {
            return this.m_missiles;
        }

        public IEnumerable<string> GetNations()
        {
            return this.m_helms.Select(i => i.Value.Ship.Nation).Distinct();
        }

        public IEnumerable<string> GetShipNames(string nation)
        {
            return this.m_helms.Where(i => i.Value.Ship.Nation == nation).Select(i => i.Value.Ship.Name).Distinct();
        }

        public void Fire(IShip from, bool left, string to, int number)
        {
            number = Math.Min(number, from.Missiles);
            var target = GetHelm(to);
            if (from.Missile == null || target == null || number <= 0)
                return;
            var result = new Missile(from, left, target.Ship, number, Time);
            m_missiles.Add(result);
        }

        private void TimingThreadStart()
        {
            while (true)
            {
                Thread.Sleep(SmallDelay);
                if (this.m_stopWatch.IsRunning)
                {
                    double t = this.Time.TotalSeconds;
                    foreach (var helm in m_helms.Values)
                        ((Ship)helm.Ship).Dynamics.UpdateTime(t);
                    foreach (Missile missile in m_missiles)
                        missile.UpdateTime(t);
                    var deleted = m_missiles.Where(missile => missile.IsDead);
                    foreach (var missile in deleted)
                        m_missiles.Remove(missile);
                }
            }
        }
    }
}
