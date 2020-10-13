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
    
//    Statistics.mode |> printfn "mode %f"
//    Statistics.m3 |> printfn "m3 %f"
//    Statistics.initElem |> printfn "initElem %f"
//    Statistics.lastElem |> printfn "lastElem %f"
//    Statistics.median |> printfn "median %f"
//    Statistics.skewness |> printfn "skewness %f"
//    Statistics.kurtosis|> printfn "kurtosis %f"
//    Statistics.coefficientVariation |> printfn "cv %f"
//    Statistics.mean |> printfn "mxG %f"
//    Statistics.variance |> printfn "dxG %f"
//    Statistics.standardDeviation |> printfn "sG %f"
//    Statistics.confidenceIntervalMean 0.95 ||> printfn "chi %f < s < %f"
//    Statistics.confidenceIntervalVariance 0.05 ||> printfn "CI %f < m < %f"
//    Statistics.nNoGroup |> printfn "n %i\n"
//    
//    printfn "\nStatistics.discrete" |> ignore
//    Statistics.discrete |> List.iter (printf " %1.6f ")
//    printfn "\nStatistics.frequenciesDiscrete" |> ignore
//    Statistics.frequenciesDiscrete |> List.ofSeq |> List.iter (printf "%i ")
//    printfn "\nHypothesis.frequenciesDiscreteShort" |> ignore
//    
//    Hypothesis.frequenciesDiscreteShort |> List.ofSeq |> List.iter (printf "%i ")
//    printfn "\nHypothesis.intervalShort" |> ignore
//    Hypothesis.intervalShort |> List.ofSeq |> List.iter (printf "%f ")
////    printfn "" |> ignore
////    Hypothesis.isShort.IsSome |> (printf "sss %A ")
//    printfn "" |> ignore
//    Hypothesis.chiSquared 0.95 |> printfn "%s"
//    Hypothesis.kolmogorov 0.05 |> printfn "%s"
//    Hypothesis.romanovsky |> printfn "%s" 
//    Hypothesis.yastremsky |> printfn "%s"
//    Hypothesis.approximate 0.95 |> printfn "%s"
//    
    
    printfn "Коэффициент корреляции %A" Analysis.r
    

    printfn "Средняя стоимость фондов (Х) %f" Analysis.meanX
    printfn "Средняя стоимость тов. продукции (Y) %f" Analysis.meanY
    printfn "Sx %f" Analysis.sX
    printfn "Sy %f" Analysis.sY
    printfn "t %f > %f" Analysis.tV (Analysis.t 0.05)
    printfn "%s" (Analysis.arth 0.05)
    printfn "Уравнение регресси Y на Х y = %f + %f * x" Analysis.a Analysis.b
    printfn "Уравнение регресси X на Y x = %f + %f * y" Analysis.a1 Analysis.b1
    printfn "%s" Analysis.confidenceIntervalB
    printfn "%s" Analysis.confidenceIntervalA
    printfn "Коэффициент детерминации R2 %f" Analysis.R2
    printfn "%s" (Analysis.F 0.05)
    Analysis.drawCorrelationField
    
    
    

    
    0
