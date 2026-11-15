using Unity.Hierarchy;
using UnityEngine;

// This is for making stat boosts. If you are not using stat boosts
// then refer to the SO itself. 

public enum StatType
{
    TopSpeed,
    Turn
}

public class Stats
{
    readonly StatsMediator mediator;
    readonly BaseStats baseStats;

    public StatsMediator Mediator => mediator;

    public float TopSpeed
    {
        get
        {
            var q = new Query(StatType.TopSpeed, baseStats.topSpeed);
            mediator.PerformQuery(this, q);
            return q.Value;
        }
    }

    public float Turn
    {
        get
        {
            var q = new Query(StatType.Turn, baseStats.turnSpeed);
            mediator.PerformQuery(this, q);
            return q.Value;
        }
    }

    public Stats(StatsMediator mediator, BaseStats baseStats)
    {
        this.mediator = mediator;
        this.baseStats = baseStats;
    }

    public override string ToString() => $"TopSpeed: {TopSpeed}, Turn: {Turn}";
}
