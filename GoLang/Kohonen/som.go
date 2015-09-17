package main

import (
	"math"
    "time"
    "fmt"
    "math/rand"
)

func main() {
    rand.Seed( time.Now().UTC().UnixNano())

    fmt.Printf("Teste!")

}

// -----------------------------------Kohonen--------------------------------------------------------------------------------------
type Kohonen struct {
    outputs [][]Neuron
    iteration,length,dimenions int
    patterns [][]float64
}

func (r Kohonen) Create(l int, d int, f string) {
    r.length = l
    r.dimenions = d

    r.Initialise()
}

func (r Kohonen) Exec(f string) {
    r.LoadData(f)
    r.NormalisePatterns()
    r.Train(0.00000001)
    r.DumpCoordinates()
}

func (r Kohonen) Initialise() {
    r.outputs=make([][]Neuron,r.length)

    for i := 0; i < r.length; i++ {
        r.outputs[i] = make([]Neuron,r.length)
    }

    for i := 0; i < r.length; i++ {
        for j := 0; j < r.length; j++ {
            outputs[i][j].Create(i,j,r.length)
            outputs[i][j].Weigths = make([]float64,r.dimenions)

            for k := 0; k < r.dimenions; k++ {
                outputs[i][j].Weigths[k] = rand.Float64()
            }
        }
    }
}

func (r Kohonen) LoadData(f string) {
    
}

func (r Kohonen) NormalisePatterns() {
    
}

func (r Kohonen) Train(maxErro float64) {
    
}

func (r Kohonen) TrainPattern(patterns []float64) {
    
}

func (r Kohonen) DumpCoordinates() {
    
}

func (r Kohonen) Winner(patterns []float64) Neuron{
    var n Neuron

    return n
}

func (r Kohonen) Distance(v1 []float64,v2 []float64)  float64{
    return 0.0
}
// -----------------------------------FimKohonen-----------------------------------------------------------------------------------


// -----------------------------------Neuronio-------------------------------------------------------------------------------------
type Neuron struct {
    Weigths []float64
    RGB []int
    x,y,length int
    nf float64
}

func (r Neuron) Create(x int, y int, l int) {
    r.x = x
    r.y = y
    r.length = l

    dl:=float64(l)
    r.nf = 1000 / math.Log(dl)
}


func (r Neuron) Gauss(win Neuron, it int) float64 {
    dx:=float64(win.x - r.x)
    dy:=float64(win.y - r.y)

    distance:=math.Sqrt(math.Pow(dx, 2) + math.Pow(dy, 2))

    return math.Exp(-math.Pow(distance, 2) / math.Pow(r.Strength(it),2))
}

func (r Neuron) LearningRate(it int) float64 {
    dit:=float64(it)

    return math.Exp(-dit / 1000) * 0.1
}

func (r Neuron) Strength(it int) float64 {
    dit:=float64(it)
    dl:=float64(r.length)

    return math.Exp(-dit / r.nf) * dl
}

func (r Neuron) UpdateWeigths(pattern []float64,winner Neuron,it int) float64 {
    sum:=float64(0)

    for i := 0; i < r.length; i++ {
        delta:=r.LearningRate(it) * r.Gauss(winner, it) * (pattern[i] - r.Weigths[i])

        r.Weigths[i]+=delta

        //RGB[i] = 
        sum+=delta
    }
    dl:=float64(r.length)

    return sum / dl
}
// --------------------------------FimNeuronio--------------------------------------------------------------------------------------

