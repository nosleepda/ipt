namespace DescriptiveStatistics

open System
open System.Collections.Generic
open GraphicLibrary

module Statistics =
//    let sourceData =
//        List<double>[28.0; 30.0; 28.0; 27.0; 28.0; 29.0; 29.0; 29.0; 31.0; 28.0; 26.0; 25.0; 33.0;
//35.0; 27.0; 31.0; 31.0; 30.0; 28.0; 33.0; 23.0; 30.0; 31.0; 33.0; 31.0; 27.0;
//30.0; 28.0; 30.0; 29.0; 30.0; 26.0; 25.0; 31.0; 33.0; 26.0; 27.0; 33.0; 29.0;
//30.0; 30.0; 36.0; 26.0; 25.0; 28.0; 30.0; 29.0; 27.0; 32.0; 29.0; 31.0; 30.0;
//31.0; 26.0; 25.0; 29.0; 31.0; 33.0; 27.0; 32.0; 30.0; 31.0; 34.0; 28.0; 26.0;
//38.0; 29.0; 31.0; 29.0; 27.0; 31.0; 30.0; 28.0; 34.0; 30.0; 26.0; 30.0; 32.0;
//30.0; 29.0; 30.0; 28.0; 32.0; 30.0; 29.0; 34.0; 32.0; 35.0; 29.0; 27.0; 28.0;
//30.0; 30.0; 29.0; 32.0; 29.0; 34.0; 30.0; 32.0; 24.0;]
    let sourceData =
        List<double>[0.90;0.79;0.84;0.86;0.88;0.90;0.89;0.85;0.91;0.98;0.91;0.80;0.87;
0.89;0.88;0.78;0.81;0.85;0.88;0.94;0.86;0.80;0.86;0.91;0.78;0.86;
0.91;0.95;0.97;0.88;0.79;0.82;0.84;0.90;0.81;0.87;0.91;0.90;0.82;
0.85;0.90;0.82;0.85;0.90;0.96;0.98;0.89;0.87;0.99;0.85;]


//    let initData data =
//        sourceData.Clear |> ignore
//        sourceData.AddRange data

    let series = List<double>()

    let sortedData = sourceData |> List.ofSeq |> List.sort

    let R =
        (List.max sortedData - List.min sortedData)

    let k =
        sortedData.Length
        |> Convert.ToDouble
        |> sqrt
    
    let kRounded =
        sortedData.Length
        |> Convert.ToDouble
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
        
//        let mutable data = List<double> sortedData
//
//        let frequencies =
//            List<int>(List.init (discrete.Length) (fun _ -> 0))
//
//        for i = 0 to discrete.Length - 1 do
//            while (data.Count > 0 && data.[0] <= discrete.[i]) do
//                frequencies.[i] <- frequencies.[i] + 1
//                data.RemoveAt(0)
//        frequencies

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
            sum <- sum + x.Key * Convert.ToDouble x.Value

        sum / Convert.ToDouble nNoGroup

    let expectedValueNoGroup =
        (List.sum sortedData) / Convert.ToDouble nNoGroup

    let varianceGroup =
        let mutable sum = 0.0
        for x in dictDa do
            sum <- sum + (x.Key ** 2.0) * (Convert.ToDouble x.Value)

        sum <- sum / Convert.ToDouble(if nNoGroup > 50 then nNoGroup - 1 else nNoGroup)
        sum - (expectedValueGroup ** 2.0)

    let varianceNoGroup =
        let mutable sum =
            (sortedData
             |> List.map (fun x -> x ** 2.0)
             |> List.sum)

        sum <- sum / Convert.ToDouble(if nNoGroup > 50 then nNoGroup - 1 else nNoGroup)
        sum - expectedValueNoGroup ** 2.0

    let standardDeviationGroup = sqrt varianceGroup

    let standardDeviationNoGroup = sqrt varianceNoGroup

    let coefficientVariation =
        standardDeviationGroup / expectedValueGroup

    let m3 =
        let mutable sum = 0.0
        for x in dictDa do
            sum <- sum + ((x.Key - expectedValueGroup) ** 3.0) * Convert.ToDouble x.Value
//        sum <- sum - expectedValueGroup ** 3.0
        sum <- sum / Convert.ToDouble nNoGroup
        sum
        

    let skewness = m3 / (standardDeviationGroup ** 3.0)

    let m4 =
        let mutable sum = 0.0
        for x in dictDa do
            sum <- sum + (x.Key ** 4.0) * Convert.ToDouble x.Value

        sum <- sum / Convert.ToDouble nNoGroup
        sum - expectedValueGroup ** 4.0

    let kurtosis =
        m4 / (standardDeviationGroup ** 4.0) - 3.0

    let emper x =
        let mutable sum = 0
        for elem in dictDa do
            sum <- sum + (if elem.Key < x then elem.Value else 0)
        sum / nNoGroup

    let ws =
        frequenciesDiscrete
        |> List.ofSeq
        |> List.map (fun x -> Convert.ToDouble x / Convert.ToDouble nNoGroup)

    let WSS =
        let da = List<float>(ws.Length)
        da.Add ws.Head
        for x = 1 to ws.Length - 1 do
            da.Add(da.[x - 1] + ws.[x])
        da

    let drawFrequencyPolygon =
        let graphs = Graphic("Frequency Polygons", "", "")
        graphs.SetPlane(0, frequenciesDiscrete.Count + 1, 0, 2, 0, (frequenciesDiscrete |> Seq.max |> Convert.ToInt32) + 2, 0, 4)
        graphs.DrawFrequencyPolygon(frequenciesDiscrete |> Array.ofSeq, true, "green")
        0

    let drawHistogram =
        let graphs = Graphic("Histogram", "", "")
        graphs.SetPlane(0, frequenciesDiscrete.Count + 1, 0, 2, 0, (frequenciesDiscrete |> Seq.max |> Convert.ToInt32) + 2, 0, 4)
        graphs.DrawBarsGraph(frequenciesDiscrete |> Array.ofSeq)
        0

    let drawFrequencyPolygon2 =
        let graphs = Graphic("Frequency Polygons", "", "")
        graphs.SetPlane(0, ws.Length + 1, 0, 2, 0, (ws |> Seq.max |> Convert.ToInt32) + 2, 0, 4)
        graphs.DrawFrequencyPolygon(WSS |> Array.ofSeq)
        0

    let drawHistograms =
        let graphs = Graphic("Histogram", "", "")
        graphs.SetPlane(0.0, Convert.ToDouble(ws.Length + 1), 0.0, 1.3, 0.0, 2.0, 0.0, 0.3)
        graphs.DrawBarsGraph(WSS |> Array.ofSeq)
        0