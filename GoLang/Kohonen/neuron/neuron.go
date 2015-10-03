package kohonen

import (
    "math"
)

type Neuron struct {
    Weights []float64
    RGB []int
    X,Y,Length int
    nf float64
    maxint float64
    maxvar float64
}

func (r Neuron) Create(x int, y int, l int) Neuron{
    r.X = x
    r.Y = y
    r.Length = l
    r.maxint = 1000
    r.maxvar = 0.1

    dl:=float64(l)

    log:=math.Log(dl)
    r.nf = r.maxint / log
    
    return r
}

func (r Neuron) Gauss(win Neuron, it int) float64 {
    dx:=float64(win.X - r.X)
    dy:=float64(win.Y - r.Y)

    distance:=math.Sqrt(math.Pow(dx, 2) + math.Pow(dy, 2))

    return math.Exp(-math.Pow(distance, 2) / math.Pow(r.Strength(it),2))
}

func (r Neuron) LearningRate(it int) float64 {
    dit:=float64(it)

    return math.Exp(-dit / r.maxint) * r.maxvar // 1000 tem que ser constrante
}

func (r Neuron) Strength(it int) float64 {
    dit:=float64(it)
    dl:=float64(r.Length)
    return math.Exp(-dit / r.nf) * dl
}

func (r Neuron) UpdateWeigths(pattern []float64,winner Neuron,it int) (float64,Neuron) {
    sum:=float64(0)
    
    for i := 0; i < len(r.Weights); i++ {
        delta:=r.LearningRate(it) * r.Gauss(winner, it) * (pattern[i] - r.Weights[i])

        r.Weights[i]+=delta

        r.RGB[i] = int((r.Weights[i] * 255))

        if r.RGB[i] > 255{
            r.RGB[i] = 255
        }

        sum+=delta
    }

    dl:=float64(r.Length)

    return sum / dl, r
}