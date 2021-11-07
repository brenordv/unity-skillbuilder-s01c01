using System.Linq;
using UnityEngine;

public class Helpers: Behaviour
{
        public static Teleport[] GetTeleportPads(Teleport refToSelf = null)
        {
                var targets = FindObjectsOfType<Teleport>()
                        .Where(w => w != refToSelf && w.isActiveAndEnabled)
                        .ToArray();

                return targets;
        }
}