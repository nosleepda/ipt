namespace DescriptiveStatistics

open System
open System.Collections.Generic
open GraphicLibrary

module Statistics =
    let sourceData =
        List<double>
            [ 0.3
              0.4
              0.8
              1.2
              1.4
              1.9
              0.7
              1.3
              1.0
              0.5
              0.9
              1.2
              1.0
              1.3
              0.6
              1.0
              1.0
              1.1
              0.5
              1.2
              1.0
              1.4
              1.6
              0.5
              1.1
              1.1
              1.8
              0.3
              0.6
              1.1
              0.8
              1.2
              0.9
              1.4
              1.3
              1.6
              1.5
              0.8
              0.7
              0.9
              1.5
              1.3
              1.1
              1.2
              1.8
              1.1
              1.0
              1.2
              0.9
              1.5
              1.3
              1.1
              1.2
              1.3 ]

    let initData data =
        sourceData.Clear |> ignore
        sourceData.AddRange data

    let series = List<double>()

    let sortedData = sourceData |> List.ofSeq |> List.sort

    let R =
        (List.max sortedData - List.min sortedData)

    let k =
        sortedData.Length
        |> Convert.ToDouble
        |> sqrt
        |> round

    let h = R / k

    let initElem = List.min sortedData - 0.5 * h
    let lastElem = List.max sortedData + 0.5 * h

    let interval = [ initElem .. h .. lastElem ]

    let frequencies =
        let mutable data = List<double> sortedData

        let frequencies =
            List<int>(List.init (interval.Length - 1) (fun _ -> 0))

        for i = 0 to interval.Length - 1 do
            while (data.Count > 0 && data.[0] < interval.[i + 1]) do
                frequencies.[i] <- frequencies.[i] + 1
                data.RemoveAt(0)
        frequencies

    let discrete =
        List.map2 (fun s e -> (s + e) / 2.0) (List.take (interval.Length - 1) interval) interval.Tail

    let nGroup = discrete.Length

    let nNoGroup = sortedData.Length

    let dictDa =
        Seq.map2 (fun d f -> d, f) discrete frequencies
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
            sum <- sum + (x.Key ** 2.0) * Convert.ToDouble x.Value

        sum <- sum / Convert.ToDouble(if nNoGroup > 50 then nNoGroup - 1 else nNoGroup)
        sum - expectedValueGroup ** 2.0

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
            sum <- sum + (x.Key ** 3.0) * Convert.ToDouble x.Value

        sum <- sum / Convert.ToDouble nNoGroup
        sum - expectedValueGroup ** 3.0

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
        frequencies
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
        graphs.SetPlane(0, frequencies.Count + 1, 0, 2, 0, (frequencies |> Seq.max |> Convert.ToInt32) + 2, 0, 4)
        graphs.DrawFrequencyPolygon(frequencies |> Array.ofSeq, true, "green")
        0

    let drawHistogram =
        let graphs = Graphic("Histogram", "", "")
        graphs.SetPlane(0, frequencies.Count + 1, 0, 2, 0, (frequencies |> Seq.max |> Convert.ToInt32) + 2, 0, 4)
        graphs.DrawBarsGraph(frequencies |> Array.ofSeq)
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