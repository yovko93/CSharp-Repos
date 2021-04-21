﻿using System.Linq;
using System.Collections.Generic;

using CounterStrike.Models.Players;
using CounterStrike.Models.Maps.Contracts;
using CounterStrike.Models.Players.Contracts;

namespace CounterStrike.Models.Maps
{
    public class Map : IMap
    {
        //  List<IPlayer> terro && List<IPlayer> counterTerro = fields
        // ctor
        // method SeparateTeams(List<IPlayer> players);
        // method IsTeamAlive();
        // method...


        public string Start(ICollection<IPlayer> players)
        {
            var terrorists = players
                .Where(x => x.GetType() == typeof(Terrorist))
                .ToList();

            var counterTerrorists = players
                .Where(x => x.GetType() == typeof(CounterTerrorist))
                .ToList();

            while (terrorists.Any(x => x.IsAlive) && counterTerrorists.Any(x => x.IsAlive))
            {
                foreach (var terro in terrorists)
                {
                    if (!terro.IsAlive)
                    {
                        continue;
                    }

                    foreach (var counterTerro in counterTerrorists)
                    {
                        if (!counterTerro.IsAlive)
                        {
                            continue;
                        }

                        counterTerro.TakeDamage(terro.Gun.Fire());
                    }

                }

                foreach (var counterTerro in counterTerrorists)
                {
                    if (!counterTerro.IsAlive)
                    {
                        continue;
                    }

                    foreach (var terro in terrorists)
                    {
                        if (!terro.IsAlive)
                        {
                            continue;
                        }

                        terro.TakeDamage(counterTerro.Gun.Fire());
                    }
                }

            }

            string result = string.Empty;

            if (terrorists.Count == 0)
            {
                result = "Counter Terrorist wins!";
            }
            else
            {
                result = "Terrorist wins!";
            }

            return result;
        }
    }
}

