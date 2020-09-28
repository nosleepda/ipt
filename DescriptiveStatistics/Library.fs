namespace DescriptiveStatistics

open System
open System.Collections.Generic
open GraphicLibrary
open MathNet.Numerics.Distributions

module Statistics =
    let sourceData =
        List<double>[28.0; 30.0; 28.0; 27.0; 28.0; 29.0; 29.0; 29.0; 31.0; 28.0; 26.0; 25.0; 33.0;
35.0; 27.0; 31.0; 31.0; 30.0; 28.0; 33.0; 23.0; 30.0; 31.0; 33.0; 31.0; 27.0;
30.0; 28.0; 30.0; 29.0; 30.0; 26.0; 25.0; 31.0; 33.0; 26.0; 27.0; 33.0; 29.0;
30.0; 30.0; 36.0; 26.0; 25.0; 28.0; 30.0; 29.0; 27.0; 32.0; 29.0; 31.0; 30.0;
31.0; 26.0; 25.0; 29.0; 31.0; 33.0; 27.0; 32.0; 30.0; 31.0; 34.0; 28.0; 26.0;
38.0; 29.0; 31.0; 29.0; 27.0; 31.0; 30.0; 28.0; 34.0; 30.0; 26.0; 30.0; 32.0;
30.0; 29.0; 30.0; 28.0; 32.0; 30.0; 29.0; 34.0; 32.0; 35.0; 29.0; 27.0; 28.0;
30.0; 30.0; 29.0; 32.0; 29.0; 34.0; 30.0; 32.0; 24.0;]
//        List<double>[0.90;0.79;0.84;0.86;0.88;0.90;0.89;0.85;0.91;0.98;0.91;0.80;0.87;
//0.89;0.88;0.78;0.81;0.85;0.88;0.94;0.86;0.80;0.86;0.91;0.78;0.86;
//0.91;0.95;0.97;0.88;0.79;0.82;0.84;0.90;0.81;0.87;0.91;0.90;0.82;
//0.85;0.90;0.82;0.85;0.90;0.96;0.98;0.89;0.87;0.99;0.85;]


//    let initData data =
//        sourceData.Clear |> ignore
//        sourceData.AddRange data

    let series = List<double>()

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
        let arr = ResizeArray<double>(int kRounded + 1)
        let mutable temp = initElem;
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
        frequenciesInterval |> List.ofSeq |> List.tail |> List<int>
        
    let nGroup = discrete.Length

    let nNoGroup = sortedData.Length

    let dictDa =
        Seq.map2 (fun d f -> d, f) discrete frequenciesDiscrete
        |> dict

    let mode: double =
        let max = dictDa.Values |> List.ofSeq |> List.max
        let mutable key = 0.0
        for x in dictDa do
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

    let expectedValueGroup =
        let mutable sum = 0.0
        for x in dictDa do
            sum <- sum + x.Key * double x.Value

        sum / double nNoGroup

    let varianceGroup =
        let mutable sum = 0.0
        for x in dictDa do
            sum <- sum + ((expectedValueGroup - x.Key) ** 2.0) * (double x.Value)

        sum <- sum / double (if nNoGroup > 50 then nNoGroup - 1 else nNoGroup)
        sum

    let standardDeviationGroup = sqrt varianceGroup

    let coefficientVariation =
        standardDeviationGroup / expectedValueGroup

    let m3 =
        let mutable sum = 0.0
        for x in dictDa do
            sum <- sum + ((x.Key - expectedValueGroup) ** 3.0) * (double x.Value)
            
        sum <- sum / double nNoGroup
        sum
        

    let skewness =
        m3 / (standardDeviationGroup ** 3.0)

    let m4 =
        let mutable sum = 0.0
        for x in dictDa do
            sum <- sum + (double (double x.Key - double expectedValueGroup) ** 4.0) * (double x.Value)

        sum <- sum / double nNoGroup
        sum

    let kurtosis =
        m4 / (standardDeviationGroup ** 4.0) - 3.0
        
    let t y =
        StudentT.InvCDF(0.0, 1.0, nNoGroup - 1 |> double, 1.0 - y/2.0)
    
    let confidenceIntervalMx y =
        let temp = t y * standardDeviationGroup / (sqrt (double nNoGroup))
        (expectedValueGroup - temp, expectedValueGroup + temp)

    let chi y =
        ChiSquared.InvCDF(nNoGroup - 1 |> double, y/2.0)
    
    let confidenceIntervalDx y =
        let temp = varianceGroup * (double nNoGroup - 1.0) 
        (sqrt (temp / chi (1.0 + y)), sqrt(temp / chi (1.0 - y)))
    
    let emper x =
        let mutable sum = 0
        for elem in dictDa do
            sum <- sum + (if elem.Key < x then elem.Value else 0)
        sum / nNoGroup

    let ws =
        frequenciesDiscrete
        |> List.ofSeq
        |> List.map (fun x -> double x / double nNoGroup)

    let WSS =
        let da = List<float>(ws.Length)
        da.Add ws.Head
        for x = 1 to ws.Length - 1 do
            da.Add(da.[x - 1] + ws.[x])
        da

    let drawFrequencyPolygon =
        let graphs = Graphic("Frequency Polygons", "", "")
        graphs.SetPlane(0, frequenciesDiscrete.Count + 1, 0, 2, 0, (frequenciesDiscrete |> Seq.max |> int) + 2, 0, 4)
        graphs.DrawFrequencyPolygon(frequenciesDiscrete |> Array.ofSeq, true, "green")
        0

    let drawHistogram =
        let graphs = Graphic("Histogram", "", "")
        graphs.SetPlane(0, frequenciesDiscrete.Count + 1, 0, 2, 0, (frequenciesDiscrete |> Seq.max |> int) + 2, 0, 4)
        graphs.DrawBarsGraph(frequenciesDiscrete |> Array.ofSeq)
        0

    let drawFrequencyPolygon2 =
        let graphs = Graphic("Frequency Polygons", "", "")
        graphs.SetPlane(0, ws.Length + 1, 0, 2, 0, (ws |> Seq.max |> int) + 2, 0, 4)
        graphs.DrawFrequencyPolygon(WSS |> Array.ofSeq)
        0

    let drawHistograms =
        let graphs = Graphic("Histogram", "", "")
        graphs.SetPlane(0.0, double(ws.Length + 1), 0.0, 1.3, 0.0, 2.0, 0.0, 0.3)
        graphs.DrawBarsGraph(WSS |> Array.ofSeq)
        0