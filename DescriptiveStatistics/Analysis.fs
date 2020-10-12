namespace DescriptiveStatistics

open System
open System.Collections.Generic
open GraphicLibrary
open MathNet.Numerics.Distributions
open MathNet.Numerics.LinearAlgebra
open MathNet.Numerics.LinearAlgebra.Double

module Analysis =
    
    let Xs = [300.0 .. 100.0 .. 1000.0]
    
    let Ys = [70.35; 75.38; 80.53; 85.81; 91.26; 96.83; 102.53; 108.27]
    
    let n = Xs.Length |> float
    
    let n1 = n - 1.0
    
    let n2 = n - 2.0
    
    let div y = y / n
    
    let div1 y = y / n1
    
    let div2 y = y / n2
    
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
    
    let emperYX =
        let r1 = [|n; List.sum Xs; List.sum Xs; List.sumBy (fun x -> x ** 2.0) Xs|]
        let matrix = DenseMatrix(2,2,r1)
        let r2 = [|List.sum Ys; List.map2 (fun x y -> x * y) Xs Ys |> List.sum |] |> vector
        matrix.Solve(r2)
    
    let emperXY =
        let r1 = [|n; List.sum Xs; List.sum Xs; List.sumBy (fun x -> x ** 2.0) Xs|]
        let matrix = DenseMatrix(2,2,r1)
        let r2 = [|List.sum Ys; List.map2 (fun x y -> x * y) Xs Ys |> List.sum |] |> vector
        matrix.Solve(r2)
    
    let a = emperYX.[0]
    
    let b = emperYX.[1]
    
    let b1 =
        r * sX / sY
    
    let a1 =
        meanX - meanY * r * sX / sY
    
    let S = List.map2 (fun x y -> (y - a - b * x) ** 2.0) Xs Ys |> List.sum |> div2 |> sqrt
    
    let confidenceIntervalB =
        let Sb = S / (sX * sqrt n1)
        (b - t 0.05 * Sb, b + t 0.05 * Sb)
    
    let confidenceIntervalA =
        let Sa = S * sqrt (1.0 / n + meanX ** 2.0 / (n1 * sX ** 2.0))
        (a - t 0.05 * Sa, a + t 0.05 * Sa)
    
    let f x = a + b * x
        
    let R2 =
        let temp = List.map2 (fun x y -> (y - f x) ** 2.0) Xs Ys |> List.sum
        List.map (fun y -> (y - meanY) ** 2.0) Ys |> List.sum |> (/) temp |> (-) 1.0
        
    let F a =
        let r = R2 * n2 / (1.0 - R2)
        let l = FisherSnedecor.InvCDF(1.0,n2,1.0 - a)
        (r,l)
        
    let Ys2 = List.map (fun x -> f x) Xs
        
    let drawCorrelationField =
        let graphs = Graphic("Correlation field", "X", "Y")
        graphs.SetPlane(List.min Xs - 20.0, List.max Xs + 20.0, List.min Xs - 20.0, 100.0, List.min Ys - 10.0, List.max Ys + 10.0,List.min Ys - 10.0, 10.0)
        graphs.AddGraph(List<float> Xs, List<float> Ys,"red")
        graphs.AddGraph(List<float> Xs, List<float> Ys2,"green")
        graphs.DrawGraph(false)
        