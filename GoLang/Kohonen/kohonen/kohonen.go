package kohonen

import (
    neu "../neuron"
    "os"
    "math"
    "math/rand"
    "image"
    "image/color"
    "image/draw"
    "image/png"
    "fmt"
    "time"
)

var (
    white color.Color = color.RGBA{255, 255, 255, 255}
    black color.Color = color.RGBA{0, 0, 0, 255}

    red  color.Color = color.RGBA{255, 0, 0, 255}
    green  color.Color = color.RGBA{0, 255, 0, 255}
    blue  color.Color = color.RGBA{0, 0, 255, 255}
)

type Kohonen struct {
    Outputs [][]neu.Neuron

    maxiteration int
    iteration, length, dimensions, Numlines, NumResults int

    Patterns,Result [][]float64
    Labels []string
    maxnormal []float64
}

func (r Kohonen) Create(l int, d int,i int) Kohonen{
    r.length = l
    r.dimensions = d
    r.iteration = 1
    r.maxiteration = i
    
    rand.Seed(time.Now().Unix())
    r = r.Initialise()

    return r
}

func (r Kohonen) Draw(){
    Screen := image.NewRGBA(image.Rect(0, 0, r.length, r.length))
    draw.Draw(Screen, Screen.Bounds(), &image.Uniform{white}, image.ZP, draw.Src)

    for i := 0; i < r.length; i++ {
        for j := 0; j < r.length; j++ {
            red:=uint8(r.Outputs[i][j].RGB[0])
            green:=uint8(r.Outputs[i][j].RGB[1])
            blue:=uint8(r.Outputs[i][j].RGB[2])

            Screen.Set(i, j, color.RGBA{red, green, blue, 255})
        }
    }
    After, _ := os.Create("After.png")
    png.Encode(After, Screen)
    After.Close()
}

func (r Kohonen) Initialise() Kohonen{
    r.Outputs=make([][]neu.Neuron,r.length)

    for i := 0; i < r.length; i++ {
        r.Outputs[i] = make([]neu.Neuron,r.length)
    }

    Screen := image.NewRGBA(image.Rect(0, 0, r.length, r.length))
    draw.Draw(Screen, Screen.Bounds(), &image.Uniform{white}, image.ZP, draw.Src)

    for i := 0; i < r.length; i++ {
        for j := 0; j < r.length; j++ {

            neu:=r.Outputs[i][j]
            neu = neu.Create(i,j,r.length,r.maxiteration)

            neu.Weights = make([]float64,r.dimensions)
            neu.ExitWeights = make([]float64, r.NumResults)
            neu.RGB = make([]int,r.dimensions)

            for k := 0; k < r.dimensions; k++ {
                neu.Weights[k] = rand.Float64()
                neu.RGB[k] = int((neu.Weights[k] * 255))
            }

            for k := 0; k < r.NumResults; k++ {
                neu.ExitWeights[k] = rand.Float64();
            }

            red:=uint8(neu.RGB[0])
            green:=uint8(neu.RGB[1])
            blue:=uint8(neu.RGB[2])

            Screen.Set(i, j, color.RGBA{red, green, blue, 255})
            r.Outputs[i][j] = neu
        }
    }
    Before, _ := os.Create("Before.png")
    png.Encode(Before, Screen)
    Before.Close()

    return r
}

func (r Kohonen) NormalisePatterns() Kohonen{
    r.maxnormal = make([]float64,r.dimensions)

    for j := 0; j < r.dimensions; j++ {
        max:=float64(0)
        for _, num := range r.Patterns {
            
            if(max < num[j]){
                max = num[j]
            }
        }
        r.maxnormal[j] = max
        for i := 0; i < r.Numlines; i++ {
            r.Patterns[i][j] = r.Patterns[i][j] / max
        }
    }
    return r
}

func (r Kohonen) Train(Iteractions int) Kohonen{
    r = r.NormalisePatterns()

    for inter := 1; inter <= Iteractions; inter++ {
        for i := 0; i < r.Numlines; i++ {
            r = r.TrainPattern(inter,r.Patterns[i],r.Result[i])
        }
    }
    return r
}

func (r Kohonen) TrainPattern(inter int, pattern []float64,patternexit []float64) (Kohonen){
    var winner neu.Neuron
    winner = r.Winner(pattern)
    
    aux:=r.Outputs

    for i := 0; i < r.length; i++ {
        for j := 0; j < r.length; j++ {
            aux[i][j] = r.Outputs[i][j].UpdateWeigths(pattern, patternexit,winner,inter)
        }   
    }
    r.iteration = inter

    r.Outputs = aux
    
    return r
}

func (r Kohonen) Test(pattern []float64) {
    
    // normaliza a entrada
    for i := 0; i < r.dimensions; i++ {
        pattern[i] = pattern[i] / r.maxnormal[i]
    }

    neu := r.Winner(pattern)
    max:=0
    fmt.Printf("[")
    for i := 0; i < len(neu.ExitWeights); i++ {
        if(neu.ExitWeights[max] <= neu.ExitWeights[i]){
            max = i
        }
        fmt.Printf(" %d ",int(neu.ExitWeights[i]*100))
    }

    fmt.Printf("]\n")
    fmt.Println(r.Labels[max])
}

func (r Kohonen) Winner(pattern []float64) neu.Neuron{
    var winner neu.Neuron

    min:= math.Sqrt(float64(len(pattern)))

    for i := 0; i < r.length; i++ {
        for j := 0; j < r.length; j++ {
            
            dist:=r.Distance(r.Outputs[i][j].Weights,pattern)

            if(dist< min){
                min = dist
                
                winner = r.Outputs[i][j]
            }
        }
    }
    return winner
}

func (r Kohonen) Distance(v1 []float64,v2 []float64)  float64{
    v:=float64(0)

    for i := 0; i < len(v1); i++ {
        v+=math.Pow(v1[i] -v2[i],2)
    }
    return math.Sqrt(v)
}