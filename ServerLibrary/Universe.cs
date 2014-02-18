using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using SF.Space;

namespace SF.ServerLibrary
{
    using System.Text;

    public sealed class Universe
    {
        private static readonly Random Random = new Random();
        private static readonly SynchronizationContext Context = SynchronizationContext.Current;
        private object m_locker = new object();

        public const int SmallDelay = 100;

        private readonly System.Diagnostics.Stopwatch m_stopWatch = new System.Diagnostics.Stopwatch();

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

        private string SerializeCollection<T>(IEnumerable<T> collection)
        {
            var list = collection.ToArray();
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
            m_backgroundWorker = new Thread(TimingThreadStart) { IsBackground = true };
        }

        public void Load(string filename, bool clear)
        {
            string xml = File.ReadAllText(filename, Encoding.Unicode);
            ViewData view = DeserializeObject<ViewData>(xml);
        }
        
        public void Save(string name)
        {
        }

        public bool IsRunning
        {
            get { return m_stopWatch.IsRunning; }
            set
            {
                if (m_stopWatch.IsRunning == value)
                    return;
                if (value)
                {
                    m_stopWatch.Start();
                    if (!m_backgroundWorker.IsAlive)
                        m_backgroundWorker.Start();
                }
                else
                    m_stopWatch.Stop();
            }
        }

        public TimeSpan Time
        {
            get { return m_stopWatch.Elapsed; }
        }

        private void TimingThreadStart()
        {
            var tPrev = Time.TotalSeconds;
            while (true)
            {
                Thread.Sleep(SmallDelay);
                if (!m_stopWatch.IsRunning)
                    continue;
                lock (m_locker)
                {
                    double t = Time.TotalSeconds;
                    var dt = t - tPrev;
                }
            }
        }

        public void InternalTest()
        {
            PermanentViewData p = new PermanentViewData
            {
                Constants = new ConstantData
                {
                    DefaultCarrierRange = 3000000,
                    DefaultScale = 10000000,
                    DefaultSkirtAngle = 0.26179938779914941,
                    DefaultThroatAngle = 0.78539816339744828,
                    MaximumMissileRange = 100000000,
                },
                Generation = 1,
                Time = TimeSpan.Zero,
                Nations = new Nation[3],
                MissileClasses = new MissileClass[2],
                ShipClasses = new ShipClass[3],
                Stars = new Star[12],
                Ships = new PermanentShipData[6],
            };
            for (int i = 0; i < p.Nations.Length; i++)
                p.Nations[i] = new Nation
                {
                    Id = 1001 + i,
                    Name = string.Format("Нация{0}", i + 1),
                };
            for (int i = 0; i < p.MissileClasses.Length; i++)
                p.MissileClasses[i] = new MissileClass
                {
                    Id = 2001 + i,
                    Name = string.Format("Ракета{0}", i + 1),
                    Acceleration = 50000,
                    FlyTime = 45,
                    HitDistance = 15000,
                    IdNation = 0,
                    Targeting = 1,
                };
            for (int i = 0; i < p.ShipClasses.Length; i++)
            {
                p.ShipClasses[i] = new ShipClass
                {
                    Id = 3001 + 1,
                    Name = string.Format("Класс{0}", i + 1),
                    FullAccelerationTime = 180,
                    FullTurnTime = 600,
                    IdNation = 0,
                    MaximumAcceleration = 3000,
                    RechargeTime = 30,
                    ReloadTime = 90,
                    Weight = 15000000,
                    RoundRollTime = 300,
                    Superclass = ShipType.CLAC,
                    Wedge = 200,
                };
            }
            for (int i = 0; i < p.Stars.Length; i++)
            {
                p.Stars[i] = new Star
                {
                    Id = 4001 + i,
                    Name = string.Format("Звезда{0}", i + 1),
                    IdNation = 0,
                    StarClass = StarType.Habitable,
                    Radius = 5000,
                    Position = Vector.Direction(i)*150000000,
                };
            }
            for (int i = 0; i < p.Ships.Length; i++)
            {
                p.Ships[i] = new PermanentShipData
                {
                    Id = 5001 + i,
                    Name = string.Format("Корабль{0}", i + 1),
                    IdNation = 0,
                    IdClass = 0,
                };
            }
            VolatileViewData v = new VolatileViewData
            {
                Time = p.Time,
                Missiles = new Missile[0],
                Ships = new VolatileShipData[p.Ships.Length],
            };
            for (int i = 0; i < v.Ships.Length; i++)
            {
                v.Ships[i] = new VolatileShipData
                {
                    Id = 5001 + i,
                    HealthRate = 1,
                    Heading = Random.NextAngle(),
                    Roll = 0,
                    Thrust = 0,
                    Position = Random.NextDirection() * Random.NextDouble() * 150000000,
                    Speed = Random.NextDirection() * Random.NextDouble() * 100000,
                };
            }
            ViewData view = new ViewData
            {
                PermanentView = p,
                VolatileView = v,
            };
            File.WriteAllText("test.xml", SerializeObject(view), Encoding.Unicode);
            //lock (m_locker)
            //{
            //    //var target = GetVisibleShips(null).First();
            //    //var missileClass = Catalog.Instance.MissileClasses.Values.First();
            //    //const int Hours = 36, Ticks = 3;

            //    //for (int i = 0; i < Hours; i++)
            //    //{
            //    //    var distance = (i + Ticks) * missileClass.HitDistance;
            //    //    var angle = 2 * i * Math.PI / Hours;
            //    //    var missile = new Missile(target, missileClass, Time, distance, angle);
            //    //    m_missiles.Add(missile);
            //    //}
            //}
        }
    }
}
