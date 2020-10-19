namespace DescriptiveStatistics

open System.Collections.Generic
open MathNet.Numerics.Distributions

module Statistics =
    
    let rand mean stdDev =
        let normalDist = Normal(mean, stdDev);
        normalDist.Sample()
        
    let sourceData =
//        List.init 300000 (fun _ -> rand 100.0 10.0) |> List<double>
        [|28.0; 30.0; 28.0; 27.0; 28.0; 29.0; 29.0; 29.0; 31.0; 28.0; 26.0; 25.0; 33.0;
 35.0; 27.0; 31.0; 31.0; 30.0; 28.0; 33.0; 23.0; 30.0; 31.0; 33.0; 31.0; 27.0;
 30.0; 28.0; 30.0; 29.0; 30.0; 26.0; 25.0; 31.0; 33.0; 26.0; 27.0; 33.0; 29.0;
 30.0; 30.0; 36.0; 26.0; 25.0; 28.0; 30.0; 29.0; 27.0; 32.0; 29.0; 31.0; 30.0;
 31.0; 26.0; 25.0; 29.0; 31.0; 33.0; 27.0; 32.0; 30.0; 31.0; 34.0; 28.0; 26.0;
 38.0; 29.0; 31.0; 29.0; 27.0; 31.0; 30.0; 28.0; 34.0; 30.0; 26.0; 30.0; 32.0;
 30.0; 29.0; 30.0; 28.0; 32.0; 30.0; 29.0; 34.0; 32.0; 35.0; 29.0; 27.0; 28.0;
 30.0; 30.0; 29.0; 32.0; 29.0; 34.0; 30.0; 32.0; 24.0;|]
//        List<double>[21.0; 29.0; 27.0; 29.0; 27.0; 29.0; 31.0; 29.0; 31.0; 29.0; 29.0; 23.0; 39.0;
//31.0; 29.0; 31.0; 29.0; 31.0; 29.0; 31.0; 33.0; 31.0; 31.0; 31.0; 27.0; 23.0;
//27.0; 33.0; 29.0; 25.0; 29.0; 19.0; 29.0; 31.0; 23.0; 31.0; 29.0; 27.0; 33.0;
//29.0; 31.0; 29.0; 31.0; 23.0; 35.0; 27.0; 29.0; 29.0; 27.0; 29.0; 29.0; 21.0;
//29.0; 27.0; 29.0; 29.0; 29.0; 33.0; 29.0; 25.0; 25.0; 27.0; 31.0; 29.0; 29.0;
//27.0; 33.0; 29.0; 31.0; 29.0; 29.0; 29.0; 35.0; 27.0; 29.0; 35.0; 29.0; 33.0;
//29.0; 27.0; 31.0; 31.0; 27.0; 29.0; 35.0; 27.0; 33.0; 29.0; 27.0; 29.0; 25.0;
//27.0; 31.0; 37.0; 25.0; 31.0; 27.0; 27.0; 29.0; 25.0;]
//        List<double>[0.90;0.79;0.84;0.86;0.88;0.90;0.89;0.85;0.91;0.98;0.91;0.80;0.87;
//0.89;0.88;0.78;0.81;0.85;0.88;0.94;0.86;0.80;0.86;0.91;0.78;0.86;
//0.91;0.95;0.97;0.88;0.79;0.82;0.84;0.90;0.81;0.87;0.91;0.90;0.82;
//0.85;0.90;0.82;0.85;0.90;0.96;0.98;0.89;0.87;0.99;0.85;]

    let sortedData = sourceData |> List.ofSeq |> List.sort

    let R =
        (List.max sortedData - List.min sortedData)

    let k =
        sortedData.Length
        |> double
        |> sqrt
    
    let kRounded =
        sortedData.Length
        |> double
        |> sqrt
        |> round

    let h = R / k

    let initElem = List.min sortedData - 0.5 * h
    let lastElem = List.max sortedData + 0.5 * h

    let interval =
        let arr = List<double>(int kRounded + 1)
        let mutable temp = initElem
        arr.Add temp
        for i = 1 to int kRounded do
            temp <- temp + h
            arr.Add (temp)
        
        arr.Add lastElem
        arr |> List.ofSeq
    
    let discrete =
        List.map2 (fun s e -> (s + e) / 2.0) (List.take (interval.Length - 1) interval) interval.Tail
        
    let frequenciesInterval =
        let mutable data = List<double> sortedData

        let frequencies2 =
            List<int>(List.init (interval.Length) (fun _ -> 0))

        for i = 0 to interval.Length - 1 do
            while (data.Count > 0 && data.[0] <= interval.[i]) do
                frequencies2.[i] <- frequencies2.[i] + 1
                data.RemoveAt(0)
        frequencies2

    let frequenciesDiscrete =
        frequenciesInterval.GetRange(1, frequenciesInterval.Count - 1)

    let n = sortedData.Length

    let dictData =
        Seq.map2 (fun d f -> d, f) discrete frequenciesDiscrete
        |> dict

    let mode: double =
        let max = dictData.Values |> List.ofSeq |> List.max
        let mutable key = 0.0
        for x in dictData do
            if max = x.Value then key <- x.Key
        key

    let median: double =
        let mutable m = 0.0
        if sortedData.Length % 2 = 0 then
            let kk = sortedData.Length / 2
            m <- (sortedData.Item kk + sortedData.Item(kk + 1)) / 2.0
        else
            let kk = (sortedData.Length - 1) / 2
            m <- sortedData.Item(kk + 1)
        m

    let mean =
        let mutable sum = 0.0
        for x in dictData do
            sum <- sum + x.Key * double x.Value

        sum / double n

    let variance =
        let mutable sum = 0.0
        for x in dictData do
            sum <- sum + ((mean - x.Key) ** 2.0) * (double x.Value)

        sum <- sum / double (if n > 50 then n - 1 else n)
        sum

    let standardDeviation = sqrt variance

    let coefficientVariation =
        standardDeviation / mean

    let m3 =
        let mutable sum = 0.0
        for x in dictData do
            sum <- sum + ((x.Key - mean) ** 3.0) * (double x.Value)
            
        sum <- sum / double n
        sum
        
    let skewness =
        m3 / (standardDeviation ** 3.0)

    let m4 =
        let mutable sum = 0.0
        for x in dictData do
            sum <- sum + (double (double x.Key - double mean) ** 4.0) * (double x.Value)

        sum <- sum / double n
        sum

    let kurtosis =
        m4 / (standardDeviation ** 4.0) - 3.0
        
    let t y =
        StudentT.InvCDF(0.0, 1.0, n - 1 |> double, 1.0 - y / 2.0)
    
    let confidenceIntervalMean y =
        let temp = t y * standardDeviation / (sqrt (double n))
        (mean - temp, mean + temp)

    let chi a =
        ChiSquared.InvCDF(n - 1 |> double, a / 2.0)
    
    let confidenceIntervalVariance y =
        let temp = variance * (double n - 1.0) 
        (sqrt (temp / chi (1.0 + y)), sqrt(temp / chi (1.0 - y)))
    
    let emper x =
        let mutable sum = 0
        for elem in dictData do
            sum <- sum + (if elem.Key < x then elem.Value else 0)
        sum / n

    let ws =
        frequenciesDiscrete
        |> List.ofSeq
        |> List.map (fun x -> double x / double n)

    let cumulate =
        let da = List<float>(ws.Length)
        da.Add ws.Head
        for x = 1 to ws.Length - 1 do
            da.Add(da.[x - 1] + ws.[x])
        da
    
    let draw =
        Graphic2.Plotly.drawHistogram discrete frequenciesDiscrete
        Graphic2.Plotly.drawDistributionFunction cumulate
        