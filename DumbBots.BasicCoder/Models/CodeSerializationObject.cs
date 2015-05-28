using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace DumbBots.BasicCoder
{
    [Serializable]
    [XmlInclude(typeof(Command))]
    [XmlInclude(typeof(HurtSelfCommand))]
    [XmlInclude(typeof(ShootBulletCommand))]
    [XmlInclude(typeof(ShootRocketCommand))]
    [XmlInclude(typeof(FalseCommand))]
    [XmlInclude(typeof(TrueCommand))]
    [XmlInclude(typeof(MaxAmmoCommand))]
    [XmlInclude(typeof(MaxHealthCommand))]
    [XmlInclude(typeof(GetEnemyPositionCommand))]
    [XmlInclude(typeof(GetEnemySightedCommand))]
    [XmlInclude(typeof(ManualNumberCommand))]
    [XmlInclude(typeof(ManualStringCommand))]
    [XmlInclude(typeof(GetNearestBazookaCommand))]
    [XmlInclude(typeof(GetNearestMedkitCommand))]
    [XmlInclude(typeof(GetRandomBazookaCommand))]
    [XmlInclude(typeof(GetRandomMedkitCommand))]
    [XmlInclude(typeof(MoveRandomCommand))]
    [XmlInclude(typeof(MoveToPointCommand))]
    [XmlInclude(typeof(StopCommand))]
    [XmlInclude(typeof(GetAmmoCommand))]
    [XmlInclude(typeof(GetHealthCommand))]
    [XmlInclude(typeof(GetPositionCommand))]
    [XmlInclude(typeof(SayTextCommand))]
    [XmlInclude(typeof(GetBazookaCountCommand))]
    [XmlInclude(typeof(GetMedkitCountCommand))]
    [XmlInclude(typeof(Condition))]
    [XmlInclude(typeof(IfElseSerializationObject))]
    [XmlInclude(typeof(Operator))]
    public class CodeSerializationObject
    {
        public object[] Statements { get; set; }
    }
}