namespace DescriptiveStatistics

open System
open MathNet.Numerics.Distributions
open MathNet.Numerics.LinearAlgebra
open MathNet.Numerics.LinearAlgebra.Double

module Analysis =
    
    let Xs =
        [6.0 .. 1.0 .. 20.0]
//        [1.94; 2.68; 3.47; 4.12; 4.77; 5.34; 5.85; 6.65]
//        [300.0 .. 100.0 .. 1000.0]
    
    let Ys =
        [13.0; 16.0; 15.0; 20.0; 19.0; 21.0; 26.0; 24.0; 30.0; 32.0; 30.0; 35.0; 34.0; 40.0; 39.0]
//        [0.82; 0.97; 1.06; 1.08; 1.1; 1.14; 1.21; 1.25]
//        [70.35; 75.38; 80.53; 85.81; 91.26; 96.83; 102.53; 108.27]
    
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
        StudentT.InvCDF(0.0, 1.0, n2, 1.0 - a / 2.0)
    
    let tV =
        abs r / sqrt (1.0 - r ** 2.0) * sqrt n2
        
    let norm a = Normal.InvCDF(0.0, 1.0, 1.0 - a / 2.0)
    
    let arth a =
        let l = Math.Atanh r - (norm a) / sqrt(n - 3.0) |> tanh
        let r = Math.Atanh r + (norm a) / sqrt(n - 3.0) |> tanh
        String.Format("Значимость коэффициента корреляции {0} < r < {1} ", l, r)
    
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
        String.Format("Доверительный интервал {0} < b < {1} ", b - t 0.05 * Sb, b + t 0.05 * Sb)
    
    let confidenceIntervalA =
        let Sa = S * sqrt (1.0 / n + meanX ** 2.0 / (n1 * sX ** 2.0))
        String.Format("Доверительный интервал {0} < a < {1} ", a - t 0.05 * Sa, a + t 0.05 * Sa)
    
    let f x = a + b * x
        
    let R2 =
        let temp = List.map2 (fun x y -> (y - f x) ** 2.0) Xs Ys |> List.sum
        List.map (fun y -> (y - meanY) ** 2.0) Ys |> List.sum |> (/) temp |> (-) 1.0
        
    let F a =
        let l = R2 * n2 / (1.0 - R2)
        let r = FisherSnedecor.InvCDF(1.0,n2,1.0 - a)
        let bool = if l > r then "" else "не"
        let bool2 = if l > r then ">" else "<"
        String.Format("Адекватность уравнения: уравнение лин. регрессии статистически {0} значимо, \nт.к. {1} {2} {3} ", bool, l, bool2, r )
        
    let Ys2 = List.map (fun x -> f x) Xs
        
    let drawCorrelationField =
        Graphic2.Plotly.draw2 Xs Ys Xs Ys2

        