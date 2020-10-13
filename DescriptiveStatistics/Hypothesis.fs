namespace DescriptiveStatistics

open System
open System.Collections.Generic
open GraphicLibrary
open MathNet.Numerics.Distributions

module Hypothesis =
    
    let frequenciesDiscreteShort =
        let result = List<int>()
        let mutable temp = 0
        for i = 0 to Statistics.frequenciesDiscrete.Count - 1 do
            temp <- temp + Statistics.frequenciesDiscrete.[i]
            if temp >= 5 then
                result.Add temp
                temp <- 0
        if temp <> 0 then
            result.[result.Count - 1] <- result.[result.Count - 1] + temp
        List.ofSeq result
    
    let intervalShort =
        let interval = Statistics.interval
        let freq = Statistics.frequenciesInterval
        let result = List<double>[Double.MinValue]
        let mutable temp = 0
        let mutable flg = true
        for i = 1 to Statistics.frequenciesInterval.Count - 1 do
            flg <- true
            temp <- temp + freq.[i]
            if temp >= 5 then
                result.Add interval.[i]
                flg <- false
                temp <- 0
        if flg then
            result.[result.Count - 1] <- Double.MaxValue
        else
            result.Add Double.MaxValue

        List.ofSeq result
    
    let frequenciesDiscreteExp =
        let us = List.map (fun elem -> (elem - Statistics.mean) / Statistics.standardDeviation) Statistics.discrete
        let value1 = 1.0 / sqrt (2.0 * Math.PI)
        let value2 = float Statistics.nNoGroup * Statistics.h / Statistics.standardDeviation
        
        let result = List.map (fun elem -> value1 * exp (elem ** 2.0 / -2.0)) us
                     |> List.map (fun elem -> value2 * elem)
                     |> List.map (fun elem -> Math.Round elem)
                     |> List.map (fun elem -> int elem)
                     |> List<int>
                     
//        result.[result.Count - 1] <- result.[result.Count - 1] + 1
        List.ofSeq result
        
    let isShort =
       List.ofSeq Statistics.frequenciesDiscrete
       |> List.tryFind (fun x -> x < 5)
    
    let frequenciesDiscreteDouble = List.ofSeq Statistics.frequenciesDiscrete |> List.map (fun elem -> double elem)
    
    let frequenciesDiscreteExpDouble = List.map (fun elem -> double elem) frequenciesDiscreteExp
    let frequenciesDiscreteShortDouble = List.map (fun elem -> double elem) frequenciesDiscreteShort
    
    let freedom = (frequenciesDiscreteExp.Length - 2 - 1) |> double
    
    let freedomShort = (frequenciesDiscreteShort.Length - 2 - 1) |> double
    
    let ps =
        let p a b = abs (Normal.CDF(Statistics.mean, Statistics.standardDeviation, a) - Normal.CDF(Statistics.mean, Statistics.standardDeviation,b))
        let result = List<double>()
        for i = 1 to intervalShort.Length - 1 do
            result.Add (p intervalShort.[i-1] intervalShort.[i])
        List.ofSeq result
        
    let chiSquaredShort =
        List.map (fun elem -> elem * double Statistics.nNoGroup) ps
        |> List.map2 (fun elem1 elem2 -> (elem1 - elem2) ** 2.0 / elem2) frequenciesDiscreteShortDouble
        |> List.sum
    
    let chiSquaredFull =
        List.map2 (fun n n' -> (n - n') ** 2.0 / n') frequenciesDiscreteDouble frequenciesDiscreteExpDouble
        |> List.sum
        
    let chiSquared a =
        let f = if isShort.IsSome then freedomShort  else freedom
        let cs = if isShort.IsSome then chiSquaredShort else chiSquaredFull
        let chiSquaredCritical = ChiSquared.InvCDF(f, a) 
        
        let bool =
            if (cs >= chiSquaredCritical) then "не " else ""
            
        String.Format("Условие критерия Пирсона {0} {1} < {2} ", bool + "выполняется", cs, chiSquaredCritical)
    
    let kolmogorov a =
        let cumulative (list:list<int>) =
            let result = List<int>[list.Item 0]
            for i = 1 to list.Length - 1 do
                result.Add (result.[i - 1] + (list.Item i))
            result |> List.ofSeq
            
        let funcK a =
            [-1000.0 .. 1000.0]
            |> List.map (fun elem -> ((-1.0) ** elem) * exp (-2.0 * (elem ** 2.0) * (a ** 2.0)))
            |> List.sum
            |> (fun x -> 1.0 - x)
            
        let c1 = cumulative (List.ofSeq Statistics.frequenciesDiscrete)
        let c2 = cumulative (List.ofSeq frequenciesDiscreteExp)
        let max = List.map2 (fun elem1 elem2 -> abs (elem1 - elem2)) c1 c2 |> List.max |> double
        let k = max / sqrt (double Statistics.nNoGroup) |> funcK
        let bool =
            if (k > a) then "" else "не "
        String.Format("Условие критерия Колмогорова {0} {1} > {2} ", bool + "выполняется", k, a)
       
    let romanovsky =
        let f = if isShort.IsSome then freedomShort  else freedom
        let cs = if isShort.IsSome then chiSquaredShort else chiSquaredFull
        
        let r = abs (cs - f) / sqrt (2.0 * f)
        let bool =
            if (r > 3.0) then "не " else ""
        String.Format("Условие критерия Романовского {0} {1} < 3 ", bool + "выполняется", r)
        
    let yastremsky =
        let cs = List.map (fun elem -> elem * double Statistics.nNoGroup) ps
                |> List.map2 (fun elem1 elem2 -> (elem1 - elem2) ** 2.0 / (elem2 * (1.0 - elem2 / double Statistics.nNoGroup))) frequenciesDiscreteShortDouble
                |> List.sum
        
        let ss = List.map2 (fun elem1 elem2 -> (elem1 - elem2) ** 2.0) frequenciesDiscreteDouble frequenciesDiscreteExpDouble
        
        let cf = List.map (fun elem -> elem / double Statistics.nNoGroup) frequenciesDiscreteExpDouble
                |> List.map (fun elem -> 1.0 - elem)
                |> List.map2 (fun elem1 elem2 -> elem1 * elem2) frequenciesDiscreteExpDouble
                |> List.map2 (fun elem1 elem2 -> (elem1 / elem2)) ss
                |> List.sum
        
        let k =  if isShort.IsSome then frequenciesDiscreteShort.Length else frequenciesDiscreteExp.Length 
        let c = if isShort.IsSome then cs else cf
        let t = 0.6       
        let j = abs (c - double k) / sqrt (2.0 * double k + 4.0 * t)
        
        let bool =
            if (j > 3.0) then "не " else ""
        String.Format("Условие критерия Ястремского {0} {1} < 3 ", bool + "выполняется", j)
    
    let approximate a =
        let n = double Statistics.nNoGroup
        let sas = sqrt (6.0 * (n - 1.0) / ((n + 1.0) * (n + 3.0)))
        let sex = sqrt ( 24.0 * n * (n - 2.0) * (n - 3.0) / (((n - 1.0) ** 2.0) * (n + 3.0) * n + 5.0))
        let bool1 = (abs Statistics.skewness <= sas) && (abs Statistics.kurtosis <= sex) 
        
        let chi =
            Statistics.skewness ** 2.0 / sas ** 2.0 + Statistics.kurtosis ** 2.0 / sex ** 2.0
        let chiCritical = ChiSquared.InvCDF(2.0, a) 
        
        let bool2 =
            if bool1 && (chi < chiCritical) then "" else "не "
            
        String.Format("Условие приближенного критерия {0} {1} < {2} ", bool2 + "выполняется", chi, chiCritical)

    let drawFrequencyPolygon =
        let graphs = Graphic("Frequency Polygons", "", "")
        graphs.SetPlane(0, Statistics.frequenciesDiscrete.Count + 1, 0, 2, 0, (Statistics.frequenciesDiscrete |> Seq.max |> int) + 2, 0, 4)
        graphs.DrawFrequencyPolygon(Statistics.frequenciesDiscrete |> Array.ofSeq, true, "green")
        0
    
    let drawFrequencyExpPolygon =
        let graphs = Graphic("Frequency Polygons", "", "")
        graphs.SetPlane(0, frequenciesDiscreteExp.Length + 1, 0, 2, 0, (frequenciesDiscreteExp |> Seq.max |> int) + 2, 0, 4)
        graphs.DrawFrequencyPolygon(frequenciesDiscreteExp |> Array.ofSeq, false, "red")
        0        