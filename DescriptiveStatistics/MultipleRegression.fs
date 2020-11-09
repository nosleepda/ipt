namespace DescriptiveStatistics

open System
open System.Collections.Generic
open MathNet.Numerics.Distributions
open MathNet.Numerics.LinearAlgebra
open MathNet.Numerics.LinearAlgebra.Double


module MultipleRegression =
    
    let ys = [4999.0; 6929.0; 6902.0; 10097.0; 8097.0; 11116.0; 4880.0; 7355.0; 10066.0; 7884.0]
    
    let xs1 = [5349.0; 6882.0; 7046.0; 7248.0; 5256.0; 14090.0; 3525.0; 5431.0; 7680.0; 8226.0;]
    
    let xs2 = [420.0; 553.0; 570.0; 883.0; 433.0; 839.0; 933.0; 526.0; 676.0; 684.0;]
    
    let xs3 = [331.0; 486.0; 498.0; 789.0; 359.0; 724.0; 821.0; 428.0; 607.0; 619.0;]
    
    let factors = [xs1; xs2; xs3]
    
    let n = ys.Length |> double
    
    let divN a x = x / (n - a) 
    
    let my =
        ys
        |> List.sum
        |> divN 0.0
    
    let m2y =
        ys
        |> List.map (fun y -> y ** 2.0)
        |> List.sum
        |> divN 0.0
        
    let sy = sqrt (m2y - my ** 2.0)
    
    let mxs =
        factors
        |> List.map (fun factor ->
           factor
           |> List.sum
           |> divN 0.0)
    
    let m2xs =
        factors
        |> List.map (fun factor ->
           factor
           |> List.map (fun x -> x ** 2.0)
           |> List.sum
           |> divN 0.0)
    
    let sxs =
        (m2xs, mxs)
        ||> List.map2 (fun x2 x -> sqrt (x2 - x ** 2.0))
    
    let yxs =
        factors
        |> List.map (fun factor ->
           (factor, ys)
           ||> List.map2 (fun x y -> x * y)
           |> List.sum
           |> divN 0.0)
    
    let ryxs =
        (yxs, mxs, sxs)
        |||> List.map3 (fun yx mx sx -> (yx - my * mx) / (sy * sx))
    
    let r2yxs = ryxs |> List.map (fun r -> r ** 2.0)
    
    let rxxIter (number:int) (factor:double list) =
        let result = List<double>(3)
        
        for i in number + 1 .. factors.Length - 1 do
            (factor, factors.[i])
            ||> List.map2 (fun currFact otherFact -> currFact * otherFact)
            |> List.sum
            |> divN 0.0
            |> (+) -(mxs.[i] * mxs.[number])
            |> (*) (1.0 / (sxs.[i] * sxs.[number]))
            |> result.Add
            
        result |> List.ofSeq
        
    let rxx =
        factors
        |> List.mapi (fun number factor -> rxxIter number factor)
        |> List.concat
    
    let xst = r2yxs |> List.map (fun r2 -> r2 * (n - 2.0) / (1.0 - r2) |> sqrt)
    
    let dadada =
        let tCrit = StudentT.InvCDF(0.0, 1.0, n - 2.0, 1.0 - 0.05 / 2.0)
        
        let dictExcluded = xst |> List.mapi (fun index x -> x <= tCrit)
        
        dictExcluded