namespace Graphic2

open XPlot.Plotly

module Plotly =
    
    let draw r1 r2 = Scatter (x = r1, y = r2) |> Chart.Plot |> Chart.WithWidth 700 |> Chart.WithHeight 500 |> Chart.Show
    
    let draw2 r1 r2 r3 r4 =
        [Scatter (x = r1, y = r2, line = Line(shape = "spline", smoothing = 1.0))
         Scatter (x = r3, y = r4, line = Line(shape = "spline", smoothing = 1.0))]
        |> Chart.Plot
        |> Chart.WithWidth 700
        |> Chart.WithHeight 500
        |> Chart.Show
    
    let draw22 r1 r2 =
        [Scatter (y = r1, line = Line(shape = "spline", smoothing = 1.0))
         Scatter (y = r2, line = Line(shape = "spline", smoothing = 1.0))]
        |> Chart.Plot
        |> Chart.WithWidth 700
        |> Chart.WithHeight 500
        |> Chart.Show
    
    let drawHistogram discrete frequenciesDiscrete=
        [Bar(x = discrete, y = frequenciesDiscrete):> Trace
         Scatter(x = discrete, y = frequenciesDiscrete, line = Line(shape = "spline", smoothing = 1.0)):> Trace]
        |> Chart.Plot
        |> Chart.WithHeight 500
        |> Chart.WithWidth 700
        |> Chart.Show
    
    let drawDistributionFunction cumulate =
        [Bar(y = cumulate):> Trace;
        Scatter(y = cumulate, line = Line(shape = "spline", smoothing = 1.0)):> Trace]
        |> Chart.Plot
        |> Chart.WithHeight 500
        |> Chart.WithWidth 700
        |> Chart.Show   
