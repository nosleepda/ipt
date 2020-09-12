open System.Collections.Generic
open DescriptiveStatistics

let data = List<double>[0.3;0.4;0.8;1.2;1.4;1.9;0.7;1.3;1.0;0.5;0.9;1.2;1.0;
1.3;0.6;1.0;1.0;1.1;0.5;1.2;1.0;1.4;1.6;0.5;1.1; 1.1;
1.8;0.3;0.6;1.1;0.8;1.2;0.9;1.4;1.3;1.6;2.7;1.5;0.8;
0.7;0.9;1.5;1.3;1.1;1.2;1.8;1.1;1.0;1.2;0.9;1.5;1.3;
1.1;1.2;1.3]

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#! "
   
    Statistics.sortedData |> printfn "%A"
    Statistics.vogue |> printfn "%i"    
//    Statistics.k |> printfn "%f"
//    Statistics.h |> printfn "%f"
//    Statistics.initElem |> printfn "%f"
//    Statistics.lastElem |> printfn "%f"
//    Statistics.sortedData.Length |> printfn "%i"
//    Statistics.interval |> List.iter (printf "%1.2f ")
//    printfn "" |> ignore
//    Statistics.frequencies |> List.ofSeq |> List.iter (printf "  %2i ")
//    printfn "\n----------------------------------------------" |> ignore
//    Statistics.discrete |> List.iter (printf "%1.2f ")
//    printfn "" |> ignore
//    Statistics.frequencies |> List.ofSeq |> List.iter (printf "  %2i ")
//    
//    Statistics.frequencies |> Seq.sum |> printfn "\n %i"
    //Statistics.calcFreq
    
    0 // return an integer exit code
