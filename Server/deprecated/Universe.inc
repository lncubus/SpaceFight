            foreach (var shipClass in ShipClasses.Values)
            {
                if (shipClass.Left == null)
                    shipClass.Left = generateMissileRacks().ToArray();
                if (shipClass.Right == null)
                    shipClass.Right = generateMissileRacks().ToArray();
            }

        private ICollection<MissileRack> generateMissileRacks()
        {
            var result = new List<MissileRack>();
            var classes = MissileClasses.Values.ToArray();
            foreach (var missileClass in classes)
            {
                var rack = new MissileRack
                {
                    Count = Random.Next(0, 3),
                    MissileClass = missileClass,
                    IdMMissileClass = missileClass.Id,
                };
                if (rack.Count > 0)
                    result.Add(rack);
            }
            return result;
        }

