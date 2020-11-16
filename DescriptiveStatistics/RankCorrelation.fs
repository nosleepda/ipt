namespace DescriptiveStatistics

open System
open MathNet.Numerics.Distributions

module RankCorrelation =
    let h = 0
    
    let x =
        [96.0; 60.0; 74.0; 88.0; 99.0; 79.0; 72.0; 47.0; 83.0; 47.0; 93.0; 72.0; 98.0] //dimok 
//        [96.0; 60.0; 74.0; 88.0; 99.0; 79.0; 72.0; 47.0; 83.0; 47.0; 93.0] //kosta
//        [95.0; 90.0; 86.0; 84.0; 75.0; 70.0; 62.0; 60.0; 57.0; 50.0] //exp
    
    let y =
        [46.0; 76.0; 53.0; 98.0; 61.0; 78.0; 49.0; 72.0; 53.0; 53.0; 52.0; 89.0; 69.0] //dimok
//        [46.0; 76.0; 53.0; 98.0; 61.0; 78.0; 49.0; 72.0; 53.0; 53.0; 52.0] // kosta
//        [92.0; 93.0; 83.0; 80.0; 55.0; 60.0; 45.0; 72.0; 62.0; 70.0] // exp
    
    let n = double x.Length 

    let xRank =
//        x |> List.sortDescending |> List.mapi (fun i x -> i + 1)
        x
        |> List.mapi (fun i x -> (i + 1, x))
        |> dict
        |> Seq.sortByDescending (fun (KeyValue(k,v)) -> v)
        |> Seq.map (fun (KeyValue(k,v)) -> v)
        |> List.ofSeq
    let xRank2 =
//        x |> List.sortDescending |> List.mapi (fun i x -> i + 1)
        x
        |> List.mapi (fun i x -> (i + 1, x))
        |> dict
        |> Seq.sortByDescending (fun (KeyValue(k,v)) -> v)
        |> Seq.map (fun (KeyValue(k,v)) -> k)
        |> List.ofSeq
   
    let yRank =
        y
        |> List.mapi (fun i y -> (i + 1, y))
        |> dict
        |> Seq.sortByDescending (fun (KeyValue(k,v)) -> v)
        |> Seq.map (fun (KeyValue(k,v)) -> k)
        |> List.ofSeq
//    
//    let d = (xRank, yRank) ||> List.map2 (fun x y -> double (abs (x - y)) ** 2.0 ) |> List.sum
//    
//    let pb = 1.0 - 6.0 * d / (n ** 3.0 - n)
//    
//    let t a =
//        let l = pb
//        let r = StudentT.InvCDF(0.0, 1.0, n - 2.0, 1.0 - a) * sqrt ((1.0 - pb ** 2.0) / (n - 2.0))
//        let bool = if l > r then "" else "не "
//        let bool2 = if l > r then ">" else "<"
//        String.Format("Ранговая корреляционная связь {0}значимая, \nт.к. {1} {2} {3} ", bool, l, bool2, r)
//    
//    let R =
//        let mutable tmp = 0
//        let mutable yi = yRank.Tail
//        
//        for i in [0 .. yRank.Length - 2] do
//            for y in yi do
//                if yRank.[i] < y then
//                    tmp <- tmp + 1            
//            yi <- yi.Tail
//            
//        double tmp 
//        
//    let tb = 4.0 * R / (n * (n - 1.0)) - 1.0
//    
//    let da a =
//        let l = abs tb
//        let z = 1.96// f(1,96) = 0,475 = (1 - 0.05 )/ 2
//        let r =  z * sqrt ((2.0 * (2.0 * n + 5.0)) / ( 9.0 * n * (n - 1.0)))
//        let bool = if l > r then "" else "не "
//        let bool2 = if l > r then ">" else "<"
//        String.Format("Ранговая корреляционная связь {0}значимая, \nт.к. {1} {2} {3} ", bool, l, bool2, r)