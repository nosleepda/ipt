open DescriptiveStatistics
open MathNet.Numerics.Distributions
open MathNet.Numerics.Distributions



[<EntryPoint>]
let main argv =
    
    NonlinearDependenciesTable.mx |> (printfn "mean x %1.2A")
    NonlinearDependenciesTable.my |> (printfn "mean y %1.2A")
    NonlinearDependenciesTable.sx |> (printfn "sx %1.2A")
    NonlinearDependenciesTable.sy |> (printfn "sy %1.2A")
    NonlinearDependenciesTable.nu |> (printfn "Nu %1.2A")
    NonlinearDependenciesTable.R2 |> (printfn "R2 %1.2A")
    NonlinearDependenciesTable.F 0.05 |> (printfn " %1.2A")
    
//    NonlinearDependencies.interpol ||> List.iter2 (printf " %f %f \n")
//    NonlinearDependencies.fxs |> List.iter (printf " %f \n")
//    NonlinearDependencies.xfs |> List.iter (printf " %1.2f")
//    printfn ""
//    NonlinearDependencies.yfs |> List.iter (printf " %1.2f")
//    printfn ""
//    
//    NonlinearDependencies.my |> (printfn "mean y =  %f ")
//    NonlinearDependencies.mx |> (printfn "mean x =  %f ")
//    NonlinearDependencies.sy |> (printfn "sy =  %f ")
//    NonlinearDependencies.sx |> (printfn "sx = %f ")
//    NonlinearDependencies.i |> (printfn "i = %f ")
//    NonlinearDependencies.funcRegressionPrint |> ignore
//    NonlinearDependencies.R2 |> (printfn  "R2 = %f ")
//    NonlinearDependencies.F 0.05 |> (printfn "%s")
    
//    AnalysisTable.nyws |> List.iter (printf " %1.2f")
//    NonlinearDependenciesTable.numberOfFunc |> List.iter (printf " %1.2f")


//    NonlinearDependenciesTable.funcRegressionPrint// |> (printf " %A")

    
//    AnalysisTable.confidenceIntervalA |> printfn "\n  %A"
//    AnalysisTable.confidenceIntervalB |> printfn "\n  %A"
//    AnalysisTable.my |> printfn "\n my %A"
//    AnalysisTable.mx |> printfn "\n mx %A"
//    AnalysisTable.sx |> printfn "\n sx %A"
//    AnalysisTable.sy |> printfn "\n  sy %A"
//    AnalysisTable.r |> printfn "\n r  %A"
//    AnalysisTable.t |> printfn "\n t vib  %A"
//    StudentT.InvCDF(0.0, 1.0, 28.0, 1.0 - 0.05 / 2.0) |> printfn "\n t krit  %A"
//
//    AnalysisTable.p 0.05 |> printfn "\n значимость r %A"
//    AnalysisTable.R2 |> printfn "\n R2  %A"
//    AnalysisTable.f  |> printfn "\n f vib %A"
//    FisherSnedecor.InvCDF(3.0,25.0,0.95) |> printfn "\n d krit %A"
//    AnalysisTable.a |> printfn "\n a  %A"
//    AnalysisTable.b |> printfn "\n b %A"
//    AnalysisTable.nyws |> List.iter (printf " %1.4f ")
//    
//    AnalysisTable.draw
    
    
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
