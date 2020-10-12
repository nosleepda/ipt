namespace DescriptiveStatistics

open System
open System.Collections.Generic
open MathNet.Numerics.Distributions

module Analysis =
    
    let Xs = [300.0 .. 100.0 .. 1000.0]
    
    let Ys = [70.35; 75.38; 80.53; 85.81; 91.26; 96.83; 102.53; 108.27]
    
    let dXY =
        List.map2 (fun d f -> d, f) Xs Ys
        |> dict
    
    let n = Xs.Length |> float
    
    let n1 = n - 1.0
    
    let n2 = n - 2.0
    
    let div y = y / n
    
    let div1 y = y / n1 
    
    let meanX =
        List.sum Xs
        |> div
        
    let meanY =
        List.sum Ys
        |> div 
        
    let sX = 
        Xs
        |> List.map (fun x -> (x - meanX) ** 2.0)
        |> List.sum
        |> div1 
        |> sqrt
        
    let sY =
        Ys
        |> List.map (fun y -> (y - meanY) ** 2.0)
        |> List.sum
        |> div1 
        |> sqrt
    
    let r =
        let r1 = Xs |> List.map2 (fun elem1 elem2 -> elem1 * elem2) Ys |> List.sum |> (+) -(n * meanY * meanX)
        let r2 = Xs |> List.map (fun x -> (x - meanX) ** 2.0) |> List.sum |> sqrt
        let r3 = Ys |> List.map (fun y -> (y - meanY) ** 2.0) |> List.sum |> sqrt
        
        r1 / (r2 * r3)
    
    let t a =
        StudentT.InvCDF(0.0, 1.0, n2, 1.0 - a/2.0)
    
    let tV =
        abs r / sqrt (1.0 - r ** 2.0) * sqrt n2
        
    let norm a = Normal.InvCDF(0.0, 1.0, 1.0 - a / 2.0)
    
    let arth a =
        let l = Math.Atanh r - (norm a) / sqrt(n - 3.0) |> tanh
        let r = Math.Atanh r + (norm a) / sqrt(n - 3.0) |> tanh
        (l,r)
    
    