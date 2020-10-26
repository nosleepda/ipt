open DescriptiveStatistics
open MathNet.Numerics.Distributions
open MathNet.Numerics.Distributions



[<EntryPoint>]
let main argv =
    
    AnalysisTable.nyws |> List.iter (printf "%1.4A ")
    
    AnalysisTable.confidenceIntervalA |> printfn "\n  %A"
    FisherSnedecor.InvCDF(3.0,25.0,0.95) |> printfn "\n  %A"
    AnalysisTable.a |> printfn "\n  %A"
    StudentT.InvCDF(0.0, 1.0, 28.0, 1.0 - 0.05 / 2.0) |> printfn "\n  %A"
//    Statistics.median |> printfn "median %f"
//    Statistics.skewness |> printfn "skewness %f"
//    Statistics.kurtosis|> printfn "kurtosis %f"
//    Statistics.coefficientVariation |> printfn "cv %f"
//    Statistics.mean |> printfn "mxG %f"
//    Statistics.variance |> printfn "dxG %f"
//    Statistics.standardDeviation |> printfn "sG %f"
//    Statistics.confidenceIntervalMean 0.95 ||> printfn "chi %f < s < %f"
//    Statistics.confidenceIntervalVariance 0.05 ||> printfn "CI %f < m < %f"
//    
//    Statistics.discrete |> List.ofSeq |> List.iter (printf " %1.0f ")
//    printfn ""
//    Statistics.frequenciesDiscrete |> List.ofSeq |> List.iter (printf " %2i ")
//    
//    Statistics.draw
//    
//    Hypothesis.intervalShort |> List.ofSeq |> List.iter (printf "%f ")
//    printfn ""
//    Hypothesis.frequenciesDiscreteShort |> List.ofSeq |> List.iter (printf "%i ")
//    printfn "" 
//    
//    Hypothesis.chiSquared 0.95 |> printfn "%s"
//    Hypothesis.kolmogorov 0.05 |> printfn "%s"
//    Hypothesis.romanovsky |> printfn "%s" 
//    Hypothesis.yastremsky |> printfn "%s"
//    Hypothesis.approximate 0.95 |> printfn "%s"
//    Hypothesis.draw 

//    printfn "Коэффициент корреляции %A" Analysis.r
//    printfn "Средний (Х) %f" Analysis.meanX
//    printfn "Средняя (Y) %f" Analysis.meanY
//    printfn "Sx %f" Analysis.sX
//    printfn "Sy %f" Analysis.sY
//    printfn "t %f > %f" Analysis.tV (Analysis.t 0.05)
//    printfn "%s" (Analysis.arth 0.05)
//    printfn "Уравнение регресси Y на Х y = %f + %f * x" Analysis.a Analysis.b
//    printfn "Уравнение регресси X на Y x = %f + %f * y" Analysis.a1 Analysis.b1
//    printfn "%s" Analysis.confidenceIntervalB
//    printfn "%s" Analysis.confidenceIntervalA
//    printfn "Коэффициент детерминации R2 %f" Analysis.R2
//    printfn "%s" (Analysis.F 0.05)
//    Analysis.drawCorrelationField
    

    0
