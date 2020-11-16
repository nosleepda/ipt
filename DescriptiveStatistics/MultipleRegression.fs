namespace DescriptiveStatistics

open System
open System.Collections.Generic
open MathNet.Numerics.Distributions
open MathNet.Numerics.LinearAlgebra
open MathNet.Numerics.LinearAlgebra.Double


module MultipleRegression =
    
    let ys =
        [81.5; 82.3; 83.8; 83.1; 84.3; 82.6; 85.4; 84.6; 86.8; 88.3] //dimok
//        [4999.0; 6929.0; 6902.0; 10097.0; 8097.0; 11116.0; 4880.0; 7355.0; 10066.0; 7884.0] //exp
    
    let xs1 =
        [37.9; 36.5; 36.6; 38.2; 39.4; 39.8; 40.1; 41.5; 42.6; 45.7] //dimok
//        [5349.0; 6882.0; 7046.0; 7248.0; 5256.0; 14090.0; 3525.0; 5431.0; 7680.0; 8226.0] //exp
    
    let xs2 =
         [11.6; 11.5; 11.5; 11.8; 12.0; 12.2; 12.5; 12.6; 12.8; 13.2] //dimok
//        [420.0; 553.0; 570.0; 883.0; 433.0; 839.0; 933.0; 526.0; 676.0; 684.0;] //exp
    
    let xs3 =
        [9.5; 10.6; 7.8; 9.1; 13.6; 14.1; 14.6; 15.1; 16.0; 17.2] //dimok
//        [331.0; 486.0; 498.0; 789.0; 359.0; 724.0; 821.0; 428.0; 607.0; 619.0;] //exp
    
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
    
    let xst =
        r2yxs |> List.map (fun r2 -> r2 * (n - 2.0) / (1.0 - r2) |> sqrt)
    
    let student a = StudentT.InvCDF(0.0, 1.0, n - 2.0, 1.0 - a / 2.0)
    
    let includedFactors =
        let tCrit = student 0.05
        
        let excludedT = xst |> List.filter (fun x -> x <= tCrit)
        if excludedT.Length = 1 then
            [xst |> List.findIndex (fun x -> x > tCrit); xst |> List.findIndexBack (fun x -> x > tCrit)] |> List.sort
        elif excludedT.Length = 0 then
            let excludedRxx = rxx |> List.filter (fun x -> x >= 0.8)
            let mutable rxxi = 0
            if excludedRxx.Length = 1 then
                rxxi <- rxx |> List.findIndex (fun x -> x >= 0.8)
            else
                rxxi <- rxx |> List.findIndex (fun x -> x = List.max rxx)
                
            let mutable rx1 = 0
            let mutable rx2 = 0
            let mutable xIncluded = 0
            match rxxi with
            | 0 -> rx1 <- 0; rx2 <- 1; xIncluded <- 2
            | 1 -> rx1 <- 0; rx2 <- 2; xIncluded <- 1
            | 2 -> rx1 <- 1; rx2 <- 2; xIncluded <- 0
            | _ -> failwith "Error"
                
            if ryxs.[rx2] > ryxs.[rx1] then
                [xIncluded; rx2] |> List.sort
            else
                [xIncluded; rx1] |> List.sort
                
        else
            let ryxi = [xst |> List.findIndex (fun x -> x <= tCrit); xst |> List.findIndexBack (fun x -> x <= tCrit)]
            let rxxi = ryxi |> List.sum |> (+) -1
            
            if rxx.[rxxi] > 0.8 then
                if ryxs.[ryxi.[1]] > ryxs.[ryxi.[0]] then
                    [xst |> List.findIndex (fun x -> x > tCrit); ryxi.[1]] |> List.sort
                else
                    [xst |> List.findIndex (fun x -> x > tCrit); ryxi.[0]] |> List.sort
            else
                failwith "Error"
    
    let Ryxx =
        let iyx = includedFactors
        let ixx = includedFactors |> List.sum |> (+) -1
        
        (r2yxs.[iyx.[0]] + r2yxs.[iyx.[1]]
         - 2.0 * ryxs.[iyx.[0]] * ryxs.[iyx.[1]] * rxx.[ixx])
        / (1.0 - rxx.[ixx] ** 2.0)
        |> sqrt
        
    let Ryxx' =
        let k = double includedFactors.Length 
        sqrt (1.0 - (1.0 - Ryxx ** 2.0) * (n - 1.0) / (n - k - 1.0))
    
    let t a =
        let l = Ryxx' / (1.0 / sqrt (n - 1.0))
        let r = StudentT.InvCDF(0.0, 1.0, n - 3.0, 1.0 - a)
        let bool = if l > r then "" else "не "
        let bool2 = if l > r then ">" else "<"
        String.Format("Множественный коэффициент корреляции {0}значим, \nт.к. {1} {2} {3} ", bool, l, bool2, r)
    
    
    let coef =
        let x1 = factors.[includedFactors.[0]] |> List.sum
        let x12 = factors.[includedFactors.[0]] |> List.map (fun x -> x ** 2.0) |> List.sum
        let x2 = factors.[includedFactors.[1]] |> List.sum
        let x22 = factors.[includedFactors.[1]] |> List.map (fun x -> x ** 2.0) |> List.sum
        let y = ys |> List.sum
        let x1x2 = (factors.[includedFactors.[0]], factors.[includedFactors.[1]]) ||> List.map2 (fun x1 x2 -> x1 * x2) |> List.sum
        let yx2 = (ys, factors.[includedFactors.[1]]) ||> List.map2 (fun y x2 -> y * x2) |> List.sum
        let yx1 = (ys, factors.[includedFactors.[0]]) ||> List.map2 (fun y x2 -> y * x2) |> List.sum
        let list = [[x1; x2; n]; [x1x2; x22; x2]; [x12; x1x2; x1]]
        let m = DenseMatrix.OfArray (array2D list)
        let fs = [y; yx2; yx1] |> vector
        m.Solve fs |> List.ofSeq
    
    let F a =
       let l = (n - 3.0) * Ryxx ** 2.0 / (2.0 - 2.0 * Ryxx ** 2.0)
       let r = FisherSnedecor.InvCDF(2.0, n - 3.0, 1.0 - a)
       let bool = if l > r then "" else "не "
       let bool2 = if l > r then ">" else "<"
       String.Format("Эмпирические данные статистически {0}значимы, \nт.к. {1} {2} {3} ", bool, l, bool2, r)

    let R =
        Ryxx ** 2.0
        
    let error =
        (factors.[includedFactors.[0]], factors.[includedFactors.[1]])
        ||> List.map2 (fun x1 x2 -> coef.[0] * x1 + coef.[1] * x2 + coef.[2])
        |> List.map2 (fun y yexp -> abs (y - yexp) / y) ys
        |> List.sum
        |> (*) (1.0 / n)
    
    let K =
        let k1 = coef.[0] * mxs.[includedFactors.[0]] / my
        let k2 = coef.[1] * mxs.[includedFactors.[1]] / my
        [k1; k2]
        
    let draw =
        Graphic2.Plotly.drawMark xs1 ys "X1"
        Graphic2.Plotly.drawMark xs2 ys "X2"
        Graphic2.Plotly.drawMark xs3 ys "X3"