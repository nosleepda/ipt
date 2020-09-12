namespace DescriptiveStatistics

open System
open System.Collections.Generic

module Statistics =
    let sourceData = List<double>[0.3;0.4;0.8;1.2;1.4;1.9;0.7;1.3;1.0;0.5;0.9;1.2;1.0;
                                  1.3;0.6;1.0;1.0;1.1;0.5;1.2;1.0;1.4;1.6;0.5;1.1; 1.1;
                                  1.8;0.3;0.6;1.1;0.8;1.2;0.9;1.4;1.3;1.6;1.5;0.8;
                                  0.7;0.9;1.5;1.3;1.1;1.2;1.8;1.1;1.0;1.2;0.9;1.5;1.3;
                                  1.1;1.2;1.3]
    
    let series = List<double>()
    
    let sortedData = sourceData |> List.ofSeq |> List.sort
    
    let R = (List.max sortedData - List.min sortedData)
    
    let k =
//        1.0 + 3.221 * log10 (Convert.ToDouble(sortedData.Length))
        sortedData.Length |> Convert.ToDouble |> sqrt |> round
    
    let h = R / k //Math.Round(R / k, 4) 
    
    let initElem = List.min sortedData - 0.5 * h
    let lastElem = List.max sortedData + 0.5 * h
    
    let interval = [initElem .. h .. lastElem]
    
    let frequencies =
        let mutable data = List<double> sortedData
        let frequencies = List<int>(List.init (interval.Length - 1) (fun _ -> 0))
        for i = 0 to interval.Length - 1   do
            while (data.Count > 0 && data.[0] < interval.[i+1]) do
                frequencies.[i] <- frequencies.[i] + 1
                data.RemoveAt(0)  
        frequencies
    
    let discrete = List.map2 (fun s e -> (s + e) / 2.0 ) (List.take (interval.Length - 1) interval ) interval.Tail
    
    let dictDa = Seq.map2 (fun d f -> d, f) discrete frequencies |> dict
    
    let vogue = dictDa.Values |> List.ofSeq |> List.max //todo neverno
    
    let median:double =
        let mutable m = 0.0
        if sortedData.Length % 2 = 0 then
            let kk = sortedData.Length / 2
            m <- (sortedData.Item kk + sortedData.Item (kk + 1)) / 2.0
        else
            let kk = (sortedData.Length - 1) / 2
            m <- sortedData.Item (kk + 1)
        m

//    let mx = 