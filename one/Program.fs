open System.Collections.Generic
open DescriptiveStatistics
open XPlot.Plotly

let data = List<double>[28.0; 30.0; 28.0; 27.0; 28.0; 29.0; 29.0; 29.0; 31.0; 28.0; 26.0; 25.0; 33.0;
35.0; 27.0; 31.0; 31.0; 30.0; 28.0; 33.0; 23.0; 30.0; 31.0; 33.0; 31.0; 27.0;
30.0; 28.0; 30.0; 29.0; 30.0; 26.0; 25.0; 31.0; 33.0; 26.0; 27.0; 33.0; 29.0;
30.0; 30.0; 36.0; 26.0; 25.0; 28.0; 30.0; 29.0; 27.0; 32.0; 29.0; 31.0; 30.0;
31.0; 26.0; 25.0; 29.0; 31.0; 33.0; 27.0; 32.0; 30.0; 31.0; 34.0; 28.0; 26.0;
38.0; 29.0; 31.0; 29.0; 27.0; 31.0; 30.0; 28.0; 34.0; 30.0; 26.0; 30.0; 32.0;
30.0; 29.0; 30.0; 28.0; 32.0; 30.0; 29.0; 34.0; 32.0; 35.0; 29.0; 27.0; 28.0;
30.0; 30.0; 29.0; 32.0; 29.0; 34.0; 30.0; 32.0; 24.0;
]
let trace1 =
    Scatter(
        x = [1; 2; 3; 4],
        y = [10; 15; 13; 17]
    )

let trace2 =
    Scatter(
        x = [2; 3; 4; 5],
        y = [16; 5; 11; 9]
    )


[<EntryPoint>]
let main argv =
    
//    Statistics.mode |> printfn "mode %f"
//    Statistics.m3 |> printfn "m3 %f"
//    Statistics.initElem |> printfn "initElem %f"
//    Statistics.lastElem |> printfn "lastElem %f"
//    Statistics.median |> printfn "median %f"
//    Statistics.skewness |> printfn "skewness %f"
//    Statistics.kurtosis|> printfn "kurtosis %f"
//    Statistics.coefficientVariation |> printfn "cv %f"
//    Statistics.expectedValueGroup |> printfn "mxG %f"
//    Statistics.varianceGroup |> printfn "dxG %f"
    Statistics.standardDeviation |> printfn "sG %f"
//    Statistics.confidenceIntervalDx 0.95 ||> printfn "chi %f < s < %f"
//    Statistics.confidenceIntervalMx 0.05 ||> printfn "CI %f < m < %f"
//    Statistics.nNoGroup |> printfn "n %i\n interval"
//    
//    Statistics.interval |> List.iter (printf "%1.6f ")
//    printfn "" |> ignore
//    Statistics.frequenciesInterval |> List.ofSeq |> List.iter (printf "%i ")
//    printfn "" |> ignore
//    
//    Statistics.frequenciesInterval |> List.ofSeq |> List.sum |> (printf "sum %i ")
//    printfn "" |> ignore
//    
//    Statistics.discrete |> List.iter (printf "%1.2f ")
//    printfn "" |> ignore
//    Statistics.frequenciesDiscrete |> List.ofSeq |> List.iter (printf "  %2i ")
//    printfn "" |> ignore
//    Statistics.frequenciesDiscrete |> List.ofSeq |> List.sum |> (printf "sum %i ")
//    printfn "" |> ignore
//
    
    Hypothesis.chiSquared 0.95 |> printfn "%s"
    Hypothesis.kolmogorov 0.05 |> printfn "%s"
    Hypothesis.romanovsky |> printfn "%s" 
    Hypothesis.yastremsky |> printfn "%s"
    Hypothesis.approximate 0.95 |> printfn "%s"
    
    printfn "" |> ignore
//    
    0
