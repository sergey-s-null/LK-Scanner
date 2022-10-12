﻿using Service.Entities.Abstract;

namespace Service.Entities;

public class ScanResult : IScanResult
{
    public int Processed { get; }
    public IReadOnlyDictionary<SuspicionType, int> Detections { get; }
    public int Errors { get; }
    public TimeSpan ExecutionTime { get; }

    public ScanResult(
        int processed,
        IReadOnlyDictionary<SuspicionType, int> detections,
        int errors,
        TimeSpan executionTime)
    {
        Processed = processed;
        Detections = detections;
        Errors = errors;
        ExecutionTime = executionTime;
    }
}