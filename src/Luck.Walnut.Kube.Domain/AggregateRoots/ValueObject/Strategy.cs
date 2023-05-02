﻿namespace Luck.Walnut.Kube.Domain.AggregateRoots.ValueObject;

/// <summary>
/// 部署更新策略对象
/// </summary>
public record Strategy
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="maxSurge"></param>
    /// <param name="maxUnavailable"></param>
    public Strategy(string type, string maxSurge, string maxUnavailable)
    {
        Type = type;
        MaxSurge = maxSurge;
        MaxUnavailable = maxUnavailable;
    }

    /// <summary>
    /// 策略类型
    /// </summary>
    public string Type { get; private set; }

    /// <summary>
    /// 可调度的最大吊舱数量超过所需吊舱数量
    /// </summary>
    public string MaxSurge { get; private set; }

    /// <summary>
    /// 更新期间不可用的最大pod数
    /// </summary>
    public string MaxUnavailable { get; private set; }

}