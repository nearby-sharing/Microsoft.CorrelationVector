﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.CorrelationVector;

public enum SpinCounterInterval
{
    /// <summary>
    /// The coarse interval drops the 24 least significant bits in DateTime.Ticks
    /// resulting in a counter that increments every 1.67 seconds. 
    /// </summary>
    Coarse,

    /// <summary>
    /// The fine interval drops the 16 least significant bits in DateTime.Ticks
    /// resulting in a counter that increments every 6.5 milliseconds.
    /// </summary>
    Fine
}

public enum SpinCounterPeriodicity
{
    /// <summary>
    /// Do not store a counter as part of the spin value.
    /// </summary>
    None,

    /// <summary>
    /// The short periodicity stores the counter using 16 bits.
    /// </summary>
    Short,

    /// <summary>
    /// The medium periodicity stores the counter using 24 bits.
    /// </summary>
    Medium,

    /// <summary>
    /// The long periodicity stores the counter using 32 bits.
    /// </summary>
    Long
}

public enum SpinEntropy
{
    /// <summary>
    /// Do not generate entropy as part of the spin value.
    /// </summary>
    None = 0,

    /// <summary>
    /// Generate entropy using 8 bits.
    /// </summary>
    One = 1,

    /// <summary>
    /// Generate entropy using 16 bits.
    /// </summary>
    Two = 2,

    /// <summary>
    /// Generate entropy using 24 bits.
    /// </summary>
    Three = 3,

    /// <summary>
    /// Generate entropy using 32 bits.
    /// </summary>
    Four = 4
}

/// <summary>
/// This class stores parameters used by the CorrelationVector Spin operator.
/// </summary>
public sealed class SpinParameters
{
    // Internal value for entropy bytes.
    private int entropyBytes;

    /// <summary>
    /// The interval (proportional to time) by which the counter increments.
    /// </summary>
    public SpinCounterInterval Interval { get; set; }

    /// <summary>
    /// How frequently the counter wraps around to zero, as determined by the amount
    /// of space to store the counter.
    /// </summary>
    public SpinCounterPeriodicity Periodicity { get; set; }

    /// <summary>
    /// The number of bytes to use for entropy. Valid values from a
    /// minimum of 0 to a maximum of 4.
    /// </summary>
    public SpinEntropy Entropy
    {
        get
        {
            return (SpinEntropy)this.entropyBytes;
        }
        set
        {
            this.entropyBytes = (int)value;
        }
    }

    /// <summary>
    /// The number of least significant bits to drop in DateTime.Ticks when
    /// computing the counter.
    /// </summary>
    internal int TicksBitsToDrop
    {
        get
        {
            return this.Interval switch
            {
                SpinCounterInterval.Coarse => 24,
                SpinCounterInterval.Fine => 16,
                _ => 24,
            };
        }
    }

    /// <summary>
    /// The number of bytes used to store the entropy.
    /// </summary>
    internal int EntropyBytes
    {
        get
        {
            return this.entropyBytes;
        }
    }

    internal int TotalBits
    {
        get
        {
            var counterBits = this.Periodicity switch
            {
                SpinCounterPeriodicity.None => 0,
                SpinCounterPeriodicity.Short => 16,
                SpinCounterPeriodicity.Medium => 24,
                SpinCounterPeriodicity.Long => 32,
                _ => 0,
            };
            return counterBits + this.EntropyBytes * 8;
        }
    }
}
