using System;
using IrrlichtNETCP;
using Game;

namespace Entities
{
    /// <summary>
    /// Represents a medkit entity
    /// </summary>
    internal class MedkitEntity : Entity
    {
        internal MedkitEntity(SceneNode node)
            : base(node)
        { }

        internal int HealthBoost
        {
            get { return RuleManager.MedkitHealthBoost; }
        }
    }
}