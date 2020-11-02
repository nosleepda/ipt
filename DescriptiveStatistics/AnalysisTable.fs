namespace DescriptiveStatistics

open System
open MathNet.Numerics.Distributions
open MathNet.Numerics.LinearAlgebra
open MathNet.Numerics.LinearAlgebra.Double

module AnalysisTable =
    
    let xTitle =
        [15.0 .. 5.0 .. 40.0]
//        [5.0 .. 5.0 .. 25.0]
   
    let yTitle =
        [30.0 .. 10.0 .. 70.0]
//        [10.0 .. 13.0]
    
//    let ns = [[0.0; 0.0; 0.0; 1.0; 4.0]
//              [0.0; 3.0; 6.0; 4.0; 1.0]
//              [1.0; 3.0; 2.0; 0.0; 1.0]
//              [3.0; 0.0; 1.0; 0.0; 0.0]]

    let ns =
        [[3.0; 3.0; 0.0; 0.0; 0.0; 0.0;]
         [0.0; 5.0; 4.0; 0.0; 0.0; 0.0;]
         [0.0; 0.0; 8.0; 40.0; 2.0; 0.0;]
         [0.0; 0.0;5.0; 10.0; 6.0; 0.0;]
         [0.0; 0.0; 0.0; 4.0; 7.0; 3.0]]
    let nsArr = array2D ns

    let matrix = DenseMatrix.OfArray nsArr

    let ps =
        matrix.RowSums()
        |> List.ofSeq 
    
    let pys = List.map2 (fun p y -> p * y) ps yTitle
    
    let ws =
        matrix.ColumnSums()
        |> List.ofSeq 

    let wxs =
        List.map2 (fun p y -> p * y) ws xTitle
    
    let k = double xTitle.Length 
    
    let n = List.sum ps
    
    let n1 = n - 1.0
    
    let nInt = int n
    
    let mx =
        wxs
        |> List.sum
        |> (*) (1.0 / n)
    
    let my =
        pys
        |> List.sum
        |> (*) (1.0 / n)
    
    let wxmx2 =
        (xTitle, ws)
        ||> List.map2 (fun elem1 elem2 -> (elem1 - mx) ** 2.0 * elem2) 
        |> List.sum
    
    let pymy2 =
        (yTitle, ps)
        ||> List.map2 (fun elem1 elem2 -> (elem1 - my) ** 2.0 * elem2) 
        |> List.sum
        
    let sx =
        wxmx2 / n1    
        |> sqrt
    
    let sy =
        pymy2 / n1
        |> sqrt
    
    let ny =
        matrix.EnumerateColumns()
        |> List.ofSeq
        |> List.map (fun column -> List.ofSeq column
                                   |> List.map2 (fun elem1 elem2 -> elem1 * elem2) yTitle
                                   |> List.sum)
        
    let r =
        ny
        |> List.map2 (fun elem1 elem2 -> elem1 * elem2) xTitle
        |> List.sum
        |> (+) -(n * mx * my)
        |> (*) (1.0 / sqrt (wxmx2 * pymy2))
    
    let b =
        r * sy / sx    
    let a =
        my - mx * r * sy / sx
        
    let func x = a + b * x
                           
    let fxs =                        
        List.map (fun x -> func x) xTitle   
                
    let t =
        let r2 = r ** 2.0
        abs r * sqrt (n - 2.0) / sqrt (1.0 - r2)
    
    let p a =
        let norm a = Normal.InvCDF(0.0, 1.0, 1.0 - a / 2.0)
        let l = Math.Atanh r - (norm a) / sqrt(n - 3.0) |> tanh
        let r = Math.Atanh r + (norm a) / sqrt(n - 3.0) |> tanh
        (l,r)
    
    let R2 =
        let r1 =
            List.map2 (fun elem1 elem2 -> (elem1 - my) ** 2.0 * elem2) yTitle ps
            |> List.sum
            
        let r2 =
            fxs
            |> List.map2 (fun elem2 elem1 -> (elem1 - my) ** 2.0 * elem2) ws
            |> List.sum
            
        r2 / r1
    
    let nyws =
            ny
            |> List.map2 (fun elem1 elem2 -> elem2 / elem1) ws
    
    let s1 =
        matrix.EnumerateColumns()
        |> List.ofSeq
        |> List.mapi (fun index column -> List.ofSeq column
                                          |> List.map2 (fun y n -> (y - nyws.[index]) ** 2.0 * n) yTitle
                                          |> List.sum)
        |> List.sum
        |> (*) (1.0 / (n - k))
    
    let s2 =
        List.map (fun x -> func x) xTitle
        |> List.map3 (fun elem2 elem3 elem1 -> (elem1 - elem2) ** 2.0 * elem3) nyws ws
        |> List.sum
        |> (*) (1.0 / (k - 2.0))
    
    let f =
        s2 / s1
    
    let S =
        (fxs, nyws)
        ||> List.map2 (fun elem1 elem2 -> (elem1 - elem2) ** 2.0)
        |> List.sum
        |> (*) (1.0 / (n - 2.0))
        |> sqrt
        
    let st a =
        StudentT.InvCDF(0.0, 1.0, 28.0, 1.0 - a / 2.0)
    
    let confidenceIntervalB =
        let Sb = S / (sx * sqrt n1)
        String.Format("Доверительный интервал {0} < b < {1}", b - st 0.05 * Sb, b + st 0.05 * Sb)
    
    let confidenceIntervalA =
        let Sa = S * sqrt (1.0 / n + mx ** 2.0 / (n1 * sx ** 2.0))
        String.Format("Доверительный интервал {0} < a < {1}", a - st 0.05 * Sa, a + st 0.05 * Sa)
        
    let draw =
        Graphic2.Plotly.draw2 xTitle nyws xTitle fxs