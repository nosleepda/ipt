open System.Collections.Generic
open DescriptiveStatistics

let data = List<double>[28.0; 30.0; 28.0; 27.0; 28.0; 29.0; 29.0; 29.0; 31.0; 28.0; 26.0; 25.0; 33.0;
35.0; 27.0; 31.0; 31.0; 30.0; 28.0; 33.0; 23.0; 30.0; 31.0; 33.0; 31.0; 27.0;
30.0; 28.0; 30.0; 29.0; 30.0; 26.0; 25.0; 31.0; 33.0; 26.0; 27.0; 33.0; 29.0;
30.0; 30.0; 36.0; 26.0; 25.0; 28.0; 30.0; 29.0; 27.0; 32.0; 29.0; 31.0; 30.0;
31.0; 26.0; 25.0; 29.0; 31.0; 33.0; 27.0; 32.0; 30.0; 31.0; 34.0; 28.0; 26.0;
38.0; 29.0; 31.0; 29.0; 27.0; 31.0; 30.0; 28.0; 34.0; 30.0; 26.0; 30.0; 32.0;
30.0; 29.0; 30.0; 28.0; 32.0; 30.0; 29.0; 34.0; 32.0; 35.0; 29.0; 27.0; 28.0;
30.0; 30.0; 29.0; 32.0; 29.0; 34.0; 30.0; 32.0; 24.0;
]

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#! "
    //Statistics.initData data
    //Statistics.sortedData |> printfn "%A"
    Statistics.mode |> printfn "mode %f"
    Statistics.initElem |> printfn "initElem %f"
    Statistics.lastElem |> printfn "lastElem %f"
    Statistics.median |> printfn "median %f"
    Statistics.skewness |> printfn "skewness %f"
    Statistics.kurtosis|> printfn "kurtosis %f"
    Statistics.coefficientVariation |> printfn "cv %f"
    Statistics.expectedValueGroup |> printfn "mxG %f"
    Statistics.expectedValueNoGroup |> printfn "mxNG %f"
    Statistics.varianceGroup |> printfn "dxG %f"
    Statistics.varianceNoGroup |> printfn "dxNG %f"
    Statistics.standardDeviationGroup |> printfn "sG %f"
    Statistics.standardDeviationNoGroup |> printfn "sNG %f"
    Statistics.nNoGroup |> printfn "n %i\n interval \n"
    
    Statistics.interval |> List.iter (printf "%1.6f ")
    printfn "" |> ignore
    Statistics.frequenciesInterval |> List.ofSeq |> List.iter (printf "%i ")
    printfn "" |> ignore
    
    Statistics.frequenciesInterval |> List.ofSeq |> List.sum |> (printf "sum %i ")
    printfn "" |> ignore
    
    Statistics.discrete |> List.iter (printf "%1.2f ")
    printfn "" |> ignore
    Statistics.frequenciesDiscrete |> List.ofSeq |> List.iter (printf "  %2i ")
    Statistics.frequenciesDiscrete |> List.ofSeq |> List.sum |> (printf "sum %i ")
    printfn "" |> ignore
//    Statistics.drawFrequencyPolygon2 |> ignore
    
    
    0 // return an integer exit code
