        public IHelm GetHelm()
        {
            return Helm;
        }

        public ICollection<IShip> GetVisibleShips()
        {
            return Ships.Values;
        }

        public ICollection<Star> GetStars()
        {
            return Stars.Values;
        }

        public ICollection<IMissile> GetVisibleMissiles()
        {
            return Missiles;
        }

        public IList<string> GetCarriedShips()
        {
            return Carried;
        }

        public void Update()
        {
            var view = Client.GetView();
            Helm.Update(view.Helm);
            Ships = view.Ships.OfType<IShip>().ToDictionary(s => s.Name);
            Missiles = view.Missiles.OfType<IMissile>().ToList();
            Stars = view.Stars.ToDictionary(s => s.Name);
            Carried = view.Carried.ToList();
        }

        public void Fire(IShip ship, int[] launchers)
        {
            Client.Fire(ship.Name, launchers);
        }

        public void Launch(string name)
        {
            Client.Launch(name);
        }
